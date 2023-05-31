var contact = {
    init: function () {
        contact.registerEvent();
    },
    registerEvent: function () {
        $('#mbtnSend').off('click').on('click', function () {
            var username = $('#mphone').val();
            var pass = $('#mpassword').val();
            var commandcode = $('#mcommandcode').val();
            $('#mloader').css('display', 'inline-block');

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
                        $('#mtxtmess').css('display', 'inline-block');
                        $('#mtxtmess').html(response.message);
                        $('#mloader').css('display', 'none');
                    }
                    contact.resetForm();
                },
            });
        });
    },
    resetForm: function () {
        $('#mform-contact')[0].reset();
    }
}
contact.init();