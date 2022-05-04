using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Application;
using AutoFixture;
using Domain;
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
    private readonly Fixture _fixture;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly HttpClient _client;

    public EmployeesTests(WebApplicationFactory<Program> factory)
    {
        _fixture = new Fixture();
        _fixture.Register(() => DateOnly.FromDateTime(DateTime.Today));

        _jsonSerializerOptions = factory
            .Services
            .GetRequiredService<IOptions<JsonOptions>>()
            .Value
            .JsonSerializerOptions;

        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped(typeof(IRepository<>), typeof(FakeRepository<>));
            });
        }).CreateClient();
    }

    [Fact]
    public async Task GivenThereAreNoEmployees_WhenGetEmployees_ThenReturnEmptyList()
    {
        HttpResponseMessage response = await _client.GetAsync("/employees");
        response.EnsureSuccessStatusCode();

        List<EmployeeDto>? employees = await response.Content
            .ReadFromJsonAsync<List<EmployeeDto>>(_jsonSerializerOptions);

        employees.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenEmployeesExist_WhenGetEmployees_ThenReturnEmployeesList()
    {
        var repository = new FakeRepository<Employee>();
        repository.AddMany(_fixture.CreateMany<Employee>(5));

        HttpResponseMessage response = await _client.GetAsync("/employees");
        response.EnsureSuccessStatusCode();

        List<EmployeeDto>? employees = await response.Content
            .ReadFromJsonAsync<List<EmployeeDto>>(_jsonSerializerOptions);

        employees.Should().HaveCount(5);
    }
}
