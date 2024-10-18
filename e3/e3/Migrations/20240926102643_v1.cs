using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e3.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhieuNhap",
                columns: table => new
                {
                    PhieuNhapId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhieu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayNhap = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuNhap", x => x.PhieuNhapId);
                });

            migrationBuilder.CreateTable(
                name: "PhieuXuat",
                columns: table => new
                {
                    PhieuXuatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhieu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayXuat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuXuat", x => x.PhieuXuatId);
                });

            migrationBuilder.CreateTable(
                name: "VatTu",
                columns: table => new
                {
                    VatTuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenVatTu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuongTon = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatTu", x => x.VatTuId);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietPhieuNhap",
                columns: table => new
                {
                    ChiTietPhieuNhapId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VatTuId = table.Column<int>(type: "int", nullable: false),
                    PhieuNhapId = table.Column<int>(type: "int", nullable: false),
                    SoLuongNhap = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietPhieuNhap", x => x.ChiTietPhieuNhapId);
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuNhap_PhieuNhap_PhieuNhapId",
                        column: x => x.PhieuNhapId,
                        principalTable: "PhieuNhap",
                        principalColumn: "PhieuNhapId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuNhap_VatTu_VatTuId",
                        column: x => x.VatTuId,
                        principalTable: "VatTu",
                        principalColumn: "VatTuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietPhieuXuat",
                columns: table => new
                {
                    ChiTietPhieuXuatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VatTuId = table.Column<int>(type: "int", nullable: false),
                    PhieuXuatId = table.Column<int>(type: "int", nullable: false),
                    SoLuongXuat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietPhieuXuat", x => x.ChiTietPhieuXuatId);
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuXuat_PhieuXuat_PhieuXuatId",
                        column: x => x.PhieuXuatId,
                        principalTable: "PhieuXuat",
                        principalColumn: "PhieuXuatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuXuat_VatTu_VatTuId",
                        column: x => x.VatTuId,
                        principalTable: "VatTu",
                        principalColumn: "VatTuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuNhap_PhieuNhapId",
                table: "ChiTietPhieuNhap",
                column: "PhieuNhapId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuNhap_VatTuId",
                table: "ChiTietPhieuNhap",
                column: "VatTuId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuXuat_PhieuXuatId",
                table: "ChiTietPhieuXuat",
                column: "PhieuXuatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuXuat_VatTuId",
                table: "ChiTietPhieuXuat",
                column: "VatTuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietPhieuNhap");

            migrationBuilder.DropTable(
                name: "ChiTietPhieuXuat");

            migrationBuilder.DropTable(
                name: "PhieuNhap");

            migrationBuilder.DropTable(
                name: "PhieuXuat");

            migrationBuilder.DropTable(
                name: "VatTu");
        }
    }
}
