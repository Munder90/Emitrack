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
    
    public partial class USUARIO_DIR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIO_DIR()
        {
            this.VENTAs = new HashSet<VENTA>();
        }
    
        public int ID { get; set; }
        public string Usuario { get; set; }
        public string Calle { get; set; }
        public string Numero_Ext { get; set; }
        public string Numero_Int { get; set; }
        public Nullable<int> Colonia { get; set; }
        public Nullable<int> Municipio { get; set; }
        public Nullable<int> Estado { get; set; }
        public Nullable<int> CP { get; set; }
        public Nullable<bool> Activo { get; set; }
    
        public virtual ESTADO ESTADO1 { get; set; }
        public virtual LOCALIDAD LOCALIDAD { get; set; }
        public virtual MUNICIPIO MUNICIPIO1 { get; set; }
        public virtual USUARIO USUARIO1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VENTA> VENTAs { get; set; }
    }
}
