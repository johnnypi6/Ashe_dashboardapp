$(document).ready(function () {

    var $dt_adminList = $('table#admin-list').DataTable({
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "targets": 0
        }],
        "order": [[1, 'asc']]
    });

    $dt_adminList.on('order.dt search.dt', function () {
        $dt_adminList.column(1, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    $('input[type="checkbox"]').on('click', function (event) {
        var $tr = $(event.target).closest("tr");
        var id = $tr.data("id");
        if (event.target.checked) {
            var $tb_adminList = $("table#admin-list");
            var $checked_inputs = $tb_adminList.find("input[type='checkbox']:checked");
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
                url: '/admin/delete?id=' + id,
                success: function (result) {

                }
            });
            $dt_adminList.row($tr).remove().draw();
        });
    });

    $('#admin_add').click(function () {
        window.location = "/admin/create";
    });

    $('#admin_edit').click(function () {
        var $tb_adminList = $("table#admin-list");
        var $checked_inputs = $tb_adminList.find("input[type='checkbox']:checked").closest("tr");
        if ($checked_inputs.length == 0) {
            alert("No customer is selected!");
            return;
        }

        var admin_id = $checked_inputs.data("id");
        window.location = "/admin/update/" + admin_id;
    });

    $('#admin_delete').click(function () {
        var $tb_adminList = $("table#admin-list");
        var $checked_inputs = $tb_adminList.find("input[type='checkbox']:checked");
        if ($checked_inputs.length == 0) {
            alert("No customer is selected!");
            return;
        }
        $("#deleteModal").modal();
    });

});