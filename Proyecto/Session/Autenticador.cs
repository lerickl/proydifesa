using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Proyecto.Session
{
    public class Autenticador
    {
        public static void Login(string dni, bool persistente)
        {
            FormsAuthentication.SetAuthCookie(dni, persistente);
        }

        public static void LogOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}