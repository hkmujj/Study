namespace ES.JCTMS.Common
{
    /// <summary>
    ///     功能描述：故障
    ///     创建人：唐林
    ///     创建时间：2014-07-15
    /// </summary>
    public class FaultInfo
    {
        /// <summary>
        ///     读取或设置故障逻辑属性
        /// </summary>
        public int Logic { get; set; }

        /// <summary>
        ///     读取或设置故障等级属性
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        ///     读取或设置故障代码属性
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     读取或设置故障内容属性
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        ///     读取或设置故障开始时间属性
        /// </summary>
        public string StartTime { get; set; } = "-";

        /// <summary>
        ///     读取或设置故障结束时间属性
        /// </summary>
        public string OverTime { get; set; } = "-";

        /// <summary>
        ///     读取故障的时间（包括开始时间与结束时间）
        /// </summary>
        public string Time
        {
            get { return StartTime + "\n" + OverTime; }
        }
    }

    /// <summary>
    ///     功能描述：故障信息
    ///     创建人：lih
    ///     创建时间：2015-07-15
    /// </summary>
    public class FaultInfo_TMS
    {
        /// <summary>
        ///     读取或设置故障车号
        /// </summary>
        public string CoachNo { get; set; }

        /// <summary>
        ///     读取或设置故障名称
        /// </summary>
        public string FaultName { get; set; }

        /// <summary>
        ///     读取或设置故障代码
        /// </summary>
        public string FaultCode { get; set; }

        /// <summary>
        ///     读取或设置故障等级
        /// </summary>
        public string FaultRank { get; set; }
    }

    /// <summary>
    ///     功能描述：历史故障信息
    ///     创建人：lih
    ///     创建时间：2015-07-15
    /// </summary>
    public class HistoryFaultInfo
    {
        /// <summary>
        ///     读取或设置历史故障车号
        /// </summary>
        public string CoachNo { get; set; }

        /// <summary>
        ///     读取或设置历史故障名称
        /// </summary>
        public string FaultName { get; set; }

        /// <summary>
        ///     读取或设置故障开始时间属性
        /// </summary>
        public string StartTime { get; set; } = "-";

        /// <summary>
        ///     读取或设置故障结束时间属性
        /// </summary>
        public string OverTime { get; set; } = "-";

        /// <summary>
        ///     读取故障的时间（包括开始时间与结束时间）
        /// </summary>
        public string Time
        {
            get { return StartTime + "\n" + OverTime; }
        }
    }

    /// <summary>
    ///     软件版本信息
    /// </summary>
    public class SoftwareVersionInfo
    {
        /// <summary>
        ///     软件名称
        /// </summary>
        public string SoftwareName { get; set; } = string.Empty;

        /// <summary>
        ///     软件版本1
        /// </summary>
        public string Version1 { get; set; } = string.Empty;

        /// <summary>
        ///     软件版本1
        /// </summary>
        public string Version2 { get; set; } = string.Empty;

        /// <summary>
        ///     软件版本1
        /// </summary>
        public string Version3 { get; set; } = string.Empty;

        /// <summary>
        ///     软件版本1
        /// </summary>
        public string Version4 { get; set; } = string.Empty;

        /// <summary>
        ///     软件版本1
        /// </summary>
        public string Version5 { get; set; } = string.Empty;

        /// <summary>
        ///     软件版本1
        /// </summary>
        public string Version6 { get; set; } = string.Empty;
    }

    /// <summary>
    ///     BZDiagnoseInfo
    /// </summary>
    public class BZDiagnoseInfo
    {
        public string FaultInfos { get; set; }
        public string PromptInfos { get; set; }
    }

    public class SoftwareVersionInfoNew
    {
        /// <summary>
        ///     软件名称
        /// </summary>
        public string SoftwareName { get; set; } = string.Empty;

        /// <summary>
        ///     版本信息
        /// </summary>
        public string VersionInfo { get; set; } = string.Empty;
    }

    /// <summary>
    ///     接口检查
    /// </summary>
    public class InterfaceCheck
    {
        /// <summary>
        ///     地址
        /// </summary>
        public string ICAddress
        {
            get { return _icAddress; }
            set
            {
                if (_icAddress != value)
                {
                    _icAddress = value;
                }
            }
        }

        private string _icAddress = string.Empty;

        /// <summary>
        ///     信号标识
        /// </summary>
        public string ICSignalFlag { get; set; } = string.Empty;

        /// <summary>
        ///     值
        /// </summary>
        public string ICValue { get; set; } = string.Empty;
    }

    /// <summary>
    ///     能耗信息
    /// </summary>
    public class EnergyInfo
    {
        /// <summary>
        ///     能耗类别
        /// </summary>
        public string EnergyStyle { get; set; } = string.Empty;

        /// <summary>
        ///     第一个月消耗值
        /// </summary>
        public string Month1Value { get; set; } = string.Empty;

        /// <summary>
        ///     第二个月消耗值
        /// </summary>
        public string Month2Value { get; set; } = string.Empty;

        /// <summary>
        ///     第三个月消耗值
        /// </summary>
        public string Month3Value { get; set; } = string.Empty;

        /// <summary>
        ///     第四个月消耗值
        /// </summary>
        public string Month4Value { get; set; } = string.Empty;

        /// <summary>
        ///     第五个月消耗值
        /// </summary>
        public string Month5Value { get; set; } = string.Empty;

        /// <summary>
        ///     第六个月消耗值
        /// </summary>
        public string Month6Value { get; set; } = string.Empty;

        /// <summary>
        ///     累计
        /// </summary>
        public string RowTotal { get; set; } = string.Empty;
    }

    /// <summary>
    ///     加速度测试
    /// </summary>
    public class AccelerationTestInfo
    {
        /// <summary>
        ///     类别
        /// </summary>
        public string Style { get; set; } = string.Empty;

        /// <summary>
        ///     初速度
        /// </summary>
        public string InitialSpeed { get; set; } = string.Empty;

        /// <summary>
        ///     末速度
        /// </summary>
        public string EndSpeed { get; set; } = string.Empty;

        /// <summary>
        ///     距离
        /// </summary>
        public string Distance { get; set; } = string.Empty;

        /// <summary>
        ///     时间
        /// </summary>
        public string Time { get; set; } = string.Empty;

        /// <summary>
        ///     加速度
        /// </summary>
        public string Acceleration { get; set; } = string.Empty;
    }

    /// <summary>
    ///     制动检查
    /// </summary>
    public class BrakeCheckInfo
    {
        /// <summary>
        ///     单元1
        /// </summary>
        public string UnitOne { get; set; } = string.Empty;

        /// <summary>
        ///     单元2
        /// </summary>
        public string UnitTwo { get; set; } = string.Empty;
    }
}