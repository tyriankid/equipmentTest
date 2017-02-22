using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equipment
{
    public class EquipmentSingle
    {
        private int _equipmentLevel;
        public int equipmentLevel
        {
            get { return _equipmentLevel; }
            set { _equipmentLevel=value; }
        }
        /// <summary>
        /// 词缀集合
        /// </summary>
        public IList<Affix> affixList { get; set; }
        /// <summary>
        /// 装备名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 装备类型
        /// </summary>
        private EquipmentTypeEnum _EquipmentType;
        public EquipmentTypeEnum EquipmentType 
        {
            get { return _EquipmentType; }
            set {
                _EquipmentType=value;
            }
        }

        public EquipmentSingle(int level,EquipmentTypeEnum type)
        {
            this.equipmentLevel = level;
            this.EquipmentType = type;
            this.affixList =new List<Affix>();
            //新建词缀
            for (int i = 0; i < this.equipmentLevel; i++)
            {
                Affix affix = new Affix(level)
                {
                    affixLevel = equipmentLevel
                };
                affixList.Add(affix);
            }

            name = getName();
        }

    



        //根据词缀和装备类型生成装备名
        private string getName()
        {
            string str = "";
            //根据词缀名拼接
            for (int i = 0; i < affixList.Count; i++)
            {
                if (i == 0)
                    str += affixList[i].affixName + "之 ";
                else if(i==1)
                    str += affixList[i].affixName + " ";
            }
            //根据装备等级拼接
            if(equipmentLevel>0 && equipmentLevel<3){
                str+="粗糙";
            }
            else if(equipmentLevel>=3 && equipmentLevel<5){
                str+="精緻";
            }
            else if(equipmentLevel>=5 && equipmentLevel<8){
                str+="極品";
            }


            //根据装备类型拼接
            switch (EquipmentType)
            {
                case EquipmentTypeEnum.Helmet:
                    str += "頭盔";
                    break;
                case EquipmentTypeEnum.Boot:
                    str += "戰靴";
                    break;
                case EquipmentTypeEnum.Glove:
                    str += "護手";
                    break;
                case EquipmentTypeEnum.Armor:
                    str += "胸甲";
                    break;
            }


            return str;
        }

        public enum EquipmentTypeEnum
        {
            Helmet,
            Boot,
            Glove,
            Armor,
        }
    }
}
