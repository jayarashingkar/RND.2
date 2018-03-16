$(document).ready(function () {

    $('#LocationOption').hide();
    $('#StudyTypeOption').hide();
    $('#btnSaveOption').hide();
    
    $('#lblReturnMessage').text('');
        
    //$('#ddStudyTypeList').attr('data-live-search', 'true');
    //$('#ddStudyTypeList').selectpicker();

    //$('#ddLocationList').attr('data-live-search', 'true');
    //$('#ddLocationList').selectpicker();

    //$('#ddStudyTypeList').change(function () {

    //    if ($('#ddStudyTypeList').val != -1) {
    //        $("#btnAddOption").attr("disabled", true);
    //        $("#btnEditOption").attr("disabled", false);
    //       // $("#btnDeleteOption").attr("disabled", false);
    //    }
    //    else {
    //        $("#btnAddOption").attr("disabled", false);
    //        $("#btnEditOption").attr("disabled", true);
    //      //  $("#btnDeleteOption").attr("disabled", true);
    //    }
    //});

    //$('#ddLocation').change(function () {

    //    if ($('#ddLocation').val != -1) {
    //        $("#btnAddOption").attr("disabled", true);
    //        $("#btnEditOption").attr("disabled", false);
    //      //  $("#btnDeleteOption").attr("disabled", false);
    //    }
    //    else {
    //        $("#btnAddOption").attr("disabled", false);
    //        $("#btnEditOption").attr("disabled", true);
    //       // $("#btnDeleteOption").attr("disabled", true);
    //    }
    //});

    $("#btnAddOption").click(function () {
        $('#lblReturnMessage').text('');
        var optionType = $("input:radio[name='OptionRadio']:checked").val();
        if (optionType == RND.OptionConstant.StudyType) {           
            $('#LocationOption').hide();
            $('#StudyTypeOption').show();
            $('#btnSaveOption').show();

          
          

        }
        else if (optionType == RND.OptionConstant.Location) {          
            $('#LocationOption').show();
            $('#StudyTypeOption').hide();
            $('#btnSaveOption').show();

        }
    });
       
    $("#btnSaveOption").click(function () {
        
        var optionType = $("input:radio[name='OptionRadio']:checked").val();
        
        if (optionType == RND.OptionConstant.StudyType) {
            var TypeDesc = $.trim($("#TypeDesc").val());
            //debugger;
          //  $('#formStudyType').bootstrapValidator('resetForm', true);
       
        }
        else if (optionType == RND.OptionConstant.Location) {          
            var PlantDesc = $.trim($("#PlantDesc").val());
            var PlantState = $.trim($("#PlantState").val());
            var PlantType = $.trim($("#PlantType").val());
          //  $('#formLocation').bootstrapValidator('resetForm', true);
           
        }
        var options = {
            // MessageList: selectedTestTypes,
            optionType: optionType,
            TypeDesc: TypeDesc,
            PlantDesc: PlantDesc,
            PlantState: PlantState,
            PlantType: PlantType
        };

        $.ajax({
            type: 'post',
            url: Api + 'api/Options',
            headers: {
                Token: GetToken()
            },
            data: options
        })
       .done(function (data) {          
           if (data.RecId) {
               var message = optionType + ': successfully added'
               $('#lblReturnMessage').text(message);
           }
           else {
               var message = optionType + ': did not get added'
               $('#lblReturnMessage').text(message);
           }
       });
    });
       
    //var formType = $("input:radio[name='OptionRadio']:checked").val();
    //if (formType == RND.OptionConstant.Location) {
    //    $('#formLocation').bootstrapValidator({
    //        message: 'This value is not valid',
    //        feedbackIcons: {
    //            valid: 'glyphicon glyphicon-ok',
    //            invalid: 'glyphicon glyphicon-remove',
    //            validating: 'glyphicon glyphicon-refresh'
    //        },
    //        fields: {
    //            PlantDesc: {
    //                validators: {
    //                    notEmpty: {
    //                        message: 'Plant Desciption ID is required.'
    //                    }
    //                }
    //            },
    //            PlantState: {
    //                validators: {
    //                    notEmpty: {
    //                        message: 'Plant State ID is required.'
    //                    }
    //                }
    //            },
    //            PlantType: {
    //                validators: {
    //                    notEmpty: {
    //                        message: 'Plant Type ID is required.'
    //                    }
    //                }
    //            }
    //        }
    //    });
    //}
    //else
    //    if (formType == RND.OptionConstant.StudyType) {
    //        $('#formStudyType').bootstrapValidator({
    //            message: 'This value is not valid',
    //            feedbackIcons: {
    //                valid: 'glyphicon glyphicon-ok',
    //                invalid: 'glyphicon glyphicon-remove',
    //                validating: 'glyphicon glyphicon-refresh'
    //            },
    //            fields: {
    //                TypeDesc: {
    //                    validators: {
    //                        notEmpty: {
    //                            message: 'Type Desciption ID is required.'
    //                        }
    //                    }
    //                }
    //            }
    //        });

    //    }



    //$("#btnEditOption").click(function () {
    //    var optionType = $("input:radio[name='OptionRadio']:checked").val();
       
    //    if (optionType == RND.OptionConstant.StudyType) {
    //        debugger;
    //        $('#LocationOption').hide();
    //        $('#StudyTypeOptionOption').show();
    //        $('#RecId').val($('#ddStudyTypeList').val());

    //    }
    //    else if (optionType == RND.OptionConstant.Location) {
    //        debugger;
    //        $('#LocationOption').show();
    //        $('#StudyTypeOptionOption').hide();
    //        $('#RecId').val($('#ddLocationList').val());
    //    }
    //});

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

   
});

