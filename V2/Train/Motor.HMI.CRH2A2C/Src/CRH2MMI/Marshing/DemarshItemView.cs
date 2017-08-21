using System.Drawing;
using CRH2MMI.Common.Util;

namespace CRH2MMI.Marshing
{
    class DemarshItemView : MarshItemView
    {
        private RectTextInfo[] m_DemarshingTexts;

        public DemarshItemView(Marsh parentView)
            : base(MarshingPage.Demarshing, parentView)
        {
        }

        public override void Init()
        {
            base.Init();

            m_DemarshingTexts = new RectTextInfo[6];
            //3
            for (int i = 0; i < 6; i++)
            {
                m_DemarshingTexts[i] = new RectTextInfo();
            }
            for (int i = 0; i < 2; i++)
            {
                m_DemarshingTexts[i].SetRectTextInfo(28 + 62.5f * i, 145, 30, 205, 0, "", 2, 5, 2, 14);
            }
            for (int i = 0; i < 4; i++)
            {
                m_DemarshingTexts[i + 2].SetRectTextInfo(565 + 62.5f * i, 145, 30, 205, 0, "", 2, 5, 2, 14);
            }

            var str3 = new string[6];
            str3[0] = " 解联";
            str3[1] = " 解联准备就绪";
            str3[2] = " 打开头罩锁";
            str3[3] = " 关上头罩";
            str3[4] = " 锁住头罩";
            str3[5] = " 解联完毕";


            for (int i = 0; i < 6; i++)
            {
                m_DemarshingTexts[i].SetRectStr(str3[i]);
            }
        }

        public override void OnDraw(Graphics g)
        {
            var tmp = (int)FValue[4];
            if (tmp <= 4 && tmp >= 0)
            {
                m_DemarshingTexts[tmp].SetBK(4);
                for (int i = 0; i < tmp - 1; i++)
                {
                    m_DemarshingTexts[i].SetBK(2);
                }
                for (int i = tmp + 1; i < 6; i++)
                {
                    m_DemarshingTexts[i].SetBK(5);
                }

            }
            else if (tmp == 5)
            {
                for (int i = tmp + 1; i < 6; i++)
                {
                    m_DemarshingTexts[i].SetBK(2);
                }
            }

            g.DrawImage(Img[3], 0, 112, Img[3].Width, Img[3].Height);
            for (int i = 0; i < 6; i++)
            {
                m_DemarshingTexts[i].OnDraw(g);

            }
            if (BValue[4])
            {
                g.FillRectangle(CRH2Resource.RedBrush, new RectangleF(280, 525, 240, 40));
                g.DrawString("动作顺序故障", CRH2Resource.Font24, CRH2Resource.WhiteBrush, new RectangleF(280, 525, 240, 40), CRH2Resource.DrawFormat);
            }
        }
    }
}