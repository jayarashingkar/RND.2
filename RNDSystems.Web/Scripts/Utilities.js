﻿var RND = RND || {};
//Constants Declared
RND.Constants = {
    AccessDenied: "Access denied.",
    AreYouDelete: "Are you sure want to delete it?"
};

RND.OptionConstant = {
    StudyType: "StudyType",
    Location: "Location"
};

RND.ResultConstant = {
    SCC: "SCC",
    EXCO: "Exco",
    MacroEtch: "Macro Etch",
    OpticalMount: "Optical Mount",
    Hardness: "Hardness",
    Fatigue: "Fatigue",
    IGC: "IGC"
};

function GetRootDirectory() {
    if (location.origin.indexOf('local') > -1)
        return "";
    else {
        var pathArr = location.pathname.substring(1, location.pathname.length).split('/');
        if (pathArr)
            return '/' + pathArr[0];
        else
            return "";
    }
}

function GetToken() {
    var token = $('#hdnToken').val();
    return token;
}