﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Snowflake
{
    /// <summary>
    /// 配置引导
    /// </summary>
    public class IdHelperBootstrapper
    {
        /// <summary>
        /// 机器Id
        /// </summary>
        protected long _worderId { get; set; }

        /// <summary>
        /// 获取机器Id
        /// </summary>
        /// <returns></returns>
        protected virtual long GetWorkerId()
        {
            return _worderId;
        }

        /// <summary>
        /// 是否可用
        /// </summary>
        /// <returns></returns>
        public virtual bool Available()
        {
            return true;
        }

        /// <summary>
        /// 设置机器Id
        /// </summary>
        /// <param name="workderId">机器Id</param>
        /// <returns></returns>
        public IdHelperBootstrapper SetWorkderId(long workderId)
        {
            _worderId = workderId;

            return this;
        }

        /// <summary>
        /// 完成配置
        /// </summary>
        public void Boot()
        {
            IdHelper.IdWorker = new IdWorker(GetWorkerId());
            IdHelper.IdHelperBootstrapper = this;
        }
    }
}
