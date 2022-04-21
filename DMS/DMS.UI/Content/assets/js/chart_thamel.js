var Charts = function() {
	"use strict";
	var runChart1 = function() {
		/*These lines are all chart setup.  Pick and choose which chart features you want to utilize. */
		nv.addGraph(function() {

			var xLabels = ['Jan', 'Feb', 'Mar', 'Apr','May', 'Jun', 'Jul', 'Aug','Sep', 'Oct', 'Nov', 'Dec'];
			var yLabels = [0, 10000, 20000, 30000, 40000, 50000, 100000];
			var chart = nv.models.lineChart().margin({
				left : 100
			})//Adjust chart margins to give the x-axis some breathing room.
			.useInteractiveGuideline(true)//We want nice looking tooltips and a guideline!
			.transitionDuration(350)//how fast do you want the lines to transition?
			.showLegend(true)//Show the legend, allowing users to turn on/off line series.
			.showYAxis(true)//Show the y-axis
			.showXAxis(true)//Show the x-axis
			;

			chart.xAxis//Chart x-axis settings
			.axisLabel('Last 12 months')
			.tickFormat(function(d){return xLabels[d]; });

			chart
			.forceY(0, 500000)
			.yAxis//Chart y-axis settings
			.axisLabel('')
			.scale().domain([0, 500000])
			.tickFormat(d3.format('.02f'));

			/* Done setting the chart up? Time to render it!*/
			var myData = sinAndCos();
			//You need data...

			d3.select('#demo-chart-1 svg')//Select the <svg> element you want to render the chart in.
			.datum(myData)//Populate the <svg> element with chart data...
			.call(chart);
			//Finally, render the chart!

			//Update the chart when window resizes.
			nv.utils.windowResize(function() {
				chart.update();
			});
			return chart;
		});
		/**************************************
		 * Simple test data generator
		 */
		function sinAndCos() {
			var MachNet = [
				{x:0, y: 10},
				{x:1, y: 20},
				{x:2, y: 15},
				{x:3, y: 10},
				{x:4, y:10},
				{x:5, y:15},{x:6, y:20},{x:7, y:18},{x:8, y:20},{x:9, y:30},{x:10, y:40},{x:11, y:50}
				],
				ThamelTexas = [
				{x:0, y: 20},{x:1, y: 40},{x:2, y: 25},{x:3, y: 30},{x:4, y:10},{x:5, y:20},{x:6, y:10},{x:7, y:10},{x:8, y:20},{x:9, y:10},{x:10, y:30},{x:11, y:10}
				],
				Prabhu = [
				{x:0, y: 30},{x:1, y: 30},{x:2, y: 35},{x:3, y: 50},{x:4, y:30},{x:5, y:40},{x:6, y:50},{x:7, y:10},{x:8, y:17},{x:9, y:50},{x:10, y:10},{x:11, y:20}
				],
				Muncha = [
				{x:0, y: 40},{x:1, y: 50},{x:2, y: 55},{x:3, y: 40},{x:4, y:40},{x:5, y:50},{x:6, y:25},{x:7, y:19},{x:8, y:13},{x:9, y:160},{x:10, y:40},{x:11, y:30}
				];

			//Line chart data should be sent as an array of series objects.
			return [{
				values : MachNet, //values - represents the array of {x,y} data points
				key : 'Requested', //key  - the name of the series.
				color : '#ff7f0e' //color - optional: choose your own line color.
			}, {
			    values : MachNet,
			    key : 'Approval',
				color : '#2ca02c'
			}, {
			    values: MachNet,
				key : 'Activated',
				color : '#7777ff',
				area : true //area - set to true if you want this line to turn into a filled area chart.
			},
            {
                values: MachNet,
                key: 'Block',
                color : '#7777ff',
                area : true //area - set to true if you want this line to turn into a filled area chart.
            },{
			    values : MachNet,
				key : 'New Requested',
				color : '#7572ff',
				area : true //area - set to true if you want this line to turn into a filled area chart.
			}];
		}

	};

	var runChart4 = function() {
		nv.addGraph(function() {
			var chart = nv.models.discreteBarChart().x(function(d) {
				return d.label;
			})//Specify the data accessors.
			.y(function(d) {
				return d.value;
			})
			//.staggerLabels(true)//Too many bars and not enough room? Try staggering labels.
			.tooltips(false)//Don't show tooltips
			.showValues(true)//...instead, show the bar value right on top of each bar.
			.transitionDuration(350);

			d3.select('#demo-chart-4 svg').datum(exampleData()).call(chart);

			nv.utils.windowResize(chart.update);

			return chart;
		});

		//Each bar represents a single discrete quantity.
		function exampleData() {
			return [{
				key : "Cumulative Return",
				values : [{
				    "label": "Requested",
					"value" : 10
				}, {
				    "label": "Approval",
					"value" : 0
				}, {
					"label" : "Activated",
					"value" : 14
				}, {
				    "label": "Block",
					"value" : 80
				
				}, {
				    "label" : "New Requested",
				    "value" : 14
				},]
			}];

		}

	};
	var runChart9_10 = function() {
		//Regular pie chart example
		nv.addGraph(function() {
			var chart = nv.models.pieChart().x(function(d) {
				return d.label;
			}).y(function(d) {
				return d.value;
			}).showLabels(true);

			d3.select("#demo-chart-9 svg").datum(exampleData()).transition().duration(350).call(chart);

			return chart;
		});

		//Donut chart example
		nv.addGraph(function() {
			var chart = nv.models.pieChart().x(function(d) {
				return d.label;
			}).y(function(d) {
				return d.value;
			}).showLabels(true)//Display pie labels
			.labelThreshold(.05)//Configure the minimum slice size for labels to show up
			.labelType("percent")//Configure what type of data to show in the label. Can be "key", "value" or "percent"
			.donut(true)//Turn on Donut mode. Makes pie chart look tasty!
			.donutRatio(0.35)//Configure how big you want the donut hole size to be.
			;

			d3.select("#demo-chart-10 svg").datum(exampleData()).transition().duration(350).call(chart);

			return chart;
		});

		//Pie chart example data. Note how there is only a single array of key-value pairs.
		function exampleData() {
			return [{
			    "label": "Requested",
				"value" : 25
			}, {
			    "label": "Approval",
				"value" : 15
			}, {
			    "label": "Activated",
				"value" : 10
			}, {
			    "label": "Block",
				"value" : 20
			}, {
			    "label": "New Requested",
				"value" : 30
			}];
		}

	};
	var runChart19_100 = function() {
		//Regular pie chart example
		nv.addGraph(function() {
			var chart = nv.models.pieChart().x(function(d) {
				return d.label;
			}).y(function(d) {
				return d.value;
			}).showLabels(true);

			d3.select("#demo-chart-19 svg").datum(exampleData()).transition().duration(350).call(chart);

			return chart;
		});

		//Donut chart example
		nv.addGraph(function() {
			var chart = nv.models.pieChart().x(function(d) {
				return d.label;
			}).y(function(d) {
				return d.value;
			}).showLabels(true)//Display pie labels
			.labelThreshold(.05)//Configure the minimum slice size for labels to show up
			.labelType("percent")//Configure what type of data to show in the label. Can be "key", "value" or "percent"
			.donut(true)//Turn on Donut mode. Makes pie chart look tasty!
			.donutRatio(0.35)//Configure how big you want the donut hole size to be.
			;

			d3.select("#demo-chart-100 svg").datum(exampleData()).transition().duration(350).call(chart);

			return chart;
		});

		//Pie chart example data. Note how there is only a single array of key-value pairs.
		function exampleData() {
			return [{
				"label" : "NIC Asia",
				"value" : 50
			}, {
				"label" : "Nepal Investment Bank",
				"value" : 30
			}, {
				"label" : "Prabhu Bank",
				"value" : 20
			}, {
				"label" : "Nepal Bangaladesh Bank",
				"value" : 30
			}, {
				"label" : "SBI Bank",
				"value" : 40
			}];
		}

	};

	function stream_layers(n, m, o) {
		if (arguments.length < 3)
			o = 0;
		function bump(a) {
			var x = 1 / (.1 + Math.random()), y = 2 * Math.random() - .5, z = 10 / (.1 + Math.random());
			for (var i = 0; i < m; i++) {
				var w = (i / m - y) * z;
				a[i] += x * Math.exp(-w * w);
			}
		}

		return d3.range(n).map(function() {
			var a = [], i;
			for ( i = 0; i < m; i++)
				a[i] = o + o * Math.random();
			for ( i = 0; i < 5; i++)
				bump(a);
			return a.map(stream_index);
		});
	}

	function stream_index(d, i) {
		return {
			x : i,
			y : Math.max(0, d)
		};
	}

	return {
		//main function to initiate template pages
		init : function() {
			runChart1();
			runChart4();
			runChart9_10();
			runChart19_100();
		}
	};
}();
