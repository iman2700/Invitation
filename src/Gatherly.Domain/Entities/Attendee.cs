using Gatherly.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Entities
{
     
    public sealed class Attendee: Entity
    {
        internal Attendee(Invitation invitation)
        {
            GatheringId = invitation.GatheringId;
            MemberId = invitation.MemberId;
            CreateOnUtx = DateTime.UtcNow;
        }

        public Guid GatheringId { get; private set; }
        public Guid MemberId { get; private set; }
        public DateTime CreateOnUtx { get; private set; }
    }
}
