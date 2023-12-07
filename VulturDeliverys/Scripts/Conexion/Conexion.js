const grabar = document.getElementById("btnCrear")

if (grabar) {
    grabar.addEventListener("click", async (e) => {
        e.preventDefault();

        const model = {
            EnvioID: document.getElementById('inputEnvioID').value,
            ConexionID: document.getElementById('inputConexionID').value,
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
                        grabar.innerHTML = "Agregar Conexión"
                        grabar.style.color = "#fff";
                        grabar.style.backgroundColor = "#286090";
                        grabar.style.borderColor = "#204d74";
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

function cargarDatosParaEdicion(conexionId) {
    fetch('/Conexion/Editar?conexionId=' + conexionId)
        .then(response => {
            if (!response.ok) {
                throw new Error('Error al cargar los datos');
            }
            return response.json();
        })
        .then(data => {
            
            document.getElementById('inputEnvioID').value = data.EnvioID;
            document.getElementById('inputConexionID').value = data.ConexionID;
            document.getElementById('ciudadOrigenID').value = data.CiudadOrigenID;
            document.getElementById('ciudadDestinoID').value = data.CiudadDestinoID;
            document.getElementById('fechaSalida').value = formatDate(data.FechaSalida);
            document.getElementById('fechaLlegada').value = data.FechaLlegada ? formatDate(data.FechaLlegada) : '';
            grabar.innerHTML = "Editar Conexión"
            grabar.style.color = "#E0E0E0"; 
            grabar.style.backgroundColor = "#009688"; 
            grabar.style.borderColor = "#00796B";
        })
        .catch(error => console.error('Error:', error));
}

function formatDate(jsonDate) {
    if (!jsonDate) {
        return '';
    }

    // Extrae el número de milisegundos del formato /Date(...)
    const matches = jsonDate.match(/\/Date\((\d+)\)\//);
    if (!matches) {
        return '';
    }

    const timestamp = parseInt(matches[1], 10);
    const date = new Date(timestamp);

    let day = ('0' + date.getDate()).slice(-2);
    let month = ('0' + (date.getMonth() + 1)).slice(-2);
    let year = date.getFullYear();
    let hours = ('0' + date.getHours()).slice(-2);
    let minutes = ('0' + date.getMinutes()).slice(-2);

    return year + '-' + month + '-' + day + 'T' + hours + ':' + minutes;
}

