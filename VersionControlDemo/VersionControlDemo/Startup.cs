using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using VersionControlDemo.Controllers;

namespace VersionControlDemo
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
            services.AddControllers();
            services.AddApiVersioning( options => 
            {
                // If no version set, default 1.0
                options.AssumeDefaultVersionWhenUnspecified = true;
                // Change Default Version as you like, 
                // Usually you should expect some lazy user never update client program, so 1.0 is totally fine
                options.DefaultApiVersion = new ApiVersion(1,0);
                //Combine multiple version accept method
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new MediaTypeApiVersionReader("MediaVersion"),
                    new HeaderApiVersionReader("HeadVersion")
                );
                //Report Verison
                options.ReportApiVersions = true;
                /*options.Conventions.Controller<ProductsController>()
                .HasDeprecatedApiVersion(1,0) // Same as deprecated version
                .HasApiVersion(2,0)
                .Action(typeof(ProductsController).GetMethod(nameof(ProductsController.GetProductsV1))).MapToApiVersion(1, 0) // map a method to version 1.0
                .Action(typeof(ProductsController).GetMethod(nameof(ProductsController.GetProductsV2))).MapToApiVersion(2, 0) // map a method to version 2.0
                ;*/
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
