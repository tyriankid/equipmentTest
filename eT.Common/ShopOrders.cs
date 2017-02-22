using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace eT.Common
{
    /// <summary>
    /// 商品订单相关通用处理类
    /// </summary>
    public class ShopOrders
    {

        /// <summary>
        /// 揭晓倒计时（秒）
        /// </summary>
        //public static readonly int Q_end_time_second = 30;
        public static int Q_end_time_second { get { return Globals.GetMasterSettings(false).Q_end_time_second; } }

        //发表晒单间隔时间
        public static readonly int Publish_Spacing_Interval = 10;

        //点赞间隔时间
        public static readonly int Dianzan_Spacing_Interval = 10;

        //网吧活动ID
        public static readonly int Wangba_activity_ID = 1;


        //网吧类型ID
        public static readonly int Wangba_type_ID = 1;


        //网吧活动商品ID
        public static readonly string Wangba_activity_shopid = "4,5,7,66,35,23"; //30话费卡，50话费卡，100话费卡，iPhone7，vivo X7，ipad mini

        /// <summary>
        /// 获取业务类型
        /// </summary>
        public enum BusinessType
        {
            /// <summary>
            /// 充值
            /// </summary>
            add,
            /// <summary>
            /// 充值XX(20)送XXX(118)红包（一人一次，领取了活动资格才送）
            /// </summary>
            addrule,
            /// <summary>
            /// 一元购
            /// </summary>
            yuan,
            /// <summary>
            /// 团购
            /// </summary>
            tuan,
            /// <summary>
            /// 网吧结算
            /// </summary>
            wangba,
            /// <summary>
            /// 直接购
            /// </summary>
            quan,
            /// <summary>
            /// 积分商城奖品
            /// </summary>
            gift,
        }


        private static int step = 0;

        /// <summary>
        /// 生成订单号
        /// </summary>
        public static string GenerateOrderID(BusinessType businessType)
        {
            //根据业务设置订单号前辍
            string strPrev = "A";
            switch (businessType)
            {
                case BusinessType.add:
                    strPrev = "C";
                    break;
                case BusinessType.yuan:
                    strPrev = "Y";
                    break;
                case BusinessType.tuan:
                    strPrev = "T";
                    break;
                case BusinessType.wangba:
                    strPrev = "W";
                    break;
                case BusinessType.quan:
                    strPrev = "Q";
                    break;
                case BusinessType.gift:
                    strPrev = "G";
                    break;
            }

            //框架自带随机在大数据量下很容易重复，自写递增随机毫秒数再加随机数来确保订单唯一性
            int currStep = ShopOrders.step;
            ShopOrders.step = ShopOrders.step + 1;
            if (ShopOrders.step > 999) ShopOrders.step = 0;

            //生成
            Random rd = new Random();
            int iRandom = rd.Next(100, 999);
            string orderid = strPrev + DateTime.Now.AddMilliseconds(currStep).ToString("yyMMddHHmmssfff") + iRandom.ToString();
            return orderid;
        }

        /// <summary>
        /// 获取头像
        /// </summary>
        public static void SetTouxiang(DataRow dr)
        {
            string touxiangField = "touxiang";
            if (!dr.Table.Columns.Contains(touxiangField)) touxiangField = "img";
            if (dr["headimg"].ToString().Trim() != "")
            {
                dr[touxiangField] = dr["headimg"];
            }
            else
            {
                if (dr["img"].ToString().Trim() != "")
                {
                    dr[touxiangField] = Globals.G_UPLOAD_PATH+dr["img"];
                }
                else
                {
                    dr[touxiangField] = Globals.G_UPLOAD_PATH + "photo/member.jpg";
                }

            }
        }

        public static string[]  ToImg(string toimg)
        {
            string[] strs=new string[2];
            Regex RegImgBase64 = new Regex("^data:\\s*image/(\\w+);base64,([\\w/=\\+]*)$");
            Match match = RegImgBase64.Match(toimg);
            if (match.Success)
            {
                strs[0] = match.Groups[1].Value;
                strs[1] = match.Groups[2].Value;
            }

            return strs;
        }


        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="strimg">base64编码--图片</param>
        /// <param name="context"></param>
        /// <returns>返回要存入数据库的名称</returns>
        public static string uploadsimg(string strimg, string lujin, HttpContext context)
        {
            string date = DateTime.Now.ToString("yyyyMMdd");
            //将base64编码解析，返回后缀名
            string[] arrayImg = ShopOrders.ToImg(strimg);
            byte[] buffer = Convert.FromBase64String(arrayImg[1]);
            string fileurl = context.Server.MapPath(Globals.UPLOAD_PATH + lujin + "/" + date + "/" + Guid.NewGuid().ToString() + "." + arrayImg[0]);
            string bcfile = lujin+"/"+ date + "/" + Guid.NewGuid().ToString() + "." + arrayImg[0];
            if (!Directory.Exists(Path.GetDirectoryName(fileurl)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileurl));
            }
            string sfilepaths = context.Server.MapPath(Globals.UPLOAD_PATH + bcfile);
            File.WriteAllBytes(sfilepaths, buffer);
            return bcfile;

        }


        /// <summary>
        /// 获取卡密使用类型
        /// </summary>
        public static string GetCardUsetype(int usetype)
        {
            string useType = string.Empty;
            switch (usetype)
            { 
                case 1:
                    useType = "充话费";
                    break;
                case 2:
                    useType = "充支付宝";
                    break;
                case 3:
                    useType = "充爽乐币";
                    break;
            }
            return useType;
        }


    }
}
