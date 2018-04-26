using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace 火车头下载小说.Help
{
    public class AddText
    {
        public void AddTextContext(string pathUrl,string contextText)
        {
            File.AppendAllText(pathUrl, contextText);
        }
    }
}