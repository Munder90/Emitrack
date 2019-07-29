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

        public int ID { get => iD; set => iD = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public decimal Total { get => total; set => total = value; }
        public List<Carritos_D> Detalle { get => detalle; set => detalle = value; }
    }
}