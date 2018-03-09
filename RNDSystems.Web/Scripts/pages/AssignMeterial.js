var columns = [

    {
        label: 'Rec. No',
        property: 'RecID',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Mill Lot No',
        property: 'MillLotNo',
        sortable: true,
        width: '30px'
    },
    {
        label: 'SoNum',
        property: 'SoNum',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Customer Part No.',
        property: 'CustPart',
        sortable: true,
        width: '15px'
    },
    {
        label: 'UAC Part No.',
        property: 'UACPart',
        sortable: true,
        width: '25px'
    },
    {
        label: 'Alloy',
        property: 'Alloy',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Temper',
        property: 'Temper',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Hole',
        property: 'Hole',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Piece No.',
        property: 'PieceNo',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Location 2',
        property: 'Location2',
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
        label: 'Material Comment',
        property: 'Comment',
        sortable: true,
        width: '15px'
    },
    //{
    //    label: 'Material Processing',
    //    property: 'MaterialProcessing',
    //    width: '50px',

    //},
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


function customColumnRenderer(helpers, callback) {
    // determine what column is being rendered
    var column = helpers.columnAttr;

    // get all the data for the entire row
    var rowData = helpers.rowData;
    var customMarkup = '';
   // debugger;
    if (typeof isUACGrid !== 'undefined' && isUACGrid) {        
        switch (column) {
            case 'RecID':
                //  customMarkup = '<input type="checkbox" name="UACPartCheck" click="selected();" value="' + rowData.RecID + '" />';                
                customMarkup = ' <input type="checkbox" name="UACPartCheck" id="UACPartCheck_' + rowData.RecID + '" class="UACPartCheck" value="' + rowData.RecID + '"/>';
                   // <input type="button" name="UACPartCheck" click="selected();" value="' + rowData.RecID + '" class="btn btn-success btn-sm center-block fa fa-plus" />';
                break;

            default:
                customMarkup = helpers.item.text();
                break;
        }
    }
    else {
        switch (column) {
            case 'RecID':
                // let's combine name and description into a single column
                customMarkup = '<div style="font-size:12px;">' + rowData.RecID + '</div>';
                break;
            case 'EntryDate':
                // let's combine name and description into a single column
                customMarkup = rowData.StrEntryDate;
                break;
            //case 'MaterialProcessing':
            //    // let's combine name and description into a single column
            //    customMarkup = "<button id='gridPM' data-RecId='" + rowData.RecID + "' data-WorkStudyID='" + rowData.WorkStudyID + "'  onclick= 'ProcessingMaterial(this)' name= 'gridPM' class='btn btn-primary btn-sm center-block' > <i class='fa fa-book'></i></button > ";
            //    break;
            case 'Edit':
                // let's combine name and description into a single column
                customMarkup = '<button onclick="GridEditClicked(' + rowData.RecID + ')" id="gridEdit" name="gridEdit" class="btn btn-info btn-sm center-block"><i class="fa fa-pencil"></i></button>';
                break;
            case 'Delete':
                // let's combine name and description into a single column
                customMarkup = '<button onclick="GridDeleteClicked(' + rowData.RecID + ')" id="gridDelete" name="gridDelete" class="btn btn-danger btn-sm center-block"><i class="fa fa-trash"></i></button>';
                break;
            default:
                // otherwise, just use the existing text value
                customMarkup = helpers.item.text();
                break;
        }
    }

    helpers.item.html(customMarkup);
    callback();
}

function customRowRenderer(helpers, callback) {
    // let's get the id and add it to the "tr" DOM element
    var item = helpers.item;
    item.attr('id', 'row' + helpers.rowData.RecID);
    callback();
}

// this example uses an API to fetch its datasource.
// the API handles filtering, sorting, searching, etc.
function customDataSource(options, callback) {
    //debugger;
    // set options
    if (typeof isUACGrid !== 'undefined' && isUACGrid) {
        columns = [
            {
                label: 'Rec. No',
                property: 'RecID',
            },
            {
                label: 'UAC Part Number',
                property: 'UACPart',
            },
            {
                label: 'Gage Thickness',
                property: 'GageThickness',
            },
            {
                label: 'Location 2',
                property: 'Location2',
            }
        ];
    }

    var pageIndex = options.pageIndex;
    var pageSize = options.pageSize;
    var search = '';

    if ($('#searchWorkStudyNumber').val())
        search += ';' + 'WorkStudyID:' + $('#searchWorkStudyNumber').val();
     

    if (typeof isUACGrid !== 'undefined' && isUACGrid) {
        //if (MillLot_No != null)
        //    search += ';' + 'MillLotNo:' + MillLot_No;

        if (UACPart != null)
            search += ';' + 'UACPart:' + UACPart;
            //    search += ';' + 'UacPartNo:' + UAC_Part;

        //if (UAC_Part != null)
        //    //  search += ';' + 'UACPartNo:' + UAC_Part;
        //    search += ';' + 'UacPartNo:' + UAC_Part;
    
    }
    else {        
        if ($('#searchMillLotNo').val())
            search += ';' + 'MillLotNo:' + $('#searchMillLotNo').val();

        //if ($('#searchCustPartNo').val())
        //    search += ';' + 'CustPartNo:' + $('#searchCustPartNo').val();

        //if ($('#searchUACPartNo').val())
        //    search += ';' + 'UACPartNo:' + $('#searchUACPartNo').val();

        if ($('#searchUACPartNo').val()) {            
            search += ';' + 'UACPart:' + $('#searchUACPartNo').val();
        }           
    }
    
    if ($('#searchAlloy').val())
        search += ';' + 'AlloyTypes:' + $('#searchAlloy').val();

    if ($('#searchTemper').val())
        search += ';' + 'TemperTypes:' + $('#searchTemper').val();
    //if ($('#Plant').val())
    //    search += ';' + 'Plant:' + $('#Plant').val();
    if (typeof isUACGrid !== 'undefined' && isUACGrid) {
        var options = {
            Screen: 'UACPartList',
            pageIndex: pageIndex,
            pageSize: pageSize,
            sortDirection: options.sortDirection,
            sortBy: options.sortProperty,
            filterBy: options.filter.value || '',
            searchBy: search || ''
        };
    }
    else
    {
        var options = {
            Screen: 'AssignMaterial',
            pageIndex: pageIndex,
            pageSize: pageSize,
            sortDirection: options.sortDirection,
            sortBy: options.sortProperty,
            filterBy: options.filter.value || '',
            searchBy: search || ''
        };
    }
   
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
          //  debugger;
            //    $("input[name='UACPartCheck']").on('change', function () {          
            $("input[class='UACPartCheck']").on('click', function () {
                var controlId = $(this).attr("id").split("_")[1];
                //  var rid = $(this).val();             
                var rid = $(this).val();
                      var selected = $.grep(items, function (x) {
                          //var checked = $("input[name='UACPartCheck']").is(':checked');
                          //if (checked)SElected
                            return x.RecID == rid;
                      })
                      //debugger;
                      if ($(this).prop('checked')) {
                          var selec = selected[0];
                          // var html = "<div>" + selec.UACPart + " - " + selec.GageThickness + " - " + selec.Location2 + "<a name='pp_as' id='pp_" + rid + "'>Remove</a>" + "</div>";
                          var html = "<div id='UACPartDiv_" + controlId + "'>" + selec.UACPart + " - " + selec.GageThickness + " - " + selec.Location2 + "<a class='SelectedRecord_" + controlId + "' name='SelectedRecord' id='SelectedRecord" + rid + "'>Remove</a>" + "</div>";
                          // $("#ppSelected").append(html)
                          $("#SelectedList").append(html)
                          $("a[name='SelectedRecord']").on('click', function () {
                              $("#UACPartCheck_" + $(this).attr("class").split("_")[1]).prop("checked", false)
                              $(this).parent().remove();
                          });
                      }
                      else {
                          $("#UACPartDiv_" + controlId).remove();

                      }


                      //if (selected && selected[0]) {      
                      //    var selec = selected[0];
                      //    // var html = "<div>" + selec.UACPart + " - " + selec.GageThickness + " - " + selec.Location2 + "<a name='pp_as' id='pp_" + rid + "'>Remove</a>" + "</div>";
                      //    var html = "<div id='UACPartDiv_" + controlId + "'>" + selec.UACPart + " - " + selec.GageThickness + " - " + selec.Location2 + "<a name='SelectedRecord' id='SelectedRecord" + rid + "'>Remove</a>" + "</div>";
                      //    // $("#ppSelected").append(html)
                      //    $("#SelectedList").append(html)
                      //    $("a[name='SelectedRecord']").on('click', function () {
                      //        $(this).parent().remove();
                      //    });
                      //}     
                //ppSelected
            });
        });
}


function GridEditClicked(id) {
    location.href = '/AssignMaterial/SaveAssignMaterial/' + id;
}


//function ProcessingMaterial(ele) {
//    var recId = $(ele).attr('data-RecId');
//    var workStudyID = $(ele).attr('data-WorkStudyID');
//    location.href = '/ProcessingMaterial/ProcessingMaterialList?recId=' + recId + '&workStudyID=' + workStudyID;
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
            if (result) {
                $.ajax({
                    url: Api + "api/AssignMaterial/" + id,
                    headers: {
                        Token: GetToken()
                    },
                    type: 'DELETE',
                    //data: JSON.stringify(ApiViewModel),
                    contentType: "application/json;charset=utf-8",
                })
                 .done(function (data) {                    
                     $('#AssignMaterialRepeater').repeater('render');
                 });
            }        
        }
    });


}

$('#btnSearch').on('click', function () {   
    $('#AssignMaterialRepeater').repeater('render');
});

$('#btnClear').on('click', function () {
    $('#searchMillLotNo').val('');
 //   $('#searchCustPartNo').val('');
    //  $('#searchUACPartNo').val('');
    $('#searchUACPartNo').val('');
   // $('#searchAlloy').val('');
  //  $('#searchTemper').val('');
    $('#AssignMaterialRepeater').repeater('render');
});



$(document).ready(function () {
    if ($('#WorkStudyID').val() !== '0') {
        $('#searchWorkStudyNumber').prop("readonly", true);
    }
    /*
    $('#AlloyTypes').attr('data-live-search', 'true');
    $('#AlloyTypes').selectpicker();
    $('#TemperTypes').attr('data-live-search', 'true');
    $('#TemperTypes').selectpicker();
    */
    $('#btnAdd').on('click', function () {
        location.href = '/AssignMaterial/SaveAssignMaterial?id=0&workStudyId=' + $('#WorkStudyID').val();
    });

    $('#btnPM').on('click', function () {
        location.href = '/AssignMaterial/SaveAssignMaterial?id=0&workStudyId=' + $('#WorkStudyID').val();
        //    customMarkup = "<button id='gridPM' data-RecId='" + rowData.RecID + "' data-WorkStudyID='" + rowData.WorkStudyID + "'  onclick= 'ProcessingMaterial(this)' name= 'gridPM' class='btn btn-primary btn-sm center-block' > <i class='fa fa-book'></i></button > ";
        var workStudyID = $('#WorkStudyID').val();
        location.href = '/ProcessingMaterial/ProcessingMaterialList?recId=0&workStudyID=' + workStudyID;
    });
});