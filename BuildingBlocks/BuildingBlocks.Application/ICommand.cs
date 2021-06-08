using System;
using MediatR;

namespace BuildingBlocks.Application
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
        Guid CorrelationId { get; init; }
    }
    public interface ICommand : IRequest
    {
        Guid CorrelationId { get; init; }
    }
}