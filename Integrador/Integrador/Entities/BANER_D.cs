//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Integrador.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class BANER_D
    {
        public int ID { get; set; }
        public Nullable<int> ID_Baner { get; set; }
        public Nullable<int> Producto { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public Nullable<int> Cantidad { get; set; }
    
        public virtual BANER BANER { get; set; }
        public virtual PRODUCTO PRODUCTO1 { get; set; }
    }
}
