function ValidarArchivo(input){
    let file = document.getElementById('inputFile');
    file.disabled = false;
    file.value = '';
    if (input.id == 'TXT') {
        file.setAttribute("onchange", "ValidarTxt()");
    } else {
        file.setAttribute("onchange", "ValidarExcel()");
    }

}
function ValidarTxt() {
    let file = document.getElementById('inputFile');
    let extension=GetNameFileExtension(file)
    if (extension != 'txt') {
        alert('El archivo cargado no es un txt')
        file.value = '';
    }
}
function ValidarExcel() {
    let file = document.getElementById('inputFile');
    let extension = GetNameFileExtension(file)
    if (extension != 'xlsx') {
        alert('El archivo cargado no es un Excel')
        file.value = '';
    }
}
function GetNameFileExtension(input){
    let arreglo = input.value.split('\\');
    let arr = arreglo[2].split('.');
    let nombreArchivo = arr[1]

    return nombreArchivo;
}

document.addEventListener('DOMContentLoaded', function () {
    const file = document.getElementById('inputFile');
    const validarBtn = document.getElementById('validar');

    file.addEventListener('change', function () {
        if (file.files.length > 0) {
            validarBtn.disabled = false;
        } else {
            validarBtn.disabled = true;
        }
    });
});

 
