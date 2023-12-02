/*
 *  Document   : login.js
 *  Author     : pixelcave
 *  Description: Custom javascript code used in Login page
 */
function reload_captcha() {
    var myimg = document.getElementById("myimg");
    myimg.style.display = "";
    myimg.src = "/Areas/Admin/Captcha.ashx?value=" + Math.random();
}
var Login = function() {

    // Function for switching form views (login, reminder and register forms)
    var switchView = function(viewHide, viewShow, viewHash){
        viewHide.slideUp(250);
        viewShow.slideDown(250, function(){
            $('input').placeholder();
        });

        if ( viewHash ) {
            window.location = '#' + viewHash;
        } else {
            window.location = '#';
        }
    };

    return {
        init: function() {
            /*
             *  Jquery Validation, Check out more examples and documentation at https://github.com/jzaefferer/jquery-validation
             */

            /* Login form - Initialize Validation */
            $('#form_login').validate({
                errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
                errorElement: 'div',
                errorPlacement: function(error, e) {
                    e.parents('.form-group > div').append(error);
                },
                highlight: function(e) {
                    $(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
                    $(e).closest('.help-block').remove();
                },
                success: function(e) {
                    e.closest('.form-group').removeClass('has-success has-error');
                    e.closest('.help-block').remove();
                },
                rules: {
                    'login_name': {
                        required: true
                    },
                    'login_password': {
                        required: true
                    }
                },
                messages: {
                    'login_name': 'Bạn chưa nhập username',
                    'login_password': {
                        required: 'Bạn chưa nhập mật khẩu'
                    }
                }
            });

            /* Reminder form - Initialize Validation */
            $('#form_reminder').validate({
                errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
                errorElement: 'div',
                errorPlacement: function(error, e) {
                    e.parents('.form-group > div').append(error);
                },
                highlight: function(e) {
                    $(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
                    $(e).closest('.help-block').remove();
                },
                success: function(e) {
                    e.closest('.form-group').removeClass('has-success has-error');
                    e.closest('.help-block').remove();
                },
                rules: {
                    'reminder_username': {
                        required: true
                    },
                    'reminder_captcha': {
                        required: true
                    }
                },
                messages: {
                    'reminder_username': 'Vui lòng nhập username của bạn',
                    'reminder_captcha': 'Vui lòng nhập mã bảo vệ bạn nhìn thấy bên dưới'
                }
            });
        }
    };
}();


var resetPassword_action;
function resetPassword(e) {
    e.preventDefault();
    if (!$("#form_reminder")[0].checkValidity()) {
        // ReSharper disable once PossiblyUnassignedProperty
        $("#form_reminder")[0].reportValidity();
        return;
    }
    var username = $("#reminder_username").val();
    var captcha = $("#reminder_captcha").val();
    //Hủy yêu cầu trước để đảm bảo chỉ xử lý lần click cuối
    if (resetPassword_action) resetPassword_action.abort();
    resetPassword_action = jQuery.ajax({
        url: "/Areas/Admin/Ajax/Users/Handler.ashx",
        type: "POST",
        dataType: "json",
        data: {
            "action": "ResetPassword",
            "username": username,
            "captcha": captcha
        },
        beforeSend: function () {
            loading(true);
        },
        complete: function () {
            loading(false);
        },
        success: function (res) {
            reload_captcha();
            $("#reminder_username").val("");
            $("#reminder_captcha").val("");
            $.bootstrapGrowl(res[1], { type: res[0] });
        },
        error: function () {
            $.bootstrapGrowl("Hệ thống đang bận, vui lòng thử lại sau!", { type: "warning", delay: 5000 });
        }
    });

}