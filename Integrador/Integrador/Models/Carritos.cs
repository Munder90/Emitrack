using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integrador.Models
{
    public class Carritos
    {
        int iD;
        string usuario;
        decimal total;
        List<Carritos_D> detalle;
        int direccion;
        int metod_Pago;
        bool factura;

        public int ID { get => iD; set => iD = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public decimal Total { get => total; set => total = value; }
        public List<Carritos_D> Detalle { get => detalle; set => detalle = value; }
        public int Direccion { get => direccion; set => direccion = value; }
        public int Metod_Pago { get => metod_Pago; set => metod_Pago = value; }
        public bool Factura { get => factura; set => factura = value; }
    }
}