// JScript File

function DeshabilitarBoton(obj) {
    obj.disabled = true;
    obj.value = "Procesando";
}

function HabilitarBoton(obj) {
    obj.enable = true;
    obj.value = "Seleccionar";
}

function CheckKey() {
    var carCode = event.keyCode;
    if ((carCode < 48) || (carCode > 57)) {
        event.cancelBubble = true
        event.returnValue = false;
    }
}
