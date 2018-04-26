using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class GenerateId
    {
        public  static string GetGuidHash()
        {
            return Guid.NewGuid().ToString().GetHashCode().ToString("x");
        }

        /// <summary>
        /// 生成一个长整型，可以转成19字节长的字符串
        /// </summary>
        /// <returns></returns>
        public static long GenerateLong()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// 生成16个字节长度的数据与英文组合串
        /// </summary>
        /// <returns></returns>
        public static string GenerateStr()
        {
            long i = 1;
            foreach (var item in Guid.NewGuid().ToByteArray())
            {
                i *= item + 1;
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        /// <summary>
        /// 唯一订单号生成
        /// </summary>
        /// <returns></returns>
        public static string GenerateOrderNumber()
        {
            string strDateTimeNumber = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            string strRandomResult = NextRandom(1000, 1).ToString("0000");

            return strDateTimeNumber + strRandomResult;
        }

        private static int NextRandom(int numSeeds,int length)
        {
            byte[] randomNumber = new byte[length];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomNumber);
            uint randomResult = 0x0;
            for (int i = 0; i < length; i++)
            {
                randomResult |= ((uint)randomNumber[i] << ((length - 1 - i) * 8));
            }
            return (int)(randomResult % numSeeds) + 1;
        }
    }
}
