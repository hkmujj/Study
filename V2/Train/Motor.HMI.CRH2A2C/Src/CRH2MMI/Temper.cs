//using System.Drawing;
//using CRH2MMI.Common.Util;
//using CRH2MMI.Common.View.Train;
//using MMI.Facility.Interface.Attribute;
//using MMICommon.MMI;

//namespace CRH2MMI
//{
//    /// <summary>
//    /// 标准温度设定
//    /// </summary>

//    [GksDataType(DataType.isMMIObjectClass)]
//    class Temper : MMIBaseClass
//    {
//        public Image[] Img = new Image[1];
//        public PointF point = new PointF();
//        public float[] fValue = new float[40];
//        public int flagmenu = 0;

//        private TrainView m_TrainView;

//        public RectTextInfo[] rec = new RectTextInfo[40];

//        public override void paint(Graphics dcGs)
//        {
//            GetValue();
//            RefreshDate();
//            OnDraw(dcGs);
//            base.paint(dcGs);
//        }
//        public void GetValue()
//        {
//            if (this.UIObj.InFloatList.Count >= 1)
//            {
//                for (int i = 0; i < 40; i++)
//                {
//                    fValue[i] = FloatList[this.UIObj.InFloatList[i]];
//                }
//            }
//        }

//        public void RefreshDate()
//        {
//            for (int i = 0; i < 40; i++)
//            {
//                rec[i].SetRectStr(fValue[i].ToString("#,#0.0"));
//            }

//        }
//        public override bool init(ref int nErrorObjectIndex)
//        {
//            nErrorObjectIndex = -1;

//            m_TrainView = TrainView.Instance;

//            if (UIObj.ParaList.Count >= 0)
//            {
//                for (int i = 0; i < 1; i++)
//                {
//                    Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
//                }
//                InitDate();
//            }
//            else
//            {
//                nErrorObjectIndex = 0;
//                return false;
//            }

//            return true;
//        }

//        protected override bool OnMouseDown(Point nPoint)
//        {
//            OnButtonClick(nPoint.X, nPoint.Y);
//            return base.mouseDown(nPoint);
//        }

//        public void OnButtonClick(int X, int Y)
//        {
//            for (int i = 0; i < 6; i++)
//            {
//                if (101 + 100 * i <= X && X <= 101 + 100 * i + Img[0].Width && Y >= 525 && Y <= 525 + Img[0].Height)
//                {
//                    flagmenu = i;
//                }

//            }
//        }

//        public override void setRunValue(int nParaA, int nParaB, float nParaC)
//        {
//            base.setRunValue(nParaA, nParaB, nParaC);
//            if (nParaA == 2)
//            {

//                switch (nParaB)
//                {
//                    case 30:
//                        InitDate();
//                        break;
//                }
//            }
//        }

//        public override string GetInfo()
//        {
//            return "标准温度设定";
//        }

//        public void InitDate()
//        {
//            //TrainView.X = 224;
//            //TrainView.Y = 130;
//            m_TrainView.Location = new Point(224, 130);

//            for (int i = 0; i < 8; i++)
//            {
//                rec[i] = new RectTextInfo();
//                rec[i].SetRectTextInfo(224 + i * 44f, 171, 42.6f, 24, 0, "", 3, 3, 1, 12);
//                rec[i+8] = new RectTextInfo();
//                rec[i+8].SetRectTextInfo(224 + i * 44f, 195, 42.6f, 24, 0, "", 0, 3, 1, 12);

//                rec[i + 16] = new RectTextInfo();
//                rec[i + 16].SetRectTextInfo(224 + i * 44f, 231, 42.6f, 24, 0, "", 1, 3, 1, 12);

//                rec[i+24] = new RectTextInfo();
//                rec[i+24].SetRectTextInfo(224 + i * 44f, 277, 42.6f, 24, 0, "", 3, 3, 1, 12);
//                rec[i + 32] = new RectTextInfo();
//                rec[i + 32].SetRectTextInfo(224 + i * 44f, 301, 42.6f, 24, 0, "", 0, 3, 1, 12);

//            }


//        }

//        public void OnDraw(Graphics e)
//        {

            
//            e.DrawImage(Img[0], 2, 150, Img[0].Width, Img[0].Height);

//            for (int i = 0; i < 40; i++)
//            {
//                rec[i].OnDraw(e);
//            }
//        }
//    }
//}
