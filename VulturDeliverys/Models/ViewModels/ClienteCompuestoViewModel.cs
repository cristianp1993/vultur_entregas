using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VulturDeliverys.Models.ViewModels
{
    public class ClienteCompuestoViewModel
    {
        public ClienteViewModel NuevoCliente { get; set; }
        public IEnumerable<ClienteViewModel> ClientesExistentes { get; set; }
    }
}