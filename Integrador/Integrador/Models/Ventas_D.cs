using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integrador.Models
{
    public class Ventas_D
    {
        int iD;
        int iD_Venta;
        int producto;
        int cantidad;
        decimal total;

        public int ID { get => iD; set => iD = value; }
        public int ID_Venta { get => iD_Venta; set => iD_Venta = value; }
        public int Producto { get => producto; set => producto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public decimal Total { get => total; set => total = value; }
    }
}