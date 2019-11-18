using Proyecto.Database;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.ClassAux
{
    public class InfoData
    {
        private DBEntities db;


        public InfoData(DBEntities db)
        {
            this.db = db;
        }

        public Producto findProduc(int id)
        {
            return db.Productos.Where(o => o.IdProducto == id).FirstOrDefault();
        }

        public Cliente findCliente(int id)
        {
            return db.Clientes.Where(o => o.IdCliente == id).FirstOrDefault();
        }



        public string getRepresEstado(int num)
        {
            switch (num)
            {
                case EstadoVenta.ANULADO:
                    return "Anulado";
                case EstadoVenta.CANCELADO:
                    return "Cancelado";
                case EstadoVenta.PENDIENTE:
                    return "Pendiente";
                default:
                    return "";
            }
        }

    }
}