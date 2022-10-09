using API.Application.Commands.BreweryCommands;
using AutoFixture;
using FunctionalTests.TestPriorityOrder;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit;

namespace FunctionalTests.FunctionalTest;

[Collection("Sequential")]
public class BreweriesControllerTest : HasOrdredTest, IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public BreweriesControllerTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _factory.Server.AllowSynchronousIO = true;
    }

    [Fact, TestPriority(1)]
    public async Task Create_brewery_should_return_success()
    {
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

        var fixture = new Fixture();

        var request = fixture.Create<CreateBreweryCommand>();

        var json = JsonSerializer.Serialize(request);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync("api/Breweries", data);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    [Fact, TestPriority(2)]
    public async Task Get_brewery_should_return_success()
    {
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

        HttpResponseMessage response = await client.GetAsync("api/Breweries");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // Save brewery id to use in update and delete
        var result = await response.Content.ReadAsStringAsync();
        Guid breweryId = JsonDocument.Parse(result).RootElement.EnumerateArray().First().GetProperty("id").GetGuid();
        _factory.RequestData.Add("breweryId", breweryId);
    }

    [Fact, TestPriority(3)]
    public async Task Update_brewery_should_return_success()
    {
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

        var fixture = new Fixture();

        var request = fixture.Build<UpdateBreweryCommand>()
                             .With(command => command.Id, _factory.RequestData["breweryId"])
                             .Create();

        var json = JsonSerializer.Serialize(request);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PutAsync("api/Breweries", data);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact, TestPriority(4)]
    public async Task Delete_brewery_should_return_success()
    {
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

        var request = "api/Breweries/" + _factory.RequestData["breweryId"];

        HttpResponseMessage response = await client.DeleteAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
