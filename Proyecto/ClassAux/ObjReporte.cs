using Proyecto.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.ClassAux
{
    public class ObjReporte
    {
        private DBEntities db;

        public const string SUBJECT = "Ferr Difesa";
       


        public ObjReporte(DBEntities db)
        {
            this.db = db;
        }


        public decimal GetGananciasDia()
        {
            decimal gan = 0;
            var dets = db.DetalleVentas.Where(o => o.Estado == EstadoVenta.CANCELADO && 
                (o.Fecha.Day == DateTime.Today.Day  && 
                 o.Fecha.Month == DateTime.Today.Month &&
                 o.Fecha.Year == DateTime.Today.Year)).ToList();

            foreach (var det in dets)
                gan += det.Total;

            return gan;
        }

        public int getVentasCanceladasDias()
        {
            var dets = db.DetalleVentas.Where(o => o.Estado == EstadoVenta.CANCELADO &&
                (o.Fecha.Day == DateTime.Today.Day &&
                 o.Fecha.Month == DateTime.Today.Month &&
                 o.Fecha.Year == DateTime.Today.Year)).ToList();
            
            return dets.Count;
        }

        public int getVentasAnuladasDias()
        {
            var dets = db.DetalleVentas.Where(o => o.Estado == EstadoVenta.ANULADO &&
                (o.Fecha.Day == DateTime.Today.Day &&
                 o.Fecha.Month == DateTime.Today.Month &&
                 o.Fecha.Year == DateTime.Today.Year)).ToList();

            return dets.Count;
        }

        public int getVentasPendientesDias()
        {
            var dets = db.DetalleVentas.Where(o => o.Estado == EstadoVenta.PENDIENTE &&
                (o.Fecha.Day == DateTime.Today.Day &&
                 o.Fecha.Month == DateTime.Today.Month &&
                 o.Fecha.Year == DateTime.Today.Year)).ToList();

            return dets.Count;
        }

        public  string GetBody()
        {
            return "<h1> Reporte del día </h1> " +
            "<h2>Cantidad de ganancias del día S./ : " + GetGananciasDia() + " </h2>" +
            "<h2>Cantidad de ventas canceladas del día : " + getVentasCanceladasDias() + "</h2>" +
            "<h2>Cantidad de ventas anuladas del día : " + getVentasAnuladasDias() + "</h2>" +
            "<h2>Cantidad de ventas pendientes del día : " + getVentasPendientesDias() + "</h2>";
        }
    }
}