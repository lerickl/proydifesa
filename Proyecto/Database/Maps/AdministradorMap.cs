using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using Proyecto.Models;

namespace Proyecto.Database.Maps
{
    public class AdministradorMap : EntityTypeConfiguration<Administrador>
    {

        public AdministradorMap()
        {
            ToTable(Table.Name.ADMIN);

            HasKey(o => o.IdAdministrador);


        }

    }
}