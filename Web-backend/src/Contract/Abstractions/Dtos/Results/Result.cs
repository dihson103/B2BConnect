﻿using System.Net;

namespace Contract.Abstractions.Dtos.Results;
public class Result
{
    public class Success : IResult
    {
        private Success() { }

        public static Success Create(string message = "Tạo thành công") => new Success()
        {
            Status = (int)HttpStatusCode.Created,
            Message = message,
            IsSuccess = true
        };

        public static Success Update(string message = "Chỉnh sửa thành công") => new Success()
        {
            Status = (int)HttpStatusCode.OK,
            Message = message,
            IsSuccess = true
        };

        public static Success Delete(string message = "Xóa thành công") => new Success()
        {
            Status = (int)HttpStatusCode.OK,
            Message = message,
            IsSuccess = true
        };
    }

    public class Success<TData> : IResult
    {
        public TData Data { get; set; }
        private Success() { }
        public static Success<TData> Get(TData tData, string message = "Lấy dữ liệu thành công") 
            => new Success<TData>()
            {
                Status = (int)HttpStatusCode.OK,
                Message = message,
                Data = tData,
                IsSuccess = true
            };

        public static Success<TData> ActionCommand(TData tData, string message = "Hành động thành công")
            => new Success<TData>()
            {
                Status = (int)HttpStatusCode.OK,
                Message = message,
                Data = tData,
                IsSuccess = true
            };
    }

}
