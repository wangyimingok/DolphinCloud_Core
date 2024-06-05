using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Result
{
    /// <summary>
    /// 分页结果集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> where T : class
    {
        public PagedList()
        {
            Data = new List<T>();
        }

        /// <summary>
        /// 结果集
        /// </summary>
        public List<T> Data { get; }
        /// <summary>
        /// 数据总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 每页数据条数
        /// </summary>
        public int PageSize { get; set; }
    }
}
