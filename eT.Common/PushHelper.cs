using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eT.Common
{
    public class PushHelper
    {
        private string AppID = "A6919805544155";
        private string AppKey = "2709BDF0-3247-EDDB-6EC2-D6CDEC44B90C";

        public string title { get; set; }
        public string content { get; set; }
        public string type { get; set; }
        public string platform { get; set; }
        public string groupName { get; set; }
        public string userIds { get; set; }

        //title–消息标题，
        //content – 消息内容
        //type – 消息类型，1:消息 2:通知
        //platform - 0:全部平台，1：ios, 2：android
        //groupName - 推送组名，多个组用英文逗号隔开.默认:全部组。eg.group1,group2 .
        //userIds - 推送用户id, 多个用户用英文逗号分隔，eg. user1,user2。
        public void ts_01()
        {
            string formData = String.Format("title={0}&content={1}&type={2}&platform={3}&groupName={4}&userIds={5}", title, content, type, 0, groupName, userIds);
            //标题1 内容2 消息1/通知2 平台0全部1ios2案桌 制定用户5    （还可以制定群组）

            string url = String.Format("https://p.apicloud.com/api/push/message");
            System.Net.HttpWebRequest request = System.Net.HttpWebRequest.Create(url) as System.Net.HttpWebRequest;
            request.Method = "POST";
            request.Headers.Add("X-APICloud-AppId", AppID);
            request.Headers.Add("X-APICloud-AppKey", GetSHA1Key(AppID, AppKey));
            //aha1加密
            request.ContentType = "application/x-www-form-urlencoded";

            byte[] postData = System.Text.Encoding.UTF8.GetBytes(formData);
            request.ContentLength = postData.Length;

            using (System.IO.Stream reqStream = request.GetRequestStream())
            {
                //StreamWriter reqWriter = new StreamWriter(reqStream);
                reqStream.Write(postData, 0, postData.Length);
                //reqWriter.Write(formData);
                using (var response = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    using (System.IO.Stream respSream = response.GetResponseStream())
                    {
                        System.IO.StreamReader respReader = new System.IO.StreamReader(respSream);
                        string result = respReader.ReadToEnd();

                        //Console.WriteLine(result);
                    }
                }
            }
        }

        /// <summary>
        /// 发送APP消息
        /// </summary>
        /// <param name="uids">用户ID或用户ID串联</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        public static void SendAppMsg(string uids, string title, string content)
        {
            PushHelper ph = new PushHelper();
            ph.platform = "0";
            ph.userIds = uids;// "1941";//用户ID 
            ph.type = "2";//消息类型，1:消息 2:通知
            ph.title = title;
            ph.content = content;
            ph.ts_01();
        }

        //sha1加密
        static String GetSHA1Key(String AppId, String AppKey)
        {

            long longTime = (long)(DateTime.Now - new DateTime(1970, 01, 01)).TotalMilliseconds;
            String value = String.Format("{0}UZ{1}UZ{2}", AppId, AppKey, longTime);
            byte[] buffer = System.Security.Cryptography.SHA1.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(value));
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            foreach (byte num in buffer)
            {
                builder.AppendFormat("{0:x2}", num);
            }
            return builder.ToString() + "." + longTime;
        }

    }
}
