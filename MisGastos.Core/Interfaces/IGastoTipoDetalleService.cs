using MisGastos.Core.CustomEntities;
using MisGastos.Core.DTOs;
using MisGastos.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MisGastos.Core.Interfaces
{
    public interface IGastoTipoDetalleService
    {
        Task<IEnumerable<GastoTipoDetalleDto>> GetAllAsync();

        Task<IEnumerable<GastoTipoDetalleDto>> GetAllByIdGastoTipoAsync(int id);

        //Task InsertAsync(GastoTipo gastoTipo);

        //Task<bool> UpdateAsync(GastoTipo gastoTipo);

        //Task<bool> DeleteAsync(int id);
    }
}