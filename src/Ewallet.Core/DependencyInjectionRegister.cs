using Ewallet.Core.Application.Behaviors;
using Ewallet.Core.Application.Users;
using Ewallet.Core.Domain.Users;
using Ewallet.Core.Infrastructure;
using Ewallet.Core.Infrastructure.Interceptors;
using Ewallet.Core.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;
using Ewallet.Core.Application.Accounts;

namespace Ewallet.Core;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjectionRegister).Assembly));

     


        //Application
        services.AddApplication();
        //Infrastructure 
        services.AddPersistance();

        return services;
    }
    private static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
       
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
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
        services.AddScoped<IAccountRepository, AccountRepository>();

        return services;
    }

}
