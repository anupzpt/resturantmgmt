var Select2Helper = {};
Select2Helper.getOption = function(id, text, selected, disabled)
{
    return '<option value="' + id + '" ' + (selected ? "selected" : "") + ' ' + (disabled ? "disabled" : "") + '>' + text + '</option>';
}