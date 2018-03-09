$(document).ready(function () {
   // debugger;
    $('#ddTestTypes').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();

    //Currently disabled - Can be used in next version for Manual Import

    $('#ddWorkStudyId').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    $('#ddWorkStudyId').attr("disabled", "disabled");
    $('#ddTestNos').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    $('#ddTestNos').attr("disabled", "disabled");

    var selectedTestType =""; 

    $('#ddlTestType').change(function () {
         selectedTestType = $.trim($("#ddTestTypes").val());
    });

    $("#btnImportDefault").click(function () {

      //  $("#filePath").click();

        selectedTestType = $.trim($("#ddTestTypes").val());

        var filePath = "none";
        //if ($("#filePath").val() != null) {
        //    // filePath = $.trim($("#filePath").val()); 
        //    filePath = GetRootDirectory() + '\\USCTRD01\\RDServer\\RD\\Database\\Export\\ForNewDataBase\\TestImport.csv';
        //}

        //$('#lblFileName').text("Importing file: " + filePath);
        //var errorMsg;
        //if (!filePath.includes(selectedTestType)) {
        //    errorMsg = "Import Error: Please check the correct file is imported";
        //}

        var options = {
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
            if (data) {
                $('#lblImported').text("Imported: " + selectedTestType + "data");
            }
            else {
                errorMsg = "Import Error: Please check if the file is open";
                $('#lblImported').text(errorMsg);
            }

        });
    });
    
    //$("#uploadTrigger").click(function() {
 
    //    debugger;

    //    $("#filePath").click();

    //    selectedTestType = $.trim($("#ddTestTypes").val());      
     
    //    var filePath = "";
    //    if ($("#filePath").val()!= null){
    //        filePath = $.trim($("#filePath").val());
    //       // filePath = GetRootDirectory() + '\\USCTRD01\\RDServer\\RD\\Database\\Export\\ForNewDataBase\\TestImport.csv';
    //    }

    //    $('#lblFileName').text("Importing file: " +filePath);
    //    var errorMsg; 
    //    if (!filePath.includes(selectedTestType)) {
    //        errorMsg = "Import Error: Please check the correct file is imported";
    //    }              

    //    var options ={
    //        Message: selectedTestType,
    //        Message1: filePath
    //        };

    //    $.ajax({
    //    type: 'post',
    //    url: Api + 'api/ImportData',
    //    headers: {
    //        Token: GetToken()
    //        },
    //    data: options
    //    })
    //    .done(function (data) {
    //        if (data) {
    //            $('#lblImported').text("Imported: " + selectedTestType + "data");
    //        }
    //        else {
    //            errorMsg = "Import Error: Please check if the file is open";
    //            $('#lblImported').text(errorMsg);
    //    }
    //});
    //});


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
            // filePath = GetRootDirectory() + '\\USCTRD01\\RDServer\\RD\\Database\\Export\\ForNewDataBase\\TestImport.csv';
        }

        $('#lblFileName').text("Importing file: " + filePath);
        var errorMsg;
        if (!filePath.includes(selectedTestType)) {
            errorMsg = "Import Error: Please check the correct file is imported";
        }

        var options = {
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
            if (data) {
                $('#lblImported').text("Imported: " + selectedTestType + "data");
            }
            else {
                errorMsg = "Import Error: Please check if the file is open";
                $('#lblImported').text(errorMsg);
            }
        });
    });



});