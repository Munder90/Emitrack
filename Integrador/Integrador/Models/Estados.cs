using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integrador.Models
{
    public class Estados
    {
        int iD;
        string nombre;
        string abreb;

        public int ID { get => iD; set => iD = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Abreb { get => abreb; set => abreb = value; }
    }
}