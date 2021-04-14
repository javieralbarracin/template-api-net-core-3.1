using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MisGastos.Core.Entities;
using MisGastos.Core.Interfaces;
using MisGastos.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisGastos.Infrastructure.Repositories
{
    public class GastoRepository : BaseRepository<Gasto>, IGastoRepository
    {
        public GastoRepository(MisGastosContext context) : base(context) { }

        //public async Task<IEnumerable<Gasto>> GetGastosByUser(int userId)
        //{
        //    return _entities.Where(x => x.UserId == userId).ToListAsync();
        //}
        public async Task<IEnumerable<Gasto>> GastosRelashionShipAsync()
        {
            return await _entities
                            .Include(x=>x.Frecuencia)
                            .Include(x=>x.GastoTipo)
                            .Include(x=>x.Mes)
                            .Where(x=>x.Activo)
                            .ToListAsync();
        }
        // Obtiene los gastos por creditos
        public async Task<IEnumerable<Gasto>> GastosCreditosAllAsync()
        {
            //IQueryable<Gasto> linqQuery = from b in _entities select b;
            //List<Gasto> gastos = linqQuery.ToList();
            var creditos = await _entities
                            .Include(x => x.Frecuencia)
                            .Include(x => x.GastoTipo)
                            .Include(x => x.Mes)
                            .Include(x=>x.GastosCreditos)
                            .Where(x => x.Activo)
                            .ToListAsync();
            return creditos;
            //return gastos;
        }
        //public async Task<IEnumerable<Gasto>> GastosFijosAllAsync()
        //{
        //    return await _entities
        //                    .OfType<GastoFijo>()
        //                    .Include(x=>x.Frecuencia)
        //                    .Include(x=>x.GastoTipo)
        //                    .Include(x=>x.Mes)
        //                    .Where(x=>x.Activo)
        //                    .ToListAsync();
        //}
        public async Task<Gasto> GetByIdAsync(int id)
        {
            return await _entities
                            .Include(x=>x.Frecuencia)
                            .Include(x=>x.GastoTipo)
                            .Include(x=>x.Mes)
                            .Where(x=>x.Activo && x.Id==id)
                            .SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<Gasto>> GastosStoredProcedureAsync(int mes,int frecuencia)
        {
            var mesParam = new SqlParameter("@mes", mes);
            var frecuenciaParam = new SqlParameter("@frecuencia", frecuencia);

            return await _entities
                            .FromSqlRaw("exec sp_GetGastosDetalles @mes, @frecuencia", mesParam, frecuenciaParam)
                            .ToListAsync();
        }
        //public async Task<IEnumerable<Gasto>> GastosStoredProcedureAsync()
        //{
        //    using (SqlConnection sql = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("GetAllValues", sql))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            var response = new List<Gasto>();
        //            await sql.OpenAsync();

        //            using (var reader = await cmd.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    response.Add(MapToValue(reader));
        //                }
        //            }

        //            return response;
        //        }
        //    }
        //}
        //private Gasto MapToValue(SqlDataReader reader)
        //{
        //    return new Gasto()
        //    {
        //        Id = (int)reader["Id"],
        //        Descripcion = (int)reader["Value1"],
        //        Frecuencia = reader["Value2"].
        //    };
        //}
    }
}
