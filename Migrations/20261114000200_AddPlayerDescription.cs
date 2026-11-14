using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using wt_lab8_8razor_postgresql_denisenko.Data;

#nullable disable

namespace wt_lab8_8razor_postgresql_denisenko.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20261114000200_AddPlayerDescription")]
public partial class AddPlayerDescription : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Description",
            table: "Players",
            type: "character varying(500)",
            maxLength: 500,
            nullable: false,
            defaultValue: "");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "Description", table: "Players");
    }
}
