using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e4.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lop",
                columns: table => new
                {
                    LopId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLop = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SiSo = table.Column<int>(type: "int", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lop", x => x.LopId);
                });

            migrationBuilder.CreateTable(
                name: "HocSinh",
                columns: table => new
                {
                    HocSinhId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LopId = table.Column<int>(type: "int", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QueQuan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocSinh", x => x.HocSinhId);
                    table.ForeignKey(
                        name: "FK_HocSinh_Lop_LopId",
                        column: x => x.LopId,
                        principalTable: "Lop",
                        principalColumn: "LopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HocSinh_LopId",
                table: "HocSinh",
                column: "LopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HocSinh");

            migrationBuilder.DropTable(
                name: "Lop");
        }
    }
}
