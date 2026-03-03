// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.LayeredArchitecture.Business.ExternalServices.Email;
using FMLab.Aspnet.LayeredArchitecture.Business.ExternalServices.Persistence;
using FMLab.Aspnet.LayeredArchitecture.Business.Queries;
using FMLab.Aspnet.LayeredArchitecture.Business.Repositories;
using FMLab.Aspnet.LayeredArchitecture.Infrastructure.ExternalServices.Email;
using FMLab.Aspnet.LayeredArchitecture.Infrastructure.Persistence.Context;
using FMLab.Aspnet.LayeredArchitecture.Infrastructure.Persistence.Queries;
using FMLab.Aspnet.LayeredArchitecture.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace FMLab.Aspnet.LayeredArchitecture.Infrastructure.DependencyInjection;
public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config, IHostEnvironment environment)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connection = new NpgsqlConnectionStringBuilder()
            {
                Host = config["Database:Server"],
                Port = int.Parse(config["Database:Port"]),
                Database = config["Database:Name"],
                Username = config["Database:User"],
                Password = config["Database:Password"],
                NoResetOnClose = true
            };

            options.UseNpgsql(connection.ConnectionString)
                   .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            if (environment.IsDevelopment())
            {
                options.EnableSensitiveDataLogging()
                       .LogTo(Console.WriteLine);
            }
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserQuery, UserQuery>();
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}

