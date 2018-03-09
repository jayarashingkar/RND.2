/// <reference path="WorkStudyList.js" />
var columns = [
    {
        label: 'Select',
        property: 'RecID',
        sortable: true
    },
    //{
    //    label: 'Rec ID',
    //    property: 'RecID',
    //    sortable: true,
    //    width: '15px'
    //},
    {
        label: 'LPLotID',
        property: 'ProcessID',
        sortable: true,
        width: '30px'
    },
    {
        label: 'HTLogID',
        property: 'HTLogID',
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
         label: 'SHTTemp',
         property: 'SHTTemp',
         sortable: true,
         width: '25px'
     },
     {
         label: 'SHSoakHrs',
         property: 'SHSoakHrs',
         sortable: true,
         width: '15px'
     },
    {
        label: 'SHSoakMns',
        property: 'SHSoakMns',
        sortable: true,
        width: '15px'
    },
    {
        label: 'SHTStartHrs',
        property: 'SHTStartHrs',
        sortable: true,
        width: '15px'
    },
    {
        label: 'SHTStartMns',
        property: 'SHTStartMns',
        sortable: true,
        width: '15px'
    },

     {
         label: 'SHTDate',
         property: 'SHTDate',
         sortable: true,
         width: '15px'
     },
     {
         label: 'StretchPct',
         property: 'StretchPct',
         sortable: true,
         width: '15px'
     },

   //  SDT(hrs) SDTHrs
   {
       label: 'SDT Hrs',
       property: 'AfterSHTHrs',
       sortable: true,
       width: '15px'
   },
     {
         label: 'SDT Mns',
         property: 'AfterSHTMns',
         sortable: true,
         width: '15px'
     },
     //NAT(Hrs)
    {
        label: 'NatAgingHrs',
        property: 'NatAgingHrs',
        sortable: true,
        width: '15px'
    },
    {
        label: 'NatAgingMns',
        property: 'NatAgingMns',
        sortable: true,
        width: '15px'
    },
    {
        label: 'AgeLotID',
        property: 'AgeLotID',
        sortable: true,
        width: '15px'
    },
    {
         label: 'Age Start Hrs',
         property: 'ArtStartHrs',
         sortable: true,
         width: '15px'
    }, 
    {
        label: 'Age Start Mins',
        property: 'ArtStartMns',
        sortable: true,
        width: '15px'
     },
    {
        label: 'Age Start Date',
        property: 'ArtAgeDate',
        sortable: true,
        width: '15px'
    },
    {
         label: 'Age Temp1',
         property: 'ArtAgeTemp1',
         sortable: true,
         width: '15px'
    },
    //Age Time-1
    {
        label: 'Age Hrs 1',
        property: 'ArtAgeHrs1',
        sortable: true,
        width: '15px'
    },
    {
        label: 'ArtAgeMns1',
        property: 'ArtAgeMns1',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Age Temp2',
        property: 'ArtAgeTemp2',
        sortable: true,
        width: '15px'
    },
    //Age Time-2
    {
        label: 'ArtAgeHrs2',
        property: 'ArtAgeHrs2',
        sortable: true,
        width: '15px'
    },
    {
        label: 'ArtAgeMns2',
        property: 'ArtAgeMns2',
        sortable: true,
        width: '15px'
    },
    {
        label: 'ArtAgeTemp3',
        property: 'ArtAgeTemp3',
        sortable: true,
        width: '15px'
    },
     //Age Time-3
    {
        label: 'ArtAgeHrs3',
        property: 'ArtAgeHrs3',
        sortable: true,
        width: '15px'
    },
    {
        label: 'ArtAgeMns3',
        property: 'ArtAgeMns3',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Target Count',
        property: 'TargetCount',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Actual Count',
        property: 'ActualCount',
        sortable: true,
        width: '15px'
    },    
    {
        label: 'LP Temper',
        property: 'FinalTemper',
        sortable: true,
        width: '15px'
    },
    
     {
        label: 'Mill Lot No',
        property: 'MillLotNo',
        sortable: true,
        width: '30px'
    },


    //{
    //    label: 'SO num',
    //    property: 'Sonum',
    //    sortable: true,
    //    width: '15px'
    //},
    //{
    //    label: 'ProcessNo',
    //    property: 'ProcessNo',
    //    sortable: true,
    //    width: '25px'
    //},
    //{
    //    label: 'ProcessID',
    //    property: 'ProcessID',
    //    sortable: true,
    //    width: '15px'
    //},
    //{
    //    label: 'HTLogNo',
    //    property: 'HTLogNo',
    //    sortable: true,
    //    width: '15px'
    //},

    //{
    //    label: 'AgeLotNo',
    //    property: 'AgeLotNo',
    //    sortable: true,
    //    width: '15px'
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
   
    // only override the output for specific columns.
    // will default to output the text value of the row item
    switch (column) {
        //case 'RecID':
        //    // let's combine name and description into a single column
        //    customMarkup = '<div style="font-size:12px;">' + rowData.RecID + '</div>';
        //    break;
        case 'RecID':
            
           // customMarkup = '<input type="button" name="ProcessRecID" click="ProcessSelected();" value="' + rowData.RecID + '" />';
         //   customMarkup = '<input type="checkbox" id="ProcessRecID" name="ProcessRecID" onchange="ProcessSelected(' + rowData.RecID + ')"/>';
            customMarkup = ' <input type="checkbox" name="ProcessRecID" id="ProcessRecID' + rowData.RecID + '" class="ProcessRecID" value="' + rowData.RecID + '"/>';

              // customMarkup = '<input type="button" name="UACPartCheck" click="selected();" value="' + rowData.RecID + '" class="btn btn-success btn-sm center-block fa fa-plus" />';
            break;
        case 'ArtAgeDate':
            // let's combine name and description into a single column
            customMarkup = rowData.ArtAgeDate;
            break;
        case 'SHTDate':
            // let's combine name and description into a single column
            customMarkup = rowData.SHTDate;
            break;
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
    // set options
    var selectedRecords = "";

    var pageIndex = options.pageIndex;
    var pageSize = options.pageSize;
    var search = '';
    if ($('#searchWorkStudyNumber').val())
        search += ';' + 'WorkStudyID:' + $('#searchWorkStudyNumber').val();

    //if ($('#searchMillLotNo').val())
    //    search += ';' + 'MillLotNo:' + $('#searchMillLotNo').val();

    //if ($('#searchHTLogID').val())
    //    search += ';' + 'HTLogID:' + $('#searchHTLogID').val();
    //if ($('#searchAgeLotID').val())
    //    search += ';' + 'AgeLotID:' + $('#searchAgeLotID').val();

    if ($('#ddMillLotNo').val())
        search += ';' + 'MillLotNo:' + $('#ddMillLotNo').val();

    if ($('#ddHTLogID').val())
        search += ';' + 'HTLogID:' + $('#ddHTLogID').val();

    if ($('#ddAgeLotID').val())
        search += ';' + 'AgeLotID:' + $('#ddAgeLotID').val();

    var options = {
        Screen: 'ProcessingMaterial',
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

            // Check or Uncheck the record for selection to add to group.
           
            //starts here
            //$("input[name='ProcessRecID']").on('click', function () {
            //    var RId = $(this).val();
                
            //    //var selected = $.grep(items, function (x) {
            //    //    //var checked = $("input[name='UACPartCheck']").is(':checked');
            //    //    //if (checked)
            //    //    return x.RecID == rid;
            //    //})
            //    if (selectedRecords == "")
            //        selectedRecords += RId;
            //    else
            //        selectedRecords += "," + RId;
            //});
            $("input[class='ProcessRecID']").on('click', function () {
                // 
                var recID = $(this).val();
               
                if ($(this).prop('checked')) {
                    if (selectedRecords == "")
                        selectedRecords += recID;
                    else
                        selectedRecords += "," + recID;
                }
                else {
                    var removeString = selectedRecords.replace(recID, "");
                    selectedRecords = removeString;
                }
                var removecomma = selectedRecords.replace(",,", ",");
                selectedRecords = removecomma;
            });
            //ends here
            //start here

          //  var GroupName = "";
          //  var GroupType = "";
         
                $('#btnNewHTGroup').on('click', function () {
                    if (selectedRecords != "") {
                                             
                       
                        var options = {
                            GroupName: 'NEW',
                            SelectedRecords: selectedRecords,
                            GroupType: "HTLogID",
                            HTLogNo: 0
                        };
                        $.ajax({
                            url: Api + "api/Processing/",
                            headers: {
                                Token: GetToken()
                            },
                            type: 'Get',
                            data: options,
                            async: false,
                            dataType: "json",

                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data) {
                                    $("input[class='ProcessRecID']").removeAttr('checked');
                                    location.href = '/ProcessingMaterial/ProcessingMaterialList?recId=0&workStudyID=' + $('#WorkStudyID').val();
                                    
                                    //  $('#ProcessingMaterialRepeater').repeater('render');
                                }
                                 else
                                 bootbox.alert("Error in adding records to Group");
                            },
                            error: function (x, y, z) { }
                        });
                      //  GroupName = "";
                      //  GroupType = "";                     

                    }
                    else {
                      
                        alert("Please Select Records to enter new HTLogID");
                    }
                    // 
                    //selectedRecords = "";
                    //$("input[class='ProcessRecID']").removeAttr('checked');
                });

                $('#btnHTGroup').on('click', function () {
                    if (selectedRecords != "") {
                       
                        // value from the dropbox ddHTLogID
                        var GroupName = "";
                        GroupName = $('#ddHTLogID').val();
                       
                        //GroupType = "HTLogID"
                       
                        if ((GroupName != "") && (GroupName.trim() != "-1")) {
                            var options = {
                                GroupName: GroupName.trim(),
                                SelectedRecords: selectedRecords,
                                GroupType: "HTLogID"                                 
                            };
                            
                            $.ajax({
                                url: Api + "api/Processing/",
                                headers: {
                                    Token: GetToken()
                                },
                                type: 'Get',
                                data: options,
                                async: false,
                                dataType: "json",
                                //    dataType: "json",
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data) {                                       
                                        //  alert("Records added to Group");
                                        //clear the input - default - none selected in dropdown         
                                        // $('#HTLogID').text("HTLogID");
                                        $('#ddMillLotNo').selectpicker('val', "-1");
                                        $('#ddHTLogID').selectpicker('val', "-1");
                                        $('#ddAgeLotID').selectpicker('val', "-1");
                                        $("input[class='ProcessRecID']").removeAttr('checked');
                                        $('#ProcessingMaterialRepeater').repeater('render');
                                        
                                    }
                                    else {
                                       
                                        bootbox.alert("Error in adding records to Group");
                                    }
                                      
                                },
                                error: function (x, y, z) { }
                            });
                            //GroupName = "";
                            //GroupType = "";
                        }
                        else {
                          //  bootbox.alert("Please Enter HTLogID");
                        }                                                
                    }
                    else {
                     //    
                   //     bootbox.alert("Please Select Records to enter HTLogID");
                    }
                   
                });
   

                $('#btnNewAgeGroup').on('click', function () {
                     
                    if (selectedRecords != "") {
                      
                      //  GroupType = "AgeLotID"                     
                        var options = {
                                GroupName: 'NEW',
                                SelectedRecords: selectedRecords,
                                GroupType: "AgeLotID",
                            };
                            $.ajax({
                                url: Api + "api/Processing/",
                                headers: {
                                    Token: GetToken()
                                },
                                type: 'Get',
                                data: options,
                                async: false,
                                dataType: "json",                               
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                
                                    if (data) {
                                     
                                        $("input[class='ProcessRecID']").removeAttr('checked');
                                        location.href = '/ProcessingMaterial/ProcessingMaterialList?recId=0&workStudyID=' + $('#WorkStudyID').val();
                                    }
                                    else
                                       bootbox.alert("Error in adding records to Group");
                                },
                                error: function (x, y, z) { }
                            });
                            //GroupName = "";
                            //GroupType = "";                                     
                    }
                    else {                        
                        bootbox.alert("Please Select Records to enter new AgeLotID");
                    }
                    // 
                    //selectedRecords = "";
                    //$("input[class='ProcessRecID']").removeAttr('checked');
                });

                $('#btnAgeGroup').on('click', function () {             
                    if (selectedRecords != "") {
                        // value from the dropbox ddAgeLotID
                        var GroupName = "";
                        GroupName =  $('#ddAgeLotID').val();
                        
                        if ((GroupName != "") && (GroupName.trim() != "-1")) {
                            var options = {
                                GroupName: GroupName,
                                SelectedRecords: selectedRecords,
                                GroupType: "AgeLotID",
                            };
                            $.ajax({
                                url: Api + "api/Processing/",
                                headers: {
                                    Token: GetToken()
                                },
                                type: 'Get',
                                data: options,
                                async: false,
                                dataType: "json",
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    if (data) {
                                     
                                        $('#ddMillLotNo').selectpicker('val', "-1");
                                        $('#ddHTLogID').selectpicker('val', "-1");
                                        $('#ddAgeLotID').selectpicker('val', "-1");
                                        $("input[class='ProcessRecID']").removeAttr('checked');
                                        $('#ProcessingMaterialRepeater').repeater('render');
                                    }
                                    else
                                       bootbox.alert("Error in adding records to Group");
                                },
                                error: function (x, y, z) { }
                            });
                            //GroupName = "";
                            //GroupType = "";                           
                        }
                        //else {                            
                        //    bootbox.alert("Please Enter AgeLotID");
                        //}     
                    }     
                    else {
                       // bootbox.alert("Please Select Records to enter AgeLotID");
                    }
                   
                    //selectedRecords = "";
                    //$("input[class='ProcessRecID']").removeAttr('checked');
                });
        });
}
 
//function ProcessSelected(RecId) {
//    if (selectedRecords == "")
//        selectedRecords += RecId;
//    else
//        selectedRecords += "," + RecId;

//}

function GridEditClicked(id) {
   
    location.href = '/ProcessingMaterial/SaveProcessingMaterial/' + id;
}


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
                    url: Api + "api/Processing/" + id,
                    headers: {
                        Token: GetToken()
                    },
                    type: 'DELETE',
                    contentType: "application/json;charset=utf-8",                    
                })
                .done(function (data) {
                  
                    $('#ProcessingMaterialRepeater').repeater('render');
                });
            }           
        }
    });    
}

$('#btnSearch').on('click', function () {   
    $('#ProcessingMaterialRepeater').repeater('render');
    return false;
});

$('#btnClear').on('click', function () {
    //$('#searchMillLotNo').val('');    
    //$('#searchHTLogID').val('');
    //$('#searchAgeLotID').val('');

    $('#ddMillLotNo').selectpicker('val', "-1");
    $('#ddHTLogID').selectpicker('val', "-1");
    $('#ddAgeLotID').selectpicker('val', "-1");

   $('#ProcessingMaterialRepeater').repeater('render');
});


$(document).ready(function () {
 
    if ($('#WorkStudyID').val() !== '0') {
        $('#searchWorkStudyNumber').prop("readonly", true);
    }
    $('#ddHTLogID').attr('data-live-search', 'true');
    $('#ddHTLogID').selectpicker();

    $('#ddMillLotNo').attr('data-live-search', 'true');
    $('#ddMillLotNo').selectpicker();

  //  $('#ddHTLogID').prop('disabled', true);

    $('#ddAgeLotID').attr('data-live-search', 'true');
    $('#ddAgeLotID').selectpicker();
  //  $('#ddHTLogID').prop('disabled', true);

    $('#btnAddProcess').on('click', function () {        
        location.href = '/ProcessingMaterial/SaveProcessingMaterial?id=0&workStudyId=' + $('#WorkStudyID').val();
    });

});