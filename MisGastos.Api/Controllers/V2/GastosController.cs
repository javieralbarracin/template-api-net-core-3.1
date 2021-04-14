using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MisGastos.Api.Response;
using MisGastos.Core.DTOs;
using MisGastos.Core.Entities;
using MisGastos.Core.Interfaces;
using MisGastos.Core.QueryFilters;
using MisGastos.Infrastructure.Interfaces;
using Newtonsoft.Json;

namespace MisGastos.Api.Controllers.V2
{
    //[Authorize]
    //[Produces("application/json")]
    [ApiVersion("2.0")]
    //[Route("api/v2/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class GastosController : ControllerBase
    {
        private readonly IGastoService _gastoService;
        private readonly IUriService _uriService;
        
        public GastosController(IGastoService gastoService, IUriService uriService)
        {
            _gastoService = gastoService;
            _uriService = uriService;            
        }
        /// <summary>
        /// Retrieve all gastos
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        //[Route("GastosFilter")]
        //[MapToApiVersion("2.0")]
        [HttpGet(Name = "ListWithFilter")]
        public async Task<IActionResult> ListWithFilter([FromQuery] GastoQueryFilter filters)
        {
            var gastos = await _gastoService.GastosAsync(filters);
            var metadata = new Metadata
            {
                TotalCount = gastos.TotalCount,
                PageSize = gastos.PageSize,
                CurrentPage = gastos.CurrentPage,
                TotalPages = gastos.TotalPages,
                HasNextPage = gastos.HasNextPage,
                HasPreviousPage = gastos.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(ListWithFilter))).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(ListWithFilter))).ToString()
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(new ApiResponse<IEnumerable<GastoDto>>(_gastoService.GetGastosDto(gastos), metadata));
        }
        /// <summary>
        /// Obtiene un gasto por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[MapToApiVersion("2.0")]
        //[HttpGet(Name = "Gasto")]
        //public async Task<IActionResult> Gasto(int id)
        //{
        //    var gastoDto = await _gastoService.GetGasto(id);
        //    return Ok(new ApiResponse<GastoDto>(gastoDto));
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetPost(int id)
        //{
        //    var gasto = await _gastoService.GetGasto(id);
        //    var gastoDto = _mapper.Map<GastoDto>(gasto);
        //    var response = new ApiResponse<GastoDto>(gastoDto);
        //    return Ok(response);
        //}
    }
}

