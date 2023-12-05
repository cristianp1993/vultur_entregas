const grabar = document.getElementById("btnCrear")

if (grabar) {
    grabar.addEventListener("click", async (e) => {
        e.preventDefault();

        const model = {
            EnvioID: document.getElementById('inputEnvioID').value,
            CiudadOrigenID: document.getElementById('ciudadOrigenID').value,
            CiudadDestinoID: document.getElementById('ciudadDestinoID').value,
            FechaSalida: document.getElementById('fechaSalida').value,
            FechaLlegada: document.getElementById('fechaLlegada').value
        };

        const formValid = await validateForm();


        if (formValid) {


            fetch(`/Conexion/Agregar`, {
                method: 'POST',
                body: JSON.stringify(model),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }

            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    return response.json(); // Procesa la respuesta como JSON
                })
                .then(data => {
                    
                    if (data.success == true) {

                        Swal.fire({
                            title: 'Muy bien!',
                            text: 'Conexión creada con exito',
                            icon: 'success',
                            timer: 2000
                        }).then(() => {
                            const envioID = document.getElementById("inputEnvioID").value
                            window.location.href = '/Conexion/Index?EnvioID=' + envioID;
                        });

                    } else if (data.success == false && data.message == "Duplicado") {
                        Swal.fire({
                            title: 'No se puede crear!',
                            text: 'Error al crear la conexión',
                            icon: 'warning',
                            timer: 3000
                        })
                    }
                    else {
                        errorInicio()
                    }

                })
                .catch(error => {

                    errorInicio()
                });
        } else {
            Swal.fire({
                title: 'Complete todos los campos',
                text: 'Hay campos obligatorios que no ha completado',
                icon: 'error',
                timer: 3000
            })
        }
    })
}


document.querySelectorAll('.btnEliminar').forEach(button => {
    button.addEventListener('click', function () {
        const conexionId = this.getAttribute('data-value');
        
        eliminarConexion(conexionId);
        
    });
});

function eliminarConexion(conexionId) {
    fetch('/Conexion/Eliminar', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ ConexionID: conexionId })
    }).then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json(); // Procesa la respuesta como JSON
    })
        .then(data => {

            if (data.success == true) {

                Swal.fire({
                    title: 'Hecho!',
                    text: 'Conexión Eliminada',
                    icon: 'success',
                    timer: 2000
                }).then(() => {
                    const envioID = document.getElementById("inputEnvioID").value
                    window.location.href = '/Conexion/Index?EnvioID=' + envioID;
                });

            }
            else {
                errorInicio()
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}