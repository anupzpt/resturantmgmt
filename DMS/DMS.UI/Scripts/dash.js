var DMSIndex = function (pendingVal,rejectedVal,approvedVal) {
    "use strict";

	
	// function to initiate Chart 3
    var runChart3 = function (pendingVal, rejectedVal, approvedVal) {
		if($("#chart3 > svg").length) {
			var data = [{
				"label": "Approved",
				"value": approvedVal
			}, {
				"label": "Rejected",
				"value": rejectedVal
			}, {
				"label": "Pending",
				"value": pendingVal
			}];
			nv.addGraph(function() {
				var chart = nv.models.pieChart().margin({
					top: 5,
					right: 0,
					bottom: 0,
					left: 10
				}).x(function(d) {
					return d.label;
				}).y(function(d) {
					return d.value;
				}).showLabels(true)//Display pie labels
				.showLegend(false).labelType("key")//Configure what type of data to show in the label. Can be "key", "value" or "percent"
				.color(['#F3EDED', '#E0CDD1', '#CAABB0']);

				d3.select("#chart3 svg").datum(data).transition().duration(350).call(chart);
				nv.utils.windowResize(chart.update);

				return chart;
			});
		}
	};

	// function to activate owlCarousel in Appointments Panel
	var runEventsSlider = function() {
		var owlEvents = $(".appointments .e-slider").data('owlCarousel');
		$(".appointments .owl-next").on("click", function(e) {
			owlEvents.next();
			e.preventDefault();
		});
		$(".appointments .owl-prev").on("click", function(e) {
			owlEvents.prev();
			e.preventDefault();
		});
	};
	return {
	    init: function (pendingVal, rejectedVal, approvedVal) {
	        runChart3(pendingVal, rejectedVal, approvedVal);
			runEventsSlider();
		}
	};
}();
