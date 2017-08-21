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
        public Int32 Logic { get; set; }
        /// <summary>
        /// 读取或设置故障等级属性
        /// </summary>
        public Int32 Grade { get; set; }
        /// <summary>
        /// 读取或设置故障代码属性
        /// </summary>
        public String Code { get; set; }
        /// <summary>
        /// 读取或设置故障内容属性
        /// </summary>
        public String Display { get; set; }
        /// <summary>
        /// 读取或设置故障开始时间属性
        /// </summary>
        public String StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }
        private String _startTime = "-";
        /// <summary>
        /// 读取或设置故障结束时间属性
        /// </summary>
        public String OverTime
        {
            get { return _overTime; }
            set { _overTime = value; }
        }
        private String _overTime = "-";

        /// <summary>
        /// 读取故障的时间（包括开始时间与结束时间）
        /// </summary>
        public String Time
        {
            get { return _startTime + "\n" + _overTime; }
        }
    }

    /// <summary>
    /// 功能描述：故障信息
    /// 创建人：lih
    /// 创建时间：2015-07-15
    /// </summary>
    public class FaultInfo_TMS
    {
        /// <summary>
        /// 读取或设置故障车号
        /// </summary>
        public String CoachNo { get; set; }

        /// <summary>
        /// 读取或设置故障名称
        /// </summary>
        public String FaultName { get; set; }

        /// <summary>
        /// 读取或设置故障代码
        /// </summary>
        public String FaultCode { get; set; }

        /// <summary>
        /// 读取或设置故障等级
        /// </summary>
        public String FaultRank { get; set; }
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
        public String CoachNo { get; set; }

        /// <summary>
        /// 读取或设置历史故障名称
        /// </summary>
        public String FaultName { get; set; }

        /// <summary>
        /// 读取或设置故障开始时间属性
        /// </summary>
        public String StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }
        private String _startTime = "-";
        /// <summary>
        /// 读取或设置故障结束时间属性
        /// </summary>
        public String OverTime
        {
            get { return _overTime; }
            set { _overTime = value; }
        }
        private String _overTime = "-";

        /// <summary>
        /// 读取故障的时间（包括开始时间与结束时间）
        /// </summary>
        public String Time
        {
            get { return _startTime + "\n" + _overTime; }
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
        public String SoftwareName
        {
            get { return _softwareName; }
            set {_softwareName=value;}
        }
        private String _softwareName = String.Empty;

        /// <summary>
        /// 软件版本1
        /// </summary>
        public String Version1
        {
            get { return _version1; }
            set { _version1 = value; }
        }
        private String _version1 = String.Empty;

        /// <summary>
        /// 软件版本1
        /// </summary>
        public String Version2
        {
            get { return _version2; }
            set { _version2 = value; }
        }
        private String _version2 = String.Empty;

        /// <summary>
        /// 软件版本1
        /// </summary>
        public String Version3
        {
            get { return _version3; }
            set { _version3 = value; }
        }
        private String _version3 = String.Empty;

        /// <summary>
        /// 软件版本1
        /// </summary>
        public String Version4
        {
            get { return _version4; }
            set { _version4 = value; }
        }
        private String _version4 = String.Empty;

        /// <summary>
        /// 软件版本1
        /// </summary>
        public String Version5
        {
            get { return _version5; }
            set { _version5 = value; }
        }
        private String _version5 = String.Empty;

        /// <summary>
        /// 软件版本1
        /// </summary>
        public String Version6
        {
            get { return _version6; }
            set { _version6 = value; }
        }
        private String _version6 = String.Empty;
        #endregion
    }

    /// <summary>
    /// BZDiagnoseInfo
    /// </summary>
    public class BZDiagnoseInfo
    {
        public String FaultInfos { get; set; }
        public String PromptInfos { get;set; }
    }

    public class SoftwareVersionInfoNew
    {
        #region 属性
        /// <summary>
        /// 软件名称
        /// </summary>
        public String SoftwareName
        {
            get { return _softwareName; }
            set { _softwareName = value; }
        }
        private String _softwareName = String.Empty;

        /// <summary>
        /// 版本信息
        /// </summary>
        public String VersionInfo
        {
            get { return _versionInfo; }
            set { _versionInfo = value; }
        }
        private String _versionInfo = String.Empty;
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
        public String ICAddress 
        {
            get
            {
                return _icAddress;
            }
            set
            {
                if (_icAddress != value)
                {
                    _icAddress = value;
                }
            }
        }
        private String _icAddress = String.Empty;

        /// <summary>
        /// 信号标识
        /// </summary>
        public String ICSignalFlag
        {
            get { return _icsignalFlag; }
            set { _icsignalFlag = value; }
        }
        private String _icsignalFlag = String.Empty;

        /// <summary>
        /// 值
        /// </summary>
        public String ICValue
        {
            get { return _icValue; }
            set { _icValue = value; }
        }
        private String _icValue = String.Empty;
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
        public String EnergyStyle
        {
            get { return _energyStyle; }
            set { _energyStyle = value; }
        }
        private String _energyStyle = String.Empty;

        /// <summary>
        /// 第一个月消耗值
        /// </summary>
        public String Month1Value
        {
            get { return _month1Value; }
            set { _month1Value = value; } 
        }
        private String _month1Value = String.Empty;


        /// <summary>
        /// 第二个月消耗值
        /// </summary>
        public String Month2Value
        {
            get { return _month2Value; }
            set { _month2Value = value; }
        }
        private String _month2Value = String.Empty;

        /// <summary>
        /// 第三个月消耗值
        /// </summary>
        public String Month3Value
        {
            get { return _month3Value; }
            set { _month3Value = value; }
        }
        private String _month3Value = String.Empty;

        /// <summary>
        /// 第四个月消耗值
        /// </summary>
        public String Month4Value
        {
            get { return _month4Value; }
            set { _month4Value = value; }
        }
        private String _month4Value = String.Empty;

        /// <summary>
        /// 第五个月消耗值
        /// </summary>
        public String Month5Value
        {
            get { return _month5Value; }
            set { _month5Value = value; }
        }
        private String _month5Value = String.Empty;

        /// <summary>
        /// 第六个月消耗值
        /// </summary>
        public String Month6Value
        {
            get { return _month6Value; }
            set { _month6Value = value; }
        }
        private String _month6Value = String.Empty;

        /// <summary>
        /// 累计
        /// </summary>
        public String RowTotal
        {
            get { return _rowTotal; }
            set { _rowTotal = value; }
        }
        private String _rowTotal = String.Empty;

         
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
        public String Style
        {
            get{  return _style;}
            set { _style = value; }
        }
        private String _style = String.Empty;

        /// <summary>
        /// 初速度
        /// </summary>
        public String InitialSpeed
        {
            get { return _initialSpeed; }
            set { _initialSpeed = value; }
        }
        private String _initialSpeed = String.Empty;

        /// <summary>
        /// 末速度
        /// </summary>
        public String EndSpeed
        {
            get { return _endSpeed; }
            set { _endSpeed = value; }
        }
        private String _endSpeed = String.Empty;

        /// <summary>
        /// 距离
        /// </summary>
        public String Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }
        private String _distance = String.Empty;

        /// <summary>
        /// 时间
        /// </summary>
        public String Time
        {
            get { return _time; }
            set { _time = value; }
        }
        private String _time = String.Empty;

        /// <summary>
        /// 加速度
        /// </summary>
        public String Acceleration
        {
            get { return _acceleration; }
            set { _acceleration = value; }
        }
        private String _acceleration = String.Empty;
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
        public String UnitOne
        {
            get{return _unitOne;}
            set { _unitOne = value; }
        }
        private String _unitOne = String.Empty;

        /// <summary>
        /// 单元2
        /// </summary>
        public String UnitTwo
        {
            get { return _unitTwo; }
            set { _unitTwo = value; }
        }
        private String _unitTwo = String.Empty;
        #endregion
    }
}
