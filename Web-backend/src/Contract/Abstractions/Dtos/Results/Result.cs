using System.Net;

namespace Contract.Abstractions.Dtos.Results;
public class Result
{
    public class Success : IResult
    {
        private Success() { }

        public static Success Create() => new Success()
        {
            Status = (int)HttpStatusCode.Created,
            Message = "Action create success",
            IsSuccess = true
        };

        public static Success Update() => new Success()
        {
            Status = (int)HttpStatusCode.OK,
            Message = "Action update success",
            IsSuccess = true
        };

        public static Success Delete() => new Success()
        {
            Status = (int)HttpStatusCode.OK,
            Message = "Action delete success",
            IsSuccess = true
        };

        public static Success Logout() => new Success()
        {
            Status = (int)HttpStatusCode.OK,
            Message = "Action logout success",
            IsSuccess = true
        };

        public static Success RequestForgetPassword() => new Success()
        {
            Status = (int)HttpStatusCode.OK,
            Message = "Action forget password request success",
            IsSuccess = true
        };
    }

    public class Success<TData> : IResult
    {
        public TData Data { get; set; }
        private Success() { }
        public static Success<TData> Get(TData tData) => new Success<TData>()
        {
            Status = (int)HttpStatusCode.OK,
            Message = "Action get success",
            Data = tData,
            IsSuccess = true
        };

        public static Success<TData> Login(TData tData) => new Success<TData>()
        {
            Status = (int)HttpStatusCode.OK,
            Message = "Action login success",
            Data = tData,
            IsSuccess = true
        };

        public static Success<TData> Upload(TData tData) => new Success<TData>()
        {
            Status = (int)HttpStatusCode.Created,
            Message = "Action upload file success",
            Data = tData,
            IsSuccess = true
        };
    }

    public class Failure : IResult
    {
    }

    public class Failure<TErrors> : IResult
    {
        public TErrors errors { get; set; }
    }
}
