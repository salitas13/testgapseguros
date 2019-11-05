namespace TestSegurosGAP.Servicios.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using TestSegurosGAP.ModeloDatos;
    using TestSegurosGAP.Entidades;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net;
    using System;
    using TestSegurosGAP.Utilidades;
    using TestSegurosGAP.Entidades.Enumeradores;
    using TestSegurosGAP.Entidades.Excepciones;
    using TestSegurosGAP.Negocio.Controladoras;
    using TestSegurosGAP.Entidades.Respuestas;

    /// <summary>
    /// customer controller class for testing security token
    /// </summary>
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PolizaController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET api/Poliza
        public Task<HttpResponseMessage> Get()
        {
            try
            {
                ControladoraPoliza controladoraPolizas = new ControladoraPoliza(unitOfWork);
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK, controladoraPolizas.ObtenerPolizas()));
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

        [HttpGet]
        [ActionName("polizasbyid")]
        public Task<HttpResponseMessage> PolizabById(int id)
        {
            try
            {
                ControladoraPoliza controladoraPolizas = new ControladoraPoliza(unitOfWork);
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK, controladoraPolizas.ObtenerPoliza(id)));
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

        // GET api/Poliza/5
        [HttpGet]
        [ActionName("polizascliente")]
        public Task<HttpResponseMessage> PolizasCliente(int id)
        {
            try
            {
                ControladoraPoliza controladoraPolizas = new ControladoraPoliza(unitOfWork);
                var polizas = controladoraPolizas.ObtenerPolizasCliente(id);

                return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK, new Respuesta<List<RespuestaPoliza>>
                {
                    result = polizas,
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

        // POST api/Poliza
        public Task<HttpResponseMessage> Post([FromBody]Poliza poliza)
        {
            try
            {
                ControladoraPoliza controladoraPolizas = new ControladoraPoliza(unitOfWork);
                controladoraPolizas.RegistrarPoliza(poliza);
            }
            catch (ExcepcionValidacion ex)
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK, new Respuesta<string>
                {
                    message = ex.Message,
                    status = (int)HttpStatusCode.BadRequest
                }));
            }
            catch (Exception)
            {
                return Task.FromResult<HttpResponseMessage>(Request.CreateResponse(HttpStatusCode.InternalServerError, UtilidadesGenerico.LeerMensaje(CodigosMensajes.ErrorGenerico.ToString())));
            }

            return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK, new Respuesta<List<Cliente>>
            {
                status = (int)HttpStatusCode.OK
            }));
        }

        // PUT api/Poliza/5
        public Task<HttpResponseMessage> Put(int id, [FromBody]Poliza poliza)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ControladoraPoliza controladoraPolizas = new ControladoraPoliza(unitOfWork);
                    controladoraPolizas.EditarPoliza(poliza);
                }
                else
                {
                    return Task.FromResult(Request.CreateResponse(HttpStatusCode.BadRequest, UtilidadesGenerico.LeerMensaje(CodigosMensajes.ErrorGenerico.ToString())));
                }
            }
            catch (ExcepcionValidacion ex)
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.InternalServerError, UtilidadesGenerico.LeerMensaje(CodigosMensajes.ErrorGenerico.ToString())));
            }

            return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK, new Respuesta<string>
            {
                status = (int)HttpStatusCode.OK
            }));
        }

        // DELETE api/Poliza/5
        public Task<HttpResponseMessage> Delete(List<int> id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ControladoraPoliza controladoraPolizas = new ControladoraPoliza(unitOfWork);
                    controladoraPolizas.EliminarPolizas(id);
                }
                else
                {
                    return Task.FromResult(Request.CreateResponse(HttpStatusCode.BadRequest, UtilidadesGenerico.LeerMensaje(CodigosMensajes.ErrorGenerico.ToString())));
                }
            }
            catch (Exception)
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.InternalServerError, UtilidadesGenerico.LeerMensaje(CodigosMensajes.ErrorGenerico.ToString())));
            }

            return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK));
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}