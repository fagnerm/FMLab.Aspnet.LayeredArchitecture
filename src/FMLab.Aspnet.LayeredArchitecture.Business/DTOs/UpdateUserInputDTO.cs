// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

namespace FMLab.Aspnet.LayeredArchitecture.Business.DTOs;
public record UpdateUserInputDTO(int Id, string? Name, string? Email);