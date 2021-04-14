using MisGastos.Core.Entities;
using MisGastos.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MisGastos.Core.Services
{
    public class LogFrontendService:ILogFrontendService
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly PaginationOptions _paginationOptions;
        //private readonly IMapper _mapper;
        public LogFrontendService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<LogFrontend>> GetAllAsync()
        {
            return await _unitOfWork.LogFrontendRepository.GetAllAsync();
        }        

        public async Task<LogFrontend> AddAsync(LogFrontend logCreacionDto)
        {
            await _unitOfWork.LogFrontendRepository.Add(logCreacionDto);
            await _unitOfWork.SaveChangesAsync();
            return logCreacionDto;

        }
    }
}
