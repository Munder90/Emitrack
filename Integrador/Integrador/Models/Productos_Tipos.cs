using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integrador.Models
{
    public class Productos_Tipos
    {
        int iD;
        int producto;
        int tipo;

        public int ID { get => iD; set => iD = value; }
        public int Producto { get => producto; set => producto = value; }
        public int Tipo { get => tipo; set => tipo = value; }
    }
}