using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equipment
{


    /// <summary>
    /// 词缀
    /// </summary>
    public class Affix 
    {
        private int _affixLevel;
        public int affixLevel 
        { 
            get {return _affixLevel;}
            set { _affixLevel = value; } 
        }

        public Attribute attribute{get;set;}//属性

        public string affixName { get; set; }//词缀名

        public Affix(int level)
        {
            attribute = new Attribute(level);

            switch (attribute.AttributeType)
            {
                case "Intelligence":
                    affixName = Enum.GetName(typeof(IntelligenceAffix),attribute.level);
                    break;
                case "Accuracy":
                    affixName = Enum.GetName(typeof(AccuracyAffix), attribute.level);
                    break;
            }


        }

        /// <summary>
        /// 智力属性词缀枚举
        /// </summary>
        public enum IntelligenceAffix
        {
            蜥蜴 = 1,
            蛇 = 2,
            海蛇 = 3,
            蜉蝣 = 4,
            龍 = 5,
            維特 = 6,
            巨飛龍 = 7,
            巴哈姆特 = 8,
        }

        /// <summary>
        /// 准确率属性词缀枚举
        /// </summary>
        public enum AccuracyAffix
        {
            青銅 = 1,
            鐵 = 2,
            鋼 = 3,
            銀 = 4,
            金 = 5,
            白金 = 6,
            隕鐵 = 7,
            怪異 = 8,
        }


    }
}
