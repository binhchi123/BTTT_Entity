using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e5.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DuAn",
                columns: table => new
                {
                    DuAnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDuAn = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuAn", x => x.DuAnId);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    NhanVienId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hoten = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeSoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.NhanVienId);
                });

            migrationBuilder.CreateTable(
                name: "PhanCong",
                columns: table => new
                {
                    PhanCongId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhanVienId = table.Column<int>(type: "int", nullable: false),
                    DuAnId = table.Column<int>(type: "int", nullable: false),
                    SoGioLam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanCong", x => x.PhanCongId);
                    table.ForeignKey(
                        name: "FK_PhanCong_DuAn_DuAnId",
                        column: x => x.DuAnId,
                        principalTable: "DuAn",
                        principalColumn: "DuAnId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhanCong_NhanVien_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "NhanVien",
                        principalColumn: "NhanVienId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhanCong_DuAnId",
                table: "PhanCong",
                column: "DuAnId");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCong_NhanVienId",
                table: "PhanCong",
                column: "NhanVienId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhanCong");

            migrationBuilder.DropTable(
                name: "DuAn");

            migrationBuilder.DropTable(
                name: "NhanVien");
        }
    }
}
