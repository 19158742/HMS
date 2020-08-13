$(document).ready(function () {
    var apiBaseUrl = "https://hotelapi20200806072002.azurewebsites.net/";
    $('#btnSubmit').click(function () {
        var focusSet = false; 
        if (!$('#CustomerName').val()) {
            if ($("#CustomerName").parent().next(".validation").length == 0) // only add if not added
            {
                $("#CustomerName").parent().after("<div class='validation' style='color:red;margin-bottom: 20px;'>Please enter customer name</div>");
            }
            e.preventDefault(); // prevent form from POST to server
            $('#CustomerName').focus();
            focusSet = true;
        } else {
            $("#CustomerName").parent().next(".validation").remove(); // remove it
        }
    });


    $('#btnGetData').click(function () {
        var frmdt = $('#FromDt').val();
        var todt = $('#ToDt').val();
        var roomtype = $('#RoomTpe').val();

        var focusSet = false;            
            if (!$('#FromDt').val()) {
                if ($("#FromDt").parent().next(".validation").length == 0) // only add if not added
                {
                    $("#FromDt").parent().after("<div class='validation' style='color:red;margin-bottom: 20px;'>Please enter from date</div>");
                }
                e.preventDefault(); // prevent form from POST to server
                $('#FromDt').focus();
                focusSet = true;
            } else {
                $("#FromDt").parent().next(".validation").remove(); // remove it
            }
             if (!$('#ToDt').val()) {
                 if ($("#ToDt").parent().next(".validation").length == 0) // only add if not added
                 {
                     $("#ToDt").parent().after("<div class='validation' style='color:red;margin-bottom: 20px;'>Please enter to date</div>");
                 }
                 e.preventDefault(); // prevent form from POST to server
                 $('#ToDt').focus();
                 focusSet = true;
             } else {
                 $("#ToDt").parent().next(".validation").remove(); // remove it
        }
        if (!focusSet) {
            var apiBaseUrl = "https://hotelapi20200806072002.azurewebsites.net/";
            $.ajax({
                url: apiBaseUrl + 'Room/Get?roomtype=' + roomtype + '&dtFrom=' + frmdt + '&dtTo=' + todt,
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    var $table = $('<table/>').addClass('dataTable table table-bordered table-striped');
                    var $header = $('<thead/>').html('<tr><th style="visibility: hidden;">id</th><th>Room Name</th><th>Price Per Day</th></tr>');
                    $table.append($header);
                    for (i = 0; i < data.length; i++) {
                        var $row = $('<tr/>');
                        $row.append($('<td style="visibility: hidden;"/>').html(data[i].room_id));
                        $row.append($('<td/>').html(data[i].room_name));
                        $row.append($('<td/>').html(data[i].room_price));
                        $table.append($row);
                    }

                    $('#updatePanel').html($table);
                    $('#DivSelectRoom').css("display", "block");
                    $('#RoomId').empty();
                    $('#RoomId').append($('<option>').text("Select Room").attr('value', 0));
                    for (i = 0; i < data.length; i++) {
                        $('#RoomId').append($('<option>').text(data[i].room_name).attr('value', data[i].room_id));
                    }
                },
                error: function () {
                    //alert('Error!');
                }
            });
        }
    });

    $('#RoomId').on("change", function () {
        var focusSet = false;
        if ($('#RoomId').val() == 0) {
            if ($("#RoomId").parent().next(".validation").length == 0) // only add if not added
            {
                $("#RoomId").parent().after("<div class='validation' style='color:red;margin-bottom: 20px;'>You have not selected a room</div>");
            }
            e.preventDefault(); // prevent form from POST to server
            $('#RoomId').focus();
            focusSet = true;
        } else {
            $("#RoomId").parent().next(".validation").remove(); // remove it
        }
        if (!focusSet) {
            $('#TotalAmt').val('');
            $('#DivFillForm').css("display", "block");
            var frmdt = $('#FromDt').val();
            var todt = $('#ToDt').val();
            var date1 = new Date(frmdt);
            var date2 = new Date(todt);
            var Difference_In_Time = date2.getTime() - date1.getTime();
            var Difference_In_Days = Difference_In_Time / (1000 * 3600 * 24);
            var roomid = $(this).val();
            $.ajax({
                url: apiBaseUrl + 'Room/GetRoomPrice?roomid=' + roomid,
                type: 'GET',
                success: function (data) {
                    debugger;
                    $('#TotalAmt').val(Difference_In_Days * data);
                },
                error: function () {
                    //alert('Error!');
                }
            });
        }
    });

    $("#FromDt").datepicker({
        onSelect: function (selectedDate) {
                $('#FromDt').val(selectedDate);
            }
    });
    $("#ToDt").datepicker({
        onSelect: function (selectedDate) {
            $('#ToDt').val(selectedDate);
        }
    });
});  

