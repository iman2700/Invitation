using Gatherly.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 5:25 file 11
namespace Gathering.Persistence.Interseptors
{
    public sealed class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
    {
        public ConvertDomainEventsToOutboxMessagesInterceptor()
        {
        }
        public override ValueTask<InterceptionResult<int>> SavedChangesAsync(
            DbContextEventData eventData, 
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default) 
        {
            DbContext? dbContext = eventData.Context;
            if (dbContext is  null)
            {
                return SavedChangesAsync(eventData, result, cancellationToken);
            }
            dbContext.ChangeTracker.Entries<AggregateRoot>().Select(x=> x.Entity).SelectMany(x => 
            { 
            
            });
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
    }
}
