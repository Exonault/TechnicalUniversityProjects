using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourseWork.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataSet",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsPaidOff = table.Column<bool>(type: "boolean", nullable: false),
                    RequestStatus = table.Column<string>(type: "text", nullable: false),
                    Product = table.Column<string>(type: "text", nullable: false),
                    ApplicationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ApprovedAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    LendedAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    IsRefinance = table.Column<bool>(type: "boolean", nullable: false),
                    IsRefinanced = table.Column<bool>(type: "boolean", nullable: false),
                    IsNewClient = table.Column<bool>(type: "boolean", nullable: false),
                    Period = table.Column<int>(type: "integer", nullable: false),
                    RepaidAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    AddressRegion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSet", x => x.RequestId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataSet");
        }
    }
}
