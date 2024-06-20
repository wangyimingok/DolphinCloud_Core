using Microsoft.AspNetCore.Authorization;

namespace DolphinCloud.OMS.WebApplication.Initialization.CustomizeAuthen
{
    /// <summary>
    /// 自定义验证器
    /// </summary>
    public class CustomizeRequirement: IAuthorizationRequirement
    {
        /// <summary>
        /// 需求名称
        /// </summary>
        public string RequirementName { get;  }
        /// <summary>
        /// 客户验证器
        /// </summary>
        public CustomizeRequirement(string requirementName)
        {
            RequirementName= requirementName;
        }
    }
}
