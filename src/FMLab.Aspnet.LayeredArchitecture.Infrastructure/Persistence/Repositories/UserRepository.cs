// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.LayeredArchitecture.Business.Entities;
using FMLab.Aspnet.LayeredArchitecture.Business.Exceptions;
using FMLab.Aspnet.LayeredArchitecture.Business.Repositories;
using FMLab.Aspnet.LayeredArchitecture.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace FMLab.Aspnet.LayeredArchitecture.Infrastructure.Persistence.Repositories;
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        try
        {
            await _context.AddAsync(user, cancellationToken)
                          .ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken)
                          .ConfigureAwait(false);
        }
        catch (DbUpdateException ex) 
            when (ex.InnerException is PostgresException { SqlState: "23505" })
        {
            _context.Entry(user).State = EntityState.Detached;
            throw new DomainException("User already exists", ex);
        }
    }

    public async Task Delete(User user)
    {
        _context.Remove(user);
        await _context.SaveChangesAsync();
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

    public async Task<User> Update(User user)
    {
        var entry = _context.Update(user);
        await _context.SaveChangesAsync();

        return entry.Entity;
    }

}
