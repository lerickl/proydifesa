using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Proyecto.Database.Maps
{
    public class DetalleVentaMap : EntityTypeConfiguration<DetalleVenta>
    {

        public DetalleVentaMap()
        {
            ToTable(Table.Name.DETALLEVENTA);

            HasKey(o => o.IdDetalleVenta);

        }


    }
}