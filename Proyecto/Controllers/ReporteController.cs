using Proyecto.ClassAux;
using Proyecto.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    [Authorize]
    public class ReporteController : Controller
    {
        private DBEntities db;
        public ReporteController()
        {
            db = new DBEntities();
        }

        [HttpGet]
        public ActionResult Reportes(int alert = 0)
        {
            ViewBag.alert = alert;
            return View(new ObjReporte(db));
        }


        [HttpPost]
        public ActionResult EnviarCorreo(string correo)
        {
            ObjReporte obj = new ObjReporte(db);
            if (Send(correo, obj.GetBody()))
            {
                ViewBag.alert = 1;
                return RedirectToAction("Reportes", "Reporte", 
                    new { alert = 1});
            }
            ViewBag.alert = 2;
            return View(obj);
        }

        
        private bool isOn()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Send(string email, string body)
        {
            
            if (isOn())
            {
                MailMessage mail = new MailMessage();
                SmtpClient client = new SmtpClient();

                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new System.Net.NetworkCredential("stadisticBot", "bot123456");
                client.EnableSsl = true;
                client.Host = "smtp.gmail.com";

                mail.From = new MailAddress("to@example.com");
                mail.To.Add(email);
                
                mail.Subject = ObjReporte.SUBJECT;
                mail.Body = body;
                mail.IsBodyHtml = true;

                client.Send(mail);

                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
