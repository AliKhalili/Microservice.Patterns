using System;

namespace BuildingBlock.Application
{
    public abstract class CommandBase<TResult>:ICommand<TResult>
    {
        public Guid CorrelationId { get; init; }
    }
}