// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

namespace FMLab.Aspnet.LayeredArchitecture.Business.Shared.Filter;
public record PaginationFilter
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public PaginationFilter(int? page, int? pageSize)
    {
        Page = page ?? 1;
        PageSize = pageSize ?? 100;
    }
}