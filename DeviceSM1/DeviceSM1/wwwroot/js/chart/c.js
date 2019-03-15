var ctx = document.getElementById('c').getContext('2d');
var myChart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: ['1', '2', '3', '4', '5', '6 ', '7', '8', '9', '10', '11'],
        datasets: [{
            label: 'apples',
            data: [12, 19, 3, 37, 6, 3, 7],
            backgroundColor: "rgba(153,255,51,0.4)"
        }]
    }
});