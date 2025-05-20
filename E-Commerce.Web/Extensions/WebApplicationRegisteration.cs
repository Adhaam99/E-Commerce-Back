using System.Text.Json;
using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWares;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace E_Commerce.Web.Extensions
{
    public static class WebApplicationRegisteration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var ObjectOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            await ObjectOfDataSeeding.DataSeedAsync();
            await ObjectOfDataSeeding.IdentityDataSeedAsync();
        }

        public static IApplicationBuilder UseCustomExceptionMiddleWares(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();

            return app;
        }

        public static IApplicationBuilder UseSwaggerMiddleWares(this IApplicationBuilder app)
        {

            app.UseSwagger();
            app.UseSwaggerUI(Options =>
            {
                Options.ConfigObject = new Swashbuckle.AspNetCore.SwaggerUI.ConfigObject()
                {
                    DisplayRequestDuration = true
                };

                Options.DocumentTitle = "E-Commerce API";

                Options.JsonSerializerOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                Options.DocExpansion(DocExpansion.None);

                Options.EnableFilter();
                Options.EnablePersistAuthorization();
            });

            return app;
        }

        
    }
}
