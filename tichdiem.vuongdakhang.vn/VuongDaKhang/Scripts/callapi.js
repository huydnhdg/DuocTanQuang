var contact = {
    init: function () {
        contact.registerEvent();
    },
    registerEvent: function () {
        $('#btnSend').off('click').on('click', function () {
            var username = $('#phone').val();
            var pass = $('#password').val();
            var commandcode = $('#commandcode').val();
            $('#loader').css('display', 'inline-block');

            $.ajax({
                url: '/Default/CallApi',
                type: 'POST',
                dataType: 'json',
                data: {
                    username: username,
                    pass: pass,
                    commandcode: commandcode
                },
                success: function (response) {
                    if (response.success) {
                        $('#txtmess').css('display', 'inline-block');
                        $('#txtmess').html(response.message);
                        $('#loader').css('display', 'none');
                    }
                    contact.resetForm();
                },
            });
        });
    },
    resetForm: function () {
        $('#form-contact')[0].reset();
    }
}
contact.init();