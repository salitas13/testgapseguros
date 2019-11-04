namespace TestSegurosGAP.Negocio.Controladoras
{
    using System.Collections.Generic;
    using TestSegurosGAP.Entidades;
    using TestSegurosGAP.Entidades.Enumeradores;
    using TestSegurosGAP.Entidades.Excepciones;
    using TestSegurosGAP.ModeloDatos;
    using TestSegurosGAP.Utilidades;
    using System.Linq;

    public class ControladoraPoliza
    {
        #region Private member variables.

        private readonly UnitOfWork _unitOfWork;

        #endregion

        #region Public constructor.

        /// <summary>
        /// Public constructor.
        /// </summary>
        public ControladoraPoliza(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public List<Poliza> ObtenerPolizas()
        {
            return _unitOfWork.PolizasRepository.GetAll().ToList();
        }

        public Poliza ObtenerPoliza(int id)
        {
            return _unitOfWork.PolizasRepository.Find(id);
        }

        public List<Poliza> ObtenerPolizasCliente(int id)
        {
            return _unitOfWork.PolizasRepository.Find(t => t.IdCliente == id).ToList();
        }

        public void RegistrarPoliza(Poliza poliza)
        {
            // Obtengo el Tipo de giesgo y valido el máximo porcentaje de cobertura
            var tipoRiesgo = _unitOfWork.TipoRiesgoRepository.Find(poliza.IdTipoRiesgo);

            // Se establecio el parametro PorcentajeCoberturaMaxima para determinar cuanto es el pocentaje 
            // máximo por Tipo de Riesgo
            if (poliza.Cobertura > tipoRiesgo.PorcentajeCoberturaMaxima)
            {
                throw new ExcepcionValidacion(UtilidadesGenerico.LeerMensaje(CodigosMensajes.ErrorRiesgoAlto.ToString()));
            }

            _unitOfWork.PolizasRepository.Add(poliza);
            _unitOfWork.Save();
        }

        public void EditarPoliza(Poliza poliza)
        {
            // Obtengo el Tipo de giesgo y valido el máximo porcentaje de cobertura
            var tipoRiesgo = _unitOfWork.TipoRiesgoRepository.Find(poliza.IdTipoRiesgo);

            // Se establecio el parametro PorcentajeCoberturaMaxima para determinar cuanto es el pocentaje 
            // máximo por Tipo de Riesgo
            if (poliza.Cobertura > tipoRiesgo.PorcentajeCoberturaMaxima)
            {
                throw new ExcepcionValidacion(UtilidadesGenerico.LeerMensaje(CodigosMensajes.ErrorRiesgoAlto.ToString()));
            }

            _unitOfWork.PolizasRepository.Edit(poliza);
            _unitOfWork.Save();
        }

        public void EliminarPolizas(List<int> idPolizas)
        {
            foreach (int id in idPolizas)
            {
                _unitOfWork.PolizasRepository.Delete(id);
            }

            _unitOfWork.Save();
        }

        /// <summary>
        /// Obtengo la lista de tipo de riesgo
        /// </summary>
        /// <returns>Lista de tipos de riesgo</returns>
        public List<TipoRiesgo> ObtenerTiposRiesgo()
        {
            return _unitOfWork.TipoRiesgoRepository.GetAll().ToList();
        }

        /// <summary>
        /// Obtengo la lista de tipo de cubrimiento
        /// </summary>
        /// <returns>Lista de tipos de cubrimiento</returns>
        public List<TipoCubrimiento> ObtenerTiposCubrimiento()
        {
            return _unitOfWork.TipoCubrimientoRepository.GetAll().ToList();
        }
    }
}
