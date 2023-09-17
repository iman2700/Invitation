using Gatherly.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Shared
{
    public class Error : IEquatable<Error>
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Error.NullValue", "the specific result value is null.");
        public Error(string code, string message) 
        {
            Code = code;
            Message = message;
        }public Error() 
        {
             
        }
        public string Code { get; }
        public string Message { get; }
        public static implicit operator string(Error error)
        {
            return error.Code;
        }
        public static bool operator !=(Error? a, Error? b)
        {
            return !(a == b);
        }
         
        public static bool operator ==(Error? a, Error? b) 
        {
            if(a is null && b is null) 
            {
                return true;
            }
            if(a is null || b is null)
            {
                return false;
            }
            return false;
        }

        public bool Equals(Error? other)
        {
            throw new NotImplementedException();
        }
    }
}
