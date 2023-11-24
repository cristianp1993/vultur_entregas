using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VulturDeliverys.Models;
using VulturDeliverys.Models.ViewModels;

namespace VulturDeliverys.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            ViewBag.ShowNavbar = false;
            return View();
        }


        /// <summary>
        /// Metodo que valida si el usuario que se va loguear existe
        /// </summary>
        /// <param name="param1">Modelo con los datos del usuario.</param>
        /// <returns>Devuelve la existencia o no del usuario</returns>
        [HttpPost]
        public ActionResult Index( UsuarioViewModel model)
        {

            using (var context = new VulturDeliverysEntities())
            {
                var user = context.Usuario.FirstOrDefault(xh=> xh.NombreUsuario== model.NombreUsuario && xh.Contrasena== model.Contrasena);
                if (user != null)
                {
                    return Json(new { success = true, message = "Inicio de sesión correcto." });
                }
                else
                {
                    return Json(new { success = false, message = "Error en el inicio de sesión." });
                }
            }
            
        }
    }
}