// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.


using FMLab.Aspnet.LayeredArchitecture.Business.DTOs;
using FMLab.Aspnet.LayeredArchitecture.Business.Entities;
using FMLab.Aspnet.LayeredArchitecture.Business.Shared.Result;

namespace FMLab.Aspnet.LayeredArchitecture.Business.Repositories;
public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    void Delete(User user);
    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken);
    User Update(User user);
    Task<CollectionResult<UserSummaryDTO>> ListAsync(ListUsersFilterDTO filter, CancellationToken cancellationToken);
    Task<UserSummaryDTO?> ListUserByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> ExistsByKeyAsync(string? name, string? email, CancellationToken cancellationToken);
}
