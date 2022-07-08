using System;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamKing.Core.Entites
{
    public class TbExamjobs : IEntity, IEntityTypeBuilder<TbExamjobs>
    {
        
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int Status { get; set; }
        
        public TbExam Exam { get; set; }
        
        public void Configure(EntityTypeBuilder<TbExamjobs> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("tb_examjobs");
            entityBuilder.HasComment("考试任务表");
            entityBuilder.HasIndex(e => e.ExamId, "examjobs_exam_id");
            entityBuilder.HasIndex(e => e.Id, "examjobs_id")
                .IsUnique();
            
            entityBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("ID");
            
            entityBuilder.Property(e => e.ExamId)
                .HasColumnName("examId")
                .HasComment("考试ID");
            
            entityBuilder.Property(e => e.Status)
                .HasColumnName("status")
                .HasComment("任务状态");
            
            entityBuilder.HasOne(d => d.Exam)
                .WithMany(p => p.Examjobses)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("examjobs_exam_id");
        }
    }
}