using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace Infrastructure
{
    public class CommonHelper
    {
        /// <summary>
        /// 计时开始
        /// </summary>
        /// <returns></returns>
        public static Stopwatch TimeStart()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Reset();
            stopWatch.Start();
            return stopWatch;
        } 

        /// <summary>
        /// 计时结束
        /// </summary>
        /// <param name="watch"></param>
        /// <returns></returns>
        public static string TimeEnd(Stopwatch watch)
        {
            watch.Stop();
            double costtime = watch.ElapsedMilliseconds;
            return costtime.ToString();
        }

        /// <summary>
        /// 数组去重
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string[] RemoveDup(string[] values)
        {
            List<string> list = new List<string>();
            foreach (var item in values)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string CreateNumber()
        {
            Random random = new Random();
            string strRandom = random.Next(1000, 10000).ToString();
            string negcod = Guid.NewGuid().ToString().GetHashCode().ToString("x");
            string codRandom = DateTime.Now.ToString("yyyyMMddHHmmss") + strRandom + negcod;
            return codRandom;
        }
    }
}
