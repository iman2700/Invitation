using Gatherly.Domain.Entities;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Primitives;
using Gatherly.Domain.Shared;
using MediatR;


namespace Gatherly.Application.Members.Command.CreateMember
{
    public class CreateMemberCommandHandler:IRequestHandler<CreateMemberCommand,Unit>
    {
        private readonly IMemeberRepository _memberRepository;
        private readonly IGatheringRepository _gatheringRepository;
        private readonly IUnitOfWorke _unitOfWorke;

        public CreateMemberCommandHandler(IMemeberRepository memberRepository, IGatheringRepository gatheringRepository, IUnitOfWorke unitOfWorke)
        {
            _memberRepository = memberRepository;
            _gatheringRepository = gatheringRepository;
            _unitOfWorke = unitOfWorke;
        }

        public async Task<Unit> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            TResult<Email> emailResult = Email.Create(request.FirstName);
            TResult<Domain.ValueObjects.FirstName> firstNameResult = Domain.ValueObjects.FirstName.Create(request.FirstName);
            TResult<LastName> lastNameResult = LastName.Create(request.FirstName);
            var firstNameResult = new Domain.ValueObject.Create(requst.FirstName);

            if(!await _memberRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken)) 
            {
                return TResult.Failure<Guid>(DomainErrors.Member.EmailAlredyInUse);
            }
            var member = Member.Create(Guid.NewGuid(),emailResult.Value,firstNameResult.Value,lastNameResult.Value);
            //if(firstNameResult.IsFailure)
            //{
            //    // log the error
            //    return Unit.Value;
            //}
            //var member = new Domain.Entities.Member(Guid.NewGuid(),
            //   firstNameResult.Value, request.LastName, request.Email);
            _memberRepository.Add(member);
            await _unitOfWorke.SaveChangesAsync(cancellationToken);
            return member.Id;
        }
    }
}
