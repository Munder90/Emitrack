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
    
    public partial class PRODUCTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCTO()
        {
            this.BANER_D = new HashSet<BANER_D>();
            this.CARRITO_D = new HashSet<CARRITO_D>();
            this.PRODUCTO_M = new HashSet<PRODUCTO_M>();
            this.PRODUCTO_TIPO = new HashSet<PRODUCTO_TIPO>();
            this.VENTA_D = new HashSet<VENTA_D>();
        }
    
        public int ID { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public Nullable<System.DateTime> Fecha_Mo { get; set; }
        public string Descripcion { get; set; }
        public Nullable<decimal> Precio_A { get; set; }
        public decimal Precio_V { get; set; }
        public Nullable<bool> Activo { get; set; }
        public string Imagen { get; set; }
        public string Codigo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BANER_D> BANER_D { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CARRITO_D> CARRITO_D { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCTO_M> PRODUCTO_M { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCTO_TIPO> PRODUCTO_TIPO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VENTA_D> VENTA_D { get; set; }
    }
}
