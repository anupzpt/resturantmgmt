// #region Menu
///===============================================================  menu =================================================================//

$("#menuTitle").on('change', function () {
    $("#descriptionMenu").val($(this).val());
});

$("#DropDownMenuName").on('change', function () {
    $("#descriptionMenu").val($(this).val());
});

$('#DropDownMenu').change(function () {
    if ((this).checked) {
        $('.hideTh').hide();
        $('.hideTh').hide();
        $('.parentName').show();
    }
    else {
        $('.hideTh').show();
        $('.hideTh').show();
        $('.parentName').hide();
    }
    $('#DropDownMenuName').val("");
    $('#menuTitle').val("");
    $('#controllerId').val("");
    $('#actionId').val("");
    $('#descriptionMenu').val("");
    $('#positonMenu').val("");
    $('#ParentMenuName').val("");
    $('#iconSelect').val(0);
    $('#statusMenu').val("checked", false);
});

$('#DropDownMenuName').change(function () {
    $('#descriptionMenu').val($(this).val());
});

$('#controllerId').change(function () {
    controllerId = $(this).val();
    controllerId = { controllerId: controllerId };
    if (controllerId.controllerId <= 0) {
        toastr.warning("Please Select Controller Name");
        //   alert("Please Select Controller Name");
        return $(this).focus();
    }
    else {
        Url = getUrl("MenuActionByController");
        $.post(Url, controllerId, function (data) {
            if (data.length > 0) {
                $('#actionId').removeAttr('disabled');
                var items = "<option value='0'><-- Select Action --></option>";
                $.each(data, function (i, item) {
                    items += "<option value=\"" + item.Id + "\">" + item.ActionName + "</option>";
                });
                $("#actionId").html(items);
            }
            else {
                toastr.info("Action List Are Not Available On This Controller");
            }
        }).fail(function () {
            alert("Error");
        });
    }

});

$('#addMenu').click(function () {
    menuTitle = $('#menuTitle').val();
    controllerId = $('#controllerId').val();
    actionId = $('#actionId').val();
    parentId = $('#ParentId').val();
    Area = $('#Area').val();
    MenuType = $('#MenuType').val();
    description = $('#descriptionMenu').val();
    positon = $('#positonMenu').val();
    dropDownName = $('#DropDownMenuName').val();
    iconname = $('#iconSelect').val();
    IsActiveMenu = $('#statusMenu').is(":checked");
    if ($('#DropDownMenu').is(":checked") === false) {
        if (menuTitle === "") {
            toastr.warning("Please Enter Menu Title ");
            return $('#menuTitle').focus();
        }
        if (controllerId <= 0) {
            toastr.info("Please Select Controller Name");
            $('#controllerId').val(0);
            return $('#controllerId').focus();
        }
        if (actionId <= 0) {
            toastr.info("Please Select Action Name");
            $('#actionId').val(0);
            return $('#actionId').focus();
        }
    }

    URL = getUrl("MenuCreatePartialList");// "/Menu/CreateMenuPartialList/";
    menu = {
        Title: menuTitle,
        _ControllerActionId: controllerId,
        ActionId: actionId,
        Description: description,
        Position: positon,
        DropDownName: dropDownName,
        IconName: iconname,
        IsActive: IsActiveMenu,
        ParentId: parentId,
        Area: Area,
        MenuType: MenuType,
    };
    try {
        $.post(URL, menu, function (data) {
            $('.addNewMenu').append(data);
            $('#ShowTable').show();
            $('#showButton').show();
            $('#DropDownMenuName').val("");
            $('#menuTitle').val("");
            $('#controllerId').val("");
            $('#actionId').val("");
            $('#descriptionMenu').val("");
            $('#positonMenu').val();
            $('#ParentMenuName').val();
            $('#iconSelect').val();
            $('#Area').val();
            $('#MenuType').val();
        }).fail(function () {
            alert("Error");
        });
    }
    catch (error) {
        window.location = getUrl("Error");
    }
});

$(document).on('click', '.removeList', function () {
    $(this).closest("tr").remove();
    index = ObjectList.indexOf(actionId);
    if (index >= 0) {
        ObjectList.splice(index, 1);
    }
});

$(document).on('click', '#updateMenuWithRole', function () {
    menuList = [];
    Id = $('#roleId').val();
    URL = getUrl("UserRoleEditRoleWitMenu");// "/UserRole/EditRoleWitMenu/";
    $("#treeview input[type=checkbox]").each(function () {
        checkOrNot = $(this).is(":checked");
        intermendit = $(this).prop("indeterminate");
        if (intermendit === true) {
            checkOrNot = intermendit;
        }
        menuId = $(this).attr('menuId');
        menu = { Id: menuId, RoleAccess: checkOrNot };
        menuList.push(menu);
    });
    $('#submitModal').modal('hide');
    onjs = { Id: Id, menuListVm: menuList }
    $.post(URL, { role: onjs }, function (data) {
        if (data === true) {
            HidePageLoadingSpinner();
            toastr.warning("Menu List With Role Updated Successfully");
        }
        else {
            toastr.warning("Failed To Upload Data");
        }
        window.location.href = getUrl("UserRoleIndex");
    }).fail(function () {
        alert("Failed");
    });
});

$(document).on('change', '.SelectAll', function () {
    thisId = $(this).attr('data-id');
    isChecked = $(this).is(':checked');
    if (isChecked) {
        return $('.Select' + thisId).prop("checked", true).change();
    }
    return $('.Select' + thisId).prop("checked", false).change();
});

///=============================================================== End menu =================================================================//

// #endregion


// #region ControllerAction

////======================================================== ControllerAction ================================================================//
///new Script for ControllerAction Delete
checkBoxCheckId = 0;
$("[data-toggle=tooltip]").tooltip();

function CkeckCheckBox(id) {
    checkBoxCheckId = (id);
}


$(document).on('click', '#UpdateData', function () {
    URl = getUrl('ControllerActionSaveEdit');// "/ControllerAction/SaveEdit/";
    obj = {
        controllerId: $('#controllerId').val(),
        controllerName: $('#controllerName').val(),
        accountName: $('#actionName').val(),
        Description: $('#DescriptionName').val()
    };
    $.post(URl, obj, function (data) {
        if (data === true) {
            LoadIndexData();
            return $('#MessageField').val("Eddted SuccessFully ");

        }
        $('#MessageField').val(data);
    }).fail(function () {
        alert("Error Occuresd");
    });

});

function LoadIndexData() {
    Url = getUrl("ControllerActionDisplayList");// "/ControllerAction/DisplayList/";
    $.post(Url, function (data) {
        $('#IndexPage').html(data);
    }).fail(function () {
        alert("Error Occured");
    });
}

ListId = 0;
$('#ShowList').click(function () {
    LoadIndexData();
});


////=============================================================== End ControllerAction ==============================================================//

// #region UserRole

$(document).on("change", '#roleName1', function () {
    url = getUrl('UserRoleDisplayTreeMenusForRole');
    roleName = $(this).val();
    $('.roleName1').val(roleName);
});


$('#createRoleWithMenu').click(function () {
    menuList = [];
    Id = $('#roleId').val();
    URL = getUrl("UserRoleManagementEditRoleWitMenu");
    $("#roleMenu input[type=checkbox]").each(function () {
        checkOrNot = $(this).is(":checked");
        intermendit = $(this).prop("indeterminate");
        if (intermendit === true) {
            checkOrNot = intermendit;
        }
        menuId = ($(this).attr('menuId'));
        menu = { Id: menuId, IsActive: checkOrNot };
        menuList.push(menu);
    });
    onjs = { Id: Id, menuWithRoleList: menuList }
    $.post(URL, { role: onjs }, function (data) {
        if (data === true) {
            alert("Data Updated");
        }
        else {
            alert("Failed To Updated Data");
        }
        window.location.href = getUrl("UserRoleManagementIndex");
    }).fail(function () {
        alert("Failed");
    });
});


$(document).on('click', '#checkAll', function () {
    $("#treeview").hummingbird("checkAll");
});
$(document).on('click', '#uncheckAll', function () {
    $("#treeview").hummingbird("uncheckAll");
});
$(document).on('click', '#collapseAll', function () {
    $("#treeview").hummingbird("collapseAll");
});
$(document).on('click', '#expandAll', function () {
    $("#treeview").hummingbird("expandAll");
});

$('#btnyesForMultipleMenu').click(function () {
    menuList = [];
    Id = $('#roleId').val();
    URL = getUrl("UserRoleEditRoleWitMenu");
    $("#roleMenu input[type=checkbox]").each(function () {
        checkOrNot = $(this).is(":checked");
        intermendit = $(this).prop("indeterminate");
        if (intermendit === true) {
            checkOrNot = intermendit;
        }
        menuId = ($(this).attr('menuId'));
        menu = { Id: menuId, IsActive: checkOrNot };
        menuList.push(menu);
    });
    onjs = { Id: Id, menuListVm: menuList }
    $.post(URL, { role: onjs }, function (data) {
        window.location.href = getUrl("UserRoleIndex");
    }).fail(function () {
        alert("Failed");
    });
});

$(document).ready(function () {
    $('.show-tab').on('click', function () {
        $($(this).data('target')).tab('show');
    });
});
// #endregion