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
    //[Authorize]
    [Produces("application/json")]
    [ApiController, ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MesController : ControllerBase
    {
        private readonly IMesService _mesServices;

        public MesController(IMesService mesServices)
        {
            _mesServices = mesServices;
        }
        [HttpGet(Name = "Meses")]
        public async Task<IActionResult> Meses()
            => Ok(new ApiResponse<IEnumerable<MesDto>>(await _mesServices.GetAllAsync()));
    }
}
