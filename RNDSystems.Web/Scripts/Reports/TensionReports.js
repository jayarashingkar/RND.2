var columns = [
    {
        label: 'RecID',
        property: 'RecID',
        sortable: true,
    },
    {
        label: 'WorkStudyID',
        property: 'WorkStudyID',
        sortable: true,
    },
    {
        label: 'TestNo',
        property: 'TestNo',
        sortable: true,
    },
    {
        label: 'Alloy',
        property: 'Alloy',
        sortable: true,
    },
    {
        label: 'Temper',
        property: 'Temper',
        sortable: true,
    },
    {
        label: 'CustPart',
        property: 'CustPart',
        sortable: true,
    },
    {
        label: 'UACPart',
        property: 'UACPart',
        sortable: true,
    },
    {
        label: 'SubConduct',
        property: 'SubConduct',
        sortable: true,
    },
    {
        label: 'SurfConduct',
        property: 'SurfConduct',
        sortable: true,
    },
    {
        label: 'FtuKsi',
        property: 'FtuKsi',
        sortable: true,
    },
   {
       label: 'FtyKsi',
       property: 'FtyKsi',
       sortable: true,
   },
   {
       label: 'eElongation',
       property: 'eElongation',
       sortable: true,
   },
      {
          label: 'EModulusMpsi',
          property: 'EModulusMpsi',
          sortable: true,
      },
   {
       label: 'SpeciComment',
       property: 'SpeciComment',
       sortable: true,
   },
   {
       label: 'Operator',
       property: 'Operator',
       sortable: true,
   },
   {
       label: 'TestDate',
       property: 'TestDate',
       sortable: true,
   },
    {
        label: 'TestTime',
        property: 'TestTime',
        sortable: true,
    },
    {
        label: 'EntryBy',
        property: 'EntryBy',
        sortable: true,
    },
    {
        label: 'EntryDate',
        property: 'EntryDate',
        sortable: true,
    },
      {
          label: 'Completed',
          property: 'Completed',
          sortable: true,
      }
];

$(document).ready(function () {
    $('#ddlWorkStudyID').attr('data-live-search', 'true');
    $('#ddlWorkStudyID').selectpicker();

    $('#TestType').prop('disabled', true);
  //  $('#TestType').attr('disabled', true);   
});


function customColumnRenderer(helpers, callback) {
    // determine what column is being rendered
    var column = helpers.columnAttr;  
    var rowData = helpers.rowData;
    var customMarkup = helpers.item.text();
    helpers.item.html(customMarkup);
    callback();
}

function customRowRenderer(helpers, callback) {
    // let's get the id and add it to the "tr" DOM element
    var item = helpers.item;
      item.attr('id', 'row' + helpers.rowData.RecID);
    //item.attr('id', 'row' + helpers.rowData.TestingNo);
    callback();
}

// this example uses an API to fetch its datasource.
// the API handles filtering, sorting, searching, etc.
function customDataSource(options, callback) {
    // set options

    var pageIndex = options.pageIndex;
    var pageSize = options.pageSize;
    var search = '';
    var flag = true;

    //if ($('#searchFromDate').val()) {
    //    var searchFromDate = $('#searchFromDate').datepicker();
    //    searchFromDate = $("#searchFromDate").data('datepicker').getFormattedDate('yyyy-mm-dd');
    //    if (searchFromDate && searchFromDate !== '')
    //        search += ';' + 'searchFromDate:' + searchFromDate;
    //}
    //if ($('#searchToDate').val()) {
    //    var searchToDate = $('#searchToDate').datepicker();
    //    searchToDate = $("#searchToDate").data('datepicker').getFormattedDate('yyyy-mm-dd');
    //    if (searchToDate && searchToDate !== '')
    //        search += ';' + 'searchToDate:' + searchToDate;
    //}
    if ($('#ddlWorkStudyID').val())
        search += ';' + 'WorkStudyID:' + $('#ddlWorkStudyID').val();     
    if ($('#Alloy').val())
        search += ';' + 'Alloy:' + $('#Alloy').val();
    if ($('#Temper').val())
        search += ';' + 'Temper:' + $('#Temper').val();
    if ($('#UACPart').val())
        search += ';' + 'UACPart:' + $('#UACPart').val();
    if ($('#CustPart').val())
        search += ';' + 'CustPart:' + $('#CustPart').val();
    search += ';' + 'TestType:' + 'Tension';

    var options = {
        Screen: 'Tension',
        pageIndex: pageIndex,
        pageSize: pageSize,
        sortDirection: options.sortDirection,
        sortBy: options.sortProperty,
        filterBy: options.filter.value || '',
        searchBy: search || ''
    };
    
    // call API, posting options
    $.ajax({
        type: 'post',
        url: Api + 'api/grid',
        headers: {
            Token: GetToken()
        },
        data: options
    })
        .done(function (data) {
            var items = data.items;
            var totalItems = data.total;
            var totalPages = Math.ceil(totalItems / pageSize);
            var startIndex = (pageIndex * pageSize) + 1;
            var endIndex = (startIndex + pageSize) - 1;

            if (items) {
                if (endIndex > items.length) {
                    endIndex = items.length;
                }
            }
            // configure datasource
            var dataSource = {
                page: pageIndex,
                pages: totalPages,
                count: totalItems,
                start: startIndex,
                end: endIndex,
                columns: columns,
                items: items
            };
            
            // invoke callback to render repeater
            callback(dataSource);
        });
}

$('#btnSearch').on('click', function () {  
    $('#TensionReportsRepeater').repeater('render');
});

$('#btnExcelReport').on('click', function () {
  
    debugger;
    //  var search = '';

    search = '';
    var WorkStudy = $('#ddlWorkStudyID').val();

    if ((WorkStudy != '-1') || (WorkStudy != null))
        search += ';' + 'WorkStudyID:' + WorkStudy;
    if ($('#Alloy').val() != '')
        search += ';' + 'Alloy:' + $('#Alloy').val();
    if ($('#Temper').val() != '')
        search += ';' + 'Temper:' + $('#Temper').val();
    if ($('#UACPart').val() != '')
        search += ';' + 'UACPart:' + $('#UACPart').val();
    if ($('#CustPart').val() != '')
        search += ';' + 'CustPart:' + $('#CustPart').val();
    search += ';' + 'TestType:' + 'Tension';
          
    var ExportDataFilter = {
        Screen: 'Tension',     
        pageIndex: 0,
        pageSize: 10000,
        //sortDirection: options.sortDirection,
       // sortBy: options.sortProperty,
        filterBy: 'all',
        searchBy: search 
    };

    // call API, posting options
    $.ajax({
        type: 'post',
        url: GetRootDirectory() + '/RnDReports/ExportToExcel',
        headers: {
            Token: GetToken()
        },
        data: ExportDataFilter
    })
        .done(function (data) {
            bootbox.alert('exported');
        });
    ;
});

$('#btnClear').on('click', function () {
    //check if this should be dopbox - currently keep text for search    
    $('#ddlWorkStudyID').selectpicker('val', "-1");
    $('#Alloy').val('');
    $('#Temper').val('');
    $('#UACPart').val('');
    $('#CustPart').val('');
    search = '';
    search += ';' + 'TestType:' + 'Tension';
    $('#TensionReportsRepeater').repeater('render');
    return false;
}); 