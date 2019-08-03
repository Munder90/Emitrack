using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integrador.Models
{
    public class Baner
    {
        int id;
        string descripcion;
        string imagen;
        string fecha;
        Nullable<bool> activo;
        int pos;
        List<Baner_D> detalle;

        public int Id { get => id; set => id = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Imagen { get => imagen; set => imagen = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public bool? Activo { get => activo; set => activo = value; }
        public int Pos { get => pos; set => pos = value; }
        public List<Baner_D> Detalle { get => detalle; set => detalle = value; }
    }
}