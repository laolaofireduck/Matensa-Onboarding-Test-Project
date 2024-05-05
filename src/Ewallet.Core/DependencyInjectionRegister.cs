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
using Ewallet.Core.Application.Services.Datetime;
using Ewallet.Core.Application.Services.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ewallet.Core;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjectionRegister).Assembly));

     


        //Application
        services.AddApplication(configration);
        //Infrastructure 
        services.AddPersistance();

        return services;
    }
    private static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configration)
    {
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
       
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        var jwtSettings = new JwtSettings();
        configration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddHttpContextAccessor();

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
