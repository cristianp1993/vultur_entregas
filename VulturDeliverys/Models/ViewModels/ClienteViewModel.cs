using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VulturDeliverys.Models.ViewModels
{
    public class ClienteViewModel
    {
        public int ClienteID { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int Documento { get; set; }

    }
}