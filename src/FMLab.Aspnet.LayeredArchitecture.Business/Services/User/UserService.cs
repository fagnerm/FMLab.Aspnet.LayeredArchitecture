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
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IUserQuery _userQuery;

    public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IUserQuery userQuery)
    {
        this._unitOfWork = unitOfWork;
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
        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.CommitTransactionAsync(cancellationToken);

        var result = new CreateUserOutputDTO(user.Id, user.Name.Value, user.Email?.Value, user.Status.ToString());

        return Result<CreateUserOutputDTO>.Success(result);
    }

    public async Task<Result<NoOutput>> DeleteUserAsync(DeleteUserInputDTO input, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByIdAsync(input.Id, cancellationToken);

        if (existingUser is null) return Result<NoOutput>.NotFound("User not found");

        _userRepository.Delete(existingUser!);
        await _unitOfWork.CommitTransactionAsync(cancellationToken);

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
        _userRepository.Update(user);
        await _unitOfWork.CommitTransactionAsync(cancellationToken);

        return Result<NoOutput>.NoContent();
    }

    public async Task<Result<ListUsersOutputDTO>> ListAllUsersAsync(GetListUsersInputDTO input, CancellationToken cancellationToken)
    {
        var filter = new ListUsersFilterDTO(input.Status, input.Page, input.PageSize);
        var result = await _userQuery.ListAsync(filter, cancellationToken)
                                     .ConfigureAwait(false);

        var output = new ListUsersOutputDTO(result.Items, result.Page, result.PageSize, result.TotalItems);
        return Result<ListUsersOutputDTO>.Success(output);
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
        var user = await _userRepository.GetByIdAsync(input.Id, cancellationToken);

        if (user is null) return Result<UpdateUserOutputDTO>.NotFound("User not found");

        var name = new Name(input.Name!);
        var email = string.IsNullOrEmpty(input.Email) ? null : new Email(input.Email!);
        user.Update(name, email);

        _userRepository.Update(user);
        await _unitOfWork.CommitTransactionAsync(cancellationToken);

        var result = new UpdateUserOutputDTO(user.Id, user.Name.Value, user.Email?.Value, user.Status.ToString());
        return Result<UpdateUserOutputDTO>.Success(result);
    }

    public async Task<Result<UpdateUserOutputDTO>> UpdateUserPartiallyAsync(PatchUserInputDTO input, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(input.Id, cancellationToken);

        if (user is null) return Result<UpdateUserOutputDTO>.NotFound("User not found");

        var name = string.IsNullOrEmpty(input.Name) ? user.Name : new Name(input.Name!);
        var email = string.IsNullOrEmpty(input.Email) ? user.Email : new Email(input.Email!);
        user.Update(name, email);

        _userRepository.Update(user);
        await _unitOfWork.CommitTransactionAsync(cancellationToken);

        var result = new UpdateUserOutputDTO(user.Id, user.Name.Value, user.Email?.Value, user.Status.ToString());
        return Result<UpdateUserOutputDTO>.Success(result);
    }
}
