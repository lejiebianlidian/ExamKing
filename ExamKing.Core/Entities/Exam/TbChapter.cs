using System;
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
            Judges = new HashSet<TbJudge>();
            Selects = new HashSet<TbSelect>();
        }

        public int Id { get; set; }
        public string ChapterName { get; set; }
        public int CourseId { get; set; }
        public string Desc { get; set; }

        public TbCourse Course { get; set; }
        public ICollection<TbJudge> Judges { get; set; }
        public ICollection<TbSelect> Selects { get; set; }

        public void Configure(EntityTypeBuilder<TbChapter> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("tb_chapter");

            entityBuilder.HasComment("课程章节表");

            entityBuilder.HasIndex(e => e.CourseId, "chapter_course_id");

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

            entityBuilder.Property(e => e.CourseId)
                .HasColumnName("courseId")
                .HasComment("课程ID");

            entityBuilder.Property(e => e.Desc)
                .IsRequired()
                .HasColumnType("varchar(200)")
                .HasColumnName("desc")
                .HasComment("章节描述")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.HasOne(d => d.Course)
                .WithMany(p => p.Chapters)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("chapter_course_id");
        }
    }
}
