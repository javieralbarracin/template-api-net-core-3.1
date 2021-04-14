using Microsoft.AspNetCore.Http;
using MisGastos.Infrastructure.Interfaces;
using System.Security.Claims;

namespace MisGastos.Api.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
        }

        public string UserId { get; }
    }
}
