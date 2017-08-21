using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;

namespace CRH2MMI.LightTrans
{
    class LightTransView : CommonInnerControlBase
    {
        private readonly Trans m_Trans;

        private readonly List<LightTransViewPage> m_LightTransViewPages;

        public LightTransViewPage CurrentPage { get; private set; }

        private readonly CRH2Button m_PageSelectButton;

        public event Action<LightTransView> PageChanged;

        public LightTransView(Trans trans)
        {
            m_Trans = trans;
            m_LightTransViewPages = GlobalInfo.CurrentCRH2Config.LightTrainsConfig.LightTransPages
                                              .Select(s => new LightTransViewPage(m_Trans, s)).ToList();

            if (GlobalInfo.CurrentCRH2Config.LightTrainsConfig.LightTransPages.Count > 1)
            {
                m_PageSelectButton = new CRH2Button()
                                   {
                                       OutLineRectangle = new Rectangle(660, 515, 100, 45),
                                       DownImage = GlobalResource.Instance.BtnDownImage,
                                       UpImage = GlobalResource.Instance.BtnUpImage,
                                       ButtonDownEvent = OnPageButtonDown,
                                       Text = "后 8 车厢",
                                       TextColor = Color.White,
                                   };
            }

            //m_LightTransViewPages = new List<LightTransViewPage>() { new LightTransViewPage(m_Trans) };
            GlobalEvent.Instance.ReversalChanged += OnReversalChanged;
            ReselectPage(m_LightTransViewPages.First());
        }

        private void OnPageButtonDown(object sender, EventArgs e)
        {
            ReselectPage(m_LightTransViewPages[0] == CurrentPage ? m_LightTransViewPages[1] : m_LightTransViewPages[0]);
        }

        [Obsolete]
        private void OnReversalChanged(object sender, EventArgs eventArgs)
        {
            CurrentPage.OnReversalChanged(sender, eventArgs);
        }

        public void ReselectPage()
        {
            ReselectPage(m_LightTransViewPages.First());
        }

        protected void ReselectPage(LightTransViewPage page)
        {
            if (CurrentPage != null)
            {

            }
            CurrentPage = page;
            if (m_PageSelectButton != null)
            {
                m_PageSelectButton.Text = CurrentPage.PageText;
            }
            if (PageChanged != null)
            {
                PageChanged(this);
            }
        }

        public LightTransViewPage GetFirstPage()
        {
            return m_LightTransViewPages.First();
        }

        public override void OnDraw(Graphics g)
        {
            if (m_PageSelectButton != null)
            {
                m_PageSelectButton.OnDraw(g);
            }
            CurrentPage.OnDraw(g);
        }

        public override bool OnMouseDown(Point point)
        {
            return m_PageSelectButton != null && m_PageSelectButton.OnMouseDown(point);
        }
    }
}
