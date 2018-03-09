$(document).ready(function () {

    if ($('#RecId').val() !== '0') {
        $('#WorkStudyID').prop("readonly", true);
    }
    else
        $('#btnAM').prop("disabled", 'disabled');
    $('#StudyStatus').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    $('#StudyType').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    $('#Plant').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    $('#StartDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });

    //  $('#StartDate').datepicker("update", new Date());   

    if ($('#StartDate').val() === '') {
        $('#StartDate').datepicker("setDate", new Date());
    }
   
    $('#DueDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#CompleteDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#btnAdd').on('click', function () {
        location.href = '/WorkStudy/SaveWorkStudy/0'
    });
    $('#btnAM').on('click', function () {
        location.href = '/AssignMaterial/SaveAssignMaterial?id=0&workStudyId=' + $('#WorkStudyID').val()
    });


    //$('#StartDate')
    //    .on('change', function (e) {
    //        console.log('value is : ' + this.value);
    //});

    var form = $('#SaveWorkStudy');
    form.bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            WorkStudyID: {
                validators: {
                    notEmpty: {
                        message: 'WorkStudy ID is required.'
                    }
                }
            },
            StudyType: {
                validators: {
                    callback: {
                        message: 'Study Type is required.',
                        callback: function (value, validator, $field) {
                            /* Get the selected options */
                            var options = validator.getFieldElements('StudyType').val();
                            return (options !== '-1');
                        }
                    }
                }
            },
            StudyDesc: {
                validators: {
                    notEmpty: {
                        message: 'Study Title is required.'
                      
                        }
                    }                
            },
            Uncertainty: {
                validators: {
                    notEmpty: {
                        message: 'Uncertainty is required.'
                        }
                    }                
            },
            Plant: {
                validators: {
                    callback: {
                        message: 'Plant is required.',
                        callback: function (value, validator, $field) {
                            /* Get the selected options */
                            var options = validator.getFieldElements('Plant').val();
                            return (options !== null && options !== '-1');
                        }
                    }
                }
            },
            StudyStatus: {
                validators: {
                    callback: {
                        message: 'Study Status is required.',
                        callback: function (value, validator, $field) {
                            /* Get the selected options */
                            var options = validator.getFieldElements('StudyStatus').val();
                            return (options !== '-1');
                        }
                    }
                }
            },
            Experimentation: {
                excluded :false,
                validators: {
                    callback: {
                        message: 'Uncertainty is required.',
                        callback: function (value, validator, $field) {
                            //debugger;
                            var options = validator.getFieldElements('StudyStatus').val();   
                            var Experimentation = validator.getFieldElements('Experimentation').val();
                            if (options.trim() == "3"){
                                if ((Experimentation == null) || (Experimentation == ""))
                                    return false;
                                else
                                    return true;
                            }                                
                            else
                                return true;
                        }
                    }
                }
            },
            FinalSummary: {
                excluded: false,
                validators: {
                    callback: {
                        message: 'Final Summary is required.',
                        callback: function (value, validator, $field) {                           
                            var options = validator.getFieldElements('StudyStatus').val();
                            var FinalSummary = validator.getFieldElements('FinalSummary').val();
                            if (options.trim() == "3") {
                                if ((FinalSummary == null) || (FinalSummary == ""))
                                    return false;
                                else
                                    return true;
                            }
                            else
                                return true;
                        }
                    }
                }
            },
            //CompleteDate: {
            //    excluded: false,
            //    validators: {
            //        callback: {
            //            message: 'Complete Date is required.',
            //            callback: function (value, validator, $field) {
            //                debugger;
            //                var options = validator.getFieldElements('StudyStatus').val();
            //                var CompletedDate = validator.getFieldElements('CompleteDate').val();
            //                if (options.trim() == "3") {
            //                    if (CompletedDate == '')
            //                        return false;
            //                    else
            //                        return true;
            //                }                                
            //                else
            //                    return true;
            //            }
            //        }
            //    }
            //},
        }
    });
});


function Submit() {
    var isValid = true;
    if ($('#StartDate').val() === '') {
        isValid = false;
        alert('Start Date is required.');
    }
    var options = $('#StudyStatus').val();
   
    if (options.trim() == "3") {
        if ($('#CompleteDate').val() === '') {
            isValid = false;
            bootbox.alert('Complete Date is required.');            
        }
        else 
            isValid = true;
    }

    return isValid;
}

function AssignMaterial() {
    var isValid = true;
    if ($('#StartDate').val() === '') {
        isValid = false;
        alert('Start Date is required.');
    }    
    return isValid;
}