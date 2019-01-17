// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
$(document).ready(() => {
    $('ul.nav li.dropdown').hover(function () {
        $(this).find('.dropdown-menu').stop(true, true).fadeIn(250);
    }, function () {
        $(this).find('.dropdown-menu').stop(true, true).fadeOut(250);
    });

    $("#planCancel").click((cancelButton) => {
        $(cancelButton.target).css("display", "none");
        $("#planSelector").html("Ativar seleção");
        $('#planSelector').attr("data-enabled", "false");

        $('#planList').children().each((index) => {
            let listItem = $("#planList").children()[index];
            let text = $(listItem).text();
            let id = $(listItem).attr("data-id");

            $(listItem).html(text);
        });
    });

    function subscribe(planId) {
        console.log(planId);
        $.ajax({
            type: "POST",
            url: "/Donations/Subscribe",
            data: {
                planId: planId
            }
        }).done((result) => {
            alert("Subscrição efetuada com sucesso");

            $("#planCancel").css("display", "none");
            $("#planSelector").html("Ativar seleção");
            $('#planSelector').attr("data-enabled", "false");

            $('#planList').children().each((index) => {
                let listItem = $("#planList").children()[index];
                let text = $(listItem).text();
                let id = $(listItem).attr("data-id");

                $(listItem).html(text);
            });
        });
    }

    $('#planSelector').click(function (plansButton) {
        let dataEnabled = (this.getAttribute("data-enabled") == "true");

        if (!dataEnabled) {
            this.setAttribute("data-enabled", "true");
            $(this).html("Subscrever");
            $("#planCancel").css("display", "");
            $('#planList').children().each((index) => {
                let listItem = $("#planList").children()[index];
                let text = $(listItem).text();
                let id = $(listItem).attr("data-id");

                $(listItem).html(
                    '<input class="plans" name="plans" type="radio" value="' + id + '"> ' + text + '</input>'
                );
            });

            $(".plans").last().attr("checked", "checked");
        } else {
            let planId = $(".plans:checked").val();
            let card = null;
            $.ajax({
                type: "GET",
                url: "/Users/Card",
                async: false
            }).done((result) => {
                    card = result;
                });

            if (card == null) {
                $("#cardModal").modal();
                $(".cancel").click((index, elem) => {
                    $("#cardModal").modal("hide");
                });

                $(".add-card").click((index, elem) => {
                    $.ajax({
                        type: "POST",
                        url: "/Users/Card",
                        async: false,
                        data: {
                            number: $("#cardNumber").val(),
                            month: $("#cardMonth").val(),
                            year: $("#cardYear").val(),
                            cvc: $("#cardCvc").val()
                        }
                    }).done((result) => {
                        console.log(result);
                        if (result == "true") {
                            $("#cardModal").modal("hide");
                            subscribe(planId);
                        } else {
                            alert("Dados do cartão incorretos!");
                        }
                    });
                });
            } else {
                let planId = $(".plans:checked").val();
                subscribe(planId);
            }

        }
    });
});

//// Write your JavaScript code.
////Tabs JS - W3School
//function openTab(evt, tabName) {
//    // Declare all variables
//    var i, tabcontent, tablinks;

//    // Get all elements with class="tabcontent" and hide them
//    tabcontent = document.getElementsByClassName("tabcontent");
//    for (i = 0; i < tabcontent.length; i++) {
//        tabcontent[i].style.display = "none";
//    }

//    // Get all elements with class="tablinks" and remove the class "active"
//    tablinks = document.getElementsByClassName("tablinks");
//    for (i = 0; i < tablinks.length; i++) {
//        tablinks[i].className = tablinks[i].className.replace(" active", "");
//    }

//    // Show the current tab, and add an "active" class to the button that opened the tab
//    document.getElementById(tabName).style.display = "block";
//    evt.currentTarget.className += " active";
//} 
//document.getElementById("defaultOpen").click();
