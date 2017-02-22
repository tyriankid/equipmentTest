using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eT.Model
{
    /// <summary>
    /// 分页-实体类
    /// </summary>
    public class Pagination
    {
        public Pagination()
        {
            this.IsCount = true;
            this.PageSize = 10;
        }

        public bool IsCount { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string SortBy { get; set; }

        public SortAction SortOrder { get; set; }
    }
}
