using FileManagement.Application.Directory.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Application;

namespace FileManagement.Application.Directory.Handlers
{
    public class CopyDirectoryHandler : ICommandHandler<CopyDirectoryCommand>
    {

        public Task<Unit> Handle(CopyDirectoryCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}