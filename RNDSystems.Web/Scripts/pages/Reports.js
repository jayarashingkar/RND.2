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
         label: 'TestDate',
         property: 'TestDate',
         sortable: true,
     },
      {
          label: 'Completed',
          property: 'Completed',
          sortable: true,
      }
];

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
    //  item.attr('id', 'row' + helpers.rowData.RecID);
    item.attr('id', 'row' + helpers.rowData.TestingNo);
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

    if ($('#searchFromDate').val()) {
        var searchFromDate = $('#searchFromDate').datepicker();
        searchFromDate = $("#searchFromDate").data('datepicker').getFormattedDate('yyyy-mm-dd');
        if (searchFromDate && searchFromDate !== '')
            search += ';' + 'searchFromDate:' + searchFromDate;
    }
    if ($('#searchToDate').val()) {
        var searchToDate = $('#searchToDate').datepicker();
        searchToDate = $("#searchToDate").data('datepicker').getFormattedDate('yyyy-mm-dd');
        if (searchToDate && searchToDate !== '')
            search += ';' + 'searchToDate:' + searchToDate;
    }
    if ($('#ddlWorkStudyID').val())
        search += ';' + 'WorkStudyID:' + $('#ddlWorkStudyID').val();
    
    //search testing number
    //$('#ddTestType').change(function () {
    //    debugger;
    //    search += ';' + 'TestType:' + $('#ddTestType').val();
    //});
    //search += ';' + 'TestType:' + $('#ddTestType').val();

    if ($('#ddTestType').val())
        search += ';' + 'TestType:' + $('#ddTestType').val();

    //var ddtestt = $('#ddTestType').val();
    ////test remove here
    //   //var ddlTestType 
    //if ((ddtestt.trim() == 'Tension') || (ddtestt.trim() == 'Compression')) {
    //       //debugger;
    //       search += ';' + 'TestType:' + $('#ddTestType').val();
    //   }         
    //    else
    //        search += ';' + 'TestType:' + 'Tension';        
    ////ends here

    var options = {
        //Screen: 'TestingMaterialList',
        Screen: 'Reports',
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
            //$('#ReportsRepeater').repeater('render');
        });

    var selectedWSId = $('#ddlWorkStudyID').val();
    var recID = 0;
    $('#ddlWorkStudyID').change(function () {
        var options = {
            WorkStudyID: selectedWSId,
            recID: recID
        };

        $.ajax({
            url: Api + "api/Reports/",
            headers: {
                Token: GetToken()
            },
            type: 'Get',
            data: options,
            asunc: false,
            dataType: "json",
            //    dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data) {                  
                    var outputTT = data.ddTestType;
                    var option1TT = '<option value="' +
                            0 + '">' + "--Select State--" + '</option>';
                    $("#ddTestType").append(option1TT);
                    var TTValue;
                    var TTText;
                    var optionTT;

                    $.each(outputTT, function (i) {
                        TTValue = outputTT[i].Value;
                        TTText = outputTT[i].Text;

                        optionTT += '<option value="' +
                            outputTT[i].Value + '">' + outputTT[i].Text + '</option>';
                    });

                    $("#ddTestType").empty();
                    $("#ddTestType").append(optionTT);
                    $("#ddTestType").selectpicker('refresh');
                }

            },
            error: function (x, y, z) { }
        });
    });


    //$('#ddTestType').change(function () {
    //    debugger;
    //    $('#ReportsRepeater').repeater('render');
    //});

    //$('#ddTestType').change(function () {
    //    search = '';
    //    if ($('#searchFromDate').val()) {
    //        var searchFromDate = $('#searchFromDate').datepicker();
    //        searchFromDate = $("#searchFromDate").data('datepicker').getFormattedDate('yyyy-mm-dd');
    //        if (searchFromDate && searchFromDate !== '')
    //            search += ';' + 'searchFromDate:' + searchFromDate;
    //    }
    //    if ($('#searchToDate').val()) {
    //        var searchToDate = $('#searchToDate').datepicker();
    //        searchToDate = $("#searchToDate").data('datepicker').getFormattedDate('yyyy-mm-dd');
    //        if (searchToDate && searchToDate !== '')
    //            search += ';' + 'searchToDate:' + searchToDate;
    //    }
    //    if ($('#ddlWorkStudyID').val())
    //        search += ';' + 'WorkStudyID:' + $('#ddlWorkStudyID').val();

    //    search += ';' + 'TestType:' + $('#ddTestType').val();
    //    debugger;
    //    var options = {
    //        //Screen: 'TestingMaterialList',
    //        Screen: 'Reports',
    //        pageIndex: pageIndex,
    //        pageSize: pageSize,
    //        sortDirection: options.sortDirection,
    //        sortBy: options.sortProperty,
    //        filterBy: options.filter.value || '',
    //        searchBy: search || ''
    //    };
    //    debugger;
    //    // call API, posting options
    //    $.ajax({
    //        type: 'post',
    //        url: Api + 'api/grid',
    //        headers: {
    //            Token: GetToken()
    //        },
    //        data: options
    //    })
    //        .done(function (data) {
    //            var items = data.items;
    //            var totalItems = data.total;
    //            var totalPages = Math.ceil(totalItems / pageSize);
    //            var startIndex = (pageIndex * pageSize) + 1;
    //            var endIndex = (startIndex + pageSize) - 1;

    //            if (items) {
    //                if (endIndex > items.length) {
    //                    endIndex = items.length;
    //                }
    //            }
    //            // configure datasource
    //            var dataSource = {
    //                page: pageIndex,
    //                pages: totalPages,
    //                count: totalItems,
    //                start: startIndex,
    //                end: endIndex,
    //                columns: columns,
    //                items: items
    //            };


    //            // invoke callback to render repeater
    //            callback(dataSource);
    //        });
    //});


}

$('#btnSearch').on('click', function () {  
    $('#ReportsRepeater').repeater('render');
});

//$('#btnClear').on('click', function () {
//    $('#searchTestingNo').val('');
//    $('#TestingMaterialRepeater').repeater('render');
//});

$(document).ready(function () {


    $('#ddlWorkStudyID').attr('data-live-search', 'true');
    $('#ddlWorkStudyID').selectpicker();

    $('#ddTestType').attr('data-live-search', 'true');
    $('#ddTestType').selectpicker();

    $('#searchFromDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#searchFromDate').datepicker("setDate", new Date(new Date().setFullYear(new Date().getFullYear() - 1)));
    $('#searchToDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#searchToDate').datepicker("setDate", new Date());
    

});
