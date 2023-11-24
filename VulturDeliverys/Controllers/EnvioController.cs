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
                    EmisorID = (int)xh.EmisorID,
                    ReceptorID = (int)xh.ReceptorID,
                    DireccionOrigen = xh.DireccionOrigen,
                    DireccionDestino = xh.DireccionDestino,
                    TelefonoContacto = xh.TelefonoContacto,
                    DescripcionPaquete = xh.DescripcionPaquete,
                    PesoPaquete = (double)xh.PesoPaquete,
                    ValorEnvio = (decimal)xh.ValorEnvio,

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

                Envio oEnvio = new Envio();

                oEnvio.EmisorID = model.EmisorID;
                oEnvio.ReceptorID = model.ReceptorID;
                oEnvio.DireccionOrigen = model.DireccionOrigen;
                oEnvio.DireccionDestino = model.DireccionDestino;
                oEnvio.TelefonoContacto = model.TelefonoContacto;
                oEnvio.DescripcionPaquete = model.DescripcionPaquete;
                oEnvio.PesoPaquete = (decimal)model.PesoPaquete;
                oEnvio.ValorEnvio = model.ValorEnvio;

                context.Envio.Add(oEnvio);
                context.SaveChanges();

                return Json(new { success = true, message = "Cliente creado." });

            }
        }
        catch (Exception ex)
        {

            throw;
        }

    }


    private List<SelectListItem> ObtenerClientesParaDropdown()
    {
        try
        {
            using (VulturDeliverysEntities context = new VulturDeliverysEntities())
            {

                var clientes = context.Cliente.Select(c => new SelectListItem
                {
                    Value = c.ClienteID.ToString(),
                    Text = c.Nombre
                })
                        .ToList();

                return clientes;

            }
        }
        catch (Exception ex)
        {

            throw;
        }
    } 
    private List<SelectListItem> ObtenerCiudadesParaDropdown()
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

}
}