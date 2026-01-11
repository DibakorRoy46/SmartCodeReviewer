
using Shared.Domain.Events;

namespace Shared.Application.Abstractions;

public interface IEventBus
{
    Task PublishAsync(IDomainEvent domainEvent);
}