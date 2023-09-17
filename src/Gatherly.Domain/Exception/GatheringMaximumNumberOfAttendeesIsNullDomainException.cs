using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Exception
{
    sealed class GatheringMaximumNumberOfAttendeesIsNullDomainException : DomainException
    {
        public GatheringMaximumNumberOfAttendeesIsNullDomainException(string message) : base(message)
        {
        }
    }
}
