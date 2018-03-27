$(document).ready(function () {

    $('#SCCResult').hide();
    $('#ExcoResult').hide();
    $('#MacroEtchResult').hide();
    $('#OpticalMountResult').hide();
    $('#HardnessResult').hide();
    $('#FatigueResult').hide();
    $('#IGCResult').hide();
    $('#btnSaveResult').hide();
     
    $('#TestStartDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#TestEndDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#TestDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#MacroEtchTestDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#OpticalMountTestDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#HardnessTestDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#FatigueTestDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#IGCTestDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });

    
 //   $('#TestStartDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
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
    if ($('#FatigueTestDate').val() === '') {
        $('#FatigueTestDate').datepicker("setDate", new Date());
    }
    if ($('#IGCTestDate').val() === '') {
        $('#IGCTestDate').datepicker("setDate", new Date());
    }

    $('#lblReturnMessage').text('');
    $('#ddTestTypesManual').attr('data-live-search', 'true');
    $('#ddTestTypesManual').selectpicker();

    $("#btnAddNew").click(function () {
        $('#lblReturnMessage').text('');

        var TestType = $.trim($("#ddTestTypesManual").val());

        if (TestType == RND.ResultConstant.SCC) {               
            $('#SCCResult').show();
            $('#ExcoResult').hide();
            $('#MacroEtchResult').hide();
            $('#OpticalMountResult').hide();
            $('#HardnessResult').hide();
            $('#FatigueResult').hide();
            $('#IGCResult').hide();
            $('#btnSaveResult').show();
        }
        else if (TestType == RND.ResultConstant.EXCO) {
            $('#SCCResult').hide();
            $('#ExcoResult').show();
            $('#MacroEtchResult').hide();
            $('#OpticalMountResult').hide();
            $('#HardnessResult').hide();
            $('#FatigueResult').hide();
            $('#IGCResult').hide();
            $('#btnSaveResult').show();
        }
        else if (TestType == RND.ResultConstant.MacroEtch) {
            $('#SCCResult').hide();
            $('#ExcoResult').hide();
            $('#MacroEtchResult').show();
            $('#OpticalMountResult').hide();
            $('#HardnessResult').hide();
            $('#FatigueResult').hide();
            $('#IGCResult').hide();
            $('#btnSaveResult').show();
        }
        else if (TestType == RND.ResultConstant.OpticalMount) {
            $('#SCCResult').hide();
            $('#ExcoResult').hide();
            $('#MacroEtchResult').hide();
            $('#OpticalMountResult').show();
            $('#HardnessResult').hide();
            $('#FatigueResult').hide();
            $('#IGCResult').hide();
            $('#btnSaveResult').show();
        }
        else if (TestType == RND.ResultConstant.Hardness) {
            $('#SCCResult').hide();
            $('#ExcoResult').hide();
            $('#MacroEtchResult').hide();
            $('#OpticalMountResult').hide();
            $('#HardnessResult').show();
            $('#FatigueResult').hide();
            $('#IGCResult').hide();
            $('#btnSaveResult').show();
        }
        else if (TestType == RND.ResultConstant.Fatigue) {
            $('#SCCResult').hide();
            $('#ExcoResult').hide();
            $('#MacroEtchResult').hide();
            $('#OpticalMountResult').hide();
            $('#HardnessResult').hide();
            $('#FatigueResult').show();
            $('#IGCResult').hide();
            $('#btnSaveResult').show();
        }
        else if (TestType == RND.ResultConstant.IGC) {
            $('#SCCResult').hide();
            $('#ExcoResult').hide();
            $('#MacroEtchResult').hide();
            $('#OpticalMountResult').hide();
            $('#HardnessResult').hide();
            $('#FatigueResult').hide();
            $('#IGCResult').show();
            $('#btnSaveResult').show();
        }
        debugger;
    });

    $("#btnSaveResult").click(function () {       
        //   var optionType = $("input:radio[name='OptionRadio']:checked").val();
        debugger;
        var TestType = $.trim($("#ddTestTypesManual").val());
        var SelectedTests = $.trim($("#SelectedTests").val());
      //  var options1;

        if (TestType == RND.ResultConstant.SCC) {
            var StressKsi = $.trim($("#StressKsi").val());
            var TimeDays = $.trim($("#TimeDays").val());
            var TestStatus = $.trim($("#TestStatus").val());
            var SpeciComment = $.trim($("#SCCSpeciComment").val());
            var Operator = $.trim($("#SCCOperator").val());
            var TestStartDate = $.trim($("#TestStartDate").val());
            var TestEndDate = $.trim($("#TestEndDate").val());

            //  $('#formStudyType').bootstrapValidator('resetForm', true);
            var options1 = {
                SelectedTests: SelectedTests,                
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
               $('#FatigueResult').hide();
               $('#IGCResult').hide();
               $('#btnSaveResult').hide();
               if (data.Success) {

                   var message = data.Message + ': successfully added'
                   $('#lblReturnMessage').text(message);
               }
               else {
                   // var message = TestType + ': did not get added'
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
                SelectedTests: SelectedTests,              
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
               $('#FatigueResult').hide();
               $('#IGCResult').hide();
               $('#btnSaveResult').hide();
               if (data.Success) {

                   var message = data.Message + ': successfully added'
                   $('#lblReturnMessage').text(message);
               }
               else {
                   // var message = TestType + ': did not get added'
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
                    SelectedTests: SelectedTests,                   
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
                   $('#FatigueResult').hide();
                   $('#btnSaveResult').hide();
                   if (data.Success) {

                       var message = data.Message + ': successfully added'
                       $('#lblReturnMessage').text(message);
                   }
                   else {
                       // var message = TestType + ': did not get added'
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
                    //  $('#formStudyType').bootstrapValidator('resetForm', true);

                    var options4 = {                      
                        SelectedTests: SelectedTests,
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
                       $('#FatigueResult').hide();
                       $('#btnSaveResult').hide();
                       if (data.Success) {

                           var message = data.Message + ': successfully added'
                           $('#lblReturnMessage').text(message);
                       }
                       else {
                           // var message = TestType + ': did not get added'
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
                            SelectedTests: SelectedTests,
                            SubConduct: HardnessSubConduct,
                            SurfConduct: HardnessSurfConduct,
                            Hardness:Hardness,
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
                           $('#FatigueResult').hide();
                           $('#btnSaveResult').hide();
                           if (data.Success) {

                               var message = data.Message + ': successfully added'
                               $('#lblReturnMessage').text(message);
                           }
                           else {
                               // var message = TestType + ': did not get added'
                               $('#lblReturnMessage').text('ERROR inserting Results');
                           }
                       });
                    }
                    else
                        if (TestType == RND.ResultConstant.Fatigue) {
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
                            var FatigueOperator = $.trim($("#FatigueOperator").val());
                            var FatigueTestDate = $.trim($("#FatigueTestDate").val());
                            var FatigueTestTime = $.trim($("#FatigueTestTime").val());
                            var options7 = {
                                SelectedTests: SelectedTests,
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
                                Operator: FatigueOperator,
                                TestDate: FatigueTestDate,
                                TestTime: FatigueTestTime
                            };
                            $.ajax({
                                type: 'Post',
                                url: Api + 'api/Fatigue',
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
                               $('#FatigueResult').hide();
                               $('#FatigueResult').hide();
                               $('#IGCResult').hide();
                               $('#btnSaveResult').hide();
                               if (data.Success) {

                                   var message = data.Message + ': successfully added'
                                   $('#lblReturnMessage').text(message);
                               }
                               else {
                                   // var message = TestType + ': did not get added'
                                   $('#lblReturnMessage').text('ERROR inserting Results');
                               }
                           });
                        }
                        else
                            if (TestType == RND.ResultConstant.IGC) {
                                debugger;
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
                                    SelectedTests: SelectedTests,
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
                                       // var message = TestType + ': did not get added'
                                       $('#lblReturnMessage').text('ERROR inserting Results');
                                   }
                               });
                            }

        });
    
    //var formType = $("input:radio[name='OptionRadio']:checked").val();
    //if (formType == RND.OptionConstant.Location) {
    //    $('#formLocation').bootstrapValidator({
    //        message: 'This value is not valid',
    //        feedbackIcons: {
    //            valid: 'glyphicon glyphicon-ok',
    //            invalid: 'glyphicon glyphicon-remove',
    //            validating: 'glyphicon glyphicon-refresh'
    //        },
    //        fields: {
    //            PlantDesc: {
    //                validators: {
    //                    notEmpty: {
    //                        message: 'Plant Desciption ID is required.'
    //                    }
    //                }
    //            },
    //            PlantState: {
    //                validators: {
    //                    notEmpty: {
    //                        message: 'Plant State ID is required.'
    //                    }
    //                }
    //            },
    //            PlantType: {
    //                validators: {
    //                    notEmpty: {
    //                        message: 'Plant Type ID is required.'
    //                    }
    //                }
    //            }
    //        }
    //    });
    //}
    //else
    //    if (formType == RND.OptionConstant.StudyType) {
    //        $('#formStudyType').bootstrapValidator({
    //            message: 'This value is not valid',
    //            feedbackIcons: {
    //                valid: 'glyphicon glyphicon-ok',
    //                invalid: 'glyphicon glyphicon-remove',
    //                validating: 'glyphicon glyphicon-refresh'
    //            },
    //            fields: {
    //                TypeDesc: {
    //                    validators: {
    //                        notEmpty: {
    //                            message: 'Type Desciption ID is required.'
    //                        }
    //                    }
    //                }
    //            }
    //        });

    //    }



    //$("#btnEditOption").click(function () {
    //    var optionType = $("input:radio[name='OptionRadio']:checked").val();

    //    if (optionType == RND.OptionConstant.StudyType) {
    //        debugger;
    //        $('#LocationOption').hide();
    //        $('#StudyTypeOptionOption').show();
    //        $('#RecId').val($('#ddStudyTypeList').val());

    //    }
    //    else if (optionType == RND.OptionConstant.Location) {
    //        debugger;
    //        $('#LocationOption').show();
    //        $('#StudyTypeOptionOption').hide();
    //        $('#RecId').val($('#ddLocationList').val());
    //    }
    //});

    //$("#btnDeleteOption").click(function () {

    //    var optionType = $("input:radio[name='OptionRadio']:checked").val();

    //    if (optionType == RND.OptionConstant.StudyType) {
    //        debugger;
    //       // $('#LocationOption').hide();
    //        //$('#StudyTypeOptionOption').show();

    //        var id = $('#ddStudyTypeList').val();
    //        var text = $('#ddStudyTypeList').text();
    //        var data = {
    //            Message: id,
    //            Message1: optionType
    //        };

    //        bootbox.confirm({
    //            message: RND.Constants.AreYouDelete,
    //            buttons: {
    //                confirm: {
    //                    label: 'Yes',
    //                    className: 'btn-success'
    //                },
    //                cancel: {
    //                    label: 'No',
    //                    className: 'btn-danger'
    //                }
    //            },
    //            callback: function (result) {
    //                if (result) {
    //                    $.ajax({
    //                        url: Api + "api/options/" + id,
    //                        headers: {
    //                            Token: GetToken()
    //                        },
    //                        data: data,
    //                        type: 'DELETE',                           
    //                        contentType: "application/json;charset=utf-8",                    
    //                    })
    //                    .done(function (data) {
    //                        var message = text + ': successfully deleted'
    //                        $('#lblReturnMessage').text(message);
    //                    });
    //                }         
    //            }
    //    });

    //    }
    //    else if (optionType == RND.OptionConstant.Location) {
    //        debugger;
    //      //  $('#LocationOption').show();
    //       // $('#StudyTypeOptionOption').hide();
    //        $('#RecId').val($('#ddLocationList').val());

    //    }
    //});


});

