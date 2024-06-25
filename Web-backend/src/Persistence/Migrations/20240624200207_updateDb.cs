using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class updateDb : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_Businesses_AccountId",
            table: "Businesses");

        migrationBuilder.CreateIndex(
            name: "IX_Businesses_AccountId",
            table: "Businesses",
            column: "AccountId",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_Businesses_AccountId",
            table: "Businesses");

        migrationBuilder.CreateIndex(
            name: "IX_Businesses_AccountId",
            table: "Businesses",
            column: "AccountId");
    }
}
