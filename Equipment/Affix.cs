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





    }
}
