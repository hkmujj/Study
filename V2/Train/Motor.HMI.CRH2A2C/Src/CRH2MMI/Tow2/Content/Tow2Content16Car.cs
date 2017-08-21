using System;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;

namespace CRH2MMI.Tow2.Content
{
    class Tow2Content16Car : Tow2ContentBase
    {
        private readonly Tow2Content16CarPageModel[] m_Tow2Content16CarPageModelList;

        private Tow2Content16CarPageModel CurrentSelectedPage
        {
            set
            {
                var old = m_CurrentSelectedPage;

                m_CurrentSelectedPage = value;

                if (old != value)
                {
                    ReselectCarBtn(value.ButtonList.First());
                    m_PageButton.Text = value.PageButtonText;
                }
            }
            get { return m_CurrentSelectedPage; }
        }

        private CRH2Button m_PageButton;
        private Tow2Content16CarPageModel m_CurrentSelectedPage;

        public Tow2Content16Car(Tow2Resource resource)
            : base(resource)
        {
            m_Tow2Content16CarPageModelList = new Tow2Content16CarPageModel[2];
        }

        public override void OnDraw(Graphics g)
        {
            CurrentSelectedPage.ButtonList.ForEach(e => e.OnDraw(g));

            m_PageButton.OnDraw(g);

            m_CarInfos.ForEach(e => e.OnPaint(g));
        }

        public override bool OnMouseDown(Point point)
        {
            return CurrentSelectedPage.ButtonList.Any(a => a.OnMouseDown(point)) || m_PageButton.OnMouseDown(point);
        }

        protected override void InitalizeButtons()
        {
            var onePageCount = m_Resource.Tow2CarNamse.Count / 2;
            m_CarBtns = m_Resource.Tow2CarNamse.Select((s, i) => new CRH2Button()
            {
                OutLineRectangle = new Rectangle(90 + 71 * (i % onePageCount), 530, 71, 43),
                BackImage = GlobalResource.Instance.BtnDownImage,
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                Text = s,
                TextColor = Color.White,
                ButtonDownEvent = OnCarButtonDownEvent
            }).ToList();

            m_PageButton = new CRH2Button()
            {
                OutLineRectangle = new Rectangle(655, 528, 120, 45),
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                TextColor = Color.White,
                ButtonDownEvent = OnPageButtonDownEvent
            };

            m_Tow2Content16CarPageModelList[0] = (new Tow2Content16CarPageModel() { ButtonList = m_CarBtns.Take(onePageCount).ToList(), PageButtonText = "后8车厢" });
            m_Tow2Content16CarPageModelList[1] = (new Tow2Content16CarPageModel() { ButtonList = m_CarBtns.Skip(onePageCount).ToList(), PageButtonText = "前8车厢" });
            CurrentSelectedPage = m_Tow2Content16CarPageModelList.First();


        }

        protected override void ReselectCarBtn(CRH2Button btn)
        {
            base.ReselectCarBtn(btn);

            var page = m_Tow2Content16CarPageModelList.FirstOrDefault(f => f.ButtonList.Contains(btn));
            if (page != null && CurrentSelectedPage != page)
            {
                CurrentSelectedPage = page;
            }
        }

        private void OnPageButtonDownEvent(object sender, EventArgs eventArgs)
        {
            CurrentSelectedPage = CurrentSelectedPage == m_Tow2Content16CarPageModelList[0] ? m_Tow2Content16CarPageModelList[1] : m_Tow2Content16CarPageModelList[0];
        }
    }
}
