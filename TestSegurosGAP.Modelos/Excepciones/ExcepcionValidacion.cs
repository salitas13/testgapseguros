namespace TestSegurosGAP.Entidades.Excepciones
{
    using System;

    /// <summary>
    /// Representa los errores que se generan durante las validaciones y procesamiento del archivo
    /// </summary>
    [Serializable]
    public class ExcepcionValidacion : Exception
    {
        /// <summary>
        /// Inicializa una instancia de la clase ExcepcionValidacion
        /// </summary>
        public ExcepcionValidacion()
        {
        }

        /// <summary>
        /// Inicializa una instancia de la clase ExcepcionValidacion con el mensaje de error especificado
        /// </summary>
        public ExcepcionValidacion(string mensaje)
            : base(mensaje)
        {
        }
    }
}
