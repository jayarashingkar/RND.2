
$(document).ready(function () {   
  
    $('#ddTestType').attr('data-live-search', 'true');
    $('#ddTestType').selectpicker();
  
});

$('#btnViewReport').on('click', function () {
  
    if ($('#ddTestType').val() && $('#ddTestType').val()!='-1' && $('#ddTestType').val()!='Please Select') {
        var TestType = $('#ddTestType').val();
      
        switch (TestType.trim()) {
            case 'Tension':
                location.href = '/RnDReports/TensionReportsList?TestType=Tension';
                break;
            case 'Compression':
                location.href = '/RnDReports/CompressionReportsList?TestType=Compression';
                break;
            case 'Optical Mount':
                location.href = '/RnDReports/OpticalMountReportsList?TestType=OpticalMount';
                break;
            case 'Macro Etch':
                location.href = '/RnDReports/MacroEtchReportsList?TestType=MacroEtch';
                break;
            
            default:
                bootbox.alert('Invalid TestType');
                break;
        }}
    else
        bootbox.alter("Please Select Test Type");
});