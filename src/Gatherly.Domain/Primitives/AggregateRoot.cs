using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Primitives
{
    public abstract class AggregateRoot: Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> GetDomainsEvent() => _domainEvents.ToList();
        public void ClearDomainEvent()
        {
            _domainEvents.Clear();
        }

        protected AggregateRoot(Guid id):base(id) 
        {

        }
        
        protected void RaiseDomainEvent(IDomainEvent domainEvent) 
        {
            _domainEvents.Add(domainEvent); 
        }
    }
}
