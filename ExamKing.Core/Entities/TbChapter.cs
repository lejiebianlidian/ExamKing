﻿using System;
using System.Collections.Generic;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbChapter : IEntity, IEntityTypeBuilder<TbChapter>
    {
        public TbChapter()
        {
            TbJudges = new HashSet<TbJudge>();
            TbSelects = new HashSet<TbSelect>();
        }

        public int Id { get; set; }
        public string ChapterName { get; set; }
        public int Courseld { get; set; }
        public string Desc { get; set; }

        public virtual TbCourse CourseldNavigation { get; set; }
        public virtual ICollection<TbJudge> TbJudges { get; set; }
        public virtual ICollection<TbSelect> TbSelects { get; set; }

        public void Configure(EntityTypeBuilder<TbChapter> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("tb_chapter");

            entityBuilder.HasComment("课程章节表");

            entityBuilder.HasIndex(e => e.Courseld, "chapter_course_id");

            entityBuilder.HasIndex(e => e.Id, "chapter_id")
                .IsUnique();

            entityBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("ID");

            entityBuilder.Property(e => e.ChapterName)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasColumnName("chapterName")
                .HasComment("章节名称")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.Courseld)
                .HasColumnName("courseld")
                .HasComment("课程ID");

            entityBuilder.Property(e => e.Desc)
                .IsRequired()
                .HasColumnType("varchar(200)")
                .HasColumnName("desc")
                .HasComment("章节描述")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.HasOne(d => d.CourseldNavigation)
                .WithMany(p => p.TbChapters)
                .HasForeignKey(d => d.Courseld)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("chapter_course_id");
        }
    }
}
