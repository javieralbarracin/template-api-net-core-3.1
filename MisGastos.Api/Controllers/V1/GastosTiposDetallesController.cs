using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisGastos.Api.Response;
using MisGastos.Core.DTOs;
using MisGastos.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisGastos.Api.Controllers.V1
{
    [Authorize]
    [Produces("application/json")]
    [ApiController, ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GastosTiposDetallesController : ControllerBase
    {
        private readonly IGastoTipoDetalleService _gastoTipoDetalleService;

        public GastosTiposDetallesController(IGastoTipoDetalleService gastoTipoDetalleService)
        {
            this._gastoTipoDetalleService = gastoTipoDetalleService;
        }
        [HttpGet()]
        public async Task<IActionResult> GastosTiposDestalles() =>
            Ok(new ApiResponse<IEnumerable<GastoTipoDetalleDto>>(await _gastoTipoDetalleService.GetAllAsync()));
       
        [HttpGet("{id:int}", Name = "GetAllByIdGastoTipo")]
        public async Task<IActionResult> GetAllByIdGastoTipo(int id) =>
            Ok(new ApiResponse<IEnumerable<GastoTipoDetalleDto>>(
                await _gastoTipoDetalleService.GetAllByIdGastoTipoAsync(id)));
    }
}
