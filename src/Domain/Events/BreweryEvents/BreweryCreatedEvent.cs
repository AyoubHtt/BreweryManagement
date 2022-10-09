using Domain.AggregatesModel.BreweryAggregate;

namespace Domain.Events.BreweryEvents;

public record BreweryCreatedEvent(Brewery Brewery) : INotification;
