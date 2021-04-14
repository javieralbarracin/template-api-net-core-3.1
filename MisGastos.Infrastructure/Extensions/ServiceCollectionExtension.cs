using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MisGastos.Core.Entities;
using MisGastos.Core.Interfaces;
using MisGastos.Core.Services;
using MisGastos.Infrastructure.Context;
using MisGastos.Infrastructure.Interfaces;
using MisGastos.Infrastructure.Options;
using MisGastos.Infrastructure.Repositories;
using MisGastos.Infrastructure.Services;
using System.Text.Json;
using System;
using System.IO;
using MisGastos.Core.Exceptions;
using MisGastos.Infrastructure.Filters;

namespace MisGastos.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        #region Config Context
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MisGastosContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("MisGastos"))
           );

            return services;
        }
        #endregion
        #region Config Options custom password, pagination, etc
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PaginationOptions>(options => configuration.GetSection("Pagination").Bind(options));
            services.Configure<PasswordOptions>(options => configuration.GetSection("PasswordOptions").Bind(options));

            return services;
        }
        #endregion
        #region Policies cors
        public static IServiceCollection AddCorsCustom(this IServiceCollection services, IConfiguration configuration)
        {
            //CORS
            services.AddCors(o => o.AddPolicy("AppCORSPolicy", builder =>
            {
                builder.AllowAnyHeader()
                       .WithMethods(configuration.GetSection("AllowedMethod").Get<string[]>())
                       .WithOrigins(configuration.GetSection("AllowedOrigin").Get<string[]>())
                       .AllowCredentials();
            }));
            return services;
        }
        #endregion
        #region Config Dependencies injection, controller
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ILogFrontendService, LogFrontendService>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IGastoService, GastoService>();
            services.AddTransient<IGastoTipoService, GastoTipoService>();
            services.AddTransient<IFrecuenciaService, FrecuenciaService>();
            services.AddTransient<IMesService, MesService>();
            services.AddTransient<IGastoTipoDetalleService, GastoTipoDetalleService>();


            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);
            });
            //services.AddControllers(options =>
            //{
            //    //options.Filters.Add<AppApiException>();
            //    options.Conventions.Add(new GroupingByNamespaceConvention());

            //}).AddNewtonsoftJson(options =>
            //{
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            //})

            //services.AddControllers(options =>
            //{
            //    options.Conventions.Add(new GroupingByNamespaceConvention());
            //});
               // .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

            return services;
        }
        #region Config Api version
        public static IServiceCollection AddApiVersion(this IServiceCollection services)
        {
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true; //informar al cliente si esta deprecate entre otros
                //x.ApiVersionReader = new QueryStringApiVersionReader("version");
                x.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
                //x.UseApiBehavior = true;
            });
            return services;
        }
        #endregion
        #endregion
        #region Config Swagger
        public static IServiceCollection AddSwagger(this IServiceCollection services, string xmlFileName)
        {
           services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation  
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "JWT Token Authentication API",
                    Description = "ASP.NET Core 3.1 Web API",
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    },
                    Contact = new OpenApiContact()
                    {
                        Name = "Javi",
                        Email = "javieralbarracin881@gmail.com"                                               
                    }
                });
                // To Enable authorization using Swagger (JWT)  
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
                swagger.SwaggerDoc("v2", new OpenApiInfo {
                    Version = "v2",
                    Title = "JWT Token Authentication API",
                    Description = "ASP.NET Core 3.1 Web API con optimizaciones"
                });
                //Filtros para le manejo de versiones en la docu de swagger
                swagger.OperationFilter<RemoveVersionParameterFilter>();
                swagger.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                

            });

            return services;
        }
        #endregion
    }
}
