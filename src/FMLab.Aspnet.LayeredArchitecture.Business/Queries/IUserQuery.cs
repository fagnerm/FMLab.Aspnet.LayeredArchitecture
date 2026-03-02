// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.LayeredArchitecture.Business.DTOs;
using FMLab.Aspnet.LayeredArchitecture.Business.Shared.Result;

namespace FMLab.Aspnet.LayeredArchitecture.Business.Queries;
public interface IUserQuery
{
    Task<CollectionResult<UserSummaryDTO>> ListAsync(ListUsersFilterDTO filter, CancellationToken cancellationToken);
    Task<UserSummaryDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
}
