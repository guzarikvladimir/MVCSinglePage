var ah = (function() {
    this.model = {
        images: ko.observableArray(),
        highlightImageHandler: function() {
            $('.selected').removeClass('selected');
            var index = model.images.indexOf(this);
            var imgElem = $('img')[index];
            imgElem.classList.add('selected');
        }
    };

    function getAll() {
        sendAjax('GET', 'Home/Get')
            .then(function(data) {
                model.images.removeAll();
                for (var i = 0; i < data.length; i++) {
                    model.images.push(data[i]);
                }
            }, (error) => alert(error.message));
    }

    function sendAjax(httpMethod, url) {
        return new Promise(function(resolve, reject) {
            $.ajax({
                url: url ? "/" + url : "",
                type: httpMethod,
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
