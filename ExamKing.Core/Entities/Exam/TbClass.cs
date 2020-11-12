using System;
using System.Collections.Generic;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbClass : IEntity, IEntityTypeBuilder<TbClass>
    {
        public TbClass()
        {
            TbStudents = new HashSet<TbStudent>();
        }

        public int Id { get; set; }
        public string ClassesName { get; set; }
        public int DeptId { get; set; }
        public string CreateTime { get; set; }

        public virtual TbDept DeptIdNavigation { get; set; }

        public virtual ICollection<TbStudent> TbStudents { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<TbCourse> TbCourses { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<TbCourseclass> TbCourseclasses { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<TbExam> TbExams { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<TbExamclass> TbExamclasses { get; set; }

        public void Configure(EntityTypeBuilder<TbClass> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("tb_classes");

            entityBuilder.HasComment("班级表");

            entityBuilder.HasIndex(e => e.DeptId, "classes_dept_id");

            entityBuilder.HasIndex(e => e.Id, "classes_id")
                .IsUnique();

            entityBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("ID");

            entityBuilder.Property(e => e.ClassesName)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("classesName")
                .HasComment("班级名称")
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

            entityBuilder.HasOne(d => d.DeptIdNavigation)
                .WithMany(p => p.TbClasses)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("classes_dept_id");

            // 多对多关系
            entityBuilder.HasMany(d => d.TbCourses)
                .WithMany(d => d.TbClasses)
                .UsingEntity<TbCourseclass>(
                  u => u.HasOne(c => c.courseIdNavigation).WithMany(c => c.TbCourseclasses).HasForeignKey(c => c.CourseId)
                , u => u.HasOne(c => c.classesIdNavigation).WithMany(c => c.TbCourseclasses).HasForeignKey(c => c.ClassesId)
                , u =>
                {
                    u.ToTable("tb_courseclasses");
                    u.HasKey(c => new { c.CourseId, c.ClassesId });
                });

            // 多对多关系
            entityBuilder.HasMany(d => d.TbExams)
                .WithMany(d => d.TbClasses)
                .UsingEntity<TbExamclass>(
                  u => u.HasOne(c => c.examIdNavigation).WithMany(c => c.TbExamclasses).HasForeignKey(c => c.ExamId)
                , u => u.HasOne(c => c.classesIdNavigation).WithMany(c => c.TbExamclasses).HasForeignKey(c => c.ClassesId)
                , u =>
                {
                    u.ToTable("tb_examclasses");
                    u.HasKey(c => new { c.ExamId, c.ClassesId });
                });
        }
    }
}
