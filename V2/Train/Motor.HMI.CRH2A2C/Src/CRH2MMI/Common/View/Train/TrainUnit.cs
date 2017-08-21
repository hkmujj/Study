using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;


namespace CRH2MMI.Common.View.Train
{
    /// <summary>
    /// 列车单元
    /// </summary>
    class TrainUnit : CommonInnerControlBase
    {
        private readonly List<string> m_UnitNames;

        private const int Interval = 4;

        private List<Line> m_Lines;
        private List<GDIRectText> m_UnitNameTexts;

        private Action<Graphics> m_DrawAction;

        private readonly List<TrainUnitConfig> m_UnitConfigList;

        /// <summary>
        /// 车的宽度
        /// </summary>
        private readonly int m_CarWidth;

        /// <summary>
        /// 单元状态是否可见
        /// </summary>
        public bool IsUnitStateVisible
        {
            set
            {
                if (value != m_IsUnitStateVisible)
                {
                    m_IsUnitStateVisible = value;
                    RefreshDrawAction();
                }

                if (m_Lines != null && m_Lines.Any())
                {
                    OnOutLineChanged(null, null);
                }

            }
            get { return m_IsUnitStateVisible; }
        }

        private void RefreshDrawAction()
        {
            m_DrawAction = graphics => { };
            if (IsUnitStateVisible)
            {
                m_DrawAction += DrawSates;
            }

            if (IsUnitNameVisible)
            {
                m_DrawAction += DrawNames;
            }
        }

        public bool IsUnitNameVisible
        {
            set
            {
                m_IsUnitNameVisible = value;
                RefreshDrawAction();

                if (m_Lines != null && m_Lines.Any())
                {
                    OnOutLineChanged(null, null);
                }
            }
            get { return m_IsUnitNameVisible; }
        }

        public TrainUnitState this[TrainUnitNo unit]
        {
            set
            {
                if (unit > m_MaxUnitNo)
                {
                    throw new ArgumentOutOfRangeException();
                }
                var idx = m_HasReversal ? (int)(m_MaxUnitNo - unit) : (int)unit;
                m_TrainUnitStates[idx] = value;
                m_StateBrush[idx].Color = TrainUnitStateColorAdpt.GetStateColor(value);

            }
            get
            {
                if (unit > m_MaxUnitNo)
                {
                    throw new ArgumentOutOfRangeException();
                }
                var idx = m_HasReversal ? (int)(m_MaxUnitNo - unit) : (int)unit;
                return m_TrainUnitStates[idx];
            }
        }

        public const int UnitStateHeight = 10;

        public const int UnitNameHeight = 25;

        public const int StateNameInterval = 26;

        private readonly TrainUnitNo m_MaxUnitNo;

        private bool m_HasReversal;

        private readonly List<TrainUnitState> m_TrainUnitStates;

        private List<Rectangle> m_TrainUnitRect;

        /// <summary>
        /// 单元的大小 
        /// </summary>
        public List<Rectangle> TrainUnitRect
        {
            get { return m_TrainUnitRect; }
        }
        private bool m_IsUnitStateVisible;
        private Size m_Size;
        private List<SolidBrush> m_StateBrush;
        private bool m_IsUnitNameVisible;

        public TrainUnit(List<TrainUnitConfig> unitConfig, Car firstCar)
        {
            m_UnitConfigList = unitConfig;
            var unitCount = unitConfig.Count;
            m_CarWidth = firstCar.Size.Width;
            //m_UnitWidth = unitWidth;
            IsUnitStateVisible = true;
            IsUnitNameVisible = true;
            m_HasReversal = false;
            //m_UnitNames = new List<string>(unitCount);

            m_Size = new Size((unitConfig.Count - 1)*Interval + unitConfig.SelectMany(s => s.CarNos).Count()*m_CarWidth,
                UnitStateHeight + UnitNameHeight + StateNameInterval);

            m_StateBrush = new List<SolidBrush>(unitCount);
            m_TrainUnitStates = new List<TrainUnitState>(unitCount);
            m_UnitNames = new List<string>();
            unitConfig.ForEach(e =>
            {
                m_UnitNames.Add(e.UnitName);
                m_TrainUnitStates.Add(TrainUnitState.Nomal);
                m_StateBrush.Add(new SolidBrush(Color.White));
            });
            m_MaxUnitNo = (TrainUnitNo)(unitConfig.Count - 1);
            OutLineChanged = OnOutLineChanged;
        }

        public override Size Size
        {
            get { return m_Size; }
            set { throw new Exception("Can not set size."); }
        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            InitCtorls();
        }

        private void InitCtorls()
        {
            m_TrainUnitRect = new List<Rectangle>();
            m_Lines = new List<Line>();
            m_UnitNameTexts = new List<GDIRectText>();

            const int lineHeight = 10;
            const float lineWidth = 3f;
            //var step = (m_UnitNames.Count / 2);
            var offset = 0;
            for (int index = 0; index < m_UnitConfigList.Count; index++)
            {
                var uitnConfig = m_UnitConfigList[index];
                var width = uitnConfig.CarNos.Count * m_CarWidth;
                var startPoint = new Point(Location.X + offset + Interval,
                    Location.Y + StateNameInterval + (IsUnitStateVisible ? UnitStateHeight : 0));
                var endPoint = new Point(Location.X + offset + width,
                    Location.Y + StateNameInterval + (IsUnitStateVisible ? UnitStateHeight : 0));
                var length = (endPoint.X - startPoint.X);

                m_Lines.Add(new Line(startPoint, endPoint, lineWidth));

                m_Lines.Add(new Line(startPoint, new Point(startPoint.X, startPoint.Y - lineHeight), lineWidth));
                m_Lines.Add(new Line(endPoint, new Point(endPoint.X, endPoint.Y - lineHeight), lineWidth));

                m_UnitNameTexts.Add(new GDIRectText
                {
                    OutLineRectangle = new Rectangle(
                        new Point((startPoint.X + endPoint.X)/2 - 20,
                            startPoint.Y - UnitNameHeight/2), new Size(40, UnitNameHeight)),
                    Text = m_UnitNames[index],
                    TextColor = Color.White,
                    TextFormat =
                        new StringFormat() {LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center},
                    NeedDarwOutline = false,
                });

                m_TrainUnitRect.Add(new Rectangle(new Point(startPoint.X, Location.Y),
                    new Size(length, UnitStateHeight)));

                offset += width;
            }
        }

        public override void OnDraw(Graphics g)
        {
            m_DrawAction(g);
        }

        private void DrawSates(Graphics g)
        {
            RefreshState();

            for (int i = 0; i < m_TrainUnitRect.Count; i++)
            {
                g.FillRectangle(m_StateBrush[i], m_TrainUnitRect[i]);
            }
        }

        private void RefreshState()
        {
            for (int i = 0; i < m_TrainUnitStates.Count; i++)
            {
                var no = TrainUnitNoHelper.AllUnitNoes[i];
                this[no] = TrainResource.Instance.GetUnitState(no);
            }
        }

        private void DrawNames(Graphics g)
        {
            m_Lines.ForEach(e => e.OnDraw(g));

            m_UnitNameTexts.ForEach(e => e.OnDraw(g));
        }
        /// <summary>
        /// 反转显示
        /// </summary>
        public void Reversal()
        {
            m_UnitNames.Reverse();
            for (int i = 0; i < m_UnitNames.Count; i++)
            {
                m_UnitNameTexts[i].Text = m_UnitNames[i];
            }
            m_TrainUnitStates.Reverse();
            m_HasReversal = !m_HasReversal;
        }
    }
}
