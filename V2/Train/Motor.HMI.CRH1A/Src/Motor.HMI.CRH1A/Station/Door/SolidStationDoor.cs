using System;
using System.Drawing;

namespace Motor.HMI.CRH1A.Station.Door
{
    class SolidStationDoor : StationDoorBase
    {

        static readonly SolidBrush ClBrush = new SolidBrush(Color.FromArgb(192, 192, 192));//车门关时显示的颜色
        static readonly SolidBrush ReBrush = new SolidBrush(Color.FromArgb(255, 255, 0));//车门
        static readonly SolidBrush OpBrush = new SolidBrush(Color.FromArgb(0, 126, 0));//开门
        static readonly SolidBrush CuBrush = new SolidBrush(Color.FromArgb(128, 255, 255));//隔离
        static readonly SolidBrush FaBrush = new SolidBrush(Color.FromArgb(255, 0, 0));//故障
        static readonly Pen Pen = new Pen(Color.FromArgb(100, 100, 100), 2);//信息栏边框
        private SolidBrush m_CurrentBrush;

        private StationDoorState m_State;

        public SolidStationDoor()
        {
            m_CurrentBrush = ClBrush;
        }

        public override void OnDraw(Graphics g)
        {
            g.FillRectangle(m_CurrentBrush, OutLineRectangle);
            g.DrawRectangle(Pen, OutLineRectangle);
        }

        public override StationDoorState State
        {
            get { return m_State; }
            set
            {
                m_State = value;
                m_CurrentBrush = GetBrush(value);
            }
        }

        private SolidBrush GetBrush(StationDoorState state)
        {
            switch (state)
            {
                case StationDoorState.Cut :
                    return CuBrush;
                case StationDoorState.Fault :
                    return FaBrush;
                case StationDoorState.Open :
                    return OpBrush;
                case StationDoorState.Release :
                    return ReBrush;
                case StationDoorState.Close :
                    return ClBrush;
                default :
                    throw new ArgumentOutOfRangeException("state");
            }
        }
    }
}
