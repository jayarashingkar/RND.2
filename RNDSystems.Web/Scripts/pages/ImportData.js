$(document).ready(function () {
    $('#ddTestTypes').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();

    //multiple selection
    $('#ddTestTypesDefault').attr('multiple', '');
    $('#ddTestTypesDefault').attr('data-actions-box', 'true');
    $('#ddTestTypesDefault').selectpicker();

    //Currently disabled - Can be used in next version for Manual Import

    $('#ddWorkStudyId').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    $('#ddWorkStudyId').attr("disabled", "disabled");
    $('#ddTestNos').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    $('#ddTestNos').attr("disabled", "disabled");

    var selectedTestType =""; 
  
    $("#btnImportDefault").prop('disabled', true);
    $("#ddTestTypesDefault").prop("disabled", true);
    $("#Import").prop('disabled', true);
    $("#ddTestTypes").prop('disabled', true);

    $("#btnSelectImport").click(function () {
           
        var importType = $("input:radio[name='ImportRadio']:checked").val();

        if (importType == 'Default') {
            $("#btnImportDefault").prop('disabled', false);
            $("#ddTestTypesDefault").prop("disabled", false);
            $("#Import").prop('disabled', true);
            $("#ddTestTypes").prop('disabled', true);

            $("#hdImportDefault").addClass("bg-primary").removeClass("bg-info");
            $("#hdImport").addClass("bg-info").removeClass("bg-primary");
        }
        if (importType == 'Custom') {
            $("#btnImportDefault").prop('disabled', true);
            $("#ddTestTypesDefault").prop("disabled", true);
            $("#Import").prop('disabled', false);
            $("#ddTestTypes").prop('disabled', false);

            $("#hdImport").addClass("bg-primary").removeClass("bg-info");
            $("#hdImportDefault").addClass("bg-info").removeClass("bg-primary");
        }
        
        $('#lblFileName').text('');
        $('#lblImported').text('');
        $('#lblError').text('');
    });
    
    $('#ddlTestType').change(function () {
         selectedTestType = $.trim($("#ddTestTypes").val());
    });

    $("#btnImportDefault").click(function () {
       var selectedTestTypes = $('#ddTestTypesDefault').val();

        var filePath = "none";

        var options = {
            MessageList: selectedTestTypes,
            //Message: selectedTestType,
            Message1: filePath
        };

        $.ajax({
            type: 'post',
            url: Api + 'api/ImportData',
            headers: {
                Token: GetToken()
            },
            data: options
        })
        .done(function (data) {
            debugger;
            if (data.Success) {
                $('#lblImported').text("Imported: " + selectedTestType + "data");
            }
            else {
                if ((data.Message1 != "")&&(data.Message1 != null)) {
                    errorMsg = data.Message1;
                }
                else
                    errorMsg = "Import Error: Please check if the file is open";

                $('#lblError').text(errorMsg);
                if ((data.Message != "") && (data.Message != null)) {
                    $('#lblImported').text(data.Message);
                }                
            }
        });
    });    

    $("#Choose").click(function () {      
        $("#filePath1").click();
         $("#filePathChoose").val($("#filePath1").val());
      
    });

    $("#Reset").click(function () {       
        $("#filePathChoose").val("");
    });

    $("#Import").click(function () {
      
       selectedTestType = $.trim($("#ddTestTypes").val());

        var filePath = "";
        if ($("#filePath1").val() != null) {
            filePath = $.trim($("#filePath1").val());
        }
        $('#lblFileName').text("Importing file: " + filePath);
        var errorMsg;
        if (!filePath.includes(selectedTestType)) {
            errorMsg = "Import Error: Please check the correct file is imported";
        }
        var options = {
           // MessageList: selectedTestTypes,
            Message: selectedTestType,
            Message1: filePath           
        };

        $.ajax({
            type: 'post',
            url: Api + 'api/ImportData',
            headers: {
                Token: GetToken()
            },
            data: options
        })
       .done(function (data) {
           debugger;
           if (data.Success) {
               $('#lblImported').text("Imported: " + selectedTestType + "data");
           }
           else {
               if ((data.Message1 != "") && (data.Message1 != null)) {
                   errorMsg = data.Message1;
               }
               else
                   errorMsg = "Import Error: Please check if the file is open";

               $('#lblError').text(errorMsg);
               if ((data.Message != "") && (data.Message != null)) {
                   $('#lblImported').text(data.Message);
               }
           }
       });
    });
});