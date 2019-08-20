using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integrador.Models
{
    public class Ventas
    {
        int iD;
        string usuario;
        string fecha;
        decimal total;
        bool entrega;
        string direccion;
        string metod_Pago;
        bool factura;
        bool comprobante;
        string comprobante1;
        List<Ventas_D> detalle;

        public int ID { get => iD; set => iD = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public decimal Total { get => total; set => total = value; }
        public bool Entrega { get => entrega; set => entrega = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Metod_Pago { get => metod_Pago; set => metod_Pago = value; }
        public bool Factura { get => factura; set => factura = value; }
        public bool Comprobante { get => comprobante; set => comprobante = value; }
        public List<Ventas_D> Detalle { get => detalle; set => detalle = value; }
        public string Comprobante1 { get => comprobante1; set => comprobante1 = value; }
    }
}