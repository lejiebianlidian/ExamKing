using System;
using System.Collections.Generic;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbCourseclass : IEntity, IEntityTypeBuilder<TbCourseclass>
    {
        public int CourseId { get; set; }
        public int ClassesId { get; set; }

        public virtual TbClass classesIdNavigation { get; set; }
        public virtual TbCourse courseIdNavigation { get; set; }

        public void Configure(EntityTypeBuilder<TbCourseclass> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasKey(d => new
            {
                d.ClassesId,
                d.CourseId
            });

            entityBuilder.ToTable("tb_courseclasses");

            entityBuilder.HasComment("课程班级关联表");

            entityBuilder.HasIndex(e => e.ClassesId, "courseclasses_classes_idx");

            entityBuilder.HasIndex(e => e.CourseId, "courseclasses_course_idx");

            entityBuilder.Property(e => e.ClassesId)
                .HasColumnName("classesId")
                .HasComment("班级ID");

            entityBuilder.Property(e => e.CourseId)
                .HasColumnName("courseId")
                .HasComment("课程ID");

            entityBuilder.HasOne(d => d.classesIdNavigation)
                .WithMany(d => d.TbCourseclasses)
                .HasForeignKey(d => d.ClassesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("courseclasses_classes_idx");

            entityBuilder.HasOne(d => d.courseIdNavigation)
                .WithMany(d => d.TbCourseclasses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("courseclasses_course_idx");


        }
    }
}
