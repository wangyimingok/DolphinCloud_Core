using Microsoft.AspNetCore.Authorization;

namespace DolphinCloud.OMS.WebApplication.Initialization
{
    public class AdminAreaRequirement: IAuthorizationRequirement
    {
        public string AreaName { get; }

        public AdminAreaRequirement(string areaName)
        {
            AreaName = areaName;
        }
    }
}
