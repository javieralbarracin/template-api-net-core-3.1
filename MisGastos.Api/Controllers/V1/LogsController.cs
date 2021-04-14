using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisGastos.Api.Response;
using MisGastos.Core.Entities;
using MisGastos.Core.Interfaces;
using MisGastos.Core.Services;
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
    public class LogsController : ControllerBase
    {
        private readonly ILogFrontendService _logService;

        public LogsController(ILogFrontendService logServices)
        {
            _logService = logServices;
        }
        [HttpGet(Name = "Logs")]
        public async Task<IActionResult> Logs() 
            => Ok(new ApiResponse<IEnumerable<LogFrontend>>(await _logService.GetAllAsync()));

        [HttpPost]
        public async Task<IActionResult> Post(LogFrontend log)
            => Ok(new ApiResponse<LogFrontend>(await _logService.AddAsync(log)));
    }
}
