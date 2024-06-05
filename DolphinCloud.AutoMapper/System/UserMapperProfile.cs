using AutoMapper;
using DolphinCloud.DataEntity.System;
using DolphinCloud.DataModel.System.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.AutoMapper.System
{
    /// <summary>
    /// 用户映射配置
    /// </summary>
    public class UserMapperProfile: Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserDataViewModel, UserInfo>().ReverseMap();
            CreateMap<UserCreateDataModel, UserInfo>().ReverseMap();
        }
    }
}
