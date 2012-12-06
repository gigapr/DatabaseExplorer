jsPlumb.bind("ready", function () {

    // chrome fix.
    document.onselectstart = function () { return false; };

    // render mode
    var resetRenderMode = function (desiredMode) {
        var newMode = jsPlumb.setRenderMode(desiredMode);
        $(".rmode").removeClass("selected");
        $(".rmode[mode='" + newMode + "']").addClass("selected");
        $(".rmode[mode='canvas']").attr("disabled", !jsPlumb.isCanvasAvailable());
        $(".rmode[mode='svg']").attr("disabled", !jsPlumb.isSVGAvailable());
        $(".rmode[mode='vml']").attr("disabled", !jsPlumb.isVMLAvailable());
    };

    $(".rmode").bind("click", function () {
        var desiredMode = $(this).attr("mode");
        if (window.jsPlumbDemo.reset) window.jsPlumbDemo.reset();
        jsPlumb.reset();
        resetRenderMode(desiredMode);
    });

    $("#explanation,.renderMode").draggable();
    resetRenderMode(jsPlumb.SVG);
});

function ConnectionExsist(from, to) {
    var hasConnection = false;
    var connection = jsPlumb.getConnections({ source: from, target: to });
    if (connection.length > 0) {
        hasConnection = true;
    }
    return hasConnection;
}

function AddConnection(from, to) {

    jsPlumb.draggable(jsPlumb.getSelector(".table"));

    if ($("#" + from).length == 0 || $("#" + to).length == 0) {
        return;
    }

    if (!ConnectionExsist( from, to)) {
        jsPlumb.draggable(jsPlumb.getSelector(".table"));

        jsPlumb.importDefaults({
            DragOptions: { cursor: "pointer", zIndex: 2000 },
            HoverClass: "connector-hover"
        });

        var stateMachineConnector = {
            connector: "StateMachine",
            paintStyle: { lineWidth: 3, strokeStyle: "#056" },
            endpoint: "Blank",
            anchor: "Continuous",
            overlays: [["PlainArrow", { location: 1, width: 10, length: 12}]]
        };

        jsPlumb.connect({
            source: from,
            target: to
        }, stateMachineConnector);
    }
}

$(function () {    
    $("#tablesContainer").droppable({
        tolerance: 'fit',
        drop: function (event, ui) {
            var element = ui.draggable.attr('id');
            if (element.indexOf("tableNameContainer_") < 0) {
                return false;
            }
            var tableName = element.replace("tableNameContainer_", "").replace(/_/g, " ");
            $.ajax({
                url: '/DatabaseExplorer/DisplayTable',
                type: 'html',
                data: { tableName: tableName },
                success: function (result) {
                    removeFromTableList(element);
                    $("#tablesContainer").append(result);
                    AddConnections();
                },
                error: function (e) {
                    alert(e.responseText);
                }
            });
            return false;
        }
    });
});

function AddConnections() {
    $.ajax({
        url: '/DatabaseExplorer/GetForeignKeyConnections',
        type: 'html',
        success: function (result) {
            for (var i = 0; i < result.length; i++) {
                AddConnection(result[i].ForeignKeyTableName.replace(/\s/g, "_"), result[i].PrimaryKeyTableName.replace(/\s/g, "_"));
            }
        },
        error: function (e) {
            alert(e.responseText);
        }
    });
    return false;
}

function removeFromTableList(element) {
    $("#" + element).remove();
}

function restoreToTableList(tableName) {
    var tableNameWithSpaces = tableName.replace("tableNameContainer_", "").replace(/_/g, " ");    
    var tableNameWithUnderscore = "tableNameContainer_" + tableName;
    
    $('#tableListContainer').append(
            "<div id='" + tableNameWithUnderscore + "' class='tableNameContainer'>" +
                    tableNameWithSpaces +
                    "<br/>" +
            "</div>"
        );
    $(".tableNameContainer").draggable({ revert: 'invalid' });
    jsPlumb.detachAllConnections(tableName.replace(/\s/g, "_"));
    $("#" + tableName).remove();
}
