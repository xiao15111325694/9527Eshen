using MVCECharts.Models;
using System.Text;

namespace MVCEchartsManager.Business
{
    public class BLLSendMasage
    {
        public void SendMasageLog()
        {
            string s = "系统";
            string k = "消息";
            string x = "客户";
            StringBuilder builder= new StringBuilder();
            builder.AppendFormat("{0}发送{1}信息通知{2}", s, k, x);
        }
    }
}