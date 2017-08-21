using System;
using System.Drawing;
using System.Text;
using CommonUtil.Controls;
using CRH2MMI.Common;
using CRH2MMI.Common.Util;

namespace CRH2MMI.TrainLineNo
{
    class TrainLineSetView : CommonInnerControlBase
    {
        private TrainLineSetKeyboard m_Keyboard;

        private TrainLineView m_TrainLineView;

        private const string SetTimeInfo = " 年        月        日行车时间表更改";
        private DateTime m_LastSetTime;
        private GDIRectText m_LastSetTimeText;
        private GDIRectText m_TitleText;
        private GDIRectText m_TrainLineText;

        private CRH2BaseClass m_SrcObj;

        /// <summary>
        /// 上次修改的时间
        /// </summary>
        public DateTime LastSetTime
        {
            set
            {
                m_LastSetTime = value;
                m_LastSetTimeText.Text = string.Format("{0}        {1}              {2}", value.Year, value.Month, value.Day);
            }

            get { return m_LastSetTime; }
        }


        public string Title
        {
            set { m_TitleText.Text = value; }
            get { return m_TitleText.Text; }
        }


        private string m_TrainLine;

        public TrainLineSetView(CRH2BaseClass srcObj)
        {
            m_SrcObj = srcObj;
        }

        public string TrainLine
        {
            set
            {
                m_TrainLineText.Text = ConvertToOut(value);
                m_TrainLine = value;
                m_TrainLineView.TrainLine = value;
            }
            get
            {
                return m_TrainLine;
            }
        }

        public void RefreshTrainLine()
        {
            m_TrainLineView.RefreshTrainLine();
            TrainLine = m_TrainLineView.TrainLine;
        }

        private string ConvertToOut(string inTrainLine)
        {
            var tmp = inTrainLine.ToCharArray();
            var sb = new StringBuilder();
            foreach (var c in tmp)
            {
                sb.Append(c);
                sb.Append(' ');
            }
            return sb.ToString();
        }

        public override void OnDraw(Graphics g)
        {
            m_TrainLineText.OnDraw(g);

            m_TitleText.OnDraw(g);

            m_Keyboard.OnDraw(g);

            m_TrainLineView.OnDraw(g);

            m_LastSetTimeText.OnDraw(g);
            g.DrawString(SetTimeInfo, CRH2Resource.Font12, CRH2Resource.WhiteBrush, new PointF(m_LastSetTimeText.Location.X + 35, m_LastSetTimeText.Location.Y));
            
        }

        public override void Init()
        {
            m_TrainLineView = new TrainLineView() { Location = new Point(200, 200) };

            m_Keyboard = new TrainLineSetKeyboard()
            {
                OutLineRectangle = new Rectangle(new Point(3, 170), new Size(786, 350)) ,
                CharContentClick = (sender, args) => m_TrainLineView.ReplaceCurrent(args.Content.ToString()),
                NumberContentClick = (sender, args) => m_TrainLineView.ReplaceCurrent(args.Content.ToString()),
                ControlClick = (sender, args) => {
                                                     switch (args.Type)
                                                     {
                                                         case ControlPressedEventArgs.ControlType.GotoLeft:
                                                             m_TrainLineView.GotoPrevious();
                                                             break;
                                                         case ControlPressedEventArgs.ControlType.GotoRight:
                                                             m_TrainLineView.GotoNext();
                                                             break;
                                                         case ControlPressedEventArgs.ControlType.Delete:
                                                             m_TrainLineView.DeleteCurrent();
                                                             break;
                                                         default:
                                                             throw new ArgumentOutOfRangeException();
                                                     }
                }
            };

            var txtSize = new Size(300, 20);
            m_LastSetTimeText = new GDIRectText()
            {
                OutLineRectangle = new Rectangle(470, 110, txtSize.Width, txtSize.Height),
                TextColor = Color.Yellow,
                BkColor = Color.Black,
            };
            LastSetTime = m_SrcObj.CurrentTime;

            m_TrainLineText = new GDIRectText()
            {
                OutLineRectangle = new Rectangle(300, 140, 300, 20),
                TextColor = Color.Yellow,
                BkColor = Color.Black,
            };

            m_TitleText = new GDIRectText()
            {
                OutLineRectangle = new Rectangle(0, 110, 300, 20),
                TextColor = CRH2Resource.WWBrush.Color,
                BkColor = Color.Black,
            };
        }

        public override bool OnMouseDown(Point point)
        {
            return m_Keyboard.OnMouseDown(point);
        }

        public void Reset()
        {
            m_TrainLineView.Reset();
        }
    }
}
