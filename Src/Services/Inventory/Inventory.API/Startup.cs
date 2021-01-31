using Inventory.API.Data.Interfaces;
using Inventory.API.Repositories.Implementation;
using Inventory.API.Repositories.Interfaces;
using Inventory.API.Settings.Implementation;
using Inventory.API.Settings.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API
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

            #region Configuration Dependencies
            services.Configure<OnlineMarketPlaceDbSettings>(Configuration.GetSection(nameof(OnlineMarketPlaceDbSettings)));
            services.AddSingleton<IOnlineMarketPlaceDbSettings>(sp =>sp.GetRequiredService<IOptions<OnlineMarketPlaceDbSettings>>().Value);
            #endregion

            #region Project Dependencies
            services.AddTransient<IInventoryContext, InventoryContext>();
            services.AddTransient<IProductRepository, ProductRepository>();
            #endregion

            #region Swagger Dependencies
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventory API", Version = "v1" });
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

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory API V1");
            });
        }
    }
}
