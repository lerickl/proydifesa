using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using Proyecto.Models;

namespace Proyecto.Database.Maps
{
    public class ProductoMap : EntityTypeConfiguration<Producto>
    {
        public ProductoMap()
        {
            ToTable(Table.Name.PRODUCTO);

            HasKey(o => o.IdProducto);
        }
    }
}