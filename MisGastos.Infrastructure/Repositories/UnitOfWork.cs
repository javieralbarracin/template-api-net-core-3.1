using MisGastos.Core.Entities;
using MisGastos.Core.Interfaces;
using MisGastos.Infrastructure.Context;
using System.Threading.Tasks;

namespace MisGastos.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MisGastosContext _context;
        private readonly ISecurityRepository _securityRepository;
        private readonly IGastoRepository _gastoRepository;
        private readonly IRepository<GastoTipo> _gastoTipoRepository;
        private readonly IRepository<Frecuencia> _frecuenciaRepository;
        private readonly IRepository<LogFrontend> _logFrontRepository;
        private readonly IRepository<Mes> _mesRepository;
        private readonly IGastoTipoDetalleRepository _gastoTipoDetalleRepository;

        public UnitOfWork(MisGastosContext context)
        {
            _context = context;
        }
        //public IPostRepository PostRepository => _postRepository ?? new PostRepository(_context);
        public ISecurityRepository SecurityRepository => _securityRepository ?? new SecurityRepository(_context);
        public IGastoRepository GastoRepository => _gastoRepository ?? new GastoRepository(_context);

        public IRepository<GastoTipo> GastoTipoRepository => _gastoTipoRepository ?? new BaseRepository<GastoTipo>(_context);
        public IRepository<Frecuencia> FrecuenciaRepository => _frecuenciaRepository ?? new BaseRepository<Frecuencia>(_context);
        public IRepository<LogFrontend> LogFrontendRepository => _logFrontRepository ?? new BaseRepository<LogFrontend>(_context);
        public IRepository<Mes> MesRepository => _mesRepository ?? new BaseRepository<Mes>(_context);
        public IGastoTipoDetalleRepository GastoTipoDetalleRepository => _gastoTipoDetalleRepository ?? new GastoTipoDetalleRepository(_context);


        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
