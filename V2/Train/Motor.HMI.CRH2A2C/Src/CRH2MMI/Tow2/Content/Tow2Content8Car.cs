using System.Drawing;
using System.Linq;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;

namespace CRH2MMI.Tow2.Content
{
    class Tow2Content8Car : Tow2ContentBase
    {
        public Tow2Content8Car(Tow2Resource resource) : base(resource)
        {
        }

        protected override void InitalizeButtons()
        {
            m_CarBtns = m_Resource.Tow2CarNamse.Select((s, i) => new CRH2Button()
            {
                OutLineRectangle = new Rectangle(200 + 71 * i + (i > 1 ? 50 : 0), 530, 71, 43),
                BackImage = GlobalResource.Instance.BtnDownImage,
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                Text = s,
                TextColor = Color.White,
                ButtonDownEvent = OnCarButtonDownEvent
            }).ToList();
        }
    }

    class Tow2ContentCRH2B: Tow2Content8Car
    {
        public Tow2ContentCRH2B(Tow2Resource resource) : base(resource)
        {
        }

        protected override void InitalizeButtons()
        {
            var x = 0;
            m_CarBtns = m_Resource.Tow2CarNamse.Select((s, i) => new CRH2Button()
            {
                OutLineRectangle = new Rectangle(x += (71 + (i % 2 ==0 ? 1:0) * 20), 530, 71, 43),
                BackImage = GlobalResource.Instance.BtnDownImage,
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                Text = s,
                TextColor = Color.White,
                ButtonDownEvent = OnCarButtonDownEvent
            }).ToList();
        }
    }

    class Tow2ContentCRH2C : Tow2Content8Car
    {
        public Tow2ContentCRH2C(Tow2Resource resource) : base(resource)
        {
        }

        protected override void InitalizeButtons()
        {
            m_CarBtns = m_Resource.Tow2CarNamse.Select((s, i) => new CRH2Button()
            {
                OutLineRectangle = new Rectangle(200 + 71 * i , 530, 71, 43),
                BackImage = GlobalResource.Instance.BtnDownImage,
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                Text = s,
                TextColor = Color.White,
                ButtonDownEvent = OnCarButtonDownEvent
            }).ToList();
        }
    }
}
