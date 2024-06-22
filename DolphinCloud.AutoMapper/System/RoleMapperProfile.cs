using AutoMapper;
using DolphinCloud.DataEntity.System;
using DolphinCloud.DataModel.System.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.AutoMapper.System
{
    /// <summary>
    /// 角色映射配置
    /// </summary>
    public class RoleMapperProfile: Profile
    {
        public RoleMapperProfile()
        {
            CreateMap<RoleDataViewModel, RoleInfo>().ReverseMap();
            CreateMap<RoleCreateDataModel, RoleInfo>().ReverseMap();
            CreateMap<RoleModifyDataModel, RoleInfo>().ReverseMap();
            CreateMap<RolePagination, RoleInfo>().ReverseMap();
        }
    }
}
