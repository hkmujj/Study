using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using CRH2MMI.Common.View.Train;

namespace CRH2MMI.Fault.View
{
    class FaultDetailInfoPage : FaultInfoPageBase
    {
        // ReSharper disable once InconsistentNaming
        private static readonly Rectangle m_ProessImageRect = new Rectangle(150, 235, 600, 260);

        public FaultDetailInfoPage(Title.Title title, FaultInfoView parentView, TrainView trainView)
            : base(title, parentView, trainView)
        {
            var location = new Point(10, 190);
            const int interval = 4;
            m_ConstInfos.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(location, DefaultTextSize),
                Text = " 故障内容：",
            });
            location.Offset(0, DefaultTextSize.Height + interval);

            m_ConstInfos.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(location, DefaultTextSize),
                Text = " 保护装置：",
            });

            location.Offset(0, DefaultTextSize.Height + interval);

            m_ConstInfos.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(location, DefaultTextSize),
                Text = " 处理措施：",
            });

            location.Offset(0, 225);

            m_ConstInfos.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(location, DefaultTextSize),
                Text = " 注    意：",
            });

            m_ConstInfos.ForEach(e =>
            {
                e.DrawFont = m_DefaultTextFont;
                e.TextColor = Color.White;
                e.BkColor = Color.Red;
                e.TextFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            });

            location = new Point(160, 190);
            var infoSize = new Size(600, DefaultTextSize.Height);
            m_Infos = new List<GDIRectText>()
                      {
                          new GDIRectText()
                          {
                              OutLineRectangle = new Rectangle(location,infoSize),
                              DrawFont =m_DefaultTextFont,
                              TextColor = Color.Yellow,
                              BkColor = Color.Black,
                              TextFormat = new StringFormat() { LineAlignment = StringAlignment.Near },
                              RefreshAction = o =>
                              {
                                  var txt = (GDIRectText) o;
                                  txt.Text = m_FaultGetter.CurrentShowFaultItemInfo != null ? m_FaultGetter.CurrentShowFaultItemInfo.Content : string.Empty;
                              },
                          },
                          new GDIRectText()
                          {
                              OutLineRectangle = new Rectangle(new Point(location.X, location.Y + infoSize.Height + 2), infoSize),
                              Text = "",
                              DrawFont = m_DefaultTextFont,
                              TextColor = Color.Yellow,
                              BkColor = Color.Black,
                              TextFormat = new StringFormat() { LineAlignment = StringAlignment.Near },
                              RefreshAction = o =>
                              {
                                  var txt = (GDIRectText) o;
                                  txt.Text = m_FaultGetter.CurrentShowFaultItemInfo != null ? m_FaultGetter.CurrentShowFaultItemInfo.ProtectedMachine : string.Empty;
                              },
                          }
                      };
        }

        public override void OnDraw(Graphics g)
        {
            if (m_FaultGetter.CurrentShowFaultItemInfo != null && m_FaultGetter.CurrentShowFaultItemInfo.ProessInfoImage != null)
            {
                g.DrawImage(m_FaultGetter.CurrentShowFaultItemInfo.ProessInfoImage, m_ProessImageRect);
            }


            base.OnDraw(g);
        }
    }
}
