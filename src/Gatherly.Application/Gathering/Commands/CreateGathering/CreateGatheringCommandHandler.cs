using Gatherly.Domain.Repositories;
using MediatR;
 

namespace Gatherly.Application.Gathering.Commands.CreateGathering
{
    public class CreateGatheringCommandHandler:IRequestHandler<CreateGatheringCommand>
    {
        //private readonly IServiceProvider _serviceProvider;
        private readonly IMemeberRepository _memberRepository;
        private readonly IGatheringRepository _gatheringRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGatheringCommandHandler(IMemberRepository memberRepository,IGatheringRepository gatheringRepository,UnitOfWork unitOfWork)
        {
            
            _memberRepository = memberRepository;
            _gatheringRepository = gatheringRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit>Handel(CreateGatheringCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);
            if(member is null)
            {
                return Unit.Value;
            }
            //create gathering
            var gathring = Domain.Entities.Gathering.Create(Guid.NewGuid(), member, request.Type, request.ScheduleAtUtc , request.Name , request.Location ,request.MaximumNumberOfAttendees ,request.InvitationsValidBeforeInHours);
            gathring.Type = GatheringType.WithFixedNumberOfAttendees;

            _gatheringRepository.Add(gathring);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public Task Handle(CreateGatheringCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
