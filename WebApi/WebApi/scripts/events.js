$(document).ready(function () {
    $('#btnUpload').click(function () {
        if (window.FormData !== undefined) {
            var fileUpload = $('#fileUpload').get(0);
            var file = fileUpload.files[0];

            var fileData = new FormData();
            fileData.append(file.name, file);

            $.ajax({
                url: '/Home/Upload',
                type: 'POST',
                contentType: false,
                processData: false,
                data: fileData,
                success: ah.getAll
            });
        } else {
            alert("FormData is not supported.");
        }
    });
});
