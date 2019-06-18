using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integrador.Models
{
    public class Carritos_D
    {
        int iD;
        int iD_Carrito;
        int producto;
        int cantidad;
        string total;

        public int ID { get => iD; set => iD = value; }
        public int ID_Carrito { get => iD_Carrito; set => iD_Carrito = value; }
        public int Producto { get => producto; set => producto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public string Total { get => total; set => total = value; }
    }
}