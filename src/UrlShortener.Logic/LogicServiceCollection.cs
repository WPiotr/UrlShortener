using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace UrlShortener.Logic
{
    public static class LogicServiceCollection
    {
        public static IServiceCollection RegisterLogic(this IServiceCollection services) =>
            services
                .AddMediatR(typeof(LogicServiceCollection).Assembly);
    }
}