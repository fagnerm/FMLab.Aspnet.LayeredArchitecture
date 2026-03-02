// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.LayeredArchitecture.Business.Entities;
using FMLab.Aspnet.LayeredArchitecture.Business.Repositories;
using FMLab.Aspnet.LayeredArchitecture.Business.ValueObjects;
using FMLab.Aspnet.LayeredArchitecture.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FMLab.Aspnet.LayeredArchitecture.Infrastructure.Persistence.Repositories;
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User User, CancellationToken cancellationToken)
    {
        await _context.AddAsync(User, cancellationToken)
                        .ConfigureAwait(false);
    }

    public void Delete(User user)
    {
        _context.Remove(user);
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _context
                            .Users
                            .AsTracking()
                            .SingleOrDefaultAsync(_ => _.Id == id, cancellationToken)
                            .ConfigureAwait(false);

        return user;
    }

    public User Update(User user)
    {
        _context.Update(user);
        return user;
    }

    public async Task<bool> ExistsByKeyAsync(Name name, Email? email, CancellationToken cancellationToken)
    {
        return await _context.Users
                             .AsNoTracking()
                             .AsQueryable()
                             .AnyAsync(u => u.Name.Value == name.Value ||
                                            (email != null && u.Email != null && u.Email.Value == email.Value), cancellationToken)
                             .ConfigureAwait(false);
    }
}
