using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Gathering.Commands.CreateGathering
{
    public sealed record CreateGatheringCommand(
        Guid MemberId,
        GatheringType Type,
        DateTime ScheduleAtUtc,
        string Name
        ,string Location
        ,int MaximumNumberOfAttendees
        ,int InvitationsValidBeforeInHours):IRequest;
    
    
}
