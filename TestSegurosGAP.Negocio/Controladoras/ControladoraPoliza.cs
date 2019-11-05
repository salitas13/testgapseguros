namespace TestSegurosGAP.Negocio.Controladoras
{
    using System.Collections.Generic;
    using TestSegurosGAP.Entidades;
    using TestSegurosGAP.Entidades.Enumeradores;
    using TestSegurosGAP.Entidades.Excepciones;
    using TestSegurosGAP.ModeloDatos;
    using TestSegurosGAP.Utilidades;
    using System.Linq;
    using TestSegurosGAP.Entidades.Respuestas;

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

        public List<RespuestaPoliza> ObtenerPolizas()
        {
            var polizas = _unitOfWork.PolizasRepository.GetAll().ToList();

            return polizas.Select(t => new RespuestaPoliza
            {
                IdCliente = t.IdCliente,
                IdPoliza = t.IdPoliza,
                Cobertura = t.Cobertura,
                Descripcion = t.Descripcion,
                FechaInicioVigencia = t.FechaInicioVigencia,
                IdTipoCubrimiento = t.IdTipoCubrimiento,
                IdTipoRiesgo = t.IdTipoRiesgo,
                Nombre = t.Nombre,
                PeriodoCobertura = t.PeriodoCobertura,
                PrecioPoliza = t.PrecioPoliza,
                Estado = t.Estado
            }).ToList();
        }

        public Poliza ObtenerPoliza(int id)
        {
            return _unitOfWork.PolizasRepository.Find(id);
        }

        public List<RespuestaPoliza> ObtenerPolizasCliente(int id)
        {
            var polizas = _unitOfWork.PolizasRepository.Find(t => t.IdCliente == id).ToList();

            return polizas.Select(t => new RespuestaPoliza
            {
                IdCliente = t.IdCliente,
                IdPoliza = t.IdPoliza,
                Cobertura = t.Cobertura,
                Descripcion = t.Descripcion,
                FechaInicioVigencia = t.FechaInicioVigencia,
                IdTipoCubrimiento = t.IdTipoCubrimiento,
                IdTipoRiesgo = t.IdTipoRiesgo,
                Nombre = t.Nombre,
                PeriodoCobertura = t.PeriodoCobertura,
                PrecioPoliza = t.PrecioPoliza,
                Estado = t.Estado,
                TipoCubrimiento = new RespuestaTipoCubrimiento
                {
                    IdTipoCubrimiento = t.TipoCubrimiento.IdTipoCubrimiento,
                    Nombre = t.TipoCubrimiento.Nombre
                },
                TipoRiesgo = new RespuestaTipoRiesgo
                {
                    IdTipoRiesgo = t.TipoRiesgo.IdTipoRiesgo,
                    Nombre = t.TipoRiesgo.Nombre
                }
            }).ToList();
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

            // Creo la póliza con Estado "Activo"
            poliza.Estado = true;

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

            var polizaUpdate = this.ObtenerPoliza(poliza.IdPoliza);

            polizaUpdate.Cobertura = poliza.Cobertura;
            polizaUpdate.Descripcion = poliza.Descripcion;
            polizaUpdate.Estado = poliza.Estado;
            polizaUpdate.FechaInicioVigencia = poliza.FechaInicioVigencia;
            polizaUpdate.IdCliente = poliza.IdCliente;
            polizaUpdate.IdTipoCubrimiento = poliza.IdTipoCubrimiento;
            polizaUpdate.IdTipoRiesgo = poliza.IdTipoRiesgo;
            polizaUpdate.Nombre = poliza.Nombre;
            polizaUpdate.PeriodoCobertura = poliza.PeriodoCobertura;
            polizaUpdate.PrecioPoliza = poliza.PrecioPoliza;

            _unitOfWork.PolizasRepository.Edit(polizaUpdate);
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
        public List<RespuestaTipoRiesgo> ObtenerTiposRiesgo()
        {
            var tipos = _unitOfWork.TipoRiesgoRepository.GetAll();

            return tipos.Select(t => new RespuestaTipoRiesgo
            {
                Nombre = t.Nombre,
                IdTipoRiesgo = t.IdTipoRiesgo
            }).ToList(); ;
        }

        /// <summary>
        /// Obtengo la lista de tipo de cubrimiento
        /// </summary>
        /// <returns>Lista de tipos de cubrimiento</returns>
        public List<RespuestaTipoCubrimiento> ObtenerTiposCubrimiento()
        {
            var tipos = _unitOfWork.TipoCubrimientoRepository.GetAll().ToList();

            return tipos.Select(t => new RespuestaTipoCubrimiento
            {
                Nombre = t.Nombre,
                IdTipoCubrimiento = t.IdTipoCubrimiento
            }).ToList(); ;
        }
    }
}
