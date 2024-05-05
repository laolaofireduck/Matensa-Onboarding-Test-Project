using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication;

namespace Ewallet.Endpoints;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjectionRegister).Assembly;
        services.AddControllers()
        .AddApplicationPart(assembly);

        services.AddAuthentication("AdminToken")
        .AddScheme<AuthenticationSchemeOptions, AdminTokenAuthenticationHandler>("AdminToken", options => { });

        return services;
    }
    public static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
    {
        app.UseExceptionHandler("/error");

        return app;
    }
}
