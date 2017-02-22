using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eT.Model
{
    /// <summary>
    /// 分页条件、统计-实体类
    /// </summary>
    public class DbQueryResult
    {
        public object Data { get; set; }

        public int TotalRecords { get; set; }

        public int TotalNumber1 { get; set; }
        public int TotalNumber2 { get; set; }
        public int TotalNumber3 { get; set; }
        public int TotalNumber4 { get; set; }
        public int TotalNumber5 { get; set; }

        public decimal TotalMoney1 { get; set; }
        public decimal TotalMoney2 { get; set; }
        public decimal TotalMoney3 { get; set; }
        public decimal TotalMoney4 { get; set; }
        public decimal TotalMoney5 { get; set; }
    }
}
