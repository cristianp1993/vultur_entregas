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
                    Conexion nuevaConexion = new Conexion
                    {

                        EnvioID = viewModel.EnvioID,
                        CiudadOrigenID = viewModel.CiudadOrigenID,
                        CiudadDestinoID = viewModel.CiudadDestinoID,
                        FechaHoraSalida = viewModel.FechaSalida,
                        FechaHoraLlegada = viewModel.FechaLlegada,

                    };

                    context.Conexion.Add(nuevaConexion);
                    context.SaveChanges();

                   
                    return Json(new { success = true, message = "Ok." });

                }
            }
            catch (Exception)
            {

                return Json(new { success = false, message = "Error" });
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

                    // Cargar listas para dropdowns si son necesarias
                    return View("Index", dataConexion);
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