﻿using System;
using BuildingBlock.Domain;

namespace FileManagement.Domain.User
{
    public record UserId(Guid Id) : ValueObject;
}