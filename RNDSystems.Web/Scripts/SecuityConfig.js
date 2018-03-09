/*
// comment here
Registered the new user, When the registered user first time login,
It will asked for the secret questions with answers. User needs to select the question
and they need to provide the appropriate answer. These details will be saved in to database.
Whether the RNDRegistered users forget the password, It will help to reset or retrieve the password.
*/

//function CheckSecuityConfig(IsSecurityApplied) {
//function CheckSecuityConfig(IsSecurityApplied) {
function CheckSecuityConfig() {  
    var IsSecurityApplied = $('#IsSecurityApplied').val();

    if (IsSecurityApplied === "False") {      
        var div = $('#popup').html();
        dialog = bootbox.dialog({
            message: div,
            //size: 'large',
            buttons: {
                cancel: {
                    label: '<i class="fa fa-times"></i> Cancel'
                },
                confirm: {
                    label: '<i class="fa fa-check"></i> Confirm',
                    callback: function (result) {
                        var form = dialog.find('#entityform');
                        form.bootstrapValidator({
                            message: 'This value is not valid',
                            feedbackIcons: {
                                valid: 'glyphicon glyphicon-ok',
                                invalid: 'glyphicon glyphicon-remove',
                                validating: 'glyphicon glyphicon-refresh'
                            },
                            fields: {
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
                                SecurityAnswer: {
                                    validators: {
                                        notEmpty: {
                                            message: 'Security Answer is required.'
                                        }
                                    }
                                },
                                LMSSecurityQuestionId: {
                                    validators: {
                                        callback: {
                                            message: 'Security Question is required.',
                                            callback: function (value, validator, $field) {
                                                /* Get the selected options */
                                                var options = validator.getFieldElements('RNDSecurityQuestionId').val();
                                                return (options !== '-1');
                                            }
                                        }
                                    }
                                },
                            }
                        });
                        var validator = form.data('bootstrapValidator');
                        validator.validate();

                        if (validator.isValid()) {
                            var Password = dialog.find('#Password').val();
                            var ConfirmPassword = dialog.find('#ConfirmPassword').val();
                            var RNDSecurityQuestionId = dialog.find('#RNDSecurityQuestionId').val();
                            var SecurityAnswer = dialog.find('#SecurityAnswer').val();
                            var HasQuestionId = $('#HasQuestionId').val();

                            var model = {
                                Password: Password,
                                ConfirmPassword: ConfirmPassword,
                                HasQuestionId: HasQuestionId,
                                RNDSecurityQuestionId: RNDSecurityQuestionId,
                                SecurityAnswer: SecurityAnswer,
                            };
                            $.ajax({
                                type: 'post',
                                url: GetRootDirectory() + '/admin/SecuityConfig',
                                data: model
                            })
                            .done(function (data) {
                                if (data && data.isSuccess) {
                                    dialog.modal('hide');
                                    location.href = GetRootDirectory() + '/WorkStudy/WorkSutdyList';
                                }
                                else {
                                    dialog.modal('hide');
                                    bootbox.alert(data.message);
                                }
                            })
                            .fail(function (x, y, x) {
                                console.log("error from security config");
                            });
                        }
                        else
                            return false;
                    }
                }
            },
            onEscape: function () {
                this.modal('hide');
            }
        });
        dialog.find('#RNDSecurityQuestionId').selectpicker({ width: '27%' });
    }
    else
        location.href = GetRootDirectory() + '/WorkStudy/WorkSutdyList';
}



$(document).ready(function () {
    $('#RNDSecurityQuestionId').attr({ 'data-live-search': 'true', 'data-width': '100%' }).selectpicker();
    var IsSecurityApplied = $('#IsSecurityApplied').val();
    //var qid = '@ViewBag.HasQuestionId';
    //if (qid === "True") {
    //    $('div[name=qexists]').hide();
    //    $('#HasQuestionId').val(true);
    //}
    //else
    //    $('#HasQuestionId').val(false);

    var form = $('#SaveSecurity');
    form.bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            RNDSecurityQuestionId: {
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
            SecurityAnswer: {
                validators: {
                    notEmpty: {
                        message: 'Security Answer is required.'
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
    $('#btnSavePassword').on('click', function () {
        CheckSecuityConfig();
    });
   // CheckSecuityConfig(IsSecurityApplied);
});