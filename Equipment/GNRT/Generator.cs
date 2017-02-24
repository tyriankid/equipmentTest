using Equipment.Eqpt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Equipment.GNRT
{
    /// <summary>
    /// 装备生成器
    /// </summary>
    public class Generator
    {
        /// <summary>
        /// 环境变量
        /// </summary>
        private EnvironmentVariable ev { get; set; }

        /// <summary>
        /// 装备生成基数键值对
        /// </summary>
        private Dictionary<EquipmentSingle.EquipmentTypeEnum, int> eqptGeneratBaseNumber {get; set; }


        public Generator()
        {
            Dictionary<EquipmentSingle.EquipmentTypeEnum, int> eqptGeneratBaseNumber = new Dictionary<EquipmentSingle.EquipmentTypeEnum, int>();
            eqptGeneratBaseNumber.Add(EquipmentSingle.EquipmentTypeEnum.Armor,  5);
            eqptGeneratBaseNumber.Add(EquipmentSingle.EquipmentTypeEnum.Helmet, 5);
            eqptGeneratBaseNumber.Add(EquipmentSingle.EquipmentTypeEnum.Boot, 5);
            eqptGeneratBaseNumber.Add(EquipmentSingle.EquipmentTypeEnum.Glove, 5);
        }


        private object generateEqpt(){
            object eqpttype=new object();
            object resulteqpttype = new object();
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
            switch ((EquipmentSingle.EquipmentTypeEnum)baseNumberList[rd.Next(baseNumTotal)])
            {
                case EquipmentSingle.EquipmentTypeEnum.Armor:
                    Armor sdf = new Armor();
                    eqpttype = typeof(Equipment.Eqpt.Armor);
                    resulteqpttype = (Armor)Activator.CreateInstance((Type)eqpttype);
                    break;
                default : //杂项物品

                    break;
            }


            return resulteqpttype;
        }

        //private  generateQuality
    }
}
