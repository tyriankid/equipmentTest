using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equipment.Attr
{
    public class Intelligence:Attribute
    {
        private readonly AttributeTypes _attrType = AttributeTypes.Intelligence;

        public  Intelligence(int level):base(level)
        {

        }

        /// <summary>
        /// 智力属性词缀枚举,枚举value对应词缀等级
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
    }
}
