using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VulturDeliverys.Models;
using VulturDeliverys.Models.ViewModels;

namespace VulturDeliverys.Controllers
{
    public class ConexionController : Controller
    {

        EnvioController envioController = new EnvioController();

        
        
        public ActionResult Index(int EnvioID)
        {
            using (VulturDeliverysEntities context = new VulturDeliverysEntities())
            {
                ConexionCompuestoViewModel dataConexion = new ConexionCompuestoViewModel();

                var todasConexiones = context.Conexion.Where(xh=> xh.EnvioID == EnvioID).ToList().Select(xh => new ConexionViewModel
                {
                    ConexionID = xh.ConexionID,
                    EnvioID = EnvioID,
                    CiudadOrigenID = (int)xh.CiudadOrigenID,
                    CiudadDestinoID = (int)xh.CiudadDestinoID,
                    NombreCiudadOrigen = xh.Ciudad.NombreCiudad,
                    NombreCiudadDestino = xh.Ciudad1.NombreCiudad,
                    FechaSalida = (DateTime)xh.FechaHoraSalida,
                    FechaLlegada = xh.FechaHoraLlegada

                }).ToList();

                List<SelectListItem> ciudades = envioController.ObtenerCiudadesParaDropdown();

                dataConexion.NuevaConexion.EnvioID = EnvioID;
                dataConexion.NuevaConexion.CiudadesOrigen = ciudades;
                dataConexion.NuevaConexion.CiudadesDestino = ciudades;

                dataConexion.TodasLasConexiones = todasConexiones;

                return View(dataConexion);
            }
        }

        [HttpPost]
        public ActionResult Agregar(ConexionViewModel viewModel)
        {
            try
            {
                using (VulturDeliverysEntities context = new VulturDeliverysEntities())
                {
                    // Buscar si ya existe una conexión con el mismo EnvioID y ConexionID
                    var conexionExistente = context.Conexion.FirstOrDefault(c => c.EnvioID == viewModel.EnvioID && c.ConexionID == viewModel.ConexionID);

                    if (conexionExistente != null)
                    {
                        // Actualizar la conexión existente
                        conexionExistente.CiudadOrigenID = viewModel.CiudadOrigenID;
                        conexionExistente.CiudadDestinoID = viewModel.CiudadDestinoID;
                        conexionExistente.FechaHoraSalida = viewModel.FechaSalida;
                        conexionExistente.FechaHoraLlegada = viewModel.FechaLlegada;
                    }
                    else
                    {
                        // Crear y agregar una nueva conexión
                        Conexion nuevaConexion = new Conexion
                        {
                            EnvioID = viewModel.EnvioID,
                            CiudadOrigenID = viewModel.CiudadOrigenID,
                            CiudadDestinoID = viewModel.CiudadDestinoID,
                            FechaHoraSalida = viewModel.FechaSalida,
                            FechaHoraLlegada = viewModel.FechaLlegada,
                        };

                        context.Conexion.Add(nuevaConexion);
                    }

                    // Guardar los cambios en la base de datos
                    context.SaveChanges();

                    return Json(new { success = true, message = "Ok." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }



        [HttpPost]
        public ActionResult Eliminar(int ConexionID)
        {
            using (VulturDeliverysEntities context = new VulturDeliverysEntities())
            {
                var conexion = context.Conexion.Find(ConexionID);
                if (conexion != null)
                {
                    context.Conexion.Remove(conexion);
                    context.SaveChanges();

                    return Json(new { success = true, message = "Ok." });
                }
                else
                {
                    // Manejar el caso en que la conexión no se encuentra
                    return Json(new { success = false, message = "Error" });
                }
            }
        }

        public ActionResult Editar(int conexionId)
        {
            using (VulturDeliverysEntities context = new VulturDeliverysEntities())
            {
                var conexion = context.Conexion.Find(conexionId);
                if (conexion != null)
                {
                    var conexionModel = new ConexionViewModel
                    {
                        ConexionID = conexion.ConexionID,
                        EnvioID = conexion.EnvioID,
                        CiudadOrigenID = (int)conexion.CiudadOrigenID,
                        CiudadDestinoID = (int)conexion.CiudadDestinoID,
                        FechaSalida = (DateTime) conexion.FechaHoraSalida,
                        FechaLlegada = conexion.FechaHoraLlegada,
                        
                    };

                    ConexionCompuestoViewModel dataConexion = new ConexionCompuestoViewModel();

                    dataConexion.NuevaConexion = conexionModel;

                    
                    return Json(dataConexion.NuevaConexion, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Manejar el caso en que la conexión no se encuentra
                    return HttpNotFound();
                }
            }
        }


    }
}