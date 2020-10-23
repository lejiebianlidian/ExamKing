using System;
using System.Collections.Generic;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbClass : IEntity, IEntityTypeBuilder<TbClass>
    {
        public TbClass()
        {
            TbStudents = new HashSet<TbStudent>();
        }

        public int Id { get; set; }
        public string ClassesName { get; set; }
        public int Deptld { get; set; }
        public string CreateTime { get; set; }

        public virtual TbDept DeptldNavigation { get; set; }
        public virtual ICollection<TbStudent> TbStudents { get; set; }

        public void Configure(EntityTypeBuilder<TbClass> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("tb_classes");

            entityBuilder.HasComment("班级表");

            entityBuilder.HasIndex(e => e.Deptld, "classes_dept_id");

            entityBuilder.HasIndex(e => e.Id, "classes_id")
                .IsUnique();

            entityBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("ID");

            entityBuilder.Property(e => e.ClassesName)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("classesName")
                .HasComment("班级名称")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.CreateTime)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("createTime")
                .HasComment("创建时间")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.Deptld)
                .HasColumnName("deptld")
                .HasComment("系别ID");

            entityBuilder.HasOne(d => d.DeptldNavigation)
                .WithMany(p => p.TbClasses)
                .HasForeignKey(d => d.Deptld)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("classes_dept_id");
        }
    }
}
