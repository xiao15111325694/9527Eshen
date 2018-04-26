using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Text;
using 自定义权限控制.ReadCofing;

namespace 自定义权限控制.Manager
{
    public class EmailManager
    {
        public void SendEmail(string to, string subject, string content, string name, string password)
        {
            //构造邮件
            var mail = new MailMessage();
            mail.From = new MailAddress(EmailConfig.AccountName, name.Trim(), Encoding.UTF8);
            mail.To.Add(new MailAddress(to.Trim()));
            mail.Subject = subject;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Body = content;
            mail.BodyEncoding= Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;

            //构造
            var client = new SmtpClient();
            client.UseDefaultCredentials = true; // 在最终发送成功的代码中，本属性必须在 Credentials 之前赋值
            client.Credentials =  new NetworkCredential(EmailConfig.AccountName, password); // 本属性必须在 UseDefaultCredentials 之后赋值
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Host = "smtp.9527.com"; //"SMTP 服务器 IP 或 域名";
            client.Port = 25;
            client.EnableSsl = true;
            client.SendCompleted += SMTPSendCompleted;   // 邮件发送完毕的回调方法

            try
            {
                client.SendAsync(mail, Guid.NewGuid());
            }
            catch (Exception e)
            {

            }

        }


        private void SMTPSendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var result = "";
            if (e.Cancelled)
            {
                result = "已取消发送邮件";
            }
            else if (e.Error != null)
            {
                result = "失败：" + e.UserState.ToString() + e.Error.Message;
            }
            else
            {
                result = "邮件发送成功";
            }

        }

    }
}