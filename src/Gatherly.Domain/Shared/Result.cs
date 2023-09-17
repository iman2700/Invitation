using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Gatherly.Domain.Shared
{
    public class Result
    {
        protected internal Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException();
            }
            if (!isSuccess && error != Error.None)
            {
                throw new InvalidOperationException();
            }
            Error = error;
            IsSuccess = isSuccess;
        }
        public bool IsSuccess { get; }
        public bool IsFailure
        {
            get
            {
                return !IsSuccess;
            }
        }
        public Error Error { get; }

        public static Result Success()
        {
            return new Result(true, Error.None);
        }
        public static <TValue> Success<TValue>(TValue value)=> new () ;
        

    }
}
