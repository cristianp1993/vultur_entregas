using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VulturDeliverys.Models.ViewModels
{
    public class TrazabilidadViewModel
    {
        public int TrazabilidadID { get; set; } 

        [DisplayName("ID del Envío")]
        public int EnvioID { get; set; } 

        [DisplayName("Fecha y Hora")]
        public DateTime FechaHora { get; set; } 

        [DisplayName("Ubicación")]
        public string Ubicacion { get; set; } 

        [DisplayName("Estado")]
        public string Estado { get; set; }
        public List<SelectListItem> EstadosPosibles { get; set; }

        public string DetallesAdicionales { get; set; }


        public TrazabilidadViewModel()
        {
            FechaHora = DateTime.Now;
            EstadosPosibles = new List<SelectListItem>
            {
            new SelectListItem { Value = "Bodega", Text = "En Bodega" },
            new SelectListItem { Value = "Ruta", Text = "En Ruta" },
            new SelectListItem { Value = "Entregado", Text = "Entregado" },
            new SelectListItem { Value = "Rechazado", Text = "Rechazado" },
            };
        }

    }
}