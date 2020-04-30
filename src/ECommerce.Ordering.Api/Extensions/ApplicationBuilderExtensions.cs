using Microsoft.AspNetCore.Builder;

namespace ECommerce.Ordering.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void AddCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
