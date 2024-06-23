using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.System.Role
{
    public class RoleAuthorityDataModel
    {
        /// <summary>
        /// 角色主键
        /// </summary>
        public int RoleID{get;set;}

        /// <summary>
        /// 权限主键
        /// </summary>
        public int PermissionID { get; set; }
    }
}
