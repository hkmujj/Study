using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Motor.HMI.CRH1A.Door
{
    /// <summary>
    /// 门信息
    /// </summary>
    public class DoorInfo
    {
        /// <summary>
        /// 车箱编号
        /// </summary>
        public int TrainNumber
        {
            get { return m_TrainNumber; }
        }

        /// <summary>
        /// 门状态 
        /// </summary>
        public DoorState State { set; get; }

        /// <summary>
        /// 门的轮廓大小 , 即外面的方块
        /// </summary>
        public Rectangle DoorOutLineRectangle { set; get; }

        /// <summary>
        /// 门图形里面的内容
        /// </summary>
        public Rectangle DoorContentRectangle { set; get; }

        private readonly DoorType m_DoorType;
        private readonly int m_TrainNumber;

        /// <summary>
        /// 门的类型, 上还是下?
        /// </summary>
        public DoorType Type { get { return m_DoorType; } }

        /// <summary>
        /// construct
        /// </summary>
        /// <param name="trainNum"></param>
        /// <param name="doorType"></param>
        public DoorInfo(int trainNum, DoorType doorType)
        {
            m_DoorType = doorType;
            m_TrainNumber = trainNum;
        }
    }
}
