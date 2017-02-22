using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;
using System.Web;

namespace eT.Common
{
    public class NetworkHelper
    {
        /// <summary>  
        /// 获取远程访问用户的Ip地址  
        /// </summary>  
        /// <returns>返回Ip地址</returns>  
        public static string GetRequestIp()
        {
            string loginip = "";
            //Request.ServerVariables[""]--获取服务变量集合   
            if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null) //判断发出请求的远程主机的ip地址是否为空  
            {
                //获取发出请求的远程主机的Ip地址  
                loginip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            //判断登记用户是否使用设置代理  
            else if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    //获取代理的服务器Ip地址  
                    loginip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else
                {
                    //获取客户端IP  
                    loginip = HttpContext.Current.Request.UserHostAddress;
                }
            }
            else
            {
                //获取客户端IP  
                loginip = HttpContext.Current.Request.UserHostAddress;
            }
            return loginip;
        }

        /// <summary>
        /// 获取IP完整信息，含城市
        /// </summary>
        public static string[] GetFullIP(string ip = "")
        {
            if (string.IsNullOrEmpty(ip)) ip = GetRequestIp();
            string cityname = "中国";
            string fullIP = ip;
            string PostUrl = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=" + ip;
            string res = GetDataByPost(PostUrl);
            string[] areaArr = res.Split('\t');
            if (areaArr.Length > 5)
            {
                cityname = areaArr[5];
                if (areaArr[5] == areaArr[4]) areaArr[4] = "";
                fullIP = areaArr[4] + " " + areaArr[5] + " IP" + ip;
            }
            return new string[] { cityname, fullIP };
        }

        /// <summary>
        /// 获取购买者IP(若开启了LBS定位则以定位为准，无则以IP为准)
        /// </summary>
        public static string GetBuyIP()
        {
            /*string fullip=string.Empty;
            if (HttpContext.Current.Request.Cookies.Get("fullip") != null)
            {
                fullip = HttpContext.Current.Request.Cookies.Get("fullip").Value;
            }
            else
            {
                fullip = GetFullIP()[1];
            }
            return fullip;*/
            return GetFullIP()[1]; ;
        }

        /// <summary>
        /// 通过IP获得城市 返回数组{国家，省份，城市}
        /// </summary>
        public static string[] GetIpAddress(string ip = "")
        {
            if (string.IsNullOrEmpty(ip)) ip = GetRequestIp();
            string PostUrl = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=" + ip;
            string res = GetDataByPost(PostUrl);
            string[] address = getAreaInfoList(res);
            return address;
        }
        /// <summary>
        /// Post请求数据
        /// </summary>
        private static string GetDataByPost(string url)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                string s = "anything";
                byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(s);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = requestBytes.Length;
                Stream requestStream = req.GetRequestStream();
                requestStream.Write(requestBytes, 0, requestBytes.Length);
                requestStream.Close();
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
                string backstr = sr.ReadToEnd();
                sr.Close();
                res.Close();
                return backstr;
            }
            catch
            {
                return "中国";
            }
        }
        /// <summary>
        /// 处理所要数据
        /// </summary>
        private static string[] getAreaInfoList(string ipData)
        {
            string[] areaArr = new string[10];
            string[] newAreaArr = new string[3];
            if (string.IsNullOrEmpty(ipData)) return newAreaArr;
            try
            {
                //取所要的数据，国省市
                areaArr = ipData.Split('\t');
                newAreaArr[0] = areaArr[3];//国
                newAreaArr[1] = areaArr[4];//省
                newAreaArr[2] = areaArr[5];//市
            }
            catch (Exception e)
            {
                // TODO: handle exception
                newAreaArr[0] = "未知的IP地址";
            }
            return newAreaArr;
        }

        /* 数字IP转真实IP
        byte[] arr = BitConverter.GetBytes(16842752);//BitConverter.ToSingle
                System.Text.StringBuilder item = new System.Text.StringBuilder();
                for (int i = arr.Length - 1; i >= 0; i--)
                {
                    item.Append(arr[i].ToString() + ".");
                }
                Response.Write(item.ToString().Substring(0, item.ToString().Length - 1));
    */


        /// 在指定时间过后执行指定的表达式 
        /// </summary>
        /// <param name="interval">事件之间经过的时间（以毫秒为单位）</param>
        /// <param name="action">要执行的表达式</param>
        public static void SetTimeout(double interval, Action action)
        {
            System.Timers.Timer timer = new System.Timers.Timer(interval);
            timer.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e)
            {
                timer.Enabled = false;
                action();
            };
            timer.Enabled = true;
        }
        /// <summary> 
        /// 在指定时间周期重复执行指定的表达式 
        /// </summary> 
        /// <param name="interval">事件之间经过的时间（以毫秒为单位）</param> 
        /// <param name="action">要执行的表达式</param> 
        public static void SetInterval(double interval, Action<ElapsedEventArgs> action)
        {
            System.Timers.Timer timer = new System.Timers.Timer(interval);
            timer.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e)
             {
                 action(e);
             };
            timer.Enabled = true;
        }
        //NetworkHelper.SetTimeout(3000, delegate { Globals.DebugLogger("调试：Page_LoadB"); });//3秒后执行
        //NetworkHelper.SetInterval(1000, delegate { });//每隔1S执行

    }
}
