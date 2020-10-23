using System;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamKing.Core.Entities
{
    public class TbAdmin : IEntity, IEntityTypeBuilder<TbAdmin>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string CreateTime { get; set; }

        public void Configure(EntityTypeBuilder<TbAdmin> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("tb_admin");

            entityBuilder.HasComment("管理员表");

            entityBuilder.HasIndex(e => e.Id, "id")
                .IsUnique();

            entityBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("ID");

            entityBuilder.Property(e => e.Username)
                .IsRequired()
                .HasColumnType("varchar(30)")
                .HasColumnName("username")
                .HasComment("账号")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.Password)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("password")
                .HasComment("密码")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.CreateTime)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("createTime")
                .HasComment("创建时间")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.HasData(
                new { Id = 1, Username = "admin", Password = "123456", CreateTime = "1603442028" }
            );
        }
    }
}
