using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Urban.Wuxi.TrainRadio
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class Black : baseClass
    {
        public static SolidBrush m_Background = new SolidBrush(Color.FromArgb(0, 0, 0, 0));
        public static SolidBrush m_BackgroundBrush0 = new SolidBrush(Color.FromArgb(0, 0, 0, 0));
        public static SolidBrush m_BackgroundBrush1 = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
        public static SolidBrush m_BackgroundBrush2 = new SolidBrush(Color.FromArgb(200, 0, 0, 0));
        public static SolidBrush m_BlackBrush = new SolidBrush(Color.Black);
        public static Pen m_BlackPen = new Pen(Color.Black, 2f);
        public static Pen m_BlackPen1 = new Pen(Color.Black, 1f);
        public bool[] m_BValue = new bool[50];
        public static StringFormat m_DrawFormat = new StringFormat();
        public float[] m_FValue = new float[20];
        public static Pen m_GreenPen = new Pen(Color.Green, 2f);
        public static Pen m_GreenPen1 = new Pen(Color.Green, 1f);
        public static Image[] m_Img = new Image[30];
        private bool[] m_IsDown = new bool[0x1c];
        public static StringFormat m_LeftFormat = new StringFormat();
        public bool[] m_OldbValue = new bool[50];
        public Point[] m_PDrawPoint = new Point[50];
        private Size m_RectSize = new Size(20, 0x2d);
        public static Pen m_RedPen = new Pen(Color.Red, 2f);
        public static StringFormat m_RightFormat = new StringFormat();
        public bool[] m_SetBValue = new bool[10];
        public int[] m_SetBValueNumb = new int[10];
        public Font m_TestFont10 = new Font("宋体", 10f);
        public Font m_TestFont12 = new Font("宋体", 12f);
        public Font m_TestFont14 = new Font("宋体", 14f);
        public Font m_TestFont20 = new Font("宋体", 20f);
        public static SolidBrush m_YellowBrush = new SolidBrush(Color.Yellow);
        public static Pen m_YellowPen1 = new Pen(Color.Yellow, 1f);
        public static Pen m_YellowPen2 = new Pen(Color.Yellow, 2f);
        public static Pen m_YellowPen3 = new Pen(Color.Yellow, 3f);


        public void ButtonDown()
        {
            switch (OnButtonClick(1))
            {
                case 1:
                    m_IsDown[1] = true;
                    break;

                case 2:
                    m_IsDown[2] = true;
                    break;

                case 3:
                    m_IsDown[3] = true;
                    break;

                case 4:
                    m_IsDown[4] = true;
                    break;

                case 5:
                    m_IsDown[5] = true;
                    break;
            }
            switch (OnButtonClick(2))
            {
                case 1:
                    m_IsDown[1] = false;
                    break;

                case 2:
                    m_IsDown[2] = false;
                    break;

                case 3:
                    m_IsDown[3] = false;
                    break;

                case 4:
                    m_IsDown[4] = false;
                    break;

                case 5:
                    m_IsDown[5] = false;
                    break;
            }
        }



        private void DrawOn(Graphics e)
        {
            DrawPicButton(e);
        }

        private void DrawPicButton(Graphics e)
        {
        }



        public override string GetInfo()
        {
            return "黑屏";
        }



        private void GetValue()
        {
            int num;
            for (num = 0; num < UIObj.InBoolList.Count; num++)
            {
                m_BValue[num] = BoolList[UIObj.InBoolList[num]];
                m_OldbValue[num] = BoolOldList[UIObj.InBoolList[num]];
            }
            for (num = 0; num < UIObj.InFloatList.Count; num++)
            {
                m_FValue[num] = FloatList[UIObj.InFloatList[num]];
            }
            for (num = 0; num < UIObj.OutBoolList.Count; num++)
            {
                m_SetBValue[num] = BoolList[UIObj.OutBoolList[num]];
                m_SetBValueNumb[num] = UIObj.OutBoolList[num];
            }
            ButtonDown();
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            nErrorObjectIndex = -1;
            if (UIObj.ParaList.Count >= 0)
            {
                for (int i = 0; i < UIObj.ParaList.Count; i++)
                {
                    m_Img[i] = Image.FromFile(RecPath + @"\" + UIObj.ParaList[i]);
                }
            }
            else
            {
                nErrorObjectIndex = 0;
                return false;
            }
            InitData();
            return true;
        }

        private void InitData()
        {
            m_DrawFormat.Alignment = StringAlignment.Center;
            m_DrawFormat.LineAlignment = StringAlignment.Center;
            m_RightFormat.Alignment = StringAlignment.Far;
            m_RightFormat.LineAlignment = StringAlignment.Center;
            m_LeftFormat.Alignment = StringAlignment.Near;
            m_LeftFormat.LineAlignment = StringAlignment.Center;
            m_PDrawPoint[0] = new Point(0, 100);
            m_PDrawPoint[1] = new Point(0, 200);
            m_PDrawPoint[2] = new Point(0, 300);
            m_PDrawPoint[3] = new Point(0, 400);
            m_PDrawPoint[4] = new Point(0, 500);
            m_PDrawPoint[5] = new Point(0x41, 0x7d);
            m_PDrawPoint[6] = new Point(0x41, 0x69);
            m_PDrawPoint[7] = new Point(0xeb, 0x69);
            m_PDrawPoint[8] = new Point(0x41, 0xc3);
            m_PDrawPoint[9] = new Point(0x69, 0xe1);
            m_PDrawPoint[10] = new Point(0x3f, 400);
            m_PDrawPoint[11] = new Point(0x41, 0x191);
            m_PDrawPoint[12] = new Point(0x41, 210);
            m_PDrawPoint[13] = new Point(0x41, 0x113);
            m_PDrawPoint[14] = new Point(0x41, 340);
            m_PDrawPoint[15] = new Point(0x66, 210);
            m_PDrawPoint[0x10] = new Point(0x84, 210);
            m_PDrawPoint[0x11] = new Point(200, 210);
            m_PDrawPoint[0x12] = new Point(230, 210);
            m_PDrawPoint[0x13] = new Point(0x113, 210);
        }



        public int OnButtonClick(int type)
        {
            int num;
            if (type == 1)
            {
                for (num = 0; num < 0x1c; num++)
                {
                    if (m_BValue[num])
                    {
                        return num;
                    }
                }
            }
            if (type == 2)
            {
                for (num = 0; num < 0x1c; num++)
                {
                    if (!(m_BValue[num] || !m_OldbValue[num]))
                    {
                        return num;
                    }
                }
            }
            else if (type == 3)
            {
                for (int i = 0; i < 0x1c; i++)
                {
                    if (!(!m_BValue[i] || m_OldbValue[i]))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public override void paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
        }
    }
}

