using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integrador.Models
{
    public class Productos
    {
        int iD;
        string nombre;
        int cantidad;
        System.DateTime fecha_Mo;
        string descripcion;
        decimal precio_A;
        decimal precio_V;
        bool activo;
        string imagen;
        string codigo;
        string etiquetas;

        public int ID { get => iD; set => iD = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public DateTime Fecha_Mo { get => fecha_Mo; set => fecha_Mo = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public decimal Precio_A { get => precio_A; set => precio_A = value; }
        public decimal Precio_V { get => precio_V; set => precio_V = value; }
        public bool Activo { get => activo; set => activo = value; }
        public string Imagen { get => imagen; set => imagen = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public string Etiquetas { get => etiquetas; set => etiquetas = value; }
    }
}