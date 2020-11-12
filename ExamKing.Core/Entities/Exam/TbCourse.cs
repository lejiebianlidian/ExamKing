using System;
using System.Collections.Generic;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbCourse : IEntity, IEntityTypeBuilder<TbCourse>
    {
        public TbCourse()
        {
            TbChapters = new HashSet<TbChapter>();
            TbExams = new HashSet<TbExam>();
            TbJudges = new HashSet<TbJudge>();
            TbSelects = new HashSet<TbSelect>();
            TbStuscores = new HashSet<TbStuscore>();
        }

        public int Id { get; set; }
        public string CourseName { get; set; }
        public int DeptId { get; set; }
        public int TeacherId { get; set; }
        public string CreateTime { get; set; }

        public TbDept Dept { get; set; }
        public TbTeacher Teacher { get; set; }
        public ICollection<TbChapter> TbChapters { get; set; }
        public ICollection<TbExam> TbExams { get; set; }
        public ICollection<TbJudge> TbJudges { get; set; }
        public ICollection<TbSelect> TbSelects { get; set; }
        public ICollection<TbStuscore> TbStuscores { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<TbClass> TbClasses { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<TbCourseclass> TbCourseclasses { get; set; }


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
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("createTime")
                .HasComment("创建时间")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.DeptId)
                .HasColumnName("DeptId")
                .HasComment("系别ID");

            entityBuilder.Property(e => e.TeacherId)
                .HasColumnName("teacherId")
                .HasComment("教师ID");

            entityBuilder.HasOne(d => d.Dept)
                .WithMany(p => p.TbCourses)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("course_dept_id");

            entityBuilder.HasOne(d => d.Teacher)
                .WithMany(p => p.TbCourses)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("course_teacher_id");

            // 多对多关系
            entityBuilder.HasMany(d => d.TbClasses)
                .WithMany(d => d.TbCourses)
                .UsingEntity<TbCourseclass>(
                  u => u.HasOne(c => c.Classes).WithMany(c => c.TbCourseclasses).HasForeignKey(c => c.ClassesId)
                , u => u.HasOne(c => c.Course).WithMany(c => c.TbCourseclasses).HasForeignKey(c => c.CourseId)
                , u =>
                {
                    u.ToTable("tb_courseclasses");
                    u.HasKey(c => new { c.CourseId, c.ClassesId });
                });
        }
    }
}
