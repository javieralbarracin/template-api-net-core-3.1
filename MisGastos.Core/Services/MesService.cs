using AutoMapper;
using MisGastos.Core.DTOs;
using MisGastos.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MisGastos.Core.Services
{
    public class MesService : IMesService
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly PaginationOptions _paginationOptions;
        private readonly IMapper _mapper;
        public MesService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MesDto>> GetAllAsync()
        {
            var meses = await _unitOfWork.MesRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MesDto>>(meses);
        }
    }
}
