using MisGastos.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MisGastos.Core.Interfaces
{
    public interface IGastoTipoDetalleRepository : IRepository<GastoTipoDetalle>
    {
        Task<IEnumerable<GastoTipoDetalle>> GetAllByIdGastoTipoAsync(int id);        
    }
}