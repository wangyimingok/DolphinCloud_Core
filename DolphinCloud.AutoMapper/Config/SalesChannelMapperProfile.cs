using AutoMapper;
using DolphinCloud.DataEntity.Config;
using DolphinCloud.DataModel.Config.SalesChannel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.AutoMapper.Config
{
    /// <summary>
    /// 销售渠道映射配置
    /// </summary>
    public class SalesChannelMapperProfile : Profile
    {
        public SalesChannelMapperProfile()
        {
            CreateMap<SalesChannelInfo, SalesChannelCreateDataModel>().ReverseMap();
            CreateMap<SalesChannelInfo, SalesChannelDataViewModel>().ReverseMap();
            CreateMap<SalesChannelInfo, SalesChannelUpdateDataModel>().ReverseMap();
        }
    }
}
