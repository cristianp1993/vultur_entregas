using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VulturDeliverys.Models;
using VulturDeliverys.Models.ViewModels;

namespace VulturDeliverys.Controllers
{
    public class TrazabilidadController : Controller
    {
        // GET: Trazabilidad
        public ActionResult Index()
        {
            var compuestoViewModel = new TrazabilidadCompuestoViewModel
            {
                NuevaTrazabilidad = new TrazabilidadViewModel()
            };

            using (var context = new VulturDeliverysEntities())
            {
                // Suponiendo que tienes una tabla o entidad llamada Trazabilidades
                var trazabilidades = context.Trazabilidad.Select(t => new TrazabilidadViewModel
                {
                    TrazabilidadID = t.TrazabilidadID,
                    EnvioID = (int)t.EnvioID,
                    FechaHora = (DateTime)t.FechaHora,
                    Ubicacion = t.Ubicacion,
                    Estado = t.Estado,
                    DetallesAdicionales = t.DetallesAdicionales
                    // Mapea otras propiedades según sea necesario
                }).OrderBy(xh => xh.FechaHora).ToList();

                compuestoViewModel.TodasLasTrazabilidades = trazabilidades;
            }

            return View(compuestoViewModel);
        }


        [HttpPost]
        public ActionResult Agregar(TrazabilidadViewModel trazabilidadViewModel)
        {
            try
            {
                using (var context = new VulturDeliverysEntities())
                {
                    var nuevaTrazabilidad = new Trazabilidad
                    {

                        EnvioID = trazabilidadViewModel.EnvioID,
                        FechaHora = trazabilidadViewModel.FechaHora,
                        Ubicacion = trazabilidadViewModel.Ubicacion,
                        Estado = trazabilidadViewModel.Estado,
                        DetallesAdicionales = trazabilidadViewModel.DetallesAdicionales

                    };

                    context.Trazabilidad.Add(nuevaTrazabilidad);
                    context.SaveChanges();

                    return Json(new { success = true, message = "Ok." });
                }
            }
            catch (Exception)
            {

                return Json(new { success = false, message = "Error" });
            }                 
            
                
        }


    }
}