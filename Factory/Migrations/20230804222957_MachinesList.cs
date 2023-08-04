using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Factory.Migrations
{
    public partial class MachinesList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EngineerId",
                table: "Machines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Machines_EngineerId",
                table: "Machines",
                column: "EngineerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Engineers_EngineerId",
                table: "Machines",
                column: "EngineerId",
                principalTable: "Engineers",
                principalColumn: "EngineerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Engineers_EngineerId",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Machines_EngineerId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "EngineerId",
                table: "Machines");
        }
    }
}
