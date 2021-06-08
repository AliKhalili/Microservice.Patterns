using System;
using BuildingBlocks.Domain;

namespace FileManagement.Domain.User
{
    public record UserId(Guid Value) : ValueObject;
}