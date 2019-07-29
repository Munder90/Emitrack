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
        int producto_id;
        string producto;
        int cantidad;
        decimal total;

        public int ID { get => iD; set => iD = value; }
        public int ID_Carrito { get => iD_Carrito; set => iD_Carrito = value; }
        public int Producto_id { get => producto_id; set => producto_id = value; }
        public string Producto { get => producto; set => producto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public decimal Total { get => total; set => total = value; }
    }
}