function ActualizarEstatus(idUsuario, input) {
    var estatus = input.checked ;
    $.ajax({
        url: UrlEstatus,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        data: {
            IdUsuario: idUsuario,
            Estatus: estatus
        },
        success: function (result) {
            if (!result.Correct) {
                input.checked = !input.checked;
            }          
        },
        error: function () {
            alert('Error al actualizar el estatus');
        }
    });

}