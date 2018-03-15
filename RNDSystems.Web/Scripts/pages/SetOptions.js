$(document).ready(function () {

   // $('#CustomOption').hide();
    $('#LocationOption').hide();
    $('#StudyTypeOptionOption').hide();
    $('#lblReturnMessage').text('');

    $('#ddStudyTypeList').change(function () {

        if ($('#ddStudyTypeList').val != -1) {
            $("#btnAddOption").attr("disabled", true);
            $("#btnEditOption").attr("disabled", false);
           // $("#btnDeleteOption").attr("disabled", false);
        }
        else {
            $("#btnAddOption").attr("disabled", false);
            $("#btnEditOption").attr("disabled", true);
          //  $("#btnDeleteOption").attr("disabled", true);
        }
    });

    $('#ddLocation').change(function () {

        if ($('#ddLocation').val != -1) {
            $("#btnAddOption").attr("disabled", true);
            $("#btnEditOption").attr("disabled", false);
          //  $("#btnDeleteOption").attr("disabled", false);
        }
        else {
            $("#btnAddOption").attr("disabled", false);
            $("#btnEditOption").attr("disabled", true);
           // $("#btnDeleteOption").attr("disabled", true);
        }
    });

    $("#btnAddOption").click(function () {
        var optionType = $("input:radio[name='OptionRadio']:checked").val();
        if (optionType == RND.OptionConstant.StudyType) {
            debugger;
            $('#LocationOption').hide();
            $('#StudyTypeOptionOption').show();           
        }
        else if (optionType == RND.OptionConstant.Location) {
            debugger;
            $('#LocationOption').show();
            $('#StudyTypeOptionOption').hide();
        }
    });

    $("#btnEditOption").click(function () {
        var optionType = $("input:radio[name='OptionRadio']:checked").val();
       
        if (optionType == RND.OptionConstant.StudyType) {
            debugger;
            $('#LocationOption').hide();
            $('#StudyTypeOptionOption').show();
            $('#RecId').val($('#ddStudyTypeList').val());

        }
        else if (optionType == RND.OptionConstant.Location) {
            debugger;
            $('#LocationOption').show();
            $('#StudyTypeOptionOption').hide();
            $('#RecId').val($('#ddLocationList').val());
        }
    });

    //$("#btnDeleteOption").click(function () {

    //    var optionType = $("input:radio[name='OptionRadio']:checked").val();

    //    if (optionType == RND.OptionConstant.StudyType) {
    //        debugger;
    //       // $('#LocationOption').hide();
    //        //$('#StudyTypeOptionOption').show();

    //        var id = $('#ddStudyTypeList').val();
    //        var text = $('#ddStudyTypeList').text();
    //        var data = {
    //            Message: id,
    //            Message1: optionType
    //        };

    //        bootbox.confirm({
    //            message: RND.Constants.AreYouDelete,
    //            buttons: {
    //                confirm: {
    //                    label: 'Yes',
    //                    className: 'btn-success'
    //                },
    //                cancel: {
    //                    label: 'No',
    //                    className: 'btn-danger'
    //                }
    //            },
    //            callback: function (result) {
    //                if (result) {
    //                    $.ajax({
    //                        url: Api + "api/options/" + id,
    //                        headers: {
    //                            Token: GetToken()
    //                        },
    //                        data: data,
    //                        type: 'DELETE',                           
    //                        contentType: "application/json;charset=utf-8",                    
    //                    })
    //                    .done(function (data) {
    //                        var message = text + ': successfully deleted'
    //                        $('#lblReturnMessage').text(message);
    //                    });
    //                }         
    //            }
    //    });

    //    }
    //    else if (optionType == RND.OptionConstant.Location) {
    //        debugger;
    //      //  $('#LocationOption').show();
    //       // $('#StudyTypeOptionOption').hide();
    //        $('#RecId').val($('#ddLocationList').val());

    //    }
    //});

    $("#btnSelectOption").click(function () {
        var optionType = $("input:radio[name='OptionRadio']:checked").val();

        //location.href = '/Options/StudyTypeOptionView?id=0&optionType=' + optionType;

        if (optionType == RND.OptionConstant.StudyType)
        {
            debugger;
            $('#LocationOption').hide();
            $('#StudyTypeOptionOption').show();

          //  location.href = '/Options/StudyTypeOptionView?id=0&optionType=' + optionType;
            // $('#CustomOption').load('@Url.Action("StudyTypeOptionView")');
          
        }
        else if (optionType == RND.OptionConstant.Location)
        {
            debugger;
            $('#LocationOption').show();
            $('#StudyTypeOptionOption').hide();         
        }
    });
});

