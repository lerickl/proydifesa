using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.ClassAux
{
    public class ObjEditar
    {
        public InfoData info { get; set; }
        public DetalleVenta detalle { get; set; }


        public ObjEditar(InfoData data, DetalleVenta det)
        {
            info = data;
            detalle = det;
        }



    }
}