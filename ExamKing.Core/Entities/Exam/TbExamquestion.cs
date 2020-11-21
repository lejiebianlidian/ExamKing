using System;
using System.Collections.Generic;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbExamquestion : IEntity, IEntityTypeBuilder<TbExamquestion>
    {
        public TbExamquestion()
        {
            Stuanswerdetails = new HashSet<TbStuanswerdetail>();
        }

        public int Id { get; set; }
        public string QuestionType { get; set; }
        public int ExamId { get; set; }
        public int QuestionId { get; set; }
        public int Score { get; set; }
        
        public TbExam Exam { get; set; }
        
        public ICollection<TbStuanswerdetail> Stuanswerdetails { get; set; }

        
        public void Configure(EntityTypeBuilder<TbExamquestion> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("tb_examquestion");

            entityBuilder.HasComment("试卷题目关联表");

            entityBuilder.HasIndex(e => e.Id, "examquestion_id")
                .IsUnique();

            entityBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("ID");
            
            entityBuilder.Property(e => e.QuestionType)
                .IsRequired()
                .HasColumnName("questionType")
                .HasComment("题型")
                .HasColumnType("varchar(30)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");
            
            entityBuilder.Property(e => e.ExamId)
                .HasColumnName("examId")
                .HasComment("试卷ID");
            
            entityBuilder.Property(e => e.QuestionId)
                .HasColumnName("questionId")
                .HasComment("题目ID");
            
            entityBuilder.Property(e => e.Score)
                .HasColumnName("score")
                .HasComment("分数");
            
            entityBuilder.HasOne(d => d.Exam)
                .WithMany(p => p.Examquestions)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("examquestion_exam_id");

        }
    }
}
