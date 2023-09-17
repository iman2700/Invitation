using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Gathering.Commands.Invitation.Commands.AcceptInvitation
{
    public sealed class AcceptInvitationCommandHandler : IRequestHandler<AcceptInvitationCommand>
    {
        private readonly IEmailServic _emailService;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IUnitOfWorke _unitOfWork;
        private readonly IAttendeeRepository _attendeeRepository;
        
        private readonly IGatheringRepository _gatheringRepository;

        public AcceptInvitationCommandHandler(
            IInvitationRepository invitationRepository,
            IGatheringRepository gatheringRepository,
            IAttendeeRepository attendeeRepository,
            IEmailServic emailServic,
            IUnitOfWorke unitOfWork    
           )
        {
            _invitationRepository = invitationRepository;
            _emailServic = emailService;
            _unitOfWorke = unitOfWork;
            _attendeeRepository = attendeeRepository;
            _gatheringRepository = gatheringRepository;
        }

        public async Task<Unit> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
        {
            var gathering = _gatheringRepository.GetByIdWithCreatorAsync(request.GatheringId, cancellationToken);
            if (gathering is null)
            {
                return Unit.Value;
            }
            //var invitation = await _invitationRepository.GetByIdAsync(request.InvitationId, cancellationToken);
            var invitation= gathering.Invitation.FirstOrDefault(i => i.id==request.InvitationId);
            
            if (invitation is null || invitation.Status != InvitationStatus.Pending)
            {
                return Unit.Value;
            }
            var member = await _memberRepository.GetByIdAsync(request.GatheringId, cancellationToken);


            TResult<Attendee> attendeeResult = gathering.AcceptInvitation(invitation);
            if (attendeeResult.IsSuccess) 
            {
                _attendeeRepository.Add(attendeeResult.Value);
            }
            var attendee = gathering.AcceptInvitation(invitation);
            //if (attendee is not null)
            //{
            //    _attendeeRepository.Add(attendee);

            //}
            
            
            await _unitOfWorke.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
