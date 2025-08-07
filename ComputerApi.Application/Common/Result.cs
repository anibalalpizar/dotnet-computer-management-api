﻿namespace ComputerApi.Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();

        public static Result<T> Success(T data)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Data = data
            };
        }

        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T>
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }

        public static Result<T> Failure(List<string> errors)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Errors = errors
            };
        }
    }

    public class Result
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();

        public static Result Success()
        {
            return new Result { IsSuccess = true };
        }

        public static Result Failure(string errorMessage)
        {
            return new Result
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
