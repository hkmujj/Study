using System;

namespace Motor.ATP._200H
{
    /// <summary>
    /// 存放F1按钮数据的结构体
    /// 主要存储时间
    /// </summary>
    public struct DataViewControl
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="driverID">司机号</param>
        /// <param name="trainID">车次号</param>
        public DataViewControl(string driverID, string trainID)
        {
            _driverID = driverID;
            _trainID = trainID;
            _theDatetime = new DateTime();
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public void UpdateTime()
        {
            _theDatetime = DateTime.Now;
            ShareData.Dirver_ID = _driverID;
            ShareData.Train_ID = _trainID;
        }

        #region :::::::::::::: initValue ::::::::::::::
        private string _driverID;
        /// <summary>
        /// 司机号
        /// </summary>
        public string DriverID
        {
            get { return _driverID; }
            set { _driverID = value; }
        }

        private string _trainID;
        /// <summary>
        /// 车次号
        /// </summary>
        public string TrainID
        {
            get { return _trainID; }
            set { _trainID = value; }
        }

        private DateTime _theDatetime;
        /// <summary>
        /// 日期时间
        /// </summary>
        public DateTime TheDatetime
        {
            get { return _theDatetime; }
        }
        #endregion
    }
}