using Gatherly.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Gathering.Commands.SendInvitation
{
    sealed class SendInvitationCommandHandler : IRequestHandler<SendInvitationCommand>
    {
       
        private readonly EmailServic _emailServic;
        private readonly IUnitOfWorke _unitOfWorke;
        private readonly InvitaionRepository _invitaionRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IGathreingRepository _gatheringRepository;

        public SendInvitationCommandHandler(EmailServic emailServic, IUnitOfWorke unitOfWorke, InvitaionRepository invitaionRepository , IMemberRepository memberRepository) 
        {
            _emailServic = emailServic;
            _unitOfWorke = unitOfWorke;
            _invitaionRepository = invitaionRepository;
            _memberRepository = memberRepository;
        }

        public async Task<Unit> Handle(SendInvitationCommand request, CancellationToken cancellationToken)
        {
          var member= await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);
            var gathring = _gatheringRepository.GetByIdWithCreatorAsync(request.GatheringId, cancellationToken);
            if (member is null || gathring is null)
            {
                return Unit.Value; 
            }
            TResult<Domain.Entities.Invitation> invitationResult = gathring.SendInvitation(member);
            if (invitationResult.IsFailure) 
            {
               return Unit.Value;   
            }

            _invitaionRepository.Add(invitationResult.Value);
            await _unitOfWorke.SaveChangesAsync(cancellationToken);
            await _emailServic.SendInvitationSendEmail(member,gathring, cancellationToken);
            return Unit.Value;

        }
    }
}
