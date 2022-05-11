using FluentMigrator;

namespace Infrastructure.Database.Migrations;

[Migration(20220510225500)]
public class AddEmployeeTable : Migration
{
    public override void Up() => Create.Table("Employee")
        .WithColumn("Id").AsGuid().PrimaryKey()
        .WithColumn("FirstName").AsString().NotNullable()
        .WithColumn("LastName").AsString().NotNullable()
        .WithColumn("StartDate").AsDate().NotNullable();

    public override void Down() => Delete.Table("Employee");
}
