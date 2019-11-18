using Proyecto.ClassAux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public string UnidadMedida { get; set; }

        

        public ProductoAux getConversion()
        {
            return new ProductoAux
            {
                IdProducto = this.IdProducto,
                Nombre = this.Nombre,
                Stock = Convert.ToString(this.Stock),
                Precio = Convert.ToString(this.Precio),
                Descripcion = this.Descripcion,
                UnidadMedida = this.UnidadMedida
            };
        }


    }
}