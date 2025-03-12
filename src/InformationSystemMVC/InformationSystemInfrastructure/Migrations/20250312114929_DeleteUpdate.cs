using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformationSystemInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Subjects",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Types",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorsPerArticle_Article",
                table: "AuthorsPerArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorsPerArticle_Authors",
                table: "AuthorsPerArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Articles",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Authors",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Members",
                table: "Organizations");

            migrationBuilder.AlterColumn<int>(
                name: "LinkID",
                table: "AuthorsPerArticle",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Subjects",
                table: "Articles",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Types",
                table: "Articles",
                column: "TypeID",
                principalTable: "PublicationTypes",
                principalColumn: "TypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorsPerArticle_Article",
                table: "AuthorsPerArticle",
                column: "Article",
                principalTable: "Articles",
                principalColumn: "ArticleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorsPerArticle_Authors",
                table: "AuthorsPerArticle",
                column: "Authors",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Articles",
                table: "Comments",
                column: "ArticleID",
                principalTable: "Articles",
                principalColumn: "ArticleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Authors",
                table: "Comments",
                column: "AuthorID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Members",
                table: "Organizations",
                column: "MembersID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Subjects",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Types",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorsPerArticle_Article",
                table: "AuthorsPerArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorsPerArticle_Authors",
                table: "AuthorsPerArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Articles",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Authors",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Members",
                table: "Organizations");

            migrationBuilder.AlterColumn<int>(
                name: "LinkID",
                table: "AuthorsPerArticle",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Subjects",
                table: "Articles",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Types",
                table: "Articles",
                column: "TypeID",
                principalTable: "PublicationTypes",
                principalColumn: "TypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorsPerArticle_Article",
                table: "AuthorsPerArticle",
                column: "Article",
                principalTable: "Articles",
                principalColumn: "ArticleID");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorsPerArticle_Authors",
                table: "AuthorsPerArticle",
                column: "Authors",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Articles",
                table: "Comments",
                column: "ArticleID",
                principalTable: "Articles",
                principalColumn: "ArticleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Authors",
                table: "Comments",
                column: "AuthorID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Members",
                table: "Organizations",
                column: "MembersID",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}
