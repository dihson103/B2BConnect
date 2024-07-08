using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class updateOneToOneRelation : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Businesses_Representatives_RepresentativeId",
            table: "Businesses");

        migrationBuilder.DropIndex(
            name: "IX_Businesses_RepresentativeId",
            table: "Businesses");

        migrationBuilder.AddColumn<Guid>(
            name: "BusinessId",
            table: "Representatives",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.CreateIndex(
            name: "IX_Representatives_BusinessId",
            table: "Representatives",
            column: "BusinessId",
            unique: true);

        migrationBuilder.AddForeignKey(
            name: "FK_Representatives_Businesses_BusinessId",
            table: "Representatives",
            column: "BusinessId",
            principalTable: "Businesses",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Representatives_Businesses_BusinessId",
            table: "Representatives");

        migrationBuilder.DropIndex(
            name: "IX_Representatives_BusinessId",
            table: "Representatives");

        migrationBuilder.DropColumn(
            name: "BusinessId",
            table: "Representatives");

        migrationBuilder.CreateIndex(
            name: "IX_Businesses_RepresentativeId",
            table: "Businesses",
            column: "RepresentativeId",
            unique: true);

        migrationBuilder.AddForeignKey(
            name: "FK_Businesses_Representatives_RepresentativeId",
            table: "Businesses",
            column: "RepresentativeId",
            principalTable: "Representatives",
            principalColumn: "Id");
    }
}
