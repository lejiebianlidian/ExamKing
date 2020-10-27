using System;
using System.Collections.Generic;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace ExamKing.Core.Entites
{
    public partial class TbQuestiontype : IEntity, IEntityTypeBuilder<TbQuestiontype>
    {
        public TbQuestiontype()
        {
            TbStuanswerdetails = new HashSet<TbStuanswerdetail>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }
        public string CreateTime { get; set; }

        public virtual ICollection<TbStuanswerdetail> TbStuanswerdetails { get; set; }

        public void Configure(EntityTypeBuilder<TbQuestiontype> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("tb_questiontype");

            entityBuilder.HasComment("题型实体");

            entityBuilder.HasIndex(e => e.Id, "questiontype_id")
                .IsUnique();

            entityBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("ID");

            entityBuilder.Property(e => e.CreateTime)
                .IsRequired()
                .HasColumnType("varchar(30)")
                .HasColumnName("createTime")
                .HasComment("创建时间")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entityBuilder.Property(e => e.TypeName)
                .IsRequired()
                .HasColumnType("varchar(30)")
                .HasColumnName("typeName")
                .HasComment("题型名称")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");
        }
    }
}
