
using Shared.Application.Abstractions;

namespace Shared.Infrastructure.Clock;

public class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}