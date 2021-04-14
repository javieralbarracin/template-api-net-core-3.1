using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MisGastos.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MisGastos.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;
        public GlobalExceptionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory
                     .CreateLogger<GlobalExceptionFilter>();
        }
        public void OnException(ExceptionContext context)
        {
            int status = 0;
            string title = string.Empty;
            string detail = string.Empty;
            /*** 1 ****/
            if (context.Exception.GetType() == typeof(BusinessException))
            {
                var exception = (BusinessException)context.Exception;
                status = 400;
                title = "Bad Request";
                detail = exception.Message;
                _logger.Log(LogLevel.Warning, title, $"[{status}]: {detail}");
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                title = "Unauthorized Access";
                status = 401;
                // handle logging here
                _logger.Log(LogLevel.Warning, title, $"[{status}]: {detail}");
            }
            else
            {
                // Unhandled errors
                //#if !DEBUG
                //var msg = "An unhandled error occurred.";                
                //string stack = null;
                //#else
                //var msg = context.Exception.GetBaseException().Message;
                //string stack = context.Exception.StackTrace;
                //#endif
                title = this.StatusCodeMessage(status);
                detail = context.Exception.StackTrace;
                _logger.Log(LogLevel.Error, title, $"[{status}]: {detail}");
            }

            /*** 2 ****/
            var validation = new
            {
                Status = status,
                Title = title,
                Detail = detail
            };

            var json = new
            {
                errors = new[] { validation }
            };
            context.Result = new BadRequestObjectResult(json);
            context.ExceptionHandled = true;            
        }
        private String StatusCodeMessage(int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                    return "Bad request.";
                case 401:
                    return "Unauthorized access.";
                case 402:
                    return "Payment required.";
                case 403:
                    return "Forbidden access.";
                case 404:
                    return "Resource not found.";
                case 405:
                    return "Method not allowed.";
                case 406:
                    return "Not acceptable.";
                case 407:
                    return "Proxy authentication required.";
                case 408:
                    return "Request timeout.";
                case 409:
                    return "Conflict";
                case 410:
                    return "Resource is gone.";
                case 411:
                    return "Length is required.";
                case 500:
                    return "Internal server error.";
                case 501:
                    return "Not implemented.";
                case 502:
                    return "Bad gateway.";
                case 503:
                    return "Service unavailable.";
                case 504:
                    return "Gateway timeout.";
                case 505:
                    return "HTTP version not supported.";
            }
            return "";
        }
    }
}
