using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VulturDeliverys.Models;
using VulturDeliverys.Models.ViewModels;

namespace VulturDeliverys.Controllers
{
    public class EnvioController : Controller
    {
        // GET: Envio
        public ActionResult Index()
        {
            ViewBag.ShowNavbar = true;

            using (VulturDeliverysEntities context = new VulturDeliverysEntities())
            {
                var TodosLosEnvios = context.Envio.Select(xh => new EnvioViewModel
                {

                    EnvioID = xh.EnvioID,
                    NombreEmisor = xh.Cliente.Nombre,
                    NombreReceptor = xh.Cliente1.Nombre,
                    DireccionOrigen = xh.DireccionOrigen,
                    DireccionDestino = xh.DireccionDestino,
                    TelefonoContacto = xh.TelefonoContacto,
                    DescripcionPaquete = xh.DescripcionPaquete,
                    PesoPaquete = xh.PesoPaquete.HasValue ? (double)xh.PesoPaquete : 0.0,
                    ValorEnvio = (decimal)xh.ValorEnvio,
                    FechaEnvio =(DateTime)xh.FechaEnvio


                }).ToList();

                var compuestoViewModel = new EnvioCompuestoViewModel
                {
                    NuevoEnvio = new EnvioViewModel
                    {
                        Clientes = ObtenerClientesParaDropdown(), // Método que devuelve List<SelectListItem>
                        Ciudades = ObtenerCiudadesParaDropdown()
                    },

                    TodosLosEnvios = TodosLosEnvios

                };


                return View(compuestoViewModel);

            }
        }


        [HttpPost]
        public ActionResult Agregar(EnvioViewModel model)
        {
            ViewBag.ShowNavbar = true;
            try
            {
                using (VulturDeliverysEntities context = new VulturDeliverysEntities())
                {
                    Envio oEnvio;
                    if (model.EnvioID != 0) 
                    {
                        oEnvio = context.Envio.FirstOrDefault(e => e.EnvioID == model.EnvioID);
                        if (oEnvio == null)
                        {
                            return Json(new { success = false, message = "Envío no encontrado." });
                        }
                    }
                    else
                    {
                        oEnvio = new Envio();
                        context.Envio.Add(oEnvio);
                    }

                    oEnvio.EmisorID = model.EmisorID;
                    oEnvio.EmisorID = model.EmisorID;
                    oEnvio.ReceptorID = model.ReceptorID;
                    oEnvio.DireccionOrigen = model.DireccionOrigen;
                    oEnvio.DireccionDestino = model.DireccionDestino;
                    oEnvio.TelefonoContacto = model.TelefonoContacto;
                    oEnvio.DescripcionPaquete = model.DescripcionPaquete;
                    oEnvio.PesoPaquete =(decimal) model.PesoPaquete;
                    oEnvio.ValorEnvio = model.ValorEnvio;                    
                    oEnvio.FechaEnvio = (DateTime)model.FechaEnvio;
                    oEnvio.CiudadOrigenID = model.CiudadOrigenID;
                    oEnvio.CiudadDestinoID = model.CiudadDestinoID;

                    context.SaveChanges();
                    return Json(new { success = true, message = "Operación exitosa." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Eliminar(int envioId)
        {
            ViewBag.ShowNavbar = true;
            try
            {
                using (VulturDeliverysEntities context = new VulturDeliverysEntities())
                {
                    Envio oEnvio = context.Envio.FirstOrDefault(e => e.EnvioID == envioId);

                    if (oEnvio == null)
                    {
                        return Json(new { success = false, message = "Envío no encontrado." });
                    }

                    context.Envio.Remove(oEnvio);
                    context.SaveChanges();

                    return Json(new { success = true, message = "Envío eliminado correctamente." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }


        public List<SelectListItem> ObtenerClientesParaDropdown()
        {
            try
            {
                using (VulturDeliverysEntities context = new VulturDeliverysEntities())
                {

                    var clientes = context.Cliente.Select(c => new SelectListItem
                    {
                        Value = c.ClienteID.ToString(),
                        Text = c.Nombre
                    }).ToList();

                    return clientes;

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SelectListItem> ObtenerCiudadesParaDropdown()
        {
            try
            {
                using (VulturDeliverysEntities context = new VulturDeliverysEntities())
                {

                    var ciudades = context.Ciudad.Select(c => new SelectListItem
                    {
                        Value = c.CiudadID.ToString(),
                        Text = c.NombreCiudad
                    })
                            .ToList();

                    return ciudades;

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public ActionResult Editar(int envioId)
        {
            using (VulturDeliverysEntities context = new VulturDeliverysEntities())
            {
                // Buscar el envío por ID
                var envio = context.Envio.FirstOrDefault(e => e.EnvioID == envioId);
                if (envio == null)
                {
                    // No se encontró el envío
                    return Json(new { success = false, message = "Error: " });
                }

                // Crear un ViewModel con los datos del envío
                var viewModel = new EnvioViewModel
                {
                    EnvioID = envio.EnvioID,
                    EmisorID = (int)envio.EmisorID,
                    ReceptorID = (int)envio.ReceptorID,
                    CiudadOrigenID = (int)envio.CiudadOrigenID,
                    DireccionOrigen = envio.DireccionOrigen,
                    CiudadDestinoID = (int)envio.CiudadDestinoID,
                    DireccionDestino = envio.DireccionDestino,
                    TelefonoContacto = envio.TelefonoContacto,
                    PesoPaquete = (double)envio.PesoPaquete,
                    ValorEnvio = (decimal)envio.ValorEnvio,
                    DescripcionPaquete = envio.DescripcionPaquete,
                    FechaEnvio = (DateTime)envio.FechaEnvio 
                };

                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }
        }

    }
}