namespace TestSegurosGAP.Servicios.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using TestSegurosGAP.Entidades;
    using TestSegurosGAP.Entidades.Respuestas;
    using TestSegurosGAP.Entidades.Solicitudes;
    using TestSegurosGAP.ModeloDatos;
    using TestSegurosGAP.Negocio.Controladoras;

    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("authenticate")]
        public Respuesta<string> Authenticate(LoginRequest login)
        {
            if (!ModelState.IsValid || login == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ControladoraUsuario controladoraUsuario = new ControladoraUsuario(unitOfWork);
            
            bool isCredentialValid = controladoraUsuario.ValidarUsuario(login);
            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(login.UserName);

                return new Respuesta<string>
                {
                    result = token,
                    status = (int)HttpStatusCode.OK
                };
            }
            else
            {
                return new Respuesta<string>
                {
                    status = (int)HttpStatusCode.Unauthorized
                };
            }
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
