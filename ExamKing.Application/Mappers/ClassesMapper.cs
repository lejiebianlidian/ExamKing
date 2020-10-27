using Fur.ObjectMapper;
using Mapster;
using ExamKing.Core.Entites;
using ExamKing.Application.Api.Student;

namespace ExamKing.Application.Mappers
{
    public class ClassesMapper : IObjectMapper
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TbClass, ClassesDto>()
                .Map(dest => dest.Dept, src => src.DeptldNavigation);

        }
    }
}
