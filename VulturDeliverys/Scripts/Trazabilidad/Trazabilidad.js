const grabar = document.getElementById("btnAgregar")

if (grabar) {
    grabar.addEventListener("click", async (e) => {
        e.preventDefault();

        const model = {
            TrazabilidadID: document.getElementById('TrazabilidadID').value,
            EnvioID: document.getElementById('EnvioID').value,
            FechaHora: document.getElementById('FechaHora').value,
            Ubicacion: document.getElementById('Ubicacion').value,
            Estado: document.getElementById('Estado').value,
            DetallesAdicionales: document.getElementById('DetallesAdicionales').value
        };

        const formValid = await validateForm();


        if (formValid) {


            fetch(`/Trazabilidad/Agregar`, {
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
                    //Si el usuario existe le damos la bienvenida y lo redirigimos a la pagian principal
                    if (data.success == true) {

                        Swal.fire({
                            title: 'Muy bien!',
                            text: 'Trazabilidad creada con exito',
                            icon: 'success',
                            timer: 2000
                        }).then(() => {

                            window.location.href = '/Trazabilidad/Index';
                            document.getElementById("TrazabilidadID").disabled = false;
                        });

                    } else if (data.success == false && data.message == "Duplicado") {
                        Swal.fire({
                            title: 'No se puede crear!',
                            text: 'La Trazabilidad ya existe',
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


function editarTrazabilidad(trazabilidadId) {
    fetch('/Trazabilidad/ObtenerDetalles?trazabilidadId=' + trazabilidadId)
        .then(response => {
            if (!response.ok) {
                throw new Error('Respuesta del servidor no es OK');
            }
            return response.json();
        })
        .then(data => {

            if (Object.keys(data).length > 0) {
                // Mapeo de los datos a los campos del formulario
                document.getElementById('TrazabilidadID').value = data.TrazabilidadID;
                document.getElementById('EnvioID').value = data.EnvioID;
                document.getElementById('Ubicacion').value = data.Ubicacion;
                document.getElementById('Estado').value = data.Estado;
                document.getElementById('DetallesAdicionales').value = data.DetallesAdicionales;

                // Formatear y establecer la fecha y hora
                if (data.FechaHora) {
                    const milisegundos = parseInt(data.FechaHora.match(/\/Date\((\d+)\)\//)[1]);
                    const fechaEnvio = new Date(milisegundos);
                    const formattedDate = fechaEnvio.toISOString().split('T')[0];
                    document.getElementById('FechaHora').value = formattedDate;
                }

                // Cambiar el texto del botón para indicar que es una edición
                grabar.innerHTML = "Editar Trazabilidad"
                grabar.style.color = "#E0E0E0";
                grabar.style.backgroundColor = "#009688";
                grabar.style.borderColor = "#00796B";

                document.getElementById("TrazabilidadID").disabled = true;
                document.getElementById('Estado').focus();
                document.getElementById('Estado').scrollIntoView({ behavior: 'smooth', block: 'center' });
            } else {
                Swal.fire({
                    title: 'Verifique el codigo de guia',
                    text: 'La Guia buscada no existe, por favor verifique los datos',
                    icon: 'error',
                    timer: 3000
                })
            }
        })
        .catch(error => {
            console.error('Error al obtener los detalles de la trazabilidad:', error);
            
        });
}


function eliminarTrazabilidad(trazabilidadId) {
    
        fetch('/Trazabilidad/Eliminar', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'X-CSRF-TOKEN': document.querySelector('[name=__RequestVerificationToken]').value
            },
            body: JSON.stringify({ trazabilidadId: trazabilidadId })
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Error al eliminar la trazabilidad');
                }
                return response.json();
            })
            .then(data => {
                if (data.success) {
                    Swal.fire({
                        title: 'Muy bien!',
                        text: 'Trazabilidad eliminada con exito',
                        icon: 'success',
                        timer: 2000
                    }).then(() => {

                        window.location.href = '/Trazabilidad/Index';
                        
                    });
                } else {
                    Swal.fire({
                        title: 'No se puede Eliminar!',
                        text: 'Error al borrar la trazabilidad',
                        icon: 'error',
                        timer: 3000
                    })
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Error al eliminar la trazabilidad.');
            });
    
}
