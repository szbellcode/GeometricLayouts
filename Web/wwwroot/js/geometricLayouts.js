var geometricLayouts = {
    gridSize: 6,
    gridCellSize: 100,
    gridLocatorStrokeWidth: 3,
    gridLocatorStrokeColour: '#ff0000',
    gridLocatorSize: 20,

    displayResult: function displayResult(text) {
        $("#result").text(text);
    },

    drawGridPosition: function drawGridPosition(cellX, cellY, isBottom) {
        var img = document.getElementById("gridImage");
        var cnvs = document.getElementById("gridCanvas");
        cnvs.style.position = "absolute";
        cnvs.style.left = img.offsetLeft + "px";
        cnvs.style.top = img.offsetTop + "px";
        cnvs.height = img.height;
        cnvs.width = img.width;

        var rootX = isBottom ? 30 : 70
        var rootY = isBottom ? 75 : 25;

        var x = (cellX - 1) * this.gridCellSize + rootX;
        var y = (this.gridSize - cellY) * this.gridCellSize + rootY;

        var ctx = cnvs.getContext("2d");
        ctx.beginPath();
        ctx.arc(x, y, this.gridLocatorSize, 0, 2 * Math.PI, false);
        ctx.lineWidth = this.gridLocatorStrokeWidth;
        ctx.strokeStyle = this.gridLocatorStrokeColour;
        ctx.stroke();
    },

    objectifyForm: function objectifyForm(formArray) {
        var returnArray = {};
        for (var i = 0; i < formArray.length; i++) {
            returnArray[formArray[i]['name']] = formArray[i]['value'];
        }
        return returnArray;
    }
}

$(function () {
    
    $("input[name=searchBy]").click(function () {
        var byNameSelected = $(this).val() == 'name';
        $("#coordinatesearch :input").attr("disabled", byNameSelected);
        $("#namesearch :input").attr("disabled", !byNameSelected);
        
        if (!byNameSelected)
            $("#namesearch :input").val("");
        else
            $("#coordinatesearch :input").val("");

        $("form[name='search']").validate().resetForm();
    });

    // form validation rules
    $("form[name='search']").validate({
        onkeyup: false, 
        rules: {
            name: {
                required: true,
                minlength: 2
            },
            vertex1: {
                required: true,
                coordinate: true
            },
            vertex2: {
                required: true,
                coordinate: true
            },
            vertex3: {
                required: true,
                coordinate: true
            },
        },
        messages: {
            name: {
                required: "Please enter a name to search for",
                minlength: "Please ensure the name contains at least 2 characters"
            },
            vertex1: {
                required: "Please enter coordiantes",
                coordinate: "Please use {x,y} format e.g. 0,0"
            },
            vertex2: {
                required: "Please enter coordiantes",
                coordinate: "Please use {x,y} format e.g. 0,10"
            },
            vertex3: {
                required: "Please enter coordiantes",
                coordinate: "Please use {x,y} format e.g. 10,0"
            }
        },

        submitHandler: function (form) {
            $.ajax({
                url: "/api/shapesearch",
                type: "post",
                data: JSON.stringify(geometricLayouts.objectifyForm($("#" + form.id).serializeArray())),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result, status, xhr) {
                    var position = result.isBottom ? "bottom" : "top";
                    var resultMessage = jQuery.validator.format("The triangle '{0}' matched your search and occurs at the following coordinates: ({1},{2}), ({3},{4}), ({5},{6}). This triangle is in the '{7}' position within grid cell [{8}, {9}]");
                    geometricLayouts.displayResult(resultMessage(result.name, result.vertex1.x, result.vertex1.y, result.vertex2.x, result.vertex2.y, result.vertex3.x, result.vertex3.y, position, result.parentCellX, result.parentCellY));
                    geometricLayouts.drawGridPosition(result.parentCellX, result.parentCellY, result.isBottom);
                },
                error: function (xhr, status, error) {
                    geometricLayouts.drawGridPosition(0, 0, false);
                    if (xhr.status == 404) {
                        geometricLayouts.displayResult("No triangle found. Please try again.");
                    }
                    else if (xhr.status == 400) {
                        geometricLayouts.displayResult("Your search was unsuccessful, please check your search criteria and try again.");
                    }
                    else {
                        geometricLayouts.displayResult("An unepected error occured. Please try again.");
                    }
                }
            });
        }
    });

    // regex to validate coord format
    $.validator.addMethod('coordinate', function (value) {
        return /^\d+,\d+$/.test(value);
    }, '');
});
