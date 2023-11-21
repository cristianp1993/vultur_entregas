using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VulturDeliverys.Models.ViewModels
{
    public class UsuarioViewModel
    {
        public int UsuarioID { get; set; }

        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
    }
}