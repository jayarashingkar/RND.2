var columns = [
      {
          label: 'Testing No',
          property: 'TestingNo',
          sortable: true,
      },
     {
         label: 'Printed',
         property: 'Printed',
         sortable:true,
     },
     //{
     //    label: 'RDStudy',
     //    property: 'WorkStudyID'
     //},     
     {
         label: 'Lot No',
         property: 'LotID',
         sortable: true,
         width: '30px'
     },
     {
         label: 'So#-Wo#',
         property: 'SoNum',
         sortable: true,
         width: '15px'
     },
     {
         label: 'Hole',
         property: 'Hole',
         sortable: true,
         width: '25px'
     },
     {
         label: 'Piece No',
         property: 'PieceNo',
         sortable: true,
         width: '15px'
     },      
    {
        label: 'Alloy',
        property: 'Alloy',
        sortable: true,
        width: '15px'
    },         
    {
        label: 'CustPartNo',
        property: 'CustPart',
        sortable: true,
        width: '15px'
    },
    {
        label: 'UAC Part No',
        property: 'UACPart',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Gage Thickness',
        property: 'GageThickness',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Orientation',
        property: 'Orientation',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Loc. 1',
        property: 'Location1',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Loc. 2',
        property: 'Location2',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Loc. 3',
        property: 'Location3',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Specimen Comment',
        property: 'SpeciComment',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Test Type',
        property: 'TestType',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Sub Test Type',
        property: 'SubTestType',
        sortable: true,
        width: '15px'
    },
    {
        label: 'MillLotNo',
        property: 'MillLotNo',
        sortable: true,
        width: '15px'
    },
    {
        label: 'TestingLab',
        property: 'TestLab',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Edit',
        property: 'Edit',
        width: '10px'
    },
    {
    label: 'Delete',
    property: 'Delete',
    width: '10px'
    }
];

var SelectedTests = ""
var selectTests;

function customColumnRenderer(helpers, callback) {
    // determine what column is being rendered
    var column = helpers.columnAttr;
    var ListofTestNos = "";
    // get all the data for the entire row
    var rowData = helpers.rowData;
    var customMarkup = '';

    // only override the output for specific columns.
    // will default to output the text value of the row item
    switch (column) {
        case 'TestingNo':
            // let's combine name and description into a single column
             //customMarkup = '<input type="checkbox" id="TestingNo" name="TestingNo" value="' + rowData.TestingNo + '" />';
           //  customMarkup = '<button id="TestingNo" name="TestingNo" onclick="TestSelected(' + rowData.TestingNo + ')" class="btn btn-inverse"><i class="fa fa-square-o"></i></button>';
      //   customMarkup = '<input type="checkbox" id="TestingNo" name="TestingNo" onchange="TestSelected(' + rowData.TestingNo+ '" class="TestingNo" value="' + rowData.TestingNo + '"/>';
            customMarkup = ' <input type="checkbox" name="CheckTesting" id="CheckTesting' + rowData.TestingNo + '" class="CheckTesting" value="' + rowData.TestingNo + '"/>';

            break;
        //case 'TestingMaterial':
        //    // let's combine name and description into a single column
        //    customMarkup = '<button onclick="GridEditClicked(' + rowData.RecId + ')" id="gridEdit" name="gridEdit" class="btn btn-success btn-sm center-block"><i class="fa fa-book"></i></button>';
        //    break;
        case 'Edit':
            // let's combine name and description into a single column
            customMarkup = '<button onclick="GridEditClicked(' + rowData.TestingNo + ')" id="gridEdit" name="gridEdit" class="btn btn-info btn-sm center-block"><i class="fa fa-pencil"></i></button>';
            break;
        case 'Delete':
            // let's combine name and description into a single column
            customMarkup = '<button onclick="GridDeleteClicked(' + rowData.TestingNo + ')" id="gridDelete" name="gridDelete" class="btn btn-danger btn-sm center-block"><i class="fa fa-trash"></i></button>';
            break;
        default:
            // otherwise, just use the existing text value
            customMarkup = helpers.item.text();
            break;
    }
    helpers.item.html(customMarkup);
    callback();


}

$('#btnSelectPrint').on('click', function () {
    //debugger;
    if (SelectedTests == null)
        SelectedTests = "";
    $("#SelectedTests").val(SelectedTests);
})
$('#btnPrint').on('click', function () {  
    SelectedTests = "";
    $("#SelectedTests").val(SelectedTests);
   // $("#SelectedTests").val(avialableTT);
})

function customRowRenderer(helpers, callback) {
    // let's get the id and add it to the "tr" DOM element
    var item = helpers.item;
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

    if ($('#WorkStudyNumber').val())
        search += ';' + 'WorkStudyID:' + $('#WorkStudyNumber').val();
    // search testing number
    if ($('#searchTestingNo').val())
        search += ';' + 'TestingNo:' + $('#searchTestingNo').val();
    
    if ($('#ddlAvailableTT').val()) {
        search += ';' + 'TestType:' + $('#ddlAvailableTT').val();
    }
   // debugger;
    //if ($('#ddlAvailableTT').val() == '-1'){
    //    flag = true;
    //    $("#ddlTestType").attr("disabled", false);
    //}
    //else{
    //    search += ';' + 'TestType:' + $('#ddlAvailableTT').val();
    //    $("#ddlTestType").attr("disabled", "disabled");
    //    flag = false;
    //}
 
    //if ($('#ddlTestType').val() && flag)
    //    search += ';' + 'TestType:' + $('#ddlTestType').val();

      //search Avialable Material Test Types
    //search Sub Test Type

    var options = {
        Screen: 'TestingMaterial',
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

            $("input[class='CheckTesting']").on('click', function () {
                debugger;
                var testingNo = $(this).val();
                if ($(this).prop('checked')) {
                    if (SelectedTests == "")
                        SelectedTests += testingNo;
                    else
                        SelectedTests += "," + testingNo;
                }
                else {                    
                    var removeString = SelectedTests.replace(testingNo, "");
                    SelectedTests = removeString;
                }
                var removecomma = SelectedTests.replace(",,", ",");
                SelectedTests = removecomma;
            });
        });
}

function GridEditClicked(id) {
    
    location.href = '/TestingMaterial/SaveTestingMaterial/' + id;
   // location.href = '/TestingMaterial/SaveTestingMaterial?id=0&workStudyId=' + $('#WorkStudyID').val() + '&avialableTT=' + avialableTT;

}


//function TestingMaterial(ele) {
//  
//    //   var recId = $(ele).attr('data-RecId');
//    //var recID = $(ele).attr('data-RecID');
//    var TestingNo = $(ele).attr('data-TestingNo');
//    var workStudyID = $(ele).attr('data-WorkStudyID');
//   
//    //location.href = '/TestingMaterial/TestingMaterialList?TestingNo=' + TestingNo + '&workStudyID=' + workStudyID;
//    location.href = '/TestingMaterial/TestingMaterialList?RecID=' + TestingNo + '&workStudyID=' + workStudyID;
//}


function GridDeleteClicked(id) {
    bootbox.confirm({
        message: RND.Constants.AreYouDelete,
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-success'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result)
            {
                $.ajax({
                    url: Api + "api/Testing/" + id,
                    headers: {
                        Token: GetToken()
                    },
                    type: 'DELETE',
                    //data: JSON.stringify(ApiViewModel),
                    contentType: "application/json;charset=utf-8",                    
                })
                 .done(function (data) {
                     $('#TestingMaterialRepeater').repeater('render');
                 });
            }
        }
    });  
}

$('#btnSearch').on('click', function () {
    
    $('#TestingMaterialRepeater').repeater('render');
     
});

$('#btnClear').on('click', function () {
    $('#searchTestingNo').val('');
    $('#ddlTestType').selectpicker('val', "-1");
    $('#ddlAvailableTT').selectpicker('val', "-1");
    $('#TestingMaterialRepeater').repeater('render');
  
});

$(document).ready(function () {
   
    //$('#ddlTestType').attr('data-live-search', 'true');
    //$('#ddlTestType').selectpicker();
    $('#ddlAvailableTT').attr('data-live-search', 'true');
    $('#ddlAvailableTT').selectpicker();
   // $('#ddlAvailableTT').prop('disabled', true);
   
    $('#ddlTestType').attr('multiple', '');
    $('#ddlTestType').attr('data-actions-box', 'true');
    $('#ddlTestType').selectpicker();
     
    var avialableTT = $('#ddlTestType').val();
    //debugger;

    //  $('#ddlTestType').on('focusout', function () {
  //   $(document).on('focusout', '#ddlTestType', function () {

   // $('#ddlTestType').blur(function () {
       // debugger;

   // $('#btnAddAvialableTT').on('click', function () {
   
   $('#ddlTestType').change(function () {
        avialableTT = $('#ddlTestType').val();
       // var avialableTT = $('#ddlTestType').find("option:selected").val();

        var options = {
            avialableTT: avialableTT
        };
 
        $.ajax({
            type: 'post',
            url: GetRootDirectory() + '/TestingMaterial/TestingMaterial',
            //'/TestingMaterial/SaveTestingMaterial?avialableTT=' + avialableTT
            data: options
        })
        .done(function (data) {
            if (data && data.isSuccess) {
                $('#ddlAvailableTT').prop('disabled', false);
               
                // avialableTT dropdown menu

                var outputSubTT = data.AvailableTestType;
                var option1SubTT = '<option value="' +
                        0 + '">' + "--Select State--" + '</option>';
                $("#ddlAvailableTT").append(option1SubTT);
                var SubTTValue;
                var SubTTText;
                var optionSubTT;
                $.each(outputSubTT, function (i) {
                    SubTTValue = outputSubTT[i].Value;
                    SubTTText = outputSubTT[i].Text;

                    optionSubTT += '<option value="' +
                        outputSubTT[i].Value + '">' + outputSubTT[i].Text + '</option>';
                });

                $("#ddlAvailableTT").empty();
                $("#ddlAvailableTT").append(optionSubTT);
                $("#ddlAvailableTT").selectpicker('refresh');
                
            }
            else {   
                $('#ddlAvailableTT').prop('disabled', true);
            }
                
        });
    });
 
 
    if ($('#WorkStudyID').val() !== '0') {
        $('#WorkStudyNumber').prop("readonly", true);
    }

    $('#btnAddTesting').on('click', function () {        
        avialableTT = $('#ddlTestType').val();       
        if ((avialableTT == "-1") || (avialableTT == null) || (avialableTT == ""))
        {
            isTestTypeSelected(avialableTT);         
        }
        else {
            location.href = '/TestingMaterial/SaveTestingMaterial?id=0&workStudyId=' + $('#WorkStudyID').val() + '&avialableTT=' + avialableTT;
        }
    });
});

function isTestTypeSelected(avialableTT) {
    bootbox.confirm({
        message: "None of the Test Types are Selected. Are you sure you want to proceed?",
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-success'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {
                location.href = '/TestingMaterial/SaveTestingMaterial?id=0&workStudyId=' + $('#WorkStudyID').val() + '&avialableTT=' + avialableTT;
            }
        }
    });
}