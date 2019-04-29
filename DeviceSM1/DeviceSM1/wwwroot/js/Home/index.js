
var Index = function () {
    var protocol = location.protocol === "https:" ? "wss:" : "ws:";
    var uri = protocol + "//" + window.location.host + "/ws";
    var $tblAlert = null;

    var addNewAlert = function (packet) {
        var alerts = packet.Alerts;
        for (var i in alerts) {
            var alert = alerts[i];
            var row = {
                DT_RowId: alert.Id,
                index: null,
                location: alert.Sensor.Device.Location.Name,
                imei: packet.IMEI,
                user: alert.Sensor.Device.User.UserName,
                timestamp: packet.Timestamp,
                sensor: alert.Sensor.SensorType.Name,
                command: null,
                reply: null
            }
            prepend(row)
        }
    }
    
    var prepend = function (row) {
        $tblAlert.row.add(row);
        var aiDisplayMaster = $tblAlert.settings()[0]["aiDisplayMaster"];
        irow = aiDisplayMaster.pop();
        aiDisplayMaster.unshift(irow);
        $tblAlert.draw();
    }

    var showAlertDetail = function (id) {
        $.ajax({
            type: 'GET',
            url: '/home/alert?id=' + id,
            success: function (xhr, status, response) {
                debugger;
                $('#pressure_value').text(xhr.log.sensorValue1);
                $('#temperature_value').text(xhr.log.sensorValue2);
                $('#humidity_value').text(xhr.log.sensorValue3);
                $('#moisture_value').text(xhr.log.sensorValue4);
                $('#liquid_value').text(xhr.log.sensorValue5);
                $('#co2_value').text(xhr.log.sensorValue6);
            }
        });
    }

    return {
        
        init: function () {
            $.fn.DataTable.ext.pager.numbers_length = 5;
            $tblAlert = $('#tblAlert').DataTable({
                select: true,
                lengthMenu: [5],
                columns: [
                    {data: "index"},
                    {data: "location"},
                    {data: "imei"},
                    {data: "user"},
                    {data: "timestamp"},
                    {data: "sensor"},
                    {data: "command"},
                    {data: "reply"}
                ]
            });

            $tblAlert.on('select', function (e, dt, type, indexes) {
                var id = $tblAlert.row(indexes[0]).id();
                showAlertDetail(id);
            });

            $tblAlert.on('order.dt search.dt', function () {
                $tblAlert.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1;
                });
            }).draw();
        },
        connect: function () {
            socket = new WebSocket(uri);
            socket.open = function (event) {
                console.log("opened connection to " + uri);
            }
            socket.onclose = function (event) {
                console.log("closed connection from" + uri);
            }
            socket.onmessage = function (event) {
                console.log(event.data);

                var packet = JSON.parse(event.data);
                addNewAlert(packet);
            }
            socket.onerror = function (event) {
                console.log("error: " + event.data);
            }
        }
    }
}();

jQuery(document).ready(function() {
    Index.init();
    Index.connect();
});