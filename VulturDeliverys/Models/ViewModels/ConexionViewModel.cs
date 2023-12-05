using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VulturDeliverys.Models.ViewModels
{
    public class ConexionViewModel
    {
        
        public int ConexionID { get; set; } // Si se utiliza un identificador automático

        [DisplayName("Guia Envio")]
        public int? EnvioID { get; set; } // Identificador del envío asociado

        [DisplayName("Ciudad Origen")]
        // Puede que necesites referencias a objetos o IDs para ciudades origen y destino
        public int CiudadOrigenID { get; set; }

        [DisplayName("Ciudad Destino")]
        public int CiudadDestinoID { get; set; }

        [DisplayName("Fecha de Salida")]
        public DateTime FechaSalida { get; set; }

        [DisplayName("Fecha de Llegada")]
        public DateTime? FechaLlegada { get; set; } // Puede ser nulo si aún no ha llegado
        public string NombreCiudadOrigen { get; set; } 
        public string NombreCiudadDestino { get; set; } 

        // Listas para dropdowns, si se necesitan en la interfaz de usuario
        public List<SelectListItem> CiudadesOrigen { get; set; }
        public List<SelectListItem> CiudadesDestino { get; set; }

        public ConexionViewModel()
        {
            CiudadesOrigen = new List<SelectListItem>();
            CiudadesDestino = new List<SelectListItem>();
        }


    }
}