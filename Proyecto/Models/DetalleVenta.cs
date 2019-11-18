using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }
        
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public int IdVendedor { get; set; }
   
        public DateTime Fecha { set; get; }
        public decimal Total { set; get; }
        public int Cantidad { set; get; }
        public int Estado { get; set; }
    }
}