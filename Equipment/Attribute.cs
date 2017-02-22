using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equipment
{
    /// <summary>
    /// 属性
    /// </summary>
    public class Attribute
    {
        private int _level;
        public int level 
        {
            get { return _level;}
            set { _level=value;}
        }//属性等级

        public int minValue { get; set; }//属性最小值

        public int maxValue { get; set; }//属性最大值

        public string AttributeType { get; set; }//属性类型

        public Attribute(int level)
        {
            this.level = level;
            //随机取得属性类型

            AttributeTypes[] types = Enum.GetValues(typeof(AttributeTypes)) as AttributeTypes[];
            Random random = new Random();
            



            //AttributeType = attributes[rd.Next(attributes.Count)].ToString();
            minValue = this.getMinValueByLevel();
            maxValue = this.getMaxValueByLevel();
            //根据属性等级
        }



        /// <summary>
        /// 根据属性等级计算出最小值
        /// </summary>
        private int getMinValueByLevel()
        {
            return level * 10 + 1;
        }

        /// <summary>
        /// 根据属性等级计算出最大值
        /// </summary>
        private int getMaxValueByLevel()
        {
            return level * 10 * 2 + 1 + level*level;
        }


        public enum AttributeTypes
        {
            Strength,
            Intelligence,
            Endurance,
            Agile,
            Accuracy,
        }

    }



    
}
