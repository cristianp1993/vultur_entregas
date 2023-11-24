using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VulturDeliverys.Models.ViewModels
{
    public class UsuarioViewModel
    {
        public int UsuarioID { get; set; }

        [Display(Name = "Usuario")]
        public string NombreUsuario { get; set; }
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }
        [Display(Name = "Correo")]
        public string Email { get; set; }
        public string Rol { get; set; }
    }
}