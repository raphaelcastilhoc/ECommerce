using ECommerce.ApiInfrastructure.Extensions;
using ECommerce.Location.Api.Application.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace ECommerce.Location.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<LocationSettings>(Configuration);

            services
                .AddScoped<IDbConnection>(x => new SqlConnection(Configuration.GetConnectionString("SqlServer")))
                .AddCustomMediatr(typeof(GetLocationByZipCodeQueryHandler).Assembly)
                .AddCustomSwagger("Location Api");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomSwagger("Location Api");

            app.UseMvc();
        }
    }
}
