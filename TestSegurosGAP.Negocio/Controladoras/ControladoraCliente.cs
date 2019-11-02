namespace TestSegurosGAP.Negocio.Controladoras
{
    using System.Collections.Generic;
    using TestSegurosGAP.Entidades;
    using TestSegurosGAP.ModeloDatos;
    using System.Linq;

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

        public List<Cliente> ObtenerClientes()
        {
            return _unitOfWork.ClienteRepository.GetAll().ToList();
        }

        public Cliente ObtenerCliente(int id)
        {
            return _unitOfWork.ClienteRepository.Find(id);
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
