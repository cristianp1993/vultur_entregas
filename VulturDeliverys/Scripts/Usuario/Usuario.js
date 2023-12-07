
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

document.getElementById("formBuscador").addEventListener("submit", (e) => {
    e.preventDefault();
    const nroGuia = document.getElementById("buscador").value;

    if (nroGuia!="") {
        fetch('/Usuario/ObtenerDetallesDeEnvioJson?envioId=' + nroGuia)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Error en la solicitud');
                }
                return response.json(); // Convierte la respuesta a JSON
            })
            .then(data => {
                if (Object.keys(data).length === 0) {
                    
                    console.log('No se encontraron detalles del envío');
                   
                } else {
                    console.log(data);

                    if (data.Envio == null) {
                        Swal.fire({
                            title: 'OPSSS!!!',
                            text: 'No existe el numero de guia que buscas!',
                            icon: 'error',
                            timer: 2000
                        })
                    } else {

                        const dialogo = document.getElementById('dialogo');
                        dialogo.innerHTML = ''; // Limpio el contenido anterior
                        // Agregar información del envío
                        dialogo.appendChild(crearSeccionEnvio(data.Envio));

                        // Agregar conexiones
                        dialogo.appendChild(crearSeccionConexion(data.Conexiones));

                        // Agregar trazabilidades
                        dialogo.appendChild(crearSeccionTrazabilidad(data.Trazabilidades));
                       

                        dialogo.showModal();
                    }
                   

                }
            })
            .catch(error => console.error('Error:', error));

    }
    

})


function crearSeccionEnvio(envio) {
    const seccion = document.createElement('div');
    seccion.innerHTML = '<h3 style="text-align:center">Detalle del Envío</h3>';

    const tabla = document.createElement('table');
    tabla.style.width = '100%';
    tabla.setAttribute('border', '1');

    const thead = document.createElement('thead');
    const tbody = document.createElement('tbody');

    // Encabezados de la tabla
    const headers = ['Envío ID', 'Nombre Emisor', 'Nombre Receptor', 'Dirección Origen', 'Dirección Destino', 'Descripción Paquete', 'Peso Paquete', 'Valor Envío', 'Fecha Envío'];
    let tr = document.createElement('tr');
    headers.forEach(function (header) {
        const th = document.createElement('th');
        th.appendChild(document.createTextNode(header));
        tr.appendChild(th);
    });
    thead.appendChild(tr);

    // Datos de la tabla
    tr = document.createElement('tr');
    const valores = [envio.EnvioID, envio.NombreEmisor, envio.NombreReceptor, envio.DireccionOrigen, envio.DireccionDestino, envio.DescripcionPaquete, envio.PesoPaquete, envio.ValorEnvio, formatFecha(envio.FechaEnvio)];
    valores.forEach(function (valor) {
        const td = document.createElement('td');
        td.appendChild(document.createTextNode(valor));
        tr.appendChild(td);
    });
    tbody.appendChild(tr);

    tabla.appendChild(thead);
    tabla.appendChild(tbody);
    seccion.appendChild(tabla);

    return seccion;
}

function crearSeccionConexion(conexiones) {
    const seccion = document.createElement('div');
    seccion.innerHTML = '<h4 style="text-align:center; margin-top:35px">Detalle de la Conexión</h4>';

    const tabla = document.createElement('table');
    tabla.style.width = '100%';
    tabla.setAttribute('border', '1');

    const thead = document.createElement('thead');
    const tbody = document.createElement('tbody');

    // Encabezados de la tabla
    const headers = ['Ciudad Origen', 'Ciudad Destino', 'Fecha y Hora de Salida', 'Fecha y Hora de Llegada'];
    let tr = document.createElement('tr');
    headers.forEach(header => {
        const th = document.createElement('th');
        th.appendChild(document.createTextNode(header));
        tr.appendChild(th);
    });
    thead.appendChild(tr);

    // Añadir filas para cada conexión
    conexiones.forEach(conexion => {
        tr = document.createElement('tr');
        const valores = [
            conexion.NombreCiudadOrigen,
            conexion.NombreCiudadDestino,
            formatFecha(conexion.FechaHoraSalida),
            formatFecha(conexion.FechaHoraLlegada)
        ];
        valores.forEach(valor => {
            const td = document.createElement('td');
            td.appendChild(document.createTextNode(valor));
            tr.appendChild(td);
        });
        tbody.appendChild(tr);
    });

    tabla.appendChild(thead);
    tabla.appendChild(tbody);
    seccion.appendChild(tabla);

    return seccion;
}


function crearSeccionTrazabilidad(trazabilidades) {
    const seccion = document.createElement('div');
    seccion.innerHTML = '<h4 style="text-align:center; margin-top:35px">Detalle de Trazabilidad</h4>';

    const tabla = document.createElement('table');
    tabla.style.width = '100%';
    tabla.setAttribute('border', '1');

    const thead = document.createElement('thead');
    const tbody = document.createElement('tbody');

    // Encabezados de la tabla
    const headers = ['Fecha', 'ubicación', 'Observaciones', 'Estado'];
    let tr = document.createElement('tr');
    headers.forEach(header => {
        const th = document.createElement('th');
        th.appendChild(document.createTextNode(header));
        tr.appendChild(th);
    });
    thead.appendChild(tr);

    // Añadir filas para cada trazabilidad
    trazabilidades.forEach(trazabilidad => {
        tr = document.createElement('tr');
        const valores = [
            formatFecha(trazabilidad.FechaHora),
            trazabilidad.Ubicacion,
            trazabilidad.DetallesAdicionales,
            trazabilidad.Estado
        ];
        valores.forEach(valor => {
            const td = document.createElement('td');
            td.appendChild(document.createTextNode(valor));
            tr.appendChild(td);
        });
        tbody.appendChild(tr);
    });

    tabla.appendChild(thead);
    tabla.appendChild(tbody);
    seccion.appendChild(tabla);

    return seccion;
}



function formatFecha(fechaString) {
    var fecha = new Date(parseInt(fechaString.match(/\d+/)[0]));
    return fecha.toLocaleDateString("es-ES");
}