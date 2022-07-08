using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbSelect : IEntity, IEntityTypeBuilder<TbSelect>
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string IsSingle { get; set; }
        public int CourseId { get; set; }
        public int ChapterId { get; set; }
        public int TeacherId { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string Ideas { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset CreateTime { get; set; }

        public TbChapter Chapter { get; set; }
        public TbCourse Course { get; set; }
        public TbTeacher Teacher { get; set; }

        public void Configure(EntityTypeBuilder<TbSelect> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {

            {
                entityBuilder.ToTable("tb_select");

                entityBuilder.HasComment("选择题表");

                entityBuilder.HasIndex(e => e.ChapterId, "select_chapter_id");

                entityBuilder.HasIndex(e => e.Id, "select_id")
                    .IsUnique();

                entityBuilder.HasIndex(e => e.CourseId, "select_source_id");

                entityBuilder.HasIndex(e => e.TeacherId, "select_teacher_id");

                entityBuilder.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("ID");

                entityBuilder.Property(e => e.Answer)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
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

                entityBuilder.Property(e => e.IsSingle)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasColumnName("isSingle")
                    .HasComment("是否单选")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entityBuilder.Property(e => e.OptionA)
                    .IsRequired()
                    .HasColumnType("varchar(300)")
                    .HasColumnName("optionA")
                    .HasComment("选项A")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entityBuilder.Property(e => e.OptionB)
                    .IsRequired()
                    .HasColumnType("varchar(300)")
                    .HasColumnName("optionB")
                    .HasComment("选项B")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entityBuilder.Property(e => e.OptionC)
                    .IsRequired()
                    .HasColumnType("varchar(300)")
                    .HasColumnName("optionC")
                    .HasComment("选项C")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entityBuilder.Property(e => e.OptionD)
                    .IsRequired()
                    .HasColumnType("varchar(300)")
                    .HasColumnName("optionD")
                    .HasComment("选项D")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entityBuilder.Property(e => e.Question)
                    .IsRequired()
                    .HasColumnType("varchar(300)")
                    .HasColumnName("question")
                    .HasComment("问题")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entityBuilder.Property(e => e.TeacherId)
                    .HasColumnName("teacherId")
                    .HasComment("教师ID");

                entityBuilder.HasOne(d => d.Chapter)
                    .WithMany(p => p.Selects)
                    .HasForeignKey(d => d.ChapterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("select_chapter_id");

                entityBuilder.HasOne(d => d.Course)
                    .WithMany(p => p.Selects)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("select_source_id");

                entityBuilder.HasOne(d => d.Teacher)
                    .WithMany(p => p.Selects)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("select_teacher_id");
            }
        }
    }
}
