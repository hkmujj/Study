using System;

namespace Motor.ATP._200H
{
    /// <summary>
    /// ���F1��ť���ݵĽṹ��
    /// ��Ҫ�洢ʱ��
    /// </summary>
    public struct DataView
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="driverID">˾����</param>
        /// <param name="trainID">���κ�</param>
        public DataView(string driverID, string trainID) : this()
        {
            DriverID = driverID;
            TrainID = trainID;
            TheDatetime = new DateTime();
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public void UpdateTime(DateTime dateTime)
        {
            TheDatetime = dateTime;
            ShareData.m_DirverID = DriverID;
            ShareData.m_TrainID = TrainID;
        }

        #region :::::::::::::: initValue ::::::::::::::

        /// <summary>
        /// ˾����
        /// </summary>
        public string DriverID { get; set; }

        /// <summary>
        /// ���κ�
        /// </summary>
        public string TrainID { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime TheDatetime { get; private set; }

        #endregion
    }
}
