// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.LayeredArchitecture.Business.DTOs;
using FMLab.Aspnet.LayeredArchitecture.Business.Services.User;
using FMLab.Aspnet.LayeredArchitecture.Helper;
using Microsoft.AspNetCore.Mvc;

namespace FMLab.Aspnet.LayeredArchitecture.Api.Endpoints.User;

public static class UserEndpoints
{
    internal static void MapUser(WebApplication app)
    {
        app.MapGet("/users", ListAllUsersEndpoint)
            .WithTags("Users")
            .Produces(StatusCodes.Status200OK)
            .RequireAuthorization()
            .WithOpenApi();

        app.MapGet("/users/{id}", ListUserEndpoint)
            .WithTags("Users")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status404NotFound)
            .RequireAuthorization()
            .WithOpenApi();

        app.MapPost("/users/{id}/deactivate", DisableUserEndpoint)
            .WithTags("Users")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem(StatusCodes.Status404NotFound)
            .RequireAuthorization()
            .WithOpenApi();

        app.MapPost("/users", PostUserEndpoint)
            .WithTags("Users")
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem(StatusCodes.Status409Conflict)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
            .RequireAuthorization()
            .WithOpenApi();

        app.MapPatch("/users/{id}", PatchUserEndpoint)
            .WithTags("Users")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
            .RequireAuthorization()
            .WithOpenApi();

        app.MapPut("/users/{id}", PutUserEndpoint)
            .WithTags("Users")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
            .RequireAuthorization()
            .WithOpenApi();

        app.MapDelete("/users/{id}", DeleteUserEndpoint)
            .WithTags("Users")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status404NotFound)
            .RequireAuthorization()
            .WithOpenApi();
    }

    private static async Task<IResult> ListAllUsersEndpoint([FromServices] IUserService service, [AsParameters] ListUsersFilterRequest request, CancellationToken cancellationToken)
    {
        var input = new ListUsersFilterDTO(request.Status, request.Page, request.PageSize);
        var output = await service.ListAllUsersAsync(input, cancellationToken);

        return output.ToProblemResult();
    }

    private static async Task<IResult> ListUserEndpoint([FromServices] IUserService service, [FromRoute] int id, CancellationToken cancellationToken)
    {
        var input = new GetUserInputDTO(id);
        var output = await service.ListUserAsync(input, cancellationToken);

        return output.ToProblemResult();
    }

    private static async Task<IResult> PostUserEndpoint([FromServices] IUserService service, [FromBody] CreateUserInputRequest body, CancellationToken cancellationToken)
    {
        var input = new CreateUserInputDTO(body.Name, body.Email);
        var output = await service.CreateUserAsync(input, cancellationToken);

        if (!output.IsSuccess) return output.ToProblemResult();

        var result = output.Data;
        return Results.Created($"/users/{result?.Id}", result);
    }

    private static async Task<IResult> DisableUserEndpoint([FromServices] IUserService service, [FromRoute] int id, CancellationToken cancellationToken)
    {
        var input = new DisableUserInputDTO(id);
        var output = await service.DisableUserAsync(input, cancellationToken);

        return output.ToProblemResult();
    }

    private static async Task<IResult> PutUserEndpoint([FromServices] IUserService service, [FromRoute] int id, [FromBody] UpdateUserInputRequest body, CancellationToken cancellationToken)
    {
        var input = new UpdateUserInputDTO(id, body.Name, body.Email);
        var output = await service.UpdateUserAsync(input, cancellationToken);

        return output.ToProblemResult();
    }

    private static async Task<IResult> PatchUserEndpoint([FromServices] IUserService service, [FromRoute] int id, [FromBody] UpdateUserInputRequest body, CancellationToken cancellationToken)
    {
        var input = new PatchUserInputDTO(id, body.Name, body.Email);
        var output = await service.UpdateUserPartiallyAsync(input, cancellationToken);

        return output.ToProblemResult();
    }

    private static async Task<IResult> DeleteUserEndpoint([FromServices] IUserService service, [FromRoute] int id, CancellationToken cancellationToken)
    {
        var input = new DeleteUserInputDTO(id);
        var output = await service.DeleteUserAsync(input, cancellationToken);

        return output.ToProblemResult();
    }
}
