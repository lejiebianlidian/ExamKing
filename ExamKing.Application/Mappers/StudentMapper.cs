using Fur.ObjectMapper;
using Mapster;
using ExamKing.Core.Entites;

namespace ExamKing.Application.Mappers
{
    public class StudentMapper : IObjectMapper
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TbStudent, StudentDto>()
                .IgnoreNullValues(true) // 忽略空值映射
                .Ignore(dest => dest.Password);

            // studentDto.Adapt<TbStudent>();
            config.ForType<StudentDto, TbStudent>()
                .IgnoreNullValues(true) // 忽略空值映射
                .IgnoreIf((src, dest) => src.ClassesId <= 0, dest => dest.ClassesId)
                .IgnoreIf((src, dest) => src.DeptId <= 0, dest => dest.DeptId)
                .IgnoreIf((src, dest) => src.StuNo=="", dest => dest.StuNo)
                .IgnoreIf((src, dest) => src.Password=="", dest => dest.Password);
        }
    }
}
