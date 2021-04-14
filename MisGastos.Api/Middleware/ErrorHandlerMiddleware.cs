using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using MisGastos.Api.Response;
using MisGastos.Core.Exceptions;
using System.Net;
using MisGastos.Infrastructure.Validators;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;

namespace MisGastos.Api.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
            //_logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new ApiResponse<string>() { isSuccess = false, message = error?.Message };

                switch (error)
                {
                    case BusinessException e:
                        // custom application error
                        //types logs:
                        //Log.Information("Log: Log.Information");
                        //Log.Warning("Log: Log.Warning");
                        //Log.Error("Log: Log.Error");
                        //Log.Fatal("Log: Log.Fatal");
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        Log.Information(e, $"[{response.StatusCode}]: {e?.Message}");
                        break;
                    case ApiException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        Log.Warning(e, $"[{response.StatusCode}]: {e?.Message}");
                        break;
                    case ValidationException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.errors = e.Errors;
                        Log.Information(e, $"[{response.StatusCode}]: {e.Errors}");
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        Log.Warning(e, $"[{response.StatusCode}]: {e?.Message}");
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        Log.Error(error, $"[{response.StatusCode}]: {error?.Message}");
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
    public static class ErrorHandlerMiddlewareExtensions
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}

