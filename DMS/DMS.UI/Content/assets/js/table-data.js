
"use strict";

function dynAddRow($tableSelector)
{
    this.parentTable = $tableSelector;
    this.tempate = $(this.parentTable).data('template');

    this.evt = this.parentTable + '.add-row';
    this.addRow = function (e) {
        // alert("add-row click");

        if (e) { e.preventDefault(); }
        var $temp = $('#' + self.tempate).val();
        if ($temp == undefined) { return;}
        var totalRow = $(self.parentTable + ' tbody tr').length;
        $temp = $temp.replace(/{i}/g, totalRow);
        var $table = $(self.parentTable + ' tbody').append('<tr>' + $temp + '</tr>');

        applySelect();

        //focus on first input
        console.log($(self.parentTable + ' tbody tr:last-child').find('.search-select').eq(0));
        $(self.parentTable + ' tbody tr:last-child').find('.search-select').eq(0).focus();


    };
    this.removeRow = function (e) {
        if (e) { e.preventDefault(); }
        $(this).closest("tr").remove(); // remove row
        return false;
    };

    var self = this;

    $(document).on('click', self.parentTable + " .add-row", self.addRow);
    $(document).on('click', self.parentTable + ' .remove-row', self.removeRow);
    
}

// Handler for .ready() called.
