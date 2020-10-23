using System;
using System.Collections.Generic;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbJudge : IEntity, IEntityTypeBuilder<TbJudge>
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Courseld { get; set; }
        public int Chapterld { get; set; }
        public int Teacherld { get; set; }
        public string Ideas { get; set; }
        public string CreateTime { get; set; }

        public virtual TbChapter ChapterldNavigation { get; set; }
        public virtual TbCourse CourseldNavigation { get; set; }
        public virtual TbTeacher TeacherldNavigation { get; set; }

        public void Configure(EntityTypeBuilder<TbJudge> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("tb_judge");

            entityBuilder.HasComment("是非题表");

            entityBuilder.HasIndex(e => e.Chapterld, "judge_chapter_id");

            entityBuilder.HasIndex(e => e.Id, "judge_id")
                .IsUnique();

            entityBuilder.HasIndex(e => e.Courseld, "judge_source_id");

            entityBuilder.HasIndex(e => e.Teacherld, "judge_teacher_id");

            entityBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("ID");

            entityBuilder.Property(e => e.Answer)
                .IsRequired()
                .HasColumnType("varchar(10)")
                .HasColumnName("answer")
                .HasComment("答案")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.Chapterld)
                .HasColumnName("chapterld")
                .HasComment("课程章节ID");

            entityBuilder.Property(e => e.Courseld)
                .HasColumnName("courseld")
                .HasComment("课程ID");

            entityBuilder.Property(e => e.CreateTime)
                .IsRequired()
                .HasColumnType("varchar(30)")
                .HasColumnName("createTime")
                .HasComment("创建时间")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.Ideas)
                .IsRequired()
                .HasColumnType("varchar(300)")
                .HasColumnName("ideas")
                .HasComment("解题思路")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.Question)
                .IsRequired()
                .HasColumnType("varchar(200)")
                .HasColumnName("question")
                .HasComment("题目")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.Teacherld)
                .HasColumnName("teacherld")
                .HasComment("教师ID");

            entityBuilder.HasOne(d => d.ChapterldNavigation)
                .WithMany(p => p.TbJudges)
                .HasForeignKey(d => d.Chapterld)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("judge_chapter_id");

            entityBuilder.HasOne(d => d.CourseldNavigation)
                .WithMany(p => p.TbJudges)
                .HasForeignKey(d => d.Courseld)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("judge_source_id");

            entityBuilder.HasOne(d => d.TeacherldNavigation)
                .WithMany(p => p.TbJudges)
                .HasForeignKey(d => d.Teacherld)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("judge_teacher_id");
        }
    }
}
