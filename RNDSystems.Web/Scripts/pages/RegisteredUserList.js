var columns = [

    //{
    //    label: 'User Id',
    //    property: 'UserId',
    //    sortable: true,
    //    width: '20px'
    //},
    {
        label: 'User Name',
        property: 'UserName',
        sortable: true,
        width: '15px'
    },
    {
        label: 'First Name',
        property: 'FirstName',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Last Name',
        property: 'LastName',
        sortable: true,
        width: '25px'
    },
    {
        label: 'Permission Level',
        property: 'PermissionLevel',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Created By',
        property: 'Created_By',
        sortable: true,
        width: '15px'
    },
    {
        label: 'Created On',
        property: 'Created_On',
        sortable: true,
        width: '15px'
    },
    //{
    //    label: 'Status Code',
    //    property: 'StatusCode',
    //    sortable: true,
    //    width: '15px'
    //},
    {
        label: 'Edit',
        property: 'Edit',
        width: '10px'
    },
    {
        label: 'Delete',
        property: 'Delete',
        width: '10px'
    }
];

function customColumnRenderer(helpers, callback) {    
    // determine what column is being rendered
    var column = helpers.columnAttr;

    // get all the data for the entire row
    var rowData = helpers.rowData;
    var customMarkup = '';

    // only override the output for specific columns.
    // will default to output the text value of the row item
    switch (column) {
        case 'UserId':
            // let's combine name and description into a single column
            customMarkup = '<div style="font-size:12px;">' + rowData.UserId + '</div>';
            break;
        case 'EntryDate':
            // let's combine name and description into a single column
            customMarkup = rowData.StrEntryDate;
            break;
        case 'Edit':
            // let's combine name and description into a single column
            customMarkup = '<button onclick="GridEditClicked(' + rowData.UserId + ')" id="gridEdit" name="gridEdit" class="btn btn-info btn-sm center-block"><i class="fa fa-pencil"></i></button>';
            break;
        case 'Delete':
            // let's combine name and description into a single column
            customMarkup = '<button onclick="GridDeleteClicked(' + rowData.UserId + ')" id="gridDelete" name="gridDelete" class="btn btn-danger btn-sm center-block"><i class="fa fa-trash"></i></button>';
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
    item.attr('id', 'row' + helpers.rowData.UserId);
    callback();
}

// this example uses an API to fetch its datasource.
// the API handles filtering, sorting, searching, etc.
function customDataSource(options, callback) {
    // set options    
    var pageIndex = options.pageIndex;
    var pageSize = options.pageSize;
    var search = '';
    if ($('#searchUserName').val())
        search += ';' + 'UserName:' + $('#searchUserName').val();
    if ($('#searchFirstName').val())
        search += ';' + 'FirstName:' + $('#searchFirstName').val();
    if ($('#searchLastName').val())
        search += ';' + 'LastName:' + $('#searchLastName').val();
    if ($('#PermissionLevel').val() && $('#PermissionLevel').val() !== '-1')
        search += ';' + 'PermissionLevel:' + $('#PermissionLevel').val();

    var options = {
        Screen: 'RegisteredUser',
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

function GridEditClicked(id) {    
    location.href = '/Register/SaveRegisterUser/' + id;
}

/*
function AssignMaterial(ele) {
    var recId = $(ele).attr('data-RecId');
    var workStudyID = $(ele).attr('data-WorkStudyID');
    location.href = '/AssignMaterial/AssignMaterialList?recId=' + recId + '&workStudyID=' + workStudyID;
}
*/

function GridDeleteClicked(id) {
    bootbox.confirm({
        message: RND.Constants.AreYouDelete,
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
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: Api + "api/Register/" + id,
                    headers: {
                        Token: GetToken()
                    },
                    type: 'DELETE',
                    //data: JSON.stringify(ApiViewModel),
                    contentType: "application/json;charset=utf-8",                    
                })
                 .done(function (data) {
                     $('#UserRegisterRepeater').repeater('render');
                 });
            }           
        }
    }); 
}

$('#btnSearch').on('click', function () {
    $('#UserRegisterRepeater').repeater('render');
});

$('#btnClear').on('click', function () {
    $('#searchUserName').val('');
    $('#searchFirstName').val('');
    $('#searchLastName').val('');
    $('#UserPermissionLevel').val('');
    $('#PermissionLevel').selectpicker('val', "-1");
    $('#UserRegisterRepeater').repeater('render');
});

$(document).ready(function () {

    $(".repeater-header").remove();
    $('#PermissionLevel').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    /*
    if ($('#WorkStudyID').val() !== '0') {
        $('#searchWorkStudyNumber').prop("readonly", true);
    }    
    $('#AlloyTypes').attr('data-live-search', 'true');
    $('#AlloyTypes').selectpicker();
    $('#TemperTypes').attr('data-live-search', 'true');
    $('#TemperTypes').selectpicker();
    */
    $('#btnAdd').on('click', function () {
        //location.href = '/AssignMaterial/SaveAssignMaterial?id=0&workStudyId=' + $('#UserId').val();
        location.href = '/Register/SaveRegisterUser?id=0';
    });

    //$("#searchUserName").focusout(function () {
    //    if ($('#searchUserName').val())
    //    {
    //        alert("User Name");
    //    }    
    //});

    //$("#searchFirstName").focusout(function () {
    //    if ($('#searchFirstName').val()) {
    //        alert("First Name");
    //    }
    //});
});