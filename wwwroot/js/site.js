

// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    var optiontype = "";
    var Choices = "";
    $("#cheBox").click(function () {
        optiontype = "checkbox";

        $("#radBox").hide();
    })
    $("#radBox").click(function () {

        optiontype = "radio";
        $("#cheBox").hide();
    })

    $("UniBut").css("padding", "25px");
    var i = 0;
    $("#answerBut").click(function () {
        if (optiontype == "radio") {
            var temp = $('input[name="Answer"]:checked').val();
            //console.log("x);
            console.log(temp);
            if (temp == null) {
                alert("Please select some Option");
            }
            else {
                temp = temp.slice(0, -1);
                console.log("Radio Button vaLue===>>" + temp);
                $("#choices").val("Radio###" + Choices);
                $("#ans").val(temp);
            }
        }
        else {

            var val = "";
            var glo = "";
            $(':checkbox:checked').each(function () {
                val = val + $(this).val();



            });

            if (val == "") {
                alert("Please select some Option");
            }
            else {

                val = val.replace(/\//g, "*");


                val = val.slice(0, -1);
                console.log(val);
                glo += val;
                $("#choices").val("Check###" + Choices);
                $("#ans").val(glo);
            }
        }





    })
    $("#UniBut").click(function () {
        var value = $("#temp").val();
        console.log(value);

        var ques = $("#Question").val();
        if (ques == "") {
            alert("Question Cannot be Empty");
        }
        else {
            if (value == "") {
                alert("option cannot be empty");
            }
            else {
                Choices += value + "*";
                var Id = "Answer_" + i;
                var Value = value;

                var option = '<input type= ' + optiontype + " " + 'name="Answer"' + 'id=' + Id + " " + 'value=' + Value + '/>' + value + '</br>';

                $(".inner").append(option);
                $("#temp").val("");
                i++;

            }
        }
    })
});