using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VulturDeliverys.Models.ViewModels
{
    public class EnvioViewModel
    {
        public int EnvioID { get; set; }

        [DisplayName("Emisor")]
        public int EmisorID { get; set; }

        [DisplayName("Receptor")]
        public int ReceptorID { get; set; }

        [DisplayName("Dirección Origen")]
        public string DireccionOrigen { get; set; }

        [DisplayName("Direccion Destino")]
        public string DireccionDestino { get; set; }

        [DisplayName("Teléfono de Contacto")]
        public string TelefonoContacto { get; set; }

        [DisplayName("Decripción del paquete")]
        public string DescripcionPaquete { get; set; }
        
        [DisplayName("Fecha de Envio")]
        public DateTime FechaEnvio { get; set; }

        [DisplayName("Peso del Paquete (Kg)")]
        public double PesoPaquete { get; set; }

        [DisplayName("Valor del envio")]
        public decimal ValorEnvio { get; set; }

        [DisplayName("Ciudad Origen")]
        public int CiudadOrigenID { get; set; } 

        [DisplayName("Ciudad Destino")]
        public int CiudadDestinoID { get; set; }

        public string NombreEmisor { get; set; }
        public string NombreReceptor { get; set; }

        // Lista para el control de selección (dropdown) de clientes
        public List<SelectListItem> Clientes { get; set; }
        public List<SelectListItem> Ciudades { get; set; } 

        public EnvioViewModel()
        {
            Clientes = new List<SelectListItem>();
            Ciudades = new List<SelectListItem>();
            FechaEnvio = DateTime.Today;
        }

    }
}