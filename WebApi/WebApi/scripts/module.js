var ah = (function() {
    var model = {
        images: ko.observableArray(),
        selectedImage: ko.observable(),
        displaySelectedImage: ko.observable(false),
        displayExif: ko.observable(false),
        getExif: function() {
            var exif = '';
            for (let key in model.selectedImage.Exif) {
                if (!!model.selectedImage.Exif[key]) {
                    exif += '<small>' + key + '</small>' + '   '
                        + '<small>' + parseExifData(key, model.selectedImage.Exif[key]) + '</small>' + '<br>';
                }
            }

            return exif;
        },
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
        }
    };

    function parseExifData(key, value) {
        return value;
    }

    function highlightImage() {
        $('.selected').removeClass('selected');

        model.displaySelectedImage(false);
        model.selectedImage = this;
        model.displaySelectedImage(true);

        var index = model.images.indexOf(this);
        var imgElem = $('img')[index];
        imgElem.classList.add('selected');

        model.displayExif(false);
        model.displayExif(!!model.selectedImage.Exif);
    }

    function getAll() {
        sendAjax('GET', 'Home/Get')
            .then(function(data) {
                model.images.removeAll();
                for (var i = 0; i < data.length; i++) {
                    model.images.push(data[i]);
                }
                model.selectedImage = model.images[0];
            }, (error) => alert(error.message));
    }

    function sendAjax(httpMethod, url, data) {
        return new Promise(function(resolve, reject) {
            $.ajax({
                url: url ? "/" + url : "",
                type: httpMethod,
                data: data,
                success: resolve,
                error: reject
            });
        });
    }

    $(document).ready(function() {
        getAll();
        ko.applyBindings(model);
    });

    return {
        model: model,
        getAll: getAll
    }
})();
