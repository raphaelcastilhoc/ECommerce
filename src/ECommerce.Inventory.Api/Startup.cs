using ECommerce.ApiInfrastructure.Extensions;
using ECommerce.EventBus;
using ECommerce.EventBusRabbitMQ;
using ECommerce.Inventory.Api.Application.Queries;
using ECommerce.Inventory.Api.Extensions;
using ECommerce.Inventory.Api.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Inventory.Api
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
            services
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(ModelValidationFilter));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<EventBusRabbitMQSettings>(Configuration.GetSection("RabbitMQ"));

            services
                .AddInfrastructure(Configuration)
                .AddCustomMediatr(typeof(GetProductByIdQueryHandler).Assembly)
                .AddCustomSwagger("Inventory Api")
                .AddEventBus();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IEventBus eventBus)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomSwagger("Inventory Api");

            app.UseEventBus(eventBus);

            app.UseMvc();
        }
    }
}
