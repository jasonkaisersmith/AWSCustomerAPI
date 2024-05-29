using AWSCustomerAPI.Validators.Filters;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWSCustomerAPI.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddControllers()
                .AddNewtonsoftJson(opts => opts.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services.AddMvc(options =>
                {
                    options.Filters.Add<ValidationFilter>();
                })
                .AddFluentValidation(mvcConfiguration => mvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddNewtonsoftJson();
          

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customer", Version = "v1" });             
            });
            #endregion
        }
    }
}
