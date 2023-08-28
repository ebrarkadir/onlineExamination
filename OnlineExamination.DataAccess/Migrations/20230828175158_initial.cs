using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineExamination.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamResaults_Exams_ExamsId",
                table: "ExamResaults");

            migrationBuilder.AlterColumn<int>(
                name: "ExamsId",
                table: "ExamResaults",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResaults_Exams_ExamsId",
                table: "ExamResaults",
                column: "ExamsId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamResaults_Exams_ExamsId",
                table: "ExamResaults");

            migrationBuilder.AlterColumn<int>(
                name: "ExamsId",
                table: "ExamResaults",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResaults_Exams_ExamsId",
                table: "ExamResaults",
                column: "ExamsId",
                principalTable: "Exams",
                principalColumn: "Id");
        }
    }
}
