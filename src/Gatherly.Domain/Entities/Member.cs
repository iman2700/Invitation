using Gatherly.Domain.Primitives;
using Gatherly.Domain.ValueObject;
using Gatherly.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Entities
{
   public sealed class Member : Entity
    {
        public FirstName FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public Member(Guid id, FirstName firstName, string lastName, string email):base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public static Member Create(Guid id,Email email,FirstName firstName, LastName lastName) 
        {
            var member=new Member(id,firstName,lastName);
            member.RaiseDomainEvent(new MemberRegisterDomainEvent(member.Id));
            return member;
        }
    }
}
