using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace eT.Model
{
    /// <summary>
    /// 网站配置-实体类
    /// </summary>
    [Serializable]
    public class SiteSettings
    {
        private string _uploadImgUrl;
        private string _uploadImgPath;
        private bool _openMonitor;
        private bool _openCacheServer;
        private string _weixinAppId;
        private string _weixinAppSecret;
        private bool _isOpenWeixin;//是否开启微信登录
        private bool _isOpenManyService;//是否打开微信多客服
        private string _weixinPartnerID;
        private string _weixinPartnerKey;
        private string _weixinCertPath;
        private string _weixinCertPassword;
        private string _weixinPaySignKey;
        private string _WeixinToken;
        private string _G_UPLOAD_PATH;
        private string _API_Domain;
        private string _WX_Domain;
        private string _TaotaolePlatformKey;
        private string _TaotaoleWxKey;
        private string _WebSiteName;
        private int _Q_end_time_second;
        private string _WX_Template_00;
        private string _WX_Template_01;
        private string _WX_Template_02;
        private string _WX_Template_03;
        private string _Yolly_ID;
        private string _Yolly_KEY;
        private string _Pay_Domain;
        private string _Pay_WeixinAppId;
        private string _Pay_WeixinAppSecret;
        //2016-12-22佣金比例设置
        private int  _zengSet;
        private int _agentSet;
        private int _giftSet;
        private int _obligateSet;





        /// <summary>
        /// 获取或设置 上传图片网络地址
        /// </summary>
        public string UploadImgUrl
        {
            get { return _uploadImgUrl; }
            set { _uploadImgUrl = value; }
        }

        /// <summary>
        /// 获取或设置 上传图片物理地址
        /// </summary>
        public string UploadImgPath
        {
            get { return _uploadImgPath; }
            set { _uploadImgPath = value; }
        }

        /// <summary>
        /// 获取或设置 是否打开监视
        /// </summary>
        public bool OpenMonitor
        {
            get { return _openMonitor; }
            set { _openMonitor = value; }
        }

        /// <summary>
        /// 获取或设置 是否启用缓存服务
        /// </summary>
        public bool OpenCacheServer
        {
            get { return _openCacheServer; }
            set { _openCacheServer = value; }
        }

        /// <summary>
        /// 微信AppId
        /// </summary>
        public string WeixinAppId
        {
            get { return _weixinAppId; }
            set { _weixinAppId = value; }
        }

        /// <summary>
        /// 微信AppSecret
        /// </summary>
        public string WeixinAppSecret
        {
            get { return _weixinAppSecret; }
            set { _weixinAppSecret = value; }
        }

        /// <summary>
        /// 是否打开微信登录
        /// </summary>
        public bool IsOpenWeixin
        {
            get { return _isOpenWeixin; }
            set { _isOpenWeixin = value; }
        }

        public bool IsOpenManyService
        { 
            get{ return _isOpenManyService;}
            set { _isOpenManyService = value; } 
        }

        public string WeixinPartnerID
        {
            get { return _weixinPartnerID; }
            set { _weixinPartnerID = value; }
        }

        public string WeixinPartnerKey
        {
            get { return _weixinPartnerKey; }
            set { _weixinPartnerKey = value; }
        }

        public string WeixinCertPath
        {
            get { return _weixinCertPath; }
            set { _weixinCertPath = value; }
        }

        public string WeixinCertPassword
        {
            get { return _weixinCertPassword; }
            set { _weixinCertPassword = value; }
        }

        public string WeixinPaySignKey
        {
            get { return _weixinPaySignKey; }
            set { _weixinPaySignKey = value; }
        }
        public string WeixinToken
        {
            get { return _WeixinToken; }
            set { _WeixinToken = value; }
        }

        /// <summary>
        /// 获取或设置 上传图片的根URL
        /// </summary>
        public string G_UPLOAD_PATH
        {
            get { return _G_UPLOAD_PATH; }
            set { _G_UPLOAD_PATH = value; }
        }
        /// <summary>
        /// 获取或设置 接口域名
        /// </summary>
        public string API_Domain
        {
            get { return _API_Domain; }
            set { _API_Domain = value; }
        }
        /// <summary>
        /// 获取或设置 微信域名
        /// </summary>
        public string WX_Domain
        {
            get { return _WX_Domain; }
            set { _WX_Domain = value; }
        }
        /// <summary>
        /// 获取或设置 平台密钥
        /// </summary>
        public string TaotaolePlatformKey
        {
            get { return _TaotaolePlatformKey; }
            set { _TaotaolePlatformKey = value; }
        }
        /// <summary>
        /// 获取或设置 微信密钥
        /// </summary>
        public string TaotaoleWxKey
        {
            get { return _TaotaoleWxKey; }
            set { _TaotaoleWxKey = value; }
        }
        /// <summary>
        /// 获取或设置 微信网站名称
        /// </summary>
        public string WebSiteName
        {
            get { return _WebSiteName; }
            set { _WebSiteName = value; }
        }
        /// <summary>
        /// 获取或设置 倒计时(秒)
        /// </summary> 
        public int Q_end_time_second
        {
            get { return _Q_end_time_second; }
            set { _Q_end_time_second = value; }
        }

        public string WX_Template_00
        {
            get { return _WX_Template_00; }
            set { _WX_Template_00 = value; }
        }
        /// <summary>
        /// 获取或设置 中奖结果通知
        /// </summary>
        public string WX_Template_01
        {
            get { return _WX_Template_01; }
            set { _WX_Template_01 = value; }
        }
        /// <summary>
        /// 获取或设置 物流状态更新通知
        /// </summary>
        public string WX_Template_02
        {
            get { return _WX_Template_02; }
            set { _WX_Template_02 = value; }
        }
        /// <summary>
        /// 获取或设置 团购失败退款通知
        /// </summary>
        public string WX_Template_03
        {
            get { return _WX_Template_03; }
            set { _WX_Template_03 = value; }
        }
        /// <summary>
        /// 获取永乐唯一ID
        /// </summary>
        public string Yolly_ID
        {
            get { return _Yolly_ID; }
            set { _Yolly_ID = value; }
        }
        /// <summary>
        /// 获取永乐唯一KEY
        /// </summary>
        public string Yolly_KEY
        {
            get { return _Yolly_KEY; }
            set { _Yolly_KEY = value; }
        }
        /// <summary>
        /// 获取或设置 支付域名
        /// </summary>
        public string Pay_Domain
        {
            get { return _Pay_Domain; }
            set { _Pay_Domain = value; }
        }
        public string Pay_WeixinAppId
        {
            get { return _Pay_WeixinAppId; }
            set { _Pay_WeixinAppId = value; }
        }
        public string Pay_WeixinAppSecret
        {
            get { return _Pay_WeixinAppSecret; }
            set { _Pay_WeixinAppSecret = value; }
        }
        //佣金设置
        public int  ZengSet
        {
            get { return _zengSet; }
            set { _zengSet = value; }
        }
        public int AgentSet
        {
            get { return _agentSet; }
            set { _agentSet = value; }
        }
        public int GiftSet
        {
            get { return _giftSet; }
            set { _giftSet = value; }
        }
        public int ObligateSet
        {
            get { return _obligateSet; }
            set { _obligateSet = value; }
        }



        public static SiteSettings FromXml(XmlDocument doc)
        {
            XmlNode node = doc.SelectSingleNode("Settings");
            return new SiteSettings()
            {
                _uploadImgUrl = node.SelectSingleNode("UploadImgUrl").InnerText,
                _uploadImgPath = node.SelectSingleNode("UploadImgPath").InnerText,
                _openMonitor = bool.Parse(node.SelectSingleNode("OpenMonitor").InnerText),
                _openCacheServer = bool.Parse(node.SelectSingleNode("OpenCacheServer").InnerText),
                _weixinAppId = node.SelectSingleNode("WeixinAppId").InnerText,
                _weixinAppSecret = node.SelectSingleNode("WeixinAppSecret").InnerText,
                _isOpenWeixin = bool.Parse(node.SelectSingleNode("IsOpenWeixin").InnerText),
                _isOpenManyService = bool.Parse(node.SelectSingleNode("IsOpenManyService").InnerText),
                _weixinPartnerID = node.SelectSingleNode("WeixinPartnerID").InnerText,
                _weixinPartnerKey = node.SelectSingleNode("WeixinPartnerKey").InnerText,
                _weixinCertPath = node.SelectSingleNode("WeixinCertPath").InnerText,
                _weixinCertPassword = node.SelectSingleNode("WeixinCertPassword").InnerText,
                _weixinPaySignKey = node.SelectSingleNode("WeixinPaySignKey").InnerText,
                _WeixinToken = node.SelectSingleNode("WeixinToken").InnerText,
                _G_UPLOAD_PATH = node.SelectSingleNode("G_UPLOAD_PATH").InnerText,
                _API_Domain = node.SelectSingleNode("API_Domain").InnerText,
                _WX_Domain = node.SelectSingleNode("WX_Domain").InnerText,
                _TaotaolePlatformKey = node.SelectSingleNode("TaotaolePlatformKey").InnerText,
                _TaotaoleWxKey = node.SelectSingleNode("TaotaoleWxKey").InnerText,
                _WebSiteName = node.SelectSingleNode("WebSiteName").InnerText,
                _Q_end_time_second = (string.IsNullOrEmpty(node.SelectSingleNode("Q_end_time_second").InnerText) ? 0 : int.Parse(node.SelectSingleNode("Q_end_time_second").InnerText)),
                _WX_Template_00 = node.SelectSingleNode("WX_Template_00").InnerText,
                _WX_Template_01 = node.SelectSingleNode("WX_Template_01").InnerText,
                _WX_Template_02 = node.SelectSingleNode("WX_Template_02").InnerText,
                _WX_Template_03 = node.SelectSingleNode("WX_Template_03").InnerText,
                _Yolly_ID = node.SelectSingleNode("Yolly_ID").InnerText,
                 _Yolly_KEY = node.SelectSingleNode("Yolly_KEY").InnerText,
                _Pay_Domain = node.SelectSingleNode("Pay_Domain").InnerText,
                _Pay_WeixinAppId = node.SelectSingleNode("Pay_WeixinAppId").InnerText,
                _Pay_WeixinAppSecret = node.SelectSingleNode("Pay_WeixinAppSecret").InnerText,
                //佣金设置
                _zengSet = (string.IsNullOrEmpty(node.SelectSingleNode("ZengSet").InnerText) ? 0 : int.Parse(node.SelectSingleNode("ZengSet").InnerText)),
                _agentSet = (string.IsNullOrEmpty(node.SelectSingleNode("AgentSet").InnerText) ? 0 : int.Parse(node.SelectSingleNode("AgentSet").InnerText)),
                _giftSet = (string.IsNullOrEmpty(node.SelectSingleNode("GiftSet").InnerText) ? 0 : int.Parse(node.SelectSingleNode("GiftSet").InnerText)),
                _obligateSet = (string.IsNullOrEmpty(node.SelectSingleNode("ObligateSet").InnerText) ? 0 : int.Parse(node.SelectSingleNode("ObligateSet").InnerText)),
            };
        }

        public void WriteToXml(XmlDocument doc)
        {
            XmlNode root = doc.SelectSingleNode("Settings");
            SetNodeValue(doc, root, "UploadImgUrl", this.UploadImgUrl);
            SetNodeValue(doc, root, "UploadImgPath", this.UploadImgPath);
            SetNodeValue(doc, root, "OpenMonitor", this.OpenMonitor.ToString());
            SetNodeValue(doc, root, "OpenCacheServer", this.OpenCacheServer.ToString());
            SetNodeValue(doc, root, "WeixinAppId", this.WeixinAppId.ToString());
            SetNodeValue(doc, root, "WeixinAppSecret", this.WeixinAppSecret.ToString());
            SetNodeValue(doc, root, "IsOpenWeixin", this.IsOpenWeixin.ToString());
            SetNodeValue(doc, root, "IsOpenManyService", this.IsOpenManyService.ToString());
            SetNodeValue(doc, root, "WeixinPartnerID", this.WeixinPartnerID.ToString());
            SetNodeValue(doc, root, "WeixinPartnerKey", this.WeixinPartnerKey.ToString());
            SetNodeValue(doc, root, "WeixinCertPath", this.WeixinCertPath.ToString());
            SetNodeValue(doc, root, "WeixinCertPassword", this.WeixinCertPassword.ToString());
            SetNodeValue(doc, root, "WeixinPaySignKey", this.WeixinPaySignKey.ToString());
            SetNodeValue(doc, root, "WeixinToken", this.WeixinToken.ToString());
            SetNodeValue(doc, root, "G_UPLOAD_PATH", this.G_UPLOAD_PATH.ToString());
            SetNodeValue(doc, root, "API_Domain", this.API_Domain.ToString());
            SetNodeValue(doc, root, "WX_Domain", this.WX_Domain.ToString());
            SetNodeValue(doc, root, "TaotaolePlatformKey", this.TaotaolePlatformKey.ToString());
            SetNodeValue(doc, root, "TaotaoleWxKey", this.TaotaoleWxKey.ToString());
            SetNodeValue(doc, root, "WebSiteName", this.WebSiteName.ToString());
            SetNodeValue(doc, root, "Q_end_time_second", this.Q_end_time_second.ToString());
            SetNodeValue(doc, root, "WX_Template_00", this.WX_Template_00.ToString());
            SetNodeValue(doc, root, "WX_Template_01", this.WX_Template_01.ToString());
            SetNodeValue(doc, root, "WX_Template_02", this.WX_Template_02.ToString());
            SetNodeValue(doc, root, "WX_Template_03", this.WX_Template_03.ToString());
            SetNodeValue(doc, root, "Yolly_ID", this.Yolly_ID.ToString());
            SetNodeValue(doc, root, "Yolly_KEY", this.Yolly_KEY.ToString());
            SetNodeValue(doc, root, "Pay_Domain", this.Pay_Domain.ToString());
            SetNodeValue(doc, root, "Pay_WeixinAppId", this.Pay_WeixinAppId.ToString());
            SetNodeValue(doc, root, "Pay_WeixinAppSecret", this.Pay_WeixinAppSecret.ToString());
            //佣金设置
            SetNodeValue(doc, root, "ZengSet", this.ZengSet.ToString());
            SetNodeValue(doc, root, "AgentSet", this.AgentSet.ToString());
            SetNodeValue(doc, root, "GiftSet", this.GiftSet.ToString());
            SetNodeValue(doc, root, "ObligateSet", this.ObligateSet.ToString());
        }

        private static void SetNodeValue(XmlDocument doc, XmlNode root, string nodeName, string nodeValue)
        {
            XmlNode newChild = root.SelectSingleNode(nodeName);
            if (newChild == null)
            {
                newChild = doc.CreateElement(nodeName);
                root.AppendChild(newChild);
            }
            newChild.InnerText = nodeValue;
        }
        
    }
}
