using Microsoft.EntityFrameworkCore.Migrations;

namespace Diplom.Migrations
{
    public partial class FixMessageRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MessageStatus_MessagesId",
                table: "MessageStatus");

            migrationBuilder.CreateIndex(
                name: "IX_MessageStatus_MessagesId",
                table: "MessageStatus",
                column: "MessagesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MessageStatus_MessagesId",
                table: "MessageStatus");

            migrationBuilder.CreateIndex(
                name: "IX_MessageStatus_MessagesId",
                table: "MessageStatus",
                column: "MessagesId",
                unique: true);
        }
    }
}
