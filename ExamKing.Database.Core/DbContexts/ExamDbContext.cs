using ExamKing.Core.Entites;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace ExamKing.Database.Core
{
    [AppDbContext("ExamConnectionString")]
    public class ExamDbContext : AppDbContext<ExamDbContext>
    {
        public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options) { }

        public virtual DbSet<TbChapter> TbChapters { get; set; }
        public virtual DbSet<TbClass> TbClasses { get; set; }
        public virtual DbSet<TbCourse> TbCourses { get; set; }
        public virtual DbSet<TbCourseclass> TbCourseclasses { get; set; }
        public virtual DbSet<TbDept> TbDepts { get; set; }
        public virtual DbSet<TbExam> TbExams { get; set; }
        public virtual DbSet<TbExamclass> TbExamclasses { get; set; }
        public virtual DbSet<TbJudge> TbJudges { get; set; }
        public virtual DbSet<TbQuestiontype> TbQuestiontypes { get; set; }
        public virtual DbSet<TbSelect> TbSelects { get; set; }
        public virtual DbSet<TbStuanswerdetail> TbStuanswerdetails { get; set; }
        public virtual DbSet<TbStudent> TbStudents { get; set; }
        public virtual DbSet<TbStuscore> TbStuscores { get; set; }
        public virtual DbSet<TbTeacher> TbTeachers { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.entityBuilder<TbChapter>(entityBuilder =>
        //    {
        //        entityBuilder.ToTable("tb_chapter");

        //        entityBuilder.HasComment("课程章节表");

        //        entityBuilder.HasIndex(e => e.Courseld, "chapter_course_id");

        //        entityBuilder.HasIndex(e => e.Id, "chapter_id")
        //            .IsUnique();

        //        entityBuilder.Property(e => e.Id)
        //            .HasColumnName("id")
        //            .HasComment("ID");

        //        entityBuilder.Property(e => e.ChapterName)
        //            .IsRequired()
        //            .HasColumnType("varchar(100)")
        //            .HasColumnName("chapterName")
        //            .HasComment("章节名称")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Courseld)
        //            .HasColumnName("courseld")
        //            .HasComment("课程ID");

        //        entityBuilder.Property(e => e.Desc)
        //            .IsRequired()
        //            .HasColumnType("varchar(200)")
        //            .HasColumnName("desc")
        //            .HasComment("章节描述")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.HasOne(d => d.CourseldNavigation)
        //            .WithMany(p => p.TbChapters)
        //            .HasForeignKey(d => d.Courseld)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("chapter_course_id");
        //    });

        //    modelBuilder.entityBuilder<TbClass>(entityBuilder =>
        //    {
        //        entityBuilder.ToTable("tb_classes");

        //        entityBuilder.HasComment("班级表");

        //        entityBuilder.HasIndex(e => e.Deptld, "classes_dept_id");

        //        entityBuilder.HasIndex(e => e.Id, "classes_id")
        //            .IsUnique();

        //        entityBuilder.Property(e => e.Id)
        //            .HasColumnName("id")
        //            .HasComment("ID");

        //        entityBuilder.Property(e => e.ClassesName)
        //            .IsRequired()
        //            .HasColumnType("varchar(50)")
        //            .HasColumnName("classesName")
        //            .HasComment("班级名称")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.CreateTime)
        //            .IsRequired()
        //            .HasColumnType("varchar(50)")
        //            .HasColumnName("createTime")
        //            .HasComment("创建时间")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Deptld)
        //            .HasColumnName("deptld")
        //            .HasComment("系别ID");

        //        entityBuilder.HasOne(d => d.DeptldNavigation)
        //            .WithMany(p => p.TbClasses)
        //            .HasForeignKey(d => d.Deptld)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("classes_dept_id");
        //    });

        //    modelBuilder.entityBuilder<TbCourse>(entityBuilder =>
        //    {
        //        entityBuilder.ToTable("tb_course");

        //        entityBuilder.HasComment("课程表");

        //        entityBuilder.HasIndex(e => e.Deptld, "course_dept_id");

        //        entityBuilder.HasIndex(e => e.Id, "course_id")
        //            .IsUnique();

        //        entityBuilder.HasIndex(e => e.Teacherld, "course_teacher_id");

        //        entityBuilder.Property(e => e.Id)
        //            .HasColumnName("id")
        //            .HasComment("ID");

        //        entityBuilder.Property(e => e.CourseName)
        //            .IsRequired()
        //            .HasColumnType("varchar(100)")
        //            .HasColumnName("courseName")
        //            .HasComment("课程名称")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.CreateTime)
        //            .IsRequired()
        //            .HasColumnType("varchar(50)")
        //            .HasColumnName("createTime")
        //            .HasComment("创建时间")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Deptld)
        //            .HasColumnName("deptld")
        //            .HasComment("系别ID");

        //        entityBuilder.Property(e => e.Teacherld)
        //            .HasColumnName("teacherld")
        //            .HasComment("教师ID");

        //        entityBuilder.HasOne(d => d.DeptldNavigation)
        //            .WithMany(p => p.TbCourses)
        //            .HasForeignKey(d => d.Deptld)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("course_dept_id");

        //        entityBuilder.HasOne(d => d.TeacherldNavigation)
        //            .WithMany(p => p.TbCourses)
        //            .HasForeignKey(d => d.Teacherld)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("course_teacher_id");
        //    });

        //    modelBuilder.entityBuilder<TbCourseclass>(entityBuilder =>
        //    {
        //        entityBuilder.HasNoKey();

        //        entityBuilder.ToTable("tb_courseclasses");

        //        entityBuilder.HasComment("课程班级关联表");

        //        entityBuilder.HasIndex(e => e.Classesld, "classes_id");

        //        entityBuilder.HasIndex(e => e.Courseld, "course_id");

        //        entityBuilder.Property(e => e.Classesld)
        //            .HasColumnName("classesld")
        //            .HasComment("班级ID");

        //        entityBuilder.Property(e => e.Courseld)
        //            .HasColumnName("courseld")
        //            .HasComment("课程ID");

        //        entityBuilder.HasOne(d => d.ClassesldNavigation)
        //            .WithMany()
        //            .HasForeignKey(d => d.Classesld)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("classes_id");

        //        entityBuilder.HasOne(d => d.CourseldNavigation)
        //            .WithMany()
        //            .HasForeignKey(d => d.Courseld)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("course_id");
        //    });

        //    modelBuilder.entityBuilder<TbDept>(entityBuilder =>
        //    {
        //        entityBuilder.ToTable("tb_dept");

        //        entityBuilder.HasComment("系别表");

        //        entityBuilder.HasIndex(e => e.Id, "dept_id")
        //            .IsUnique();

        //        entityBuilder.Property(e => e.Id)
        //            .HasColumnName("id")
        //            .HasComment("ID");

        //        entityBuilder.Property(e => e.CreateTime)
        //            .IsRequired()
        //            .HasColumnType("varchar(50)")
        //            .HasColumnName("createTime")
        //            .HasComment("创建时间")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.DeptName)
        //            .IsRequired()
        //            .HasColumnType("varchar(50)")
        //            .HasColumnName("deptName")
        //            .HasComment("系别名称")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");
        //    });

        //    modelBuilder.entityBuilder<TbExam>(entityBuilder =>
        //    {
        //        entityBuilder.ToTable("tb_exam");

        //        entityBuilder.HasComment("试卷表");

        //        entityBuilder.HasIndex(e => e.Courseld, "exam_course_id");

        //        entityBuilder.HasIndex(e => e.Id, "exam_id")
        //            .IsUnique();

        //        entityBuilder.HasIndex(e => e.Teacherld, "exam_teacher_id");

        //        entityBuilder.Property(e => e.Id)
        //            .HasColumnName("id")
        //            .HasComment("ID");

        //        entityBuilder.Property(e => e.Courseld)
        //            .HasColumnName("courseld")
        //            .HasComment("课程ID");

        //        entityBuilder.Property(e => e.CreateTime)
        //            .IsRequired()
        //            .HasColumnType("varchar(30)")
        //            .HasColumnName("createTime")
        //            .HasComment("创建时间")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Duration)
        //            .HasColumnName("duration")
        //            .HasComment("考试时长");

        //        entityBuilder.Property(e => e.EndTime)
        //            .IsRequired()
        //            .HasColumnType("varchar(30)")
        //            .HasColumnName("endTime")
        //            .HasComment("结束时间")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.ExamName)
        //            .IsRequired()
        //            .HasColumnType("varchar(200)")
        //            .HasColumnName("examName")
        //            .HasComment("试卷名称")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.ExamScore)
        //            .HasColumnName("examScore")
        //            .HasComment("试卷总分");

        //        entityBuilder.Property(e => e.IsEnable)
        //            .IsRequired()
        //            .HasColumnType("varchar(10)")
        //            .HasColumnName("isEnable")
        //            .HasComment("启用状态")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.IsFinish)
        //            .IsRequired()
        //            .HasColumnType("varchar(10)")
        //            .HasColumnName("isFinish")
        //            .HasComment("结束状态")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.JudgeScore)
        //            .HasColumnName("judgeScore")
        //            .HasComment("是非题分值");

        //        entityBuilder.Property(e => e.Judges)
        //            .IsRequired()
        //            .HasColumnType("varchar(200)")
        //            .HasColumnName("judges")
        //            .HasComment("是非题")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.SelectScore)
        //            .HasColumnName("selectScore")
        //            .HasComment("多选题分值");

        //        entityBuilder.Property(e => e.Selects)
        //            .IsRequired()
        //            .HasColumnType("varchar(200)")
        //            .HasColumnName("selects")
        //            .HasComment("多选题")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.SingleScore)
        //            .HasColumnName("singleScore")
        //            .HasComment("单选题分值");

        //        entityBuilder.Property(e => e.Singles)
        //            .IsRequired()
        //            .HasColumnType("varchar(200)")
        //            .HasColumnName("singles")
        //            .HasComment("单选题")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.StartTime)
        //            .IsRequired()
        //            .HasColumnType("varchar(30)")
        //            .HasColumnName("startTime")
        //            .HasComment("开始时间")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Teacherld)
        //            .HasColumnName("teacherld")
        //            .HasComment("教师ID");

        //        entityBuilder.HasOne(d => d.CourseldNavigation)
        //            .WithMany(p => p.TbExams)
        //            .HasForeignKey(d => d.Courseld)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("exam_course_id");

        //        entityBuilder.HasOne(d => d.TeacherldNavigation)
        //            .WithMany(p => p.TbExams)
        //            .HasForeignKey(d => d.Teacherld)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("exam_teacher_id");
        //    });

        //    modelBuilder.entityBuilder<TbExamclass>(entityBuilder =>
        //    {
        //        entityBuilder.HasNoKey();

        //        entityBuilder.ToTable("tb_examclasses");

        //        entityBuilder.HasComment("试卷班级关联表");

        //        entityBuilder.Property(e => e.Classesld)
        //            .HasColumnName("classesld")
        //            .HasComment("班级ID");

        //        entityBuilder.Property(e => e.Examld)
        //            .HasColumnName("examld")
        //            .HasComment("试卷ID");
        //    });

        //    modelBuilder.entityBuilder<TbJudge>(entityBuilder =>
        //    {
        //        entityBuilder.ToTable("tb_judge");

        //        entityBuilder.HasComment("是非题表");

        //        entityBuilder.HasIndex(e => e.Chapterld, "judge_chapter_id");

        //        entityBuilder.HasIndex(e => e.Id, "judge_id")
        //            .IsUnique();

        //        entityBuilder.HasIndex(e => e.Courseld, "judge_source_id");

        //        entityBuilder.HasIndex(e => e.Teacherld, "judge_teacher_id");

        //        entityBuilder.Property(e => e.Id)
        //            .HasColumnName("id")
        //            .HasComment("ID");

        //        entityBuilder.Property(e => e.Answer)
        //            .IsRequired()
        //            .HasColumnType("varchar(10)")
        //            .HasColumnName("answer")
        //            .HasComment("答案")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Chapterld)
        //            .HasColumnName("chapterld")
        //            .HasComment("课程章节ID");

        //        entityBuilder.Property(e => e.Courseld)
        //            .HasColumnName("courseld")
        //            .HasComment("课程ID");

        //        entityBuilder.Property(e => e.CreateTime)
        //            .IsRequired()
        //            .HasColumnType("varchar(30)")
        //            .HasColumnName("createTime")
        //            .HasComment("创建时间")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Ideas)
        //            .IsRequired()
        //            .HasColumnType("varchar(300)")
        //            .HasColumnName("ideas")
        //            .HasComment("解题思路")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Question)
        //            .IsRequired()
        //            .HasColumnType("varchar(200)")
        //            .HasColumnName("question")
        //            .HasComment("题目")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Teacherld)
        //            .HasColumnName("teacherld")
        //            .HasComment("教师ID");

        //        entityBuilder.HasOne(d => d.ChapterldNavigation)
        //            .WithMany(p => p.TbJudges)
        //            .HasForeignKey(d => d.Chapterld)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("judge_chapter_id");

        //        entityBuilder.HasOne(d => d.CourseldNavigation)
        //            .WithMany(p => p.TbJudges)
        //            .HasForeignKey(d => d.Courseld)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("judge_source_id");

        //        entityBuilder.HasOne(d => d.TeacherldNavigation)
        //            .WithMany(p => p.TbJudges)
        //            .HasForeignKey(d => d.Teacherld)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("judge_teacher_id");
        //    });

        //    modelBuilder.entityBuilder<TbQuestiontype>(entityBuilder =>
        //    {
        //        entityBuilder.ToTable("tb_questiontype");

        //        entityBuilder.HasComment("题型实体");

        //        entityBuilder.HasIndex(e => e.Id, "questiontype_id")
        //            .IsUnique();

        //        entityBuilder.Property(e => e.Id)
        //            .HasColumnName("id")
        //            .HasComment("ID");

        //        entityBuilder.Property(e => e.CreateTime)
        //            .IsRequired()
        //            .HasColumnType("varchar(30)")
        //            .HasColumnName("createTime")
        //            .HasComment("创建时间")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.TypeName)
        //            .IsRequired()
        //            .HasColumnType("varchar(30)")
        //            .HasColumnName("typeName")
        //            .HasComment("题型名称")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");
        //    });

        //    modelBuilder.entityBuilder<TbSelect>(entityBuilder =>
        //    {
        //        entityBuilder.ToTable("tb_select");

        //        entityBuilder.HasComment("选择题表");

        //        entityBuilder.HasIndex(e => e.ChapterId, "select_chapter_id");

        //        entityBuilder.HasIndex(e => e.Id, "select_id")
        //            .IsUnique();

        //        entityBuilder.HasIndex(e => e.CourseId, "select_source_id");

        //        entityBuilder.HasIndex(e => e.TeacherId, "select_teacher_id");

        //        entityBuilder.Property(e => e.Id)
        //            .HasColumnName("id")
        //            .HasComment("ID");

        //        entityBuilder.Property(e => e.Answer)
        //            .IsRequired()
        //            .HasColumnType("varchar(50)")
        //            .HasColumnName("answer")
        //            .HasComment("答案")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.ChapterId)
        //            .HasColumnName("chapterId")
        //            .HasComment("课程章节ID");

        //        entityBuilder.Property(e => e.CourseId)
        //            .HasColumnName("courseId")
        //            .HasComment("课程ID");

        //        entityBuilder.Property(e => e.CreateTime)
        //            .IsRequired()
        //            .HasColumnType("varchar(30)")
        //            .HasColumnName("createTime")
        //            .HasComment("创建时间")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Ideas)
        //            .IsRequired()
        //            .HasColumnType("varchar(300)")
        //            .HasColumnName("ideas")
        //            .HasComment("解题思路")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.IsSingle)
        //            .IsRequired()
        //            .HasColumnType("varchar(10)")
        //            .HasColumnName("isSingle")
        //            .HasComment("是否单选")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.OptionA)
        //            .IsRequired()
        //            .HasColumnType("varchar(300)")
        //            .HasColumnName("optionA")
        //            .HasComment("选项A")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.OptionB)
        //            .IsRequired()
        //            .HasColumnType("varchar(300)")
        //            .HasColumnName("optionB")
        //            .HasComment("选项B")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.OptionC)
        //            .IsRequired()
        //            .HasColumnType("varchar(300)")
        //            .HasColumnName("optionC")
        //            .HasComment("选项C")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.OptionD)
        //            .IsRequired()
        //            .HasColumnType("varchar(300)")
        //            .HasColumnName("optionD")
        //            .HasComment("选项D")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Question)
        //            .IsRequired()
        //            .HasColumnType("varchar(300)")
        //            .HasColumnName("question")
        //            .HasComment("问题")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.TeacherId)
        //            .HasColumnName("teacherId")
        //            .HasComment("教师ID");

        //        entityBuilder.HasOne(d => d.Chapter)
        //            .WithMany(p => p.TbSelects)
        //            .HasForeignKey(d => d.ChapterId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("select_chapter_id");

        //        entityBuilder.HasOne(d => d.Course)
        //            .WithMany(p => p.TbSelects)
        //            .HasForeignKey(d => d.CourseId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("select_source_id");

        //        entityBuilder.HasOne(d => d.Teacher)
        //            .WithMany(p => p.TbSelects)
        //            .HasForeignKey(d => d.TeacherId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("select_teacher_id");
        //    });

        //    modelBuilder.entityBuilder<TbStuanswerdetail>(entityBuilder =>
        //    {
        //        entityBuilder.ToTable("tb_stuanswerdetail");

        //        entityBuilder.HasComment("答题明细表");

        //        entityBuilder.HasIndex(e => e.Examld, "answerdetail_exam_id");

        //        entityBuilder.HasIndex(e => e.QuesionTypeld, "answerdetail_quesiotype_id");

        //        entityBuilder.HasIndex(e => e.Stuld, "answerdetail_stu_id");

        //        entityBuilder.HasIndex(e => e.Id, "stuanseerdetail_id")
        //            .IsUnique();

        //        entityBuilder.Property(e => e.Id)
        //            .HasColumnName("id")
        //            .HasComment("ID");

        //        entityBuilder.Property(e => e.Answer)
        //            .IsRequired()
        //            .HasColumnType("varchar(100)")
        //            .HasColumnName("answer")
        //            .HasComment("正确答案")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.CreateTime)
        //            .IsRequired()
        //            .HasColumnType("varchar(30)")
        //            .HasColumnName("createTime")
        //            .HasComment("创建时间")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Examld)
        //            .HasColumnName("examld")
        //            .HasComment("试卷ID");

        //        entityBuilder.Property(e => e.Isright)
        //            .IsRequired()
        //            .HasColumnType("varchar(10)")
        //            .HasColumnName("isright")
        //            .HasComment("是否正确")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.QuesionTypeld)
        //            .HasColumnName("quesionTypeld")
        //            .HasComment("题型ID");

        //        entityBuilder.Property(e => e.Questionld)
        //            .HasColumnName("questionld")
        //            .HasComment("题目ID");

        //        entityBuilder.Property(e => e.Stuanswer)
        //            .IsRequired()
        //            .HasColumnType("varchar(100)")
        //            .HasColumnName("stuanswer")
        //            .HasComment("学生答案")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Stuld)
        //            .HasColumnName("stuld")
        //            .HasComment("学生ID");

        //        entityBuilder.HasOne(d => d.ExamldNavigation)
        //            .WithMany(p => p.TbStuanswerdetails)
        //            .HasForeignKey(d => d.Examld)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("answerdetail_exam_id");

        //        entityBuilder.HasOne(d => d.QuesionTypeldNavigation)
        //            .WithMany(p => p.TbStuanswerdetails)
        //            .HasForeignKey(d => d.QuesionTypeld)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("answerdetail_quesiotype_id");

        //        entityBuilder.HasOne(d => d.StuldNavigation)
        //            .WithMany(p => p.TbStuanswerdetails)
        //            .HasForeignKey(d => d.Stuld)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("answerdetail_stu_id");
        //    });

        //    modelBuilder.entityBuilder<TbStudent>(entityBuilder =>
        //    {
        //        entityBuilder.ToTable("tb_student");

        //        entityBuilder.HasComment("学生表");

        //        entityBuilder.HasIndex(e => e.ClassesId, "student_classes_id");

        //        entityBuilder.HasIndex(e => e.DeptId, "student_dept_id");

        //        entityBuilder.HasIndex(e => e.Id, "student_id")
        //            .IsUnique();

        //        entityBuilder.Property(e => e.Id)
        //            .HasColumnName("id")
        //            .HasComment("ID");

        //        entityBuilder.Property(e => e.ClassesId)
        //            .HasColumnName("classesId")
        //            .HasComment("班级ID");

        //        entityBuilder.Property(e => e.CreateTime)
        //            .IsRequired()
        //            .HasColumnType("varchar(50)")
        //            .HasColumnName("createTime")
        //            .HasComment("创建时间")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.DeptId)
        //            .HasColumnName("deptId")
        //            .HasComment("系别ID");

        //        entityBuilder.Property(e => e.IdCard)
        //            .IsRequired()
        //            .HasColumnType("varchar(30)")
        //            .HasColumnName("idCard")
        //            .HasComment("身份证号码")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Password)
        //            .IsRequired()
        //            .HasColumnType("varchar(50)")
        //            .HasColumnName("password")
        //            .HasComment("密码")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Sex)
        //            .IsRequired()
        //            .HasColumnType("varchar(10)")
        //            .HasColumnName("sex")
        //            .HasComment("性别")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.StuName)
        //            .IsRequired()
        //            .HasColumnType("varchar(50)")
        //            .HasColumnName("stuName")
        //            .HasComment("姓名")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.StuNo)
        //            .IsRequired()
        //            .HasColumnType("varchar(50)")
        //            .HasColumnName("stuNo")
        //            .HasComment("学号")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Telphone)
        //            .IsRequired()
        //            .HasColumnType("varchar(30)")
        //            .HasColumnName("telphone")
        //            .HasComment("联系电话")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.HasOne(d => d.Classes)
        //            .WithMany(p => p.TbStudents)
        //            .HasForeignKey(d => d.ClassesId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("student_classes_id");

        //        entityBuilder.HasOne(d => d.Dept)
        //            .WithMany(p => p.TbStudents)
        //            .HasForeignKey(d => d.DeptId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("student_dept_id");
        //    });

        //    modelBuilder.entityBuilder<TbStuscore>(entityBuilder =>
        //    {
        //        entityBuilder.ToTable("tb_stuscore");

        //        entityBuilder.HasComment("学生成绩表");

        //        entityBuilder.HasIndex(e => e.CourseId, "stuscore_course_id");

        //        entityBuilder.HasIndex(e => e.ExamId, "stuscore_exam_id");

        //        entityBuilder.HasIndex(e => e.Id, "stuscore_id")
        //            .IsUnique();

        //        entityBuilder.HasIndex(e => e.StuId, "stuscore_stu_id");

        //        entityBuilder.Property(e => e.Id)
        //            .HasColumnName("id")
        //            .HasComment("ID");

        //        entityBuilder.Property(e => e.CourseId)
        //            .HasColumnName("courseId")
        //            .HasComment("课程ID");

        //        entityBuilder.Property(e => e.CreateTime)
        //            .IsRequired()
        //            .HasColumnType("varchar(30)")
        //            .HasColumnName("createTime")
        //            .HasComment("创建时间")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.ExamId)
        //            .HasColumnName("examId")
        //            .HasComment("考试ID");

        //        entityBuilder.Property(e => e.Score)
        //            .HasColumnName("score")
        //            .HasComment("分数");

        //        entityBuilder.Property(e => e.StuId)
        //            .HasColumnName("stuId")
        //            .HasComment("学生ID");

        //        entityBuilder.HasOne(d => d.Course)
        //            .WithMany(p => p.TbStuscores)
        //            .HasForeignKey(d => d.CourseId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("stuscore_course_id");

        //        entityBuilder.HasOne(d => d.Exam)
        //            .WithMany(p => p.TbStuscores)
        //            .HasForeignKey(d => d.ExamId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("stuscore_exam_id");

        //        entityBuilder.HasOne(d => d.Stu)
        //            .WithMany(p => p.TbStuscores)
        //            .HasForeignKey(d => d.StuId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("stuscore_stu_id");
        //    });

        //    modelBuilder.entityBuilder<TbTeacher>(entityBuilder =>
        //    {
        //        entityBuilder.ToTable("tb_teacher");

        //        entityBuilder.HasComment("教师表");

        //        entityBuilder.HasIndex(e => e.Deptld, "teacher_dept_id");

        //        entityBuilder.HasIndex(e => e.Id, "teacher_id")
        //            .IsUnique();

        //        entityBuilder.Property(e => e.Id)
        //            .HasColumnName("id")
        //            .HasComment("ID");

        //        entityBuilder.Property(e => e.CreateTime)
        //            .IsRequired()
        //            .HasColumnType("varchar(50)")
        //            .HasColumnName("createTime")
        //            .HasComment("创建时间")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Deptld)
        //            .HasColumnName("deptld")
        //            .HasComment("系别ID");

        //        entityBuilder.Property(e => e.IdCard)
        //            .IsRequired()
        //            .HasColumnType("varchar(20)")
        //            .HasColumnName("idCard")
        //            .HasComment("身份证号")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Password)
        //            .IsRequired()
        //            .HasColumnType("varchar(50)")
        //            .HasColumnName("password")
        //            .HasComment("密码")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Sex)
        //            .IsRequired()
        //            .HasColumnType("varchar(10)")
        //            .HasColumnName("sex")
        //            .HasComment("性别")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.TeacherName)
        //            .IsRequired()
        //            .HasColumnType("varchar(50)")
        //            .HasColumnName("teacherName")
        //            .HasComment("姓名")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.TeacherNo)
        //            .IsRequired()
        //            .HasColumnType("varchar(20)")
        //            .HasColumnName("teacherNo")
        //            .HasComment("教师编号")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.Property(e => e.Telphone)
        //            .IsRequired()
        //            .HasColumnType("varchar(25)")
        //            .HasColumnName("telphone")
        //            .HasComment("联系电话")
        //            .HasCharSet("utf8")
        //            .HasCollation("utf8_general_ci");

        //        entityBuilder.HasOne(d => d.DeptldNavigation)
        //            .WithMany(p => p.TbTeachers)
        //            .HasForeignKey(d => d.Deptld)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("teacher_dept_id");
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}

        //private virtual OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}