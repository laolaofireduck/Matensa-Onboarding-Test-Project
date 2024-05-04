using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ewallet.Core;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjectionRegister).Assembly));

        //services.AddScoped(
        //    typeof(IPipelineBehavior<,>),
        //    typeof(ValidationBehavior<,>));

        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
