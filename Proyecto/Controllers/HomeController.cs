using Proyecto.Database;
using Proyecto.Models;
using Proyecto.Session;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class HomeController : Controller
    {
        private DBEntities db;

        public HomeController()
        {
            db = new DBEntities();
        }


        [HttpGet]
        public ActionResult LoginAdmin()
        {
            var asd = db.Administradores.ToList();
            return View(new Administrador());
        }

        [HttpPost]
        public ActionResult LoginAdmin(Administrador admin)
        {
            validBasic(admin.Dni, admin.Pass, admin);

            if (ModelState.IsValid)
            {
                Autenticador.Login(admin.Dni, false);

                return RedirectToAction("Productos", "Producto");
            }
            return View(admin);
        }


        [HttpGet]
        public ActionResult LoginVend()
        {
            return View(new Vendedor());
        }

        [HttpPost]
        public ActionResult LoginVend(Vendedor vend)
        {
            validBasic(vend.Dni, vend.Pass, vend);

            if(ModelState.IsValid)
            {
                Autenticador.Login(vend.Dni, false);
                return RedirectToAction("Reportes", "Reporte");
            }
            
            return View(vend);
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            Autenticador.LogOut();
            
            return RedirectToAction("LoginAdmin", "Home");
        }



        private void validBasic(string dni, string pass, object obj)
        {
            ModelState.Clear();

            if (string.IsNullOrEmpty(dni))
                ModelState.AddModelError("Dni", "Debe agregar un dni!");

            if (string.IsNullOrEmpty(pass))
            {
                ModelState.AddModelError("Pass", "Debe agregar una contraseña!");
                return;
            }
            if (!isCorrectDni(dni))
            {
                ModelState.AddModelError("Dni", "El dni debe contener solo 8 números enteros!");
                return;
            }

            if (obj is Vendedor)
            {
                var aux = (Vendedor)obj;
                var vends = db.Vendedores.ToList();
                bool find = false;
                for (int i = 0; i < vends.Count; i++)
                    if (vends.ElementAt(i).Dni.Equals(aux.Dni) &&
                        vends.ElementAt(i).Pass.Equals(aux.Pass))
                    {
                        find = true;
                        Session[SessionNameManager.Vend.VENDDATA] = vends.ElementAt(i);
                        break;
                    }
                
                if (!find) { ModelState.AddModelError("General", "Usuario y/o pass incorrectos"); return; }
            }
            if (obj is Administrador)
            {
                var aux = (Administrador)obj;
                var admins = db.Administradores.ToList();
                bool find = false;

                for (int i = 0; i < admins.Count; i++)
                    if (admins.ElementAt(i).Dni.Equals(aux.Dni) &&
                        admins.ElementAt(i).Pass.Equals(aux.Pass))
                    {
                        find = true;
                        Session[SessionNameManager.Admin.ADMINDATA] = admins.ElementAt(i);
                        break;
                    }

                if (!find) { ModelState.AddModelError("General", "Usuario y/o pass incorrectos"); return; }
            }
        }

        private bool isCorrectDni(string dni)
        {
            int cont = 0;
            if (dni.Length == 8)
            {
                for (int i = 0; i < dni.Length; i++)
                    if (Char.IsDigit(dni[i])) cont++;
                return (cont == 8);
            }
            return false;
        }

    }
}