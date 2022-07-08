using System;
using Furion.DependencyInjection;

namespace ExamKing.Core.Utils
{
    [SuppressSniffer]
    public class TimeUtil
    {
        /// <summary>
        /// 获取当前时间戳字符串
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStampNow()
        {
            // var ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            // var createTime = Convert.ToInt64(ts.TotalSeconds).ToString();
            var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            return timestamp;
        }
        
        /// <summary>
        /// 时间戳转换成Datetime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(string timeStamp)
        {
            timeStamp ??= "0";
            var UninTimeStamp = long.Parse(timeStamp);
            var DateTimeUnix = DateTimeOffset.FromUnixTimeSeconds(UninTimeStamp);
            var dateTime = DateTimeUnix.DateTime;
            return dateTime.ToLocalTime();
        }
        
        
        /// <summary>
        /// 时间戳转换成Datetimes
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(int timeStamp)
        {
            if (timeStamp == null) timeStamp = 0;
            var UninTimeStamp = (long) timeStamp;
            var DateTimeUnix = DateTimeOffset.FromUnixTimeSeconds(UninTimeStamp);
            var dateTime = DateTimeUnix.DateTime;
            return dateTime.ToLocalTime();
        }  
        
    }
}