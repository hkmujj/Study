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

namespace CRH2MMI.CarInfo
{
    internal class CarInfoPage2 : CarInfoPageBase
    {
        private static readonly Size Page2RecTextSize = new Size(50, 24);

        /// <summary>
        /// 第二页的标题
        /// </summary>
        private List<GDIRectText> m_Page2Titles;

        private List<GridLine> m_StateGrid;

        /// <summary>
        /// 上一界面的按键
        /// </summary>
        private GDIButton m_PreBtn;


        public CarInfoPage2(CarInfo carInfo)
            : base(carInfo)
        {
            InnerTextFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            GlobalEvent.Instance.ReversalChanged += (sender, args) => m_StateGrid.Reverse();
        }

        public override void Init()
        {
            m_StateGrid = m_CarInfo.Resource.CarInfoConfig.Page2Grid.Select(s => GDIGridLineHelper.CreateGridLine(s, InitInnerContrl))
                .ToList();

            m_PreBtn = new CRH2Button()
            {
                OutLineRectangle = new Rectangle(580, 515, 150, 45),
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                TextColor = Color.White,
                Text = "上一页面",
                ButtonDownEvent =
                    (sender, args) =>
                        HandleUtil.OnHandle(ChangePageEvent, this, new ChangeCarInfoPageEventArgs(ChangePageType.ToPage1))
            };

            InitTitles();
        }

        private void InitTitles()
        {
            m_Page2Titles = CreateTitles(m_StateGrid.First(), m_CarInfo.Resource.CarInfoConfig.Page2Grid.First(), 80);

        }

        public override bool OnMouseDown(Point point)
        {
            return m_PreBtn.OnMouseDown(point);
        }

        public override void OnDraw(Graphics g)
        {
            m_StateGrid.ForEach(e => e.OnPaint(g));

            m_Page2Titles.ForEach(e => e.OnDraw(g));

            m_PreBtn.OnDraw(g);
        }
    }
}
