using System;

namespace Motor.ATP._200H
{
    /// <summary>
    /// ���F1��ť���ݵĽṹ��
    /// ��Ҫ�洢ʱ��
    /// </summary>
    public struct DataViewControl
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="driverID">˾����</param>
        /// <param name="trainID">���κ�</param>
        public DataViewControl(string driverID, string trainID)
        {
            _driverID = driverID;
            _trainID = trainID;
            _theDatetime = new DateTime();
        }

        /// <summary>
        /// ����ʱ��
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
        /// ˾����
        /// </summary>
        public string DriverID
        {
            get { return _driverID; }
            set { _driverID = value; }
        }

        private string _trainID;
        /// <summary>
        /// ���κ�
        /// </summary>
        public string TrainID
        {
            get { return _trainID; }
            set { _trainID = value; }
        }

        private DateTime _theDatetime;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime TheDatetime
        {
            get { return _theDatetime; }
        }
        #endregion
    }
}