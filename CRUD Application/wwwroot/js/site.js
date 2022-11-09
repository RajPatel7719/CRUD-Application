// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//$(document).on("change", ".uploadProfileInput", function () {
//    var triggerInput = this;
//    var currentImg = $(this).closest(".pic-holder").find(".pic").attr("src");
//    var holder = $(this).closest(".pic-holder");
//    var wrapper = $(this).closest(".profile-pic-wrapper");
//    $(wrapper).find('[role="alert"]').remove();
//    triggerInput.blur();
//    var files = !!this.files ? this.files : [];
//    if (!files.length || !window.FileReader) {
//        return;
//    }
//    if (/^image/.test(files[0].type)) {
//        // only image file
//        var reader = new FileReader(); // instance of the FileReader
//        reader.readAsDataURL(files[0]); // read the local file

//        reader.onloadend = function () {
//            $(holder).addClass("uploadInProgress");
//            $(holder).find(".pic").attr("src", this.result);
//            $(holder).append(
//                '<div class="upload-loader"><div class="spinner-border text-primary" role="status"><span class="sr-only">Loading...</span></div></div>'
//            );

//            // Dummy timeout; call API or AJAX below
//            setTimeout(() => {
//                $(holder).removeClass("uploadInProgress");
//                $(holder).find(".upload-loader").remove();
//                // If upload successful
//                if (Math.random() < 0.9) {
//                    $(wrapper).append(
//                        '<div class="snackbar show" role="alert"><i class="fa fa-check-circle text-success"></i> Profile image updated successfully</div>'
//                    );

//                    // Clear input after upload
//                    $(triggerInput).val("");

//                    setTimeout(() => {
//                        $(wrapper).find('[role="alert"]').remove();
//                    }, 3000);
//                } else {
//                    $(holder).find(".pic").attr("src", currentImg);
//                    $(wrapper).append(
//                        '<div class="snackbar show" role="alert"><i class="fa fa-times-circle text-danger"></i> There is an error while uploading! Please try again later.</div>'
//                    );

//                    // Clear input after upload
//                    $(triggerInput).val("");
//                    setTimeout(() => {
//                        $(wrapper).find('[role="alert"]').remove();
//                    }, 3000);
//                }
//            }, 1500);
//        };
//    } else {
//        $(wrapper).append(
//            '<div class="alert alert-danger d-inline-block p-2 small" role="alert">Please choose the valid image.</div>'
//        );
//        setTimeout(() => {
//            $(wrapper).find('role="alert"').remove();
//        }, 3000);
//    }
//});

var img_object = $(".img-object"),
    media_input = $("#media-input");

media_input.change(function () {
    if (this.files && this.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            img_object.attr("src", e.target.result);
        };

        reader.readAsDataURL(this.files[0]);
    }
});

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imagePreview').css('background-image', 'url(' + e.target.result + ')');
            $('#imagePreview').hide();
            $('#imagePreview').fadeIn(650);
        }
        reader.readAsDataURL(input.files[0]);
    }
}
$("#imageUpload").change(function () {
    readURL(this);
});

const rmCheck = document.getElementById("remember"),
    userName = document.getElementById("userName"),
    password = document.getElementById("password");

if (getCookie("rememberMe") !== null) {
    rmCheck.setAttribute("checked", "checked");
    userName.value = getCookie("userName");
    password.value = getCookie("password");
} else {
    rmCheck.removeAttribute("checked");
    userName.value = "";
    password.value = "";
}

function lsRememberMe() {
    if (rmCheck.checked && userName.value !== "" && password.value !== "") {
        setCookie("userName", userName.value, 30)
        setCookie("password", password.value, 30)
        setCookie("rememberMe", rmCheck.value, 30)
    } else {
        removeCookie("userName");
        removeCookie("password");
        removeCookie("rememberMe");
    }
}

function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}
function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}
function removeCookie(name) {
    document.cookie = name + '=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}