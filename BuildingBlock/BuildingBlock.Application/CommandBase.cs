using System;

namespace BuildingBlocks.Application
{
    public abstract class CommandBase<TResult>:ICommand<TResult>
    {
        public Guid CorrelationId { get; init; }
    }
}