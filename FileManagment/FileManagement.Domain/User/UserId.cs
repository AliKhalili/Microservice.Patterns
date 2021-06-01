using System;
using Infrastructures.Domain;

namespace FileManagement.Domain.User
{
    public record UserId(Guid Id) : ValueObject;
}