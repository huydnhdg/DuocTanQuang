var contact = {
    init: function () {
        contact.registerEvent();
    },
    registerEvent: function () {
        $('#bbtnSend').off('click').on('click', function () {
            var username = $('#bphone').val();
            var pass = $('#bpassword').val();
            var commandcode = $('#bcommandcode').val();
            $('#bloader').css('display', 'inline-block');

            $.ajax({
                url: '/Default/CallApi',
                type: 'POST',
                dataType: 'json',
                data: {
                    username: username,
                    pass: pass,
                    commandcode: commandcode,
                },
                success: function (response) {
                    if (response.success) {
                        $('#boxcom').css('display', 'inline-block');
                        $('#btxtmess').css('display', 'inline-block');
                        $('#btxtmess').html(response.message);
                        $('#bloader').css('display', 'none');
                    }
                    //contact.resetForm();
                },
            });
        });
    },
    resetForm: function () {
        //$('#bform-contact')[0].reset();
    }
}
contact.init();