using System;
using BuildingBlocks.Application;

namespace FileManagement.Application.Directory.Commands
{
    public class CopyDirectoryCommand : ICommand
    {
        public CopyDirectoryCommand(Guid sourceDirectoryId, Guid destinationDirectoryId)
        {
            CorrelationId = CorrelationIds.NewId;
            SourceDirectoryId = sourceDirectoryId;
            DestinationDirectoryId = destinationDirectoryId;
        }

        public Guid SourceDirectoryId { get; }
        public Guid DestinationDirectoryId { get; }
        public Guid CorrelationId { get; init; }
    }
}