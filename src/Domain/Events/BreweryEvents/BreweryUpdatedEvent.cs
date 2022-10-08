using Domain.AggregatesModel.BreweryAggregate;

namespace Domain.Events.BreweryEvents;

public record BreweryUpdatedEvent(Brewery Brewery) : INotification;
