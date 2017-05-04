using Items.GNRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items.Attr
{
    public class Accuracy:Attribute
    {
        public  Accuracy(EnvironmentVariable ev):base(ev)
        {
            this.ev = ev;
        }

        public override void Execute()
        {
            int affixLevel = ev.ilvl / 10;
            AffixName = Enum.GetName(typeof(AccuracyAffix), ((AccuracyAffix)affixLevel));
            minValue = affixLevel * 10;
            maxValue = affixLevel * 10 * 2;
            Random rd = new Random();
            trueValue = rd.Next(minValue, maxValue);
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
