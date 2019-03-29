$(document).ready(function () {
    // Device list table
    var $dt_deviceList = $('#device-list').DataTable({
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "targets": 0
        }],
        "order": [[1, 'asc']]
    });
    // No. column of device list table
    $dt_deviceList.on('order.dt search.dt', function () {
        $dt_deviceList.column(1, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    // Modal head - device information
    var $dt_deviceHead = $('#device_head')
    // Modal body - sensor list
    var $dt_sensorList = $('#sensor_list').DataTable({
        "order": [[0, 'asc']]
    });
    // No. column of sensor list table
    $dt_sensorList.on('order.dt search.dt', function () {
        $dt_sensorList.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    // Click event handler - click on row on device list
    $('#device-list tbody').on('click', 'tr', function (event) {
        var $tr = $(event.target).closest("tr");
        var id = $tr.data("id");

        $.ajax({
            type: 'GET',
            url: '/device/Modal?id=' + id,
            success: function (xhr, status, response) {
                var tbody_str = "<tr>";
                tbody_str += "<td>" + xhr.imei + "</td>";
                tbody_str += "<td>" + xhr.user.userName + "</td>";
                tbody_str += "<td>" + xhr.simCard + "</td>";
                tbody_str += "<td>" + xhr.location.name + "</td>";
                tbody_str += "<td>" + xhr.deviceType.name + "</td>";
                tbody_str += "<td>" + xhr.status + "</td>";
                tbody_str += "</tr>";

                $("#device_head tbody").html(tbody_str);
                
                var sensors = xhr.sensors;

                $dt_sensorList.clear();
                sensors.forEach(sensor => {
                    $dt_sensorList.row.
                        add([
                            null,
                            sensor.sensorType.name,
                            sensor.serialNumber,
                            sensor.highThreshold,
                            sensor.lowThreshold,
                            sensor.status
                        ]).draw(false);
                });
            }
        });
        event.preventDefault();
        $("#detailModal").modal();
    });
    
    $('input[type="checkbox"]').on('click', function (event) {
        var $tr = $(event.target).closest("tr");
        var id = $tr.data("id");
        if (event.target.checked) {
            var $tb_deviceList = $("table#device-list");
            var $checked_inputs = $tb_deviceList.find("input[type='checkbox']:checked");
            for (var i = 0; i < $checked_inputs.length; i++) {
                if ($checked_inputs[i] == event.target) {
                    continue;
                }

                $checked_inputs[i].checked = false;
            }
        }

        event.stopPropagation();
        $('#rowDelete').click(function () {
            $.ajax({
                type: 'POST',
                url: '/device/Delete?id=' + id,
                success: function (result) {
                    $dt_deviceList.row($tr).remove().draw();
                }
            });
        });
    });

    // Add new device
    $('#device_add').click(function () {
        window.location = "/device/create";
    });
    $('#device_delete').click(function () {
        var $tb_deviceList = $("table#device-list");
        var $checked_inputs = $tb_deviceList.find("input[type='checkbox']:checked");
        if ($checked_inputs.length == 0) {
            alert("No device is selected!");
            return;
        }

        $("#deleteModal").modal();

    });
    $('#device_edit').click(function () {
        var $dt_deviceList = $("table#device-list");
        var $checked_inputs = $dt_deviceList.find("input[type='checkbox']:checked").closest("tr");
        if ($checked_inputs.length == 0) {
            alert("No customer is selected!");
            return;
        }

        var device_id = $checked_inputs.data("id");
        window.location = "/device/update/" + device_id;
    });

});