using Items.Attr;
using Items.BaseClass;
using Items.Eqpt;
using Items.Misc;
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
        public static Dictionary<string, Type> miscClassesDict = new Dictionary<string, Type>();
        

        

        /// <summary>
        /// item 子类生成基数
        /// </summary>
        private static Dictionary<Type, int> itemGeneratBaseNumber = new Dictionary<Type, int>()
        {
            //装备类
            { typeof(Armor),  50},
            { typeof(Helmet),  5},
            { typeof(Boot),  5 },
            { typeof(Glove),  5},
            //杂项类
            { typeof(Rune),1},
            { typeof(Gold),2 },
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
                    if (types[j].IsSubclassOf(typeof(EquipmentSingle)))//将装备类丢入dict
                    {
                        //MessageBox.Show("classname " + className);
                        equipmentClassesDict.Add(className, types[j]);
                    }
                    else if (types[j].IsSubclassOf(typeof(Miscs)))//将杂项类丢入dict
                    {
                        miscClassesDict.Add(className, types[j]);
                    }
                }
            }
        }

        


        public static EquipmentSingle generateEquipment(EnvironmentVariable ev)
        {
            //将类型基数取和
            int baseNumTotal = 0;
            //新建一个数组,存入每个装备基数对应的key,例如 基数为5对应索引0-5的位置 用于根据基数随机出出现的类型
            ArrayList baseNumberList = new ArrayList();
            foreach (KeyValuePair<Type, int> kvp in itemGeneratBaseNumber)
            {
                baseNumTotal += kvp.Value;
                for (int i = 0; i < kvp.Value; i++)
                {
                    baseNumberList.Add(kvp.Key);
                }
            }            
            //根据基数总数为随机数上限,然后在数组中随机取值,对应的类型就是当前要生成的装备类型
            Random rd = new Random();
            return Activator.CreateInstance((Type)baseNumberList[rd.Next(baseNumTotal)],ev) as EquipmentSingle;

            /*
            //将得到的类型进行switch,以返回相应的装备类型
            string equipmentTypeName = ((EquipmentSingle.EquipmentTypeEnum)baseNumberList[rd.Next(baseNumTotal)]).ToString();
            //根据枚举名获得对应类型的装备
            Type equipmentType = null;
            if (equipmentClassesDict.TryGetValue(typeof(baseNumberList[rd.Next(baseNumTotal)]), out equipmentType))
            {
                return Activator.CreateInstance(equipmentType) as EquipmentSingle;
            }
            else
            {
                return null;
            }
            */
        }



        public static Miscs generateMiscs()
        {
            //将类型基数取和
            int baseNumTotal = 0;
            //新建一个数组,存入每个装备基数对应的key,例如 基数为5对应索引0-5的位置 用于根据基数随机出出现的类型
            ArrayList baseNumberList = new ArrayList();
            foreach (KeyValuePair<Type, int> kvp in itemGeneratBaseNumber)
            {
                baseNumTotal += kvp.Value;
                for (int i = 0; i < kvp.Value; i++)
                {
                    baseNumberList.Add(kvp.Key);
                }
            }
            //根据基数总数为随机数上限,然后在数组中随机取值,对应的类型就是当前要生成的装备类型
            Random rd = new Random();
            return Activator.CreateInstance((Type)baseNumberList[rd.Next(baseNumTotal+1)]) as Miscs;
        }

        /// <summary>
        /// Attribute 子类生成基数
        /// </summary>
        private static Dictionary<Type, int> attrGeneratBaseNumber = new Dictionary<Type, int>()
        {
            //装备类
            { typeof(Accuracy),  50},
            { typeof(Intelligence),  5},
        };


        public static Attribute generateAttr(EnvironmentVariable ev)
        {
            //将类型基数取和
            int baseNumTotal = 0;
            //新建一个数组,存入每个装备基数对应的key,例如 基数为5对应索引0-5的位置 用于根据基数随机出出现的类型
            ArrayList baseNumberList = new ArrayList();
            foreach (KeyValuePair<Type, int> kvp in attrGeneratBaseNumber)
            {
                baseNumTotal += kvp.Value;
                for (int i = 0; i < kvp.Value; i++)
                {
                    baseNumberList.Add(kvp.Key);
                }
            }
            //根据基数总数为随机数上限,然后在数组中随机取值,对应的类型就是当前要生成的装备类型
            Random rd = new Random();
            return Activator.CreateInstance((Type)baseNumberList[rd.Next(baseNumTotal+1)],ev) as Attribute;

        }


        /// <summary>
        /// EquipmentSingle.Quality 装备品质生成基数
        /// </summary>
        private static Dictionary<EquipmentSingle.Quality, int> eqptQualityGeneratBaseNumber = new Dictionary<EquipmentSingle.Quality, int>()
        {
            //装备类
            { EquipmentSingle.Quality.普通,  20},
            { EquipmentSingle.Quality.扩展,  15},
            { EquipmentSingle.Quality.精华,  1},
        };

        public static EquipmentSingle.Quality generateQuality()
        {
            //将类型基数取和
            int baseNumTotal = 0;
            //新建一个数组,存入每个装备基数对应的key,例如 基数为5对应索引0-5的位置 用于根据基数随机出出现的类型
            ArrayList baseNumberList = new ArrayList();
            foreach (KeyValuePair<EquipmentSingle.Quality, int> kvp in eqptQualityGeneratBaseNumber)
            {
                baseNumTotal += kvp.Value;
                for (int i = 0; i < kvp.Value; i++)
                {
                    baseNumberList.Add(kvp.Key);
                }
            }
            //根据基数总数为随机数上限,然后在数组中随机取值,对应的类型就是当前要生成的装备类型
            Random rd = new Random();
            return (EquipmentSingle.Quality)baseNumberList[rd.Next(baseNumTotal+1)] ;

        }

        //private  generateQuality
    }
}
