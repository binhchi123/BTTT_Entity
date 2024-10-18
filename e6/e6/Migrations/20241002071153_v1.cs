using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e6.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhoaHoc",
                columns: table => new
                {
                    KhoaHocId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKhoaHoc = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HocPhi = table.Column<int>(type: "int", maxLength: 10000000, nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhoaHoc", x => x.KhoaHocId);
                });

            migrationBuilder.CreateTable(
                name: "HocVien",
                columns: table => new
                {
                    HocVienId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhoaHocId = table.Column<int>(type: "int", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QueQuan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocVien", x => x.HocVienId);
                    table.ForeignKey(
                        name: "FK_HocVien_KhoaHoc_KhoaHocId",
                        column: x => x.KhoaHocId,
                        principalTable: "KhoaHoc",
                        principalColumn: "KhoaHocId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NgayHoc",
                columns: table => new
                {
                    NgayHocId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhoaHocId = table.Column<int>(type: "int", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NgayHoc", x => x.NgayHocId);
                    table.ForeignKey(
                        name: "FK_NgayHoc_KhoaHoc_KhoaHocId",
                        column: x => x.KhoaHocId,
                        principalTable: "KhoaHoc",
                        principalColumn: "KhoaHocId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HocVien_KhoaHocId",
                table: "HocVien",
                column: "KhoaHocId");

            migrationBuilder.CreateIndex(
                name: "IX_NgayHoc_KhoaHocId",
                table: "NgayHoc",
                column: "KhoaHocId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HocVien");

            migrationBuilder.DropTable(
                name: "NgayHoc");

            migrationBuilder.DropTable(
                name: "KhoaHoc");
        }
    }
}
