using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ProductStore.API.Configs;
using ProductStore.API.Filters;
using ProductStore.Core.IoC;
using ProductStore.Infrastructure.Configs;
using ProductStore.Infrastructure.IoC;

namespace ProductStore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
               {
                  options.Filters.Add(typeof(HttpGlobalExceptionFilter));
               })
                .AddNewtonsoftJson(options => options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.Configure<AuthorizationConfig>(Configuration.GetSection("AuthorizationConfig"));
            AddDatabaseAndMigrationServices(services);


            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product Store API", Version = "v1" });
                c.OperationFilter<ApiKeyHeaderSwaggerAttribute>();
            });
            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;
            });
            services.AddCoreServices();
            services.AddInfrastructureServices();
        }

        private void AddDatabaseAndMigrationServices(IServiceCollection services)
        {
            services.Configure<DatabaseConfig>(Configuration.GetSection("DatabaseConfig"));
            services.AddTransient<IStartupFilter, DatabaseInitFilter>();
            services.AddSingleton(provider =>
            {
                var configValue = provider.GetRequiredService<IOptions<DatabaseConfig>>().Value;
                return configValue;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Store API V1");
            });
        }
    }
}