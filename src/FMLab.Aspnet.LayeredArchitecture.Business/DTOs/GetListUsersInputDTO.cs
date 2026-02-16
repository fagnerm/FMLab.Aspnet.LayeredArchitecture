// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.LayeredArchitecture.Business.Enums;
using FMLab.Aspnet.LayeredArchitecture.Business.Shared.Filter;

namespace FMLab.Aspnet.LayeredArchitecture.Business.DTOs;
public record GetListUsersInputDTO(UserStatus? Status, int Page = 1, int PageSize = 20)
    : PaginationFilter(Page, PageSize);