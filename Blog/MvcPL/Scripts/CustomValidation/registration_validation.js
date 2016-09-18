var isValid = true;

function checkRequiredInput(id) {
    var input = document.getElementById(id);

    if (!input.value) {
        isValid = false;
        var errorMessageContainer = document.getElementById(id.toLowerCase() + "-error-message-container");
        var message = input.getAttribute("data-val-required");
        errorMessageContainer.innerHTML += message + " ";
    }
}

function checkInputByRegex(id) {
    var input = document.getElementById(id);
    var regex = input.getAttribute("data-val-regex-pattern");

    if (!input.value.match(regex)) {
        isValid = false;
        var errorMessageContainer = document.getElementById(id.toLowerCase() + "-error-message-container");
        var message = errorMessageContainer.getAttribute("data-error-message");
        errorMessageContainer.innerHTML += message;
    }
}

function isMatchPasswords(password, confirmPassword) {
    if (password.length != confirmPassword.length) {
        isValid = false;
        var errorMessageContainer = document.getElementById("password-error-message-container");
        errorMessageContainer.innerHTML += "Passwords don't match!";
    }
}

function validateRegistrationForm() {
    isValid = true;
    checkRequiredInput("Nickname");
    checkInputByRegex("Nickname");
    checkRequiredInput("Email");
    checkInputByRegex("Email");
    checkRequiredInput("Password");
    checkRequiredInput("ConfirmPassword");
    checkRequiredInput("Captcha");
    isMatchPasswords(document.getElementById("Password").value, document.getElementById("ConfirmPassword").value);

    return isValid;
}