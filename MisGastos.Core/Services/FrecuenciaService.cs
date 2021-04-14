using AutoMapper;
using MisGastos.Core.DTOs;
using MisGastos.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MisGastos.Core.Services
{
    public class FrecuenciaService:IFrecuenciaService
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly PaginationOptions _paginationOptions;
        private readonly IMapper _mapper;
        public FrecuenciaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FrecuenciaDto>> GetAllAsync()
        {
            var frecuencias = await _unitOfWork.FrecuenciaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FrecuenciaDto>>(frecuencias);
        }
    }
}
