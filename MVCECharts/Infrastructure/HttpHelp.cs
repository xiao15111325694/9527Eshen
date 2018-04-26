using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class HttpHelp
    {
        private HttpClient _httpClient;

        /// <summary>
        /// Get请求数据
        ///   /// <para>最终以url参数的方式提交</para>
        /// <para>yubaolee 2016-3-3 重构与post同样异步调用</para>
        /// </summary>
        /// <param name="parameters">参数字典,可为空</param>
        /// <param name="requestUri">例如/api/Files/UploadFile</param>
        /// <returns></returns>
        public string Get(Dictionary<string, string> parameters, string requestUrl)
        {
            if (parameters != null)
            {
                var strParam = string.Join("&", parameters.Select(x => x.Key + "=" + x.Value));
                requestUrl = string.Concat(ConcatURL(requestUrl), '?', strParam);
            }
            else
            {
                requestUrl = ConcatURL(requestUrl);
            }
            var result = _httpClient.GetStringAsync(requestUrl);
            return result.Result;
        }

        /// <summary>
        /// 以json的方式Post数据 返回string类型
        /// <para>最终以json的方式放置在http体中</para>
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="requestUri">例如/api/Files/UploadFile</param>
        /// <returns></returns>
        public string Post(object entity,string requstUrl)
        {
            string request = string.Empty;
            if(request!=null)
                request = JsonConvert.SerializeObject(entity, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            HttpContent httpContext = new StringContent(request);
            httpContext.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return Post(requstUrl, httpContext);
        }

        private string Post(string requestUrl, HttpContent content)
        {
            var result = _httpClient.PostAsync(ConcatURL(requestUrl), content);
            return result.Result.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        ///  把请求的URL相对路径组合成绝对路径
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public string ConcatURL(string requestUrl)
        {
            return new Uri(_httpClient.BaseAddress, requestUrl).OriginalString;
        }
    }
}
