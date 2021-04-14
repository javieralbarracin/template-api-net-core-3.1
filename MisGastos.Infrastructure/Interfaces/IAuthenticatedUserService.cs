using System;
using System.Collections.Generic;
using System.Text;

namespace MisGastos.Infrastructure.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
    }
}
