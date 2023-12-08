using Newtonsoft.Json.Linq;
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
        public ActionResult Index(UsuarioViewModel model)
        {

            using (var context = new VulturDeliverysEntities())
            {
                var user = context.Usuario.FirstOrDefault(xh => xh.NombreUsuario == model.NombreUsuario && xh.Contrasena == model.Contrasena);
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

        public ActionResult ObtenerDetallesDeEnvioJson(int envioId)
        {
            // Método que obtiene los detalles
            var detallesEnvio = ConsultarGuia(envioId);

            if (detallesEnvio != null)
            {
                return Json(detallesEnvio, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // Manejar el caso en que no se encuentren detalles para el envío
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Crear()
        {
            var roles = new List<SelectListItem>
            {
                new SelectListItem { Value = "Administrador", Text = "Administrador" },
                new SelectListItem { Value = "Asesor", Text = "Asesor" }
                
            };

            ViewBag.Roles = roles;
            UsuarioViewModel user = new UsuarioViewModel();

            return View();
        }

        [HttpPost]
        public ActionResult CrearUsuario(UsuarioViewModel model)
        {

            try
            {
                using (VulturDeliverysEntities context = new VulturDeliverysEntities())
                {

                    Usuario nuevoUsuario = new Usuario
                    {
                        NombreUsuario = model.NombreUsuario,
                        Contrasena = model.Contrasena,
                        Email = model.Email,
                        Rol = model.Rol
                    };

                    // Agregar el nuevo usuario al contexto y guardar los cambios
                    context.Usuario.Add(nuevoUsuario);
                    context.SaveChanges();

                    // Redireccionar a otro view, como el listado de usuarios, tras una creación exitosa
                    return RedirectToAction("Usuarios", "Usuario");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Error al crear el usuario: " + ex.Message);
            }

            // Si llegamos aquí, algo falló, vuelve a mostrar el formulario
            return View(model);
        }

        public ActionResult Usuarios()
        {
            List<UsuarioViewModel> usuariosViewModel = new List<UsuarioViewModel>();

            using (VulturDeliverysEntities context = new VulturDeliverysEntities())
            {

                List<Usuario> usuarios = context.Usuario.ToList();

                // Convertir cada entidad Usuario a UsuarioViewModel
                foreach (var usuario in usuarios)
                {
                    UsuarioViewModel viewModel = new UsuarioViewModel
                    {
                        UsuarioID = usuario.UsuarioID,
                        NombreUsuario = usuario.NombreUsuario,
                        Email = usuario.Email,
                        Rol = usuario.Rol

                    };
                    usuariosViewModel.Add(viewModel);
                }
            }

            return View(usuariosViewModel);
        }


        public EnvioDetallesViewModel ConsultarGuia(int nroGuia)
        {
            using (var context = new VulturDeliverysEntities())
            {
                var viewModel = new EnvioDetallesViewModel();

                // Obteniendo los datos del envío
                viewModel.Envio = context.Envio
                    .Where(e => e.EnvioID == nroGuia)
                    .Join(context.Cliente, en => en.EmisorID, cl => cl.ClienteID, (en, cl) => new { en, cl })
                    .Join(context.Cliente, temp => temp.en.ReceptorID, cli => cli.ClienteID, (temp, cli) => new EnvioModelQuery
                    {
                        EnvioID = temp.en.EnvioID,
                        NombreEmisor = temp.cl.Nombre,
                        NombreReceptor = cli.Nombre,
                        DireccionOrigen = temp.en.DireccionOrigen,
                        DireccionDestino = temp.en.DireccionDestino,
                        DescripcionPaquete = temp.en.DescripcionPaquete,
                        PesoPaquete = (double)temp.en.PesoPaquete,
                        ValorEnvio = (decimal)temp.en.ValorEnvio,
                        FechaEnvio = (DateTime)temp.en.FechaEnvio
                    })
                    .FirstOrDefault();

                // Obteniendo las conexiones relacionadas con el envío
                viewModel.Conexiones = context.Conexion
                    .Where(c => c.EnvioID == nroGuia)
                    .Join(context.Ciudad, en => en.CiudadOrigenID, ci => ci.CiudadID, (en, ci) => new { en, ci })
                    .Join(context.Ciudad, temp => temp.en.CiudadDestinoID, ciu => ciu.CiudadID, (temp, ciu) => new ConexionModelQuery
                    {
                        NombreCiudadOrigen = temp.ci.NombreCiudad,
                        NombreCiudadDestino = ciu.NombreCiudad,
                        FechaHoraSalida = (DateTime)temp.en.FechaHoraSalida,
                        FechaHoraLlegada = temp.en.FechaHoraLlegada
                    }).OrderBy(xh => xh.FechaHoraSalida)
                    .ToList();

                // Obteniendo las trazabilidades relacionadas con el envío
                viewModel.Trazabilidades = context.Trazabilidad
                    .Where(t => t.EnvioID == nroGuia)
                    .Select(t => new TrazabilidadModelQuery
                    {
                        FechaHora = (DateTime)t.FechaHora,
                        Ubicacion = t.Ubicacion,
                        Estado = t.Estado,
                        DetallesAdicionales = t.DetallesAdicionales
                    }).OrderBy(xh => xh.FechaHora)
                    .ToList();

                return viewModel;
            }
        }

    }
}