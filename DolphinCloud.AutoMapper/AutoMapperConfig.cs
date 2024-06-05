using AutoMapper;
using DolphinCloud.AutoMapper.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.AutoMapper
{
    /// <summary>
    ///     AutoMapper 配置
    ///     所有实体类与数据模型的映射关系都在这个类里配置
    ///     注意模块划分 可采用一个模块一个配置类或者一个业务领域一个配置类
    /// </summary>
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            //创建AutoMapperConfiguration, 提供静态方法Configure，一次加载所有层中Profile定义 
            //MapperConfiguration实例可以静态存储在一个静态字段中，也可以存储在一个依赖注入容器中。 一旦创建，不能更改/修改。
            return new MapperConfiguration(cfg =>
            {
                //菜单信息映射配置
                cfg.AddProfile<MenuMapperProfile>();
                //用户信息配置映射
                cfg.AddProfile<UserMapperProfile>();
                
            });
        }
    }
}
