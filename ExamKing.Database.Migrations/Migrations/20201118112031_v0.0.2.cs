using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamKing.Database.Migrations.Migrations
{
    public partial class v002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "answerdetail_quesiotype_id",
                table: "tb_stuanswerdetail");

            migrationBuilder.DropTable(
                name: "tb_questiontype");

            migrationBuilder.DropIndex(
                name: "answerdetail_quesiotype_id",
                table: "tb_stuanswerdetail");

            migrationBuilder.DropColumn(
                name: "quesionTypeld",
                table: "tb_stuanswerdetail");

            migrationBuilder.DropColumn(
                name: "judges",
                table: "tb_exam");

            migrationBuilder.DropColumn(
                name: "selects",
                table: "tb_exam");

            migrationBuilder.DropColumn(
                name: "singles",
                table: "tb_exam");

            migrationBuilder.AddColumn<int>(
                name: "questionType",
                table: "tb_stuanswerdetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "题型");

            migrationBuilder.CreateTable(
                name: "tb_examquestion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    questionType = table.Column<string>(type: "varchar(30)", nullable: false, comment: "题型", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    examId = table.Column<int>(type: "int", nullable: false, comment: "试卷ID"),
                    questionId = table.Column<int>(type: "int", nullable: false, comment: "题目ID"),
                    score = table.Column<int>(type: "int", nullable: false, comment: "分数")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_examquestion", x => x.id);
                    table.ForeignKey(
                        name: "examquestion_exam_id",
                        column: x => x.examId,
                        principalTable: "tb_exam",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "试卷题目关联表");

            migrationBuilder.CreateIndex(
                name: "answerdetail_examquestion_id",
                table: "tb_stuanswerdetail",
                column: "questionId");

            migrationBuilder.CreateIndex(
                name: "examquestion_id",
                table: "tb_examquestion",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_examquestion_examId",
                table: "tb_examquestion",
                column: "examId");

            migrationBuilder.AddForeignKey(
                name: "answerdetail_examquestion_id",
                table: "tb_stuanswerdetail",
                column: "questionId",
                principalTable: "tb_examquestion",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "answerdetail_examquestion_id",
                table: "tb_stuanswerdetail");

            migrationBuilder.DropTable(
                name: "tb_examquestion");

            migrationBuilder.DropIndex(
                name: "answerdetail_examquestion_id",
                table: "tb_stuanswerdetail");

            migrationBuilder.DropColumn(
                name: "questionType",
                table: "tb_stuanswerdetail");

            migrationBuilder.AddColumn<int>(
                name: "quesionTypeld",
                table: "tb_stuanswerdetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "题型ID");

            migrationBuilder.AddColumn<string>(
                name: "judges",
                table: "tb_exam",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "",
                comment: "是非题",
                collation: "utf8_general_ci")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.AddColumn<string>(
                name: "selects",
                table: "tb_exam",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "",
                comment: "多选题",
                collation: "utf8_general_ci")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.AddColumn<string>(
                name: "singles",
                table: "tb_exam",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "",
                comment: "单选题",
                collation: "utf8_general_ci")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.CreateTable(
                name: "tb_questiontype",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    createTime = table.Column<string>(type: "varchar(30)", nullable: false, comment: "创建时间", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    typeName = table.Column<string>(type: "varchar(30)", nullable: false, comment: "题型名称", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_questiontype", x => x.id);
                },
                comment: "题型实体");

            migrationBuilder.CreateIndex(
                name: "answerdetail_quesiotype_id",
                table: "tb_stuanswerdetail",
                column: "quesionTypeld");

            migrationBuilder.CreateIndex(
                name: "questiontype_id",
                table: "tb_questiontype",
                column: "id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "answerdetail_quesiotype_id",
                table: "tb_stuanswerdetail",
                column: "quesionTypeld",
                principalTable: "tb_questiontype",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
