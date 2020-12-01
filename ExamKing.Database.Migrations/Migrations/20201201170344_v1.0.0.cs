using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamKing.Database.Migrations.Migrations
{
    public partial class v100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_admin",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(30)", nullable: false, comment: "账号", collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(50)", nullable: true, comment: "密码", collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "创建时间")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_admin", x => x.id);
                },
                comment: "管理员表");

            migrationBuilder.CreateTable(
                name: "tb_dept",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    deptName = table.Column<string>(type: "varchar(50)", nullable: false, comment: "系别名称", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "创建时间")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_dept", x => x.id);
                },
                comment: "系别表");

            migrationBuilder.CreateTable(
                name: "tb_classes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    classesName = table.Column<string>(type: "varchar(50)", nullable: false, comment: "班级名称", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    DeptId = table.Column<int>(type: "int", nullable: false, comment: "系别ID"),
                    createTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "创建时间")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_classes", x => x.id);
                    table.ForeignKey(
                        name: "classes_dept_id",
                        column: x => x.DeptId,
                        principalTable: "tb_dept",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "班级表");

            migrationBuilder.CreateTable(
                name: "tb_teacher",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    teacherName = table.Column<string>(type: "varchar(50)", nullable: false, comment: "姓名", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    sex = table.Column<string>(type: "varchar(10)", nullable: false, comment: "性别", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    telphone = table.Column<string>(type: "varchar(25)", nullable: false, comment: "联系电话", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    teacherNo = table.Column<string>(type: "varchar(20)", nullable: false, comment: "教师编号", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    password = table.Column<string>(type: "varchar(50)", nullable: false, comment: "密码", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    DeptId = table.Column<int>(type: "int", nullable: false, comment: "系别ID"),
                    idCard = table.Column<string>(type: "varchar(20)", nullable: false, comment: "身份证号", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "创建时间")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_teacher", x => x.id);
                    table.ForeignKey(
                        name: "teacher_dept_id",
                        column: x => x.DeptId,
                        principalTable: "tb_dept",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "教师表");

            migrationBuilder.CreateTable(
                name: "tb_student",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    stuName = table.Column<string>(type: "varchar(50)", nullable: false, comment: "姓名", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    deptId = table.Column<int>(type: "int", nullable: false, comment: "系别ID"),
                    classesId = table.Column<int>(type: "int", nullable: false, comment: "班级ID"),
                    sex = table.Column<string>(type: "varchar(10)", nullable: false, comment: "性别", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    stuNo = table.Column<string>(type: "varchar(50)", nullable: false, comment: "学号", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    password = table.Column<string>(type: "varchar(50)", nullable: false, comment: "密码", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    telphone = table.Column<string>(type: "varchar(30)", nullable: false, comment: "联系电话", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    idCard = table.Column<string>(type: "varchar(30)", nullable: false, comment: "身份证号码", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "创建时间")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_student", x => x.id);
                    table.ForeignKey(
                        name: "student_classes_id",
                        column: x => x.classesId,
                        principalTable: "tb_classes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "student_dept_id",
                        column: x => x.deptId,
                        principalTable: "tb_dept",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "学生表");

            migrationBuilder.CreateTable(
                name: "tb_course",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    courseName = table.Column<string>(type: "varchar(100)", nullable: false, comment: "课程名称", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    DeptId = table.Column<int>(type: "int", nullable: false, comment: "系别ID"),
                    teacherId = table.Column<int>(type: "int", nullable: false, comment: "教师ID"),
                    createTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "创建时间")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_course", x => x.id);
                    table.ForeignKey(
                        name: "course_dept_id",
                        column: x => x.DeptId,
                        principalTable: "tb_dept",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "course_teacher_id",
                        column: x => x.teacherId,
                        principalTable: "tb_teacher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "课程表");

            migrationBuilder.CreateTable(
                name: "tb_chapter",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    chapterName = table.Column<string>(type: "varchar(100)", nullable: false, comment: "章节名称", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    courseId = table.Column<int>(type: "int", nullable: false, comment: "课程ID"),
                    desc = table.Column<string>(type: "varchar(200)", nullable: false, comment: "章节描述", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_chapter", x => x.id);
                    table.ForeignKey(
                        name: "chapter_course_id",
                        column: x => x.courseId,
                        principalTable: "tb_course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "课程章节表");

            migrationBuilder.CreateTable(
                name: "tb_courseclasses",
                columns: table => new
                {
                    courseId = table.Column<int>(type: "int", nullable: false, comment: "课程ID"),
                    classesId = table.Column<int>(type: "int", nullable: false, comment: "班级ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_courseclasses", x => new { x.classesId, x.courseId });
                    table.ForeignKey(
                        name: "courseclasses_classes_idx",
                        column: x => x.classesId,
                        principalTable: "tb_classes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "courseclasses_course_idx",
                        column: x => x.courseId,
                        principalTable: "tb_course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "课程班级关联表");

            migrationBuilder.CreateTable(
                name: "tb_exam",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    examName = table.Column<string>(type: "varchar(200)", nullable: false, comment: "试卷名称", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    courseId = table.Column<int>(type: "int", nullable: false, comment: "课程ID"),
                    teacherId = table.Column<int>(type: "int", nullable: false, comment: "教师ID"),
                    startTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "开始时间"),
                    endTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "结束时间"),
                    duration = table.Column<int>(type: "int", nullable: false, comment: "考试时长"),
                    isEnable = table.Column<string>(type: "varchar(10)", nullable: false, comment: "启用状态", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    isFinish = table.Column<string>(type: "varchar(10)", nullable: false, comment: "结束状态", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "创建时间")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    examScore = table.Column<int>(type: "int", nullable: false, comment: "试卷总分"),
                    judgeScore = table.Column<int>(type: "int", nullable: false, comment: "是非题分值"),
                    singleScore = table.Column<int>(type: "int", nullable: false, comment: "单选题分值"),
                    selectScore = table.Column<int>(type: "int", nullable: false, comment: "多选题分值")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_exam", x => x.id);
                    table.ForeignKey(
                        name: "exam_course_id",
                        column: x => x.courseId,
                        principalTable: "tb_course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "exam_teacher_id",
                        column: x => x.teacherId,
                        principalTable: "tb_teacher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "试卷表");

            migrationBuilder.CreateTable(
                name: "tb_judge",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    question = table.Column<string>(type: "varchar(200)", nullable: false, comment: "题目", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    answer = table.Column<string>(type: "varchar(10)", nullable: false, comment: "答案", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    courseId = table.Column<int>(type: "int", nullable: false, comment: "课程ID"),
                    chapterId = table.Column<int>(type: "int", nullable: false, comment: "课程章节ID"),
                    teacherId = table.Column<int>(type: "int", nullable: false, comment: "教师ID"),
                    ideas = table.Column<string>(type: "varchar(300)", nullable: false, comment: "解题思路", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "创建时间")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_judge", x => x.id);
                    table.ForeignKey(
                        name: "judge_chapter_id",
                        column: x => x.chapterId,
                        principalTable: "tb_chapter",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "judge_source_id",
                        column: x => x.courseId,
                        principalTable: "tb_course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "judge_teacher_id",
                        column: x => x.teacherId,
                        principalTable: "tb_teacher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "是非题表");

            migrationBuilder.CreateTable(
                name: "tb_select",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    question = table.Column<string>(type: "varchar(300)", nullable: false, comment: "问题", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    answer = table.Column<string>(type: "varchar(50)", nullable: false, comment: "答案", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    isSingle = table.Column<string>(type: "varchar(10)", nullable: false, comment: "是否单选", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    courseId = table.Column<int>(type: "int", nullable: false, comment: "课程ID"),
                    chapterId = table.Column<int>(type: "int", nullable: false, comment: "课程章节ID"),
                    teacherId = table.Column<int>(type: "int", nullable: false, comment: "教师ID"),
                    optionA = table.Column<string>(type: "varchar(300)", nullable: false, comment: "选项A", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    optionB = table.Column<string>(type: "varchar(300)", nullable: false, comment: "选项B", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    optionC = table.Column<string>(type: "varchar(300)", nullable: false, comment: "选项C", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    optionD = table.Column<string>(type: "varchar(300)", nullable: false, comment: "选项D", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    ideas = table.Column<string>(type: "varchar(300)", nullable: false, comment: "解题思路", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "创建时间")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_select", x => x.id);
                    table.ForeignKey(
                        name: "select_chapter_id",
                        column: x => x.chapterId,
                        principalTable: "tb_chapter",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "select_source_id",
                        column: x => x.courseId,
                        principalTable: "tb_course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "select_teacher_id",
                        column: x => x.teacherId,
                        principalTable: "tb_teacher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "选择题表");

            migrationBuilder.CreateTable(
                name: "tb_examclasses",
                columns: table => new
                {
                    examId = table.Column<int>(type: "int", nullable: false, comment: "试卷ID"),
                    classesId = table.Column<int>(type: "int", nullable: false, comment: "班级ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_examclasses", x => new { x.examId, x.classesId });
                    table.ForeignKey(
                        name: "examclasses_classes_idx",
                        column: x => x.classesId,
                        principalTable: "tb_classes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "examclasses_exam_idx",
                        column: x => x.examId,
                        principalTable: "tb_exam",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "试卷班级关联表");

            migrationBuilder.CreateTable(
                name: "tb_examjobs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    examId = table.Column<int>(type: "int", nullable: false, comment: "考试ID"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "任务状态")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_examjobs", x => x.id);
                    table.ForeignKey(
                        name: "examjobs_exam_id",
                        column: x => x.examId,
                        principalTable: "tb_exam",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "考试任务表");

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

            migrationBuilder.CreateTable(
                name: "tb_stuscore",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int unsigned", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    stuId = table.Column<int>(type: "int", nullable: false, comment: "学生ID"),
                    courseId = table.Column<int>(type: "int", nullable: false, comment: "课程ID"),
                    examId = table.Column<int>(type: "int", nullable: false, comment: "考试ID"),
                    score = table.Column<int>(type: "int", nullable: false, comment: "分数"),
                    createTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "创建时间")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_stuscore", x => x.id);
                    table.ForeignKey(
                        name: "stuscore_course_id",
                        column: x => x.courseId,
                        principalTable: "tb_course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "stuscore_exam_id",
                        column: x => x.examId,
                        principalTable: "tb_exam",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "stuscore_stu_id",
                        column: x => x.stuId,
                        principalTable: "tb_student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "学生成绩表");

            migrationBuilder.CreateTable(
                name: "tb_stuanswerdetail",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    stuId = table.Column<int>(type: "int", nullable: false, comment: "学生ID"),
                    examId = table.Column<int>(type: "int", nullable: false, comment: "试卷ID"),
                    questionId = table.Column<int>(type: "int", nullable: false, comment: "题目ID"),
                    questionType = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true, comment: "题型"),
                    stuanswer = table.Column<string>(type: "varchar(100)", nullable: false, comment: "学生答案", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    answer = table.Column<string>(type: "varchar(100)", nullable: false, comment: "正确答案", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    isright = table.Column<string>(type: "varchar(10)", nullable: false, comment: "是否正确", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "创建时间")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_stuanswerdetail", x => x.id);
                    table.ForeignKey(
                        name: "answerdetail_exam_id",
                        column: x => x.examId,
                        principalTable: "tb_exam",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "answerdetail_examquestion_id",
                        column: x => x.questionId,
                        principalTable: "tb_examquestion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "answerdetail_stu_id",
                        column: x => x.stuId,
                        principalTable: "tb_student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "答题明细表");

            migrationBuilder.InsertData(
                table: "tb_admin",
                columns: new[] { "id", "createTime", "password", "username" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2020, 11, 18, 21, 15, 38, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "e10adc3949ba59abbe56e057f20f883e", "admin" });

            migrationBuilder.CreateIndex(
                name: "id",
                table: "tb_admin",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "chapter_course_id",
                table: "tb_chapter",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "chapter_id",
                table: "tb_chapter",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "classes_dept_id",
                table: "tb_classes",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "classes_id",
                table: "tb_classes",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "course_dept_id",
                table: "tb_course",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "course_id",
                table: "tb_course",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "course_teacher_id",
                table: "tb_course",
                column: "teacherId");

            migrationBuilder.CreateIndex(
                name: "courseclasses_classes_idx",
                table: "tb_courseclasses",
                column: "classesId");

            migrationBuilder.CreateIndex(
                name: "courseclasses_course_idx",
                table: "tb_courseclasses",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "dept_id",
                table: "tb_dept",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "exam_course_id",
                table: "tb_exam",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "exam_id",
                table: "tb_exam",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "exam_teacher_id",
                table: "tb_exam",
                column: "teacherId");

            migrationBuilder.CreateIndex(
                name: "examclasses_classes_idx",
                table: "tb_examclasses",
                column: "classesId");

            migrationBuilder.CreateIndex(
                name: "examclasses_exam_idx",
                table: "tb_examclasses",
                column: "examId");

            migrationBuilder.CreateIndex(
                name: "examjobs_exam_id",
                table: "tb_examjobs",
                column: "examId");

            migrationBuilder.CreateIndex(
                name: "examjobs_id",
                table: "tb_examjobs",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "examquestion_id",
                table: "tb_examquestion",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_examquestion_examId",
                table: "tb_examquestion",
                column: "examId");

            migrationBuilder.CreateIndex(
                name: "judge_chapter_id",
                table: "tb_judge",
                column: "chapterId");

            migrationBuilder.CreateIndex(
                name: "judge_id",
                table: "tb_judge",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "judge_source_id",
                table: "tb_judge",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "judge_teacher_id",
                table: "tb_judge",
                column: "teacherId");

            migrationBuilder.CreateIndex(
                name: "select_chapter_id",
                table: "tb_select",
                column: "chapterId");

            migrationBuilder.CreateIndex(
                name: "select_id",
                table: "tb_select",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "select_source_id",
                table: "tb_select",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "select_teacher_id",
                table: "tb_select",
                column: "teacherId");

            migrationBuilder.CreateIndex(
                name: "answerdetail_exam_id",
                table: "tb_stuanswerdetail",
                column: "examId");

            migrationBuilder.CreateIndex(
                name: "answerdetail_examquestion_id",
                table: "tb_stuanswerdetail",
                column: "questionId");

            migrationBuilder.CreateIndex(
                name: "answerdetail_stu_id",
                table: "tb_stuanswerdetail",
                column: "stuId");

            migrationBuilder.CreateIndex(
                name: "answerdetail_stu_id_question_id",
                table: "tb_stuanswerdetail",
                columns: new[] { "stuId", "examId", "questionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "stuanseerdetail_id",
                table: "tb_stuanswerdetail",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "student_classes_id",
                table: "tb_student",
                column: "classesId");

            migrationBuilder.CreateIndex(
                name: "student_dept_id",
                table: "tb_student",
                column: "deptId");

            migrationBuilder.CreateIndex(
                name: "student_id",
                table: "tb_student",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "stuscore_course_id",
                table: "tb_stuscore",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "stuscore_exam_id",
                table: "tb_stuscore",
                column: "examId");

            migrationBuilder.CreateIndex(
                name: "stuscore_id",
                table: "tb_stuscore",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "stuscore_stu_id",
                table: "tb_stuscore",
                column: "stuId");

            migrationBuilder.CreateIndex(
                name: "stuscore_stu_id_exam_id",
                table: "tb_stuscore",
                columns: new[] { "stuId", "examId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "teacher_dept_id",
                table: "tb_teacher",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "teacher_id",
                table: "tb_teacher",
                column: "id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_admin");

            migrationBuilder.DropTable(
                name: "tb_courseclasses");

            migrationBuilder.DropTable(
                name: "tb_examclasses");

            migrationBuilder.DropTable(
                name: "tb_examjobs");

            migrationBuilder.DropTable(
                name: "tb_judge");

            migrationBuilder.DropTable(
                name: "tb_select");

            migrationBuilder.DropTable(
                name: "tb_stuanswerdetail");

            migrationBuilder.DropTable(
                name: "tb_stuscore");

            migrationBuilder.DropTable(
                name: "tb_chapter");

            migrationBuilder.DropTable(
                name: "tb_examquestion");

            migrationBuilder.DropTable(
                name: "tb_student");

            migrationBuilder.DropTable(
                name: "tb_exam");

            migrationBuilder.DropTable(
                name: "tb_classes");

            migrationBuilder.DropTable(
                name: "tb_course");

            migrationBuilder.DropTable(
                name: "tb_teacher");

            migrationBuilder.DropTable(
                name: "tb_dept");
        }
    }
}
