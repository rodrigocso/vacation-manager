using FluentMigrator.Runner;
using Infrastructure.Database.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Database;

public static class DatabaseInitializer
{
    public static void Init(string connectionString)
    {
        var serviceProvider = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSQLite()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(AddEmployeeTable).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);

        using var scope = serviceProvider.CreateScope();
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }
}
