// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.LayeredArchitecture.Business.Enums;

namespace FMLab.Aspnet.LayeredArchitecture.Api.Endpoints.User;

public record ListUsersFilterRequest(UserStatus? Status, int? Page, int? PageSize);
