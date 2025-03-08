using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformationSystemInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmailColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PublicationTypes",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Requirements = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationTypes", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Info = table.Column<string>(type: "text", nullable: true),
                    Contacts = table.Column<string>(type: "text", nullable: true),
                    Avatar = table.Column<byte[]>(type: "image", nullable: true),
                    SignUpDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors/Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Topic = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    PublicationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Media = table.Column<byte[]>(type: "image", nullable: true),
                    SubjectID = table.Column<int>(type: "int", nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleID);
                    table.ForeignKey(
                        name: "FK_Articles_Subjects",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID");
                    table.ForeignKey(
                        name: "FK_Articles_Types",
                        column: x => x.TypeID,
                        principalTable: "PublicationTypes",
                        principalColumn: "TypeID");
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrgID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Contacts = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: false),
                    MembersID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrgID);
                    table.ForeignKey(
                        name: "FK_Organizations_Members",
                        column: x => x.MembersID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "AuthorsPerArticle",
                columns: table => new
                {
                    LinkID = table.Column<int>(type: "int", nullable: false),
                    Authors = table.Column<int>(type: "int", nullable: false),
                    Article = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorsPerArticle", x => x.LinkID);
                    table.ForeignKey(
                        name: "FK_AuthorsPerArticle_Article",
                        column: x => x.Article,
                        principalTable: "Articles",
                        principalColumn: "ArticleID");
                    table.ForeignKey(
                        name: "FK_AuthorsPerArticle_Authors",
                        column: x => x.Authors,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ComID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "text", nullable: false),
                    PublicationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ArticleID = table.Column<int>(type: "int", nullable: false),
                    AuthorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ComID);
                    table.ForeignKey(
                        name: "FK_Comments_Articles",
                        column: x => x.ArticleID,
                        principalTable: "Articles",
                        principalColumn: "ArticleID");
                    table.ForeignKey(
                        name: "FK_Comments_Authors",
                        column: x => x.AuthorID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_SubjectID",
                table: "Articles",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_TypeID",
                table: "Articles",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorsPerArticle_Article",
                table: "AuthorsPerArticle",
                column: "Article");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorsPerArticle_Authors",
                table: "AuthorsPerArticle",
                column: "Authors");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleID",
                table: "Comments",
                column: "ArticleID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorID",
                table: "Comments",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_MembersID",
                table: "Organizations",
                column: "MembersID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorsPerArticle");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "PublicationTypes");
        }
    }
}
