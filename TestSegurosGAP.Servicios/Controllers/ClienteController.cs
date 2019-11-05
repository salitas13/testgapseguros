namespace TestSegurosGAP.Servicios.Controllers
{
    using AutoMapper;
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
    using System.Linq;

    /// <summary>
    /// customer controller class for testing security token
    /// </summary>
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ClienteController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET api/Cliente
        public Task<HttpResponseMessage> Get()
        {
            try
            {
                ControladoraCliente ControladoraCliente = new ControladoraCliente(unitOfWork);

                var respuesta = ControladoraCliente.ObtenerClientes();

                return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK, new Respuesta<List<RespuestaCliente>>
                {
                    result = respuesta,
                    status = (int)HttpStatusCode.OK
                }));
            }
            catch (Exception)
            {
                return Task.FromResult<HttpResponseMessage>(Request.CreateResponse(HttpStatusCode.InternalServerError, UtilidadesGenerico.LeerMensaje(CodigosMensajes.ErrorGenerico.ToString())));
            }
        }

        // GET api/Cliente/5
        public Task<HttpResponseMessage> Get(int id)
        {
            try
            {
                ControladoraCliente controladoraCliente = new ControladoraCliente(unitOfWork);

                var cliente = controladoraCliente.ObtenerCliente(id);

                return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK, new Respuesta<RespuestaCliente>
                {
                    result = cliente,
                    status = (int)HttpStatusCode.OK
                }));
            }
            catch (Exception)
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK, new Respuesta<string>
                {
                    message = UtilidadesGenerico.LeerMensaje(CodigosMensajes.ErrorGenerico.ToString()),
                    status = (int)HttpStatusCode.InternalServerError
                }));
            }
        }

        // POST api/Cliente
        public Task<HttpResponseMessage> Post([FromBody]Cliente ciente)
        {
            try
            {
                ControladoraCliente controladoraCliente = new ControladoraCliente(unitOfWork);
                controladoraCliente.RegistrarCliente(ciente);
            }
            catch (Exception)
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.InternalServerError, new Respuesta<string>
                {
                    message = UtilidadesGenerico.LeerMensaje(CodigosMensajes.ErrorGenerico.ToString()),
                    status = (int)HttpStatusCode.OK
                }));
            }

            return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK, new Respuesta<string>
            {
                status = (int)HttpStatusCode.OK
            }));
        }

        // PUT api/Cliente/5
        public Task<HttpResponseMessage> Put(int id, [FromBody]Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ControladoraCliente ControladoraCliente = new ControladoraCliente(unitOfWork);
                    ControladoraCliente.EditarCliente(cliente);
                }
                else
                {
                    return Task.FromResult(Request.CreateResponse(HttpStatusCode.BadRequest, new Respuesta<string>
                    {
                        message = UtilidadesGenerico.LeerMensaje(CodigosMensajes.ErrorGenerico.ToString()),
                        status = (int)HttpStatusCode.BadRequest
                    }));
                }
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
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.InternalServerError, new Respuesta<string>
                {
                    message = UtilidadesGenerico.LeerMensaje(CodigosMensajes.ErrorGenerico.ToString()),
                    status = (int)HttpStatusCode.InternalServerError
                }));
            }

            return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK, new Respuesta<string>
            {
                status = (int)HttpStatusCode.OK
            }));
        }

        // DELETE api/Cliente/5
        public Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ControladoraCliente controladoraCliente = new ControladoraCliente(unitOfWork);
                    controladoraCliente.EliminarCliente(id);
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
