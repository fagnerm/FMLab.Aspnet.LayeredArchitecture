// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.LayeredArchitecture.Business.DTOs;
using FMLab.Aspnet.LayeredArchitecture.Business.ExternalServices.Persistence;
using FMLab.Aspnet.LayeredArchitecture.Business.Queries;
using FMLab.Aspnet.LayeredArchitecture.Business.Repositories;
using FMLab.Aspnet.LayeredArchitecture.Business.Shared.Result;
using FMLab.Aspnet.LayeredArchitecture.Business.ValueObjects;

namespace FMLab.Aspnet.LayeredArchitecture.Business.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserQuery _userQuery;

    public UserService(IUserRepository userRepository, IUserQuery userQuery)
    {
        _userRepository = userRepository;
        _userQuery = userQuery;
    }

    public async Task<Result<CreateUserOutputDTO>> CreateUserAsync(CreateUserInputDTO input, CancellationToken cancellationToken)
    {
        var name = new Name(input.Name);
        var email = input.Email is null ? null : new Email(input.Email);

        var found = await _userRepository.ExistsByKeyAsync(name, email, cancellationToken)
                                         .ConfigureAwait(false);

        if (found) return Result<CreateUserOutputDTO>.Conflict("User already exists");

        var user = new Entities.User(name, email);
        await _userRepository.AddAsync(user, cancellationToken)
                             .ConfigureAwait(false);

        var result = new CreateUserOutputDTO(user.Id, user.Name.Value, user.Email?.Value, user.Status.ToString());

        return Result<CreateUserOutputDTO>.Success(result);
    }

    public async Task<Result<NoOutput>> DeleteUserAsync(DeleteUserInputDTO input, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByIdAsync(input.Id, cancellationToken);

        if (existingUser is null) return Result<NoOutput>.NotFound("User not found");

        await _userRepository.Delete(existingUser!)
                         .ConfigureAwait(false);

        return Result<NoOutput>.NoContent();
    }

    public async Task<Result<NoOutput>> DisableUserAsync(DisableUserInputDTO input, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(input.Id, cancellationToken);

        if (user == null)
        {
            return Result<NoOutput>.NotFound("User not found");
        }

        user.Deactivate();
        await _userRepository.Update(user)
                             .ConfigureAwait(false);

        return Result<NoOutput>.NoContent();
    }

    public async Task<Result<CollectionResult<UserSummaryDTO>>> ListAllUsersAsync(ListUsersFilterDTO input, CancellationToken cancellationToken)
    {
        var result = await _userQuery.ListAsync(input, cancellationToken)
                                     .ConfigureAwait(false);

        return Result<CollectionResult<UserSummaryDTO>>.Success(result);
    }

    public async Task<Result<UserSummaryDTO>> ListUserAsync(GetUserInputDTO input, CancellationToken cancellationToken)
    {
        var user = await _userQuery.GetByIdAsync(input.Id, cancellationToken)
                                   .ConfigureAwait(false);

        if (user == null)
        {
            return Result<UserSummaryDTO>.NotFound("User not found");
        }

        return Result<UserSummaryDTO>.Success(user);
    }

    public async Task<Result<UpdateUserOutputDTO>> UpdateUserAsync(UpdateUserInputDTO input, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(input.Id, cancellationToken)
                                        .ConfigureAwait(false);

        if (user is null) return Result<UpdateUserOutputDTO>.NotFound("User not found");

        var name = new Name(input.Name!);
        var email = string.IsNullOrEmpty(input.Email) ? null : new Email(input.Email!);
        user.Update(name, email);

        await _userRepository.Update(user)
                             .ConfigureAwait(false);

        var result = new UpdateUserOutputDTO(user.Id, user.Name.Value, user.Email?.Value, user.Status.ToString());
        return Result<UpdateUserOutputDTO>.Success(result);
    }

    public async Task<Result<UpdateUserOutputDTO>> UpdateUserPartiallyAsync(PatchUserInputDTO input, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(input.Id, cancellationToken)
                                        .ConfigureAwait(false);

        if (user is null) return Result<UpdateUserOutputDTO>.NotFound("User not found");

        var name = string.IsNullOrEmpty(input.Name) ? user.Name : new Name(input.Name!);
        var email = string.IsNullOrEmpty(input.Email) ? user.Email : new Email(input.Email!);
        user.Update(name, email);

        await _userRepository.Update(user)
                             .ConfigureAwait(false);

        var result = new UpdateUserOutputDTO(user.Id, user.Name.Value, user.Email?.Value, user.Status.ToString());
        return Result<UpdateUserOutputDTO>.Success(result);
    }
}
