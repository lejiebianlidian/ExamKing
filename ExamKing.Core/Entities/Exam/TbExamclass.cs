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
        public int ExamId { get; set; }
        public int ClassesId { get; set; }

        public TbClass Classes { get; set; }
        public TbExam Exam { get; set; }

        public void Configure(EntityTypeBuilder<TbExamclass> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasKey(d => new
            {
                d.ExamId,
                d.ClassesId
            });

            entityBuilder.ToTable("tb_examclasses");

            entityBuilder.HasComment("试卷班级关联表");

            entityBuilder.HasIndex(e => e.ClassesId, "examclasses_classes_idx");

            entityBuilder.HasIndex(e => e.ExamId, "examclasses_exam_idx");

            entityBuilder.Property(e => e.ClassesId)
                .HasColumnName("classesId")
                .HasComment("班级ID");

            entityBuilder.Property(e => e.ExamId)
                .HasColumnName("examId")
                .HasComment("试卷ID");

            entityBuilder.HasOne(d => d.Classes)
                .WithMany(d => d.TbExamclasses)
                .HasForeignKey(d => d.ClassesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("examclasses_classes_idx");

            entityBuilder.HasOne(d => d.Exam)
                .WithMany(d => d.TbExamclasses)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("examclasses_exam_idx");
        }
    }
}
