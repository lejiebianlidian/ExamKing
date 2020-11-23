using System.Threading.Tasks;
using ExamKing.Application.Services;

namespace ExamKing.WebApp.Student
{
    /// <summary>
    /// 首页接口
    /// </summary>
    public class IndexController : ApiControllerBase
    {
        private readonly IStuanswerdetailService _stuanswerdetailService;

        /// <inheritdoc />
        public IndexController(IStuanswerdetailService stuanswerdetailService)
        {
            _stuanswerdetailService = stuanswerdetailService;
        }
        
        /// <summary>
        /// 获取全部错题数
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetWrongAnswerCount()
        {
            var studentId = GetUserId();
            return await _stuanswerdetailService.GetWrongAnswerByStudent(studentId);
        }

        /// <summary>
        /// 获取今日错题数
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetWrongAnswerTodayCount()
        {
            var studentId = GetUserId();
            return await _stuanswerdetailService.GetWrongAnswerTodayByStudent(studentId);
        }
        
        
    }
}