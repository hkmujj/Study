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
    /// 改变确认
    /// </summary>
    class TrainLineChangedConfirmationPage : CommonInnerControlBase, ITranLinePage
    {
        private List<CRH2Button> m_ControlBtns;

        public EventHandler<TrainLineChangePageEventArgs> ChangePage;

        public EventHandler ConfirmaClick;

        private Title.Title m_Title;

        public override void Init()
        {
            m_Title = Title.Title.Instance;
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

        public override bool OnMouseDown(Point point)
        {
            return m_ControlBtns.Any(a => a.OnMouseDown(point));
        }

        public override void OnDraw(Graphics g)
        {
            m_ControlBtns.ForEach(e => e.OnDraw(g));

            //Title.Title.TitleText = "车次转换确认";
            var point=new PointF();
            point.X = 143;
            point.Y = 191;
            g.DrawString("列车车次", CRH2Resource.Font32, CRH2Resource.WhiteBrush, point);
            point.X = 355;
            point.Y = 191;
            g.DrawString(TNSET.TrainLine, CRH2Resource.Font32, CRH2Resource.YellowBrush, point);

            point.X = 271;
            point.Y = 380;
            g.DrawString("设定内容正确的话，请按确认键", CRH2Resource.Font16, CRH2Resource.WWBrush, point);
        }

        public void Reset()
        {
            
        }

        public string TitleText { get { return "车次转换确认"; }}
    }
}
