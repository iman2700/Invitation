using Gatherly.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.ValueObjects
{
   public sealed class FirstName : Primitives.ValueObject
    {
        public const int MaxLength = 50;
        private FirstName(string value) 
        {
            if(value.Length > MaxLength)
            {
                throw new ArgumentException();
            }
            Value = value; 
        }
        public string Value { get;  }
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static TResult<FirstName> Create(string firstName) 
        {
            if (string.IsNullOrWhiteSpace(firstName)) 
            {
                return TResult.Failure<FirstName>(new Error("FirstName.empty", "FirstName is empty"));
            }
            if(firstName.Length > MaxLength) 
            {
                return TResult.Failure<FirstName>(new Error("FirstName.istooLong", "First Name is too long"));
            }
            return new FirstName(firstName);
        }
    }
}
