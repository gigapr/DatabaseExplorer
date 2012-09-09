function DisplayTablesList(result) {
    $("#errorMessageContainer").remove();
    if (result.responseText.indexOf("errorMessageContainer") < 0) { //success
        $("#connectionForm").hide("slow");
        $("#disconnectForm").show("slow");
        $('body').append(result.responseText);
        $(document).ready(function() {
            $("#tableListPanelTrigger").click(function() {
                $("#tableListPanel").toggle("fast");                
                $(this).toggleClass("active");
                return false;
            });
            $(".tableNameContainer").draggable({ revert: 'invalid' });
        });
        
        ClearConnectioForm();
    } else {
        $('body').append(result.responseText);        
    }
}

$(document).ready(function () {
    $("#connectionPanelTrigger").click(function () {
        $("#connectionPanel").toggle("fast");
        $(this).toggleClass("active");
        return false;
    });
});

function ClearConnectioForm() {
    $("#DataSource").val('');
    $("#DatabaseName").val('');
    $("#Username").val('');
    $("#Password").val('');
}

function Disconnect() {
    $("#tableListPanelTrigger").remove();
    $("#tableListPanel").remove();
    $("#tablesContainer").html('');
    $("#connectionForm").show("slow");
    $("#disconnectForm").hide("slow");
}