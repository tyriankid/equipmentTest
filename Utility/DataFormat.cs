using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Utility
{
    /// <summary>
    /// 数据格式化(转换)类【通用处理类】
    /// 最后更新 JHB: ON 2012-09-05
    /// </summary>
    public class DataFormat
    {
        /// <summary>
        /// 以Base64进行编码
        /// </summary>
        public static string Base64Encode(string AStr)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(AStr));
        }

        /// <summary>
        /// 以Base64进行解码
        /// </summary>
        public static string Base64Decode(string ABase64)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(ABase64));
        }

        /// <summary>
        /// 物理图片转换为byte数组
        /// </summary>
        /// <param name="imgPath">绝对路径</param>
        public static byte[] ImgPathToByte(string imgPath)
        {
            MemoryStream stream = new MemoryStream();
            Image dest = new Bitmap(imgPath);
            dest.Save(stream, ImageFormat.Png);
            byte[] b = stream.ToArray();
            dest.Dispose();
            return b;
        }
    }
}
