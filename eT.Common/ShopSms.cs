using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using eT.Model;
using Utility;

namespace eT.Common
{
    /// <summary>
    /// 短信相关通用处理类
    /// </summary>
    public class ShopSms
    {
        /// <summary>
        /// 发送短信时间间隔(秒)
        /// </summary>
        public static int Time_Difference_Span = 120;
        /// <summary>
        /// 短信有效期(秒)
        /// </summary>
        public static int Time_Difference_Val = 600;

        /// <summary>
        /// 获取时间间隔(秒)
        /// </summary>
        public static long Get_Time_Difference(DateTime old, bool isMin = true)
        {
            DateTime dt = DateTime.Now;
            //当前时间转换为秒
            long now = dt.Ticks / 10000000;
            //上次发送时间转换为秒
            long Telcodt = old.Ticks / 10000000;
            if (isMin)
                return now - Telcodt;
            else
                return Telcodt - now;
        }


        /// <summary>
        /// 获取时间间隔(秒)
        /// </summary>
        public static double Get_second_Difference(DateTime old, bool isMin = true)
        {
          System.TimeSpan timeSpan =   old - DateTime.Now;

          return timeSpan.TotalSeconds;
        }



        /// <summary>
        /// 发送验证码类型
        /// </summary>
        public enum SendSmsType
        {
            /// <summary>
            /// 注册
            /// </summary>
            Reg,
            /// <summary>
            /// 找回密码
            /// </summary>
            FindPwd,
            /// <summary>
            /// 中奖
            /// </summary>
            Win
        }

        /// <summary>
        /// 发送手机验证码
        /// </summary>
        public static int APISendSms(string mobile,string code, SendSmsType sendSmsType = SendSmsType.Reg)
        {
            string msgs = string.Empty;
            switch (sendSmsType)
            {
                case SendSmsType.Reg:
                    msgs = "你在" + Globals.WebSiteName + "的短信验证码是:" + code;
                    break;
                case SendSmsType.FindPwd:
                    msgs = "你正在" + Globals.WebSiteName + "的找回密码的短信验证码是:" + code;
                    break;
                case SendSmsType.Win:
                    msgs = "你在" + Globals.WebSiteName + "购买的商品已中奖,中奖码是:" + code;
                    break;
            }
            if (!string.IsNullOrEmpty(msgs))
                Send(mobile, msgs);
            return 0;
        }

        /// <summary>
        /// 发送手机验证码
        /// </summary>
        private static int Send(string mobile, string content)
        {
            string url = "http://120.26.244.194:8888/sms.aspx?action=send";
            string para = string.Empty;
            para += string.Format("&userid={0}", HttpContext.Current.Server.UrlEncode("3041"));
            para += string.Format("&account={0}", HttpContext.Current.Server.UrlEncode("jhblr9"));
            para += string.Format("&password={0}", HttpContext.Current.Server.UrlEncode("yihuikeji888"));
            para += string.Format("&content={0}", HttpContext.Current.Server.UrlEncode("【易惠科技】" + content));
            para += string.Format("&mobile={0}", HttpContext.Current.Server.UrlEncode(mobile));
            para += string.Format("&sendtime={0}", "");//不定时发送，值为0，定时发送，输入格式YYYYMMDDHHmmss的日期值
            string str = doPostRequest(url, Encoding.ASCII.GetBytes(para));
            //DataSet ds = new DataSet();
            //ds.ReadXml(str);
            //new SystemDataSet().ReadXml(xmlSR, XmlReadMode.InferTypedSchema)
            return 0;

        }

        //POST方式发送得结果
        private static String doPostRequest(string url, byte[] bData)
        {
            System.Net.HttpWebRequest hwRequest;
            System.Net.HttpWebResponse hwResponse;

            string strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                hwRequest.Timeout = 5000;
                hwRequest.Method = "POST";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
                hwRequest.ContentLength = bData.Length;

                System.IO.Stream smWrite = hwRequest.GetRequestStream();
                smWrite.Write(bData, 0, bData.Length);
                smWrite.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
                return strResult;
            }

            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
            }

            return strResult;
        }

        //GET方式发送得结果
        private static String doGetRequest(string url)
        {
            HttpWebRequest hwRequest;
            HttpWebResponse hwResponse;

            string strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)WebRequest.Create(url);
                hwRequest.Timeout = 5000;
                hwRequest.Method = "GET";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
                return strResult;
            }

            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
            }

            return strResult;
        }

        private static void WriteErrLog(string strErr)
        {
            Console.WriteLine(strErr);
            System.Diagnostics.Trace.WriteLine(strErr);
        }
    }
}
