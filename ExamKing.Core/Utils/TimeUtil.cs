using System;

namespace ExamKing.Core.Utils
{
    public class TimeUtil
    {
        /// <summary>
        /// 获取当前时间戳字符串
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStampNow()
        {
            var ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var createTime = Convert.ToInt64(ts.TotalSeconds).ToString();
            return createTime;
        }
        
        /// <summary>
        /// 获取时间戳字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int GetTimeStamp(DateTime dt)  
        {  
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);  
            int timeStamp = Convert.ToInt32((dt - dateStart).TotalSeconds);  
            return timeStamp;  
        } 
        
        /// <summary>
        /// 时间戳转换成Datetime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        
        
        /// <summary>
        /// 时间戳转换成Datetimes
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(int timeStamp)  
        {  
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));  
            long lTime = ((long)timeStamp * 10000000);  
            TimeSpan toNow = new TimeSpan(lTime);  
            DateTime targetDt = dtStart.Add(toNow);  
            return targetDt;  
        }  
        
    }
}