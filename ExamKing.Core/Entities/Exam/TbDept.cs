using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbDept : IEntity, IEntityTypeBuilder<TbDept>
    {
        public TbDept()
        {
            Classes = new HashSet<TbClass>();
            Courses = new HashSet<TbCourse>();
            Students = new HashSet<TbStudent>();
            Teachers = new HashSet<TbTeacher>();
        }

        public int Id { get; set; }
        public string DeptName { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset CreateTime { get; set; }

        public ICollection<TbClass> Classes { get; set; }
        public ICollection<TbCourse> Courses { get; set; }
        public ICollection<TbStudent> Students { get; set; }
        public ICollection<TbTeacher> Teachers { get; set; }

        public void Configure(EntityTypeBuilder<TbDept> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("tb_dept");

            entityBuilder.HasComment("系别表");

            entityBuilder.HasIndex(e => e.Id, "dept_id")
                .IsUnique();

            entityBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("ID");

            entityBuilder.Property(e => e.CreateTime)
                .HasColumnName("createTime")
                .HasComment("创建时间");

            entityBuilder.Property(e => e.DeptName)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("deptName")
                .HasComment("系别名称")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");
        }
    }
}
