namespace Domain.Abstractioins.Enities.Base;

internal interface IEntityBase<T>
{
    public T Id { get; set; }
}
