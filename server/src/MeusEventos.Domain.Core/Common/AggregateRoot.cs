using System;

namespace MeusEventos.Domain.Core.Common
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }

    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
    }
}
