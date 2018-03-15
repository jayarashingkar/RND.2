$(document).ready(function () {

   // $('#CustomOption').hide();
    $('#LocationOption').hide();
    $('#StudyTypeOptionOption').hide();

    $("#btnSelectOption").click(function () {
        var optionType = $("input:radio[name='OptionRadio']:checked").val();

        //location.href = '/Options/StudyTypeOptionView?id=0&optionType=' + optionType;

        if (optionType == RND.OptionConstant.StudyType)
        {
            debugger;
            $('#LocationOption').hide();
            $('#StudyTypeOptionOption').show();

          //  location.href = '/Options/StudyTypeOptionView?id=0&optionType=' + optionType;
            // $('#CustomOption').load('@Url.Action("StudyTypeOptionView")');
          
        }
        else if (optionType == RND.OptionConstant.Location)
        {
            debugger;
            $('#LocationOption').show();
            $('#StudyTypeOptionOption').hide();         
        }
    });
});

