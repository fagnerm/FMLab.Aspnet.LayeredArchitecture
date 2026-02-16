// API - Layered architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.LayeredArchitecture.Business.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FMLab.Aspnet.LayeredArchitecture.Infrastructure.Persistence.Converters;
public class EmailToStringConverter : ValueConverter<Email, string>
{
    public EmailToStringConverter() :
        base(n => n.Value,
             n => new Email(n))
    {
    }
}