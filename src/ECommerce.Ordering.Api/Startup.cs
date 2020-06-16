using ECommerce.ApiInfrastructure.Extensions;
using ECommerce.ApiInfrastructure.Filters;
using ECommerce.Ordering.Api.Application.Commands;
using ECommerce.Ordering.Api.Application.Constants;
using ECommerce.Ordering.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Ordering.Api
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
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<OrderingSettings>(Configuration);

            services
                .AddInfrastructure()
                .AddCustomDbContext(Configuration)
                .AddCustomSwagger("Order Api")
                .AddCustomMediatr(typeof(AddBuyerCommandHandler).Assembly)
                .AddCustomHttpClient(HttpClientName.Inventory, Configuration.GetValue<string>("ExternalInventoryBaseUrl"))
                .AddEventBus();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomSwagger("Order Api");

            app.UseMvc();
        }
    }
}
