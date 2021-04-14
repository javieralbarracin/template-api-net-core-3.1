using MisGastos.Core.CustomEntities;
using MisGastos.Core.DTOs;
using MisGastos.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MisGastos.Core.Interfaces
{
    public interface IGastoTipoService
    {
        Task<IEnumerable<GastoTipoDto>> GetAllAsync();

        //Task<GastoTipo> GetById(int id);

        //Task InsertAsync(GastoTipo gastoTipo);

        //Task<bool> UpdateAsync(GastoTipo gastoTipo);

        //Task<bool> DeleteAsync(int id);
    }
}