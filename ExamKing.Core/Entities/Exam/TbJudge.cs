using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Furion.DatabaseAccessor;
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
        public int CourseId { get; set; }
        public int ChapterId { get; set; }
        public int TeacherId { get; set; }
        public string Ideas { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset CreateTime { get; set; }

        public TbChapter Chapter { get; set; }
        public TbCourse Course { get; set; }
        public TbTeacher Teacher { get; set; }

        public void Configure(EntityTypeBuilder<TbJudge> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("tb_judge");

            entityBuilder.HasComment("是非题表");

            entityBuilder.HasIndex(e => e.ChapterId, "judge_chapter_id");

            entityBuilder.HasIndex(e => e.Id, "judge_id")
                .IsUnique();

            entityBuilder.HasIndex(e => e.CourseId, "judge_source_id");

            entityBuilder.HasIndex(e => e.TeacherId, "judge_teacher_id");

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

            entityBuilder.Property(e => e.ChapterId)
                .HasColumnName("chapterId")
                .HasComment("课程章节ID");

            entityBuilder.Property(e => e.CourseId)
                .HasColumnName("courseId")
                .HasComment("课程ID");

            entityBuilder.Property(e => e.CreateTime)
                .HasColumnName("createTime")
                .HasComment("创建时间");

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

            entityBuilder.Property(e => e.TeacherId)
                .HasColumnName("teacherId")
                .HasComment("教师ID");

            entityBuilder.HasOne(d => d.Chapter)
                .WithMany(p => p.Judges)
                .HasForeignKey(d => d.ChapterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("judge_chapter_id");

            entityBuilder.HasOne(d => d.Course)
                .WithMany(p => p.Judges)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("judge_source_id");

            entityBuilder.HasOne(d => d.Teacher)
                .WithMany(p => p.Judges)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("judge_teacher_id");
        }
    }
}
