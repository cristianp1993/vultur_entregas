using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VulturDeliverys.Models.ViewModels
{
    public class ConexionCompuestoViewModel
    {
        public ConexionViewModel NuevaConexion { get; set; }

        // Lista de conexiones existentes para mostrar en una tabla
        public List<ConexionViewModel> TodasLasConexiones { get; set; }

        public ConexionCompuestoViewModel()
        {
            NuevaConexion = new ConexionViewModel();
            TodasLasConexiones = new List<ConexionViewModel>();
        }
    }
}