using Items.GNRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items.Attr
{
    public class Intelligence:Attribute
    {

        public Intelligence(EnvironmentVariable ev):base(ev)
        {
            int affixLevel = ev.ilvl / 10;
            AffixName = Enum.GetName(typeof(IntelligenceAffix), ((IntelligenceAffix)affixLevel));
            minValue = affixLevel * 10;
            maxValue = affixLevel * 10 * 2;
            Random rd = new Random();
            trueValue = rd.Next(minValue, maxValue);
        }

        public override void Execute()
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
