using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integrador.Models
{
    public class Municipios
    {
        int iD;
        string nombre;
        int estado;

        public int ID { get => iD; set => iD = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Estado { get => estado; set => estado = value; }
    }
}