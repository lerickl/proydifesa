using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.ClassAux
{
    public class ProductoAux
    {
        public int IdProducto { get; set; }

        public string Nombre { get; set; }
        public string Stock { get; set; }
        public string Precio { get; set; }
        public string Descripcion { get; set; }
        public string UnidadMedida { get; set; }



        public Producto getConversion()
        {
            return new Producto
            {
                IdProducto = this.IdProducto,
                Nombre = this.Nombre,
                Stock = Convert.ToInt32(this.Stock),
                Precio = Convert.ToDecimal(this.Precio),
                Descripcion = this.Descripcion,
                UnidadMedida = this.UnidadMedida
            };
        }


        public struct Unidades
        {
            public const string KILO = "Kilo";
            public const string GALON = "Galon";
            public const string UNIDAD = "unidad";
            public const string LITROS = "Litro";
            public const string METRO  = "Metro";
        }
    }
}