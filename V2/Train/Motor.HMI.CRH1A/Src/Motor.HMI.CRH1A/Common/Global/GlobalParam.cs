using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CommonUtil.Util;
using Motor.HMI.CRH1A.Common.Train;

namespace Motor.HMI.CRH1A.Common.Global
{
    internal class GlobalParam
    {
        private static GlobalParam _instance;
        private bool m_IsReversalTrain;

        public static GlobalParam Instance
        {
            get { return _instance ?? (_instance = new GlobalParam()); }
        }

        private GlobalParam()
        {
            m_IsReversalTrain = false;
            TrainInfo = new TrainInfo();
        }

        /// <summary>
        /// 根配置文件路径 
        /// </summary>
        public static string RootConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");

        public void Init()
        {

        }

        public void Refresh()
        {
            var viewClass = GlobalInfo.Instance;
            var reverState = viewClass.BoolList[viewClass.UIObj.InBoolList[0]];
            if (reverState != IsReversalTrain)
            {
                LogMgr.Debug("Reversal ......");
                IsReversalTrain = reverState;
            }

            TrainInfo.Speed = GlobalInfo.Instance.Crh1AConfig.AdaptConfig.CurrentSpeedCoefficient*viewClass.FloatList[viewClass.UIObj.InFloatList[0]];

            if (viewClass.BoolList[viewClass.UIObj.InBoolList[1]])
            {
                TrainInfo.DriverLocation = DriverLocation.Left;
            }
            else if (viewClass.BoolList[viewClass.UIObj.InBoolList[2]])
            {
                TrainInfo.DriverLocation = DriverLocation.Right;
            }
            else
            {
                TrainInfo.DriverLocation = DriverLocation.Unkown;
            }
        }

        /// <summary>
        /// 是否反转车辆, 默认不反, 即最左边为0号车
        /// </summary>
        public bool IsReversalTrain
        {
            set
            {
                m_IsReversalTrain = value;
                HandleUtil.OnHandle(GlobalEvent.Instance.ReversalChanged, this, null);
            }
            get { return m_IsReversalTrain; }
        }

        /// <summary>
        /// 车厢个数
        /// </summary>
        public const int CarCount = 8;


        public TrainInfo TrainInfo { private set; get; }
    }
}
