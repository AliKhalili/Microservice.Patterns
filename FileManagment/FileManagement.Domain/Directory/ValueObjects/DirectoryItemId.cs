﻿using System;
using BuildingBlock.Domain;

namespace FileManagement.Domain.Directory.ValueObjects
{
    public record DirectoryItemId(Guid Value) : ValueObject
    {

    }
}