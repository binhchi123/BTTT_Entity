using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e2.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoaiMonAn",
                columns: table => new
                {
                    LoaiMonAnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiMonAn", x => x.LoaiMonAnId);
                });

            migrationBuilder.CreateTable(
                name: "NguyenLieu",
                columns: table => new
                {
                    NguyenLieuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNguyenLieu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguyenLieu", x => x.NguyenLieuId);
                });

            migrationBuilder.CreateTable(
                name: "MonAn",
                columns: table => new
                {
                    MonAnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiMonAnId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonAn", x => x.MonAnId);
                    table.ForeignKey(
                        name: "FK_MonAn_LoaiMonAn_LoaiMonAnId",
                        column: x => x.LoaiMonAnId,
                        principalTable: "LoaiMonAn",
                        principalColumn: "LoaiMonAnId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CongThuc",
                columns: table => new
                {
                    CongThucId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonAnId = table.Column<int>(type: "int", nullable: false),
                    NguyenLieuId = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CongThuc", x => x.CongThucId);
                    table.ForeignKey(
                        name: "FK_CongThuc_MonAn_MonAnId",
                        column: x => x.MonAnId,
                        principalTable: "MonAn",
                        principalColumn: "MonAnId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CongThuc_NguyenLieu_NguyenLieuId",
                        column: x => x.NguyenLieuId,
                        principalTable: "NguyenLieu",
                        principalColumn: "NguyenLieuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CongThuc_MonAnId",
                table: "CongThuc",
                column: "MonAnId");

            migrationBuilder.CreateIndex(
                name: "IX_CongThuc_NguyenLieuId",
                table: "CongThuc",
                column: "NguyenLieuId");

            migrationBuilder.CreateIndex(
                name: "IX_MonAn_LoaiMonAnId",
                table: "MonAn",
                column: "LoaiMonAnId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CongThuc");

            migrationBuilder.DropTable(
                name: "MonAn");

            migrationBuilder.DropTable(
                name: "NguyenLieu");

            migrationBuilder.DropTable(
                name: "LoaiMonAn");
        }
    }
}
