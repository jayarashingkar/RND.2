$(document).ready(function () {

    $('#LocationOption').hide();
    $('#StudyTypeOption').hide();
    $('#btnSaveOption').hide();
    
    $('#lblReturnMessage').text('');  

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
          
});

