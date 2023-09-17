using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Gathering.Commands.Invitation.Commands.AcceptInvitation
{
    public sealed record AcceptInvitationCommand(Guid GatheringId,Guid InvitationId):IRequest;
    
}
