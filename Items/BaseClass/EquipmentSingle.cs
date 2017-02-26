using Items.BaseClass;
using Items.GNRT;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Items
{
    public class EquipmentSingle:Item
    {
        protected string name;
        protected string affixName;
        protected Quality quality;
        protected IList<Attribute> attrList;
        
        /// <summary>
        /// 词缀集合
        /// </summary>
        public IList<Attribute> AttrList = new List<Attribute>();
        /// <summary>
        /// 装备名
        /// </summary>
        public string Name { get; set; }

        public override void Execute()
        {
            


            MessageBox.Show("Use Weapon  " + name+" affix count "+AttrList.Count);
        }

        public EquipmentSingle(EnvironmentVariable ev)
        {
            /*
             词缀的选择和魔法/稀有物品的选择规律是一样的，但在词缀的数量上，存在一些差异，主要是由成品的ilvl决定的，ilvl决定了成品上出现词缀数目的最小值，
             最大值固定为4：ilvl在71~99之间时，词缀数目必定是4个；ilvl在51~70之间时，词缀数目为3~4个；ilvl在31~50之间时，词缀数目为2~4个；ilvl小于等于30时，词缀数为1~4个。
             */
            Random rd = new Random();
            int affix_count_min = 0;//词缀数量最小值
            int affix_count_max = 0;//词缀数量最大值
            if (ev.ilvl >=71 && ev.ilvl <= 99)
            {
                affix_count_min = 4;
                affix_count_max = 4;
            }
            else if (ev.ilvl >= 51 && ev.ilvl <= 70)
            {
                affix_count_min = 3;
                affix_count_max = 4;
            }
            else if (ev.ilvl >= 31 && ev.ilvl <= 59)
            {
                affix_count_min = 2;
                affix_count_max = 4;
            }
            else if (ev.ilvl <= 30)
            {
                affix_count_min = 1;
                affix_count_max = 4;
            }

            
            for(int i = 0; i < rd.Next(affix_count_min, affix_count_max); i++){
                AttrList.Add(Generator.generateAttr(ev));
                //AttrList[i].Execute();
            }

            //决定品质
            quality = Generator.generateQuality();
        }



        public enum Quality
        {
            普通,
            扩展,
            精华
        }

        /*
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
        */




        public enum EquipmentTypeEnum
        {
            Helmet,
            Boot,
            Glove,
            Armor,
        }
    }
}
