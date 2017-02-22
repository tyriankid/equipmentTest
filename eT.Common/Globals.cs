using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Xml;
using eT.Model;
using Utility;
using System.Net;

namespace eT.Common
{
    /// <summary>
    /// 全局管理类（备注：只能web、api等外部应用引用引项目）
    /// </summary>
    public static class Globals
    {
        //public static string WebSiteName = "爽乐购";
        public static string WebSiteName { get { return GetMasterSettings(false).WebSiteName; } }
        private const string MasterSettingsCacheKey = "FileCache-MasterSettings";
        //public const string TaotaolePlatformKey = "taotaole888AppSn";
        public static string TaotaolePlatformKey { get { return GetMasterSettings(false).TaotaolePlatformKey; } }
        //public const string TaotaoleWxKey = "taotaole888";
        public static string TaotaoleWxKey { get { return GetMasterSettings(false).TaotaoleWxKey; } }

        public static readonly string  TaotaoleMemberKey = "YiHuiTaotaole-Member-V0926";

        /// <summary>
        /// 上传图片的根URL
        /// </summary>
        public static string G_UPLOAD_PATH { get { return GetMasterSettings(false).G_UPLOAD_PATH; } }

        /// <summary>
        /// 相对图片上传路径
        /// </summary>
        public static string UPLOAD_PATH = "/Resources/uploads/";
        public static string IMG_PATH = "/Resources/images/";
        public static string JS_PATH = "/Resources/js/";
        public static string CSS_PATH = "/Resources/css/";

        /// <summary>
        /// 获取云购码表单表最大记录数
        /// </summary>
        public static readonly int CodeTablemMax = 500000;

        /// <summary>
        /// 接口域名(后期将更换域名使用https协议)
        /// </summary>
        public static string API_Domain { get { return GetMasterSettings(false).API_Domain; } }
        public static string WX_Domain { get { return GetMasterSettings(false).WX_Domain; } }

        /// <summary>
        /// 获取网站配置
        /// </summary>
        /// <param name="cacheable">是否使用缓存读取</param>
        public static SiteSettings GetMasterSettings(bool cacheable = true)
        {
            if (!cacheable)
            {
                WebCache.Remove("FileCache-MasterSettings");
            }
            XmlDocument document = WebCache.Get("FileCache-MasterSettings") as XmlDocument;
            if (document == null)
            {
                string masterSettingsFilename = GetMasterSettingsFilename();
                if (!File.Exists(masterSettingsFilename))
                {
                    return null;
                }
                document = new XmlDocument();
                document.Load(masterSettingsFilename);
                if (cacheable)
                {
                    WebCache.Max("FileCache-MasterSettings", document, new CacheDependency(masterSettingsFilename));
                }
            }
            return SiteSettings.FromXml(document);
        }

        /// <summary>
        /// 更新网站配置
        /// </summary>
        /// <param name="settings">网站配置实体</param>
        public static void SaveMasterSettings(SiteSettings settings)
        {
            saveMasterSettings(settings);
            WebCache.Remove("FileCache-MasterSettings");
        }

        private static string GetMasterSettingsFilename()
        {
            HttpContext current = HttpContext.Current;
            return ((current != null) ? current.Request.MapPath("~/config/SiteSettings.config") : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"config\SiteSettings.config"));
        }

        private static void saveMasterSettings(SiteSettings settings)
        {
            string masterSettingsFilename = GetMasterSettingsFilename();
            XmlDocument doc = new XmlDocument();
            if (File.Exists(masterSettingsFilename))
            {
                doc.Load(masterSettingsFilename);
            }
            settings.WriteToXml(doc);
            doc.Save(masterSettingsFilename);
        }
        /// <summary>
        /// 获取当前用户cookie值[YiHuiTaotaole-Manager](用户主键ID)
        /// </summary>
        public static int GetCurrentMemberUserId()
        {
            int result = 0;
            string uid = SecurityHelper.Decrypt(Globals.TaotaoleWxKey, GetCurrentMemberUserIdSign());
            int.TryParse(uid, out result);
            return result;
        }
        public static string GetCurrentMemberUserIdSign()
        {

            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(Globals.TaotaoleMemberKey);
            if ((cookie == null) || string.IsNullOrEmpty(cookie.Value))
            {
                return "";
            }
            return cookie.Value;

            //return SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1824");//临时本地测试
        }

        /// <summary>
        /// 获取当前管理员cookie值[YiHuiTaotaole-Manager](用户主键ID)
        /// </summary>
        public static int GetCurrentManagerUserId()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("YiHuiTaotaole-Manager");
            if ((cookie == null) || string.IsNullOrEmpty(cookie.Value))
            {
                return 0;
            }
            int result = 0;
            int.TryParse(cookie.Value, out result);
            return result;
        }

        /// <summary>
        /// 获取应用目录路径
        /// </summary>
        public static string ApplicationPath
        {
            get
            {
                string applicationPath = "/";
                if (HttpContext.Current != null)
                {
                    try
                    {
                        applicationPath = HttpContext.Current.Request.ApplicationPath;
                    }
                    catch
                    {
                        applicationPath = AppDomain.CurrentDomain.BaseDirectory;
                    }
                }
                if (applicationPath == "/")
                {
                    return string.Empty;
                }
                return applicationPath.ToLower(CultureInfo.InvariantCulture);
            }
        }

        public static string UrlDecode(string urlToDecode)
        {
            if (string.IsNullOrEmpty(urlToDecode))
            {
                return urlToDecode;
            }
            return HttpUtility.UrlDecode(urlToDecode, Encoding.UTF8);
        }

        public static string UrlEncode(string urlToEncode)
        {
            if (string.IsNullOrEmpty(urlToEncode))
            {
                return urlToEncode;
            }
            return HttpUtility.UrlEncode(urlToEncode, Encoding.UTF8);
        }

        public static string GetGenerateId()
        {
            string str = string.Empty;
            Random random = new Random();
            for (int i = 0; i < 7; i++)
            {
                int num = random.Next();
                str = str + ((char)(0x30 + ((ushort)(num % 10)))).ToString();
            }
            return (DateTime.Now.ToString("yyyyMMdd") + str);
        }

        /// <summary>
        /// 支付方式
        /// </summary>
        public enum payType
        {
            /// <summary>
            /// 微信内支付
            /// </summary>
            wxPay = 1,
            /// <summary>
            /// app微信支付
            /// </summary>
            appWxPay = 2
        }

        public enum businessType
        {
            /// <summary>
            /// 商品类型 0:实体 1:卡密 2:游戏
            /// </summary>
            productType,
            /// <summary>
            /// 商品状态 0:正常 1:下架
            /// </summary>
            productStatus,
            /// <summary>
            /// 微信回复匹配类型1:关键字回复 2:关注时回复 3:无匹配回复
            /// </summary>
            replyType,
            /// <summary>
            /// 微信回复类型 1:文本 2:单图文 3:多图文
            /// </summary>
            messageType,
            /// <summary>
            /// 永乐订单状态
            /// </summary>
            yollystatus,
            /// <summary>
            /// 永乐订单使用类型
            /// </summary>
            yollytype,
            /// <summary>
            /// 支付方式, 1:微信支付 2:app微信支付
            /// </summary>
            pay_type,
            /// <summary>
            /// 用户来源, 0:默认微信关注 1:app登录 9:百度推广
            /// </summary>
            user_type,
            /// <summary>
            /// 积分商城抽奖游戏类型
            /// </summary>
            giftGameType
        }

        /// <summary>
        /// 获取某业务类型下id对应的意义(例如,product表下的商品状态status,0返回'正常',1返回'下架')
        /// </summary>
        /// <param name="type"></param>
        /// <param name="typevalue"></param>
        /// <returns></returns>
        public static string getEnumDisplayName(businessType type, int typevalue)
        {
            string resultStr = "";
            switch (type)
            {
                #region 商品相关属性
                //商品类型
                case businessType.productType:
                    switch (typevalue)
                    {
                        case 0:
                            resultStr = "实体商品"; break;
                        case 1:
                            resultStr = "话费商品"; break;
                        case 2:
                            resultStr = "游戏商品"; break;
                        case 3:
                            resultStr = "Q币商品"; break;
                        case 4:
                            resultStr = "账号商品"; break;
                    }
                    break;

                //商品状态
                case businessType.productStatus:
                    switch (typevalue)
                    {
                        case 0:
                            resultStr = "正常"; break;
                        case 1:
                            resultStr = "下架"; break;
                    }
                    break;
                #endregion
                #region 微信相关
                case businessType.replyType: //匹配回复类型
                    switch (typevalue)
                    {
                        case 1:
                            resultStr = "关键字回复"; break;
                        case 2:
                            resultStr = "关注时回复"; break;
                        case 3:
                            resultStr = "无匹配回复"; break;
                    }
                    break;

                case businessType.messageType: //回复内容类型
                    switch (typevalue)
                    {
                        case 1:
                            resultStr = "文本"; break;
                        case 2:
                            resultStr = "单图文"; break;
                        case 3:
                            resultStr = "多图文"; break;
                    }
                    break;
                case businessType.yollystatus:
                    switch (typevalue)
                    {
                        case 0:
                            resultStr = "<span style='color:red'>待充值</span>"; break;
                        case 1:
                            resultStr = "已完成"; break;
                        case 2:
                            resultStr = "<span style='color:red'>失&nbsp;&nbsp;&nbsp;败</span>"; break;
                    }
                    break;
                //1、话费 2、支付宝 4、Q币 11、城市服务商佣金 12、网吧活动结算
                case businessType.yollytype:
                    switch (typevalue)
                    {
                        case 1:
                            resultStr = "话费充值"; break;
                        case 2:
                            resultStr = "支付宝充值"; break;
                        case 3:
                            resultStr = "充值爽乐币"; break;
                        case 4:
                            resultStr = "QQ币充值"; break;
                        case 11:
                            resultStr = "城市服务商佣金"; break;
                        case 12:
                            resultStr = "网吧活动结算"; break;
                    }
                    break;
                #endregion
                #region 支付相关
                case businessType.pay_type: //匹配回复类型
                    switch (typevalue)
                    {
                        case 1:
                            resultStr = "微信支付"; break;
                        case 2:
                            resultStr = "app微信支付"; break;
                    }
                    break;
                #endregion
                #region 用户相关
                case businessType.user_type: //用户来源
                    switch (typevalue)
                    {
                        case 0:
                            resultStr = "微信关注"; break;
                        case 1:
                            resultStr = "app登录"; break;
                        case 9:
                            resultStr = "百度推广"; break;
                    }
                    break;
                #endregion

            }
            return resultStr;
        }

        public static string getGiftCodeSum(string CodeSums, string winSums)
        {
            string Sum = "0";
            if (!string.IsNullOrEmpty(CodeSums))
            {
                Sum = CodeSums.TrimEnd(',').TrimStart(',').Split(',').Length.ToString();
            }
            if (!string.IsNullOrEmpty(winSums))
            {
                Sum = winSums.TrimEnd(',').TrimStart(',').Split(',').Length.ToString();
            }
            return Sum;
        }


        public static string getGiftGameType(string gameStr)
        {
                #region 奖品相关
            string str = "";
            if (gameStr.ToString().IndexOf("pao") >= 0)
                str += " 跑马灯";
            if (gameStr.ToString().IndexOf("zhuan") >= 0)
                str += " 大转盘";
            if (gameStr.ToString().IndexOf("gua") >= 0)
                str += " 刮刮乐";
            return str;
                #endregion
        }
        public static string getOrderType(string type)
        {
            #region 订单来源相关
            string str = "";
            if (type.ToString()=="gift")
                str = "积分商城";
            if (type.ToString() == "quan")
                str= "直购商城";
            if (type.ToString() == "yuan")
                str= "元购商城";
            if (type.ToString() == "tuan")
                str= " 团购商城";
            return str;
            #endregion
        }
        /// <summary>
        /// 获取某业务类型下所有的下拉框
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataView getEnumDisplayNameList(businessType type)
        {
            object businessType = WebCache.Get("FileCache-businessType");
            if (businessType == null)
            {
                #region  初始化枚举数据
                DataTable dtData = new DataTable();
                dtData.Columns.Add("type", typeof(string));
                dtData.Columns.Add("name", typeof(string));
                dtData.Columns.Add("value", typeof(int));

                dtData.Rows.Add(new object[] { "productType", 0, "实体商品" });
                dtData.Rows.Add(new object[] { "productType", 1, "卡密商品" });
                dtData.Rows.Add(new object[] { "productType", 2, "游戏商品" });

                dtData.Rows.Add(new object[] { "productStatus", 0, "正常" });
                dtData.Rows.Add(new object[] { "productStatus", 1, "下架" });

                WebCache.Max("FileCache-businessType", dtData, new CacheDependency("businessType"));
                #endregion
            }
            DataTable dataTable = businessType as DataTable;
            DataView dvResult = new DataView(dataTable, string.Format("type='{0}' "), type.ToString()
                , DataViewRowState.CurrentRows);
            return dvResult;
        }

        /// <summary>
        /// 验证签名,成功返回对应数据
        /// </summary>
        /// <param name="sign">待验证的签名</param>
        /// <param name="dataid">明文传输时的验证(目前针对微信)</param>
        public static string ValidateSign(string sign, string dataid = null)
        {
            if (string.IsNullOrEmpty(sign)) return "";
            if (sign.IndexOf("MW_") != 0)
            {
                return SecurityHelper.Decrypt(DbServers.APIKey, sign);
            }
            else
            {
                if (string.IsNullOrEmpty(dataid) || sign != SecurityHelper.GetSign(dataid)) return "";
                return SecurityHelper.Decrypt(Globals.TaotaoleWxKey, dataid);
            }
        }

        /// <summary>
        /// 验证APP内部传输签名
        /// </summary>
        public static bool ValidateSignAppInner(string sign, string dataid)
        {
            return SecurityHelper.GetSignAppInner(dataid) == sign;
        }

        private static readonly bool isLogger = true;

        /// <summary>
        /// 调试日志
        /// </summary>
        public static void DebugLogger(string log, string filename = "Debug_Logger.txt")
        {
            if (!isLogger) return;
            string logFile = HttpContext.Current.Request.MapPath("~/" + filename + "");
            File.AppendAllText(logFile, string.Format("{0}:{1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), log));
        }
        public static string menujson;

        /// <summary>
        /// 获取当前页面的权限
        /// </summary>
        /// <param name="title">当前页面的title</param>
        /// <returns> //code：0查看、1维护、2全部。维护权限时不能做审核、结算等操作，只有基本的增删改。</returns>
        public static int getcode(string thisPageTitle)
        {
            if (menujson == null || menujson == "")
            {
                return 1;
            }
             DataTable dtmenu=DataHelper.JsonToDataTable(menujson);
             int code=1;
             for (int i = 0; i < dtmenu.Rows.Count; i++)
             {
                 if (thisPageTitle == dtmenu.Rows[i]["name"].ToString() && dtmenu.Rows[i]["code"].ToString() != "")
                 {
                     code =int.Parse(dtmenu.Rows[i]["code"].ToString());
                     break;
                 }
             }
             return code;   //code：0查看、1维护、2全部。维护权限时不能做审核、结算等操作，只有基本的增删改。
        }


        public static int ordercount = 100;
    }
}
