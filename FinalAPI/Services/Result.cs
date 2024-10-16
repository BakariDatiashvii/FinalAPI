namespace FinalAPI.Services
{
    public class Result
    {
        public string Message { get; set; }

        public object Response { get; set; }
        public bool IsSuccess { get; set; }

        public ResultStatus Status { get; set; } = ResultStatus.Success;

        public static Result Success()
        {
            return new Result(ResultStatus.Success);
        }

        public static Result Success(string message)
        {
            return new Result(message, ResultStatus.Success);
        }

        public static Result Success(object responce)
        {
            return new Result(ResultStatus.Success, responce);
        }

        public static Result Success(AuthResponseDto dto)
        {
            return new Result(dto);
        }

        public static Result Success(bool isSuccess)
        {
            return new Result(isSuccess);
        }

        public static Result Error(string message)
        {
            return new Result(message, ResultStatus.Error);
        }

        public static Result AccessDenied(string message)
        {
            return new Result(message, ResultStatus.AccessDenied);
        }

        public static Result Unauthorized(string message)
        {
            return new Result(message, ResultStatus.Unauthorized);
        }

        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public Result(AuthResponseDto dto)
        {
            Message = dto.Message;
            IsSuccess = dto.IsSuccess;
            Response = dto.Token;

        }

        public Result(ResultStatus status)
        {
            Status = status;
        }

        public Result(ResultStatus status, object responce)
        {
            Status = status;
            Response = responce;
        }

        public Result(string message, ResultStatus status)
        {
            Message = message;
            Status = status;
        }
    }
}
