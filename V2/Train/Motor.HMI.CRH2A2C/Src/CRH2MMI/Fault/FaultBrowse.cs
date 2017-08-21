using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Util;
using CRH2MMI.Resource.Images;
using CRH2MMI.WorkState;
using MMI.Facility.Interface.Attribute;

namespace CRH2MMI.Fault
{
    /// <summary>
    /// 故障一览
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class FaultBrowse : CRH2BaseClass
    {
        private Image m_BKImage;

        protected List<FaultItemInfo> FaultItemInfos { set; get; }

        private List<CRH2Button> m_ControlBtns;

        private FaultMgr.IFaultGetter m_AllFaultGetter;

        private readonly StringFormat m_PageInfFormat = new StringFormat(){LineAlignment = StringAlignment.Center,Alignment = StringAlignment.Center};

        private Title.Title m_Title;

        public override void paint(Graphics g)
        {
            OnDraw(g);

            m_ControlBtns.ForEach(e => e.OnPaint(g));
        }

        public override bool Init()
        {
            m_Title = Title.Title.Instance;

            m_BKImage = ImageResource.fault;

            m_AllFaultGetter = FaultMgr.Instance.GetterFacotry.CreateGetter(FaultGetterType.All);

            InitBtns();

            return true;
        }

        private void InitBtns()
        {
            m_ControlBtns = new List<CRH2Button>()
            {
                new CRH2Button()
                {
                    OutLineRectangle = new Rectangle(433, 530, 118, 43),
                    DownImage = GlobalResource.Instance.BtnDownImage,
                    UpImage = GlobalResource.Instance.BtnUpImage,
                    ButtonDownEvent = (sender, args) =>
                    {
                        if (m_AllFaultGetter.HasPreviousFault)
                        {
                            m_AllFaultGetter.GotoPreviousFault();
                        }
                    },
                    Text = "上一页面",
                    TextColor = Color.White,
                },
                new CRH2Button()
                {
                    OutLineRectangle = new Rectangle(433 + 118, 530, 118, 43),
                    DownImage = GlobalResource.Instance.BtnDownImage,
                    UpImage = GlobalResource.Instance.BtnUpImage,
                    ButtonDownEvent = (sender, args) =>
                    {
                        if (m_AllFaultGetter.HasNextFault)
                        {
                            m_AllFaultGetter.GotoNextFault();
                        }
                    },
                    Text = "下一页面",
                    TextColor = Color.White,
                }
            };
        }

        public override string GetInfo()
        {
            return "故障一览";
        }

        protected override bool OnMouseDown(Point point)
        {
            return m_ControlBtns.Any(a => a.OnMouseDown(point));
        }


        public void OnDraw(Graphics g)
        {
            g.DrawImage(m_BKImage, 0, 110, m_BKImage.Width, m_BKImage.Height);

            DrowCurrentFault(g);

            g.DrawString(m_AllFaultGetter.PageInfo, CRH2Resource.Font14, CRH2Resource.YellowBrush, new Rectangle(310, 524, 85, 17), m_PageInfFormat);
        }
        
        private void DrowCurrentFault(Graphics g)
        {
            //tandw 2015/05/28 update 修改点进故障一览页面没有值 Start
            var currentFault = m_AllFaultGetter.CurrentShowFaultItemInfo;
            if (currentFault == null && m_AllFaultGetter.HasFault)
            {

                currentFault = FaultMgr.Instance.AllFaultItemInfos[0];
            }
           //tandw 2015/05/28 update End  
            if (currentFault != null)
            {
                var point = new PointF { X = 139, Y = 129 };
                g.DrawString(currentFault.Name, CRH2Resource.Font16, CRH2Resource.YellowBrush, point);

                point.X = 160;
                point.Y = 219;
                g.DrawString(currentFault.Id.ToString(), CRH2Resource.Font12, CRH2Resource.YellowBrush, point);

                point.X = 180;
                point.Y = 279;
                g.DrawString(string.Join(",", currentFault.CarNumbers), CRH2Resource.Font12, CRH2Resource.YellowBrush, point);
                point.X = 546;
                point.Y = 279;
                g.DrawString(currentFault.TrainNo, CRH2Resource.Font12, CRH2Resource.YellowBrush, point);

                point.X = 156;
                point.Y = 342;
                g.DrawString(currentFault.Year, CRH2Resource.Font12, CRH2Resource.YellowBrush, point);
                point.X = 244;
                point.Y = 342;
                g.DrawString(currentFault.Month, CRH2Resource.Font12, CRH2Resource.YellowBrush, point);
                point.X = 316;
                point.Y = 342;
                g.DrawString(currentFault.Day, CRH2Resource.Font12, CRH2Resource.YellowBrush, point);
                point.X = 550;
                point.Y = 342;
                g.DrawString(currentFault.Hour, CRH2Resource.Font12, CRH2Resource.YellowBrush, point);
                point.X = 612;
                point.Y = 342;
                g.DrawString(currentFault.Minute, CRH2Resource.Font12, CRH2Resource.YellowBrush, point);
                point.X = 673;
                point.Y = 342;
                g.DrawString(currentFault.Sencond, CRH2Resource.Font12, CRH2Resource.YellowBrush, point);

                point.X = 158;
                point.Y = 405;
                g.DrawString(currentFault.Speed, CRH2Resource.Font12, CRH2Resource.YellowBrush, point);
                point.X = 543;
                point.Y = 405;
                g.DrawString(currentFault.Distance, CRH2Resource.Font12, CRH2Resource.YellowBrush, point);

                point.X = 156;
                point.Y = 465;

                g.DrawString(currentFault.Level, CRH2Resource.Font12, CRH2Resource.YellowBrush, point);

                if ((currentFault.Brake & BrakeType.Urgency) == BrakeType.Urgency)
                {
                    point.X = 432;
                    point.Y = 494;
                    g.DrawString("紧急", CRH2Resource.Font12, CRH2Resource.RedBrush, point);
                }
                if ((currentFault.Brake & BrakeType.Quickly) == BrakeType.Quickly)
                {
                    point.X = 491;
                    point.Y = 494;
                    g.DrawString("快速", CRH2Resource.Font12, CRH2Resource.WhiteBrush, point);
                }
                if ((currentFault.Brake & BrakeType.Nomarl) == BrakeType.Nomarl)
                {
                    point.X = 552;
                    point.Y = 494;
                    g.DrawString("常用", CRH2Resource.Font12, CRH2Resource.WhiteBrush, point);
                }
                if ((currentFault.Brake & BrakeType.EndureSnow) == BrakeType.EndureSnow)
                {
                    point.X = 610;
                    point.Y = 494;
                    g.DrawString("耐雪", CRH2Resource.Font12, CRH2Resource.WhiteBrush, point);
                }
            }
        }
    }
}
