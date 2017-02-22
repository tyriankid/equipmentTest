using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Utility
{
    /// <summary>
    /// 图片相关常用管理类【通用处理类】
    /// 最后更新 JHB: ON 2012-09-04
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        /// 生成缩略图(自适应按比例缩放)
        /// </summary>
        /// <param name="strOldPic">原始图片绝对地址</param>
        /// <param name="strNewPic">缩略图图片绝对地址</param>
        /// <param name="intHeight">缩略图宽度</param>
        /// <param name="intHeight">缩略图高度</param>
        public static void BuildMin(string strOldPic, string strNewPic, int intWidth, int intHeight)
        {
            System.Drawing.Bitmap objPic, objNewPic;
            try
            {
                objPic = new System.Drawing.Bitmap(strOldPic);
                if (objPic.Height >= objPic.Width && objPic.Height >= intHeight)
                {
                    intWidth = intHeight * objPic.Width / objPic.Height;
                    objNewPic = new System.Drawing.Bitmap(objPic, intWidth, intHeight);
                    objNewPic.Save(strNewPic);
                    objPic.Dispose();
                    objNewPic.Dispose();
                }
                else if (objPic.Height <= objPic.Width && objPic.Width > intWidth)
                {
                    intHeight = intWidth * objPic.Height / objPic.Width;
                    objNewPic = new System.Drawing.Bitmap(objPic, intWidth, intHeight);
                    objNewPic.Save(strNewPic);
                    objPic.Dispose();
                    objNewPic.Dispose();
                }
                else
                {
                    objPic.Save(strNewPic);
                    objPic.Dispose();
                }
            }
            catch { }
        }

        public static void WriteFont(string imgPath,string str)
        {
            /*
            Image image = Image.FromFile(imgPath);// 具体这张图是从文件读取还是从picturebox什么的获取你来指定
            using (Graphics g = Graphics.FromImage(image))
            {
                g.DrawString("xxxxx", new Font("宋体", 13),
                    Brushes.Red, new PointF(100, 100));
                g.Flush();
            }
            image.Save("D:\newimage.jpg");
            */

            
            //如果原图片是索引像素格式之列的，则需要转换
            Image imgPhoto = Image.FromFile(imgPath);
            Bitmap bmp = new Bitmap(imgPhoto.Width, imgPhoto.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(imgPhoto, 0, 0);
                imgPhoto.Dispose();
            }

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawString(str, new Font("宋体", 13),
                    Brushes.Red, new PointF(100, bmp.Height-20));
                g.Flush();
            }
            bmp.Save(Path.GetDirectoryName(imgPath) + "/" + Path.GetFileNameWithoutExtension(imgPath)+".bmp");
        }
       
    }
}


