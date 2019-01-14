$(document).ready(function () {

    window.baseUrl = "https://localhost:44350";

        $.get(window.baseUrl + "/api", function(data) {
            console.log("test");
            $("#requestSequenceId").text("Request sequence id: " + data.requestSequenceId);
            $("#phoneNumber").text("Phone: " + data.phoneNumber);

            if (data.isActive === false) {
                $("#isActive").text("Service inactive");
            }

            if (data.isActive === true) {
                $("#serviceLanguage").text(data.serviceLanguage);
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
                            "XL-service active (" + data.xlService.xlServiceActivationTime
                            + " - " + data.xlService.xlServiceEndTime + ") in "
                            + data.xlService.xlServiceLanguage + " language");
                    }

                    if (data.xlService.isOverrideListInUse === true) {
                        $("#contact").text("Except for");

                        var contacts = data.xlService.Contacts;
                        contacts.forEach();
                    }
                }
            }
        }, "json");
});