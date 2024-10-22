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
                name: "LK_LikesTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEng = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_LikesTypes", x => x.Id);
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
                name: "TB_SecretKeywords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Keyword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_SecretKeywords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_Lover_TB = table.Column<int>(type: "int", nullable: true),
                    TB_BlogsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Comments", x => x.Id);
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
                    TB_LoversId = table.Column<int>(type: "int", nullable: true),
                    TB_MessagesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_FilePaths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_Lovers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TB_FilesPath_ProfilePictureId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Lovers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_Lovers_TB_FilePaths_TB_FilesPath_ProfilePictureId",
                        column: x => x.TB_FilesPath_ProfilePictureId,
                        principalTable: "TB_FilePaths",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TB_Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsMessageDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IsSeen = table.Column<bool>(type: "bit", nullable: true),
                    SeenAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ID_Lovers_Sender_TB = table.Column<int>(type: "int", nullable: true),
                    ID_Lovers_Receiver_TB = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_Messages_TB_Lovers_ID_Lovers_Receiver_TB",
                        column: x => x.ID_Lovers_Receiver_TB,
                        principalTable: "TB_Lovers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TB_Messages_TB_Lovers_ID_Lovers_Sender_TB",
                        column: x => x.ID_Lovers_Sender_TB,
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
                name: "TB_Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_LK_LikesTypes = table.Column<int>(type: "int", nullable: true),
                    ID_Lover_TB = table.Column<int>(type: "int", nullable: true),
                    TB_BlogsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_Likes_LK_LikesTypes_ID_LK_LikesTypes",
                        column: x => x.ID_LK_LikesTypes,
                        principalTable: "LK_LikesTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TB_Likes_TB_Lovers_ID_Lover_TB",
                        column: x => x.ID_Lover_TB,
                        principalTable: "TB_Lovers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TB_Likes_TB_OurBlogs_TB_BlogsId",
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
                table: "LK_LikesTypes",
                columns: new[] { "Id", "NameAr", "NameEng" },
                values: new object[,]
                {
                    { 1, "اعجاب", "Like" },
                    { 2, "احبته", "Love" },
                    { 3, "اعتني به", "Care" }
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
                columns: new[] { "Id", "Name", "Password", "Role", "TB_FilesPath_ProfilePictureId" },
                values: new object[,]
                {
                    { 1, "Semsem", "SemsemFallInLoveWithHisBascota", "User", null },
                    { 2, "Bascota", "BascotaFallInLoveWithHerSemsem", "User", null },
                    { 3, "HeroSuperAdmin", "HeroWhatIsHero", "Admin", null }
                });

            migrationBuilder.InsertData(
                table: "TB_SecretKeywords",
                columns: new[] { "Id", "Keyword", "Title" },
                values: new object[] { 1, "MabascotaStolenMaHeart", "firstTimePass" });

            migrationBuilder.InsertData(
                table: "TB_OurBlogs",
                columns: new[] { "Id", "DateCreatedAt", "Description", "ID_Blog_Type_LK", "ID_Events_LK", "ID_Lovers_TB", "ID_Published_LK", "ItsDate", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 22, 10, 32, 0, 882, DateTimeKind.Local).AddTicks(7995), "aaaaaaaaaaaa", 1, 1, 1, 1, null, "aa" },
                    { 2, new DateTime(2024, 10, 22, 10, 32, 0, 882, DateTimeKind.Local).AddTicks(8042), "bbbbbbbbbbbb", 1, 1, 1, 1, null, "bb" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Comments_ID_Lover_TB",
                table: "TB_Comments",
                column: "ID_Lover_TB");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Comments_TB_BlogsId",
                table: "TB_Comments",
                column: "TB_BlogsId");

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
                name: "IX_TB_FilePaths_TB_MessagesId",
                table: "TB_FilePaths",
                column: "TB_MessagesId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Likes_ID_LK_LikesTypes",
                table: "TB_Likes",
                column: "ID_LK_LikesTypes");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Likes_ID_Lover_TB",
                table: "TB_Likes",
                column: "ID_Lover_TB");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Likes_TB_BlogsId",
                table: "TB_Likes",
                column: "TB_BlogsId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Lovers_TB_FilesPath_ProfilePictureId",
                table: "TB_Lovers",
                column: "TB_FilesPath_ProfilePictureId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Messages_ID_Lovers_Receiver_TB",
                table: "TB_Messages",
                column: "ID_Lovers_Receiver_TB");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Messages_ID_Lovers_Sender_TB",
                table: "TB_Messages",
                column: "ID_Lovers_Sender_TB");

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

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Comments_TB_Lovers_ID_Lover_TB",
                table: "TB_Comments",
                column: "ID_Lover_TB",
                principalTable: "TB_Lovers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Comments_TB_OurBlogs_TB_BlogsId",
                table: "TB_Comments",
                column: "TB_BlogsId",
                principalTable: "TB_OurBlogs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Descriptions_TB_Lovers_TB_LoversId",
                table: "TB_Descriptions",
                column: "TB_LoversId",
                principalTable: "TB_Lovers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_FilePaths_TB_Lovers_TB_LoversId",
                table: "TB_FilePaths",
                column: "TB_LoversId",
                principalTable: "TB_Lovers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_FilePaths_TB_Messages_TB_MessagesId",
                table: "TB_FilePaths",
                column: "TB_MessagesId",
                principalTable: "TB_Messages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_FilePaths_TB_OurBlogs_TB_BlogsId",
                table: "TB_FilePaths",
                column: "TB_BlogsId",
                principalTable: "TB_OurBlogs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_FilePaths_TB_Lovers_TB_LoversId",
                table: "TB_FilePaths");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_Messages_TB_Lovers_ID_Lovers_Receiver_TB",
                table: "TB_Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_Messages_TB_Lovers_ID_Lovers_Sender_TB",
                table: "TB_Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_OurBlogs_TB_Lovers_ID_Lovers_TB",
                table: "TB_OurBlogs");

            migrationBuilder.DropTable(
                name: "TB_Comments");

            migrationBuilder.DropTable(
                name: "TB_Descriptions");

            migrationBuilder.DropTable(
                name: "TB_Likes");

            migrationBuilder.DropTable(
                name: "TB_SecretKeywords");

            migrationBuilder.DropTable(
                name: "LK_LikesTypes");

            migrationBuilder.DropTable(
                name: "TB_Lovers");

            migrationBuilder.DropTable(
                name: "TB_FilePaths");

            migrationBuilder.DropTable(
                name: "TB_Messages");

            migrationBuilder.DropTable(
                name: "TB_OurBlogs");

            migrationBuilder.DropTable(
                name: "LK_BlogTypeLookup");

            migrationBuilder.DropTable(
                name: "LK_EventsLookup");

            migrationBuilder.DropTable(
                name: "LK_PublishedLookup");
        }
    }
}
