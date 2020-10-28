using Fur.FriendlyException;

namespace ExamKing.Core.ErrorCodes
{

    /// <summary>
    /// 班级服务错误码
    /// </summary>
    [ErrorCodeType]
    public enum ClassErrorCodes
    {
        [ErrorCodeItemMetadata("系别不存在")]
        c1000
    }

    /// <summary>
    /// 学生服务错误码
    /// </summary>
    [ErrorCodeType]
    public enum StudentErrorCodes
    {
        [ErrorCodeItemMetadata("学号或密码错误")]
        s1000,
        [ErrorCodeItemMetadata("班级不存在")]
        s1001,
        [ErrorCodeItemMetadata("系别不存在")]
        s1002,
        [ErrorCodeItemMetadata("学生不存在")]
        s1003,
    }
}
