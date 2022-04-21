var Pending = 50;
var Proceed = 30;
var Approved = 10;
var Reject = 5;
var _Return = 5;

var randomScalingFactor = function () {
    return Math.round(Math.random() * 100);
};


function DataCall() {
  
    //var URLLivk = PiaChartDataUrl;    
    //var ctx = document.getElementById('chart-area').getContext('2d');
    //$.get(URLLivk, function (result) {
    //    console.log("result" + result.Pending);
    //    console.log("result" + result.Proceed);
    //    console.log("result" + result.Approved);
    //    console.log("result" + result.Reject);      
    //            Pending = result.Pending,
    //            Proceed = result.Proceed,
    //            Approved = result.Approved,
    //            Reject = result.Reject,
    //            _Return = result._Return
    //         window.myPie = new Chart(ctx, config);
    //}).fail(function (res) {

    //});

}

var config = {
    type: 'pie',   
    data: {
        datasets: [{
            data: [
            Pending,
            Proceed,
            Approved,
            Reject,
            _Return

            ],
            backgroundColor: [
                window.chartColors.red,
                window.chartColors.orange,
                window.chartColors.yellow,
                window.chartColors.green,
                window.chartColors.blue,
            ],
            label: 'Dataset 1'
        }],
        labels: [
            'Pending',
            'Proceed',
            'Approved',
            'Reject',
            'Return'
        ]
    },
    options: {
        responsive: true
    }
};

$(document).ready(function () {
    //var ctx = document.getElementById('chart-area').getContext('2d');
    //window.myPie = new Chart(ctx, config);
    DataCall();

    
});
var colorNames = Object.keys(window.chartColors);

//document.getElementById('randomizeData').addEventListener('click', function () {
//    config.data.datasets.forEach(function (dataset) {
//        dataset.data = dataset.data.map(function () {
//            return randomScalingFactor();
//        });
//    });

//    window.myPie.update();
//});


//document.getElementById('addDataset').addEventListener('click', function () {
//    var newDataset = {
//        backgroundColor: [],
//        data: [],
//        label: 'New dataset ' + config.data.datasets.length,
//    };

//    for (var index = 0; index < config.data.labels.length; ++index) {
//        newDataset.data.push(randomScalingFactor());

//        var colorName = colorNames[index % colorNames.length];
//        var newColor = window.chartColors[colorName];
//        newDataset.backgroundColor.push(newColor);
//    }

//    config.data.datasets.push(newDataset);
//    window.myPie.update();
//});

//document.getElementById('removeDataset').addEventListener('click', function () {
//    config.data.datasets.splice(0, 1);
//    window.myPie.update();
//});