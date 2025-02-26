using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class AddTestResultDetailEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestResultDetail",
                columns: table => new
                {
                    TestResultDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Qtype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResultId = table.Column<int>(type: "int", nullable: false),
                    ScoreType = table.Column<double>(type: "float", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResultDetail", x => x.TestResultDetailId);
                    table.ForeignKey(
                        name: "FK_TestResultDetail_TestResult_TestResultId",
                        column: x => x.TestResultId,
                        principalTable: "TestResult",
                        principalColumn: "TestResultID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestResultDetail_TestResultId",
                table: "TestResultDetail",
                column: "TestResultId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestResultDetail");
        }
    }
}
