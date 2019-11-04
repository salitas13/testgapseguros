namespace TestSegurosGAP.Negocio.Controladoras
{
    using System;
    using System.Linq;
    using TestSegurosGAP.Entidades.Solicitudes;
    using TestSegurosGAP.ModeloDatos;

    public class ControladoraUsuario
    {
        #region Private member variables.

        private readonly UnitOfWork _unitOfWork;

        #endregion

        #region Public constructor.

        /// <summary>
        /// Public constructor.
        /// </summary>
        public ControladoraUsuario(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public bool ValidarUsuario(LoginRequest login)
        {
            bool valido = false;
            // string passwordHash = Utilidades.PasswordHandler.CreatePasswordHash(login.Password);

            var usuario = _unitOfWork.UsuarioRepository.Find(t => t.UserName == login.UserName && t.PasswordHash == login.Password).FirstOrDefault();

            if (usuario != null)
            {
                usuario.UltimoLogin = DateTime.Now;
                _unitOfWork.UsuarioRepository.Edit(usuario);
                _unitOfWork.Save();

                valido = true;
            }

            return valido;
        }
    }
}
