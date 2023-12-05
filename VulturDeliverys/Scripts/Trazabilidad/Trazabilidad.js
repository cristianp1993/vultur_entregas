const grabar = document.getElementById("btnAgregar")

if (grabar) {
    grabar.addEventListener("click", async (e) => {
        e.preventDefault();

        const model = {
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


