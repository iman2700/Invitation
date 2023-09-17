using Gatherly.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Entities
{
    public sealed class Invitation : Entity
    {
        public Invitation(Guid id, Member member, Gathering gathering) : base(id)
        {

            GatheringId = gathering.Id;
            MemberId = member.Id;
            Status = InvitationStatus.Panding;
            CreatedOnUtc = DateTime.UtcNow;
        }

        public Guid GatheringId { get; private set; }
        public Guid MemberId { get; private set; }
        public InvitationStatus Status { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime? ModifiedOnUtc { get; private set; }
        internal void Expire()
        {
             Status = InvitationStatus.Expired;
            ModifiOnUtc = DateTime.UtcNow;
           
        }
        internal Attendee Accept()
        {
            Status = InvitationStatus.Accepted;
            ModifiedOnUtc = DateTime.UtcNow;
            var attendee=new Attendee(this);
            return attendee;

        }
    }
}
