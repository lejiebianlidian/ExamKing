using System;
using System.Collections.Generic;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbDept : IEntity, IEntityTypeBuilder<TbDept>
    {
        public TbDept()
        {
            TbClasses = new HashSet<TbClass>();
            TbCourses = new HashSet<TbCourse>();
            TbStudents = new HashSet<TbStudent>();
            TbTeachers = new HashSet<TbTeacher>();
        }

        public int Id { get; set; }
        public string DeptName { get; set; }
        public string CreateTime { get; set; }

        public ICollection<TbClass> TbClasses { get; set; }
        public ICollection<TbCourse> TbCourses { get; set; }
        public ICollection<TbStudent> TbStudents { get; set; }
        public ICollection<TbTeacher> TbTeachers { get; set; }

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
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("createTime")
                .HasComment("创建时间")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

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
