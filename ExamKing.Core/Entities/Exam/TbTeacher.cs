using System;
using System.Collections.Generic;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbTeacher : IEntity, IEntityTypeBuilder<TbTeacher>
    {
        public TbTeacher()
        {
            TbCourses = new HashSet<TbCourse>();
            TbExams = new HashSet<TbExam>();
            TbJudges = new HashSet<TbJudge>();
            TbSelects = new HashSet<TbSelect>();
        }

        public int Id { get; set; }
        public string TeacherName { get; set; }
        public string Sex { get; set; }
        public string Telphone { get; set; }
        public string TeacherNo { get; set; }
        public string Password { get; set; }
        public int DeptId { get; set; }
        public string IdCard { get; set; }
        public string CreateTime { get; set; }

        public virtual TbDept DeptIdNavigation { get; set; }
        public virtual ICollection<TbCourse> TbCourses { get; set; }
        public virtual ICollection<TbExam> TbExams { get; set; }
        public virtual ICollection<TbJudge> TbJudges { get; set; }
        public virtual ICollection<TbSelect> TbSelects { get; set; }

        public void Configure(EntityTypeBuilder<TbTeacher> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {

            {
                entityBuilder.ToTable("tb_teacher");

                entityBuilder.HasComment("教师表");

                entityBuilder.HasIndex(e => e.DeptId, "teacher_dept_id");

                entityBuilder.HasIndex(e => e.Id, "teacher_id")
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

                entityBuilder.Property(e => e.DeptId)
                    .HasColumnName("DeptId")
                    .HasComment("系别ID");

                entityBuilder.Property(e => e.IdCard)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasColumnName("idCard")
                    .HasComment("身份证号")
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

                entityBuilder.Property(e => e.TeacherName)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("teacherName")
                    .HasComment("姓名")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entityBuilder.Property(e => e.TeacherNo)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasColumnName("teacherNo")
                    .HasComment("教师编号")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entityBuilder.Property(e => e.Telphone)
                    .IsRequired()
                    .HasColumnType("varchar(25)")
                    .HasColumnName("telphone")
                    .HasComment("联系电话")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entityBuilder.HasOne(d => d.DeptIdNavigation)
                    .WithMany(p => p.TbTeachers)
                    .HasForeignKey(d => d.DeptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("teacher_dept_id");
            }
        }
    }
}
