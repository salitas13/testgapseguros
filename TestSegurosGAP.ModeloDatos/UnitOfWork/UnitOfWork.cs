namespace TestSegurosGAP.ModeloDatos
{
    using System;
    using TestSegurosGAP.AccesoDatos.EFProvider.DataContext;
    using TestSegurosGAP.ModeloDatos.GenericRepository;
    using TestSegurosGAP.Entidades;

    public class UnitOfWork : IDisposable
    {
        private TestSegurosGAPEntities context = null;

        #region Repositories

        private GenericRepository<Cliente> clienteRepository;
        private GenericRepository<Poliza> polizaRepository;
        private GenericRepository<TipoCubrimiento> tipoCubrimientoRepository;
        private GenericRepository<TipoRiesgo> tipoRiesgoRepository;
        private GenericRepository<Usuario> usuarioRepository;

        #endregion Repositories

        public UnitOfWork()
        {
            context = new TestSegurosGAPEntities();
        }

        public GenericRepository<Cliente> ClienteRepository
        {
            get
            {

                if (this.clienteRepository == null)
                {
                    this.clienteRepository = new GenericRepository<Cliente>(context);
                }

                return clienteRepository;
            }
        }

        public GenericRepository<Poliza> PolizasRepository
        {
            get
            {
                if (this.polizaRepository == null)
                {
                    this.polizaRepository = new GenericRepository<Poliza>(context);
                }

                return polizaRepository;
            }
        }

        public GenericRepository<TipoCubrimiento> TipoCubrimientoRepository
        {
            get
            {
                if (this.tipoCubrimientoRepository == null)
                {
                    this.tipoCubrimientoRepository = new GenericRepository<TipoCubrimiento>(context);
                }

                return tipoCubrimientoRepository;
            }
        }

        public GenericRepository<TipoRiesgo> TipoRiesgoRepository
        {
            get
            {
                if (this.tipoRiesgoRepository == null)
                {
                    this.tipoRiesgoRepository = new GenericRepository<TipoRiesgo>(context);
                }

                return tipoRiesgoRepository;
            }
        }

        public GenericRepository<Usuario> UsuarioRepository
        {
            get
            {
                if (this.usuarioRepository == null)
                {
                    this.usuarioRepository = new GenericRepository<Usuario>(context);
                }

                return usuarioRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
