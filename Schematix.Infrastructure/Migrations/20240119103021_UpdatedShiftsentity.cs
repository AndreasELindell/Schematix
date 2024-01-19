using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schematix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedShiftsentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_AspNetUsers_ManagerId1",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_AspNetUsers_EmployeeId1",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_EmployeeId1",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Branches_ManagerId1",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "ManagerId1",
                table: "Branches");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Shifts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_EmployeeId",
                table: "Shifts",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_AspNetUsers_EmployeeId",
                table: "Shifts",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_AspNetUsers_EmployeeId",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_EmployeeId",
                table: "Shifts");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Shifts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId1",
                table: "Shifts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ManagerId1",
                table: "Branches",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_EmployeeId1",
                table: "Shifts",
                column: "EmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_ManagerId1",
                table: "Branches",
                column: "ManagerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_AspNetUsers_ManagerId1",
                table: "Branches",
                column: "ManagerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_AspNetUsers_EmployeeId1",
                table: "Shifts",
                column: "EmployeeId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
