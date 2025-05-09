$(document).ready(function () {
    //MostrarAlerta();
});

//Id, class 

function MostrarAlerta() {
    alert('Hola');
}
$("#btnPrueba").click(function (e) {
    var txtNombre = $("#txtNombre").val(); //value
    alert(txtNombre);
});

function ValidarSoloLetras(event, LabelId) {

    var letra = event.key;
    var regularExpression = /^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\ ]+$/;

    if (regularExpression.test(letra)) {
        $("#" + LabelId).text('')
        return true;
    }
    else { //no es una letra
        $("#"+LabelId).text('Solo se permiten letras').css('color', 'red');
        return false;
    }
}

function ValidarSoloNumeros(event,LabelId) {

    var num= event.key;
    var regularExpression = /^[0-9]+$/;

    if (regularExpression.test(num)) {
        $("#" + LabelId).text('')
        return true;
    }
    else { //no es una letra
        $("#"+LabelId).text('Solo se permiten Numeros').css('color', 'red');
        //$("#lblErrorCelular").text('Solo se permiten Numeros').css('color', 'red');
        return false;
    }
}
function formato(input, LabelId) {
    var num = input.value.replace(/\D/g, '');
    if (num.length > 10) {
        num = num.substring(0, 10);
    }
    var format = '';
    if (num.length > 0) {
        format =num.substring(0,2);
    }
    if (num.length > 2) {
        format += '-' + num.substring(2, 6);
    }
    if (num.length > 6) {
        format += '-' + num.substring(6);
    }
    input.value = format;

    if (/^\d{2}-\d{4}-\d{4}$/.test(input.value)) {
        $("#"+LabelId).text('');
    }
}
function ValidarUsername(input) {
    //console.log(input.value)
    var username = input.value;
    var regex = /^[A-Z](?=.*\d)[a-zA-Z0-9]{7}$/;

    if (regex.test(username)) {
        $("#lblErrorUsername").text('');
        return true;
    } else {
        $("#lblErrorUsername").text('El username tiene que tener 8 caracteres exactos, La primera letra mayúscula y debe contener al menos un número').css('color', 'red');
        return false;
    }    
}
function ValidarEmail() {
    var correo = $("#txtEmail").val();
    var regex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

    if (regex.test(correo)) {
        $("#txtEmail").css('border-color', 'green'); 
        $("#txtEmail").css('border-width', '3px');
    }
    else {
        $("#lblErrorEmail").text('Ingrese un Email correcto').css('color', 'red');
        $("#txtEmail").css('border-color', 'red');
        $("#txtEmail").css('border-width', '3px');
        
    }
}

function ValidarConfirmacionEmail(LabelId) {
    var correo = $("#txtEmail").val();
    var correoConfirm = $("#txtEmailConfirm").val();
    
        if (correo == correoConfirm) {
            $("#txtEmail").css('border-color', 'green');
            $("#txtEmailConfirm").css('border-color', 'green');
            $("#" + LabelId).text('');
        } else {
            $("#txtEmail").css('border-color', 'red');
            $("#txtEmailConfirm").css('border-color', 'red');
            $("#" + LabelId).text('El Email no coincide').css('color', 'red');
        }      
}

function ValidarCurp(input,LabelId) {
    var curp = input.value;
    var regex = /^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0\d|1[0-2])(?:[0-2]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/;

    if (regex.test(curp)) {
        $("#" + LabelId).text('').css('color', 'green');
        //alert('curp valida')
    } else {
        $("#" + LabelId).text('Ingresa una curp correcta').css('color', 'red');
        //alert('curp Invalida')
    }
}

function ValidarPassword() {
    var password = $("#txtPassword").val();
    var regex = /^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$/;
    if (regex.test(password)) {

        $("#txtPassword").css('border-color', 'green');
        $("#lblErrorPassword").text('');
        //alert('contraseña valida')
    } else {
        $("#txtPassword").css('border-color', 'red');
        $("#lblErrorPassword").text('Lacontraseña tiene que tener 8 caracteres, La primera letra mayúscula y debe contener al menos un número').css('color', 'red');
        //alert('contraseña Invalidat
    }
}
document.getElementById("togglePassword").addEventListener("click", function () {
    const password = document.getElementById("txtPassword");
    //const icon = this.querySelector("i");
    const type = password.getAttribute("type") === "password" ? "text" : "password";
    password.setAttribute("type", type);

    this.classList.toggle("bi-eye");
    this.classList.toggle("bi-eye-slash");
});
function ValidarConfirmarPassword(LabelId) {
    var password = $("#txtPassword").val();
    var passwordConfirm = $("#txtPasswordConfirm").val();

    if (password == passwordConfirm) {
        $("#txtPassword").css('border-color', 'green');
        $("#txtPasswordConfirm").css('border-color', 'green');
        $("#" + LabelId).text('');
    } else {
        $("#txtPassword").css('border-color', 'red');
        $("#txtPasswordConfirm").css('border-color', 'red');
        $("#" + LabelId).text('Las contraseñas no coinciden').css('color', 'red');
    }
}
document.getElementById("togglePasswordConfirm").addEventListener("click", function () {
    const password = document.getElementById("txtPasswordConfirm");
    //const icon = this.querySelector("i");
    const type = password.getAttribute("type") === "password" ? "text" : "password";
    password.setAttribute("type", type);

    this.classList.toggle("bi-eye");
    this.classList.toggle("bi-eye-slash");
});

function validarForm() {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    const forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.from(forms).forEach(form => {
        form.addEventListener('submit', event => {
            if (!form.checkValidity()) {
                event.preventDefault()
                event.stopPropagation()
            }

            form.classList.add('was-validated')
        }, false)
    })
}
document.addEventListener('DOMContentLoaded', validarForm);




