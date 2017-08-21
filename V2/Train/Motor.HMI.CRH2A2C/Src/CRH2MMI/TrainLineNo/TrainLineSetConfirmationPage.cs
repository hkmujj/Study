using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Util;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CommonUtil.Controls;
using CRH2MMI.Common.Util;


namespace CRH2MMI.TrainLineNo
{
    /// <summary>
    /// 设定确认
    /// </summary>
    class TrainLineSetConfirmationPage : CommonInnerControlBase, ITranLinePage
    {

        private int m_Count;

        private List<CRH2Button> m_ControlBtns; 

        public EventHandler<TrainLineChangePageEventArgs> ChangePage;

        public EventHandler ConfirmaClick; 

        public override void Init()
        {
            m_ControlBtns = new List<CRH2Button>()
            {
                new CRH2Button()
                {
                    OutLineRectangle = new Rectangle(687, 113, 93, 30),
                    DownImage = GlobalResource.Instance.BtnDownImage,
                    UpImage = GlobalResource.Instance.BtnUpImage,
                    ButtonDownEvent =
                        (sender, args) =>
                            HandleUtil.OnHandle(ChangePage, this,
                                new TrainLineChangePageEventArgs(TrainLinePageType.Menu)),
                    Text = "返 回",
                    TextColor = Color.White,
                },
                new CRH2Button()
                {
                    OutLineRectangle = new Rectangle(308, 450, 179, 57),
                    DownImage = GlobalResource.Instance.BtnDownImage,
                    UpImage = GlobalResource.Instance.BtnUpImage,
                    ButtonDownEvent = (sender, args) =>
                    {
                        HandleUtil.OnHandle(ConfirmaClick, this, null);
                        HandleUtil.OnHandle(ChangePage, this, new TrainLineChangePageEventArgs(TrainLinePageType.Main));
                    },
                    Text = "确  认",
                    TextColor = Color.White,
                }
            };
        }

        public void Reset()
        {
            m_Count = 0;
        }

        public string TitleText { get { return "车次设定确认"; }}

        public override bool OnMouseDown(Point point)
        {
            return m_ControlBtns.Any(a => a.OnMouseDown(point));
        }

        public override void OnDraw(Graphics g)
        {
            var point = new PointF();
            if (m_Count > 12)
            {
                m_ControlBtns.ForEach(e => e.OnDraw(g));
                //Title.Title.TitleText = "车次设定确认";
                point.X = 143;
                point.Y = 191;
                g.DrawString("列车车次", CRH2Resource.Font32, CRH2Resource.WhiteBrush, point);
                point.X = 355;
                point.Y = 191;
                g.DrawString(TNSET.TrainLine, CRH2Resource.Font32, CRH2Resource.YellowBrush, point);
                point.X = 143;
                point.Y = 250;
                g.DrawString("始发站", CRH2Resource.Font32, CRH2Resource.WhiteBrush, point);

                point.X = 271;
                point.Y = 380;
                g.DrawString("设定内容正确的话，请按确认键", CRH2Resource.Font16, CRH2Resource.WWBrush, point);

            }
            else
            {
              
                point.X = 263;
                point.Y = 180;
                g.DrawString("正在搜索信息。", CRH2Resource.Font32, CRH2Resource.WWBrush, point);
                point.X = 337;
                point.Y = 308;
                g.DrawString("请稍等。", CRH2Resource.Font32, CRH2Resource.WWBrush, point);
                m_Count++;
            }
        }
    }
}
