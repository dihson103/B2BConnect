namespace Domain.Abstractioins.Enities.Base;
internal interface IUserTracking
{
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
