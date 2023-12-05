async function validateForm() {
    const requiredElements = document.querySelectorAll('form .required');
    let isValid = true;

    requiredElements.forEach(function (element) {
        // Reiniciar el estilo del borde a su estado original
        element.style.borderColor = '';

        // Verificar si el campo está vacío o su valor es 0
        if (element.value === '' || element.value === '0') {
            element.style.borderColor = 'red';
            isValid = false;
        }

        // Validamos lo eelementos tipo select
        if (element.tagName === 'SELECT' && element.selectedIndex === 0) {
            element.style.borderColor = 'red';
            isValid = false;
        }
    });

    return isValid;
}

function errorInicio() {
    Swal.fire({
        title: 'Error',
        text: 'Ocurrió un error al intentar realizar el proceso',
        icon: 'error',
        timer: 2000
    });
}