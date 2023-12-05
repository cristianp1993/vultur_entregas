using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VulturDeliverys.Models;
using VulturDeliverys.Models.ViewModels;

namespace VulturDeliverys.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            ViewBag.ShowNavbar = true;

            try
            {
                using (VulturDeliverysEntities context = new VulturDeliverysEntities())
                {

                    var clientes = context.Cliente.ToList().Select(xh => new ClienteViewModel
                    {
                        ClienteID = xh.ClienteID,
                        Documento = (int)xh.Documento,
                        Nombre = xh.Nombre,
                        Direccion = xh.Direccion,
                        Telefono = xh.Telefono,
                        Email = xh.Email
                    }).ToList();

                    var compuestoViewModel = new ClienteCompuestoViewModel
                    {
                        NuevoCliente = new ClienteViewModel(), // Un nuevo cliente vacío para el formulario
                        ClientesExistentes = clientes  // La lista de clientes existentes
                    };

                    return View(compuestoViewModel);


                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        [HttpPost]
        public ActionResult Agregar(ClienteViewModel model)
        {
            ViewBag.ShowNavbar = true;

            try
            {
                using (VulturDeliverysEntities context = new VulturDeliverysEntities())
                {

                    Cliente oCliente = new Cliente();

                    var query = context.Cliente.FirstOrDefault(xh => xh.Documento == model.Documento);

                    if (query != null)
                    {
                        return Json(new { success = false, message = "Duplicado" });
                    }
                    else
                    {
                        oCliente.Documento = model.Documento;
                        oCliente.Nombre = model.Nombre;
                        oCliente.Direccion = model.Direccion;
                        oCliente.Telefono = model.Telefono;
                        oCliente.Email = model.Email;

                        context.Cliente.Add(oCliente);
                        context.SaveChanges();

                        return Json(new { success = true, message = "Cliente creado." });
                    }

                }


            }
            catch (Exception)
            {

                return Json(new { success = false, message = "Error." });
            }


        }

        [HttpGet]
        public ActionResult Detalles(int ClienteID)
        {
            using (VulturDeliverysEntities context = new VulturDeliverysEntities())
            {

                var model = context.Cliente.Where(xh => xh.ClienteID == ClienteID).Select(xh => new ClienteViewModel
                {
                    ClienteID = xh.ClienteID,
                    Documento = (int)xh.Documento,
                    Nombre = xh.Nombre,
                    Direccion = xh.Direccion,
                    Telefono = xh.Telefono,
                    Email = xh.Email
                }).FirstOrDefault();

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Actualizar(ClienteViewModel model)
        {
            try
            {
                using (VulturDeliverysEntities context = new VulturDeliverysEntities())
                {
                    var cliente = context.Cliente.Find(model.ClienteID);

                    if (cliente != null)
                    {
                        cliente.Nombre = model.Nombre;
                        cliente.Documento = model.Documento;
                        cliente.Direccion = model.Direccion;
                        cliente.Telefono = model.Telefono;
                        cliente.Email = model.Email;

                        context.SaveChanges();

                        return Json(new { success = true, message = "Exitoso." });
                    }
                    else

                    {
                        return Json(new { success = false, message = "Error." });
                    }
                }



            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }




       
        public ActionResult Eliminar(int ClienteID)
        {
            try
            {
                using (VulturDeliverysEntities context = new VulturDeliverysEntities())
                {
                    var cliente = context.Cliente.Find(ClienteID);

                    if (cliente != null)
                    {
                        context.Cliente.Remove(cliente);
                        context.SaveChanges();
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}