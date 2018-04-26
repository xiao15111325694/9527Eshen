using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class MileHelp
    {
        /// <summary>
        /// 邮件服务器地址
        /// </summary>
        public string MailServer { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string MailUserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string MailPassword { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string MailName { get; set; }

        public void SendByThread(string to,string title,string body,int port)
        {
              new Thread(new ThreadStart(delegate() {
                  try
                  {
                      SmtpClient smtp = new SmtpClient();
                      //邮箱的smtp地址
                      smtp.Host = MailServer;
                      //端口号
                      smtp.Port = port;
                      //构建发件人的身份凭据类
                      smtp.Credentials = new NetworkCredential()
                  }
                  catch (Exception)
                  {

                      throw;
                  }
              }))
        }

    }
}
