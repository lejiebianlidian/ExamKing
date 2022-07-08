using ExamKing.Application.Services;
using ExamKing.Core.Entites;
using ExamKing.Core.Utils;
using Furion.DataEncryption;
using Mapster;

namespace ExamKing.Application.Mappers
{
    public class AdminMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TbAdmin, AdminDto>()
                .IgnoreNullValues(true) // 忽略空值映射
                .Ignore(dest => dest.Password);

            config.ForType<AdminDto, TbAdmin>()
                .IgnoreNullValues(true) // 忽略空值映射
                .IgnoreIf((src, dest) => src.Password == "", dest => dest.Password)
                .Map(desc => desc.Password, src => MD5Encryption.Encrypt(src.Password, false));

        }
    }
}