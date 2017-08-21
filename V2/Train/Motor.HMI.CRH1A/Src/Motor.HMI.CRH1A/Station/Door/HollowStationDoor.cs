using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CommonUtil.Controls;

namespace Motor.HMI.CRH1A.Station.Door
{
    class HollowStationDoor : StationDoorBase
    {
        static readonly Pen ClPen = new Pen(Color.FromArgb(115, 115, 115),2);//车门关时显示的颜色
        static readonly Pen RePen = new Pen(Color.FromArgb(255, 255, 0), 2);//车门
        static readonly Pen OpPen = new Pen(Color.FromArgb(0, 126, 0), 2);//开门
        static readonly Pen CuPen = new Pen(Color.FromArgb(128, 255, 255), 2);//隔离
        static readonly Pen FaPen = new Pen(Color.FromArgb(255, 0, 0), 2);//故障
        //static readonly Pen Pen = new Pen(Color.FromArgb(100, 100, 100), 2);//信息栏边框
        private Pen m_CurrentPen;

        private StationDoorState m_State;

        public HollowStationDoor()
        {
            m_CurrentPen = ClPen;
        }

        public override void OnDraw(Graphics g)
        {
            g.DrawRectangle(m_CurrentPen, OutLineRectangle);
        }

        public override StationDoorState State
        {
            get { return m_State; }
            set
            {
                m_State = value;
                m_CurrentPen = GetPen(value);
            }
        }

        private Pen GetPen(StationDoorState state)
        {
            switch (state)
            {
                case StationDoorState.Cut :
                    return CuPen;
                case StationDoorState.Fault :
                    return FaPen;
                case StationDoorState.Open :
                    return OpPen;
                case StationDoorState.Release :
                    return RePen;
                case StationDoorState.Close :
                    return ClPen;
                default :
                    throw new ArgumentOutOfRangeException("state");
            }
        }
    }
}
