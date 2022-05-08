using Application;
using Infrastructure;
using Web.Converters;

var builder = WebApplication.CreateBuilder(args);

// Application injection
builder.Services.AddScoped<EmployeeFacade>();

// Infrastructure injection
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
