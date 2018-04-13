"use strict";

var ah = (function () {
    var model = {
        images: ko.observableArray(),
        selectedImage: ko.observable(),
        displaySelectedImage: ko.observable(false),
        displayExif: ko.observable(false),
        getExif: getExif,
        highlightImageHandler: highlightImage,
        editingMode: ko.observable(false),
        editDescription: function (obj, event) {
            event.preventDefault();
            model.editingMode(true);
        },
        saveDescription: function (obj, event) {
            event.preventDefault();
            model.editingMode(false);
            sendAjax('POST', 'Home/UpdateImage', model.selectedImage);
        },
        file: ko.observable(),
        uploadImage: uploadFile
    };

    function highlightImage(obj) {
        $('.selected').removeClass('selected');

        model.displaySelectedImage(false);
        model.selectedImage = obj;
        model.displaySelectedImage(true);

        var imgElem = $('img')[model.images.indexOf(model.selectedImage)];
        imgElem.classList.add('selected');

        model.displayExif(false);
        model.displayExif(!!model.selectedImage.Exif);
    }

    function getAll() {
        sendAjax('GET', 'Home/Get')
            .then(addAll, (error) => alert(error.message));
    }

    function uploadFile() {
        var fileData = new FormData();
        fileData.append(model.file.name, model.file);

        sendAjax('POST', 'Home/Upload', fileData, { contentType: false, processData: false })
            .then(function (data) {
                addAll(data);
                highlightImage(data[0]);
            });
    }

    function sendAjax(httpMethod, url, data, options) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: url ? "/" + url : "",
                type: httpMethod,
                data: data,
                contentType: options ? options.contentType : 'application/x-www-form-urlencoded; charset=UTF-8',
                processData: options ? options.processData : true,
                success: resolve,
                error: reject
            });
        });
    }

    function addAll(data) {
        model.images.removeAll();
        for (var i = 0; i < data.length; i++) {
            model.images.push(data[i]);
        }
    }

    function getExif() {
        var exif = '';
        for (let key in model.selectedImage.Exif) {
            if (!!model.selectedImage.Exif[key]) {
                exif += '<small>' + key + '</small>' + '   '
                    + '<small>' + parseExifData(key, model.selectedImage.Exif[key]) + '</small>' + '<br>';
            }
        }

        return exif;
    }

    function parseExifData(key, value) {
        if (key == "DateAndTime") {
            var dateString = value.match(/\d+/);
            var date = new Date(+dateString[0]).toLocaleDateString("en-us",
                { day: "numeric", year: "numeric", month: "short" });
            var dateParts = date.match(/\w+/g);
            return `${dateParts[1]} ${dateParts[0]} ${dateParts[2]}`;
        }

        return value;
    }

    $(document).ready(function () {
        ko.applyBindings(model);
        getAll();
    });

    return {
        model: model
    }
})();
