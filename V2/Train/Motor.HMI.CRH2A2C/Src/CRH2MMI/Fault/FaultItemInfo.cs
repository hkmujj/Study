using System;
using System.Collections.Generic;
using System.Drawing;
using CRH2MMI.WorkState;

namespace CRH2MMI.Fault
{
    /// <summary>
    /// 
    /// </summary>
    public class FaultItemInfo
    {
        private DateTime m_OccurTime;

        /// <summary>
        /// 逻辑号
        /// </summary>
        public int LogicIndex { set; get; }

        /// <summary>
        /// 故障ID
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// 故障名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 故障内容 
        /// </summary>
        public string Content { get { return string.Format("{0} （ {1:0 0 0} ）", Name, Id); } }

        /// <summary>
        /// 故障发生的车厢号
        /// </summary>
        public List<int> CarNumbers { set; get; }

        /// <summary>
        /// 线路名称
        /// </summary>
        public string TrainNo { set; get; }

        /// <summary>
        /// 处理信息的图片文件
        /// </summary>
        public string ProessInfoImageFile { set; get; }

        /// <summary>
        /// 处理信息的图片
        /// </summary>
        public Image ProessInfoImage { set; get; }

        /// <summary>
        /// 准备被激活,显示出来
        /// </summary>
        public bool ToBeActived { set; get; }

        /// <summary>
        /// 激活状态
        /// </summary>
        public bool Activie { set; get; }

        /// <summary>
        /// 已被删除 
        /// </summary>
        public bool HasDeleted { set; get; }

        /// <summary>
        /// 保护装置
        /// </summary>
        public string ProtectedMachine { set; get; }

        /// <summary>
        /// 故障发生的时间
        /// </summary>
        public DateTime OccurTime
        {
            set
            {
                m_OccurTime = value;
                UpdateDateTime();
            }
            get { return m_OccurTime; }
        }

        public FaultItemInfo(DateTime occurTime)
        {
            Activie = true;
            ToBeActived = true;
            OccurTime = occurTime;
        }

        #region 故障发生时的信息, 从车辆获得

        public string Year { private set; get; }
        public string Month { private set; get; }
        public string Day { private set; get; }
        public string Hour { private set; get; }
        public string Minute { private set; get; }
        public string Sencond { private set; get; }
        public string Speed { set; get; }
        public string Distance { set; get; }
        public string Level { set; get; }
        public BrakeType Brake { set; get; }

        #endregion


        private void UpdateDateTime()
        {
            Year = string.Format("` {0}", OccurTime.Year % 100);
            Month = OccurTime.Month.ToString();
            Day = OccurTime.Day.ToString();
            Hour = OccurTime.Hour.ToString();
            Minute = OccurTime.Minute.ToString();
            Sencond = OccurTime.Second.ToString();
        }
    }
}
