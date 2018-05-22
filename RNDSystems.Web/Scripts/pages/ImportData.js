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
        $('#lblFileName').text('');
        //$('#lblImported').text('');
        //$('#lblError').text('');

    var selectedTestType = "";

    $("#btnImportDefault").prop('disabled', true);
   $("#ddTestTypesDefault").prop("disabled", true);
    $("#Import").prop('disabled', true);
    $("#Reset").prop('disabled', true);
    $("#Choose").prop('disabled', true);
    $("#filePathChoose").prop('disabled', true);
      $("#ddTestTypes").prop('disabled', true);

    $("#btnSelectImport").click(function () {

        $('#lblFileName').text('');
        $('#lblImported').text('');
        $('#lblError').text('');

        var importType = $("input:radio[name='ImportRadio']:checked").val();

        if (importType == 'Default') {
            $("#btnImportDefault").prop('disabled', false);
            $("#ddTestTypesDefault").prop("disabled", false);
            $("#Import").prop('disabled', true);
            $("#Reset").prop('disabled', true);
            $("#Choose").prop('disabled', true);
            $("#filePathChoose").prop('disabled', true);
            $("#ddTestTypes").prop('disabled', true);

            $("#hdImportDefault").addClass("bg-primary").removeClass("bg-info");
            $("#hdImport").addClass("bg-info").removeClass("bg-primary");
        }
        if (importType == 'Custom') {
            $("#btnImportDefault").prop('disabled', true);
             $("#ddTestTypesDefault").prop("disabled", true);
            $("#Import").prop('disabled', false);
            $("#Choose").prop('disabled', false);
            $("#Reset").prop('disabled', false);
            $("#filePathChoose").prop('disabled', false);
            $("#ddTestTypes").prop('disabled', false);

            $("#hdImport").addClass("bg-primary").removeClass("bg-info");
            $("#hdImportDefault").addClass("bg-info").removeClass("bg-primary");
        }       
    });

    $('#ddlTestType').change(function () {
        selectedTestType = $.trim($("#ddTestTypes").val());
    });

    $("#btnImportDefault").click(function () {
           var selectedTestTypes = $('#ddTestTypesDefault').val();
     //   var selectedTestTypes = $.trim($("#ddTestTypes").val());
    //     debugger;
        var filePath = "none";

        var options = {
            MessageList: selectedTestTypes,
            //Message: selectedTestType,
           // tt: selectedTestTypes,
            Message1: filePath
        };
  //      debugger;
        $.ajax({
            type: 'post',
            url: Api + 'api/ImportData',
            headers: {
                Token: GetToken()
            },
            data: options
        })
        .done(function (data) {
           // debugger;
            errorMsg = "";
            successMsg = "";
            if (data.Success) {
                if ((data.Message != "") && (data.Message != null)) {
                    successMsg = data.Message;
                }
                if (data.Message1 != "") {
                    errorMsg = data.Message1;
                }               
            }
            else
                errorMsg = "Import Error: Please check if the file is open";   
            
            if (successMsg !="")
                $('#lblMessage').text(successMsg);
            if (errorMsg != "")
                $('#lblError').text(errorMsg);

        });
    });

    $("#Choose").click(function () {

        $("#file").click();
        $("#filePathChoose").val($("#file").val());
    });

    $("#Reset").click(function () {
        $("#filePathChoose").val("");
    });

    //try 
    $("#Import").click(function () {
        if (selectedTestType == '-1')
            bootbox.alert("Please select Test Type");
        else {
            selectedTestType = $.trim($("#ddTestTypes").val());
            $("#selectedTestType").val(selectedTestType);
        }

        var filePath = "";
        if ($("#file").val() != null) {
            filePath = $.trim($("#file").val());
        }
        $('#lblFileName').text("Importing file: " + filePath);
        //var errorMsg;
        //if (!filePath.includes(selectedTestType)) {
        //    errorMsg = "Import Error: Please check the correct file is imported";
        //    $('#lblError').text(errorMsg);
        //}

        // var options = {
        //    // MessageList: selectedTestTypes,
        //     Message: selectedTestType,
        //     Message1: filePath           
        // };

        // $.ajax({
        //     type: 'post',
        //     url: Api + 'api/ImportData',
        //     headers: {
        //         Token: GetToken()
        //     },
        //     data: options
        // })
        //.done(function (data) {          
        //    if (data.Success) {
        //        $('#lblImported').text("Imported: " + selectedTestType + "data");
        //    }
        //    else {
        //        if ((data.Message1 != "") && (data.Message1 != null)) {
        //            errorMsg = data.Message1;
        //        }
        //        else
        //            errorMsg = "Import Error: Please check if the file is open";

        //        $('#lblError').text(errorMsg);
        //        if ((data.Message != "") && (data.Message != null)) {
        //            $('#lblImported').text(data.Message);
        //        }
        //    }
        //});

    });
    $('#lblImported').addClass("bg-success");

});