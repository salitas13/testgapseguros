namespace TestSegurosGAP.Negocio.Controladoras
{
    using System.Collections.Generic;
    using TestSegurosGAP.Entidades;
    using TestSegurosGAP.ModeloDatos;
    using System.Linq;
    using TestSegurosGAP.Entidades.Respuestas;

    public class ControladoraCliente
    {
        #region Private member variables.

        private readonly UnitOfWork _unitOfWork;

        #endregion

        #region Public constructor.

        /// <summary>
        /// Public constructor.
        /// </summary>
        public ControladoraCliente(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public List<RespuestaCliente> ObtenerClientes()
        {
            var cientes = _unitOfWork.ClienteRepository.GetAll();

            return cientes.Select(t => new RespuestaCliente
            {
                IdCliente = t.IdCliente,
                Nombres = t.Nombres,
                Apellidos = t.Apellidos,
                FechaNacimiento = t.FechaNacimiento,
                Cedula = t.Cedula
            }).ToList();
        }

        public RespuestaCliente ObtenerCliente(int id)
        {
            var cliente = _unitOfWork.ClienteRepository.Find(id);

            return new RespuestaCliente
            {
                IdCliente = cliente.IdCliente,
                Nombres = cliente.Nombres,
                Apellidos = cliente.Apellidos,
                FechaNacimiento = cliente.FechaNacimiento,
                Cedula = cliente.Cedula
            };
        }

        public void RegistrarCliente(Cliente cliente)
        {
            _unitOfWork.ClienteRepository.Add(cliente);
            _unitOfWork.Save();
        }

        public void EditarCliente(Cliente cliente)
        {
            _unitOfWork.ClienteRepository.Edit(cliente);
            _unitOfWork.Save();
        }

        public void EliminarCliente(int id)
        {
            _unitOfWork.ClienteRepository.Delete(id);
            _unitOfWork.Save();
        }
    }
}
