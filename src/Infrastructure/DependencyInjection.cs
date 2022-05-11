using Application;
using Infrastructure.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        DatabaseInitializer.Init(config.GetConnectionString("VacationDb"));

        return services
            .AddScoped<IEmployeeRepository, EmployeeRepository>();
    }
}
