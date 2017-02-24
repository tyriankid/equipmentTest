using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equipment.Attr
{
    public class Accuracy:Attribute
    {
        private readonly AttributeTypes _attrType = AttributeTypes.Accuracy;

        public  Accuracy(int level):base(level)
        {

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
