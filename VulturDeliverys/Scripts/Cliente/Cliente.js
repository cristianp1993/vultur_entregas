/// <summary>
/// Inserta un nuevo cliente en la base de datos
/// </summary>
/// <param name="model">La funcion envia el modelo del index al backend</param>
/// <returns>Descripción de lo que devuelve el método.</returns>
const miFormulario = document.getElementById("btnAddCliente")

if (miFormulario) {
    miFormulario.addEventListener("click", async function (e) {
        e.preventDefault(); // Prevenir el envío tradicional del formulario

        const model = {

            Nombre: document.getElementById("Nombre").value,
            Documento: parseInt(document.getElementById("Documento").value),
            Direccion: document.getElementById("Direccion").value,
            Telefono: document.getElementById("Telefono").value,
            Email: document.getElementById("Email").value
        };

        const formValid = await validateForm();


        if (formValid) {
            fetch(`/Cliente/Agregar`, {
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
                            text: 'Cliente creado con exito',
                            icon: 'success',
                            timer: 2000
                        }).then(() => {

                            window.location.href = '/Cliente/Index';
                        });

                    } else if (data.success == false && data.message == "Duplicado") {
                        Swal.fire({
                            title: 'No se puede crear!',
                            text: 'El cliente ya existe',
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
        }
    });
}

const btnActualizar = document.getElementById("btnActualizar")
if (btnActualizar) {
    btnActualizar.addEventListener("click", function (e) {
        e.preventDefault();


        const model = {
            ClienteID: parseInt(document.getElementById("ClienteID").value),
            Nombre: document.getElementById("Nombre").value,
            Documento: parseInt(document.getElementById("Documento").value),
            Direccion: document.getElementById("Direccion").value,
            Telefono: document.getElementById("Telefono").value,
            Email: document.getElementById("Email").value
        };

        fetch(`/Cliente/Actualizar`, {
            method: 'POST',
            body: JSON.stringify(model),
            headers: {
                'Content-Type': 'application/json',

            }
        })
            .then(response => response.json())
            .then(data => {
                if (data.success == true) {

                    Swal.fire({
                        title: 'Actualización terminada!',
                        text: 'Cliente Actualizado con exito',
                        icon: 'success',
                        timer: 2000
                    }).then(() => {

                        window.location.href = '/Cliente/Index';
                    });

                } else if (data.success == false && data.message == "Duplicado") {
                    Swal.fire({
                        title: 'Error!',
                        text: 'Hubo un problema al actualizar',
                        icon: 'error',
                        timer: 2000
                    })
                }
            })
            .catch(error => {
                console.error('Error:', error);
            });

    });
}
