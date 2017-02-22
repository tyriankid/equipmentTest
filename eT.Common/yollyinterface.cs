using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;
using Utility;

namespace eT.Common
{
    /// <summary>
    /// 永乐通用接口
    /// </summary>
    public class yollyinterface
    {
        private static string yollyid = "65548187";//永乐账号
        private static string yollykey = "D39316FEC4984ABE97ED554D749EBE86";//永乐账号唯一key

        //话费充值接口地址：
        private static string rephone = " http://www.yolly.cn/third/interfaceNew/recharge.do?";


        /// <summary>
        /// 使用类型
        /// </summary>
        public enum usetype
        {
            /// <summary>
            /// 话费充值
            /// </summary>
            Phone,
            /// <summary>
            /// Q币充值
            /// </summary>
            QQB,
            ///<summary>
            ///支付宝充值
            ///</summary>
            Alipay,
            ///<summary>
            ///网吧充值
            ///</summary>
            Wangba,
            
        }

        //生成流水号
        private static int step = 0;

        /// <summary>
        /// 生成订单号
        /// </summary>
        public static string GenerateYollyID(usetype usetype)
        {
            //根据业务设置订单号前辍
            string strPrev = "W";
            switch (usetype)
            {
                case usetype.Phone:
                    strPrev = "P";
                    break;
                case usetype.QQB:
                    strPrev = "Q";
                    break;
                case usetype.Alipay:
                    strPrev = "A";
                    break;
                case usetype.Wangba:
                strPrev = "W";
                break;
            }

            //框架自带随机在大数据量下很容易重复，自写递增随机毫秒数再加随机数来确保订单唯一性
            int currStep = yollyinterface.step;
            yollyinterface.step = yollyinterface.step + 1;
            if (yollyinterface.step > 999) yollyinterface.step = 0;

            //生成
            Random rd = new Random();
            int iRandom = rd.Next(100, 999);
            string orderid = strPrev + DateTime.Now.AddMilliseconds(currStep).ToString("yyMMddHHmmssfff") + iRandom.ToString();
            return orderid;
        }




        /// <summary>
        /// 永乐充值接口
        /// </summary>
        /// <param name="post">充值参数数组，顺序为：充值时间、充值号码、充值金额(个数)、使用类型、真实姓名</param>
        /// <param name="usetype">充值类型</param>
        /// <returns></returns>
        public static string APIyolly(object[] post, usetype usetype)
        {
            string YOLLYID = yollyinterface.yollyid;
            string YOLLYKEY = yollyinterface.yollykey;
            string url = "";
            string para = string.Empty;
            switch(usetype)
            {     
                case usetype.Phone:
                    url = " http://www.yolly.cn/third/interfaceNew/recharge.do?";
                    para += string.Format("YOLLYID={0}", HttpContext.Current.Server.UrlEncode(YOLLYID));
                    para += string.Format("&YOLLYTIME={0}", HttpContext.Current.Server.UrlEncode((post[0]).ToString()));//发起时间
                    para += string.Format("&YOLLYURL={0}", HttpContext.Current.Server.UrlEncode(Globals.API_Domain+"Calls/call_yolly.ashx"));//回调url
                    para += string.Format("&YOLLYFLOW={0}", HttpContext.Current.Server.UrlEncode((post[3]).ToString()));//流水号
                    para += string.Format("&MOBILE={0}", HttpContext.Current.Server.UrlEncode((post[1]).ToString()));//充值号码
                    para += string.Format("&MONEY={0}", HttpContext.Current.Server.UrlEncode((post[2]).ToString()));//充值金额  65548187000000120160823184853134770789511D39316FEC4984ABE97ED554D749EBE86
                    para += string.Format("&TYPE={0}", HttpContext.Current.Server.UrlEncode(""));        
                    para += string.Format("&CIPHERTEXT={0}", HttpContext.Current.Server.UrlEncode("yolly2.0"));//密文标识
                    string Pmd5 = SecurityHelper.GetMd5To32(YOLLYID + (post[3]).ToString() + (post[0]).ToString() + Globals.API_Domain + "Calls/call_yolly.ashx" + (post[1]).ToString() + (post[2]).ToString() + "" + YOLLYKEY).ToLower();
                    para += string.Format("&MD5TEXT={0}", HttpContext.Current.Server.UrlEncode(Pmd5));
                    break;
                case usetype.QQB:
                    url = " http://www.yolly.cn/third/interfaceGame/rechargeGame.do?";
                    para += string.Format("YOLLYID={0}", HttpContext.Current.Server.UrlEncode(YOLLYID));
                    para += string.Format("&YOLLYTIME={0}", HttpContext.Current.Server.UrlEncode((post[0]).ToString()));//发起时间
                    para += string.Format("&YOLLYURL={0}", HttpContext.Current.Server.UrlEncode(Globals.API_Domain+"Calls/call_yolly.ashx"));//发起时间
                    para += string.Format("&YOLLYFLOW={0}", HttpContext.Current.Server.UrlEncode((post[3]).ToString()));//流水号
                    para += string.Format("&ACCOUNT={0}", HttpContext.Current.Server.UrlEncode((post[1]).ToString()));//充值号码
                    para += string.Format("&NUM={0}", HttpContext.Current.Server.UrlEncode((post[2]).ToString()));//充值金额  65548187000000120160823184853134770789511D39316FEC4984ABE97ED554D749EBE86
                    para += string.Format("&TYPE={0}", HttpContext.Current.Server.UrlEncode((post[4]).ToString()));
                    para += string.Format("&CIPHERTEXT={0}", HttpContext.Current.Server.UrlEncode("yolly2.0"));//密文标识
                    para += string.Format("&alipayName={0}", HttpContext.Current.Server.UrlEncode((post[5]).ToString()));//用户支付宝真实姓名
                    string Qmd5 = SecurityHelper.GetMd5To32(YOLLYID + (post[3]).ToString() + (post[0]).ToString() + Globals.API_Domain + "Calls/call_yolly.ashx" + (post[1]).ToString() + (post[4]).ToString() + (post[2]).ToString() + (post[5]).ToString() + YOLLYKEY).ToLower();
                    para += string.Format("&MD5TEXT={0}", HttpContext.Current.Server.UrlEncode(Qmd5));
                    break;
                case usetype.Alipay:
                    string reusename = HttpUtility.UrlEncode(HttpUtility.UrlEncode((post[5]).ToString(), UTF8Encoding.UTF8), UTF8Encoding.UTF8);
                    url = " http://www.yolly.cn/third/interfaceGame/rechargeGame.do?";
                    para += string.Format("YOLLYID={0}", HttpContext.Current.Server.UrlEncode(YOLLYID));
                    para += string.Format("&YOLLYTIME={0}", HttpContext.Current.Server.UrlEncode((post[0]).ToString()));//发起时间
                    para += string.Format("&YOLLYURL={0}", HttpContext.Current.Server.UrlEncode(Globals.API_Domain+"Calls/call_yolly.ashx"));//发起时间
                    para += string.Format("&YOLLYFLOW={0}", HttpContext.Current.Server.UrlEncode((post[3]).ToString()));//流水号
                    para += string.Format("&ACCOUNT={0}", HttpContext.Current.Server.UrlEncode((post[1]).ToString()));//充值号码
                    para += string.Format("&NUM={0}", HttpContext.Current.Server.UrlEncode((post[2]).ToString()));//充值金额  65548187000000120160823184853134770789511D39316FEC4984ABE97ED554D749EBE86
                    para += string.Format("&TYPE={0}", HttpContext.Current.Server.UrlEncode((post[4]).ToString()));//1.QQ币 2.支付宝账号充值
                    para += string.Format("&CIPHERTEXT={0}", HttpContext.Current.Server.UrlEncode("yolly2.0"));//密文标识
                    para += string.Format("&alipayName={0}", HttpContext.Current.Server.UrlEncode(reusename));//用户支付宝真实姓名
                    string Amd5 = SecurityHelper.GetMd5To32(YOLLYID + (post[3]).ToString() + (post[0]).ToString() + Globals.API_Domain + "Calls/call_yolly.ashx" + (post[1]).ToString() + (post[4]).ToString() + (post[2]).ToString() + (post[5]).ToString() + YOLLYKEY).ToLower();
                    para += string.Format("&MD5TEXT={0}", HttpContext.Current.Server.UrlEncode(Amd5));
                    break;
            }

            return sendreceive(Encoding.ASCII.GetBytes(para), url);
        }


        public static string sendreceive(byte[] post, string url)
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
                hwRequest.ContentLength = post.Length;

                System.IO.Stream smWrite = hwRequest.GetRequestStream();
                smWrite.Write(post, 0, post.Length);
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
            //用UTF-8两次解密返回数据
            string xmlMsg = HttpUtility.UrlDecode(HttpUtility.UrlDecode(strResult, UTF8Encoding.UTF8), UTF8Encoding.UTF8);
            //Globals.DebugLogger("xmlMsg：" + xmlMsg);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlMsg);
            string state="";
            string content="";
            XmlNodeList xxList = doc.GetElementsByTagName("YOLLY");
            //循环xml数据，得到自己想要的值
            foreach (XmlNode xxNode in xxList)  //Node 是每一个<CL>...</CL>体  
            {
                XmlNode node1 = xxNode.SelectNodes("RESPONSE/RSPCODE").Item(0);
                XmlNode node2 = xxNode.SelectNodes("RESPONSE/RSPDESC").Item(0);

                if (node1 != null)
                {
                    state = node1.InnerText;
                }
                if (node2 != null)
                {
                    content = node2.InnerText;
                }
            }
            return (state + "|" + content);
        }

        /// <summary>
        /// 通过流水号查询订单接口
        /// </summary>
        /// <param name="serialid">流水号</param>
        /// <param name="type">查询类型，1、话费 2、游戏</param>
        /// <returns></returns>
        public static string telephoneyolly(string serialid, string type)
        {
            string url = null;
            switch (type)
            {
                case "1"://话费查询
                    url = "http://www.yolly.cn/third/interfaceNew/queryOrder.do?";
                    break;
                case "2"://支付宝
                case "4"://QQ币
                    url = "http://www.yolly.cn/third/interfaceGame/queryGameOrder.do?";
                    break;
            }
            string para = string.Empty;
            para += string.Format("YOLLYID={0}", HttpContext.Current.Server.UrlEncode(yollyid));
            para += string.Format("&FLOWNUMBER={0}", HttpContext.Current.Server.UrlEncode(serialid));//流水号
            para += string.Format("&CIPHERTEXT={0}", HttpContext.Current.Server.UrlEncode("yolly2.0"));//密文标识
            string Pmd5 = SecurityHelper.GetMd5To32(yollyid + serialid + yollykey).ToLower();
            para += string.Format("&MD5TEXT={0}", HttpContext.Current.Server.UrlEncode(Pmd5));
            Encoding.ASCII.GetBytes(para);
            return selectreceive(Encoding.ASCII.GetBytes(para), url);
        }
        public static string selectreceive(byte[] post, string url)
        {
            System.Net.HttpWebRequest hwRequest;
            System.Net.HttpWebResponse hwResponse;
            string strResult = string.Empty;
            hwRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            hwRequest.Timeout = 5000;
            hwRequest.Method = "POST";
            hwRequest.ContentType = "application/x-www-form-urlencoded";
            hwRequest.ContentLength = post.Length;

            System.IO.Stream smWrite = hwRequest.GetRequestStream();
            smWrite.Write(post, 0, post.Length);
            smWrite.Close();
            hwResponse = (HttpWebResponse)hwRequest.GetResponse();
            StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
            strResult = srReader.ReadToEnd();
            srReader.Close();
            hwResponse.Close();
            string xmlMsg = HttpUtility.UrlDecode(HttpUtility.UrlDecode(strResult, UTF8Encoding.UTF8), UTF8Encoding.UTF8);
            //Globals.DebugLogger(xmlMsg);
            return xmlMsg;
        }
        private static void WriteErrLog(string strErr)
        {
            Console.WriteLine(strErr);
            System.Diagnostics.Trace.WriteLine(strErr);
        }
    }
}
