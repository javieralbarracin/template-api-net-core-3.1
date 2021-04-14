using MisGastos.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisGastos.Infrastructure.Interfaces
{
    public interface IUriService
    {
        Uri GetPostPaginationUri(GastoQueryFilter filter, string actionUrl);
    }
}
