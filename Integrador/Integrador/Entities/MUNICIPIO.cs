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
    
    public partial class MUNICIPIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MUNICIPIO()
        {
            this.LOCALIDADs = new HashSet<LOCALIDAD>();
            this.USUARIO_DIR = new HashSet<USUARIO_DIR>();
        }
    
        public int ID { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> Estado { get; set; }
    
        public virtual ESTADO ESTADO1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOCALIDAD> LOCALIDADs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USUARIO_DIR> USUARIO_DIR { get; set; }
    }
}
