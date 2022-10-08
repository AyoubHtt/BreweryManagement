using Infrastructure.EventLogEF.Events;
using MediatR;

namespace Infrastructure.EventLogEF.Extensions;

static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, BreweryContext ctx)
    {
        // Retrieve entities that has changes
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.GetDomainEvents() != null && x.Entity.GetDomainEvents().Any());

        // Retrieve domain events
        var eventLogs = domainEntities
            .Select(x => new EventLogs(x.Entity.GetDomainEvents().ToList(), x.Entity.Id))
            .ToList();

        // Clear all domain events
        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        //Log events
        eventLogs.ForEach(async eventLog => await mediator.Publish(eventLog));

        // publish all notifications
        foreach (var domainEvent in eventLogs.SelectMany(c => c.LogEvents))
            await mediator.Publish(domainEvent);
    }
}
