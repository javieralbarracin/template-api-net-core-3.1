using MisGastos.Core.CustomEntities;
using MisGastos.Core.DTOs;
using MisGastos.Core.Entities;
using MisGastos.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MisGastos.Core.Interfaces
{
    public interface IGastoService
    {
        Task<IEnumerable<GastoDto>> GetGastosAsync(GastoQueryFilter filters);
        Task<PagedList<Gasto>> GetGastos(GastoQueryFilter filters);
        Task<PagedList<Gasto>> GastosAsync(GastoQueryFilter filters);
        Task<PagedList<Gasto>> GastosStoredProcedureAsync(GastoQueryFilter filters);
        Task<IEnumerable<GastoDetalleDto>> GastosCreditosAllAsync();
        Task<IEnumerable<GastoDto>> GastosFijosAllAsync();
        IEnumerable<GastoDto> GetGastosDto(PagedList<Gasto> pagedPosts);
        Task<GastoCreacionDto> GetGasto(int id);
        Task<GastoDto> InsertGasto(GastoCreacionDto gastoDto);
        Task<bool> UpdateGasto(int id, GastoCreacionDto gastoDto);
        Task<bool> DeleteGasto(int id);
    }
}
