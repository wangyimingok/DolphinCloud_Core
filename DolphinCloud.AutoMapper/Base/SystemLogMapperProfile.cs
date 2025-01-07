using AutoMapper;
using DolphinCloud.DataEntity.System;
using DolphinCloud.DataModel.Base.SystemLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.AutoMapper.Base
{
    /// <summary>
    /// 系统日志映射配置
    /// </summary>
    public class SystemLogMapperProfile : Profile
    {
        public SystemLogMapperProfile()
        {
            CreateMap<SystemLogInfo, SystemLogDataViewModel>().ReverseMap();
        }
    }
}
