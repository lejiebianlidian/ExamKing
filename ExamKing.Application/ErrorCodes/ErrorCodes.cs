using Furion.FriendlyException;

namespace ExamKing.Application.ErrorCodes
{
    /// <summary>
    /// 管理员错误码
    /// </summary>
    [ErrorCodeType]
    public enum AdminErrorCodes
    {
        [ErrorCodeItemMetadata("账号或者密码错误")]
        a1001,
        [ErrorCodeItemMetadata("管理员不存在")]
        a1002,
        [ErrorCodeItemMetadata("管理员已存在")]
        a1003
    }
    
    /// <summary>
    /// 班级错误码
    /// </summary>
    [ErrorCodeType]
    public enum ClassErrorCodes
    {
        [ErrorCodeItemMetadata("班级不存在")]
        c1101
    }

    /// <summary>
    /// 学生错误码
    /// </summary>
    [ErrorCodeType]
    public enum StudentErrorCodes
    {
        [ErrorCodeItemMetadata("学号或密码错误")]
        s1201,
        [ErrorCodeItemMetadata("班级不存在")]
        s1202,
        [ErrorCodeItemMetadata("系别不存在")]
        s1203,
        [ErrorCodeItemMetadata("学生不存在")]
        s1204,
        [ErrorCodeItemMetadata("学号已存在")]
        s1205,
        [ErrorCodeItemMetadata("身份证后六位不正确")]
        s1206,
    }
    
    /// <summary>
    /// 系别错误码
    /// </summary>
    [ErrorCodeType]
    public enum DeptErrorCodes
    {
        [ErrorCodeItemMetadata("系别不存在")]
        d1301
    }
    
    /// <summary>
    /// 教师错误码
    /// </summary>
    [ErrorCodeType]
    public enum TeacherErrorCodes
    {
        [ErrorCodeItemMetadata("教师已存在")]
        t1401,
        [ErrorCodeItemMetadata("教师不存在")]
        t1402,
        [ErrorCodeItemMetadata("工号或者密码错误")]
        t1403
    }
    
    /// <summary>
    /// 课程错误码
    /// </summary>
    [ErrorCodeType]
    public enum CourseErrorCodes
    {
        [ErrorCodeItemMetadata("课程不存在")]
        c1501
    }
    
    /// <summary>
    /// 章节错误码
    /// </summary>
    [ErrorCodeType]
    public enum ChapterErrorCodes
    {
        [ErrorCodeItemMetadata("章节不存在")]
        z1601
    }
    
    /// <summary>
    /// 选择题错误码
    /// </summary>
    [ErrorCodeType]
    public enum SelectErrorCodes
    {
        [ErrorCodeItemMetadata("选择题不存在")]
        x1701
    }
    
    /// <summary>
    /// 是非题错误码
    /// </summary>
    [ErrorCodeType]
    public enum JudgeErrorCodes
    {
        [ErrorCodeItemMetadata("是非题不存在")]
        s1801
    }
    
    /// <summary>
    /// 试卷错误码
    /// </summary>
    [ErrorCodeType]
    public enum ExamErrorCodes
    {
        [ErrorCodeItemMetadata("试卷不存在")]
        s1901
    }
    
    /// <summary>
    /// 考试成绩错误码
    /// </summary>
    [ErrorCodeType]
    public enum ExamScoreErrorCodes
    {
        [ErrorCodeItemMetadata("成绩不存在")]
        k2001,
        [ErrorCodeItemMetadata("成绩已存在")]
        k2002
    }
    
    /// <summary>
    /// 答题错误码
    /// </summary>
    [ErrorCodeType]
    public enum ExamAnswerScoreErrorCodes
    {
        [ErrorCodeItemMetadata("题目不存在")]
        d2101,
        [ErrorCodeItemMetadata("请不要重复答题")]
        d2102
    }
}
