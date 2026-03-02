// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.LayeredArchitecture.Business.ExternalServices.Email;
using FMLab.Aspnet.LayeredArchitecture.Business.ExternalServices.Persistence;
using FMLab.Aspnet.LayeredArchitecture.Business.Queries;
using FMLab.Aspnet.LayeredArchitecture.Business.Repositories;
using FMLab.Aspnet.LayeredArchitecture.Business.Shared.Settings;
using FMLab.Aspnet.LayeredArchitecture.Infrastructure.ExternalServices.Email;
using FMLab.Aspnet.LayeredArchitecture.Infrastructure.Persistence.Context;
using FMLab.Aspnet.LayeredArchitecture.Infrastructure.Persistence.Queries;
using FMLab.Aspnet.LayeredArchitecture.Infrastructure.Persistence.Repositories;
using FMLab.Aspnet.LayeredArchitecture.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FMLab.Aspnet.LayeredArchitecture.Infrastructure.DependecyInjection;
public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IHostEnvironment environment)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase("layered-arch")
                   .LogTo(Console.WriteLine, LogLevel.Information)
                   .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            if (environment.IsDevelopment())
                options.EnableSensitiveDataLogging();
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserQuery, UserQuery>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddSingleton<IApplicationSettings, ApplicationSettingsMonitor>();
        
        return services;
    }
}

