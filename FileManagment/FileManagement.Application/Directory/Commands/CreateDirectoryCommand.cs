using System;
using BuildingBlocks.Application;

namespace FileManagement.Application.Directory.Commands
{
    public class CreateDirectoryCommand:ICommand
    {
        public Guid CorrelationId { get; init; }
    }
}