using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAccountAssignment.API.Migrations
{
    /// <inheritdoc />
    public partial class Add_BankAccountNumber_Sequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "BankAccountSequence",
                minValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "BankAccountSequence");
        }
    }
}
