using Shared.Application.Abstractions;
using Shared.Domain.Events;

namespace Shared.Infrastructure.EventBus;

public class InMemoryEventBus : IEventBus
{
    private readonly List<Func<IDomainEvent, Task>> _handlers = new();

    public Task PublishAsync(IDomainEvent domainEvent)
    {
        var tasks = _handlers.Select(h => h(domainEvent));
        return Task.WhenAll(tasks);
    }

    public void Subscribe(Func<IDomainEvent, Task> handler)
    {
        _handlers.Add(handler);
    }
}
