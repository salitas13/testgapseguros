//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestSegurosGAP.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class Poliza
    {
        public int IdPoliza { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime FechaInicioVigencia { get; set; }
        public short PeriodoCobertura { get; set; }
        public decimal PrecioPoliza { get; set; }
        public int IdTipoCubrimiento { get; set; }
        public int IdTipoRiesgo { get; set; }
        public int IdCliente { get; set; }
        public short Cobertura { get; set; }
        public bool Estado { get; set; }
    
        public virtual TipoRiesgo TipoRiesgo { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual TipoCubrimiento TipoCubrimiento { get; set; }
    }
}
