using System;
using System.Drawing;
using CRH2MMI.Common;
using CRH2MMI.Common.Util;
using CRH2MMI.Common.View.Train;
using System.Threading;
using CRH2MMI.Resource.Images;
using MMI.Facility.Interface.Attribute;

namespace CRH2MMI
{
    //

    /// <summary>
    /// 试运行
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Preoperation : CRH2BaseClass
    {
        PointF m_Point = new PointF();
        int flag = 3;
        String[] strb = new String[8];

        RectTextInfo[] rec1 = new RectTextInfo[32];

        float[] fValue = new float[35];
        bool[] bValue = new bool[6];
        Timer m_timer;
        float time = 0;
        bool start = false;
        bool bflag = false;
        private TrainView m_TrainView;

        private void TimerProc(object state)
        {
            // The state object is the Timer object.
            if (start && bflag)
            {
                time = time + 1;
            }
            bflag = !bflag;
        }

        public override void paint(Graphics g)
        {
            GetValue();
            RefreshDate();
            OnDraw(g);
        }

        public void GetValue()
        {
            if (this.UIObj.InFloatList.Count >= 1)
            {
                for (int i = 0; i < 35; i++)
                {
                    fValue[i] = FloatList[this.UIObj.InFloatList[i]];
                }
            }
            if (this.UIObj.InBoolList.Count >= 1)
            {
                for (int i = 0; i < 6; i++)
                {
                    bValue[i] = BoolList[this.UIObj.InBoolList[i]];
                }
            }
        }

        public void RefreshDate()
        {
            for (int i = 0; i < 32; i++)
            {
                rec1[i].SetRectStr(((int)fValue[i + 1]).ToString());
            }
        }

        public override bool Init()
        {

            m_TrainView = TrainView.Instance;

            InitDate();

            return true;
        }

        protected override bool OnMouseDown(Point point)
        {
            OnButtonClick(point.X, point.Y);
            return base.mouseDown(point);
        }

        public void OnButtonClick(int x, int y)
        {

            for (int i = 0; i < 2; i++)
            {
                if (1 + 120 * i <= x && x <= 1 + 120 * i + 120 && y >= 529 && y <= 529 + 42)
                {
                    if (i == 0)
                    {
                        start = true;
                    }
                    else if (i == 1)
                    {
                        start = false;
                    }
                }

            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == 2)
            {

                switch (nParaB)
                {
                    case 30:
                        InitDate();
                        break;
                }
            }
        }

        public override string GetInfo()
        {
            return "试运行";
        }

        public void InitDate()
        {
        //    TrainView.X = 225;
        //    TrainView.Y = 125;
            m_TrainView.Location = new Point(225, 125);

            strb[0] = "开始记录";
            strb[1] = "记录中断";

            for (int i = 0; i < 32; i++)
            {
                rec1[i] = new RectTextInfo();
            }
            for (int i = 0; i < 6; i++)
            {
                rec1[i].SetRectTextInfo(269 + i * 44f, 194, 42.6f, 24, 0, "", 3, 3, 1, 12);
                rec1[i + 6].SetRectTextInfo(269 + i * 44f, 218, 42.6f, 24, 0, "", 0, 3, 1, 12);
                rec1[i + 12].SetRectTextInfo(269 + i * 44f, 284, 42.6f, 24, 0, "", 3, 3, 1, 12);
                rec1[i + 18].SetRectTextInfo(269 + i * 44f, 308, 42.6f, 24, 0, "", 0, 3, 1, 12);
            }
            for (int i = 0; i < 8; i++)
            {
                rec1[i + 24].SetRectTextInfo(225 + i * 44f, 345, 42.6f, 24, 0, "", 0, 3, 1, 12);
            }

            m_timer = new Timer(new TimerCallback(TimerProc));
            m_timer.Change(1000, 1000);
        }

        public void OnDraw(Graphics e)
        {

            e.DrawImage(ImageResource.preoperation, 0, 162, ImageResource.preoperation.Width, ImageResource.preoperation.Height);

            for (int i = 0; i < 32; i++)
            {
                rec1[i].OnDraw(e);
            }



            for (int i = 0; i < 2; i++)
            {
                if (flag == i)
                {
                    e.DrawImage(ImageResource.bluedown, 1 + 120 * i, 529, 120, 42);
                    e.DrawString(strb[i], CRH2Resource.Font12, CRH2Resource.BlackBrush, new Rectangle(1 + 120 * i, 529, 120, 42), CRH2Resource.DrawFormat);
                }
                else
                {
                    e.DrawImage(ImageResource.blueb, 1 + 120 * i, 529, 120, 42);
                    e.DrawString(strb[i], CRH2Resource.Font12, CRH2Resource.WhiteBrush, new Rectangle(1 + 120 * i, 529, 120, 42), CRH2Resource.DrawFormat);
                }
            }

            e.DrawImage(ImageResource.blueb, 672, 529, 120, 42);
            e.DrawString("设  定", CRH2Resource.Font12, CRH2Resource.WhiteBrush, new Rectangle(672, 529, 120, 42), CRH2Resource.DrawFormat);

            e.FillRectangle(CRH2Resource.GreenBrush, 140, 478, time * 608 / 2000f, 23);
            e.DrawString(((int)time).ToString(), CRH2Resource.Font14, CRH2Resource.YellowBrush, new Rectangle(24, 482, 72, 16), CRH2Resource.RightFormat);

            m_Point.X = 164;
            m_Point.Y = 416;
            e.DrawString(fValue[33].ToString("#,#0.0"), CRH2Resource.Font14, CRH2Resource.YellowBrush, new Rectangle(163, 416, 60, 15), CRH2Resource.RightFormat);

            e.DrawString(((int)fValue[34]).ToString(), CRH2Resource.Font14, CRH2Resource.YellowBrush, new Rectangle(482, 416, 115, 15), CRH2Resource.RightFormat);



            m_Point.X = 50;
            m_Point.Y = 390;

            e.DrawString("级", CRH2Resource.Font12, CRH2Resource.WhiteBrush, m_Point);

            m_Point.X = 90;
            m_Point.Y = 390;

            if ((int)fValue[0] == 0)
                e.DrawString("OFF", CRH2Resource.Font12, CRH2Resource.YellowBrush, m_Point);
            else if ((int)fValue[0] < 11)
                e.DrawString("P" + ((int)fValue[0]).ToString(), CRH2Resource.Font12, CRH2Resource.YellowBrush, m_Point);

            m_Point.X = 178;
            m_Point.Y = 390;
            if (bValue[0])
                e.DrawString("恒速", CRH2Resource.Font12, CRH2Resource.YellowBrush, m_Point);

            m_Point.X = 312;
            m_Point.Y = 390;
            if (bValue[1])
                e.DrawString("制    动", CRH2Resource.Font12, CRH2Resource.WhiteBrush, m_Point);

            m_Point.X = 392;
            m_Point.Y = 390;
            if (bValue[2])
                e.DrawString("紧急", CRH2Resource.Font12, CRH2Resource.RedBrush, m_Point);

            m_Point.X = 450;
            m_Point.Y = 390;
            if (bValue[3])
                e.DrawString("快速", CRH2Resource.Font12, CRH2Resource.YellowBrush, m_Point);

            m_Point.X = 510;
            m_Point.Y = 390;
            if (bValue[4])
                e.DrawString("常用", CRH2Resource.Font12, CRH2Resource.YellowBrush, m_Point);

            m_Point.X = 570;
            m_Point.Y = 390;
            if (bValue[5])
                e.DrawString("耐雪", CRH2Resource.Font12, CRH2Resource.YellowBrush, m_Point);

        }
    }
}
