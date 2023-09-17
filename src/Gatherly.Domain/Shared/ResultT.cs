using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Shared
{
    public class TResult<TValue> : Result
    {
        private readonly TValue _value;
        protected internal TResult( bool isSuccess, Error error, TValue? value) : base(isSuccess, error)
        {
            _value = value;
        }
        public TValue Value
        {
            get
            {
                return IsSuccess ? _value! : throw new InvalidOperationException("The value of a failure result can not accessed");
            }
        }
        public static implicit operator TResult<TValue>(TValue? value) => Create(value);

        
    }
}
