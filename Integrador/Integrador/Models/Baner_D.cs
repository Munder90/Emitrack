using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integrador.Models
{
    public class Baner_D
    {
        int id;
        Nullable<int> id_baner;
        Nullable<int> producto;
        Nullable<decimal> precio;
        Nullable<int> cantidad;

        public int Id { get => id; set => id = value; }
        public int? Id_baner { get => id_baner; set => id_baner = value; }
        public int? Producto { get => producto; set => producto = value; }
        public decimal? Precio { get => precio; set => precio = value; }
        public int? Cantidad { get => cantidad; set => cantidad = value; }
    }
}