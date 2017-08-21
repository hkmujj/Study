using System;

namespace Motor.ATP._200H
{
    /// <summary>
    /// 存放F1按钮数据的结构体
    /// 主要存储时间
    /// </summary>
    public struct DataView
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="driverID">司机号</param>
        /// <param name="trainID">车次号</param>
        public DataView(string driverID, string trainID) : this()
        {
            DriverID = driverID;
            TrainID = trainID;
            TheDatetime = new DateTime();
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public void UpdateTime(DateTime dateTime)
        {
            TheDatetime = dateTime;
            ShareData.m_DirverID = DriverID;
            ShareData.m_TrainID = TrainID;
        }

        #region :::::::::::::: initValue ::::::::::::::

        /// <summary>
        /// 司机号
        /// </summary>
        public string DriverID { get; set; }

        /// <summary>
        /// 车次号
        /// </summary>
        public string TrainID { get; set; }

        /// <summary>
        /// 日期时间
        /// </summary>
        public DateTime TheDatetime { get; private set; }

        #endregion
    }
}
