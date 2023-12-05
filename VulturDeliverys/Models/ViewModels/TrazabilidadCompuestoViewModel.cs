using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VulturDeliverys.Models.ViewModels
{
    public class TrazabilidadCompuestoViewModel
    {
        public TrazabilidadViewModel NuevaTrazabilidad { get; set; }
        public List<TrazabilidadViewModel> TodasLasTrazabilidades { get; set; }

        public TrazabilidadCompuestoViewModel()
        {
            NuevaTrazabilidad = new TrazabilidadViewModel();
            TodasLasTrazabilidades = new List<TrazabilidadViewModel>();
        }
    }
}