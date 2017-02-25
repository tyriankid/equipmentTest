using Items.Eqpt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Items.GNRT
{
    /// <summary>
    /// 装备生成器
    /// </summary>
    public class Generator
    {

        public static Dictionary<string, Type> equipmentClassesDict = new Dictionary<string, Type>();
        /// <summary>
        /// 环境变量
        /// </summary>
        private EnvironmentVariable ev { get; set; }



        /// <summary>
        /// 装备生成基数键值对
        /// </summary>
        private static Dictionary<EquipmentSingle.EquipmentTypeEnum, int> eqptGeneratBaseNumber = new Dictionary<EquipmentSingle.EquipmentTypeEnum, int>() {
            { EquipmentSingle.EquipmentTypeEnum.Armor,  5},
            { EquipmentSingle.EquipmentTypeEnum.Helmet,  5},
            { EquipmentSingle.EquipmentTypeEnum.Boot,  5},
            { EquipmentSingle.EquipmentTypeEnum.Glove,  5},
        };

        
        public static void CollectAllEntityClasses()
        {
            equipmentClassesDict.Clear();
            System.Reflection.Assembly[] AS = System.AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < AS.Length; i++)
            {
                Type[] types = AS[i].GetTypes();
                for (int j = 0; j < types.Length; j++)
                {
                    string className = types[j].Name;
                    if (types[j].IsSubclassOf(typeof(EquipmentSingle)))
                    {
                        //MessageBox.Show("classname " + className);
                        equipmentClassesDict.Add(className, types[j]);
                    }

                }
            }
        }

        public static Item generateItem()
        {
            //根据得到的Item子类型生成相应子类
            if ("是道具类" == "")
            {
                return null;
            }
            else if("是装备类" == "")
            {
                return generateEqpt();
            }
            else
            {
                return null;
            }
        }

        public static EquipmentSingle generateEqpt(){
            EquipmentSingle eqpt =new EquipmentSingle();
            //将装备类型基数取和
            int baseNumTotal = 0;
            //新建一个数组,存入每个装备基数对应的key,例如 基数为5对应索引0-5的位置 用于根据基数随机出出现的装备类型
            ArrayList baseNumberList = new ArrayList();
            foreach (KeyValuePair<EquipmentSingle.EquipmentTypeEnum, int> kvp in eqptGeneratBaseNumber)
            {
                baseNumTotal += kvp.Value;
                for (int i = 0; i < kvp.Value; i++)
                {
                    baseNumberList.Add(kvp.Key);
                }
            }
            //根据基数总数为随机数上限,然后在数组中随机取值,对应的类型就是当前要生成的装备类型
            Random rd = new Random();
            //将得到的类型进行switch,以返回相应的装备类型
            string equipmentTypeName = ((EquipmentSingle.EquipmentTypeEnum)baseNumberList[rd.Next(baseNumTotal)]).ToString();
            //根据枚举名获得对应类型的装备
            Type equipmentType = null;
            if (equipmentClassesDict.TryGetValue(equipmentTypeName, out equipmentType))
            {
                return Activator.CreateInstance(equipmentType) as EquipmentSingle;
            }
            else
            {
                return null;
            }
        }

        //private  generateQuality
    }
}
