using MisGastos.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MisGastos.Core.Interfaces
{
    public interface IGastoRepository : IRepository<Gasto>
    {
        //Task<IEnumerable<Gasto>> GetGastosByUser(int userId);
        Task<IEnumerable<Gasto>> GastosRelashionShipAsync();
        Task<IEnumerable<Gasto>> GastosCreditosAllAsync();
        //Task<IEnumerable<Gasto>> GastosFijosAllAsync();
        Task<Gasto> GetByIdAsync(int id);
        Task<IEnumerable<Gasto>> GastosStoredProcedureAsync(int mes, int frecuencia);

    }
}