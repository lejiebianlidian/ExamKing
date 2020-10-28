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
                .Map(dest => dest.Classes, src => src.Classes)
                .Map(dest => dest.Dept, src => src.Dept);
        }
    }
}
