

// validation check
$(document).ready(function () {

    $.validator.setDefaults({
        debug: true,
        success: "valid"
    });
    $xyz = false;

    $abc = $("#ChangePassword").validate({
        onfocus: function (element) {
            this.element(element);
        },
        rules: {
           
            Password: {
                required: true,
                pwcheck: true,
                minlength: 8
            },
            ConfirmPassword: {
                required: true,
                equalTo: "#Password"
            },
        },
        messages: {
            Password: "",
            ConfirmPassword: "Password doesn't match.",
        },
        showErrors: function (errorMap, errorList) {
            this.defaultShowErrors();
            // label.parent().addClass('error');
            $xyz = false;

        },
        success: function (label, element) {
            label.parent().removeClass('error');
            label.remove();
        },
        submitHandler: function (form) {
            if ($(form).valid())
                //dialogBoxDemo();
                form.submit();
            return false; // prevent normal form posting
        }

    });



    $.validator.addMethod("pwcheck", function (value) {
       
       // console.log("check", /^(?=.*[a-z])[A-Za-z0-9\d=!\-@._*]+$/.test(value));
       // return /^[A-Za-z0-9\d=!\-@._*]*$/.test(value);
        //return /^(?=.*\d)(?=.*[a-z])(?=.* [A-Z]).{ 8,}+$/.test(value);

        //return /^[A-Za-z0-9\d=!\-@._*]*$/.test(value) // consists of only these
        //        && /[a-z]/.test(value) // has a lowercase letter
        //    && /\d/.test(value) // has a digit
        //    && /^[^A-Z0-9]+$/.test(value)
        ////    && /^[a-z\[!@# $%&*\/?=_.,:;\\\]"-]+$/.test(value)

        var pattern = /^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%&_]).*$/;
        if (pattern.test(value)) {
            return true;
        } else {
            return false;
        }


    });

});




