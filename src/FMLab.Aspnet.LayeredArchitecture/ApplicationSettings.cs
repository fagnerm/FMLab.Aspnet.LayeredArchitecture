// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using Microsoft.Extensions.Options;

namespace FMLab.Aspnet.LayeredArchitecture;

public class ApplicationSettings : IOptions<ApplicationSettings>
{
    public ApplicationSettings Value => throw new NotImplementedException();
}
