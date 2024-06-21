using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.Base
{
    /// <summary>
    /// 选项数据模型
    /// 用于前端下拉框等带选项的控件
    /// </summary>
    public class OptionDataModel
    {
        /// <summary>
        /// 选项名称
        /// </summary>
        [JsonProperty("optionName")]
        public string OptionName { get; set; }

        /// <summary>
        /// 选项值
        /// </summary>
        [JsonProperty("optionValue")]
        public string OptionValue { get; set; }
    }
}
