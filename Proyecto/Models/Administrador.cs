using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Administrador
    {
        public int IdAdministrador { get; set; }


        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Dni { get; set; }
        public string Direccion { get; set; }

        public string Pass { get; set; }

    }
}