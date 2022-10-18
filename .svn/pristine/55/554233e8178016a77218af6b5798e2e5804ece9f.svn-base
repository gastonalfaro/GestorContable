// JScript File

function SoloNumeros() {
    if ((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105))
        return true;
    else
        return false;
}

function limit(field, n) {
    if (field.value.length >= n) {
        field.value = field.value.substring(0, n);
    }
}

function FocoVistaPrevia() {
    BtnCalcular.Focus = true;
}



function validarEmail(valor) {
    if (valor.value.length > 0) {
        if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(valor.value)) {
            return (true)
        }
        else {
            alert("El correo ingresado es incorrecto.");
            valor.value = "";
            valor.focus();
            valor.select();
            return (false);
        }
    }
}

function Quitar_Formato_Fecha(arg) {
    for (i = 0; i <= arg.value.length; i++)
        arg.value = arg.value.replace("/", "");
    return arg.value;
}

function Formato_Fecha(arg) {
    var idia, imes, iano
    var fecha = true;
    for (i = 0; i <= arg.value.length; i++)
        arg.value = arg.value.replace("/", "");

    if (arg.value.length > 0) {
        if (arg.value.length == 8) {
            idia = arg.value.substr(0, 2);
            imes = arg.value.substr(2, 2);
            iano = arg.value.substr(4, 4);
        }
        else {
            alert("El formato de la fecha debe ser ddmmaaaa");
            fecha = false;
            return arg.value = "";
        }

        if (iano < 1900) {
            alert("El ano debe ser superior a 1900");
            fecha = false;
            return arg.value = "";
        }

        if ((iano % 4 == 0) && (iano % 100 == 0) || (iano % 400 == 0)) {
            if (imes == 02)
                if ((idia < 1) || (idia > 29)) {
                    alert(" El dia esta fuera del rango permitido");
                    fecha = false;
                    return arg.value = "";
                }
        }
        else {
            if (imes == 02)
                if ((idia < 1) || (idia > 29)) {
                    alert(" El mes esta fuera del rango permitido");
                    fecha = false;
                    return arg.value = "";
                }
        }

        if ((imes == 01) || (imes == 03) || (imes == 05) || (imes == 07) || (imes == 08) || (imes == 10) || (imes == 12)) {
            if ((idia < 01) || (idia > 31)) {
                alert(" El dia esta fuera del rango permitido");
                fecha = false;
                return arg.value = "";
            }
        }
        else {
            if ((imes == 04) || (imes == 06) || (imes == 09) || (imes == 11)) {
                if ((idia < 01) || (idia > 30)) {
                    alert("El dia esta fuera del rango permitido");
                    fecha = false;
                    return arg.value = "";
                }
            }
            else {
                if (imes == 02) {
                    if ((idia < 1) || (idia > 29)) {
                        alert("El dia esta fuera del rango permitido");
                        fecha = false;
                        return arg.value = "";
                    }
                }
                else {
                    alert(" El dia esta fuera del rango permitido");
                    fecha = false;
                    return arg.value = "";
                }
            }
        }

        if (fecha)
            arg.value = idia + "/" + imes + "/" + iano;
    }
}

function Formato_Fecha2(arg) {
    var idia, imes, iano
    var fecha = true;

    for (i = 0; i <= arg.length; i++)
        arg = arg.replace("/", "");

    if (arg.length > 0) {
        if (arg.length == 8) {
            idia = arg.substr(0, 2);
            imes = arg.substr(2, 2);
            iano = arg.substr(4, 4);
        }
        else {
            alert("El formato de la fecha debe ser ddmmaaaa");
            fecha = false;
            return arg = "";
        }

        if (iano < 1900) {
            alert("El año debe ser superior a 1900");
            fecha = false;
            return arg = "";
        }

        if ((iano % 4 == 0) && ((iano * 100) % 100 == 0) || ((iano * 100) % 400 == 0)) {
            if (imes == 02)
                if ((idia < 1) || (idia > 29)) {
                    alert("El dia esta fuera del rango permitido");
                    fecha = false;
                    return arg = "";
                }
        }
        else {
            if (imes == 02)
                if ((idia < 1) || (idia > 28)) {
                    alert(" El dia esta fuera del rango permitido");
                    fecha = false;
                    return arg = "";
                }
        }

        if ((imes == 01) || (imes == 03) || (imes == 05) || (imes == 07) || (imes == 08) || (imes == 10) || (imes == 12)) {
            if ((idia < 01) || (idia > 31)) {
                alert("El dia esta fuera del rango permitido");
                fecha = false;
                return arg = "";
            }
        }
        else {
            if ((imes == 04) || (imes == 06) || (imes == 09) || (imes == 11)) {
                if ((idia < 01) || (idia > 30)) {
                    alert(" El dia esta fuera del rango permitido");
                    fecha = false;
                    return arg = "";
                }
            }
            else {
                if (imes == 02) {
                    if ((idia < 1) || (idia > 29)) {
                        alert(" El dia esta fuera del rango permitido");
                        fecha = false;
                        return arg = "";
                    }
                }
                else {
                    alert(" El dia esta fuera del rango permitido");
                    fecha = false;
                    return arg = "";
                }
            }
        }

        if (fecha) {
            arg = idia + "/" + imes + "/" + iano;
            return arg;
        }
    }
}

function mostrarAlerta(msg) {
    window.alert(msg);
}

function AceptarSoloNumerosMonto(evt) {
    //44 = ,
    //45 = -
    //46 = .
    //8 = Backspace
    //13 = Enter
    //9 = tab
    //11 = tab vertical
    //27 = escape
    //127 = delete
    evt = (evt) ? evt : event
    var key = (evt.which) ? evt.which : evt.keyCode;
    if (key > 47 && key < 58 || key == 8 || key == 46 || key == 44 || key == 13)
        return true;
    else
        return false;
}

//Solo permite numeros en el input
function AceptarSoloNumeros(evt) {
    //44 = ,
    //45 = -
    //46 = .
    //8 = Backspace
    //13 = Enter
    //9 = tab
    //11 = tab vertical
    //27 = escape
    //127 = delete
    // NOTE: Backspace = 8, Enter = 13, '0' = 48, '9' = 57  

    evt = (evt) ? evt : event
    var key = (evt.which) ? evt.which : evt.keyCode;
    if (key > 47 && key < 58 || key == 8 || key == 9 || key == 11 || key == 13 || key == 127 || key == 46)
        return true;
    else
        return false;
}
function Formateo_Monto(arg1) {
    var argtemp = "";

    // valida_numero(arg1);
    for (i = 0; i <= arg1.value.length; i++)
        arg1.value = arg1.value.replace(",", "");
    for (i = 0; i <= arg1.value.length; i++)
        if (arg1.value.substr(i, 1) == ".") {
            if ((i) == (arg1.value.length - 1))
                arg1.value = arg1.value + "00";
            if ((i + 1) == (arg1.value.length - 1))
                arg1.value = arg1.value + "0";
            if ((i + 2) != (arg1.value.length - 1))
                return alert("El Monto debe llevar dos decimales");
        }

    if (arg1.value.substr((arg1.value.length - 3), 1) != ".")
        arg1.value = arg1.value + ".00";

    argtemp = arg1.value.substr((arg1.value.length - 3), 3);
    ii = 0;

    for (i = (arg1.value.length - 4) ; i >= 0; i--) {
        argtemp = arg1.value.substr(i, 1) + argtemp;
        ii++;
        if ((ii == 3) && (i != 0)) {
            argtemp = "," + argtemp;
            ii = 0;
        }
    }
    return arg1.value = argtemp;
}

function Formateo_MontoCR(arg1) {
    var argtemp = "";

    // valida_numero(arg1);
    for (i = 0; i <= arg1.value.length; i++)
        arg1.value = arg1.value.replace(".", "");//arg1.value.replace(",", "");
    for (i = 0; i <= arg1.value.length; i++)
        if (arg1.value.substr(i, 1) == ",") {//if (arg1.value.substr(i, 1) == ".") {
            if ((i) == (arg1.value.length - 1))
                arg1.value = arg1.value + "00";
            if ((i + 1) == (arg1.value.length - 1))
                arg1.value = arg1.value + "0";
            if ((i + 2) != (arg1.value.length - 1))
                return alert("El Monto debe llevar dos decimales");
        }

    if (arg1.value.substr((arg1.value.length - 3), 1) != ",")//if (arg1.value.substr((arg1.value.length - 3), 1) != ".")
        arg1.value = arg1.value + ",00";//arg1.value = arg1.value + ".00";

    argtemp = arg1.value.substr((arg1.value.length - 3), 3);
    ii = 0;

    for (i = (arg1.value.length - 4) ; i >= 0; i--) {
        argtemp = arg1.value.substr(i, 1) + argtemp;
        ii++;
        if ((ii == 3) && (i != 0)) {
            argtemp = "." + argtemp;//argtemp = "," + argtemp;
            ii = 0;
        }
    }
    return arg1.value = argtemp;
}

function Formateo_Cuenta_Bancaria(arg1) {
    for (i = 0; i <= arg1.value.length; i++)
        arg1.value = arg1.value.replace("-", "");

    if (arg1.value.length == 15) {
        argtemp = arg1;
        argtemp = argtemp.value.substr(0, 3) + "-" +
                    argtemp.value.substr(3, 2) + "-" +
                    argtemp.value.substr(5, 3) + "-" +
                    argtemp.value.substr(8, 6) + "-" +
                    argtemp.value.substr(14, 1);

        arg1.value = argtemp;
        return (arg1.value);
    }
    else {
        if (arg1.value.length > 0) {
            alert("El formato debe ser ###-##-###-######-#");
            return arg1.value = ""; // Asigna un valor vacio al campo de la cuenta bancaria
            // return(arg1.value);  // Retornaría el mismo valor
        }
    }
}

function Formateo_Identificaciones(arg1, arg2) {
    for (i = 0; i <= arg1.value.length; i++)
        arg1.value = arg1.value.replace("-", "");

    if (arg2 == 1)
        if (arg1.value.length == 9) {
            argtemp = arg1;
            argtemp = argtemp.value.substr(0, 1) + "-" +
                        argtemp.value.substr(1, 4) + "-" +
                        argtemp.value.substr(5, 4);
            arg1.value = argtemp;
            return (arg1.value);
        }
        else {
            if (arg1.value.length > 0) {
                alert("El formato debe ser #-####-####");
                return (arg1.value);
            }
        }

    if (arg2 == 2 || arg2 == 9)
        if (arg1.value.length == 10) {
            argtemp = arg1;
            argtemp = argtemp.value.substr(0, 1) + "-" +
                        argtemp.value.substr(1, 3) + "-" +
                        argtemp.value.substr(4, 6);
            arg1.value = argtemp;
            return (arg1.value);
        }
        else {
            if (arg1.value.length > 0) {
                alert("El formato debe ser #-###-######");
                return (arg1.value);
            }
        }
}

function Formato_Periodo(arg) {
    var imes, iano
    var fecha = true;

    for (i = 0; i <= arg.value.length; i++)
        arg.value = arg.value.replace("/", "");

    if (arg.value.length > 0) {
        if (arg.value.length == 6) {
            imes = arg.value.substr(0, 2);
            iano = arg.value.substr(2, 4);
        }
        else {
            alert("El formato del periodo debe ser mmaaaa");
            fecha = false;
            return arg.value = "";
        }

        if (iano < 1900) {
            alert("El año debe ser superior a 1900");
            fecha = false;
            return arg.value = "";
        }

        if (imes < 1) {
            alert("El mes esta fuera del rango permitido");
            fecha = false;
            return arg.value = "";
        }

        if (imes > 12) {
            alert("El mes esta fuera del rango permitido");
            fecha = false;
            return arg.value = "";
        }

        if (fecha)
            arg.value = imes + "/" + iano;
    }
}

function EliminarEspacios(arg1) {
    var argTemp = "";
    argTemp = arg1.value;
    arg1.value = arg1.value.toUpperCase().replace(/^\s*|\s*$/g, "");
}

function Formatea_Telefono(arg1) {
    var argtemp = "";
    for (i = 0; i <= arg1.value.length; i++)
        arg1.value = arg1.value.replace("-", "");
    if (arg1.value.length == 8) {
        argtemp = arg1; argtemp = argtemp.value.substr(0, 4) + "-" + argtemp.value.substr(4, 8);
        arg1.value = argtemp;
        return (arg1.value);
    }
    else {
        if (arg1.value.length != 0) {
            arg1.value = "";
            alert("Valor incorrecto. Ej. 2212-0000");
        }
    }
}

function AceptarSoloTelefono(evt) {
    evt = (evt) ? evt : event
    var key = (evt.which) ? evt.which : evt.keyCode;

    if (key > 47 && key < 58 || key == 45)
        return true;
    else
        return false;
}

function AceptarSoloEnteros(evt) {
    evt = (evt) ? evt : event
    var key = (evt.which) ? evt.which : evt.keyCode;

    if (key > 47 && key < 58)
        return true;
    else
        return false;
}

function ConvierteMayuscula(arg1) {
    var argTemp = "";
    argTemp = arg1.value;
    arg1.value = arg1.value.toUpperCase();
}