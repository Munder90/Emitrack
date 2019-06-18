using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integrador.Models
{
    public class Carritos
    {
        int iD;
        int usuario;
        int total;

        public int ID { get => iD; set => iD = value; }
        public int Usuario { get => usuario; set => usuario = value; }
        public int Total { get => total; set => total = value; }
    }
}