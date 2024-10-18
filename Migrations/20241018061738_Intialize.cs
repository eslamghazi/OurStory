using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OurStory.Migrations
{
    /// <inheritdoc />
    public partial class Intialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LK_BlogTypeLookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEng = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_BlogTypeLookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LK_EventsLookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEng = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_EventsLookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LK_PublishedLookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEng = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_PublishedLookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_Lovers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Lovers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_Descriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TB_LoversId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Descriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_Descriptions_TB_Lovers_TB_LoversId",
                        column: x => x.TB_LoversId,
                        principalTable: "TB_Lovers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TB_OurBlogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_Blog_Type_LK = table.Column<int>(type: "int", nullable: true),
                    ID_Published_LK = table.Column<int>(type: "int", nullable: true),
                    ID_Events_LK = table.Column<int>(type: "int", nullable: true),
                    ItsDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_Lovers_TB = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_OurBlogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_OurBlogs_LK_BlogTypeLookup_ID_Blog_Type_LK",
                        column: x => x.ID_Blog_Type_LK,
                        principalTable: "LK_BlogTypeLookup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TB_OurBlogs_LK_EventsLookup_ID_Events_LK",
                        column: x => x.ID_Events_LK,
                        principalTable: "LK_EventsLookup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TB_OurBlogs_LK_PublishedLookup_ID_Published_LK",
                        column: x => x.ID_Published_LK,
                        principalTable: "LK_PublishedLookup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TB_OurBlogs_TB_Lovers_ID_Lovers_TB",
                        column: x => x.ID_Lovers_TB,
                        principalTable: "TB_Lovers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TB_FilePaths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TB_BlogsId = table.Column<int>(type: "int", nullable: true),
                    TB_LoversId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_FilePaths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_FilePaths_TB_Lovers_TB_LoversId",
                        column: x => x.TB_LoversId,
                        principalTable: "TB_Lovers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TB_FilePaths_TB_OurBlogs_TB_BlogsId",
                        column: x => x.TB_BlogsId,
                        principalTable: "TB_OurBlogs",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "LK_BlogTypeLookup",
                columns: new[] { "Id", "NameAr", "NameEng" },
                values: new object[,]
                {
                    { 1, "غير مخصص", "NotSpecific" },
                    { 2, "احداثنا", "OurEvents" },
                    { 3, "احلامنا", "OurDreams" },
                    { 4, "اسرارنا", "OurSecrets" },
                    { 5, "بدايتنا", "OurStart" },
                    { 6, "قصتنا", "OurStory" }
                });

            migrationBuilder.InsertData(
                table: "LK_EventsLookup",
                columns: new[] { "Id", "NameAr", "NameEng" },
                values: new object[,]
                {
                    { 1, "غير مرتبطه", "notRelated" },
                    { 2, "مرت", "Passed" },
                    { 3, "قادمه", "OurDreams" }
                });

            migrationBuilder.InsertData(
                table: "LK_PublishedLookup",
                columns: new[] { "Id", "NameAr", "NameEng" },
                values: new object[,]
                {
                    { 1, "غير مرتبطه", "notRelated" },
                    { 2, "ليس بعد", "NotYet" },
                    { 3, "منشوره", "Published" }
                });

            migrationBuilder.InsertData(
                table: "TB_Lovers",
                columns: new[] { "Id", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "Semsem", "SemsemFallInLoveWithHisBascota", "User" },
                    { 2, "Bascota", "BascotaFallInLoveWithHerSemsem", "User" },
                    { 3, "HeroSuperAdmin", "HeroWhatIsHero", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "TB_OurBlogs",
                columns: new[] { "Id", "DateCreatedAt", "Description", "ID_Blog_Type_LK", "ID_Events_LK", "ID_Lovers_TB", "ID_Published_LK", "ItsDate", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 18, 9, 17, 36, 270, DateTimeKind.Local).AddTicks(16), "aaaaaaaaaaaa", 1, 1, 1, 1, null, "aa" },
                    { 2, new DateTime(2024, 10, 18, 9, 17, 36, 270, DateTimeKind.Local).AddTicks(66), "bbbbbbbbbbbb", 1, 1, 1, 1, null, "bb" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Descriptions_TB_LoversId",
                table: "TB_Descriptions",
                column: "TB_LoversId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_FilePaths_TB_BlogsId",
                table: "TB_FilePaths",
                column: "TB_BlogsId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_FilePaths_TB_LoversId",
                table: "TB_FilePaths",
                column: "TB_LoversId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_OurBlogs_ID_Blog_Type_LK",
                table: "TB_OurBlogs",
                column: "ID_Blog_Type_LK");

            migrationBuilder.CreateIndex(
                name: "IX_TB_OurBlogs_ID_Events_LK",
                table: "TB_OurBlogs",
                column: "ID_Events_LK");

            migrationBuilder.CreateIndex(
                name: "IX_TB_OurBlogs_ID_Lovers_TB",
                table: "TB_OurBlogs",
                column: "ID_Lovers_TB");

            migrationBuilder.CreateIndex(
                name: "IX_TB_OurBlogs_ID_Published_LK",
                table: "TB_OurBlogs",
                column: "ID_Published_LK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Descriptions");

            migrationBuilder.DropTable(
                name: "TB_FilePaths");

            migrationBuilder.DropTable(
                name: "TB_OurBlogs");

            migrationBuilder.DropTable(
                name: "LK_BlogTypeLookup");

            migrationBuilder.DropTable(
                name: "LK_EventsLookup");

            migrationBuilder.DropTable(
                name: "LK_PublishedLookup");

            migrationBuilder.DropTable(
                name: "TB_Lovers");
        }
    }
}
