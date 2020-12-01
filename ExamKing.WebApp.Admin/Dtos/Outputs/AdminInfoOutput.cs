using System;

namespace ExamKing.WebApp.Admin
{
    /// <summary>
    /// 管理员信息
    /// </summary>
    public class AdminInfoOutput
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreateTime { get; set; }
    }
}