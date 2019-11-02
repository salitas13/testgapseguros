namespace TestSegurosGAP.Servicios.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using TestSegurosGAP.Entidades;
    using TestSegurosGAP.Entidades.Enumeradores;
    using TestSegurosGAP.Entidades.Excepciones;
    using TestSegurosGAP.ModeloDatos;
    using TestSegurosGAP.Negocio.Controladoras;
    using TestSegurosGAP.Utilidades;

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
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.BadRequest, ControladoraCliente.ObtenerClientes()));
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
                if (ModelState.IsValid)
                {
                    ControladoraCliente controladoraCliente = new ControladoraCliente(unitOfWork);
                    return Task.FromResult(Request.CreateResponse(HttpStatusCode.BadRequest, controladoraCliente.ObtenerCliente(id)));
                }
                else
                {
                    return Task.FromResult(Request.CreateResponse(HttpStatusCode.BadRequest, UtilidadesGenerico.LeerMensaje(CodigosMensajes.ErrorGenerico.ToString())));
                }
            }
            catch (Exception)
            {
                return Task.FromResult<HttpResponseMessage>(Request.CreateResponse(HttpStatusCode.InternalServerError, UtilidadesGenerico.LeerMensaje(CodigosMensajes.ErrorGenerico.ToString())));
            }
        }

        // POST api/Cliente
        public Task<HttpResponseMessage> Post([FromBody]Cliente ciente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ControladoraCliente controladoraCliente = new ControladoraCliente(unitOfWork);
                    controladoraCliente.RegistrarCliente(ciente);
                }
                else
                {
                    return Task.FromResult(Request.CreateResponse(HttpStatusCode.BadRequest, UtilidadesGenerico.LeerMensaje(CodigosMensajes.ErrorGenerico.ToString())));
                }
            }
            catch (Exception)
            {
                return Task.FromResult<HttpResponseMessage>(Request.CreateResponse(HttpStatusCode.InternalServerError, UtilidadesGenerico.LeerMensaje(CodigosMensajes.ErrorGenerico.ToString())));
            }

            return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK));
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
