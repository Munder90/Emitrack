using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integrador.Models
{
    public class Productos_T
    {
        int iD;
        string descripcion;

        public int ID { get => iD; set => iD = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}