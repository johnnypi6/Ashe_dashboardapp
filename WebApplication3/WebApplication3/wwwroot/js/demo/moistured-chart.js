


var ctx = document.getElementById("moistured").getContext('2d');
var moistured = new Chart(ctx, {
        type: 'bar',
  data: {
        labels: ["M", "T", "W", "R", "F", "S", "S"],
    datasets: [{
        label: 'apples',
    data: [12, 19, 3, 17, 28, 24, 7]
    }, {
        label: 'oranges',
    data: [30, 29, 5, 5, 20, 3, 10]
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
                yAxes: [{
                    ticks: {
                        beginAtZero: false
                    }
                }]
            },
            legend: {
                display: false
            }
        }
    });
}