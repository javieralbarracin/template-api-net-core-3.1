using System;
using System.Reflection;
using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MisGastos.Api.Middleware;
using MisGastos.Api.Services;
using MisGastos.Infrastructure.Extensions;
using MisGastos.Infrastructure.Filters;
using MisGastos.Infrastructure.Interfaces;
using MisGastos.Infrastructure.Options;

namespace MisGastos.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsCustom(Configuration);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddOptions(Configuration);
            services.AddDbContexts(Configuration);
            services.AddServices();
            services.AddApiVersion();
            services.AddSwagger($"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            #region Configuration Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],
                    ValidAudience = Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]))
                };

            });
            #endregion
            #region Filter Validation
            services.AddMvcCore(options =>
            {
                options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });
            #endregion
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
            #region Global exception Filter
            services.AddControllers(options =>
            {
                //options.Filters.Add<GlobalExceptionFilter>();
                options.Conventions.Add(new GroupingByNamespaceConvention());

            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Mis Gastos API V1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "Mis Gastos API V2");
                options.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseHttpsRedirection();

            // Serilog configuration
            //Log.Logger = new LoggerConfiguration()
            //        .WriteTo.RollingFile(pathFormat: "logs\\log-{Date}.log")
            //        .CreateLogger();
            
            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseErrorHandlingMiddleware();
            //app.UseCorsMiddleware();
            //app.UsePreflightRequestHandler();

            app.UseCors("AppCORSPolicy");
            // This will make the HTTP requests log as rich logs instead of plain text.
            //app.UseSerilogRequestLogging(); // <-- Add this line            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors("AppCORSPolicy"); 
            });

        }
    }
}
