const grabarEnvio = document.getElementById("btnCrearEnvio")

if (grabarEnvio) {
    grabarEnvio.addEventListener("click", async (e) => {
        e.preventDefault();

        const model = {
            EnvioID: document.getElementById('EnvioID').value,
            EmisorID: document.getElementById('EmisorID').value,
            ReceptorID: document.getElementById('ReceptorID').value,
            DireccionOrigen: document.getElementById('DireccionOrigen').value,
            DireccionDestino: document.getElementById('DireccionDestino').value,
            TelefonoContacto: document.getElementById('TelefonoContacto').value,
            DescripcionPaquete: document.getElementById('DescripcionPaquete').value,
            PesoPaquete: parseFloat(document.getElementById('PesoPaquete').value),
            ValorEnvio: parseFloat(document.getElementById('ValorEnvio').value),
            CiudadOrigenID: document.getElementById('CiudadOrigen').value,
            CiudadDestinoID: document.getElementById('CiudadDestino').value
        };

        const formValid = await validateForm();


        if (formValid) {


            fetch(`/Envio/Agregar`, {
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
                            text: 'Envio creado con exito',
                            icon: 'success',
                            timer: 2000
                        }).then(() => {

                            window.location.href = '/Envio/Index';
                        });
                        grabarEnvio.innerHTML = "Agregar Envío"
                        grabarEnvio.style.color = "#fff";
                        grabarEnvio.style.backgroundColor = "#286090";
                        grabarEnvio.style.borderColor = "#204d74";

                    } else if (data.success == false && data.message == "Duplicado") {
                        Swal.fire({
                            title: 'No se puede crear!',
                            text: 'El envio ya existe',
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


function cargarDatosEnvio(envioId) {
    fetch('/Envio/Editar?envioId=' + envioId)
        .then(response => response.json())
        .then(data => {
            
            document.getElementById('EnvioID').value = data.EnvioID;

            // Mapeo de otros campos
            document.getElementById('EmisorID').value = data.EmisorID;
            document.getElementById('CiudadOrigen').value = data.CiudadOrigenID;
            document.getElementById('DireccionOrigen').value = data.DireccionOrigen;
            document.getElementById('ReceptorID').value = data.ReceptorID;
            document.getElementById('CiudadDestino').value = data.CiudadDestinoID;
            document.getElementById('DireccionDestino').value = data.DireccionDestino;
            document.getElementById('TelefonoContacto').value = data.TelefonoContacto;
            document.getElementById('PesoPaquete').value = data.PesoPaquete;
            document.getElementById('ValorEnvio').value = data.ValorEnvio;
            document.getElementById('DescripcionPaquete').value = data.DescripcionPaquete;

         
            if (data.FechaEnvio) {
                // Extraer los milisegundos del formato /Date(...)
                const milisegundos = parseInt(data.FechaEnvio.match(/\/Date\((\d+)\)\//)[1]);
                const fechaEnvio = new Date(milisegundos);
                const formattedDate = fechaEnvio.toISOString().split('T')[0];
                document.getElementById('FechaEnvio').value = formattedDate;
            }


            grabarEnvio.innerHTML = "Editar Envío"
            grabarEnvio.style.color = "#E0E0E0";
            grabarEnvio.style.backgroundColor = "#009688";
            grabarEnvio.style.borderColor = "#00796B";

            document.getElementById('EmisorID').focus();
            document.getElementById('EmisorID').scrollIntoView({ behavior: 'smooth', block: 'center' });
        })
        .catch(error => console.error('Error:', error));
}
function eliminarEnvio(envioId) {

    fetch('/Envio/Eliminar', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ envioId: envioId })
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    Swal.fire({
                        title: 'Envío Eliminado',
                        text: 'Se eliminó correctamente',
                        icon: 'success',
                        timer: 3000
                    })
                    setTimeout(function () {
                        window.location.reload();
                    }, 2000);
                } else {
                    Swal.fire({
                        title: 'No se puede eliminar',
                        text: 'Tiene información asociada',
                        icon: 'error',
                        timer: 3000
                    })
                }
            })
            .catch(error => {
                console.error('Error:', error);
    });
    
}
