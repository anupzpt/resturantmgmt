var Index = function() {"use strict";


	// function to initiate Report Range Date
	var runReportRange = function() {
		if($('#reportrange').length) {
			$('#reportrange').daterangepicker({
				ranges: {
					'Today': [moment(), moment()],
					'Yesterday': [moment().subtract('days', 1), moment().subtract('days', 1)],
					'Last 7 Days': [moment().subtract('days', 6), moment()],
					'Last 30 Days': [moment().subtract('days', 29), moment()],
					'This Month': [moment().startOf('month'), moment().endOf('month')],
					'Last Month': [moment().subtract('month', 1).startOf('month'), moment().subtract('month', 1).endOf('month')]
				},
				startDate: moment().subtract('days', 29),
				endDate: moment()
			}, function(start, end) {
				$('#reportrange span').html(start.format('MMM D, YYYY') + ' - ' + end.format('MMM D, YYYY') + ' ');
			});
		}

	};
	// function to animate CoreBox Icons
	var runCoreBoxIcons = function() {
		$(".core-box").on("mouseover", function() {
			$(this).find(".icon-big").addClass("tada animated");
		}).on("mouseleave", function() {
			$(this).find(".icon-big").removeClass("tada animated");
		});
	};
	// function to activate animated Clock and Actual Date
	var runClock = function() {
		function update() {
			var now = moment(), second = now.seconds() * 6, minute = now.minutes() * 6 + second / 60, hour = ((now.hours() % 12) / 12) * 360 + 90 + minute / 12;
			$('#hour').css({
				"-webkit-transform": "rotate(" + hour + "deg)",
				"-moz-transform": "rotate(" + hour + "deg)",
				"-ms-transform": "rotate(" + hour + "deg)",
				"transform": "rotate(" + hour + "deg)"
			});
			$('#minute').css({
				"-webkit-transform": "rotate(" + minute + "deg)",
				"-moz-transform": "rotate(" + minute + "deg)",
				"-ms-transform": "rotate(" + minute + "deg)",
				"transform": "rotate(" + minute + "deg)"
			});
			$('.clock #second').css({
				"-webkit-transform": "rotate(" + second + "deg)",
				"-moz-transform": "rotate(" + second + "deg)",
				"-ms-transform": "rotate(" + second + "deg)",
				"transform": "rotate(" + second + "deg)"
			});
		}

		function timedUpdate() {
			update();
			setTimeout(timedUpdate, 1000);
		}

		timedUpdate();
		$(".actual-date .actual-day").text(moment().format('DD'));
		$(".actual-date .actual-month").text(moment().format('MMMM'));
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
		init: function() {
			runReportRange();
			runClock();
			runCoreBoxIcons();
			runEventsSlider();
		}
	};
}();
