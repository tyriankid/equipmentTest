using Items.Attr;
using Items.GNRT;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items
{
    /// <summary>
    /// 属性
    /// </summary>
    public abstract class Attribute
    {
        /// <summary>
        /// 环境变量
        /// </summary>
        public EnvironmentVariable ev { get; set; }

        public int minValue { get; set; }//属性最小值

        public int maxValue { get; set; }//属性最大值

        public int trueValue { get; set; }//实际值

        public string AffixName { get; set; }
       

        public Attribute(EnvironmentVariable ev)
        {
            this.ev = ev;//赋予环境变量

           
        }

        public abstract void Execute();
        
        

        /// <summary>
        /// 可能出现在的装备类型
        /// </summary>
        public Type[] showInEquipmentType { get; set; }




    }



    
}
