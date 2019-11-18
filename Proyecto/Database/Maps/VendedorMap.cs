using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using Proyecto.Models;

namespace Proyecto.Database.Maps
{
    public class VendedorMap : EntityTypeConfiguration<Vendedor>
    {
        public VendedorMap()
        {
            ToTable(Table.Name.VENDEDOR);

            HasKey(o => o.IdVendedor);
            
        }
    }
}