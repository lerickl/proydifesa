using Proyecto.ClassAux;
using Proyecto.Database;
using Proyecto.Models;
using Proyecto.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    [Authorize]
    public class VentaController : Controller
    {
        private DBEntities db;
        public VentaController()
        {
            db = new DBEntities();
        }


        [HttpGet]
        public ActionResult IndexVentasV()
        {
            datos = db.DetalleVentas.ToList();
            return View(datos);
        }

        List<DetalleVenta> datos = new List<DetalleVenta>();

        [HttpGet]
        public ActionResult IndexVentas()
        {
            datos = db.DetalleVentas.ToList();
            return View(datos);
        }

        [HttpGet]
        public ActionResult Realizar()
        {
            return View(new DetalleVenta());
        }

        [HttpPost]
        public ActionResult Realizar(DetalleVenta detalleVenta)
        {

                db.DetalleVentas.Add(detalleVenta);
                db.SaveChanges();
                return RedirectToAction("IndexVentasV");
          
        }



        [HttpPost]
        public ActionResult AddDetalle(DetalleVenta  det)
        {
            det.Fecha = DateTime.Today;

            db.DetalleVentas.Add(det);
            db.SaveChanges();

            var actVend = (Vendedor)Session[SessionNameManager.Vend.VENDDATA];

            var lastDet = db.DetalleVentas.Where(o => o.IdVendedor ==
            actVend.IdVendedor).ToList().LastOrDefault();
            
            var oldProd = db.Productos.Where(o => o.IdProducto == det.IdProducto).FirstOrDefault();

            if (det.Cantidad > oldProd.Stock)
                return Json(false);
            
            oldProd.Stock -= det.Cantidad;
            db.SaveChanges();   

            return Json(lastDet);
        }

        [HttpPost]
        public ActionResult getInfoUpdate(int id)
        {
            var prod = db.Productos.Where(o => o.IdProducto == id).FirstOrDefault();
            return Json(prod == null ? new Producto() : prod);
        }

        [HttpPost]
        public ActionResult QuitarDetalle(int id)
        {
            var deta = db.DetalleVentas.Where(o => o.IdDetalleVenta == id).FirstOrDefault();
            
            db.DetalleVentas.Remove(deta);
            db.SaveChanges();

            return Json(true);
        }

        [HttpGet]
        public ActionResult BuscarVentas()
        {
            ViewBag.key = "";
            ViewBag.user = new Cliente();
            
            return View(new ObjetoBuscar(new InfoData(db), 
                new List<DetalleVenta>()));
        }

        [HttpPost]
        public ActionResult BuscarVentas(string key)
        {
            ViewBag.key = key;
            
            var auser = db.Clientes.Where(o => o.Dni.Equals(key)).FirstOrDefault()
            == null ? new Cliente() : db.Clientes.Where(o => o.Dni.Equals(key)).FirstOrDefault();

            ViewBag.user = auser;
            
            var detalles = db.DetalleVentas.Where(o => o.IdCliente == auser.IdCliente).ToList();

            return View(new ObjetoBuscar(new InfoData(db),
                detalles));
        }


        [HttpGet]
        public ActionResult EditarEstado(int id = 0)
        {
            var detalle = db.DetalleVentas.Where(o => o.IdDetalleVenta == id).FirstOrDefault();

            return View(new ObjEditar(new InfoData(db), detalle));
        }

        [HttpPost]
        public ActionResult EditarEstado(int newEstado, int idDet)
        {
            var oldDet = db.DetalleVentas.Where(o => o.IdDetalleVenta == idDet).FirstOrDefault();
            var prod = db.Productos.Where(o => o.IdProducto ==
                           oldDet.IdProducto).FirstOrDefault();

            switch (oldDet.Estado)
            {
                case EstadoVenta.PENDIENTE:
                case EstadoVenta.CANCELADO:
                    {
                        switch(newEstado)
                        {
                            case EstadoVenta.PENDIENTE:
                            case EstadoVenta.CANCELADO:
                                break;
                            case EstadoVenta.ANULADO:
                                {
                                    prod.Stock += oldDet.Cantidad;
                                    break;
                                }
                            default:break;
                        }
                        break;
                    }
                case EstadoVenta.ANULADO:
                    {
                        switch (newEstado)
                        {
                            case EstadoVenta.PENDIENTE:
                            case EstadoVenta.CANCELADO:
                                {
                                    prod.Stock -= oldDet.Cantidad;
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                default:
                    break;
            }
            
            oldDet.Estado = newEstado;
            db.SaveChanges();

            return RedirectToAction("BuscarVentas", "Venta");
        }


        
        
        
    }
}