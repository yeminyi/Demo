
using CodeTestDemo.Core.Interfaces;
using CodeTestDemo.Infrastructure.Database;
using CodeTestDemo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CodeTestDemo.Api.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AutoMapper;
using CodeTestDemo.Infrastructure.Resources;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using CodeTestDemo.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json.Serialization;
using System.Linq;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace CodeTestDemo.Api
{
    public class Startup
    {
        private static IConfiguration Configuration { get; set; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CodeTest API",
                    Version = "v1"
                });
              
            });
            services.AddMvc(
              options =>
              {
                  options.ReturnHttpNotAcceptable = true;
                  options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
              })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddFluentValidation();

            services.AddDbContext<MyContext>(options =>
            {

                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlite(connectionString);
            });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 6001;
            });
     
   
            services.AddTransient<IValidator<ScoreAddResource>, ScoreAddOrUpdateResourceValidator<ScoreAddResource>>();
           

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IScoreRepository, ScoreRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var propertyMappingContainer = new PropertyMappingContainer();
            propertyMappingContainer.Register<ScorePropertyMapping>();
            services.AddSingleton<IPropertyMappingContainer>(propertyMappingContainer);
            services.AddTransient<ITypeHelperService, TypeHelperService>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDevOrigin",
                    builder => builder.WithOrigins(Configuration["ClientAddress"])
                        .WithExposedHeaders("X-Pagination")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });


            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = @"dist";
            });
        }
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment  env)
        {
            app.UseMyExceptionHandler(loggerFactory);
            
            app.UseCors("AllowAngularDevOrigin");

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeTestDemo v1");
            });

            app.UseMvc();

            app.UseSpa(spa =>
            {

                  spa.Options.SourcePath = @"../codetest-client";

            });
        }
    }
}

