using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using CRH2MMI.Common.Util;

namespace CRH2MMI.Marshing
{
    class CRH380AUnineMarshingTR1ItemView : MarshingItemView
    {
        private Rectangle m_TR2ButtonRegion = new Rectangle(129, 529, 115, 40);
        private List<CommonInnerControlBase> m_Collection;
        public CRH380AUnineMarshingTR1ItemView(Marsh parentView)
            : base(MarshingPage.MarshingTR1, parentView)
        {
        }

        public override void SwitchToThis()
        {
            this.Title.TitleText = "联挂顺序(T1车）";
        }

        public override void Init()
        {
            base.Init();

            m_MarshingTexts = new RectTextInfo[10];

            for (int i = 0; i < 10; i++)
            {
                m_MarshingTexts[i] = new RectTextInfo();
            }
            for (int i = 0; i < 5; i++)
            {
                m_MarshingTexts[i].SetRectTextInfo(25 + 62.5f * i, 145, 30, 205, 0, "", 2, 5, 2, 14);
            }
            for (int i = 0; i < 4; i++)
            {
                m_MarshingTexts[i + 5].SetRectTextInfo(608 + 62.5f * (i - 1), 145, 30, 205, 0, "", 2, 5, 2, 14);
            }
            m_MarshingTexts[9].SetRectTextInfo(733, 358, 30, 167, 0, "", 2, 5, 2, 14);

            var marshingStr = new string[10];
            marshingStr[0] = " 联挂准备";
            marshingStr[1] = " 打开头罩锁";
            marshingStr[2] = " 打开头罩";
            marshingStr[3] = " 锁住头罩";
            marshingStr[4] = " 联挂准备就绪";
            marshingStr[5] = " 机械勾锁闭";
            marshingStr[6] = " 总风管气压开关";
            marshingStr[7] = " 电连接器接通";
            marshingStr[8] = " 联挂完成";
            marshingStr[9] = " 机械勾连接不良";

            for (int i = 0; i < 10; i++)
            {
                m_MarshingTexts[i].SetRectStr(marshingStr[i]);
            }
            m_Collection = new List<CommonInnerControlBase>();
            var tmp = new Point(542, 240);
            var height = 5;
            m_Collection.Add(new Line() { StartPoint = new Point(tmp.X - height, tmp.Y - height), EndPoint = tmp, Color = Color.FromArgb(130, 130, 130) });
            m_Collection.Add(new Line() { StartPoint = new Point(tmp.X - height, tmp.Y + height), EndPoint = tmp,Color = Color.FromArgb(130,130,130)});
        }

        public override bool OnMouseDown(Point point)
        {
            if (m_TR2ButtonRegion.Contains(point))
            {
                OnMarshButtonClick(this, MarshButtonType.TR2);
                return true;
            }

            return base.OnMouseDown(point);
        }

        public override void OnDraw(Graphics g)
        {
            var tmp = (int)FValue[2];
            if (tmp <= 6 && tmp >= 0)
            {
                m_MarshingTexts[tmp].SetBK(4);
                for (int i = 0; i < tmp - 1; i++)
                {
                    m_MarshingTexts[i].SetBK(2);
                }
                for (int i = tmp + 1; i < 10; i++)
                {
                    m_MarshingTexts[i].SetBK(5);
                }

            }
            else if (tmp == 7)
            {
                for (int i = 0; i < 8; i++)
                {
                    m_MarshingTexts[i].SetBK(2);
                }

                m_MarshingTexts[8].SetBK(5);
            }
            else if (tmp == 8)
            {
                for (int i = 0; i < 7; i++)
                {
                    m_MarshingTexts[i].SetBK(2);
                }
                m_MarshingTexts[7].SetBK(5);
                m_MarshingTexts[8].SetBK(2);
            }

            g.DrawImage(Img[1], 0, 112, Img[1].Width, Img[1].Height);
            for (int i = 0; i < 10; i++)
            {
                m_MarshingTexts[i].OnDraw(g);
            }
            if (BValue[2])
            {
                g.FillRectangle(CRH2Resource.RedBrush, new RectangleF(280, 525, 240, 40));
                g.DrawString("动作顺序故障", CRH2Resource.Font24, CRH2Resource.WhiteBrush, new RectangleF(280, 525, 240, 40), CRH2Resource.DrawFormat);
            }
            m_Collection.ForEach(e => e.OnDraw(g));
        }

    }
}