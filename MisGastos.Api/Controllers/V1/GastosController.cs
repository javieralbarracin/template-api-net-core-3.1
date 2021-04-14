using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MisGastos.Api.Response;
using MisGastos.Core.DTOs;
using MisGastos.Core.Entities;
using MisGastos.Core.Interfaces;
using MisGastos.Core.QueryFilters;
using MisGastos.Infrastructure.Interfaces;
using Newtonsoft.Json;

namespace MisGastos.Api.Controllers.V1
{
    [Authorize]
    //[EnableCors("AppCORSPolicy")]
    //[Produces("application/json")] //especific type data response
    [ApiVersion("1.0", Deprecated = true)]
    //[ApiVersion("2.0")]
    //[Route("api/v1/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]    
    [ApiController]
    public class GastosController : ControllerBase
    {
        private readonly IGastoService _gastoService;        
      
        public GastosController(IGastoService gastoService)
        {
            _gastoService = gastoService;      
        }
        /// <summary>
        /// Retrieve all gastos
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        //[Route("GastosFilter")]
        [MapToApiVersion("1.0")]
        [HttpGet(Name = "ListWithFilter")]
        public async Task<IActionResult> ListWithFilter([FromQuery] GastoQueryFilter filters)
        {
            var gastos = await _gastoService.GetGastosAsync(filters);
            return Ok(new ApiResponse<IEnumerable<GastoDto>>(gastos));
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("ListCredits")]
        public async Task<IActionResult> ListCredits()
            =>  Ok(new ApiResponse<IEnumerable<GastoDetalleDto>>(
                    await _gastoService.GastosCreditosAllAsync()));
        
        /// <summary>
        /// Retrieve one gastos
        /// </summary>
        /// <param name="id">Search by id</param>
        /// <returns></returns>
        //[MapToApiVersion("1.0")]
        [HttpGet("{id:int}", Name = "GastoById")]
        public async Task<IActionResult> GastoById(int id)
          => Ok(new ApiResponse<GastoCreacionDto>(await _gastoService.GetGasto(id)));
        
        [HttpPost]
        public async Task<IActionResult> Post(GastoCreacionDto gastoCreacionDto)
            => Ok(new ApiResponse<GastoDto>(await _gastoService.InsertGasto(gastoCreacionDto)));
       
        [HttpPut("{id}")]
        //[HttpOptions]
        public async Task<IActionResult> Put(int id, GastoCreacionDto gastoDto)
            => Ok(new ApiResponse<bool>(await _gastoService.UpdateGasto(id, gastoDto)));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
            => Ok(new ApiResponse<bool>(await _gastoService.DeleteGasto(id)));

        //[HttpGet(Name = nameof(GetGastos))]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<GastoDto>>))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public IActionResult GetGastos([FromQuery] GastoQueryFilter filters)
        //{
        //    var gastos = _gastoService.GetGastos(filters);
        //    var metadata = new Metadata
        //    {
        //        TotalCount = gastos.TotalCount,
        //        PageSize = gastos.PageSize,
        //        CurrentPage = gastos.CurrentPage,
        //        TotalPages = gastos.TotalPages,
        //        HasNextPage = gastos.HasNextPage,
        //        HasPreviousPage = gastos.HasPreviousPage,
        //        NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetGastos))).ToString(),
        //        PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetGastos))).ToString()
        //    };

        //    var response = new ApiResponse<IEnumerable<GastoDto>>(_gastoService.GetGastosDto(gastos))
        //    {
        //        Meta = metadata
        //    };
        //    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

        //    return Ok(response);
        //}
        //[HttpGet("{id}", Name = "Gasto")]
        //public async Task<ActionResult<GastoDto>> Gast(int id)
        //{
        //    var gastoDto = await _gastoService.GetGasto(id);
        //    var response = new ApiResponse<GastoDto>(gastoDto);
        //    return Ok(response);

        //    //var pelicula = await context.Peliculas
        //    //    .Include(x => x.PeliculasActores).ThenInclude(x => x.Actor)
        //    //    .Include(x => x.PeliculasGeneros).ThenInclude(x => x.Genero)
        //    //    .FirstOrDefaultAsync(x => x.Id == id);

        //    //if (pelicula == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //pelicula.PeliculasActores = pelicula.PeliculasActores.OrderBy(x => x.Orden).ToList();

        //    //return mapper.Map<PeliculaDetallesDTO>(pelicula);

        //}

    }
}

