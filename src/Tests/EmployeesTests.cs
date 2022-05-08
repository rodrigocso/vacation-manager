using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                services.AddScoped<IEmployeeRepository, FakeEmployeeRepository>();
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
        List<Employee> existingEmployees = _fixture.CreateMany<Employee>(5).ToList();
        FakeEmployeeRepository.AddMany(existingEmployees);

        HttpResponseMessage response = await _client.GetAsync("/employees");
        response.EnsureSuccessStatusCode();

        List<EmployeeDto>? employees = await response.Content
            .ReadFromJsonAsync<List<EmployeeDto>>(_jsonSerializerOptions);

        employees.Should().BeEquivalentTo(existingEmployees.Select(e => e.ToDto()));
        FakeEmployeeRepository.Clear();
    }

    [Fact]
    public async Task CanSaveNewEmployee()
    {
        var employeeDto = _fixture.Create<EmployeeDto>();

        HttpResponseMessage response = await _client
            .PostAsJsonAsync("/employees", employeeDto, _jsonSerializerOptions);
        response.EnsureSuccessStatusCode();

        FakeEmployeeRepository.Employees.Should().Contain(employee => employee.Id == employeeDto.Id);
        FakeEmployeeRepository.Clear();
    }

    [Fact]
    public async Task CanFindEmployeeById()
    {
        var employee = _fixture.Create<Employee>();
        FakeEmployeeRepository.Employees.Add(employee);

        HttpResponseMessage response = await _client.GetAsync($"/employees/{employee.Id}");
        response.EnsureSuccessStatusCode();

        EmployeeDto? employeeDto = await response.Content.ReadFromJsonAsync<EmployeeDto>(_jsonSerializerOptions);

        employeeDto?.Id.Should().Be(employee.Id);
    }

    [Fact]
    public async Task GivenEmployeeDoesNotExist_WhenRequestEmployeeById_ThenRespondWith404()
    {
        HttpResponseMessage response = await _client.GetAsync("/employees/1");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
