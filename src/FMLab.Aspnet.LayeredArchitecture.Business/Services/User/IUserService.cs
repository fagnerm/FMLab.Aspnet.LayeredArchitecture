// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.LayeredArchitecture.Business.DTOs;
using FMLab.Aspnet.LayeredArchitecture.Business.Shared.Result;

namespace FMLab.Aspnet.LayeredArchitecture.Business.Services.User;

public interface IUserService
{
    Task<Result<ListUsersOutputDTO>> ListAllUsersAsync(GetListUsersInputDTO input, CancellationToken cancellationToken);
    Task<Result<UserSummaryDTO>> ListUserAsync(GetUserInputDTO input, CancellationToken cancellationToken);
    Task<Result<CreateUserOutputDTO>> CreateUserAsync(CreateUserInputDTO input, CancellationToken cancellationToken);
    Task<Result<NoOutput>> DisableUserAsync(DisableUserInputDTO input, CancellationToken cancellationToken);
    Task<Result<UpdateUserOutputDTO>> UpdateUserAsync(UpdateUserInputDTO input, CancellationToken cancellationToken);
    Task<Result<UpdateUserOutputDTO>> UpdateUserPartiallyAsync(PatchUserInputDTO input, CancellationToken cancellationToken);
    Task<Result<NoOutput>> DeleteUserAsync(DeleteUserInputDTO input, CancellationToken cancellationToken);
}