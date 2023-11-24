const grabarEnvio = document.getElementById("btnCrearEnvio")

if (grabarEnvio) {
    grabarEnvio.addEventListener("click", () => {
        e.preventDefault();

        const model = {

            Nombre: document.getElementById("Nombre").value,
            Documento: parseInt(document.getElementById("Documento").value),
            Direccion: document.getElementById("Direccion").value,
            Telefono: document.getElementById("Telefono").value,
            Email: document.getElementById("Email").value
        };

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
    })
}


function errorInicio() {
    Swal.fire({
        title: 'Error',
        text: 'Ocurrió un error al intentar crear el envio',
        icon: 'error',
        timer: 2000
    });
}