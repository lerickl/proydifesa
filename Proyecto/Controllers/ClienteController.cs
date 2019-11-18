using Proyecto.Database;
using Proyecto.Models;
using Proyecto.Validaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private DBEntities db;
        private readonly validacion val;
        public ClienteController()
        {
            db = new DBEntities();
            val = new validacion();
        }


        [HttpGet]
        public ViewResult Listar()
        {
            var datos = db.Clientes.ToList();
            return View(datos);
        }



        [HttpGet]
        public ActionResult Registrar()
        {
            return View(new Cliente());
        }

        [HttpPost]
        public ActionResult Registrar(Cliente client)
        {
            val.validCliente(client, ModelState);

            if (ModelState.IsValid)
            {
                db.Clientes.Add(client);
                db.SaveChanges();

                return RedirectToAction("Listar");
            }
            return View(client);
        }

        [HttpPost]
        public ActionResult BuscarCliente(string key)
        {
            var result = db.Clientes.Where(o => o.Nombre.ToLower().Equals(key.ToLower()) ||
            o.Dni.Equals(key)).FirstOrDefault();

            return Json(result == null ? new Cliente() : result);
        }
        [HttpPost]
        public JsonResult getDatosUsuarios(string ename)
        {
            DBEntities Db = new DBEntities();
            var emp = (from x in db.Clientes
                       where x.Nombre.StartsWith(ename)
                       select new { label = x.Nombre, id = x.IdCliente }).ToList();
            return Json(emp);
        }


        public ActionResult Eliminar(int id)
        {


            var clienteDb = db.Clientes.Where(o => o.IdCliente == id).First();

            db.Clientes.Remove(clienteDb);

            db.SaveChanges();

            return RedirectToAction("Listar");
        }

    }
}