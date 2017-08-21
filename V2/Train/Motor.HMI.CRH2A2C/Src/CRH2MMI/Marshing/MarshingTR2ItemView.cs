using System.Drawing;
using CRH2MMI.Common.Util;

namespace CRH2MMI.Marshing
{
    class MarshingTR2ItemView : MarshingItemView
    {
        private Rectangle m_TR1ButtonRegion = new Rectangle(11, 529, 115, 40);

        public MarshingTR2ItemView(Marsh parentView)
            : base(MarshingPage.MarshingTR2, parentView)
        {
        }

        public override void SwitchToThis()
        {
            this.Title.TitleText = "联挂顺序(T2车）";
        }

        public override void Init()
        {
            base.Init();

            m_MarshingTexts = new RectTextInfo[6];

            //2
            for (int i = 0; i < 6; i++)
            {
                m_MarshingTexts[i] = new RectTextInfo();
            }
            for (int i = 0; i < 5; i++)
            {
                m_MarshingTexts[i].SetRectTextInfo(39 + 62.5f * i, 145, 30, 205, 0, "", 2, 5, 2, 14);
            }
            m_MarshingTexts[5].SetRectTextInfo(725, 145, 30, 205, 0, "", 2, 5, 2, 14);

            var str2 = new string[6];
            str2[0] = " 联挂准备";
            str2[1] = " 打开头罩锁";
            str2[2] = " 打开头罩";
            str2[3] = " 锁住头罩";
            str2[4] = " 联挂准备就绪";
            str2[5] = " 联挂完成";


            for (int i = 0; i < 6; i++)
            {
                m_MarshingTexts[i].SetRectStr(str2[i]);
            }
        }

        public override bool OnMouseDown(Point point)
        {
            if (m_TR1ButtonRegion.Contains(point))
            {
                OnMarshButtonClick(this, MarshButtonType.TR1);
                return true;
            }

            return base.OnMouseDown(point);
        }

        public override void OnDraw(Graphics g)
        {
          var  tmp = (int)FValue[3];
            if (tmp <= 4 && tmp >= 0)
            {
                m_MarshingTexts[tmp].SetBK(4);
                for (int i = 0; i < tmp - 1; i++)
                {
                    m_MarshingTexts[i].SetBK(2);
                }
                for (int i = tmp + 1; i < 6; i++)
                {
                    m_MarshingTexts[i].SetBK(5);
                }

            }
            else if (tmp == 5)
            {
                for (int i = tmp + 1; i < 6; i++)
                {
                    m_MarshingTexts[i].SetBK(2);
                }
            }

            g.DrawImage(Img[2], 0, 112, Img[2].Width, Img[2].Height);
            for (var i = 0; i < 6; i++)
            {
                m_MarshingTexts[i].OnDraw(g);

            }
            if (BValue[3])
            {
                g.FillRectangle(CRH2Resource.RedBrush, new RectangleF(280, 525, 240, 40));
                g.DrawString("动作顺序故障", CRH2Resource.Font24, CRH2Resource.WhiteBrush, new RectangleF(280, 525, 240, 40), CRH2Resource.DrawFormat);
            }
        }
    }
}