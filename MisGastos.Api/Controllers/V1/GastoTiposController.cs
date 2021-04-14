using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisGastos.Api.Response;
using MisGastos.Core.DTOs;
using MisGastos.Core.Interfaces;
using MisGastos.Infrastructure.Interfaces;
using MisGastos.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MisGastos.Api.Controllers.V1
{
    [Authorize]
    [Produces("application/json")]
    [ApiController, ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GastoTiposController : ControllerBase
    {
        private readonly IGastoTipoService _gastoTipoService;

        public GastoTiposController(IGastoTipoService gastoTipoService)
        {
            this._gastoTipoService = gastoTipoService;
        }
        /// <summary>
        /// Retrieve all gastos tipos
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        //[Route("GastosFilter")]
        [HttpGet()]
        public async Task<IActionResult> GastosTipos() =>
            Ok(new ApiResponse<IEnumerable<GastoTipoDto>>(await _gastoTipoService.GetAllAsync()));
    }
}
