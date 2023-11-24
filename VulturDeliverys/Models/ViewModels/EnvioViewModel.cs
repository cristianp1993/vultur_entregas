using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VulturDeliverys.Models.ViewModels
{
    public class EnvioViewModel
    {
        public int EnvioID { get; set; }
        public int EmisorID { get; set; }
        public int ReceptorID { get; set; }
        public string DireccionOrigen { get; set; }
        public string DireccionDestino { get; set; }
        public string TelefonoContacto { get; set; }
        public string DescripcionPaquete { get; set; }
        public double PesoPaquete { get; set; }
        public decimal ValorEnvio { get; set; }
        public int CiudadOrigenID { get; set; }
        public int CiudadDestinoID { get; set; }

        // Lista para el control de selección (dropdown) de clientes
        public List<SelectListItem> Clientes { get; set; }
        public List<SelectListItem> Ciudades { get; set; } 

        public EnvioViewModel()
        {
            Clientes = new List<SelectListItem>();
            Ciudades = new List<SelectListItem>();
        }

    }
}