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
        public int Deptld { get; set; }
        public int Teacherld { get; set; }
        public string CreateTime { get; set; }

        public virtual TbDept DeptldNavigation { get; set; }
        public virtual TbTeacher TeacherldNavigation { get; set; }
        public virtual ICollection<TbChapter> TbChapters { get; set; }
        public virtual ICollection<TbExam> TbExams { get; set; }
        public virtual ICollection<TbJudge> TbJudges { get; set; }
        public virtual ICollection<TbSelect> TbSelects { get; set; }
        public virtual ICollection<TbStuscore> TbStuscores { get; set; }

        public void Configure(EntityTypeBuilder<TbCourse> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("tb_course");

            entityBuilder.HasComment("课程表");

            entityBuilder.HasIndex(e => e.Deptld, "course_dept_id");

            entityBuilder.HasIndex(e => e.Id, "course_id")
                .IsUnique();

            entityBuilder.HasIndex(e => e.Teacherld, "course_teacher_id");

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

            entityBuilder.Property(e => e.Deptld)
                .HasColumnName("deptld")
                .HasComment("系别ID");

            entityBuilder.Property(e => e.Teacherld)
                .HasColumnName("teacherld")
                .HasComment("教师ID");

            entityBuilder.HasOne(d => d.DeptldNavigation)
                .WithMany(p => p.TbCourses)
                .HasForeignKey(d => d.Deptld)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("course_dept_id");

            entityBuilder.HasOne(d => d.TeacherldNavigation)
                .WithMany(p => p.TbCourses)
                .HasForeignKey(d => d.Teacherld)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("course_teacher_id");
        }
    }
}
