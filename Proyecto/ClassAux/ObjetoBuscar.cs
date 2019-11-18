using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.ClassAux
{
    public class ObjetoBuscar
    {
        public InfoData info { get; set; }
        public List<DetalleVenta> detalleVentas { get; set; }

        public ObjetoBuscar(InfoData info, List<DetalleVenta> detalleVentas)
        {
            this.info = info;
            this.detalleVentas = detalleVentas;
        }
        
    }
}