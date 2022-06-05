

$(document).ready(() => {
    $("#EMail").blur(() => {
        $.ajax({
            url: "/kunde/checkEMail",
            methode: "GET",
            data: { email: $("#EMail").val() }
        })
            .done((dataFromServer) => {
                if (dataFromServer === true) {
                    $("#eMailMessage").css("visibility", "visible");
                    $("#EMail").addClass("redBorder");

                } else {
                    $("#eMailMessage").css("visibility", "hidden");
                    $("#EMail").removeClass("redBorder");

                }
            })
            .fail(() => {
                alert("Server/URL nicht erreichbar")
            });
    });

});