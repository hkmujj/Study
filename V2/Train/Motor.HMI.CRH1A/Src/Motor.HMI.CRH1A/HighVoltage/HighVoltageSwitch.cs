using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonUtil.Controls;
using Motor.HMI.CRH1A.Common;
using CommonUtil.Controls;

namespace Motor.HMI.CRH1A.HighVoltage
{
    /// <summary>
    /// 高压系统网侧断路器
    /// </summary>
    public class HighVoltageSwitch : SelectableRectangleControl
    {
        public HighVoltageSwitch(GT_HighvoltageStatus gtHighvoltage, int id, Orientation orientation, Rectangle rect)
            : base(rect)
        {
            Orientation = orientation;
            m_GtHighvoltage = gtHighvoltage;
            Id = id;
        }

        /// <summary>
        /// 方向
        /// </summary>
        public Orientation Orientation { private set; get; }

        /// <summary>
        /// 当前状态 
        /// </summary>
        public State CurrentState { set; get; }

        /// <summary>
        /// ID
        /// </summary>
        public int Id { private set; get; }

        private readonly GT_HighvoltageStatus m_GtHighvoltage;

        public override void OnDraw(Graphics g)
        {
            g.DrawImage(m_GtHighvoltage.ImageMgr.GetImage(this), OutLineRectangle);

            base.OnDraw(g);
        }

        public enum State
        {
            /// <summary>
            /// 全绿显示闭合
            /// </summary>
            HasConnect,

            /// <summary>
            /// 绿边框显示断开（或状态不明）
            /// </summary>
            DisconnectOrUnkown,

            /// <summary>
            /// 全蓝显示切断并闭合
            /// </summary>
            CutOffButConnect,

            /// <summary>
            /// 蓝边框显示切断并断开
            /// </summary>
            CutOffAndDisconnect,

            /// <summary>
            /// 全红显示有故障并闭合。
            /// </summary>
            FaultButConnect,

            /// <summary>
            /// 红边框显示有故障并断开。
            /// </summary>
            FaultAndDisconnect,
        }

    }
}
