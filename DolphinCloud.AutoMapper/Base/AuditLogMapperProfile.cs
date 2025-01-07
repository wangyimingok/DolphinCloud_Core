using AutoMapper;
using DolphinCloud.DataEntity.Base;
using DolphinCloud.DataModel.Base.AudiotLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.AutoMapper.Base
{
    /// <summary>
    /// 审计日志映射配置
    /// </summary>
    public class AuditLogMapperProfile : Profile
    {
        public AuditLogMapperProfile()
        {
            CreateMap<AuditLogInfo, AuditLogCreateDataModel>().ReverseMap();
            CreateMap<AuditLogInfo, AuditLogDataViewModel>().ReverseMap();
            CreateMap<AuditLogInfo, AuditLogDataModel>().ReverseMap();
        }
    }
}
