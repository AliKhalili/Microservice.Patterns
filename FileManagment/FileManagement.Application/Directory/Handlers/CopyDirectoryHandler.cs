using FileManagement.Application.Directory.Commands;
using BuildingBlock.Application;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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