using Microsoft.EntityFrameworkCore;
using MisGastos.Core.Entities;
using MisGastos.Core.Interfaces;
using MisGastos.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisGastos.Infrastructure.Repositories
{
    public class GastoTipoDetalleRepository : BaseRepository<GastoTipoDetalle>, IGastoTipoDetalleRepository
    {
        public GastoTipoDetalleRepository(MisGastosContext context) : base(context) { }

        public async Task<IEnumerable<GastoTipoDetalle>> GetAllByIdGastoTipoAsync(int id)
        {
            return await _entities.Where(x => x.GastoTipoId==id)
                            .ToListAsync();
        }
    }
}
