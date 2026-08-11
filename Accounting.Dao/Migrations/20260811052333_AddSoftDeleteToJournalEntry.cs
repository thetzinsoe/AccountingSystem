using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Dao.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteToJournalEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "journal_entries",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "journal_entries");
        }
    }
}
