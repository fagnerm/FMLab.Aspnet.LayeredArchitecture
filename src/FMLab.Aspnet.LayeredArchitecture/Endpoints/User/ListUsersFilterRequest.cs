// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.LayeredArchitecture.Business.Enums;
using System.ComponentModel.DataAnnotations;

namespace FMLab.Aspnet.LayeredArchitecture.Api.Endpoints.User;

public record ListUsersFilterRequest(UserStatus? Status, [MinLength(1)] int Page, [MaxLength(100)] int PageSize);
