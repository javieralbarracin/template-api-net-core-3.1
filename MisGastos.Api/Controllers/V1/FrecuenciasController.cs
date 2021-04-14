using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisGastos.Api.Response;
using MisGastos.Core.DTOs;
using MisGastos.Core.Entities;
using MisGastos.Core.Interfaces;
using MisGastos.Infrastructure.Interfaces;
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
    public class FrecuenciasController : ControllerBase
    {
        private readonly IFrecuenciaService _repository;

        public FrecuenciasController(IFrecuenciaService repository)
        {
            this._repository = repository;
        }
        /// <summary>
        /// Retrieve all gastos tipos
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        //[Route("GastosFilter")]
        [HttpGet()]
        public async Task<IActionResult> Frecuencias() =>
            Ok(new ApiResponse<IEnumerable<FrecuenciaDto>>(await _repository.GetAllAsync()));
    }
}

