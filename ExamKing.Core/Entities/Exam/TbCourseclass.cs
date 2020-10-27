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
        public int Courseld { get; set; }
        public int Classesld { get; set; }

        public virtual TbClass ClassesldNavigation { get; set; }
        public virtual TbCourse CourseldNavigation { get; set; }

        public void Configure(EntityTypeBuilder<TbCourseclass> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasKey(d => new
            {
                d.Classesld,
                d.Courseld
            });

            entityBuilder.ToTable("tb_courseclasses");

            entityBuilder.HasComment("课程班级关联表");

            entityBuilder.HasIndex(e => e.Classesld, "courseclasses_classes_idx");

            entityBuilder.HasIndex(e => e.Courseld, "courseclasses_course_idx");

            entityBuilder.Property(e => e.Classesld)
                .HasColumnName("classesld")
                .HasComment("班级ID");

            entityBuilder.Property(e => e.Courseld)
                .HasColumnName("courseld")
                .HasComment("课程ID");

            entityBuilder.HasOne(d => d.ClassesldNavigation)
                .WithMany(d => d.TbCourseclasses)
                .HasForeignKey(d => d.Classesld)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("courseclasses_classes_idx");

            entityBuilder.HasOne(d => d.CourseldNavigation)
                .WithMany(d => d.TbCourseclasses)
                .HasForeignKey(d => d.Courseld)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("courseclasses_course_idx");


        }
    }
}
