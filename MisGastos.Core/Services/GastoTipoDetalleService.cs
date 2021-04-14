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
    public class GastoTipoDetalleService : IGastoTipoDetalleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GastoTipoDetalleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;            
            _mapper = mapper;
        }

        public async Task<IEnumerable<GastoTipoDetalleDto>> GetAllAsync()
        {
            var gastoTipos = await _unitOfWork.GastoTipoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GastoTipoDetalleDto>>(gastoTipos);
        }
        public async Task<IEnumerable<GastoTipoDetalleDto>> GetAllByIdGastoTipoAsync(int id)
        {
            var gastoTipos = await _unitOfWork.GastoTipoDetalleRepository.GetAllByIdGastoTipoAsync(id);
            return _mapper.Map<IEnumerable<GastoTipoDetalleDto>>(gastoTipos);
        }
    }
}
