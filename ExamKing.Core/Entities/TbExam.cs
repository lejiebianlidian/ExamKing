using System;
using System.Collections.Generic;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbExam : IEntity, IEntityTypeBuilder<TbExam>
    {
        public TbExam()
        {
            TbStuanswerdetails = new HashSet<TbStuanswerdetail>();
            TbStuscores = new HashSet<TbStuscore>();
        }

        public int Id { get; set; }
        public string ExamName { get; set; }
        public int Courseld { get; set; }
        public int Teacherld { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Judges { get; set; }
        public string Singles { get; set; }
        public string Selects { get; set; }
        public int Duration { get; set; }
        public string IsEnable { get; set; }
        public string IsFinish { get; set; }
        public string CreateTime { get; set; }
        public int ExamScore { get; set; }
        public int JudgeScore { get; set; }
        public int SingleScore { get; set; }
        public int SelectScore { get; set; }

        public virtual TbCourse CourseldNavigation { get; set; }
        public virtual TbTeacher TeacherldNavigation { get; set; }
        public virtual ICollection<TbStuanswerdetail> TbStuanswerdetails { get; set; }
        public virtual ICollection<TbStuscore> TbStuscores { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<TbClass> TbClasses { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<TbExamclass> TbExamclasses { get; set; }

        public void Configure(EntityTypeBuilder<TbExam> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("tb_exam");

            entityBuilder.HasComment("试卷表");

            entityBuilder.HasIndex(e => e.Courseld, "exam_course_id");

            entityBuilder.HasIndex(e => e.Id, "exam_id")
                .IsUnique();

            entityBuilder.HasIndex(e => e.Teacherld, "exam_teacher_id");

            entityBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("ID");

            entityBuilder.Property(e => e.Courseld)
                .HasColumnName("courseld")
                .HasComment("课程ID");

            entityBuilder.Property(e => e.CreateTime)
                .IsRequired()
                .HasColumnType("varchar(30)")
                .HasColumnName("createTime")
                .HasComment("创建时间")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.Duration)
                .HasColumnName("duration")
                .HasComment("考试时长");

            entityBuilder.Property(e => e.EndTime)
                .IsRequired()
                .HasColumnType("varchar(30)")
                .HasColumnName("endTime")
                .HasComment("结束时间")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.ExamName)
                .IsRequired()
                .HasColumnType("varchar(200)")
                .HasColumnName("examName")
                .HasComment("试卷名称")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.ExamScore)
                .HasColumnName("examScore")
                .HasComment("试卷总分");

            entityBuilder.Property(e => e.IsEnable)
                .IsRequired()
                .HasColumnType("varchar(10)")
                .HasColumnName("isEnable")
                .HasComment("启用状态")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.IsFinish)
                .IsRequired()
                .HasColumnType("varchar(10)")
                .HasColumnName("isFinish")
                .HasComment("结束状态")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.JudgeScore)
                .HasColumnName("judgeScore")
                .HasComment("是非题分值");

            entityBuilder.Property(e => e.Judges)
                .IsRequired()
                .HasColumnType("varchar(200)")
                .HasColumnName("judges")
                .HasComment("是非题")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.SelectScore)
                .HasColumnName("selectScore")
                .HasComment("多选题分值");

            entityBuilder.Property(e => e.Selects)
                .IsRequired()
                .HasColumnType("varchar(200)")
                .HasColumnName("selects")
                .HasComment("多选题")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.SingleScore)
                .HasColumnName("singleScore")
                .HasComment("单选题分值");

            entityBuilder.Property(e => e.Singles)
                .IsRequired()
                .HasColumnType("varchar(200)")
                .HasColumnName("singles")
                .HasComment("单选题")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.StartTime)
                .IsRequired()
                .HasColumnType("varchar(30)")
                .HasColumnName("startTime")
                .HasComment("开始时间")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.Teacherld)
                .HasColumnName("teacherld")
                .HasComment("教师ID");

            entityBuilder.HasOne(d => d.CourseldNavigation)
                .WithMany(p => p.TbExams)
                .HasForeignKey(d => d.Courseld)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("exam_course_id");

            entityBuilder.HasOne(d => d.TeacherldNavigation)
                .WithMany(p => p.TbExams)
                .HasForeignKey(d => d.Teacherld)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("exam_teacher_id");
        }
    }
}
