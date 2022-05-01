using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Application;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tests.Fakes;
using Xunit;

namespace Tests;

public class EmployeesTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public EmployeesTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _jsonSerializerOptions = _factory
            .Services
            .GetRequiredService<IOptions<JsonOptions>>()
            .Value
            .JsonSerializerOptions;
    }

    [Fact]
    public async Task GivenThereAreNoEmployees_WhenGetEmployees_ThenReturnEmptyList()
    {
        HttpClient client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped(typeof(IRepository<>), typeof(FakeRepository<>));
            });
        }).CreateClient();

        HttpResponseMessage response = await client.GetAsync("/employees");
        response.EnsureSuccessStatusCode();

        List<EmployeeDto>? employees = await response.Content
            .ReadFromJsonAsync<List<EmployeeDto>>(_jsonSerializerOptions);

        employees.Should().BeEmpty();
    }
}
