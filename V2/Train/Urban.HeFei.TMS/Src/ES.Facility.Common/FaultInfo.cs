using System;

namespace ES.Facility.Common
{

    /// <summary>
    /// 功能描述：故障
    /// 创建人：唐林
    /// 创建时间：2014-07-15
    /// </summary>
    public class FaultInfo
    {
        /// <summary>
        /// 读取或设置故障逻辑属性
        /// </summary>
        public int Logic { get; set; }
        /// <summary>
        /// 读取或设置故障等级属性
        /// </summary>
        public int Grade { get; set; }
        /// <summary>
        /// 读取或设置故障代码属性
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 读取或设置故障内容属性
        /// </summary>
        public string Display { get; set; }
        /// <summary>
        /// 读取或设置故障开始时间属性
        /// </summary>
        public string StartTime
        {
            get { return m_StartTime; }
            set { m_StartTime = value; }
        }
        private string m_StartTime = "-";
        /// <summary>
        /// 读取或设置故障结束时间属性
        /// </summary>
        public string OverTime
        {
            get { return m_OverTime; }
            set { m_OverTime = value; }
        }
        private string m_OverTime = "-";

        /// <summary>
        /// 读取故障的时间（包括开始时间与结束时间）
        /// </summary>
        public string Time
        {
            get { return m_StartTime + "\n" + m_OverTime; }
        }
    }

    /// <summary>
    /// 功能描述：故障信息
    /// 创建人：lih
    /// 创建时间：2015-07-15
    /// </summary>
    public class FaultInfoTms
    {
        /// <summary>
        /// 读取或设置故障车号
        /// </summary>
        public string CoachNo { get; set; }

        /// <summary>
        /// 读取或设置故障名称
        /// </summary>
        public string FaultName { get; set; }

        /// <summary>
        /// 读取或设置故障代码
        /// </summary>
        public string FaultCode { get; set; }

        /// <summary>
        /// 读取或设置故障等级
        /// </summary>
        public string FaultRank { get; set; }
    }

    /// <summary>
    /// 功能描述：历史故障信息
    /// 创建人：lih
    /// 创建时间：2015-07-15
    /// </summary>
    public class HistoryFaultInfo
    {
        /// <summary>
        /// 读取或设置历史故障车号
        /// </summary>
        public string CoachNo { get; set; }

        /// <summary>
        /// 读取或设置历史故障名称
        /// </summary>
        public string FaultName { get; set; }

        /// <summary>
        /// 读取或设置故障开始时间属性
        /// </summary>
        public string StartTime
        {
            get { return m_StartTime; }
            set { m_StartTime = value; }
        }
        private string m_StartTime = "-";
        /// <summary>
        /// 读取或设置故障结束时间属性
        /// </summary>
        public string OverTime
        {
            get { return m_OverTime; }
            set { m_OverTime = value; }
        }
        private string m_OverTime = "-";

        /// <summary>
        /// 读取故障的时间（包括开始时间与结束时间）
        /// </summary>
        public string Time
        {
            get { return m_StartTime + "\n" + m_OverTime; }
        }
    }

    /// <summary>
    /// 软件版本信息
    /// </summary>
    public class SoftwareVersionInfo
    {
        #region 属性
        /// <summary>
        /// 软件名称
        /// </summary>
        public string SoftwareName
        {
            get { return m_SoftwareName; }
            set {m_SoftwareName=value;}
        }
        private string m_SoftwareName = string.Empty;

        /// <summary>
        /// 软件版本1
        /// </summary>
        public string Version1
        {
            get { return m_Version1; }
            set { m_Version1 = value; }
        }
        private string m_Version1 = string.Empty;

        /// <summary>
        /// 软件版本1
        /// </summary>
        public string Version2
        {
            get { return m_Version2; }
            set { m_Version2 = value; }
        }
        private string m_Version2 = string.Empty;

        /// <summary>
        /// 软件版本1
        /// </summary>
        public string Version3
        {
            get { return m_Version3; }
            set { m_Version3 = value; }
        }
        private string m_Version3 = string.Empty;

        /// <summary>
        /// 软件版本1
        /// </summary>
        public string Version4
        {
            get { return m_Version4; }
            set { m_Version4 = value; }
        }
        private string m_Version4 = string.Empty;

        /// <summary>
        /// 软件版本1
        /// </summary>
        public string Version5
        {
            get { return m_Version5; }
            set { m_Version5 = value; }
        }
        private string m_Version5 = string.Empty;

        /// <summary>
        /// 软件版本1
        /// </summary>
        public string Version6
        {
            get { return m_Version6; }
            set { m_Version6 = value; }
        }
        private string m_Version6 = string.Empty;
        #endregion
    }

    /// <summary>
    /// BZDiagnoseInfo
    /// </summary>
    public class BzDiagnoseInfo
    {
        public string FaultInfos { get; set; }
        public string PromptInfos { get;set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SoftwareVersionInfoNew
    {
        #region 属性
        /// <summary>
        /// 软件名称
        /// </summary>
        public string SoftwareName
        {
            get { return m_SoftwareName; }
            set { m_SoftwareName = value; }
        }
        private string m_SoftwareName = string.Empty;

        /// <summary>
        /// 版本信息
        /// </summary>
        public string VersionInfo
        {
            get { return m_VersionInfo; }
            set { m_VersionInfo = value; }
        }
        private string m_VersionInfo = string.Empty;
        #endregion
    }

    /// <summary>
    /// 接口检查
    /// </summary>
    public class InterfaceCheck
    {
        #region 属性
        /// <summary>
        /// 地址
        /// </summary>
        public string IcAddress 
        {
            get
            {
                return m_IcAddress;
            }
            set
            {
                if (m_IcAddress != value)
                {
                    m_IcAddress = value;
                }
            }
        }
        private string m_IcAddress = string.Empty;

        /// <summary>
        /// 信号标识
        /// </summary>
        public string IcSignalFlag
        {
            get { return m_IcsignalFlag; }
            set { m_IcsignalFlag = value; }
        }
        private string m_IcsignalFlag = string.Empty;

        /// <summary>
        /// 值
        /// </summary>
        public string IcValue
        {
            get { return m_IcValue; }
            set { m_IcValue = value; }
        }
        private string m_IcValue = string.Empty;
        #endregion
    }

    /// <summary>
    /// 能耗信息
    /// </summary>
    public class EnergyInfo
    {
        /// <summary>
        /// 能耗类别
        /// </summary>
        public string EnergyStyle
        {
            get { return m_EnergyStyle; }
            set { m_EnergyStyle = value; }
        }
        private string m_EnergyStyle = string.Empty;

        /// <summary>
        /// 第一个月消耗值
        /// </summary>
        public string Month1Value
        {
            get { return m_Month1Value; }
            set { m_Month1Value = value; } 
        }
        private string m_Month1Value = string.Empty;


        /// <summary>
        /// 第二个月消耗值
        /// </summary>
        public string Month2Value
        {
            get { return m_Month2Value; }
            set { m_Month2Value = value; }
        }
        private string m_Month2Value = string.Empty;

        /// <summary>
        /// 第三个月消耗值
        /// </summary>
        public string Month3Value
        {
            get { return m_Month3Value; }
            set { m_Month3Value = value; }
        }
        private string m_Month3Value = string.Empty;

        /// <summary>
        /// 第四个月消耗值
        /// </summary>
        public string Month4Value
        {
            get { return m_Month4Value; }
            set { m_Month4Value = value; }
        }
        private string m_Month4Value = string.Empty;

        /// <summary>
        /// 第五个月消耗值
        /// </summary>
        public string Month5Value
        {
            get { return m_Month5Value; }
            set { m_Month5Value = value; }
        }
        private string m_Month5Value = string.Empty;

        /// <summary>
        /// 第六个月消耗值
        /// </summary>
        public string Month6Value
        {
            get { return m_Month6Value; }
            set { m_Month6Value = value; }
        }
        private string m_Month6Value = string.Empty;

        /// <summary>
        /// 累计
        /// </summary>
        public string RowTotal
        {
            get { return m_RowTotal; }
            set { m_RowTotal = value; }
        }
        private string m_RowTotal = string.Empty;

         
    }

    /// <summary>
    /// 加速度测试
    /// </summary>
    public class AccelerationTestInfo
    {
        #region 属性/变量
        /// <summary>
        /// 类别
        /// </summary>
        public string Style
        {
            get{  return m_Style;}
            set { m_Style = value; }
        }
        private string m_Style = string.Empty;

        /// <summary>
        /// 初速度
        /// </summary>
        public string InitialSpeed
        {
            get { return m_InitialSpeed; }
            set { m_InitialSpeed = value; }
        }
        private string m_InitialSpeed = string.Empty;

        /// <summary>
        /// 末速度
        /// </summary>
        public string EndSpeed
        {
            get { return m_EndSpeed; }
            set { m_EndSpeed = value; }
        }
        private string m_EndSpeed = string.Empty;

        /// <summary>
        /// 距离
        /// </summary>
        public string Distance
        {
            get { return m_Distance; }
            set { m_Distance = value; }
        }
        private string m_Distance = string.Empty;

        /// <summary>
        /// 时间
        /// </summary>
        public string Time
        {
            get { return m_Time; }
            set { m_Time = value; }
        }
        private string m_Time = string.Empty;

        /// <summary>
        /// 加速度
        /// </summary>
        public string Acceleration
        {
            get { return m_Acceleration; }
            set { m_Acceleration = value; }
        }
        private string m_Acceleration = string.Empty;
        #endregion
    }

    /// <summary>
    /// 制动检查
    /// </summary>
    public class BrakeCheckInfo
    {
        #region 属性

        /// <summary>
        /// 单元1
        /// </summary>
        public string UnitOne
        {
            get{return m_UnitOne;}
            set { m_UnitOne = value; }
        }
        private string m_UnitOne = string.Empty;

        /// <summary>
        /// 单元2
        /// </summary>
        public string UnitTwo
        {
            get { return m_UnitTwo; }
            set { m_UnitTwo = value; }
        }
        private string m_UnitTwo = string.Empty;
        #endregion
    }
}
