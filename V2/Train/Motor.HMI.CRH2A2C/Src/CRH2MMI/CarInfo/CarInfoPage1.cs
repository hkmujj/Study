using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Util;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Util;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using CommonUtil.Controls.Grid.GridLine;
using CRH2MMI.Resource.Images;


namespace CRH2MMI.CarInfo
{
    internal class CarInfoPage1 : CarInfoPageBase
    {
        /// <summary>
        /// 第一页上
        /// </summary>
        private List<GridLine> m_ActStateGrid;

        /// <summary>
        /// 第一页下
        /// </summary>
        private List<GridLine> m_StateGrid;

        /// <summary>
        /// 第一页的标题
        /// </summary>
        private List<GDIRectText> m_Page1Titles;

        /// <summary>
        /// 下一页的按键
        /// </summary>
        private GDIButton m_NextBtn;


        public CarInfoPage1(CarInfo carInfo)
            : base(carInfo)
        {
            InnerTextFormat = new StringFormat()
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.DirectionRightToLeft
            };
            GlobalEvent.Instance.ReversalChanged += OnReversalChanged;
        }

        private void OnReversalChanged(object sender, EventArgs e)
        {
            m_ActStateGrid.Reverse();
            m_StateGrid.Reverse();
        }


        public override void Init()
        {
            InitGrid();

            InitTitle();

            m_NextBtn = new CRH2Button()
            {
                OutLineRectangle = new Rectangle(580, 515, 150, 45),
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                TextColor = Color.White,
                Text = "下一页面",
                ButtonDownEvent =
                    (sender, args) =>
                        HandleUtil.OnHandle(ChangePageEvent, this, new ChangeCarInfoPageEventArgs(ChangePageType.ToPage2))
            };
        }

        public override bool OnMouseDown(Point point)
        {
            return m_NextBtn.OnMouseDown(point);
        }

        private void InitTitle()
        {
            m_Page1Titles = new List<GDIRectText>();

            m_Page1Titles.AddRange(CreateTitles(m_ActStateGrid.First(), m_CarInfo.Resource.CarInfoConfig.Page1UpGrid.First()));
            m_Page1Titles.AddRange(CreateTitles(m_StateGrid.First(), m_CarInfo.Resource.CarInfoConfig.Page1DownGrid.First()));

        }

        private void InitGrid()
        {

            m_ActStateGrid = m_CarInfo.Resource.CarInfoConfig.Page1UpGrid.Select(
                s => GDIGridLineHelper.CreateGridLine(s, InitInnerContrl)).ToList();

            m_StateGrid = m_CarInfo.Resource.CarInfoConfig.Page1DownGrid.Select(
                s => GDIGridLineHelper.CreateGridLine(s, InitInnerContrl)).ToList();
        }
        
        public override void OnDraw(Graphics g)
        {
            g.DrawImage(ImageResource.carinfo, 200, 185, ImageResource.carinfo.Width, ImageResource.carinfo.Height);

            m_ActStateGrid.ForEach(e => e.OnPaint(g));
            m_StateGrid.ForEach(e => e.OnPaint(g));

            m_Page1Titles.ForEach(e => e.OnDraw(g));

            m_NextBtn.OnDraw(g);
        }
    }
}
