// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.LayeredArchitecture.Api.Endpoints.User;

namespace FMLab.Aspnet.LayeredArchitecture.Configuration;

public static class AppEndpoints
{
    public static WebApplication UseApplicationEndpoints(this WebApplication app)
    {
        UserEndpoints.MapUser(app);

        return app;
    }
}
