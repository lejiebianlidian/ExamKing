using System;
using System.Collections.Generic;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbStudent : IEntity, IEntityTypeBuilder<TbStudent>
    {
        public TbStudent()
        {
            TbStuanswerdetails = new HashSet<TbStuanswerdetail>();
            TbStuscores = new HashSet<TbStuscore>();
        }

        public int Id { get; set; }
        public string StuName { get; set; }
        public int DeptId { get; set; }
        public int ClassesId { get; set; }
        public string Sex { get; set; }
        public string StuNo { get; set; }
        public string Password { get; set; }
        public string Telphone { get; set; }
        public string IdCard { get; set; }
        public string CreateTime { get; set; }

        public virtual TbClass Classes { get; set; }
        public virtual TbDept Dept { get; set; }
        public virtual ICollection<TbStuanswerdetail> TbStuanswerdetails { get; set; }
        public virtual ICollection<TbStuscore> TbStuscores { get; set; }

        public void Configure(EntityTypeBuilder<TbStudent> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("tb_student");

            entityBuilder.HasComment("学生表");

            entityBuilder.HasIndex(e => e.ClassesId, "student_classes_id");

            entityBuilder.HasIndex(e => e.DeptId, "student_dept_id");

            entityBuilder.HasIndex(e => e.Id, "student_id")
                .IsUnique();

            entityBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("ID");

            entityBuilder.Property(e => e.ClassesId)
                .HasColumnName("classesId")
                .HasComment("班级ID");

            entityBuilder.Property(e => e.CreateTime)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("createTime")
                .HasComment("创建时间")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.DeptId)
                .HasColumnName("deptId")
                .HasComment("系别ID");

            entityBuilder.Property(e => e.IdCard)
                .IsRequired()
                .HasColumnType("varchar(30)")
                .HasColumnName("idCard")
                .HasComment("身份证号码")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.Password)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("password")
                .HasComment("密码")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.Sex)
                .IsRequired()
                .HasColumnType("varchar(10)")
                .HasColumnName("sex")
                .HasComment("性别")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.StuName)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("stuName")
                .HasComment("姓名")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.StuNo)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("stuNo")
                .HasComment("学号")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.Telphone)
                .IsRequired()
                .HasColumnType("varchar(30)")
                .HasColumnName("telphone")
                .HasComment("联系电话")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.HasOne(d => d.Classes)
                .WithMany(p => p.TbStudents)
                .HasForeignKey(d => d.ClassesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_classes_id");

            entityBuilder.HasOne(d => d.Dept)
                .WithMany(p => p.TbStudents)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_dept_id");
        }
    }
}
