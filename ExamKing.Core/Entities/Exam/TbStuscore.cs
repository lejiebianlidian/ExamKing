using System;
using System.Collections.Generic;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbStuscore : IEntity, IEntityTypeBuilder<TbStuscore>
    {
        public uint Id { get; set; }
        public int StuId { get; set; }
        public int CourseId { get; set; }
        public int ExamId { get; set; }
        public int Score { get; set; }
        public string CreateTime { get; set; }

        public TbCourse Course { get; set; }
        public TbExam Exam { get; set; }
        public TbStudent Student { get; set; }

        public void Configure(EntityTypeBuilder<TbStuscore> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {

            {
                entityBuilder.ToTable("tb_stuscore");

                entityBuilder.HasComment("学生成绩表");

                entityBuilder.HasIndex(e => e.CourseId, "stuscore_course_id");

                entityBuilder.HasIndex(e => e.ExamId, "stuscore_exam_id");

                entityBuilder.HasIndex(e => e.Id, "stuscore_id")
                    .IsUnique();

                entityBuilder.HasIndex(e => e.StuId, "stuscore_stu_id");

                entityBuilder.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("ID");

                entityBuilder.Property(e => e.CourseId)
                    .HasColumnName("courseId")
                    .HasComment("课程ID");

                entityBuilder.Property(e => e.CreateTime)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasColumnName("createTime")
                    .HasComment("创建时间")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entityBuilder.Property(e => e.ExamId)
                    .HasColumnName("examId")
                    .HasComment("考试ID");

                entityBuilder.Property(e => e.Score)
                    .HasColumnName("score")
                    .HasComment("分数");

                entityBuilder.Property(e => e.StuId)
                    .HasColumnName("stuId")
                    .HasComment("学生ID");

                entityBuilder.HasOne(d => d.Course)
                    .WithMany(p => p.Stuscores)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("stuscore_course_id");

                entityBuilder.HasOne(d => d.Exam)
                    .WithMany(p => p.Stuscores)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("stuscore_exam_id");

                entityBuilder.HasOne(d => d.Student)
                    .WithMany(p => p.Stuscores)
                    .HasForeignKey(d => d.StuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("stuscore_stu_id");
            }
        }
    }
}
