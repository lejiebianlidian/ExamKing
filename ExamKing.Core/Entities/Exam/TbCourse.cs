using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbCourse : IEntity, IEntityTypeBuilder<TbCourse>
    {
        public TbCourse()
        {
            Chapters = new HashSet<TbChapter>();
            Exams = new HashSet<TbExam>();
            Judges = new HashSet<TbJudge>();
            Selects = new HashSet<TbSelect>();
            Stuscores = new HashSet<TbStuscore>();
        }

        public int Id { get; set; }
        public string CourseName { get; set; }
        public int DeptId { get; set; }
        public int TeacherId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset CreateTime { get; set; }

        public TbDept Dept { get; set; }
        public TbTeacher Teacher { get; set; }
        public ICollection<TbChapter> Chapters { get; set; }
        public ICollection<TbExam> Exams { get; set; }
        public ICollection<TbJudge> Judges { get; set; }
        public ICollection<TbSelect> Selects { get; set; }
        public ICollection<TbStuscore> Stuscores { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<TbClass> Classes { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<TbCourseclass> Courseclasses { get; set; }


        public void Configure(EntityTypeBuilder<TbCourse> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("tb_course");

            entityBuilder.HasComment("课程表");

            entityBuilder.HasIndex(e => e.DeptId, "course_dept_id");

            entityBuilder.HasIndex(e => e.Id, "course_id")
                .IsUnique();

            entityBuilder.HasIndex(e => e.TeacherId, "course_teacher_id");

            entityBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("ID");

            entityBuilder.Property(e => e.CourseName)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasColumnName("courseName")
                .HasComment("课程名称")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.CreateTime)
                .HasColumnName("createTime")
                .HasComment("创建时间");

            entityBuilder.Property(e => e.DeptId)
                .HasColumnName("DeptId")
                .HasComment("系别ID");

            entityBuilder.Property(e => e.TeacherId)
                .HasColumnName("teacherId")
                .HasComment("教师ID");

            entityBuilder.HasOne(d => d.Dept)
                .WithMany(p => p.Courses)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("course_dept_id");

            entityBuilder.HasOne(d => d.Teacher)
                .WithMany(p => p.Courses)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("course_teacher_id");

            // 多对多关系
            entityBuilder.HasMany(d => d.Classes)
                .WithMany(d => d.Courses)
                .UsingEntity<TbCourseclass>(
                  u => u.HasOne(c => c.Classes).WithMany(c => c.Courseclasses).HasForeignKey(c => c.ClassesId)
                , u => u.HasOne(c => c.Course).WithMany(c => c.Courseclasses).HasForeignKey(c => c.CourseId)
                , u =>
                {
                    u.ToTable("tb_courseclasses");
                    u.HasKey(c => new { c.CourseId, c.ClassesId });
                });
        }
    }
}
