using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyStatistics.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ActivitiesModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityGroups_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityKinds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityKinds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityKinds_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DayRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayRecords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityGroupMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrackingActivityGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrackingActivityKindId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Coefficient = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityGroupMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityGroupMembers_ActivityGroups_TrackingActivityGroupId",
                        column: x => x.TrackingActivityGroupId,
                        principalTable: "ActivityGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityGroupMembers_ActivityKinds_TrackingActivityKindId",
                        column: x => x.TrackingActivityKindId,
                        principalTable: "ActivityKinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrackingActivityKindId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityRecords_ActivityKinds_TrackingActivityKindId",
                        column: x => x.TrackingActivityKindId,
                        principalTable: "ActivityKinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityRecords_DayRecords_DayRecordId",
                        column: x => x.DayRecordId,
                        principalTable: "DayRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityGroupMembers_TrackingActivityGroupId",
                table: "ActivityGroupMembers",
                column: "TrackingActivityGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityGroupMembers_TrackingActivityKindId",
                table: "ActivityGroupMembers",
                column: "TrackingActivityKindId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityGroups_UserId",
                table: "ActivityGroups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityKinds_UserId",
                table: "ActivityKinds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityRecords_DayRecordId",
                table: "ActivityRecords",
                column: "DayRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityRecords_TrackingActivityKindId",
                table: "ActivityRecords",
                column: "TrackingActivityKindId");

            migrationBuilder.CreateIndex(
                name: "IX_DayRecords_UserId",
                table: "DayRecords",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityGroupMembers");

            migrationBuilder.DropTable(
                name: "ActivityRecords");

            migrationBuilder.DropTable(
                name: "ActivityGroups");

            migrationBuilder.DropTable(
                name: "ActivityKinds");

            migrationBuilder.DropTable(
                name: "DayRecords");
        }
    }
}
