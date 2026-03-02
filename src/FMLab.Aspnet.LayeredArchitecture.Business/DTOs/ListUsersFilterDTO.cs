// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.LayeredArchitecture.Business.Enums;
using FMLab.Aspnet.LayeredArchitecture.Business.Shared.Filter;

namespace FMLab.Aspnet.LayeredArchitecture.Business.DTOs;
public record ListUsersFilterDTO : PaginationFilter
{
    public UserStatus? Status { get; init; }

    public ListUsersFilterDTO(UserStatus? status, int? page, int? pageSize)
        : base(page, pageSize)
    {
        Status = status;
    }
}
