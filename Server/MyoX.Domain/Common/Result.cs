﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyoX.Domain.Common
{
    public class Result
    {
        protected Result(bool isSuccess, Error? error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }

        public Error? Error { get; }

        public static Result Success() => new(true, Error.None);

        public static Result Failure(Error error) => new(false, error);
    }

    public class Result<T> : Result
    {
        private readonly T? _value;

        private Result(T? value, bool isSuccess, Error? error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        public T Value =>
            IsSuccess
                ? _value!
                : throw new InvalidOperationException("No value for failure result.");

        public static Result<T> Success(T value) => new(value, true, Error.None);

        public static new Result<T> Failure(Error error) => new(default, false, error);
    }
}
