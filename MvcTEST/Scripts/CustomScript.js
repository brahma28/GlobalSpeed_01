function InitNumericValidation() {
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "number") {
            inputs[i].onkeydown = function () {
                return IsKeyNumber(this.event);
            }
        }
    }
}
function IsKeyNumber(evt) {
    evt = (evt) ? evt : ((window.event) ? event : null);
    if (evt.shiftKey) return false;
    var code = evt.which ? evt.which : evt.keyCode;
    if ((code >= 48 && code <= 57)
        || (code >= 96 && code <= 105)
        || code == 8
        || code == 9
        || code == 37
        || code == 39
        || code == 46
        || code == 110)
        return true;
    else return false;
}