$(function () {
    generatePassword();

    $("#passwordLength").on("change", (e) => {
        $("#passwordLengthBadge").text(e.target.value);
        generatePassword();
    });

    $(".included-characters").click(() => {
        generatePassword();
    });

    function generatePassword() {
        $.ajax({
            type: "POST",
            url: "/RandomPassword/Index?handler=Generate",
            data: {
                "passwordLength": $("#passwordLength").val(),
                "includeCharacters": $("#chkIncludeCharacters").prop("checked"),
                "includeNumbers": $("#chkIncludeNumbers").prop("checked"),
                "includeSpecialCharacters": $("#chkIncludeSpecialCharacters").prop("checked")
            },
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            beforeSend: (xhr) => {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            headers: { "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: (response) => {
                $("#password").val(response.generatedPassword);
                $("#passwordStatus").text(response.status);

                var lastClass = $('#passwordStatus').attr('class').split(' ').pop();
                $("#passwordStatus").removeClass(lastClass);
                $("#passwordStatus").addClass(response.statusClass);
            },
            failure: (response) => { },
            error: (response) => { }
        });
    };
});