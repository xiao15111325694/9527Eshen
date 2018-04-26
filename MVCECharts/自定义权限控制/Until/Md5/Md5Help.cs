
namespace 自定义权限控制.Until.Md5
{
    public class Md5Help
    {
        public static string Md5(string str,int cod)
        {
            string strEncrypt = string.Empty;
            if(cod == 16)
            {
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").Substring(8, 16);
            } 
            if(cod == 32)
            {
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5");
            }
            return strEncrypt;
        }
    }
}