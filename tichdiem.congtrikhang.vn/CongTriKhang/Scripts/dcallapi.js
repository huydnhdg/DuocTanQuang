var contact = {
    init: function () {
        contact.registerEvent();
    },
    registerEvent: function () {
        $('#dbtnSend').off('click').on('click', function () {
            var username = $('#dphone').val();
            var pass = $('#dpassword').val();
            var commandcode = $('#dcommandcode').val();
            $('#dloader').css('display', 'inline-block');

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
                        $('#dtxtmess').css('display', 'inline-block');
                        $('#dtxtmess').html(response.message);
                        $('#dloader').css('display', 'none');
                    }
                    contact.resetForm();
                },
            });
        });
    },
    resetForm: function () {
        $('#dform-contact')[0].reset();
    }
}
contact.init();