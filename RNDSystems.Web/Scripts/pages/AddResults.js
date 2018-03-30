var columns = [
      {
          label: 'Add',
          property: 'Add',
          sortable: true,
          width: '15px'
      },
       {
           label: 'Testing No',
           property: 'TestingNo',
           sortable: true,
           width: '30px'
       },    
    {
        label: 'Test Type',
        property: 'TestType',
        sortable: true,
        width: '30px'
    }  
];

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
        case 'Add':
            // let's combine name and description into a single column
             customMarkup = "<button id='gridAdd' data-RecId='" + rowData.TestingNo + "' data-TestType='" + rowData.TestType + "'  onclick= 'GridAddClicked(this)' name= 'gridAdd' class='btn btn-success btn-sm center-block' > <i class='fa fa-plus'></i></button > ";
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
    item.attr('id', 'row' + helpers.rowData.TestingNo);
    callback();
}

function customDataSource(options, callback) {

    var pageIndex = options.pageIndex;
    var pageSize = options.pageSize;
    var search = '';
    var flag = true;

   search = $('#SelectedTests').val();

    var options = {
        Screen: 'Results',
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
            var Message = data.message;
            $('#lblGridMessage').text(Message);
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

function GridAddClicked(ele) {

   $('#gridAdd').prop('disabled', true);

    var id = $(ele).attr('data-RecId');
    var TestType = $(ele).attr('data-TestType').trim();
    $('#SCCResult').hide();
    $('#ExcoResult').hide();
    $('#MacroEtchResult').hide();
    $('#OpticalMountResult').hide();
    $('#HardnessResult').hide();
    $('#FatigueCrackGrowthResult').hide();
    $('#IGCResult').hide();
    $('#btnSaveResult').show();

    if (TestType == RND.ResultConstant.SCC) {
        $('#SCCResult').show();        
    }
    else if (TestType == RND.ResultConstant.EXCO) {
        $('#ExcoResult').show();
    }
    else if (TestType == RND.ResultConstant.MacroEtch) {       
        $('#MacroEtchResult').show();       
    }
    else if (TestType == RND.ResultConstant.OpticalMount) {     
        $('#OpticalMountResult').show();      
    }
    else if (TestType == RND.ResultConstant.Hardness) {
        $('#HardnessResult').show();
    }
    else if (TestType == RND.ResultConstant.FatigueCrackGrowth) {   
        $('#FatigueCrackGrowthResult').show();   
    }
    else if (TestType == RND.ResultConstant.IGC) { 
        $('#IGCResult').show();
    }
    $("#btnSaveResult").click(function () {
        debugger;


        if (TestType == RND.ResultConstant.SCC) {

            var StressKsi = $.trim($("#StressKsi").val());
            var TimeDays = $.trim($("#TimeDays").val());
            var TestStatus = $.trim($("#TestStatus").val());
            var SpeciComment = $.trim($("#SCCSpeciComment").val());
            var Operator = $.trim($("#SCCOperator").val());
            var TestStartDate = $.trim($("#TestStartDate").val());
            var TestEndDate = $.trim($("#TestEndDate").val());

            var options1 = {
                SelectedTests: id,
                StressKsi: StressKsi,
                TimeDays: TimeDays,
                TestStatus: TestStatus,
                SpeciComment: SpeciComment,
                Operator: Operator,
                TestStartDate: TestStartDate,
                TestEndDate: TestEndDate,
            };
            $.ajax({
                type: 'Post',
                url: Api + 'api/SCCResults',
                headers: {
                    Token: GetToken()
                },
                data: options1
            })
           .done(function (data) {

               $('#SCCResult').hide();
               $('#ExcoResult').hide();
               $('#MacroEtchResult').hide();
               $('#OpticalMountResult').hide();
               $('#HardnessResult').hide();
               $('#FatigueCrackGrowthResult').hide();
               $('#IGCResult').hide();
               $('#btnSaveResult').hide();
               if (data.Success) {

                   var message = data.Message + ': successfully added'
                   $('#lblReturnMessage').text(message);
               }
               else {
                   $('#lblReturnMessage').text('ERROR inserting Results');
               }
           });
        }
        else
            if (TestType == RND.ResultConstant.EXCO) {

                var ExcoRating = $.trim($("#ExcoRating").val());
                var StartWT = $.trim($("#StartWT").val());
                var FinalWT = $.trim($("#FinalWT").val());
                var ExposedArea = $.trim($("#ExposedArea").val());
                var StartpH = $.trim($("#StartpH").val());
                var FinalpH = $.trim($("#FinalpH").val());
                var SpeciComment = $.trim($("#EXCOSpeciComment").val());
                var Operator = $.trim($("#EXCOOperator").val());
                var TestDate = $.trim($("#TestDate").val());
                var TimeHrs = $.trim($("#TimeHrs").val());
                var TimeMns = $.trim($("#TimeMns").val());
                var BatchNo = $.trim($("#BatchNo").val());
                //  $('#formStudyType').bootstrapValidator('resetForm', true);

                var options2 = {
                    SelectedTests: id,
                    ExcoRating: ExcoRating,
                    StartWT: StartWT,
                    FinalWT: FinalWT,
                    ExposedArea: ExposedArea,
                    StartpH: StartpH,
                    FinalpH: FinalpH,
                    SpeciComment: SpeciComment,
                    Operator: Operator,
                    TestDate: TestDate,
                    TimeHrs: TimeHrs,
                    TimeMns: TimeMns,
                    BatchNo: BatchNo
                };
                $.ajax({
                    type: 'Post',
                    url: Api + 'api/EXCOResults',
                    headers: {
                        Token: GetToken()
                    },
                    data: options2
                })
               .done(function (data) {

                   $('#SCCResult').hide();
                   $('#ExcoResult').hide();
                   $('#MacroEtchResult').hide();
                   $('#OpticalMountResult').hide();
                   $('#HardnessResult').hide();
                   $('#FatigueCrackGrowthResult').hide();
                   $('#IGCResult').hide();
                   $('#btnSaveResult').hide();
                   if (data.Success) {

                       var message = data.Message + ': successfully added'
                       $('#lblReturnMessage').text(message);
                   }
                   else {
                       $('#lblReturnMessage').text('ERROR inserting Results');
                   }
               });
            }
            else
                if (TestType == RND.ResultConstant.MacroEtch) {

                    var MaxRexGrainDepth = $.trim($("#MaxRexGrainDepth").val());
                    var MacroEtchSpeciComment = $.trim($("#MacroEtchSpeciComment").val());
                    var MacroEtchOperator = $.trim($("#MacroEtchOperator").val());
                    var MacroEtchTestDate = $.trim($("#MacroEtchTestDate").val());
                    var MacroEtchTimeHrs = $.trim($("#MacroEtchTimeHrs").val());
                    var MacroEtchTimeMns = $.trim($("#MacroEtchTimeMns").val());
                    //  $('#formStudyType').bootstrapValidator('resetForm', true);

                    var options3 = {
                        MaxRexGrainDepth: MaxRexGrainDepth,
                        SelectedTests: id,
                        SpeciComment: MacroEtchSpeciComment,
                        Operator: MacroEtchOperator,
                        TestDate: MacroEtchTestDate,
                        TimeHrs: MacroEtchTimeHrs,
                        TimeMns: MacroEtchTimeMns
                    };
                    $.ajax({
                        type: 'Post',
                        url: Api + 'api/MacroEtch',
                        headers: {
                            Token: GetToken()
                        },
                        data: options3
                    })
                   .done(function (data) {

                       $('#SCCResult').hide();
                       $('#ExcoResult').hide();
                       $('#MacroEtchResult').hide();
                       $('#HardnessResult').hide();
                       $('#FatigueCrackGrowthResult').hide();
                       $('#btnSaveResult').hide();
                       if (data.Success) {

                           var message = data.Message + ': successfully added'
                           $('#lblReturnMessage').text(message);
                       }
                       else {
                           $('#lblReturnMessage').text('ERROR inserting Results');
                       }
                   });
                }
                else
                    if (TestType == RND.ResultConstant.OpticalMount) {

                        var OpticalMountSpeciComment = $.trim($("#OpticalMountSpeciComment").val());
                        var OpticalMountOperator = $.trim($("#OpticalMountOperator").val());
                        var OpticalMountTestDate = $.trim($("#OpticalMountTestDate").val());
                        var OpticalMountTimeHrs = $.trim($("#OpticalMountTimeHrs").val());
                        var OpticalMountTimeMns = $.trim($("#OpticalMountTimeMns").val());

                        var options4 = {
                            SelectedTests: id,
                            SpeciComment: OpticalMountSpeciComment,
                            Operator: OpticalMountOperator,
                            TestDate: OpticalMountTestDate,
                            TimeHrs: OpticalMountTimeHrs,
                            TimeMns: OpticalMountTimeMns
                        };
                        $.ajax({
                            type: 'Post',
                            url: Api + 'api/OpticalMount',
                            headers: {
                                Token: GetToken()
                            },
                            data: options4
                        })
                       .done(function (data) {

                           $('#SCCResult').hide();
                           $('#ExcoResult').hide();
                           $('#OpticalMountResult').hide();
                           $('#MacroEtchResult').hide();
                           $('#HardnessResult').hide();
                           $('#FatigueCrackGrowthResult').hide();
                           $('#btnSaveResult').hide();
                           if (data.Success) {

                               var message = data.Message + ': successfully added'
                               $('#lblReturnMessage').text(message);
                           }
                           else {
                               $('#lblReturnMessage').text('ERROR inserting Results');
                           }
                       });
                    }
                    else
                        if (TestType == RND.ResultConstant.Hardness) {

                            var HardnessSubConduct = $.trim($("#HardnessSubConduct").val());
                            var HardnessSurfConduct = $.trim($("#HardnessSurfConduct").val());
                            var Hardness = $.trim($("#Hardness").val());
                            var HardnessSpeciComment = $.trim($("#HardnessSpeciComment").val());
                            var HardnessOperator = $.trim($("#HardnessOperator").val());
                            var HardnessTestDate = $.trim($("#HardnessTestDate").val());
                            var HardnessTimeHrs = $.trim($("#HardnessTimeHrs").val());
                            var HardnessTimeMns = $.trim($("#HardnessTimeMns").val());

                            var options5 = {
                               SelectedTests: id,
                                SubConduct: HardnessSubConduct,
                                SurfConduct: HardnessSurfConduct,
                                Hardness: Hardness,
                                SpeciComment: HardnessSpeciComment,
                                Operator: HardnessOperator,
                                TestDate: HardnessTestDate,
                                TimeHrs: HardnessTimeHrs,
                                TimeMns: HardnessTimeMns
                            };
                            $.ajax({
                                type: 'Post',
                                url: Api + 'api/Hardness',
                                headers: {
                                    Token: GetToken()
                                },
                                data: options5
                            })
                           .done(function (data) {

                               $('#SCCResult').hide();
                               $('#ExcoResult').hide();
                               $('#MacroEtchResult').hide();
                               $('#OpticalMountResult').hide();
                               $('#HardnessResult').hide();
                               $('#FatigueCrackGrowthResult').hide();
                               $('#btnSaveResult').hide();
                               if (data.Success) {

                                   var message = data.Message + ': successfully added'
                                   $('#lblReturnMessage').text(message);
                               }
                               else {
                                   $('#lblReturnMessage').text('ERROR inserting Results');
                               }
                           });
                        }
                        else
                            if (TestType == RND.ResultConstant.FatigueCrackGrowth) {

                                var SpecimenDrawing = $.trim($("#SpecimenDrawing").val());
                                var MinStress = $("#MinStress").val();
                                var MaxStress = $("#MaxStress").val();
                                var MinLoad = $("#MinLoad").val();
                                var MaxLoad = $("#MaxLoad").val();
                                var WidthOrDia = $("#WidthOrDia").val();
                                var Thickness = $("#Thickness").val();
                                var HoleDia = $("#HoleDia").val();
                                var AvgChamferDepth = $("#AvgChamferDepth").val();
                                var Frequency = $.trim($("#Frequency").val());
                                var CyclesToFailure = $("#CyclesToFailure").val();
                                var Roughness = $("#Roughness").val();
                                var TestFrame = $.trim($("#TestFrame").val());
                                var Comment = $.trim($("#Comment").val());
                                var FractureLocation = $.trim($("#FractureLocation").val());
                                var FatigueCrackGrowthOperator = $.trim($("#FatigueCrackGrowthOperator").val());
                                var FatigueCrackGrowthTestDate = $.trim($("#FatigueCrackGrowthTestDate").val());
                                var FatigueCrackGrowthTestTime = $.trim($("#FatigueCrackGrowthTestTime").val());
                                var options7 = {
                                    SelectedTests: id,
                                    SpecimenDrawing: SpecimenDrawing,
                                    MinStress: MinStress,
                                    MaxStress: MaxStress,
                                    MinLoad: MinLoad,
                                    MaxLoad: MaxLoad,
                                    WidthOrDia: WidthOrDia,
                                    Thickness: Thickness,
                                    HoleDia: HoleDia,
                                    AvgChamferDepth: AvgChamferDepth,
                                    Frequency: Frequency,
                                    CyclesToFailure: CyclesToFailure,
                                    Roughness: Roughness,
                                    TestFrame: TestFrame,
                                    Comment: Comment,
                                    FractureLocation: FractureLocation,
                                    Operator: FatigueCrackGrowthOperator,
                                    TestDate: FatigueCrackGrowthTestDate,
                                    TestTime: FatigueCrackGrowthTestTime
                                };
                                $.ajax({
                                    type: 'Post',
                                    url: Api + 'api/FatigueCrackGrowth',
                                    headers: {
                                        Token: GetToken()
                                    },
                                    data: options7
                                })
                               .done(function (data) {

                                   $('#SCCResult').hide();
                                   $('#ExcoResult').hide();
                                   $('#MacroEtchResult').hide();
                                   $('#OpticalMountResult').hide();
                                   $('#FatigueCrackGrowthResult').hide();
                                   $('#FatigueCrackGrowthResult').hide();
                                   $('#IGCResult').hide();
                                   $('#btnSaveResult').hide();
                                   if (data.Success) {

                                       var message = data.Message + ': successfully added'
                                       $('#lblReturnMessage').text(message);
                                   }
                                   else {
                                       $('#lblReturnMessage').text('ERROR inserting Results');
                                   }
                               });
                            }
                            else
                                if (TestType == RND.ResultConstant.IGC) {

                                    var IGCSubConduct = $.trim($("#IGCSubConduct").val());
                                    var IGCSurfConduct = $.trim($("#IGCSurfConduct").val());
                                    var IGCMinDepth = $.trim($("#IGCMinDepth").val());
                                    var IGCMaxDepth = $.trim($("#IGCMaxDepth").val());
                                    var IGCAvgDepth = $.trim($("#IGCAvgDepth").val());
                                    var IGCSpeciComment = $.trim($("#IGCSpeciComment").val());
                                    var IGCOperator = $.trim($("#IGCOperator").val());
                                    var IGCTestDate = $.trim($("#IGCTestDate").val());
                                    var IGCTimeHrs = $.trim($("#IGCTimeHrs").val());
                                    var IGCTimeMns = $.trim($("#IGCTimeMns").val());

                                    var options8 = {
                                        SelectedTests: id,
                                        SubConduct: IGCSubConduct,
                                        SurfConduct: IGCSurfConduct,
                                        MinDepth: IGCMinDepth,
                                        MaxDepth: IGCMaxDepth,
                                        AvgDepth: IGCAvgDepth,
                                        SpeciComment: IGCSpeciComment,
                                        Operator: IGCOperator,
                                        TestDate: IGCTestDate,
                                        TimeHrs: IGCTimeHrs,
                                        TimeMns: IGCTimeMns
                                    };
                                    $.ajax({
                                        type: 'Post',
                                        url: Api + 'api/IGC',
                                        headers: {
                                            Token: GetToken()
                                        },
                                        data: options8
                                    })
                                   .done(function (data) {

                                       $('#SCCResult').hide();
                                       $('#ExcoResult').hide();
                                       $('#MacroEtchResult').hide();
                                       $('#OpticalMountResult').hide();
                                       $('#IGCResult').hide();
                                       $('#IGCResult').hide();
                                       $('#IGCResult').hide();
                                       $('#btnSaveResult').hide();
                                       if (data.Success) {

                                           var message = data.Message + ': successfully added'
                                           $('#lblReturnMessage').text(message);
                                       }
                                       else {
                                           $('#lblReturnMessage').text('ERROR inserting Results');
                                       }
                                   });
                                }

    });


}


$(document).ready(function () {
    $('#SCCResult').hide();
    $('#ExcoResult').hide();
    $('#MacroEtchResult').hide();
    $('#OpticalMountResult').hide();
    $('#HardnessResult').hide();
    $('#FatigueCrackGrowthResult').hide();
    $('#IGCResult').hide();
    $('#btnSaveResult').hide();
     
    $('#TestStartDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#TestEndDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#TestDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#MacroEtchTestDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#OpticalMountTestDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#HardnessTestDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#FatigueCrackGrowthTestDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#IGCTestDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });

    if ($('#TestStartDate').val() === '') {
        $('#TestStartDate').datepicker("setDate", new Date());
    }
    if ($('#TestEndDate').val() === '') {
        $('#TestEndDate').datepicker("setDate", new Date());
    }
    if ($('#TestDate').val() === '') {
        $('#TestDate').datepicker("setDate", new Date());
    }
    if ($('#MacroEtchTestDate').val() === '') {
        $('#MacroEtchTestDate').datepicker("setDate", new Date());
    }
    if ($('#OpticalMountTestDate').val() === '') {
        $('#OpticalMountTestDate').datepicker("setDate", new Date());
    }
    if ($('#HardnessTestDate').val() === '') {
        $('#HardnessTestDate').datepicker("setDate", new Date());
    }
    if ($('#FatigueCrackGrowthTestDate').val() === '') {
        $('#FatigueCrackGrowthTestDate').datepicker("setDate", new Date());
    }
    if ($('#IGCTestDate').val() === '') {
        $('#IGCTestDate').datepicker("setDate", new Date());
    }

    $('#lblReturnMessage').text('');
    $('#lblGridMessage').text('');

    $('#ddTestTypesManual').attr('data-live-search', 'true');
    $('#ddTestTypesManual').selectpicker();
    
});

