using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShuaPiaoMachine
{
    class Shua
    {
        
        /// <summary>
        /// 获得post请求后响应的数据
        /// </summary>
        /// <param name="postUrl">请求地址</param>
        /// <param name="referUrl">请求引用地址</param>
        /// <param name="data">请求带的数据</param>
        /// <returns>响应内容</returns>
        public static string PostLogin()
        {
            string data = "optionId=44&openId=" + Guid.NewGuid().ToString().Substring(0, 22);
            string result = "";
            try
            {
                //命名空间System.Net下的HttpWebRequest类
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://ec.dashunengyun.com/wechat/wx/vote/vote");
                //参照浏览器的请求报文 封装需要的参数 这里参照ie9
                //浏览器可接受的MIME类型
                request.Accept = "*/*";
                //包含一个URL，用户从该URL代表的页面出发访问当前请求的页面
                request.Referer = "http://ec.dashunengyun.com/wechat2/?from=timeline";
                //浏览器类型，如果Servlet返回的内容与浏览器类型有关则该值非常有用
                request.UserAgent = "User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.117 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded";
                //请求方式
                request.Method = "POST";
                //是否保持常连接
                request.KeepAlive = true;
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
                request.Headers.Add("Origin", "http://ec.dashunengyun.com");
                request.Host = "ec.dashunengyun.com";
                //表示请求消息正文的长度
                request.ContentLength = data.Length;
                Stream postStream = request.GetRequestStream();
                byte[] postData = Encoding.UTF8.GetBytes(data);
                //将传输的数据，请求正文写入请求流
                postStream.Write(postData, 0, postData.Length);
                postStream.Dispose();
                //响应
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var jsonStr = streamReader.ReadToEnd();
                    JObject jo = (JObject)JsonConvert.DeserializeObject(jsonStr);
                    var re = jo["code"].ToString();
                    result = "0".Equals(re) ? "yes" : "发生错误，请重启！";
                }
                return result;
            }
            catch (Exception)
            {
                return "发生错误，请重启！";

            }
        }
    }
}
