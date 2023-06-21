using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using SalesPlatform.Infrastructure;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SalesPlatform.APIv1
{
    /// <summary>
    /// Startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Configuration app.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Servies app.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services configuration.
            services.AddSingleton<IConfiguration>(this.Configuration);

            // Load database context
            services.AddDbContext<SalesPlatformContext>(
                opts => opts.UseInMemoryDatabase("SalesPlatformDB")
                            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                );

            // Allow API for all sites. (Pending....)
            services.AddCors(c =>
            {
                c.AddDefaultPolicy(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            // Stores all information http request
            services.AddHttpContextAccessor();

            // Register the Swagger generator, defining 1 or more Swagger documents
            string version = typeof(Startup).Assembly.GetName().Version.ToString();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sales platform manager API",
                    Version = "v" + version,
                    Description = "Sales platform API REST",
                });

                // Enabled swagger anotation
                c.EnableAnnotations();
            });

            services.AddMvcCore().
               AddNewtonsoftJson(options =>
                   options.SerializerSettings.Converters.Add(new StringEnumConverter())
               )
               .AddJsonOptions(opts =>
               {
                   opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
               })
               .AddApiExplorer();

            services.AddSwaggerGenNewtonsoftSupport();

            // Add dependency injection
            services.AddScoped<Domain.Interfaces.IProductsRepository, Infrastructure.Rpositories.ProductsRepository>();
            services.AddScoped<Domain.Interfaces.ISalesRepository, Infrastructure.Rpositories.SalesRepository>();

            // Add Controler
            services.AddControllers();

            services.AddMvc();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Enviroment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Cors
            app.UseCors();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sales platform API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
