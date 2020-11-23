using System.Threading.Tasks;

namespace ExamKing.Application.Services
{
    /// <summary>
    /// 答题服务接口
    /// </summary>
    public interface IStuanswerdetailService
    {
        /// <summary>
        /// 获取学生全部错题数量
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Task<int> GetWrongAnswerByStudent(int studentId);

        /// <summary>
        /// 获取学生今日错题数
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Task<int> GetWrongAnswerTodayByStudent(int studentId);
    }
}