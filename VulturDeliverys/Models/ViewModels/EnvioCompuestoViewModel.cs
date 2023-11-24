using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VulturDeliverys.Models.ViewModels
{
    public class EnvioCompuestoViewModel
    {
        public EnvioViewModel NuevoEnvio { get; set; }
        public List<EnvioViewModel> TodosLosEnvios { get; set; }

        public EnvioCompuestoViewModel()
        {
            NuevoEnvio = new EnvioViewModel();
            TodosLosEnvios = new List<EnvioViewModel>();
        }
    }
}