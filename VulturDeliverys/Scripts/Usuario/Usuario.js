
/// <summary>
/// Verfica con el backend si el usuario existe
/// </summary>
/// <param name="param1">La funcion envia el modelo del index al backend</param>
/// <returns>Descripción de lo que devuelve el método.</returns>
document.getElementById("miFormulario").addEventListener("submit", function (e) {
    e.preventDefault(); // Prevenir el envío tradicional del formulario
   
    fetch(this.action, {
        method: 'POST',
        body: new FormData(this),
        headers: {
            'Accept': 'application/json' 
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
                    title: 'Bienvenido',
                    text: 'Inicio de sesión exitoso!',
                    icon: 'success',
                    timer: 2000
                }).then(() => {
                    
                    window.location.href = '/Home/Index';
                });
                
            } else {
                errorInicio()
            }

            
        })
        .catch(error => {
           
            errorInicio()
        });
});

function errorInicio() {
    Swal.fire({
        title: 'Error',
        text: 'Ocurrió un error en el inicio de sesion',
        icon: 'error',
        timer: 2000
    });
}