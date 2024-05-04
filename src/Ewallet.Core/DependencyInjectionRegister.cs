using Ewallet.Core.Application.Users;
using Ewallet.Core.Domain.Users;
using Ewallet.Core.Infrastructure;
using Ewallet.Core.Infrastructure.Interceptors;
using Ewallet.Core.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        //Application
        services.AddApplication();
        //Infrastructure 
        services.AddPersistance();

        return services;
    }
    private static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }
    private static IServiceCollection AddPersistance(this IServiceCollection services)
    {
        var rootPath = Directory.GetCurrentDirectory();
        var dbPath = Path.Combine(rootPath, "Data", "ewallet.db");

        services.AddDbContext<EwalletDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}")
            );

        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

}
