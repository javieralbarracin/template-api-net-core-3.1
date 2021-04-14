using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisGastos.Api.Response;
using MisGastos.Core.DTOs;
using MisGastos.Core.Entities;
using MisGastos.Core.Enumerations;
using MisGastos.Core.Interfaces;
using MisGastos.Infrastructure.Interfaces;


namespace MisGastos.Api.Controllers
{
    [Authorize(Roles = nameof(RoleType.Administrator))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public SecurityController(ISecurityService securityService, IMapper mapper, IPasswordService passwordService)
        {
            _securityService = securityService;
            _mapper = mapper;
            _passwordService = passwordService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(SecurityDto securityDto)
        {
            var security = _mapper.Map<Security>(securityDto);

            security.Password = _passwordService.Hash(security.Password);
            await _securityService.RegisterUser(security);

            securityDto = _mapper.Map<SecurityDto>(security);
            var response = new ApiResponse<SecurityDto>(securityDto);
            return Ok(response);
        }

    }
}