using Proyecto.ClassAux;
using Proyecto.Database;
using Proyecto.Validaciones;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {

       
        private readonly DBEntities db;
        private readonly validacion val;
  
        public ProductoController()
        {
            db = new DBEntities();
            val = new validacion();
           
        }

        [HttpGet]
        public ActionResult Productos()
        {
            return View(db.Productos.ToList());
        }

        [HttpGet]
        public ActionResult Registrar()
        {
            return View(new ProductoAux());
        }

        [HttpPost]
        public ActionResult Registrar(ProductoAux producto)
        {
            val.validarProducto(producto, ModelState);

            if (ModelState.IsValid)
            {
                db.Productos.Add(producto.getConversion());
                db.SaveChanges();
                return RedirectToAction("Productos", "Producto");
            }
            return View(producto);
        }

        
        [HttpGet]
        public ActionResult Editar(int id)
        {
            var prod = db.Productos.Where(o => o.IdProducto == id).FirstOrDefault().getConversion();
            return View(prod == null ?new ProductoAux():prod);
        }

        [HttpPost]
        public ActionResult Editar(ProductoAux produc)
        {
            val.validarProducto(produc , ModelState);

            if (ModelState.IsValid)
            {
                editarProduc(produc);

                return RedirectToAction("Productos", "Producto");
            }
            return View(produc);
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            var produc = db.Productos.Where(o => o.IdProducto == id).FirstOrDefault();
            db.Productos.Remove(produc);
            db.SaveChanges();

            return RedirectToAction("Productos", "Producto");
        }

        [HttpPost]
        public ActionResult Buscar(string key)
        {
            var prod = db.Productos.Where(o => o.Nombre.ToLower()
                                .Equals(key.ToLower())).FirstOrDefault();

            

            return Json(prod == null ? new Models.Producto() : prod);
        }


        
        private void editarProduc(ProductoAux produc)
        {
            var oldProd = db.Productos.Where(o => o.IdProducto == produc.IdProducto).FirstOrDefault();
            oldProd.Nombre = produc.Nombre;
            oldProd.Stock = Convert.ToInt32(produc.Stock);
            oldProd.Precio = Convert.ToDecimal(produc.Precio);
            oldProd.Descripcion = produc.Descripcion;
            oldProd.UnidadMedida = produc.UnidadMedida;

            db.SaveChanges();
        }

        [HttpPost]
        public JsonResult getDatos(string ename)
        {
            DBEntities Db = new DBEntities();
            var emp = (from x in db.Productos
                       where x.Nombre.StartsWith(ename)

                       select new { label = x.Nombre, id = x.IdProducto, precio = x.Precio, stock=x.Stock, UnidadMedida=x.UnidadMedida }).ToList();

            return Json(emp);
        }
    }
}
