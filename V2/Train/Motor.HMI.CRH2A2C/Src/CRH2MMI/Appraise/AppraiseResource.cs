using System.Collections.Generic;
using System.Linq;
using CommonUtil.Util;
using CRH2MMI.Common.Config;
using CRH2MMI.Common.Global;

namespace CRH2MMI.Appraise
{
    class AppraiseResource
    {
        //视图名称和发送接口映射
        private Dictionary<ViewConfig, int> m_ViewConfigIndexDictionary;
        //需要发送评价信息的视图名称
        private readonly ViewConfig[] viewCellection;
        public AppraiseResource()
        {
            viewCellection = new[]
                              {
                                ViewConfig.DriveState, 
                                ViewConfig.CarInfo,
                                ViewConfig.FaultView,
                                ViewConfig.Marsh, 
                                ViewConfig.JackInfo,
                                ViewConfig.Delivery,
                                ViewConfig.RemovalState,
                                ViewConfig.BrakeInfo, 
                                ViewConfig.Tow1, 
                                ViewConfig.Tow2, 
                                ViewConfig.AcumuElec,
                                ViewConfig.Racing, 
                                ViewConfig.PowerVoltage,
                                ViewConfig.PowerClassfy,
                                ViewConfig.DoorInfo,
                                ViewConfig.Trans,
                                ViewConfig.TNSet,
                                ViewConfig.MonitorSet, 
                                ViewConfig.Axis, 
                                ViewConfig.Telecontr,
                                ViewConfig.BreakLocked,
                                ViewConfig.CurrentStationSet,
                                ViewConfig.TNChange, 
                                ViewConfig.VigilantView,
                                ViewConfig.BPRescueView,
                              };
        }

        /// <summary>
        /// 获取视图发送评价接口
        /// </summary>
        /// <param name="viewConfig">视图名称</param>
        /// <returns>返回-1 发送的视图不在映射表中</returns>
        public int GetIndexOf(ViewConfig viewConfig)
        {
            if (m_ViewConfigIndexDictionary==null)
            {
                m_ViewConfigIndexDictionary = viewCellection.ToDictionary(s => s, 
                s =>(int)GlobalResource.Instance.GetOutBoolIndex(EnumUtil.GetDescription(s).FirstOrDefault()));
            }
            if (!m_ViewConfigIndexDictionary.ContainsKey(viewConfig))
            {
                return -1;
            }
            return m_ViewConfigIndexDictionary[viewConfig];
        }
    }
}
