namespace TestSegurosGAP.Servicios.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using TestSegurosGAP.Entidades;
    using TestSegurosGAP.Entidades.Enumeradores;
    using TestSegurosGAP.Entidades.Excepciones;
    using TestSegurosGAP.Entidades.Respuestas;
    using TestSegurosGAP.ModeloDatos;
    using TestSegurosGAP.Negocio.Controladoras;
    using TestSegurosGAP.Utilidades;

    /// <summary>
    /// customer controller class for testing security token
    /// </summary>
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TipoRiesgoController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: api/TipoRiesgo
        public Task<HttpResponseMessage> Get()
        {
            try
            {
                ControladoraPoliza controladoraPolizas = new ControladoraPoliza(unitOfWork);
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK, new Respuesta<List<TipoRiesgo>>
                {
                    result = controladoraPolizas.ObtenerTiposRiesgo(),
                    status = (int)HttpStatusCode.OK
                }));
            }
            catch (ExcepcionValidacion ex)
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            catch (Exception)
            {
                return Task.FromResult<HttpResponseMessage>(Request.CreateResponse(HttpStatusCode.InternalServerError, UtilidadesGenerico.LeerMensaje(CodigosMensajes.ErrorGenerico.ToString())));
            }
        }
    }
}
