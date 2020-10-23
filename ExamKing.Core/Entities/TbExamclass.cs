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

        public void Configure(EntityTypeBuilder<TbExamclass> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasNoKey();

            entityBuilder.ToTable("tb_examclasses");

            entityBuilder.HasComment("试卷班级关联表");

            entityBuilder.Property(e => e.Classesld)
                .HasColumnName("classesld")
                .HasComment("班级ID");

            entityBuilder.Property(e => e.Examld)
                .HasColumnName("examld")
                .HasComment("试卷ID");
        }
    }
}
