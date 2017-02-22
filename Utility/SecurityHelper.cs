using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Utility
{
    /// <summary>
    /// 安全(加解密等)常用类【通用处理类】
    /// 最后更新 JHB: ON 2012-12-22
    /// </summary>
    public class SecurityHelper
    {
        #region 可逆加密方法

        private static byte[] DESKey = new byte[] { 11, 23, 93, 102, 72, 41, 18, 12 };
        private static byte[] DESIV = new byte[] { 75, 158, 46, 97, 78, 57, 17, 36 };

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="notEncryptStr">待加密的明文字符串</param>
        /// <returns>加密后的字符串</returns>
        private static string Encode(string notEncryptStr)
        {
            //初始化加密器生成器
            DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
            MemoryStream objMemoryStream = new MemoryStream();
            CryptoStream objCryptoStream = new CryptoStream(objMemoryStream, objDES.CreateEncryptor(DESKey, DESIV), CryptoStreamMode.Write);
            StreamWriter objStreamWriter = new StreamWriter(objCryptoStream);
            objStreamWriter.Write(notEncryptStr);
            objStreamWriter.Flush();
            objCryptoStream.FlushFinalBlock();
            objMemoryStream.Flush();
            string decryptStr = Convert.ToBase64String(objMemoryStream.GetBuffer(), 0, (int)objMemoryStream.Length);
            return decryptStr;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="decryptStr">待解密的密文字符串</param>
        /// <returns>解密后的字符串</returns>
        private static string Decode(string decryptStr)
        {
            //初始化加密器生成器
            DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
            byte[] Input = Convert.FromBase64String(decryptStr);
            MemoryStream objMemoryStream = new MemoryStream(Input);
            CryptoStream objCryptoStream = new CryptoStream(objMemoryStream, objDES.CreateDecryptor(DESKey, DESIV), CryptoStreamMode.Read);
            StreamReader objStreamReader = new StreamReader(objCryptoStream);
            string notEncryptStr = objStreamReader.ReadToEnd();
            return notEncryptStr;
        }

        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="sKey">密钥</param>
        /// <param name="sText">待加密的字符</param>
        public static string Encrypt(string sKey, string sText)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(sText);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        
        /// <summary>
        /// 解密数据  
        /// </summary>
        /// <param name="sKey">密钥</param>
        /// <param name="sText">待解密的字符</param>
        public static string Decrypt(string sKey, string sText)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                int len;
                len = sText.Length / 2;
                byte[] inputByteArray = new byte[len];
                int x, i;
                for (x = 0; x < len; x++)
                {
                    i = Convert.ToInt32(sText.Substring(x * 2, 2), 16);
                    inputByteArray[x] = (byte)i;
                }
                des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
                des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Encoding.Default.GetString(ms.ToArray());
            }
            catch {
                return "";
            }
        }  

        #endregion

        /// <summary>
        /// md5 16位加密
        /// </summary>
        /// <param name="ConvertString">待加密的字符</param>
        /// <returns>16位加密字符</returns>
        public static string GetMd5To16(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash
             (UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }
        static string UserMd5(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大 写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X");

            }
            return pwd;
        }

        /// <summary>
        /// md5 32位加密
        /// </summary>
        /// <param name="ConvertString">加密的字符</param>
        /// <returns>32位加密字符</returns>
        public static string GetMd5To32(string pwd)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "md5");
        }

        /// <summary>
        /// sha1 40位加密
        /// </summary>
        /// <param name="ConvertString">加密的字符</param>
        /// <returns>40位加密字符</returns>
        public static string GetSha1To40(string pwd)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "sha1");
        }


        /// <summary>
        /// 明文传输签名规则
        /// </summary>
        public static string GetSign(string transmissioninfo)
        {
            string sign = "MW_";
            sign += SecurityHelper.GetMd5To32("wx" + transmissioninfo + "ttl");
            return sign;
        }

        /// <summary>
        /// APP内部传输签名算法
        /// </summary>
        public static string GetSignAppInner(string transmissioninfo)
        {
            string str = string.Format("GetSign_{0}_AppInnerKey_jhb", transmissioninfo);
            string sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5");
            return sign;
        }


    }


}
