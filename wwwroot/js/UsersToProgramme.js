$(function () {

    var errorText = "An error has occurred. An administrator has been notified. Please try again later";

    $("button[name='SaveSelectedUsers']").prop('disabled', true);

    $('select').on('change', function () {

        var url = "/Admin/UsersToProgramme/GetUsersForProgramme?programmeId=" + this.value;

        if (this.value != 0) {
            $.ajax(
                {
                    type: "GET",
                    url: url,
                    success: function (data) {
                        $("#UsersCheckList").html(data);
                        $("button[name='SaveSelectedUsers']").prop('disabled', false);
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        PresentClosableBootstrapAlert("#alert_placeholder", "danger", "An error occurred!", errorText);
                        console.error("An error has occurred: " + thrownError + "Status: " + xhr.status + "\r\n" + xhr.responseText);
                    }
                }
            );

        }
        else {
            $("button[name='SaveSelectedUsers']").prop('disabled', true);
            $("input[type=checkbox]").prop("checked", false);
            $("input[type=checkbox]").prop("disabled", true);
        }

    });

    $('#SaveSelectedUsers').click(function () {

        var url = "/Admin/UsersToProgramme/SaveSelectedUsers";

        var programmeId = $("#ProgrammeId").val();

        var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

        var usersSelected = [];

        DisableControls(true);

        $(".progress").show("fade");


        $('input[type=checkbox]:checked').each(function () {
            var userModel = {
                Id: $(this).attr("value")
            };
            usersSelected.push(userModel);
        });

        var usersSelectedForProgramme = {
            __RequestVerificationToken: antiForgeryToken,
            ProgrammeId: programmeId,
            UsersSelected: usersSelected
        };
        $.ajax(
            {
                type: "POST",
                url: url,
                data: usersSelectedForProgramme,
                success: function (data) {
                    $("#UsersCheckList").html(data);

                    $(".progress").hide("fade", function () {
                        $(".alert-success").fadeTo(2000, 500).slideUp(500, function () {
                            DisableControls(false);

                        });

                    });

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    $(".progress").hide("fade", function () {
                        PresentClosableBootstrapAlert("#alert_placeholder", "danger", "An error occurred!", errorText);
                        console.error("An error has occurred: " + thrownError + "Status: " + xhr.status + "\r\n" + xhr.responseText);

                        DisableControls(false);
                    });
                }
            }
        );

        function DisableControls(disable) {
            $('input[type=checkbox]').prop("disabled", disable);
            $("#SaveSelectedUsers").prop('disabled', disable);
            $('select').prop('disabled', disable);
        }

    });
});