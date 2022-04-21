$(".Drop").select2();
var fromDate;
var toDate;





$(document).ready(function () {
    //when the page is done loading, disable autocomplete on all inputs[text]
    $('input[type="text"]').attr('autocomplete', 'off');
});



//// Regex Validation
numberRegex = /^[+-]?\d+(\.\d{0,2})?([eE][+-]?\d+)?$/;
integerRegex = /^[0-9]*$/;

decimalcount = 0;
$(document).on('keyup keydown', ".Number", function (e) {
    if (e.type == "keydown") {
        if (e.keyCode == "189") {
            ToasterNotification("Please Enter Non-Negative Numeric Value!");
            $(this).val(value);
            $(this).focus();
            value = "";
            return false;
        }
        value = $(this).val();
    } else {
        number = Number($(this).val());
        if (number != undefined) {
            if (!numberRegex.test(number) || number < 0) {
                ToasterNotification("Please Enter Numeric Value!");
                $(this).val(value);
                $(this).focus();
                value = "";
                return false;
            }
        }
    }
});



$(document).on('keyup keydown', ".Integer", function (e) {
    if (e.type == "keydown") {
        if (e.keyCode == "189") {
            ToasterNotification("Please Enter Non-Negative Numeric Value!");
            $(this).val(value);
            $(this).focus();
            value = "";
            return false;
        }
        value = $(this).val();
    } else {
        if (!integerRegex.test($(this).val())) {
            ToasterNotification("Please Enter Non-Decimal Numeric Value!");
            //    alert("Please Enter Non-Decimal Numeric Value!");
            $(this).val(value);
            $(this).focus();
            value = "";
            return false;
        }
    }
});












$(document).on("click", "#SubmitButton", function () {
    FormValues = $(this.form);
    
    if ($(".Initiate").length > 0) {
        if (  !($("#UploadedFileCode").length > 0)) {
            return alert("No File Available. Thanks!!");
        }
    }

    if ($(".DOB").length > 0) {
        AjaxVal = { DOB: $(".DOB").val() }
        $.post("/Employee/IsDOBValid/", AjaxVal, function (Data) {
            if (Data.IsValid) {
                if (FormValues.valid()) {
                    $("#msg").text("Submit");
                    $('#myModal').modal("show");
                }
            } else {
                $(".DOB").focus();
                return alert("Age Must Be Minimum 18 years.");
            }
        });
    } else {
        if (FormValues.valid()) {
            $("#msg").text("Submit");
            $('#myModal').modal("show");
        }
    }


});

$(document).on("click", "#ModalSubmitButton", function () {
    $("#SubmitButton").prop("disabled", true);
    if ($("#myModal").length) {
        $('#myModal').modal('toggle');
    }
});









var dateReg = /^\d{4}([./-])\d{2}\1\d{2}$/


////////////////// Nepdate in Textbox



       var d = new Date();

       var strDate = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate();
       var  TodayDate = AD2BS(strDate);
       

        $('.nepaliDate').nepaliDatePicker({
            npdMonth: true,
            npdYear: true,
            npdYearCount: 10
        });

        $('.nepaliDate').val(TodayDate);
 



        $(document).on('click',".ndp-date", function () { 
    
            fromDate = $("#FromDate").val();
            toDate = $("#ToDate").val();
            if (fromDate != undefined && toDate != undefined) {
             if (fromDate > toDate) {
                ToasterNotification("FromDate cannot exceed ToDate!");
                $("#FromDate").val(TodayDate);
                $("#ToDate").val(TodayDate);              
            }
        }
});



    