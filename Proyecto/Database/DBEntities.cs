using Proyecto.Database.Maps;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proyecto.Database
{
    public class DBEntities : DbContext
    {
        public IDbSet<Administrador> Administradores { get; set; }
        public IDbSet<Cliente> Clientes { get; set; }
        public IDbSet<DetalleVenta> DetalleVentas { get; set; }
        public IDbSet<Producto> Productos { get; set; }
        public IDbSet<Vendedor> Vendedores { get; set; }



        public DBEntities()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AdministradorMap());
            modelBuilder.Configurations.Add(new ClienteMap());
            modelBuilder.Configurations.Add(new DetalleVentaMap());
            modelBuilder.Configurations.Add(new ProductoMap());
            modelBuilder.Configurations.Add(new VendedorMap());
            
        }
    }
}