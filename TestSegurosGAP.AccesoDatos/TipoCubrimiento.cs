//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestSegurosGAP.AccesoDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class TipoCubrimiento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoCubrimiento()
        {
            this.Poliza = new HashSet<Poliza>();
        }
    
        public int IdTipoCubrimiento { get; set; }
        public string NombreTipoCubrimiento { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Poliza> Poliza { get; set; }
    }
}
