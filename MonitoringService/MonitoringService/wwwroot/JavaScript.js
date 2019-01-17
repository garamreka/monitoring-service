$(document).ready(function () {

    window.baseUrl = "https://localhost:44350";

    CallBackendAndDisplayData();
});

function CallBackendAndDisplayData() {
    $.ajax({
        type: "GET",
        url: window.baseUrl + "/api",
        dataType: "json",
        success: function (data) {

            $("#requestSequenceId").text("Request sequence id: " + data.requestSequenceId);
            $("#phoneNumber").text("Phone: " + data.phoneNumber);

            if (data.isActive === false) {
                $("#isActive").text("Service inactive");
            }

            if (data.isActive === true) {
                $("#serviceLanguage").text("Language: " + data.serviceLanguage);
                $("#isActive").text("Service active until " + data.expiryDateAndTime);

                if (data.isXlServiceActive === false) {
                    $("#activeXlService").text("XL-service inactive");
                }

                if (data.isXlServiceActive === true) {

                    if (data.xlService.xlServiceLanguage === "Undefined") {
                        $("#activeXlService").text(
                            "XL-service active (" +
                            data.xlService.xlServiceActivationTime +
                            " - " +
                            data.xlService.xlServiceEndTime +
                            ")");
                    } else {
                        $("#activeXlService").text(
                            "XL-service active (" +
                            data.xlService.xlServiceActivationTime +
                            " - " +
                            data.xlService.xlServiceEndTime +
                            ") in " +
                            data.xlService.xlServiceLanguage +
                            " language");
                    }

                    if (data.xlService.isOverrideListInUse === true) {
                        $("#overrideList").text("Except for");

                        var tableContainer = document.getElementsByTagName('body')[0];
                        var table = document.createElement("table");
                        tableContainer.appendChild(table);

                        $("table").append("<tr><th class='headerColor'>Phone</th>" +
                            "<th class='headerColor'>Name</th></tr>");

                        $.each(data.xlService.contacts, function (i, contact) {
                            $("table").append(
                                "<tr><td>" +
                                contact.phoneNumber +
                                "</td><td>" +
                                contact.name +
                                "</td></tr>");
                        });
                    }
                }
            }
        }, error: function (request, status, errorThrown) {
            alert(request.responseText);
        }
    });
}