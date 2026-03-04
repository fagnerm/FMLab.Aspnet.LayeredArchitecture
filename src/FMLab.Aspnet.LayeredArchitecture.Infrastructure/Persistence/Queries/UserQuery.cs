// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.LayeredArchitecture.Business.DTOs;
using FMLab.Aspnet.LayeredArchitecture.Business.Queries;
using FMLab.Aspnet.LayeredArchitecture.Business.Shared.Result;
using FMLab.Aspnet.LayeredArchitecture.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FMLab.Aspnet.LayeredArchitecture.Infrastructure.Persistence.Queries;
public class UserQuery : IUserQuery
{
    private readonly ApplicationDbContext _context;

    public UserQuery(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CollectionResult<UserSummaryDTO>> ListAsync(ListUsersFilterDTO filter, CancellationToken cancellationToken)
    {
        var query = _context.Users
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
                t.Email != null ? t.Email.Value : null,
                t.Status.ToString()
                ))
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return new CollectionResult<UserSummaryDTO>(
            items, filter.Page, filter.PageSize, totalCount);
    }

    public async Task<UserSummaryDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var query = _context.Users
                            .AsQueryable();

        var user = await query.SingleOrDefaultAsync(u => u.Id == id, cancellationToken)
                              .ConfigureAwait(false);

        if (user is null)
        {
            return null;
        }

        return new UserSummaryDTO(user.Id, user.Name.Value, user.Email?.Value, user.Status.ToString());
    }
}
