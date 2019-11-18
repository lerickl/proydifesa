using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Proyecto.Database.Maps
{
    public class ClienteMap : EntityTypeConfiguration<Cliente>
    {
        public ClienteMap()
        {
            ToTable(Table.Name.CLIENTE);

            HasKey(o => o.IdCliente);
            

        }
    }
}