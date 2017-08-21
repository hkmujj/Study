using System;
using System.Drawing;
using CommonUtil.Controls;
using Motor.HMI.CRH3C380B.Resource.Images;

namespace Motor.HMI.CRH3C380B.Common
{
    /// <summary>
    /// 充电机
    /// </summary>
    public class ChargingGeneratorControl : CommonInnerControlBase
    {
        public enum ChargingGeneratorState
        {
            Default,
            Unkown,
            Blue,
            Yellow,
        }

        private readonly CRH3C380BBase m_SrcObj;

        public ChargingGeneratorControl(CRH3C380BBase srcObj)
        {
            m_SrcObj = srcObj;
        }

        public Tuple<string, ChargingGeneratorState>[] StateNames { set; get; }

        public ChargingGeneratorState State { private set; get; }

        /// <summary>
        /// 刷新, 调用 RefreshAction
        /// </summary>
        public override void Refresh()
        {
            State = ChargingGeneratorState.Default;

            if (StateNames != null)
            {
                foreach (var name in StateNames)
                {
                    if (m_SrcObj.GetInBoolValue(name.Item1))
                    {
                        State = name.Item2;
                    }
                }
            }

            base.Refresh();
        }

        public override void OnDraw(Graphics g)
        {
            switch (State)
            {
                case ChargingGeneratorState.Unkown:
                    g.DrawImage(CommonImages.StateUnkown, OutLineRectangle);
                    break;
                case ChargingGeneratorState.Default:
                    g.DrawImage(m_SrcObj.DMITitle.NightMode ? MSImages.xfk_1_2 : MSImages.xfk_0_2, OutLineRectangle);
                    break;
                case ChargingGeneratorState.Blue:
                    g.DrawImage(m_SrcObj.DMITitle.NightMode ? MSImages.xfk_1_0 : MSImages.xfk_0_0, OutLineRectangle); 
                    break;
                case ChargingGeneratorState.Yellow:
                    g.DrawImage(m_SrcObj.DMITitle.NightMode ? MSImages.xfk_1_1 : MSImages.xfk_0_1, OutLineRectangle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}