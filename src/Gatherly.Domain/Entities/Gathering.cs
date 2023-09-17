using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Exception;
using Gatherly.Domain.Primitives;
using Gatherly.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Entities
{
    sealed class Gathering : AggregateRoot
    {
        private Gathering(Guid id, Member creator, GatheringType type, string name, DateTime scheduledAtUtc, string? location, int maximumNumberOfAttendees) : base(id)
        {

            Creator = creator;
            Type = type;
            Name = name;
            ScheduledAtUtc = scheduledAtUtc;
            Location = location;
            MaximumNumberOfAttendees = maximumNumberOfAttendees;
            InvitationsExpireAtUtc = invitationsExpireAtUtc;
            NumberOfAttendees = numberOfAttendees;
            Attendee = attendee;

        }
        private readonly List<Invitation> _invitations = new();
        private readonly List<Attendee> _attendee = new();

        public Member Creator { get; private set; }
        public GatheringType Type { get; private set; }
        public string Name { get; private set; }
        public DateTime ScheduledAtUtc { get; private set; }
        public string? Location { get; private set; }
        public int? MaximumNumberOfAttendees { get; private set; }
        public DateTime? InvitationsExpireAtUtc { get; private set; }
        public int NumberOfAttendees { get; private set; }
        public IReadOnlyCollection<Attendee> Attendee
        {
            get
            {
                return _attendee;
            }
        }
        public IReadOnlyCollection<Invitation> Invitations
        {
            get
            {
                return _invitations;
            }
        }

        public static Gathering Create(Guid id, Member creator, GatheringType type, string name, DateTime scheduledAtUtc, string location, int maximumNumberOfAttendees, int invitationsValidBeforeInHours)
        {
            var gathring = new Gathering(
                Guid.NewGuid(),
                member,
                type,
                name,
                scheduledAtUtc,
                location, maximumNumberOfAttendees);
            switch (gathring.Type)
            {
                case GathringType.withFixedNumberOfAttendees:
                    if (maximumNumberOfAttendees is null)
                    {
                        throw new GatheringMaximumNumberOfAttendeesIsNullDomainException($"{nameof(maximumNumberOfAttendees)} can not be null");
                    }
                    gathring.MaximumNumberOfAttendees = maximumNumberOfAttendees;
                    break;

                case GathringType.WithExpirationForInvitations:
                    if (invitationsValidBeforeInHours is null)
                    {
                        throw new GatheringInvitationsValidBeforeInHours($"{nameof(invitationsValidBeforeInHours)} can not be null");
                    }
                    gathring.InvitationsExpireAtUtc = gathring.ScheduledAtUtc.AddHours(-invitationsValidBeforeInHours.Value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gathring));

                    return gathring;
            }

        }

        public TResult<Invitation> SendInvitation(Member member)
        {
            if (Creator.Id == member.Id)
            {
                return TResult.Failure<Invitation>(new Error("Gathering.InvitationCreator","can not send Invitation to gathering creator."));
                  
            }
            if (ScheduledAtUtc < DateTime.UtcNow)
            {
                return TResult.Failure<Invitation>(new Error("Gathering.AlreadyPassed", "can not send Invitation to gathering creator."));
            }
            var invitation = new Invitation(Guid.NewGuid(), member, this);
            _invitations.Add(invitation);
            return invitation;

        }
        public TResult<Attendee> AcceptInvitation(Invitation invitation)
        {
            var reachMaxNumberOfAttendees = Type == GathringType.WithFixedNumberOfAttendees && NumberOfAttendees ==
                MaximumNumberOfAttendees;
            var reachExpirationForInvitation = Type == GatheringType.WithExpirationForInvitation &&
                InvitationsExpireAtUtc < DateTime.UtcNow;


            var expire = reachMaxNumberOfAttendees || reachExpirationForInvitation;
                 
            if (expire)
            {
                invitation.Expire();
                return TResult.Failure<Attendee>(DomainErrors.Gathering.Expired);
            }

            var attendee = invitation.Accept(); 
            RaiseDomainEvent(new InvitationAcceptedDomainEvent(invitation.Id, Id));
            _attendee.Add(attendee);
            NumberOfAttendees++;
            return attendee;
        }
    }
}
