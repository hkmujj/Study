using System;
using CommonUtil.Util;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.Config;
using Subway.TCMS.Infrasturcture.Model.Constance;

namespace Subway.TCMS.Infrasturcture.Service
{
    /// <summary>
    /// 项目描述文件服务
    /// </summary>
    public class ProjectDecriptionService : IProjectDecriptionService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">车辆屏类型</param>
        public ProjectDecriptionService(TCMSType type)
        {
            Type = type;
        }

        /// <summary>
        /// 车辆屏类型
        /// </summary>
        public TCMSType Type { get; private set; }

        /// <summary>
        /// 输入Bool接口描述字典
        /// </summary>
        public IReadOnlyDictionary<string, int> InBoolDictionary { get; private set; }

        /// <summary>
        /// 输入Float接口描述字典
        /// </summary>
        public IReadOnlyDictionary<string, int> InFloatDictionary { get; private set; }

        /// <summary>
        /// 输出Bool接口描述字典
        /// </summary>
        public IReadOnlyDictionary<string, int> OutBoolDictionary { get; private set; }

        /// <summary>
        /// 输出Float接口描述字典
        /// </summary>
        public IReadOnlyDictionary<string, int> OutFloatDictionary { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="config"></param>
        public void Initaliza(IProjectIndexDescriptionConfig config)
        {
            if (config == null)
            {
                AppLog.Error("{0}描述文件信息为空,请检查描述文件信息！", Type);
                return;
            }

            InBoolDictionary = config.InFloatDescriptionDictionary;
            InFloatDictionary = config.InFloatDescriptionDictionary;
            OutBoolDictionary = config.OutBoolDescriptionDictionary;
            OutFloatDictionary = config.OutFloatDescriptionDictionary;
            AppLog.Info("{0}描述文件加载完成！", this.Type);
        }
    }
}