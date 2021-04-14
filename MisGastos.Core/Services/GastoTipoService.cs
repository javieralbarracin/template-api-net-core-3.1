using AutoMapper;
using MisGastos.Core.CustomEntities;
using MisGastos.Core.DTOs;
using MisGastos.Core.Entities;
using MisGastos.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MisGastos.Core.Services
{
    public class GastoTipoService : IGastoTipoService
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly PaginationOptions _paginationOptions;
        private readonly IMapper _mapper;
        public GastoTipoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;            
            _mapper = mapper;
        }

        public async Task<IEnumerable<GastoTipoDto>> GetAllAsync()
        {
            var gastoTipos = await _unitOfWork.GastoTipoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GastoTipoDto>>(gastoTipos);
        }
    }
}
