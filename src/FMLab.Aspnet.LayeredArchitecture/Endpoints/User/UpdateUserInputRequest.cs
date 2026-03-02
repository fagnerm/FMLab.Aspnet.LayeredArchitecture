// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

namespace FMLab.Aspnet.LayeredArchitecture.Api.Endpoints.User;

public record UpdateUserInputRequest(string? Name, string? Email);