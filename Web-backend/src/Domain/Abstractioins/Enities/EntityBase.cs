using Domain.Abstractioins.Enities.Base;

namespace Domain.Abstractioins.Enities;
public abstract class EntityBase<T> : IEntityBase<T>
{
    public T Id { get; set; }
}
