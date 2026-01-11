
using Shared.Application.Abstractions;

namespace Shared.Infrastructure.IdGeneration;

public class GuidIdGenerator : IIdGenerator
{
    public Guid NewId() => Guid.NewGuid();
}