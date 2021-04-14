using MisGastos.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MisGastos.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISecurityRepository SecurityRepository { get; }
        IRepository<LogFrontend> LogFrontendRepository { get; }
        IGastoRepository GastoRepository { get; }
        IRepository<GastoTipo> GastoTipoRepository { get; }
        IRepository<Frecuencia> FrecuenciaRepository { get; }
        IRepository<Mes> MesRepository { get; }
        IGastoTipoDetalleRepository GastoTipoDetalleRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
