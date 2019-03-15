


var ctx = document.getElementById("moisture").getContext('2d');
var moistured = new Chart(ctx, {
    type: 'bar',
    data: {
        labels: ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11"],
        datasets: [{
            backgroundColor: "rgba(255, 165, 0,1)",
            data: [30, 29, 5, 5, 20, 3, 10, 30, 29, 5, 30]
        }]
    }
});
var chLine = document.getElementById("chLine");
if (chLine) {
    new Chart(chLine, {
        type: 'line',
        data: chartData,
        options: {
            scales: {
                xAxes: [{
                    time: {
                        unit: 'month'
                    },
                    gridLines: {
                        display: false
                    },
                    ticks: {
                        maxTicksLimit: 6
                    }
                }],
                yAxes: [{
                    ticks: {
                        min: 0,
                        max: 15000,
                        maxTicksLimit: 5
                    },
                    gridLines: {
                        display: true
                    }
                }],
            },
            legend: {
                display: false
            }
        }
    });
}

//display: block;
//width: 224px;
//height: 130px;