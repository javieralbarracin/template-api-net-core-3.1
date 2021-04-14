using AutoMapper;
using Microsoft.Extensions.Options;
using MisGastos.Core.CustomEntities;
using MisGastos.Core.DTOs;
using MisGastos.Core.Entities;
using MisGastos.Core.Exceptions;
using MisGastos.Core.Interfaces;
using MisGastos.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisGastos.Core.Services
{
    public class GastoService : IGastoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IMapper _mapper;
        public GastoService(IUnitOfWork unitOfWork, IMapper mapper, 
                            IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _mapper = mapper;
        }

        public async Task<PagedList<Gasto>> GastosStoredProcedureAsync(GastoQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            
            var gastos = await _unitOfWork.GastoRepository.GastosStoredProcedureAsync(1, 1);
            
            if (filters.Description != null)
            {
                gastos = gastos.Where(x => x.Descripcion.ToLower().Contains(filters.Description.ToLower()));
            }

            var pagedGastos = PagedList<Gasto>.Create(gastos, filters.PageNumber, filters.PageSize);

            return pagedGastos;
        }
        public async Task<IEnumerable<GastoDto>> GetGastosAsync(GastoQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var gastos = await _unitOfWork.GastoRepository.GastosRelashionShipAsync();

            if (filters.Description != null)
            {
                gastos = gastos.Where(x => x.Descripcion.ToLower().Contains(filters.Description.ToLower()));
            }

            var gastosDto = _mapper.Map<IEnumerable<GastoDto>>(gastos);

            return gastosDto;
        }
        public async Task<PagedList<Gasto>> GastosAsync(GastoQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var gastos = await _unitOfWork.GastoRepository.GastosRelashionShipAsync();

            if (filters.Description != null)
            {
                gastos = gastos.Where(x => x.Descripcion.ToLower().Contains(filters.Description.ToLower()));
            }

            var pagedGastos = PagedList<Gasto>.Create(gastos, filters.PageNumber, filters.PageSize);

            return pagedGastos;
            
        }
        public async Task<PagedList<Gasto>> GetGastos(GastoQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var gastos = await _unitOfWork.GastoRepository.GetAllAsync();
            
            //if (filters.UserId != null)
            //{
            //    gastos = gastos.Where(x => x.Id == filters.UserId);
            //}

            //if (filters.Date != null)
            //{
            //    gastos = gastos.Where(x => x.FechaVencimiento.ToShortDateString() == filters.Date?.ToShortDateString());
            //}

            if (filters.Description != null)
            {
                gastos = gastos.Where(x => x.Descripcion.ToLower().Contains(filters.Description.ToLower()));
            }

            var pagedGastos = PagedList<Gasto>.Create(gastos, filters.PageNumber, filters.PageSize);

            return pagedGastos;
        }
        // Obtiene los gastos por creditos
        public async Task<IEnumerable<GastoDetalleDto>> GastosCreditosAllAsync()
        {
            var gastos = await _unitOfWork
                                .GastoRepository
                                .GastosCreditosAllAsync();
            return _mapper.Map<IEnumerable<GastoDetalleDto>>(gastos);
        }
        public async Task<IEnumerable<GastoDto>> GastosFijosAllAsync()
        {
            var gastos = await _unitOfWork
                            .GastoRepository
                            .GastosCreditosAllAsync();
            return _mapper.Map<IEnumerable<GastoDto>>(gastos);
        }
        public IEnumerable<GastoDto> GetGastosDto(PagedList<Gasto> pagedPosts)
        {
            return _mapper.Map<IEnumerable<GastoDto>>(pagedPosts);
        }
        public async Task<GastoCreacionDto> GetGasto(int id)
        {
            var gasto = await _unitOfWork.GastoRepository.GetByIdAsync(id);
            var gastoDto = _mapper.Map<GastoCreacionDto>(gasto);
            return gastoDto;
        }
        public async Task<GastoDto> InsertGasto(GastoCreacionDto gastoCreacionDto)
        {
            if (gastoCreacionDto.Descripcion.Contains("Sexo") || gastoCreacionDto.Descripcion.Contains("sexo"))
            {
                throw new BusinessException("Contenido no permitido en la descripcion");
                //throw new ApiException("Contenido no permitido");
            }
            var gasto = _mapper.Map<Gasto>(gastoCreacionDto);
            gasto.Activo = true;
            //gasto.ImporteTotal = gasto.GastosCreditos.Sum(x => x.Importe);
            await _unitOfWork.GastoRepository.Add(gasto);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<GastoDto>(gasto);

            //var user = await _unitOfWork.UserRepository.GetById(gasto.UserId);
            //if (user == null)
            //{
            //    throw new BusinessException("User doesn't exist");
            //}

            //var userGasto = await _unitOfWork.GastoRepository.GetGastosByUser(gasto.UserId);
            //Regla de validacion
            //if (userGasto.Count() < 10)
            //{
            //    var lastGasto = userGasto.OrderByDescending(x => x.FechaCreacion).FirstOrDefault();
            //    if ((DateTime.Now - lastGasto.FechaCreacion).TotalDays < 7)
            //    {
            //        throw new BusinessException("No puedes publicar la publicación");
            //    }
            //}

        }
        public async Task<bool> UpdateGasto(int id, GastoCreacionDto gastoEdicionDto)
        {
            //if (gastoEdicionDto.Descripcion.IndexOf("sexo", StringComparison.CurrentCultureIgnoreCase) >= 0)
            if (gastoEdicionDto.Descripcion.Contains("Sexo")|| gastoEdicionDto.Descripcion.Contains("sexo"))
            {
                //throw new AppApiException("Contenido no permitido en la descripcion");
                throw new BusinessException("Contenido no permitido en la descripcion");
            }
            var existingGasto = await _unitOfWork.GastoRepository.GetByIdAsync(id);
            if (existingGasto==null)
            {
                throw new BusinessException("No se encuentra la entidad a modificar");
            }
            var gasto = _mapper.Map<Gasto>(gastoEdicionDto);
            //existingGasto.ImporteTotal = gasto.GastosCreditos.Sum(x=>x.Importe);
            existingGasto.Descripcion = gasto.Descripcion;
            existingGasto.FrecuenciaId = gasto.FrecuenciaId;
            existingGasto.GastoTipoId = gasto.GastoTipoId;
            existingGasto.MesId = gasto.MesId;
            _unitOfWork.GastoRepository.Update(existingGasto);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteGasto(int id)
        {
            await _unitOfWork.GastoRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}

