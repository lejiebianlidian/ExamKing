using System;
using System.Collections.Generic;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbExamclass : IEntity, IEntityTypeBuilder<TbExamclass>
    {
        public int Examld { get; set; }
        public int Classesld { get; set; }

        public virtual TbClass ClassesldNavigation { get; set; }
        public virtual TbExam ExamldNavigation { get; set; }

        public void Configure(EntityTypeBuilder<TbExamclass> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasKey(d => new
            {
                d.Examld,
                d.Classesld
            });

            entityBuilder.ToTable("tb_examclasses");

            entityBuilder.HasComment("试卷班级关联表");

            entityBuilder.HasIndex(e => e.Classesld, "examclasses_classes_idx");

            entityBuilder.HasIndex(e => e.Examld, "examclasses_exam_idx");

            entityBuilder.Property(e => e.Classesld)
                .HasColumnName("classesld")
                .HasComment("班级ID");

            entityBuilder.Property(e => e.Examld)
                .HasColumnName("examld")
                .HasComment("试卷ID");

            entityBuilder.HasOne(d => d.ClassesldNavigation)
                .WithMany(d => d.TbExamclasses)
                .HasForeignKey(d => d.Classesld)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("examclasses_classes_idx");

            entityBuilder.HasOne(d => d.ExamldNavigation)
                .WithMany(d => d.TbExamclasses)
                .HasForeignKey(d => d.Examld)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("examclasses_exam_idx");
        }
    }
}
