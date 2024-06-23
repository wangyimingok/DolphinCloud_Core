using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.Base
{
    /// <summary>
    /// 树控件数据模型
    /// </summary>
    public class LayuiTreeDataModel
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        [JsonProperty("id")]
        public int TreeID { get; set; }

        /// <summary>
        /// 节点标题
        /// </summary>
        [JsonProperty("title")]
        public string NodeName { get; set; }

        /// <summary>
        /// 子节点
        /// 支持设定属性选项同父节点
        /// </summary>
        [JsonProperty("children")]
        public List<LayuiTreeDataModel> Children { get; set; }
    }
}
