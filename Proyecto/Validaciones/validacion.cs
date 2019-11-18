using Proyecto.ClassAux;
using Proyecto.Database;
using Proyecto.Models;
using Proyecto.Session;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Proyecto.Validaciones
{
    public class validacion
    {

        private DBEntities db;

        public void validarProducto(ProductoAux producto, System.Web.Mvc.ModelStateDictionary modelState)
        {
            modelState.Clear();

            if (string.IsNullOrEmpty(producto.Nombre))
                modelState.AddModelError("Nombre", "El nombre es campo obligatorio!");

            //var duplicado = db.Productos.Any(a => a.Nombre == producto.Nombre);

            //if (duplicado)
            //{
            //    modelState.AddModelError("Nombre", " El producto ya existe en la base de datos !");
            //}
            //else
            //{
            //    db.SaveChanges();
            //}
                       
            
            if (string.IsNullOrEmpty(producto.Stock))
                modelState.AddModelError("Stock", "El stock es campo obligatorio!");
            if (string.IsNullOrEmpty(producto.Precio))
                modelState.AddModelError("Precio", "El precio es campo obligatorio!");
            if (string.IsNullOrEmpty(producto.UnidadMedida))
            {
                modelState.AddModelError("UnidadMedida", "La unidad de medida es campo obligatorio!");
                return;
            }
            if (!isValidNumber(producto.Precio))
                       modelState.AddModelError("Precio", "El precio no debe tener caracteres espciales!");

                
        }
        private bool isValidNumber(string str)
        {
            try
            {
                decimal dec = Convert.ToDecimal(str);
                return true;
            }
            catch (Exception) { return false; }
        }


        public void validVend(Vendedor vend, System.Web.Mvc.ModelStateDictionary modelState)
        {
            modelState.Clear();
            if (string.IsNullOrEmpty(vend.Nombre))
                modelState.AddModelError("Nombre", "Campo nombre obligatorio!");
            if (string.IsNullOrEmpty(vend.ApPaterno))
                modelState.AddModelError("ApPaterno", "Campo ApPaterno obligatorio!");
            if (string.IsNullOrEmpty(vend.ApMaterno))
                modelState.AddModelError("ApMaterno", "Campo ApMaterno obligatorio!");
            if (string.IsNullOrEmpty(vend.Dni))
                modelState.AddModelError("Dni", "Campo DNI obligatorio!");
            if (string.IsNullOrEmpty(vend.Pass))
            { modelState.AddModelError("Pass", "Campo Pass obligatorio!"); return; }

            if (!isValidNombre(vend.Nombre))
                modelState.AddModelError("Nombre", "El nombre no debe tener caracteres especiales!");
            if (!isValidNombre(vend.ApPaterno))
                modelState.AddModelError("ApPaterno", "El ApPaterno no debe tener caracteres especiales!");
            if (!isValidNombre(vend.ApMaterno))
                modelState.AddModelError("ApMaterno", "El ApMaterno no debe tener caracteres especiales!");
            if (!isCorrectDni(vend.Dni))
            { modelState.AddModelError("Dni", "El DNI no debe tener caracteres especiales!"); return; }

            if (existeUser(vend.Dni))
                modelState.AddModelError("Dni", "Ya existe el mismo DNI!");
        }


        private bool existeUser(string dni)
        {
            var vends = db.Vendedores.ToList();
            foreach (var vend in vends)
                if (vend.Dni.Equals(dni)) return true;
            return false;
        }

        private bool isValidNombre(string str)
        {
            int cont = 0;
            if (str.Length < 3 || str.Length > 70) return false;
            if (string.IsNullOrWhiteSpace(str) ||
                string.IsNullOrEmpty(str)) return false;
            for (int i = 0; i < str.Length; i++)
            {
                if (!(char.IsLetter(str[i]) ||
                    char.IsWhiteSpace(str[i]))) cont++;
            }
            return (cont == 0);
        }

        private bool isCorrectDni(string dni)
        {
            int cont = 0;
            if (dni.Length == 8)
            {
                for (int i = 0; i < dni.Length; i++)
                    if (Char.IsDigit(dni[i])) cont++;
                return (cont == 8);
            }
            return false;
        }


        public void validCliente(Cliente client, System.Web.Mvc.ModelStateDictionary modelState)
        {
            modelState.Clear();

            if (string.IsNullOrEmpty(client.Nombre))
                modelState.AddModelError("Nombre", "El campo nombre es obligatorio!");
            if (string.IsNullOrEmpty(client.Dni))
            { modelState.AddModelError("Dni", "El campo DNI es obligatorio!"); return; }

            if (string.IsNullOrEmpty(client.Direccion))
                modelState.AddModelError("Direccion", "La direccion es obligatoria!");

            if (!isValidNombreC(client.Nombre))
            { modelState.AddModelError("Nombre", "El nombre no debe tener caracteres especiales!"); }

            if (!isCorrectDniC(client.Dni))
            { modelState.AddModelError("Dni", "El DNI no debe tener caracteres especiales!"); return; }

            if (existeUserC(client.Dni))
                modelState.AddModelError("Dni", "Otro cliente tiene el mismo DNI!");
        }

        private bool existeUserC(string dni)
        {
            var vends = db.Clientes.ToList();
            foreach (var vend in vends)
                if (vend.Dni.Equals(dni)) return true;
            return false;
        }

        private bool isValidNombreC(string str)
        {
            int cont = 0;
            if (str.Length < 3 || str.Length > 70) return false;
            if (string.IsNullOrWhiteSpace(str) ||
                string.IsNullOrEmpty(str)) return false;
            for (int i = 0; i < str.Length; i++)
            {
                if (!(char.IsLetter(str[i]) ||
                    char.IsWhiteSpace(str[i]))) cont++;
            }
            return (cont == 0);
        }

        private bool isCorrectDniC(string dni)
        {
            int cont = 0;
            if (dni.Length == 8)
            {
                for (int i = 0; i < dni.Length; i++)
                    if (Char.IsDigit(dni[i])) cont++;
                return (cont == 8);
            }
            return false;
        }

    }
}