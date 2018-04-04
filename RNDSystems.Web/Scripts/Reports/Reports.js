
//$('#btnClear').on('click', function () {
//    $('#searchTestingNo').val('');
//    $('#TestingMaterialRepeater').repeater('render');
//});

$(document).ready(function () {
   
    $('#ddlWorkStudyID').attr('data-live-search', 'true');
    $('#ddlWorkStudyID').selectpicker();

    $('#ddTestType').attr('data-live-search', 'true');
    $('#ddTestType').selectpicker();
    
    
    //$('#searchFromDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    //$('#searchFromDate').datepicker("setDate", new Date(new Date().setFullYear(new Date().getFullYear() - 1)));
    //$('#searchToDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    //$('#searchToDate').datepicker("setDate", new Date());
    
});

$('#ddlWorkStudyID').on('change', function () {
   
    var  WorkStudyID = $('#ddlWorkStudyID').val();
    var options = {
        WorkStudyID: WorkStudyID
    };
 
    $.ajax({
        type: 'post',
        url: GetRootDirectory() + '/RnDReports/Reports',
        //url:  '../TestingMaterial/TestingMaterial',
        //'/TestingMaterial/SaveTestingMaterial?avialableTT=' + avialableTT
        data: options
    })
       .done(function (data) {
          
           if (data && data.isSuccess) {
          // if (data) {
                 $('#ddTestType').prop('disabled', false);
            
               // Populating TestType dropdown menu

               var outputSubTT = data.ddTestType;
               var option1SubTT = '<option value="' +
                       0 + '">' + "--Select State--" + '</option>';
               $("#ddTestType").append(option1SubTT);
               var SubTTValue;
               var SubTTText;
               var optionSubTT;
               $.each(outputSubTT, function (i) {
                   SubTTValue = outputSubTT[i].Value;
                   SubTTText = outputSubTT[i].Text;

                   optionSubTT += '<option value="' +
                       outputSubTT[i].Value + '">' + outputSubTT[i].Text + '</option>';
               });

               $("#ddTestType").empty();
               $("#ddTestType").append(optionSubTT);
               $("#ddTestType").selectpicker('refresh');
                
           }
           else {   
               $('#ddTestType').prop('disabled', true);
           }
                
       });
});


$('#btnReport').on('click', function () {

    var TestType = $('#ddTestType').val();
    var workStudyID = $('#ddlWorkStudyID').val();

    debugger;
    switch (TestType.trim()) {
        case 'Tension':
            location.href = '/RnDReports/TensionReportsList?workStudyID=' + workStudyID;
            break;
        case 'Compression':
            location.href = '/RnDReports/CompressionReportslList?workStudyID=' + workStudyID;
            break;
        case 'Optical Mount':
            location.href = '/RnDReports/OpticalMountReportslList?workStudyID=' + workStudyID;
            break;
        default:
            bootbox.alert('Invalid TestType');
            break;

    }

});