
function ShowHideColumn(target, colIndex) {
    var mTable = '#' + target;
    var myDataTable = $(mTable).DataTable();
    // Get the column API object
    var column = myDataTable.column(colIndex);

    // Toggle the visibility
    column.visible(!column.visible());
}
$(document).on('click', 'a.toggle-vis', function (e) {
    e.preventDefault();
    ShowHideColumn($(this).data('target'), $(this).attr('data-column'));
});
$(document).on('change', '.toggle-vis', function (e) {
    e.preventDefault();
    ShowHideColumn($(this).data('target'), $(this).attr('data-column'));
});
var runApplyShowHideColumns = function () {
    $('.showHideColumns').each(function (item, index) {
        if ($(this).hasClass('toggleApplied')) { return; }
        var t = $(this).data('target');
        var thDrop = $(this).find('.dropdown-menu');
        thDrop.html('');
        $(this).addClass('toggleApplied');
        if ($('#' + t).length >= 1) {
            $('#' + t).find('thead th').each(function (index) {
                var isVisible = $(this).hasClass('hide');
                thDrop.append('<li><a href="#"><input type="checkbox" ' + (isVisible ? "checked" : "") + ' class="toggle-vis" data-column="' + index + '" data-target="' + t + '" />' + $(this).text() + '</a></li>');
            });
        }
    });
}
//function ReApplyDataTable($selector) {
//    $selector = $selector || '.dataTable';
//    oTable = $($selector).dataTable({
//        select: true,
//        "aoColumnDefs": [{
//            "aTargets": [0]
//        }],
//        "oLanguage": {
//            "sLengthMenu": "Show _MENU_ Rows",
//            "sSearch": "",
//            "oPaginate": {
//                "sPrevious": "",
//                "sNext": ""
//            }
//        },
//        "aaSorting": [[0, 'asc']],
//        // set the initial value
//        "lengthMenu": [
//            [5, 10, 25, 50, 100, 500, 1000, -1],
//            [5, 10, 25, 50, 100, 500, 1000, "All"]
//        ],
//        "iDisplayLength": 100,
//        //scrollX: true,
//        dom: 'Blfrtpi',
//        fixedHeader: true,
//        buttons: ['copy', 'csv', 'excel', 'pdf', 'print']
//    });
//    $('.dataTables_wrapper .dataTables_filter input').addClass("form-control input-sm").attr("placeholder", "Search");
//    runApplyShowHideColumns();
//}
//var oTable;

var runToggle = function () {
    $("[data-toggle='toggle']").bootstrapToggle('destroy')
    $("[data-toggle='toggle']").bootstrapToggle();
}



function ReApplyMajorJSComponents() {

    //ReApplyDataTable();
    $("[data-toggle=tooltip]").tooltip();

    $('.nepali-calendar').nepaliDatePicker({

        format: "yyyy-mm-dd",
        npdMonth: true,
        npdYear: true,
        npdYearCount: 50,
    });

    //$(".add-row").on("click", function () {
    //    console.log("remove re remove");
    //    var GroupDepositAdd = new dynAddRow('#sample_11');
    //});


    //$(".remove-row").on("click", function () {
    //    console.log("remove re remove");
    //    var GroupDepositRemove = new dynAddRow('#sample_11');
    //});

    applySelect();

    //$('.summernote').summernote('fontName', 'Times New Roman');
    $('.datepicker').datepicker({
        format: 'yyyy-mm-dd',
        // "setDate": new Date(),
        "autoclose": true
    });//.datepicker("setDate", new Date());

    $('.date-picker').datepicker({
        format: 'yyyy-mm-dd',
        // "setDate": new Date(),
        "autoclose": true
    });//.datepicker("setDate", new Date());

    runToggle();

}
$(document).ready(function () {
    //Main.init();
    TableExport.init();
    //Calendar.init();
    //UIModals.init();
    // console.log('init...');

    $('#cmdPrint, .cmdPrint').on('click', function (e) {
        e.preventDefault();
        window.print();
    });

    ReApplyMajorJSComponents();
});

$(document).on('submit', 'form.ajaxSubmit', function (e) {

    console.log('checking....', $(this).valid());
    if (!$(this).valid()) {
        //validation failed
        return false;
    }

    $(this).addClass('Submitting');
    var callBack = window[$(this).data('callback')];
    console.log("callback=" + typeof callBack);
    var form = $(this)[0];
    $postURL = $(this).attr('action');
    var data = new FormData(form);
    //data = $(this).serialize();
    showLoading();
    $.ajax({
        type: "POST",
        enctype: 'multipart/form-data',
        url: $postURL,
        data: data,
        processData: false,
        contentType: false,
        cache: false,
        timeout: 600000,
        success: function (response) {
            hideLoading();
            console.log(response);
            var stat = response.Status || true;
            console.log("stat=" + stat);
            if (stat) {
                //$(".ajaxSubmit")[0].reset();
                console.log("check");
                console.log("callback=" + typeof callBack);
                $.toaster(response.msg);

                if (typeof callBack == "function") {
                    callBack(response);
                }
            }
        }, error: function (err) {
            hideLoading();
            $.toaster(err);
            console.log("error", err);
        }
    });

    $(this).addClass('showingConfirmation');
    console.log('showing modal');
    $('#myModal').modal("show");
    e.preventDefault();
    return false;

});



$(document).ready(function () {
    $(document).on("click", "#ModalSubmitButton", function () {
        if ($("#modal_submit_button").length <= 0) {
            $("#modal_submit_button").unbind('click');
        }
        //$("#SubmitButton").prop("disabled", true);
        $('form.showingConfirmation').addClass('confirmed').submit();
        if ($("#myModal").length) {
            $('#myModal').modal("hide");
        }
    });
    $('#myModal *[data-dismiss="modal"]').on('click', function () {
        $('form.showingConfirmation').removeClass('showingConfirmation');
    });
    $('#myModal').on('hidden.bs.modal', function () {
        $('form.showingConfirmation').removeClass('showingConfirmation');
    });
});
/////added new 11 jul 2019
//$('#myModal').modal({
//    backdrop: 'static',
//    keyboard: false
//})

//js to load target url in load
$(document).ready(function () {
    $('.ajaxLoad').each(function (index, item) {
        $(this).load($(this).data('url'));
    });

    $(document).on('click', 'a.ajaxCall', function (e) {
        var $url = $(this).attr('href');
        var $callback = window[$(this).data('callback')];
        e.preventDefault();
        showLoading();
        $.ajax({
            url: $url,
            success: function (response) {
                hideLoading();
                $.toaster(response.msg);
                if (typeof $callback == "function") {
                    $callback(response);
                }
            }, error: function (error) {
                hideLoading();
                console.log(error);
            }
        });


        return false;
    });
    $('*[data-poload]').hover(function () {
        var e = $(this);
        e.off('hover');
        $.get(e.data('poload'), function (d) {
            e.popover({ content: d, trigger: 'hover' });//.popover('show');
        });
    });
});

function refreshLandBuilding(response) {
    $('#landAndBuildingBlock').load($('#landAndBuildingBlock').data('url'));
    $.toaster("Saved successfully");
}


$(document).on('change', '.pg_birthdate', function () {
    $parentRow = $(this).closest('tr');
    console.log($(this).val());
    var d = new Date();
    var end = new Date($(this).val());
    var start = new Date(d);
    var diff = (start - end);
    console.log(diff);
    var days = diff / (1000 * 60 * 60 * 24);
    //assume 365.25 days per year
    var years = days / 365.25;
    $parentRow.find('.pg_age').val(parseInt(years));
    console.log(years);
}).trigger('change');


//
$(document).on('change', ".GetTargetedDropDownDataByID", function () {
    var id = $(this).find(":selected").val();
    var data_target = "#" + $(this).data("target");
    var data_url = $(this).data("url");
    Search(id, data_url,data_target);
});

function Search(id, data_url,data_target) {
    $.ajax({
        dataType: 'json',
        data: { id: id },
        url: data_url,
        success: function (response) {
            console.log(response);
            var rData = SetDropDownData(response.data);
            $(data_target).html(rData);
        }
    });
}

function SetDropDownData(Data) {
    var Result = '';
    if (Data.length > 0) {
        Result += '<option value="" readonly>-- Select --</option>';
        for (var i = 0; i < Data.length; i++) {

            if (Data[i] != null) {
                Result += '<option value="' + Data[i].BindField + '">' +
                    Data[i].DisplayField +
                    '</option>';
            }
        }
        console.log(Result);
        return Result;
    }
    return Result;
}


//for old address
$(document).ready(function () {
    //$("#old_municipality").attr('disabled', 'disabled');
    //$("#old_district").attr('disabled', 'disabled');

    $(document).on('change', ".old_zone", function () {
        var zone = $(this).find(":selected").val();
        console.log("val=" + zone);
        OldDistrictSearch(zone, "#" + $(this).data("target"));
        $("#" + $(this).data("target")).removeAttr('disabled');
    });

    $(document).on('change', '.old_district', function () {
        old_district_value = $(this).find(":selected").val()
        var district = old_district_value;
        console.log("val=" + district);
        OldMunicipalityTypeSearch(district, "#" + $(this).data("target"));
        $("#" + $(this).data("target")).removeAttr('disabled');
    });

    var old_district_value;

    $(document).on('change', ".old_mun_type", function () {
        var mun_type = $(this).find(":selected").val();
        var district = old_district_value;
        console.log("val=" + district);
        OldMunicipalitySearch(district, mun_type, "#" + $(this).data("target"));
        $("#" + $(this).data("target")).removeAttr('disabled');
    });

    function OldDistrictSearch(zone, dis_id) {
        $.ajax({
            dataType: 'json',
            data: { set38uin: zone },
            url: Config.Url.GetOldDistrict,

            success: function (districts) {
                console.log(districts);
                var rData = abc(districts);
                $(dis_id).html(rData);
            }
        });
    }

    function OldMunicipalitySearch(district, mun_type, mun_id) {
        $.ajax({
            dataType: 'json',
            data: { set39uin: district, set40type: mun_type },
            url: Config.Url.GetOldMunicipality,

            success: function (municipalities) {
                console.log(municipalities);
                var rData = OldMunicipal(municipalities);
                $(mun_id).html(rData);
            }
        });
    }

    function OldMunicipalityTypeSearch(district, mun_id) {
        $.ajax({
            dataType: 'json',
            data: { set39uin: district },
            url: Config.Url.GetOldMunicipalityTypesDetails,

            success: function (municipalities) {
                console.log(municipalities);
                var rData = OldMunicipalType(municipalities);
                $(mun_id).html(rData);
            }
        });
    }

    function OldMunicipalType(Data) {
        var Result = '';
        if (Data.length > 0) {
            Result += '<option value="" readonly>--Select Municipality Type--</option>';
            for (var i = 0; i < Data.length; i++) {

                if (Data[i] != null) {
                    Result += '<option value="' + Data[i].set05uin + '">' +
                        Data[i].set05title +
                        '</option>';
                }
            }
            console.log(Result);
            return Result;
        }
        return Result;
    }

    function OldMunicipal(Data) {
        var Result = '';
        if (Data.length > 0) {
            Result += '<option value="" readonly>--Select Municipality--</option>';
            for (var i = 0; i < Data.length; i++) {

                if (Data[i] != null) {
                    Result += '<option value="' + Data[i].set40uin + '">' +
                        Data[i].set40title +
                        '</option>';
                }
            }
            console.log(Result);
            return Result;
        }
        return Result;
    }

    function abc(Data) {
        var Result = '';
        if (Data.length > 0) {
            Result += '<option value="" readonly>--Select District--</option>';
            for (var i = 0; i < Data.length; i++) {

                if (Data[i] != null) {
                    Result += '<option value="' + Data[i].set39uin + '">' +
                        Data[i].set39title +
                        '</option>';
                }
            }
            console.log(Result);
            return Result;
        }
        return Result;
    }
});
//--old address