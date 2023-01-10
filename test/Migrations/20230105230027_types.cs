using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test.Migrations
{
    /// <inheritdoc />
    public partial class types : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Assistances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assistances_ScheduleID",
                table: "Assistances",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_Assistances_WorkplaceID",
                table: "Assistances",
                column: "WorkplaceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Assistances_Schedules_ScheduleID",
                table: "Assistances",
                column: "ScheduleID",
                principalTable: "Schedules",
                principalColumn: "ScheduleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assistances_Workplaces_WorkplaceID",
                table: "Assistances",
                column: "WorkplaceID",
                principalTable: "Workplaces",
                principalColumn: "WorkplaceID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assistances_Schedules_ScheduleID",
                table: "Assistances");

            migrationBuilder.DropForeignKey(
                name: "FK_Assistances_Workplaces_WorkplaceID",
                table: "Assistances");

            migrationBuilder.DropIndex(
                name: "IX_Assistances_ScheduleID",
                table: "Assistances");

            migrationBuilder.DropIndex(
                name: "IX_Assistances_WorkplaceID",
                table: "Assistances");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Assistances");
        }
    }
}
