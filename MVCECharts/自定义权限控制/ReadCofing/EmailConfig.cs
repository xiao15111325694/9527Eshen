using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace 自定义权限控制.ReadCofing
{
    public class EmailConfig
    {
        /// <summary>
        /// 控制台创建的发信地址
        /// </summary>

        public static string AccountName
        {
            get
            {
                return ConfigurationManager.AppSettings["AccountName"];
            }
        }
    }
}