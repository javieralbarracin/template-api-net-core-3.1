using MisGastos.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MisGastos.Core.Interfaces
{
    public interface IFrecuenciaService
    {
        Task<IEnumerable<FrecuenciaDto>> GetAllAsync();

        //Task<GastoTipo> GetById(int id);

        //Task InsertAsync(GastoTipo gastoTipo);

        //Task<bool> UpdateAsync(GastoTipo gastoTipo);

        //Task<bool> DeleteAsync(int id);
    }
}
