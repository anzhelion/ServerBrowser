using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerBrowser.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PublisherId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServerLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PublisherId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServerIds = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServerReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServerId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PublisherId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerReviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PublisherId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServerType = table.Column<int>(type: "int", nullable: false),
                    IPAddress = table.Column<int>(type: "int", nullable: false),
                    LastLiveOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveTimeStart = table.Column<int>(type: "int", nullable: false),
                    ActiveTimeEnd = table.Column<int>(type: "int", nullable: false),
                    PeakConcurrentUsers = table.Column<int>(type: "int", nullable: false),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
                    IsDedicated = table.Column<bool>(type: "bit", nullable: false),
                    IsOfficial = table.Column<bool>(type: "bit", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerTypes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "ServerLists");

            migrationBuilder.DropTable(
                name: "ServerReviews");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "ServerTypes");
        }
    }
}
