using DolphinCloud.Common.Result;
using DolphinCloud.DataModel.System.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataInterFace.System
{
    /// <summary>
    /// 用户数据接口
    /// </summary>
    public interface IUserDataInterFace
    {
        /// <summary>
        /// 生成管理员账号
        /// </summary>
        /// <returns></returns>
        Task GenerateAdmin();

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        Task<OperationMessage> CreateUser(UserCreateDataModel dataModel);
    }
}
