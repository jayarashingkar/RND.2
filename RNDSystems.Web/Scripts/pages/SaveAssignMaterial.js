$(document).ready(function () {
    
    var IsAutoFilled = false;
   
    var SelectedDataBase = "USA";

    $('#btnMaterialList').on('click', function () {
        var workStudyID = $('#WorkStudyID').val();
        var recId = $('#RecId').val();
        location.href = '/AssignMaterial/AssignMaterialList?recId=' + recId + '&workStudyID=' + workStudyID;
    });

    $("#MillLotNo").keydown(function (e) {

        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });

    $("#UACPart").keydown(function (e) {

        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    
    $('#DBCntry').attr('data-live-search', 'true');
    $('#DBCntry').selectpicker();
 
    if ($('#RecID').val() !== '0') {
        $('#WorkStudyID').prop("readonly", true);
    }

    if ($('#MillLotNo').val() === '0') {
        $('#MillLotNo').val('');
    }

    if ($('#MillLotNo').val()) {
        //  debugger;
        $('#MillLotNo').prop("readonly", true);
        $('#btnSelected').prop('disabled', false);
        MillLot_No = $('#MillLotNo').val();

    }
    else {
        $('#MillLotNo').prop("readonly", false);
        $('#btnSelected').prop('disabled', true);
    }

    $("#MillLotNo").change(function () {
        //   debugger;      

        if (SelectedDataBase != 'NO') {
            //automatically fills up the values CustPart,UACPart,Alloy,Temper,SoNum

            var RecID = $("#RecID").val();

            if ((RecID == "0" || RecID == undefined) && $('#MillLotNo').val()) {
                var LotNo = $('#MillLotNo').val();

                var options = {
                    MillLotNo: $('#MillLotNo').val(),                   
                    recID: 0,
                    DataBaseName: SelectedDataBase
                };
               
                $.ajax({
                    url: Api + "api/AssignMaterial/",
                    headers: {
                        Token: GetToken()
                    },
                    type: 'Get',
                    data: options,
                    async: false,
                    dataType: "json",
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        //   debugger;
                        if (data && data.MillLotNo > 0) {
                            $('#SaveAssignMaterial').bootstrapValidator('resetForm', true);
                            $("#CustPart").val(data.CustPart);
                            $("#UACPart").val(data.UACPart);
                            $("#Alloy").val(data.Alloy);
                            $("#Temper").val(data.Temper);
                            $("#SoNum").val(data.SoNum);
                            //debugger;
                            JqueryFunction.ReadOnly();
                            $('#MillLotNo').val(data.MillLotNo)
                            $('#btnSelected').prop('disabled', false);

                            if ($('#MillLotNo').val()) {
                                MillLot_No = $('#MillLotNo').val();
                            }

                            if ($('#UACPart').val()) {
                                UACPart = $('#UACPart').val();
                            }

                            if (typeof MillLot_No !== 'undefined' && MillLot_No != null && typeof UACPart !== 'undefined' && UACPart != null) {
                                LoadGrid("UACRepeater");
                            }
                           
                            IsAutoFilled = true;
                           
                        }
                        else {
                      
                            JqueryFunction.ReadOnly();
                            $('#btnSelected').prop('disabled', true);
                            
                        }
                    },
                    error: function (x, y, z) {
                    
                        bootbox.confirm({
                            message: "The MillLotNumber is not a valid number for the selected database. Would you like to change the MillLotNo. ?",
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
                            callback: function (result){
                                if (!result){
                                    //disable and change database name to none and make it readonly.( DBCntry)
                                    //do not allow to change the MillLotNo.

                                    SelectedDataBase = "NO";                                    
                                    $('#DBCntry').val('NO').change();
                                    $('#btnSelected').prop('disabled', true);
                                    $('#DBCntry').attr("disabled", true);
                                    $('#MillLotNo').attr("readonly", true);                                 
                                }
                                else
                                {
                                    //check
                                    //debugger;
                                    $('#MillLotNo').val('');
                                    $('#SaveAssignMaterial').bootstrapValidator('resetForm', true);
                                }
                            }                            
                        });
                    }
                });
            } //if (RecID == "0" || RecID == undefined) && $('#MillLotNo').val()
        }// if (selected Database != NO)
        else {
            $('#btnSelected').prop('disabled', true);
           // $('#SaveAssignMaterial').bootstrapValidator('resetForm', true);
        }
  
    });

    $("#DBCntry").on('change', function () {
        //  debugger;
        SelectedDataBase = $(this).find("option:selected").val();
        if (SelectedDataBase == "-1")
            SelectedDataBase = "USA";
    });

    function loadSelectItems(select, items) {
        var options = '';
        var dataValue;
        var dataKey;

        $.each(items, function (key, value) {
            dataKey = key;
            dataValue = value;
            options += '<option value=' + value + '>' + value + '</option>';
        });

        select.empty();
        select.append(options);
        select.selectpicker('refresh');
    }

    if ($('#UACPart').val() === '0') {
        $('#UACPart').val('');
    }

    $('#btnAdd').on('click', function () {
        console.log('cliked');
        location.href = '/AssignMaterial/SaveAssignMaterial/0';
    });

    $('#btnSelected').on('click', function () {
        var ids = '';
        $("a[name='SelectedRecord']").each(function () {
            ids += $(this).attr('id').replace('SelectedRecord', '') + ';';
            // ids += $(this).attr('id').replace('pp_', '') + ';';
        });

        if (ids != '') {
            if ($('#MillLotNo').val() !== '' && $('#MillLotNo').val() !== '0') {
                var obj = {
                    records: ids,
                    WorkStudyID: $('#WorkStudyID').val(),
                    SoNum: $("#SoNum").val(),
                    MillLotNo: $('#MillLotNo').val(),
                    CustPart: $('#CustPart').val(),
                    UACPart: $('#UACPart').val(),
                    Alloy: $("#Alloy").val(),
                    Temper: $("#Temper").val(),
                    Hole: $("#Hole").val(),
                    PieceNo: $("#PieceNo").val(),
                    Comment: $('#Comment').val(),
                    DBCntry: SelectedDataBase
                };
                
                var json = JSON.stringify(obj);
                $.ajax({
                    url: Api + "api/UACListing",
                    headers: {
                        Token: GetToken()
                    },
                    type: 'Post',
                    data: json,
                    //data: JSON.stringify(ApiViewModel),
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data) {
                            // debugger;
                            if (!data.Success)
                                bootbox.alert(data.Message);
                            else {
                                bootbox.alert("Records added! Please click View Material List to View");
                            }
                        }
                    },
                    error: function (x, y, z) {
                    }
                });
            }
            else
                alert('Enter MillLotNo');
        }
        else {
            bootbox.alert("Please select Records!")
        }
    });

    JqueryFunction = {
        ReadOnly: function () {
          

            var inp = $("#MillLotNo").val();

            if (jQuery.trim(inp).length > 0) {
                $('#MillLotNo').prop("readonly", true);
            }
            else {
                $('#MillLotNo').prop("readonly", false);
            }

            inp = $("#CustPart").val();

            if (jQuery.trim(inp).length > 0) {
                $('#CustPart').prop("readonly", true);
            }
            else {
                $('#CustPart').prop("readonly", false);
            }

            inp = $("#UACPart").val();

            if (jQuery.trim(inp).length > 0) {
                $('#UACPart').prop("readonly", true);
            }
            else {
                $('#UACPart').prop("readonly", false);
            }

            inp = $("#Alloy").val();

            if (jQuery.trim(inp).length > 0) {
                $('#Alloy').prop("readonly", true);
            }
            else {
                $('#Alloy').prop("readonly", false);
            }

            inp = $("#Temper").val();

            if (jQuery.trim(inp).length > 0) {
                $('#Temper').prop("readonly", true);
            }
            else {
                $('#Temper').prop("readonly", false);
            }

            inp = $("#SoNum").val();

            if (jQuery.trim(inp).length > 0) {
                $('#SoNum').prop("readonly", true);
            }
            else {
                $('#SoNum').prop("readonly", false);
            }
          
            // make database also readonly
            $('#DBCntry').attr("disabled", true);
        }
    }

    //$('#btnUACPart').on('click', function () {
    //    //$('#ppUACListing').show();
    //    //var div = $('#ppUACListing').html();
    //    //dialog = bootbox.dialog({
    //    //    message: div,
    //    //    size: 'large',
    //    //    buttons: {
    //    //        cancel: {
    //    //            label: '<i class="fa fa-times"></i> Cancel',
    //    //            className: 'btn-danger'
    //    //        },
    //    //        confirm: {
    //    //            label: '<i class="fa fa-check"></i> Save',
    //    //            className: 'btn-success',
    //    //            callback: function (result) {
    //    //                console.log(result);
    //    //            }
    //    //        },
    //    //    },
    //    //    onEscape: function () {
    //    //        this.modal('hide');
    //    //    }
    //    //})
    //    //var obj = {
    //    //    id: $('#RecId').val(), MillLotNo: $('#MillLotNo').val()
    //    //};
    //    //$.ajax({
    //    //    type: 'post',
    //    //    url: GetRootDirectory() + '/AssignMaterial/UACListing',
    //    //    data: obj
    //    //}).done(function (data) {
    //    //    $('#ppUACListing').html(data);
    //    //    var div = $('#ppUACListing').html();
    //    //    dialog = bootbox.dialog({
    //    //        message: div,
    //    //        size: 'large',
    //    //        buttons: {
    //    //            cancel: {
    //    //                label: '<i class="fa fa-times"></i> Cancel',
    //    //                className: 'btn-danger'
    //    //            },
    //    //            confirm: {
    //    //                label: '<i class="fa fa-check"></i> Save',
    //    //                className: 'btn-success',
    //    //                callback: function (result) {
    //    //                    console.log(result);
    //    //                }
    //    //            },
    //    //        },
    //    //        onEscape: function () {
    //    //            this.modal('hide');
    //    //        }
    //    //    })
    //    //});
    //});


    var form = $('#SaveAssignMaterial');
    form.bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            MillLotNo: {
                excluded: false,
                validators: {
                    notEmpty: {
                        message: 'Mill Lot No. is required.'                       
                    }
                }                            
            },
            UACPart: {
                validators: {
                    notEmpty: {
                        message: 'UAC Part No is required.'
                    }
                }
            },
            Alloy: {
                validators: {
                    notEmpty: {
                        message: 'Alloy is required.'
                    }
                }
            },
            Temper: {
                validators: {
                    notEmpty: {
                        message: 'Temper is required.'
                    }
                }
            },
            GageThickness: {
                validators: {
                    notEmpty: {
                        message: 'GageThickness is required.'
                    }
                }

                //validators: {
                //    callback: {
                //        message: 'GageThickness is required.',
                //        callback: function (value, validator, $field) {
                //            //debugger;
                //            return IsAutoFilled;
                //        }
                //    }
                //}
            },
            Location2: {
                validators: {
                    notEmpty: {
                        message: 'Location2 is required.'
                    }
                }

                //validators: {
                //    callback: {
                //        message: 'Location2 is required.',
                //        callback: function (value, validator, $field) {
                //            return IsAutoFilled;

                //        }
                //    }
                //}
            },
        }
    });
});

