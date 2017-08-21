using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Motor.HMI.CRH1A.Common;
using CommonUtil.Controls;

namespace Motor.HMI.CRH1A.Door
{
    class DoorMgr : IInnerControl
    {
        private DoorInfo[] m_DoorInfos = new DoorInfo[8 * 2];

        private Rectangle m_Recposition = new Rectangle(0, 170, 800, 100);

        private DoorStateAdpt m_DoorStateAdpt;
        private GT_DoorStatus m_DoorStatus;

        private bool[] m_Valueb = new bool[80];
        private SolidBrush m_Recbrush = new SolidBrush(Color.FromArgb(119, 136, 153));//背景画刷
        private readonly SolidBrush m_GreenBrush = new SolidBrush(Color.FromArgb(0, 126, 0));//绿色画刷
        private readonly SolidBrush m_RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));//红色画刷
        private readonly SolidBrush m_BlueBrush = new SolidBrush(Color.FromArgb(0, 0, 255));//蓝色画刷画刷

        private readonly Pen m_GreenPen = new Pen(Color.FromArgb(0, 255, 0), 3);
        private readonly Pen m_RedPen = new Pen(Color.FromArgb(255, 0, 0), 3);
        private readonly Pen m_BluePen = new Pen(Color.FromArgb(0, 0, 255), 3);
        private readonly Pen m_GrayPen = new Pen(Color.FromArgb(115, 115, 115), 3);
        private readonly Pen m_SmallPen = new Pen(Color.FromArgb(85, 85, 85), 1);

        private readonly Pen m_SelectDoorPen = new Pen(Color.Black);

        /// <summary>
        /// 当前选中的DOOR, 如果没有选中为null;
        /// </summary>
        public DoorInfo SelectDoorInfo { get; set; }

        public DoorMgr(GT_DoorStatus doorStatus)
        {
            m_DoorStateAdpt = new DoorStateAdpt();
            m_DoorStatus = doorStatus;
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            SelectDoorInfo = null;
            for (int i = 0; i < 8; i++)
            {
                //上 侧 门 
                var outLineUp = new Rectangle(m_Recposition.X + 55 + i * 90, m_Recposition.Y, 50, 40);
                var contentUp = new Rectangle(m_Recposition.X + 75 + i * 90, m_Recposition.Y + 7, 10, 25);
                m_DoorInfos[i] = new DoorInfo(i, DoorType.UpDoor)
                {
                    DoorContentRectangle = contentUp,
                    DoorOutLineRectangle = outLineUp
                };
                //下 侧 门 
                var outLineD = new Rectangle(m_Recposition.X + 55 + i * 90, m_Recposition.Y + 113, 50, 40);
                var contentD = new Rectangle(m_Recposition.X + 75 + i * 90, m_Recposition.Y + 120, 10, 25);
                m_DoorInfos[i + 8] = new DoorInfo(i, DoorType.DownDoor)
                {
                    DoorContentRectangle = contentD,
                    DoorOutLineRectangle = outLineD
                };
            }
        }

        public void OnPaint(Graphics dcGs)
        {
            GetValue();

            OnDraw(dcGs);
        }

        private void GetValue()
        {
            if (m_DoorStatus.UIObj.InBoolList.Count >= 80)
            {
                for (int i = 0; i < 80; i++)
                {
                    m_Valueb[i] = m_DoorStatus.BoolList[m_DoorStatus.UIObj.InBoolList[i]];
                }
            }
        }

        public bool OnMouseDown(Point point)
        {
            var di = GetSelectDoor(point);

            return RefreshSelectDoor(di);

        }

        private bool RefreshSelectDoor(DoorInfo di)
        {
            if (di != null)
            {
                if (SelectDoorInfo == di)
                {
                    DeselectDoor(SelectDoorInfo);
                    
                }
                else
                {
                    DeselectDoor(SelectDoorInfo);
                    SelectDoor(di);
                }
                return true;
            }
            return false;
        }

        private void SelectDoor(DoorInfo di)
        {
            SelectDoorInfo = di;
        }

        private void DeselectDoor(DoorInfo selectDoorInfo)
        {
            SelectDoorInfo = null;
        }

        private DoorInfo GetSelectDoor(Point point)
        {
            foreach (var info in m_DoorInfos)
            {
                if (info.DoorOutLineRectangle.Contains(point))
                {
                    return info;
                }
            }
            return null;
        }

        public bool OnMouseUp(Point point)
        {
            // TODO ZCY 按键响应实现 
            throw new NotImplementedException();
        }

        public void OnDraw(Graphics g)
        {
            foreach (var doorInfo in m_DoorInfos)
            {
                doorInfo.State = m_DoorStateAdpt.GetDoorState(doorInfo, m_Valueb);

                switch (doorInfo.State)
                {
                    case DoorState.OpenOrRelease:
                        g.FillRectangle(m_GreenBrush, doorInfo.DoorOutLineRectangle);
                        break;
                    case DoorState.OpenAndFault:
                        g.FillRectangle(m_RedBrush, doorInfo.DoorOutLineRectangle);
                        break;
                    case DoorState.OpenAndOff:
                        g.FillRectangle(m_BlueBrush, doorInfo.DoorOutLineRectangle);
                        break;
                    case DoorState.Close:
                        g.DrawRectangle(m_GreenPen, doorInfo.DoorOutLineRectangle);
                        break;
                    case DoorState.CloseAndFault:
                        g.DrawRectangle(m_RedPen, doorInfo.DoorOutLineRectangle);
                        break;
                    case DoorState.CloseAndOff:
                        g.DrawRectangle(m_BluePen, doorInfo.DoorOutLineRectangle);
                        break;
                    case DoorState.Unknown:
                        g.DrawRectangle(m_GrayPen, doorInfo.DoorOutLineRectangle);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                g.DrawRectangle(m_SmallPen, doorInfo.DoorContentRectangle);
            }
            // 刷新选中的门
            if (SelectDoorInfo != null)
            {
                g.DrawRectangle(m_SelectDoorPen, SelectDoorInfo.DoorOutLineRectangle);
            }

        }

        public bool Contains(Point point)
        {
            throw new NotImplementedException();
        }

        public Action<object> RefreshAction { get; set; }
        public object Tag { get; set; }
    }
}
