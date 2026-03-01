// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.LayeredArchitecture.Business.Shared.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FMLab.Aspnet.LayeredArchitecture.Infrastructure.Settings;
public class ApplicationSettingsMonitor : IApplicationSettings
{
    private readonly IOptionsMonitor<ApplicationSettings> _monitor;
    private readonly IConfiguration _configuration;

    public ApplicationSettingsMonitor(IConfiguration configuration, IOptionsMonitor<ApplicationSettings> monitor)
    {
        _monitor = monitor;
        _configuration = configuration;
    }

    public string ApiKey => _configuration["ApiKey"];
}
