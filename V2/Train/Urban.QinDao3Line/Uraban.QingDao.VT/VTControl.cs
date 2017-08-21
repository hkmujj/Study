using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using MMI.Facility.Interface.Data;
using MMI.Facility.Interface;

namespace Urban.QingDao.VT
{
    public class VTControl
    {
        private Rectangle m_OutRectangle;

        public VTControl(ControlType type, int controlId, int defaultStatus, baseClass baseClass, PageType pageType)
        {
            Type = type;
            ControlId = controlId;
            CurrentStatus = defaultStatus;
            DefaultStatus = defaultStatus;
            m_BaseClass = baseClass;
            PageType = pageType;
        }

        public int DefaultStatus { get; private set; }
        public int CurrentStatus { get; private set; }
        public ControlType Type { get; private set; }
        public PageType PageType { get; private set; }
        public Dictionary<int, Tuple<Image, int>> Images { get; set; }
        public int InKey { get; set; }
        public int ControlId { get; private set; }
        public Rectangle OutRectangle
        {
            get { return m_OutRectangle; }
            set
            {
                m_OutRectangle = value;
                switch (Type)
                {
                    case ControlType.Button:
                    case ControlType.Dial:
                    case ControlType.CircuitBreaker:
                        LeftHot = RightHot = value;
                        break;
                    case ControlType.Knob:
                    case ControlType.AutoKnob:
                        LeftHot = new Rectangle(value.Location.X, value.Location.Y, value.Width / 2, value.Height);
                        RightHot = new Rectangle(value.Location.X + value.Width / 2, value.Location.Y, value.Width / 2, value.Height);
                        break;
                    case ControlType.Back:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

            }
        }

        public void Loaded()
        {
            Reset();
            SendData();
        }
        public Rectangle LeftHot { get; private set; }
        public Rectangle RightHot { get; private set; }
        private baseClass m_BaseClass;
        public string Text { get; set; }
        public void Last()
        {
            if (CurrentStatus > Images.Keys.Min())
            {
                CurrentStatus--;
                SendData();
            }
            else if (Type == ControlType.CircuitBreaker)
            {
                Next();
            }
        }

        public void Next()
        {

            if (CurrentStatus < Images.Keys.Max())
            {
                CurrentStatus++;
                SendData();

            }
            else if (Type == ControlType.CircuitBreaker)
            {
                Last();
            }
        }

        public void AutoControlReset()
        {
            Reset();
            SendData();
        }
        private static Pen LinePen = new Pen(Color.Red);
        readonly Font m_TextFont = new Font("宋体", 16);
        StringFormat SfFormat = new StringFormat()
        {
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Center
        };

        public void DrawBack(Graphics g)
        {
            if (Type != ControlType.Back)
            {
                return;
            }
            g.DrawImage(Images[DefaultStatus].Item1, OutRectangle);
        }
        public void OnDraw(Graphics g)
        {
            if (Type != ControlType.Button && Type != ControlType.Back)
            {
                if (Images[CurrentStatus].Item1 != null)
                {
                    g.DrawImage(Images[CurrentStatus].Item1, OutRectangle);
                }

            }
            else
            {
                g.DrawRectangle(LinePen, OutRectangle);
                g.DrawString(Text, m_TextFont, Brushes.Red, OutRectangle, SfFormat);

            }
        }

        public void Reset()
        {
            CurrentStatus = DefaultStatus;
        }
        public void Refresh()
        {
            if (Type == ControlType.CircuitBreaker)
            {
                if (m_BaseClass.BoolList.ContainsKey(InKey) && InKey != 0)
                {
                    if (m_BaseClass.BoolList[InKey])
                    {
                        CurrentStatus = 1;
                        SendData();
                    }
                }
            }
        }

        public void ClearSendData()
        {
            Images.Values.Where(w => m_BaseClass.OutBoolList.ContainsKey(w.Item2)).ToList().ForEach(f =>
            {
                m_BaseClass.append_postCmd(CmdType.SetBoolValue, f.Item2, 0, 0);
            });
        }
        public void SendData()
        {

            if (Type == ControlType.CircuitBreaker)
            {
                Images.Values.Where(w => w.Item2 != 0).ToList().ForEach(f =>
                {
                    m_BaseClass.append_postCmd(CmdType.SetBoolValue, f.Item2, CurrentStatus == DefaultStatus ? 1 : 0, 0);
                });
            }
            else
            {
                ClearSendData();
                if (Images.ContainsKey(CurrentStatus) && m_BaseClass.OutBoolList.ContainsKey(Images[CurrentStatus].Item2))
                {
                    m_BaseClass.append_postCmd(CmdType.SetBoolValue, Images[CurrentStatus].Item2, 1, 0);
                }
            }

        }
    }
}