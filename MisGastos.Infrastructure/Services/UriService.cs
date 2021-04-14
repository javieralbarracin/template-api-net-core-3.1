using MisGastos.Core.QueryFilters;
using MisGastos.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisGastos.Infrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPostPaginationUri(GastoQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}
