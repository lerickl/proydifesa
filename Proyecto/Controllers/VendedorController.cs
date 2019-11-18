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
    public class VendedorController : Controller
    {
        private DBEntities db;
        private readonly validacion val;

        public VendedorController()
        {
            db = new DBEntities();
            val = new validacion();
        }

        [HttpGet]
        public ActionResult Vendedores()
        {
            return View(db.Vendedores.ToList());
        }


        [HttpGet]
        public ActionResult Registrar()
        {
            return View(new Vendedor());
        }
        [HttpPost]
        public ActionResult Registrar(Vendedor vend)
        {
            val.validVend(vend, ModelState);

            if (ModelState.IsValid)
            {
                db.Vendedores.Add(vend);

                db.SaveChanges();

                return RedirectToAction("Vendedores", "Vendedor");
            }

            return View(vend);
        }


        [HttpGet]
        public ActionResult Editar(int id)
        {
            var vend = db.Vendedores.Where(o => o.IdVendedor == id).FirstOrDefault();

            return View(vend == null ? new Vendedor() : vend);
        }

        [HttpPost]
        public ActionResult Editar(Vendedor vend)
        {
            val.validVend(vend, ModelState);

            if (ModelState.IsValid)
            {
                editarVend(vend);

                return RedirectToAction("Vendedores", "Vendedor");
            }

            return View(vend);
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            var vend = db.Vendedores.Where(o => o.IdVendedor == id).FirstOrDefault();
            db.Vendedores.Remove(vend);
            db.SaveChanges();

            return RedirectToAction("Vendedores", "Vendedor");
        }

        private void editarVend(Vendedor vend)
        {
            var oldVend = db.Vendedores.Where(o => o.IdVendedor == vend.IdVendedor).FirstOrDefault();
            oldVend.Nombre = vend.Nombre;
            oldVend.ApPaterno = vend.ApPaterno;
            oldVend.ApMaterno = vend.ApMaterno;
            oldVend.Dni = vend.Dni;
            oldVend.Direccion = vend.Direccion;
            oldVend.Pass = vend.Pass;

            db.SaveChanges();
        }


    }
}
