// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.LayeredArchitecture.Business.Shared.Settings;
using FMLab.Aspnet.LayeredArchitecture.Infrastructure.Settings;

namespace FMLab.Aspnet.LayeredArchitecture.Configuration;

public static class ApplicationSettings
{
    public static IServiceCollection AddAppSettings(this IServiceCollection services)
    {
        services.AddSingleton<IApplicationSettings, ApplicationSettingsMonitor>();

        return services;
    }
}
