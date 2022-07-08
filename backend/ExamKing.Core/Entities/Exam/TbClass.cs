using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbClass : IEntity, IEntityTypeBuilder<TbClass>
    {
        public TbClass()
        {
            Students = new HashSet<TbStudent>();
        }

        public int Id { get; set; }
        public string ClassesName { get; set; }
        public int DeptId { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset CreateTime { get; set; }

        public TbDept Dept { get; set; }

        public ICollection<TbStudent> Students { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<TbCourse> Courses { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<TbCourseclass> Courseclasses { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<TbExam> Exams { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public ICollection<TbExamclass> Examclasses { get; set; }

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
                .HasColumnName("createTime")
                .HasComment("创建时间");

            entityBuilder.Property(e => e.DeptId)
                .HasColumnName("DeptId")
                .HasComment("系别ID");

            entityBuilder.HasOne(d => d.Dept)
                .WithMany(p => p.Classes)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("classes_dept_id");

            // 多对多关系
            entityBuilder.HasMany(d => d.Courses)
                .WithMany(d => d.Classes)
                .UsingEntity<TbCourseclass>(
                  u => u.HasOne(c => c.Course).WithMany(c => c.Courseclasses).HasForeignKey(c => c.CourseId)
                , u => u.HasOne(c => c.Classes).WithMany(c => c.Courseclasses).HasForeignKey(c => c.ClassesId)
                , u =>
                {
                    u.ToTable("tb_courseclasses");
                    u.HasKey(c => new { c.CourseId, c.ClassesId });
                });

            // 多对多关系
            entityBuilder.HasMany(d => d.Exams)
                .WithMany(d => d.Classes)
                .UsingEntity<TbExamclass>(
                  u => u.HasOne(c => c.Exam).WithMany(c => c.Examclasses).HasForeignKey(c => c.ExamId)
                , u => u.HasOne(c => c.Classes).WithMany(c => c.Examclasses).HasForeignKey(c => c.ClassesId)
                , u =>
                {
                    u.ToTable("tb_examclasses");
                    u.HasKey(c => new { c.ExamId, c.ClassesId });
                });
        }
    }
}
