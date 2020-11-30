using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbExam : IEntity, IEntityTypeBuilder<TbExam>
    {
        public TbExam()
        {
            Stuanswerdetails = new HashSet<TbStuanswerdetail>();
            Stuscores = new HashSet<TbStuscore>();
            Examquestions = new HashSet<TbExamquestion>();
        }

        public int Id { get; set; }
        public string ExamName { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public int Duration { get; set; }
        public string IsEnable { get; set; }
        public string IsFinish { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset CreateTime { get; set; }
        public int ExamScore { get; set; }
        public int JudgeScore { get; set; }
        public int SingleScore { get; set; }
        public int SelectScore { get; set; }

        public TbCourse Course { get; set; }
        public TbTeacher Teacher { get; set; }
        public ICollection<TbStuanswerdetail> Stuanswerdetails { get; set; }
        public ICollection<TbStuscore> Stuscores { get; set; }
        public ICollection<TbExamquestion> Examquestions { get; set; }
        public ICollection<TbExamjobs> Examjobses { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<TbClass> Classes { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public ICollection<TbExamclass> Examclasses { get; set; }

        public void Configure(EntityTypeBuilder<TbExam> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("tb_exam");

            entityBuilder.HasComment("试卷表");

            entityBuilder.HasIndex(e => e.CourseId, "exam_course_id");

            entityBuilder.HasIndex(e => e.Id, "exam_id")
                .IsUnique();

            entityBuilder.HasIndex(e => e.TeacherId, "exam_teacher_id");

            entityBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("ID");

            entityBuilder.Property(e => e.CourseId)
                .HasColumnName("courseId")
                .HasComment("课程ID");

            entityBuilder.Property(e => e.CreateTime)
                .HasColumnName("createTime")
                .HasComment("创建时间");

            entityBuilder.Property(e => e.Duration)
                .HasColumnName("duration")
                .HasComment("考试时长");

            entityBuilder.Property(e => e.EndTime)
                .IsRequired()
                .HasColumnName("endTime")
                .HasComment("结束时间");

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

            entityBuilder.Property(e => e.SelectScore)
                .HasColumnName("selectScore")
                .HasComment("多选题分值");


            entityBuilder.Property(e => e.SingleScore)
                .HasColumnName("singleScore")
                .HasComment("单选题分值");

            entityBuilder.Property(e => e.StartTime)
                .IsRequired()
                .HasColumnName("startTime")
                .HasComment("开始时间");

            entityBuilder.Property(e => e.TeacherId)
                .HasColumnName("teacherId")
                .HasComment("教师ID");

            entityBuilder.HasOne(d => d.Course)
                .WithMany(p => p.Exams)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("exam_course_id");

            entityBuilder.HasOne(d => d.Teacher)
                .WithMany(p => p.Exams)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("exam_teacher_id");

            //多对多关系
            entityBuilder.HasMany(d => d.Classes)
                .WithMany(d => d.Exams)
                .UsingEntity<TbExamclass>(
                  u => u.HasOne(c => c.Classes).WithMany(c => c.Examclasses).HasForeignKey(c => c.ClassesId)
                , u => u.HasOne(c => c.Exam).WithMany(c => c.Examclasses).HasForeignKey(c => c.ExamId)
                , u =>
                {
                    u.ToTable("tb_examclasses");
                    u.HasKey(c => new { c.ExamId, c.ClassesId });
                });
        }
    }
}
