using AutoMapper;
using DolphinCloud.DataEntity.System;
using DolphinCloud.DataModel.System.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.AutoMapper.System
{
    /// <summary>
    /// 菜单映射配置
    /// </summary>
    public class MenuMapperProfile : Profile
    {
        public MenuMapperProfile()
        {
            CreateMap<MenuDataViewModel, MenuInfo>().ReverseMap();
            CreateMap<MenuCreateDataModel, MenuInfo>().ReverseMap();
            CreateMap<MenuModifyDataModel, MenuInfo>().ReverseMap();
            CreateMap<SideBarNavDataModel, MenuInfo>()
                .ForMember(entity => entity.ChildMenuData, dest => dest.MapFrom(model => model.childMenuData))
                .ReverseMap();
        }
    }
}
