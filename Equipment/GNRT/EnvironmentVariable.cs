using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items.GNRT
{
    /// <summary>
    /// 环境变量
    /// </summary>
    public class EnvironmentVariable
    {
        /// <summary>
        /// 场景等级
        /// </summary>
        public float areaLevel { get; set; }
        /// <summary>
        /// 怪物等级
        /// </summary>
        public float monLevel { get; set; }
        /// <summary>
        /// 魔法装备获取基数
        /// </summary>
        public int magicFind { get; set; }
    }
}
