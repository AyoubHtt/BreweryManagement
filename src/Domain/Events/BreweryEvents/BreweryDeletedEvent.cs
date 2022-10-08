using Domain.AggregatesModel.BreweryAggregate;

namespace Domain.Events.BreweryEvents;

public record BreweryDeletedEvent(Brewery Brewery) : INotification;
