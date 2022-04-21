var UITreeview = function() {
	"use strict";
	//function to initiate jquery.dynatree
	var runTreeView = function() {
		//Default Tree
        $('#tree').jstree({
			"core" : {
				"themes" : {
					"responsive" : false
				},
				check_callback: true,
				"multiple": false
			},
			"types" : {
			    "#" : {
			        "max_children" : 99,
			        "max_depth" : 4,
			        "valid_children" : ["root"]
			    },
				"default" : {
					"icon" : "fa fa-folder text-yellow fa-lg"
				},
				"file" : {
					"icon" : "fa fa-file text-azure fa-lg"
				}
			},
			"plugins" : ["types", "contextmenu", "wholerow"]
		});

	};
	return {
		//main function to initiate template pages
		init : function() {
			runTreeView();
		}
	};
}();
