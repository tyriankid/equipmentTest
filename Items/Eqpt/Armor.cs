using Items.GNRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items.Eqpt
{
    public class Armor:EquipmentSingle
    {
        public Armor(EnvironmentVariable ev):base(ev)
        {
            switch (quality)
            {
                case EquipmentSingle.Quality.普通:
                    NormalArmor[] normalArmors = Enum.GetValues(typeof(NormalArmor)) as NormalArmor[];
                    Random random = new Random();
                    name = Enum.GetName(typeof(NormalArmor),normalArmors[random.Next(0, normalArmors.Length)]);
                    break;
                case EquipmentSingle.Quality.扩展:
                    ExtendArmor[] ExtendArmors = Enum.GetValues(typeof(ExtendArmor)) as ExtendArmor[];
                    Random Extendrandom = new Random();
                    name = Enum.GetName(typeof(ExtendArmor), ExtendArmors[Extendrandom.Next(0, ExtendArmors.Length)]);
                    break;
            }
            
        }
        

        public enum NormalArmor
        {
            布甲=0,
            皮甲=1,
            硬皮甲=2,
            鑲嵌甲=3,
            鎖環甲=4,
            鱗甲=5,
            鎖子甲=6,
            胸甲=7,
            板甲=8,
            鎧甲=9,
            實戰鎧甲=10,
            哥德戰甲=11,
            高級戰甲=12,
            古代裝甲=13,
            輕型裝甲=14,
        }

        public enum ExtendArmor
        {
            鬼魂戰甲=0,
            海蛇皮甲=1,
            魔皮戰甲=2,
            盤繞戰甲=3,
            連扣戰甲=4,
            提格萊特戰甲=5,
            鐵網戰甲=6,
            護胸甲=7,
            羅瑟戰甲=8,
            聖堂武士外袍=9,
            鯊齒戰甲=10,
            凸紋戰甲=11,
            混沌戰甲=12,
            華麗戰甲=13,
            法師鎧甲=14,
        }
    }
}
