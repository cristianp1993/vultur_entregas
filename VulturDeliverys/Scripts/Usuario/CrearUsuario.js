document.addEventListener("DOMContentLoaded", function () {
    const nombreUsuario = document.getElementById('nombreUsuario');
    const contrasena = document.getElementById('contrasena');
    const email = document.getElementById('email');
    const rol = document.getElementById('rol');
    const btnCrear = document.getElementById('btnCrear');

    function validarFormulario() {
        const nombreUsuarioValido = nombreUsuario.value.length >= 4;
        const contrasenaValida = contrasena.value.length >= 6 && /[!@#$%^&*(),.?":{}|<>]/.test(contrasena.value);
        const emailValido = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email.value);
        const rolSeleccionado = rol.value !== '' && rol.value !== 'Seleccione un Rol';

        // Actualizar estilos según la validez
        nombreUsuario.style.borderColor = nombreUsuarioValido ? 'green' : 'red';
        contrasena.style.borderColor = contrasenaValida ? 'green' : 'red';
        email.style.borderColor = emailValido ? 'green' : 'red';
        rol.style.borderColor = rolSeleccionado ? 'green' : 'red';

        btnCrear.disabled = !(nombreUsuarioValido && contrasenaValida && emailValido && rolSeleccionado);
    }

    nombreUsuario.addEventListener('change', validarFormulario);
    contrasena.addEventListener('change', validarFormulario);
    email.addEventListener('change', validarFormulario);
    rol.addEventListener('change', validarFormulario);

    validarFormulario();
});


document.getElementById('formularioCrear').addEventListener('submit', function (event) {
    if (btnCrear.disabled) {
        event.preventDefault(); // Prevenir el envío del formulario
        alert('Por favor, completa el formulario correctamente.');
    }
});