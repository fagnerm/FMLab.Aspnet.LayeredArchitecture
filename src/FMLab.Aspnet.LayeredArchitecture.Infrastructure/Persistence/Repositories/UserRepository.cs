// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.LayeredArchitecture.Business.DTOs;
using FMLab.Aspnet.LayeredArchitecture.Business.Entities;
using FMLab.Aspnet.LayeredArchitecture.Business.Repositories;
using FMLab.Aspnet.LayeredArchitecture.Business.Shared.Result;
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
                            .SingleOrDefaultAsync(_ => _.Id == id, cancellationToken);

        return user;
    }

    public User Update(User user)
    {
        _context.Update(user);
        return user;
    }

    public async Task<bool> ExistsByKeyAsync(string? name, string? email, CancellationToken cancellationToken)
    {
        return await _context.Users
                             .AsNoTracking()
                             .AsQueryable()
                             .AnyAsync(u => (name != null && u.Name.Value == name) ||
                                            (email != null && u.Email!.Value == email), cancellationToken);
    }

    public async Task<CollectionResult<UserSummaryDTO>> ListAsync(ListUsersFilterDTO filter, CancellationToken cancellationToken)
    {
        var query = _context.Users
                            .AsNoTracking()
                            .AsQueryable();

        if (filter.Status.HasValue)
        {
            query = query.Where(t => t.Status == filter.Status.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query.OrderByDescending(t => t.Name)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(t => new UserSummaryDTO(
                t.Id,
                t.Name.Value,
                t.Email!.Value,
                t.Status.ToString()
                ))
            .ToListAsync(cancellationToken);

        return new CollectionResult<UserSummaryDTO>(
            items, filter.Page, filter.PageSize, totalCount);
    }

    public async Task<UserSummaryDTO?> ListUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        var query = _context.Users
                            .AsNoTracking()
                            .AsQueryable();

        var user = await query.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (user is null)
        {
            return null;
        }

        return new UserSummaryDTO(user.Id, user.Name.Value, user.Email?.Value, user.Status.ToString());
    }
}
