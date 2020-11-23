using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbStuanswerdetail : IEntity, IEntityTypeBuilder<TbStuanswerdetail>
    {
        public int Id { get; set; }
        public int StuId { get; set; }
        public int ExamId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionType { get; set; }
        public string Stuanswer { get; set; }
        public string Answer { get; set; }
        public string Isright { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset CreateTime { get; set; }

        public TbExam Exam { get; set; }
        public TbStudent Student { get; set; }
        public TbExamquestion Examquestion { get; set; }

        public void Configure(EntityTypeBuilder<TbStuanswerdetail> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {

            {
                entityBuilder.ToTable("tb_stuanswerdetail");

                entityBuilder.HasComment("答题明细表");

                entityBuilder.HasIndex(e => e.ExamId, "answerdetail_exam_id");

                entityBuilder.HasIndex(e => e.StuId, "answerdetail_stu_id");
                
                entityBuilder.HasIndex(e => e.QuestionId, "answerdetail_examquestion_id");

                entityBuilder.HasIndex(e => e.Id, "stuanseerdetail_id")
                    .IsUnique();

                entityBuilder.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("ID");

                entityBuilder.Property(e => e.Answer)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasColumnName("answer")
                    .HasComment("正确答案")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entityBuilder.Property(e => e.CreateTime)
                    .HasColumnName("createTime")
                    .HasComment("创建时间");

                entityBuilder.Property(e => e.ExamId)
                    .HasColumnName("examId")
                    .HasComment("试卷ID");

                entityBuilder.Property(e => e.Isright)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasColumnName("isright")
                    .HasComment("是否正确")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entityBuilder.Property(e => e.QuestionType)
                    .HasColumnName("questionType")
                    .HasComment("题型");

                entityBuilder.Property(e => e.QuestionId)
                    .HasColumnName("questionId")
                    .HasComment("题目ID");

                entityBuilder.Property(e => e.Stuanswer)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasColumnName("stuanswer")
                    .HasComment("学生答案")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entityBuilder.Property(e => e.StuId)
                    .HasColumnName("stuId")
                    .HasComment("学生ID");

                entityBuilder.HasOne(d => d.Exam)
                    .WithMany(p => p.Stuanswerdetails)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("answerdetail_exam_id");

                entityBuilder.HasOne(d => d.Student)
                    .WithMany(p => p.Stuanswerdetails)
                    .HasForeignKey(d => d.StuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("answerdetail_stu_id");
                
                entityBuilder.HasOne(d => d.Examquestion)
                    .WithMany(p => p.Stuanswerdetails)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("answerdetail_examquestion_id");
            }
        }
    }
}
