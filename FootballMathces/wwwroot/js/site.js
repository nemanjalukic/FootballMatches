// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    var placeholderElement = $('#modal-placeholder');
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url);
        $.get(decodedUrl).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });

    });
    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();

        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        console.log(form);
        var dataToSend = form.serialize();
        var dataSend = new FormData(form[0]);
        console.log(actionUrl);
        $.ajax({
            type: 'POST',
            url: actionUrl,
            data: dataSend,
            contentType: false,
            processData: false,
            success: function (data) {
                var newBody = $('.modal-body', data);
                var isValid = newBody.find('[name="IsValid"]').val() != 'True';
                if (isValid) {
                    placeholderElement.find('.modal-body').replaceWith(newBody);
                }
                else {
                    placeholderElement.find('.modal').modal('hide');
                    var url = actionUrl.split('/');
                    var urlAction = '/' + url[1];
                    location.href = urlAction;
                }
            },
            error: function (err) {
                console.log(err)
            }
        });
    });
    placeholderElement.on('click', '[data-save="button"]', function (event) {
        event.preventDefault();

        var form = $(this).parent();
        var actionUrl = form.attr('action');
        //console.log(form);
        var dataToSend = form.serialize();
        var dataSend = new FormData(form[0]);
        console.log(actionUrl);
        $.ajax({
            type: 'POST',
            url: actionUrl,
            data: dataSend,
            contentType: false,
            processData: false,
            success: function (data) {

                    placeholderElement.find('.modal').modal('hide');
                    var url = actionUrl.split('/');
                    var urlAction = '/' + url[1];
                    location.href = urlAction;
                
            },
            error: function (err) {
                console.log(err)
            }
        });
    });
    placeholderElement.on('change', '#inputButton', function (event) {

        const file = $('#inputButton')[0].files[0];
        $('#inputLabel').html($('#inputButton')[0].files[0].name);
        var reader = new FileReader();
        reader.addEventListener("load", function () {
            var preview =$('#imagePreview');
            console.log("radi2");
            $('#imagePreview').attr('src', reader.result);
        }, false);
        if (file) {
            console.log("radi");
            reader.readAsDataURL(file);
        }
        else {
            console.log("ne radi");
        }

    });
    placeholderElement.on('change', 'select', function (event) {
        event.preventDefault();

        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        console.log(form);
        var url = actionUrl.split('/');
        var urlAction = '/' + url[1]+'/Edit';
        var dataSend = new FormData(form[0]);
        console.log(actionUrl);
        $.ajax({
            type: 'POST',
            url: urlAction,
            data: dataSend,
            contentType: false,
            processData: false,
            success: function (data) {
                var newBody = $('.modal-body', data);
                placeholderElement.find('.modal-body').replaceWith(newBody);
                
            },
            error: function (err) {
                console.log(err)
            }
        });


    });
    placeholderElement.on('click', 'button', function (event) {
        var placeholderElement2 = $('#modal-placeholder2');
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url);
        $.get(decodedUrl).done(function (data) {
            placeholderElement2.html(data);
            placeholderElement2.find('.modal').modal('show');
        });

    });
    var placeholderElement2 = $('#modal-placeholder2');
    placeholderElement2.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();

        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        console.log(form);
       
        var dataSend = new FormData(form[0]);
        console.log(actionUrl);
        $.ajax({
            type: 'POST',
            url: actionUrl,
            data: dataSend,
            contentType: false,
            processData: false,
            success: function (data) {
                console.log(data);
                placeholderElement2.find('.modal').modal('hide');
                var newBody = $('.modal-body', data);
                placeholderElement.find('.modal-body').replaceWith(newBody);
                

            },
            error: function (err) {
                console.log(err)
            }
        });
    });
});