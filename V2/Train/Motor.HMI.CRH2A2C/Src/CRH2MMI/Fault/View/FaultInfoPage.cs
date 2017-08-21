using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Util;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Title;
using System.Threading;

namespace CRH2MMI.Fault.View
{
    internal class FaultInfoPage : FaultInfoPageBase
    {
        public FaultInfoPage(Title.Title title, FaultInfoView parentView, TrainView trainView)
            : base(title, parentView, trainView)
        {
            m_ConstInfos.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(0, 300, 800, 27),
                Text = " 故  障  发  生  信  息",
                DrawFont = new Font(CRH2Resource.Font12.FontFamily, 12, FontStyle.Bold),
                TextColor = Color.White,
                BkColor = Color.Red,
                TextFormat =
                    new StringFormat() {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center},
            });
            var location = new Point(0, 400);
            const int interval = 4;
            m_Infos = new List<GDIRectText>();
            m_ConstInfos.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(location, DefaultTextSize),
                Text = " 故障内容：",
                DrawFont = m_DefaultTextFont,
                TextColor = Color.White,
                BkColor = Color.Red,
                TextFormat =
                    new StringFormat() {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center},
            });
            m_Infos.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(new Point(160, location.Y), new Size(600, DefaultTextSize.Height)),
                DrawFont = new Font(CRH2Resource.Font12.FontFamily, 10, FontStyle.Bold),
                TextColor = Color.Yellow,
                BkColor = Color.Black,
                TextFormat = new StringFormat() {LineAlignment = StringAlignment.Near},
                RefreshAction = o =>
                {
                    var txt = (GDIRectText) o;
                    txt.Text = m_FaultGetter.CurrentShowFaultItemInfo != null
                        ? m_FaultGetter.CurrentShowFaultItemInfo.Content
                        : string.Empty;
                },
            });

            location.Offset(0, DefaultTextSize.Height + interval);
            m_ConstInfos.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(location, DefaultTextSize),
                Text = " 保护装置：",
                DrawFont = m_DefaultTextFont,
                TextColor = Color.White,
                BkColor = Color.Red,
                TextFormat =
                    new StringFormat() {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center},
            });
            m_Infos.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(new Point(160, location.Y), new Size(600, DefaultTextSize.Height)),
                Text = "",
                DrawFont = new Font(CRH2Resource.Font12.FontFamily, 10, FontStyle.Bold),
                TextColor = Color.Yellow,
                BkColor = Color.Black,
                TextFormat = new StringFormat() {LineAlignment = StringAlignment.Near},
                RefreshAction = o =>
                {
                    var txt = (GDIRectText) o;
                    txt.Text = m_FaultGetter.CurrentShowFaultItemInfo != null
                        ? m_FaultGetter.CurrentShowFaultItemInfo.ProtectedMachine
                        : string.Empty;
                },
            });

            const int controlBtnHeight = 510;
            m_ControlBtn.Add(new CRH2Button()
            {
                OutLineRectangle = new Rectangle(new Point(10, controlBtnHeight), DefaultBtnSize),
                Text = "故障详情",
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                TextColor = Color.White,
                ButtonDownEvent = (sender, args) =>
                {
                    m_Title.TitleMenuClicking -= OnTitleMenuClick;
                    m_ParentView.GotoDetailFaultInfoPage();
                },
            });
        }


        public override void OnTitleMenuClick(object sender, TitleMenuClickArgs args)
        {
            //var iters = new List<FaultItemInfo>(m_FaultGetter.IterPath);
            //iters = iters.Distinct().ToList();

            //iters.ForEach(e => m_FaultGetter.DeleteFaultItem(e));
            //m_FaultGetter.ClearIterPath();
            //m_FaultGetter.DeleteFaultItem(m_FaultGetter.CurrentShowFaultItemInfo);
            //m_TrainView.ResetCarState();
            m_FaultGetter.ViewFinishied();

            m_Title.ResetTitleShowStrategy();

            base.OnTitleMenuClick(sender, args);

        }
    }
}
