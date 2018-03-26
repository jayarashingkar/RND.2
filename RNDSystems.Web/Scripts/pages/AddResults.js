$(document).ready(function () {

    $('#SCCResult').hide();
    $('#ExcoResult').hide();
    $('#btnSaveResult').hide();

    $('#TestStartDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#TestEndDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#TestDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
 //   $('#TestStartDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });

    $('#lblReturnMessage').text('');
    $('#ddTestTypesManual').attr('data-live-search', 'true');
    $('#ddTestTypesManual').selectpicker();

    $("#btnAddNew").click(function () {
        debugger;
        $('#lblReturnMessage').text('');

        var TestType = $.trim($("#ddTestTypesManual").val());

        if (TestType == RND.ResultConstant.SCC) {               
            $('#SCCResult').show();
            $('#ExcoResult').hide();
            $('#btnSaveResult').show();
        }
        else if (TestType == RND.ResultConstant.EXCO) {
            $('#SCCResult').hide();
            $('#ExcoResult').show();
            $('#btnSaveResult').show();
        }
    });

    $("#btnSaveResult").click(function () {       
        //   var optionType = $("input:radio[name='OptionRadio']:checked").val();
      
        var TestType = $.trim($("#ddTestTypesManual").val());
        var SelectedTests = $.trim($("#SelectedTests").val());
        var options;

        if (TestType == RND.ResultConstant.SCC) {
            var StressKsi = $.trim($("#StressKsi").val());
            var TimeDays = $.trim($("#TimeDays").val());
            var TestStatus = $.trim($("#TestStatus").val());
            var SpeciComment = $.trim($("#SCCSpeciComment").val());
            var Operator = $.trim($("#SCCOperator").val());
            var TestStartDate = $.trim($("#TestStartDate").val());
            var TestEndDate = $.trim($("#TestEndDate").val());

            //  $('#formStudyType').bootstrapValidator('resetForm', true);
            options1 = {
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
          
            options2 = {
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

