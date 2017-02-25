using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Items
{
    public class EquipmentSingle:Item
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

        public override void Execute()
        {
            MessageBox.Show("Use Weapon  " + name);
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
