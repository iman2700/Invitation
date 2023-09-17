using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Members.Events
{
    internal sealed class MemberRegisterDomainEvent:IDomainEventHandler<MebmerRegisterDomainEvent>
    {
        private readonly IEmailServic _emailService;
        private readonly IMemberRepository _memberRepository;

        public MemberRegisterDomainEvent(IEmailServic emailService, IMemberRepository memberRepository)
        {
            _emailService = emailService;
            _memberRepository = memberRepository;
        }
        public async Task Handle(MebmerRegisterDomainEvent notification,CancellationToken cancellationToken) 
        {
            Member? member=await _memberRepository.GetByIdAsync(notification.MemberId,cancellationToken);
            await _emailService.RegisterEmailAsync(notification);
            if(notification.Email is null) 
            {
                return;
            }
            _emailService.SendWellcomEmailAsync(member, cancellationToken);
        }
    }
}
