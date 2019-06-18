using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integrador.Models
{
    public class Ventas
    {
        int iD;
        int usuario;
        System.DateTime fecha;
        decimal total;
        bool entrega;
        int direccion;
        int metod_Pago;
        bool factura;

        public int ID { get => iD; set => iD = value; }
        public int Usuario { get => usuario; set => usuario = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public decimal Total { get => total; set => total = value; }
        public bool Entrega { get => entrega; set => entrega = value; }
        public int Direccion { get => direccion; set => direccion = value; }
        public int Metod_Pago { get => metod_Pago; set => metod_Pago = value; }
        public bool Factura { get => factura; set => factura = value; }
    }
}