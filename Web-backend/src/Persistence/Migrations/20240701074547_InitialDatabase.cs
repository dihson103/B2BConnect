using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class InitialDatabase : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Accounts",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Email = table.Column<string>(type: "varchar(50)", nullable: false),
                Password = table.Column<string>(type: "varchar(100)", nullable: false),
                IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                IsAdmin = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Accounts", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Events",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "varchar(100)", nullable: false),
                Description = table.Column<string>(type: "text", nullable: true),
                StartAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                EndAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                Status = table.Column<int>(type: "integer", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                CreatedBy = table.Column<string>(type: "text", nullable: false),
                UpdatedBy = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Events", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Industries",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "varchar(200)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Industries", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Representatives",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                GovernmentId = table.Column<string>(type: "varchar(12)", nullable: false),
                Fullname = table.Column<string>(type: "varchar(50)", nullable: false),
                Dob = table.Column<DateOnly>(type: "date", nullable: false),
                Gender = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                Address = table.Column<string>(type: "varchar(200)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Representatives", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Businesses",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                TaxCode = table.Column<string>(type: "varchar(15)", nullable: false),
                Name = table.Column<string>(type: "varchar(100)", nullable: false),
                DateOfEstablishment = table.Column<DateOnly>(type: "date", nullable: false),
                WebSite = table.Column<string>(type: "varchar(50)", nullable: true),
                AvatarImage = table.Column<string>(type: "varchar(50)", nullable: true),
                CoverImage = table.Column<string>(type: "varchar(50)", nullable: true),
                NumberOfEmployee = table.Column<int>(type: "integer", nullable: false),
                IsVerified = table.Column<bool>(type: "boolean", nullable: false),
                AccountId = table.Column<int>(type: "integer", nullable: false),
                RepresentativeId = table.Column<int>(type: "integer", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Businesses", x => x.Id);
                table.ForeignKey(
                    name: "FK_Businesses_Accounts_AccountId",
                    column: x => x.AccountId,
                    principalTable: "Accounts",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Businesses_Representatives_RepresentativeId",
                    column: x => x.RepresentativeId,
                    principalTable: "Representatives",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Branches",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Email = table.Column<string>(type: "varchar(50)", nullable: true),
                Phone = table.Column<string>(type: "varchar(10)", nullable: true),
                Address = table.Column<string>(type: "varchar(200)", nullable: false),
                IsMainBranch = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                BusinessId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Branches", x => x.Id);
                table.ForeignKey(
                    name: "FK_Branches_Businesses_BusinessId",
                    column: x => x.BusinessId,
                    principalTable: "Businesses",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Images",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Value = table.Column<string>(type: "varchar(50)", nullable: false),
                BusinessId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Images", x => x.Id);
                table.ForeignKey(
                    name: "FK_Images_Businesses_BusinessId",
                    column: x => x.BusinessId,
                    principalTable: "Businesses",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Participations",
            columns: table => new
            {
                BusinessId = table.Column<int>(type: "integer", nullable: false),
                EventId = table.Column<int>(type: "integer", nullable: false),
                JoinDate = table.Column<DateOnly>(type: "date", nullable: false),
                Status = table.Column<int>(type: "integer", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                CreatedBy = table.Column<string>(type: "text", nullable: false),
                UpdatedBy = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Participations", x => new { x.BusinessId, x.EventId });
                table.ForeignKey(
                    name: "FK_Participations_Businesses_BusinessId",
                    column: x => x.BusinessId,
                    principalTable: "Businesses",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Participations_Events_EventId",
                    column: x => x.EventId,
                    principalTable: "Events",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Sectors",
            columns: table => new
            {
                BusinessId = table.Column<int>(type: "integer", nullable: false),
                IndustryId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Sectors", x => new { x.BusinessId, x.IndustryId });
                table.ForeignKey(
                    name: "FK_Sectors_Businesses_BusinessId",
                    column: x => x.BusinessId,
                    principalTable: "Businesses",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Sectors_Industries_IndustryId",
                    column: x => x.IndustryId,
                    principalTable: "Industries",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Verifications",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                BusinessLicense = table.Column<string>(type: "varchar(50)", nullable: false),
                EstablishmentCertificate = table.Column<string>(type: "varchar(50)", nullable: false),
                Note = table.Column<string>(type: "text", nullable: true),
                IsChecked = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                BusinessId = table.Column<int>(type: "integer", nullable: false),
                CheckedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                BusinessType = table.Column<int>(type: "integer", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                CreatedBy = table.Column<string>(type: "text", nullable: false),
                UpdatedBy = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Verifications", x => x.Id);
                table.ForeignKey(
                    name: "FK_Verifications_Businesses_BusinessId",
                    column: x => x.BusinessId,
                    principalTable: "Businesses",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Accounts_Email",
            table: "Accounts",
            column: "Email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Branches_BusinessId",
            table: "Branches",
            column: "BusinessId");

        migrationBuilder.CreateIndex(
            name: "IX_Businesses_AccountId",
            table: "Businesses",
            column: "AccountId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Businesses_RepresentativeId",
            table: "Businesses",
            column: "RepresentativeId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Businesses_TaxCode",
            table: "Businesses",
            column: "TaxCode",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Images_BusinessId",
            table: "Images",
            column: "BusinessId");

        migrationBuilder.CreateIndex(
            name: "IX_Participations_EventId",
            table: "Participations",
            column: "EventId");

        migrationBuilder.CreateIndex(
            name: "IX_Sectors_IndustryId",
            table: "Sectors",
            column: "IndustryId");

        migrationBuilder.CreateIndex(
            name: "IX_Verifications_BusinessId",
            table: "Verifications",
            column: "BusinessId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Branches");

        migrationBuilder.DropTable(
            name: "Images");

        migrationBuilder.DropTable(
            name: "Participations");

        migrationBuilder.DropTable(
            name: "Sectors");

        migrationBuilder.DropTable(
            name: "Verifications");

        migrationBuilder.DropTable(
            name: "Events");

        migrationBuilder.DropTable(
            name: "Industries");

        migrationBuilder.DropTable(
            name: "Businesses");

        migrationBuilder.DropTable(
            name: "Accounts");

        migrationBuilder.DropTable(
            name: "Representatives");
    }
}
