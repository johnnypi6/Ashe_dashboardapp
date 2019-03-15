// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#292b2c';

// Bar Chart Example
var ctx = document.getElementById("liquid");
var myLineChart = new Chart(ctx, {
  type: 'bar',
  data: {
      labels: ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11"],
    datasets: [{
      label: "Revenue",
      backgroundColor: "rgba(122,11717,21621,12)",
      borderColor: "rgba(2,117,216,1)",
        data: [15, 12, 25, 41, 21, 14, 15, 12, 25, 41, 21],
    }],
  },
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
          max: 40,
          maxTicksLimit: 9
        },
        gridLines: {
          display: true
        }
      }],
    },
    legend: {
      display: true
    }
  }
});
