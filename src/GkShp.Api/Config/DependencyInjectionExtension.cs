using GkShp.Catalog.Application.Services;
using GkShp.Catalog.Data;
using GkShp.Catalog.Data.Repository;
using GkShp.Catalog.Domain;
using GkShp.Core.Bus;

namespace GkShp.Api.Config
{
    public static class DependencyInjectionExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Catalogo
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductApplicationService, ProductApplicationService>();
            services.AddScoped<IStockService, StockService>();
        }
    }
}