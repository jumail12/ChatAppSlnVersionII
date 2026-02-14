using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Shared.ApiResponses
{
    public interface IApiResult
    {
        ResultType ResultType { get; set; }
        string Message { get; set; }
    }
    public class BaseApiExeResult : IApiResult
    {
        public ResultType ResultType { get; set; }
        public string Message { get; set; }
    }

    public class SucessResult<T> : IApiResult
    {
        public SucessResult(T result)
        {
            Data = result;
        }
        public ResultType ResultType { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }


    public class PaginatedApiExeResult<T> : IApiResult
    {
        public PaginatedApiExeResult(T resultData)
        {
            ResultData = new PaginationResultData<T>
            {
                Data = resultData
            };
        }
        public ResultType ResultType { get; set; }
        public string Message { get; set; }
        public PaginationResultData<T> ResultData { get; set; }
    }

    public class PaginationResultData<T>
    {
        public int PageNo { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public T Data { get; set; }
    }

    public class FetchApiExeResult<T> : IApiResult
    {
        public ResultType ResultType { get; set; }
        public string Message { get; set; }
        public ResultData<T> Result { get; set; }
    }

    public class ResultData<T>
    {
        public T Data { get; set; }
    }

    public class ApiExceptionResponse<T> : IApiResult
    {
        public ResultType ResultType { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public List<T> Errors { get; set; }
    }

    public class ApiExceptionResponse : IApiResult
    {
        public ResultType ResultType { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
    }


    public class FieldError
    {
        public string Field { get; set; }
        public string[] Messages { get; set; }
    }

    public enum ResultType
    {
        Success,
        PSQLError,
        ValidationException,
        NotFound,
        NoData,
        Error,
        UnAuthorized,
        Forbidden
    }
}
