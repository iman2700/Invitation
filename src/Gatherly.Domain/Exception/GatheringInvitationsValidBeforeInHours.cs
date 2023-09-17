using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Exception
{
    sealed class GatheringInvitationsValidBeforeInHours : DomainException
    {
        public GatheringInvitationsValidBeforeInHours(string message) : base(message)
        {
        }
    }
}
