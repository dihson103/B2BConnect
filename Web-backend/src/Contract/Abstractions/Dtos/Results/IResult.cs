namespace Contract.Abstractions.Dtos.Results;
public abstract class IResult
{
    public int Status { get; set; }
    public string Message { get; set; }
    public bool IsSuccess { get; set; }
}

