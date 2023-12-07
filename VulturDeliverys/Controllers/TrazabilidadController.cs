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
                    Trazabilidad trazabilidad;

                    if (trazabilidadViewModel.TrazabilidadID != 0)
                    {
                        // Actualizar la trazabilidad existente
                        trazabilidad = context.Trazabilidad.FirstOrDefault(t => t.TrazabilidadID == trazabilidadViewModel.TrazabilidadID);
                        if (trazabilidad == null)
                        {
                            return Json(new { success = false, message = "Trazabilidad no encontrada." });
                        }
                    }
                    else
                    {
                        // Crear una nueva trazabilidad
                        trazabilidad = new Trazabilidad();
                        context.Trazabilidad.Add(trazabilidad);
                    }

                    // Asignar/Actualizar campos
                    trazabilidad.EnvioID = trazabilidadViewModel.EnvioID;
                    trazabilidad.FechaHora = trazabilidadViewModel.FechaHora;
                    trazabilidad.Ubicacion = trazabilidadViewModel.Ubicacion;
                    trazabilidad.Estado = trazabilidadViewModel.Estado;
                    trazabilidad.DetallesAdicionales = trazabilidadViewModel.DetallesAdicionales;

                    context.SaveChanges();

                    return Json(new { success = true, message = "Operación exitosa." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        public ActionResult ObtenerDetalles(int trazabilidadId)
        {
            try
            {
                using (var context = new VulturDeliverysEntities())
                {
                    var trazabilidad = context.Trazabilidad.FirstOrDefault(t => t.TrazabilidadID == trazabilidadId);
                    if (trazabilidad == null)
                    {
                        return Json(new {}, JsonRequestBehavior.AllowGet);
                    }

                    var trazabilidadDto = new TrazabilidadViewModel
                    {
                        TrazabilidadID = trazabilidad.TrazabilidadID,
                        EnvioID = (int)trazabilidad.EnvioID,
                        FechaHora = (DateTime)trazabilidad.FechaHora,
                        Ubicacion = trazabilidad.Ubicacion,
                        Estado = trazabilidad.Estado,
                        DetallesAdicionales = trazabilidad.DetallesAdicionales
                        // Otros campos según sea necesario
                    };

                    return Json(trazabilidadDto , JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new {}, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Eliminar(int trazabilidadId)
        {
            try
            {
                using (var context = new VulturDeliverysEntities())
                {
                    var trazabilidad = context.Trazabilidad.FirstOrDefault(t => t.TrazabilidadID == trazabilidadId);
                    if (trazabilidad == null)
                    {
                        return Json(new { success = false, message = "Trazabilidad no encontrada." });
                    }
                    else
                    {
                        context.Trazabilidad.Remove(trazabilidad);
                        context.SaveChanges();

                        return Json(new { success = true, message = "Trazabilidad Eliminada." });
                    }

                }
            }
            catch (Exception ex)
            {
                return View("Index");
            }
        }



    }
}