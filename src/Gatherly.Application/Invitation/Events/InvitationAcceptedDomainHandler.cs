using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Invitation.Events
{
    internal sealed class InvitationAcceptedDomainHandler : INotificationHandler<InvitationAcceptedDomainEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IGatheringRepository _gatheringRepository;
        public async Task Handle(InvitationAcceptedDomainEvent notification, CancellationToken cancellationToken)
        {
             var gathering = await _gatheringRepository.GetByIdWithCreatorAsync(notification.GatheringId, cancellationToken);
            if (gathering is null) 
            {
                return;
            }
                await _emailServic.SendInvitationAcceptedEmailAsync(ghather.Id, attondee);
             
        }
    }
}
