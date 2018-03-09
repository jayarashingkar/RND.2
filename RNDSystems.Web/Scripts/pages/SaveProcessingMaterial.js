/// <reference path="SaveAssignMaterial.js" />
$(document).ready(function () {

    $('#MillLotNo').attr('data-live-search', 'true');

    if ($('#WorkStudyID').val() !== '0') {
        $('#WorkStudyID').prop("readonly", true);
    }
   
    //Edit
    if (($("#RecId").val() != "0")) {
        $('#MillLotNo').attr("disable", true);
    }
    else {
        $('#MillLotNo').selectpicker();
    }

    $("#MillLotNo").change(function () {
        var RecID = $("#RecId").val();
        var MillLotNo = $('#MillLotNo').val();
        //  if ((RecID == "0" || RecID == undefined)){ //&& MillLotNo!="-1") {
        {          
            var LotNo = $('#MillLotNo').val();
            
            var options = {                
                recID: 0,
                MillLotNo: $('#MillLotNo').val(),
                WorkStudyId: $('#WorkStudyID').val(),
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
                    //changed here start
                        var outputHole = data.ddHole;
                        var optionHole1 = '<option value="' +
                                0 + '">' + "--Select State--" + '</option>';
                        $("#ddHole").append(optionHole1);
                        var HoleValue;
                        var HoleText;
                        var optionHole;
                        $.each(outputHole, function (i) {
                            HoleValue = outputHole[i].Value;
                            HoleText = outputHole[i].Text;

                            optionHole += '<option value="' +
                                outputHole[i].Value + '">' + outputHole[i].Text + '</option>';
                        });

                        $("#ddHole").empty();
                        $("#ddHole").append(optionHole);
                        $("#ddHole").selectpicker('refresh');

                        //PieceNo
                        var outputPieceNo = data.ddPieceNo;
                        var optionPieceNo1 = '<option value="' +
                                0 + '">' + "--Select State--" + '</option>';
                        $("#ddPieceNo").append(optionPieceNo1);
                        var PieceNoValue;
                        var PieceNoText;
                        var optionPieceNo;
                        $.each(outputPieceNo, function (i) {
                            PieceNoValue = outputPieceNo[i].Value;
                            PieceNoText = outputPieceNo[i].Text;

                            optionPieceNo += '<option value="' +
                                outputPieceNo[i].Value + '">' + outputPieceNo[i].Text + '</option>';
                        });

                        $("#ddPieceNo").empty();
                        $("#ddPieceNo").append(optionPieceNo);
                        $("#ddPieceNo").selectpicker('refresh');
                        //changed here end                  
                       
                },
                error: function(x,y,z){}
            });
        }
    });

    $('#SHTStartHrs').attr('data-live-search', 'true');
    $('#SHTStartHrs').selectpicker();

    $('#SHTStartMns').attr('data-live-search', 'true');
    $('#SHTStartMns').selectpicker();

    $('#ArtStartHrs').attr('data-live-search', 'true');
    $('#ArtStartHrs').selectpicker();

    $('#ArtStartMns').attr('data-live-search', 'true');
    $('#ArtStartMns').selectpicker();


    //changed here start
    $('#ddHole').attr('data-live-search', 'true');
    $('#ddHole').selectpicker();

    $('#ddHole').change(function () {
        var Hole = ($(this).find("option:selected").val()).trim();
        $("#Hole").val(Hole);   
    });

    $('#ddPieceNo').attr('data-live-search', 'true');
    $('#ddPieceNo').selectpicker();
    
    $('#ddPieceNo').change(function () {
        var PieceNo = ($(this).find("option:selected").val()).trim();
        $("#PieceNo").val(PieceNo);
    });

 
    $('#SHTDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#ArtAgeDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });

    var form = $('#SaveProcessingMaterial');
    form.bootstrapValidator({        
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            MillLotNo: {
                        validators: {
                            callback: {
                               
                                callback: function (value, validator, $field) {
                                    /* Get the selected options */
                                    var options = validator.getFieldElements('MillLotNo').val();
                                    return (options !== '-1');
                                }
                            }
                        }
                    },
        }

    });
});