using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e7.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoaiNguyenLieu",
                columns: table => new
                {
                    LoaiNguyenLieuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiNguyenLieu", x => x.LoaiNguyenLieuId);
                });

            migrationBuilder.CreateTable(
                name: "PhieuThu",
                columns: table => new
                {
                    PhieuThuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NhanVienLap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThanhTien = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuThu", x => x.PhieuThuId);
                });

            migrationBuilder.CreateTable(
                name: "NguyenLieu",
                columns: table => new
                {
                    NguyenLieuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiNguyenLieuId = table.Column<int>(type: "int", nullable: false),
                    TenNguyenLieu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GiaBan = table.Column<double>(type: "float", nullable: false),
                    DonViTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SoLuongKho = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguyenLieu", x => x.NguyenLieuId);
                    table.ForeignKey(
                        name: "FK_NguyenLieu_LoaiNguyenLieu_LoaiNguyenLieuId",
                        column: x => x.LoaiNguyenLieuId,
                        principalTable: "LoaiNguyenLieu",
                        principalColumn: "LoaiNguyenLieuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietPhieuThu",
                columns: table => new
                {
                    ChiTietPhieuThuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguyenLieuId = table.Column<int>(type: "int", nullable: false),
                    PhieuThuId = table.Column<int>(type: "int", nullable: false),
                    SoLuongBan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietPhieuThu", x => x.ChiTietPhieuThuId);
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuThu_NguyenLieu_NguyenLieuId",
                        column: x => x.NguyenLieuId,
                        principalTable: "NguyenLieu",
                        principalColumn: "NguyenLieuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuThu_PhieuThu_PhieuThuId",
                        column: x => x.PhieuThuId,
                        principalTable: "PhieuThu",
                        principalColumn: "PhieuThuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuThu_NguyenLieuId",
                table: "ChiTietPhieuThu",
                column: "NguyenLieuId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuThu_PhieuThuId",
                table: "ChiTietPhieuThu",
                column: "PhieuThuId");

            migrationBuilder.CreateIndex(
                name: "IX_NguyenLieu_LoaiNguyenLieuId",
                table: "NguyenLieu",
                column: "LoaiNguyenLieuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietPhieuThu");

            migrationBuilder.DropTable(
                name: "NguyenLieu");

            migrationBuilder.DropTable(
                name: "PhieuThu");

            migrationBuilder.DropTable(
                name: "LoaiNguyenLieu");
        }
    }
}
