using MisGastos.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MisGastos.Core.Interfaces
{
    public interface ILogFrontendService
    {
        Task<IEnumerable<LogFrontend>> GetAllAsync();
        Task<LogFrontend> AddAsync(LogFrontend logCreacionDto);
    }
}
