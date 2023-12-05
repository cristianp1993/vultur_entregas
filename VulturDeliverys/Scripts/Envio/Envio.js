﻿const grabarEnvio = document.getElementById("btnCrearEnvio")

if (grabarEnvio) {
    grabarEnvio.addEventListener("click", async (e) => {
        e.preventDefault();

        const model = {
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
