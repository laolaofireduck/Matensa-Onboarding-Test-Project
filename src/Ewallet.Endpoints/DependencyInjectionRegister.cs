using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;

namespace Ewallet.Endpoints;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjectionRegister).Assembly;
        services.AddControllers()
        .AddApplicationPart(assembly);
        return services;
    }
    public static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
    {
        app.UseExceptionHandler("/error");

        return app;
    }
}
