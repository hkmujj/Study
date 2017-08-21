using System.Drawing;
using CommonUtil.Controls;

namespace Motor.HMI.CRH1A.HighVoltage
{
    /// <summary>
    /// 受电弓
    /// </summary>
    public class AcceptEleArc : SelectableRectangleControl
    {
        /// <summary>
        /// construct 
        /// </summary>
        /// <param name="outLineRectangle"></param>
        /// <param name="id"></param>
        /// <param name="gtHighvoltage"></param>
        public AcceptEleArc(Rectangle outLineRectangle, Id id, GT_HighvoltageStatus gtHighvoltage) : base(outLineRectangle)
        {
            CurrentId = id;
            m_GtHighvoltage = gtHighvoltage;
        }

        /// <summary>
        /// 受电弓编号 
        /// </summary>
        public Id CurrentId { private set; get; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public State CurrentState { set; get; }


        private readonly GT_HighvoltageStatus m_GtHighvoltage;

        public override void OnDraw(Graphics g)
        {

            g.DrawImage(m_GtHighvoltage.ImageMgr.GetImage(this), OutLineRectangle);

            base.OnDraw(g);
        }

        /// <summary>
        /// 受电弓状态, 
        /// </summary>
        public enum State
        {
            /// <summary>
            /// 全绿显示已起升。
            /// </summary>
            HasRise=16 ,

            /// <summary>
            /// 绿边框显示受电弓降下（或状态不明）
            /// </summary>
            DropOrUnkown=17,

            /// <summary>
            /// 全蓝显示被切断并已起升
            /// </summary>
            CutOffAndRise = 14,

            /// <summary>
            /// 蓝边框显示切断并降下
            /// </summary>
            CutOffAndDrop=15,

            /// <summary>
            /// 全红显示有故障并已起升
            /// </summary>
            FaultAndRise=12,

            /// <summary>
            /// 红边框显示有故障并降下
            /// </summary>
            FaultAndDrop=13,

        }

        public enum Id
        {
            No2,
            No7,
        }

    }
}
