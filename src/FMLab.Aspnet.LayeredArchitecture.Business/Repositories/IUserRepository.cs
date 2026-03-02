// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.


using FMLab.Aspnet.LayeredArchitecture.Business.Entities;
using FMLab.Aspnet.LayeredArchitecture.Business.ValueObjects;

namespace FMLab.Aspnet.LayeredArchitecture.Business.Repositories;
public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task Delete(User user);
    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<User> Update(User user);
    Task<bool> ExistsByKeyAsync(Name name, Email? email, CancellationToken cancellationToken);
}
