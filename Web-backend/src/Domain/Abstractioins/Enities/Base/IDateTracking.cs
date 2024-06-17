namespace Domain.Abstractioins.Enities.Base;
internal interface IDateTracking
{
    DateTime CreatedDate { get; set; }
    DateTime? UpdatedDate { get; set; }
}
