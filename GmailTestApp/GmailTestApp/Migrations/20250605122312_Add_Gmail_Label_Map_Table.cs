using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GmailTestApp.Migrations
{
    /// <inheritdoc />
    public partial class Add_Gmail_Label_Map_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GmailLabelMap",
                columns: table => new
                {
                    GmailId = table.Column<int>(type: "int", nullable: false),
                    LabelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GmailLabelMap", x => new { x.GmailId, x.LabelId });
                    table.ForeignKey(
                        name: "FK_GmailLabelMap_Gmails_GmailId",
                        column: x => x.GmailId,
                        principalTable: "Gmails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GmailLabelMap_Labels_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Labels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GmailLabelMap_LabelId",
                table: "GmailLabelMap",
                column: "LabelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GmailLabelMap");
        }
    }
}
