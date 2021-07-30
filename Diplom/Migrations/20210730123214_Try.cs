using Microsoft.EntityFrameworkCore.Migrations;

namespace Diplom.Migrations
{
    public partial class Try : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MessageStatus_MessagesId",
                table: "MessageStatus");

            migrationBuilder.AlterColumn<long>(
                name: "MessagesId",
                table: "MessageStatus",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageStatus_MessagesId",
                table: "MessageStatus",
                column: "MessagesId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MessageStatus_MessagesId",
                table: "MessageStatus");

            migrationBuilder.AlterColumn<long>(
                name: "MessagesId",
                table: "MessageStatus",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_MessageStatus_MessagesId",
                table: "MessageStatus",
                column: "MessagesId",
                unique: true,
                filter: "[MessagesId] IS NOT NULL");
        }
    }
}
