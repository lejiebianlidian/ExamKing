using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ExamKing.Core.Utils;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbAdmin : IEntity, IEntitySeedData<TbAdmin>, IEntityTypeBuilder<TbAdmin>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset CreateTime { get; set; }

        public void Configure(EntityTypeBuilder<TbAdmin> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("tb_admin");

            entityBuilder.HasComment("管理员表");

            entityBuilder.HasIndex(e => e.Id, "id")
                .IsUnique();

            entityBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("ID");

            entityBuilder.Property(e => e.CreateTime)
                .HasColumnName("createTime")
                .HasComment("创建时间");

            entityBuilder.Property(e => e.Password)
                .HasColumnType("varchar(50)")
                .HasColumnName("password")
                .HasComment("密码")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");

            entityBuilder.Property(e => e.Username)
                .IsRequired()
                .HasColumnType("varchar(30)")
                .HasColumnName("username")
                .HasComment("账号")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");
        }

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<TbAdmin> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]
            {
                new TbAdmin
                {
                    Id=1,Username="admin",Password="123456",CreateTime = DateTimeOffset.Parse("2020-11-18 21:15:38").ToLocalTime()
                }
            };
        }
    }
}
