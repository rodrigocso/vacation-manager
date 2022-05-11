using Application;
using Infrastructure;
using Web.Converters;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddScoped<EmployeeFacade>()
    .AddInfrastructure(builder.Configuration)
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public abstract partial class Program { }
