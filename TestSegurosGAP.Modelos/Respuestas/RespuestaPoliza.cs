namespace TestSegurosGAP.Entidades.Respuestas
{
    public class RespuestaPoliza
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

        public RespuestaTipoRiesgo TipoRiesgo { get; set; }
        public RespuestaTipoCubrimiento TipoCubrimiento { get; set; }
    }
}
