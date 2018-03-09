$(document).ready(function () {

    if ($('#UserId').val() === '0') {
        $('#UserId').val('');
    }

    //$('#UserId').prop("readonly", true);
    
    if ($('#UserId').val()) {
        $('#UserName').prop("readonly", true);
        $("div[name='divpwd'").empty();
    }
    else {
        $('#Password').prop("readonly", false);
        $('#ConfirmPassword').prop("readonly", false);
    }
    
    $('#PermissionLevel').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
        
    $('#btnAdd').on('click', function () {
        location.href = '/Register/SaveRegisterUser/0'
    });


    //$('#StartDate')
    //    .on('change', function (e) {
    //        console.log('value is : ' + this.value);
    //});

    var form = $('#SaveRegisterUser');
    form.bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            UserName: {
                validators: {
                    notEmpty: {
                        message: 'User name is required.'
                    }
                }
            },
            FirstName: {
                validators: {
                    notEmpty: {
                        message: 'First name is required.'
                    }
                }
            },
            LastName: {
                validators: {
                    notEmpty: {
                        message: 'Last name is required.'
                    }
                }
            },
            PermissionLevel: {
                validators: {
                    callback: {
                        message: 'User permission is required.',
                        callback: function (value, validator, $field) {
                            /* Get the selected options */
                            var options = validator.getFieldElements('UserType').val();
                            return (options !== '-1');
                        }
                    }
                }
            },
            Password: {
                validators: {
                    identical: {
                        field: 'ConfirmPassword',
                        message: 'The Password and its confirm are not the same'
                    },
                    notEmpty: {
                        message: 'Password is required.'
                    }
                }
            },
            ConfirmPassword: {
                validators: {
                    identical: {
                        field: 'Password',
                        message: 'The Password and its confirm are not the same'
                    },
                    notEmpty: {
                        message: 'Confirm Password is required.'
                    }
                }
            },
        }
    });
});


//function Submit() {
//    var isValid = true;
//    //if ($('#MillLotNo').val() === '') {
//    //    isValid = false;
//    //    alert('Mill LotNo is required.');
//    //}
//    //else if ($('#CompleteDate').val() === '') {
//    //    isValid = false;
//    //    alert('Complete Date is required.');
//    //}
//    //else if ($('#DueDate').val() === '') {
//    //    isValid = false;
//    //    alert('Due Date is required.');
//    //}
//    return isValid;
//}