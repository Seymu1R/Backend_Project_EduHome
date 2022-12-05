using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHomeProject.Migrations
{
    public partial class DeletingSomeFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSosialMedias_FooterLeftSides_FooterLeftSideId",
                table: "TeacherSosialMedias");

            migrationBuilder.DropIndex(
                name: "IX_TeacherSosialMedias_FooterLeftSideId",
                table: "TeacherSosialMedias");

            migrationBuilder.DropColumn(
                name: "FooterLeftSideId",
                table: "TeacherSosialMedias");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FooterLeftSideId",
                table: "TeacherSosialMedias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSosialMedias_FooterLeftSideId",
                table: "TeacherSosialMedias",
                column: "FooterLeftSideId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSosialMedias_FooterLeftSides_FooterLeftSideId",
                table: "TeacherSosialMedias",
                column: "FooterLeftSideId",
                principalTable: "FooterLeftSides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
