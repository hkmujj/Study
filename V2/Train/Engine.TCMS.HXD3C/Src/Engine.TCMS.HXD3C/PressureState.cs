using System.Collections.ObjectModel;
using System.Drawing;
using Engine.TCMS.HXD3C.Utils;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;

namespace Engine.TCMS.HXD3C
{
    [GksDataType(DataType.isMMIObjectClass)]
    class PressureState : baseClass
    {
        #region ::::::::::::::::::::::::init override::::::::::::::::::::::::::::::#
        public override string GetInfo()
        {
            return "���״̬����";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            //3
            nErrorObjectIndex = -1;

            m_Img = this.GetImages();

            InitData();

            return true;
        }

        #endregion#

        #region ::::::::::::::::::::::::event override:::::::::::::::::::::::::::::#

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                ButtomButtonView.Instance.ButtonStr = new ReadOnlyCollection<string>(Common.Str202);
            }
        }
        public override void paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
        }

        #endregion#

        #region :::::::::::::::::::::::: ex funes ::::::::::::::::::::::::::::::::#
        /// <summary>
        /// ��������
        /// </summary>
        private void GetValue()
        {
            for (int i = 0; i < UIObj.InBoolList.Count; i++)
            {
                m_BValue[i] = BoolList[UIObj.InBoolList[i]];
                m_OldbValue[i] = BoolOldList[UIObj.InBoolList[i]];
            }
            for (int i = 0; i < UIObj.InFloatList.Count; i++)
            {
                m_FValue[i] = FloatList[UIObj.InFloatList[i]];
            }
            for (int i = 0; i < UIObj.OutBoolList.Count; i++)
            {
                m_SetBValue[i] = BoolList[UIObj.OutBoolList[i]];
                m_SetBValueNumb[i] = UIObj.OutBoolList[i];
            }

        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="e"></param>
        private void DrawRect(Graphics e)
        {
            for (int i = 0; i < 10; i++)
            {
                if (m_BValue[0 + i * 2])
                {
                    e.FillRectangle(Common.GreenBrush, m_Rects[i]);
                    e.DrawString(m_StrRect[i], Common.Txt14FontR, Common.BlackBrush, m_Rects[i], Common.DrawFormat);
                }
                else if (m_BValue[1 + i * 2])
                {
                    e.FillRectangle(Common.RedBrush, m_Rects[i]);
                    e.DrawString(m_StrRect[i], Common.Txt14FontR, Common.WhiteBrush, m_Rects[i], Common.DrawFormat);
                }
                else
                    e.DrawString(m_StrRect[i], Common.Txt14FontR, Common.WhiteBrush, m_Rects[i], Common.DrawFormat);
                e.DrawRectangle(Common.WhitePen1, m_Rects[i]);
            }
            for (int i = 0; i < 5; i++)
            {
                e.DrawString(m_Str[i], Common.Txt14FontB, Common.WhiteBrush, m_Rects[10 + i], Common.DrawFormat);
            }
        }

        /// <summary>
        /// ��ͼ
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            DrawRect(e);
        }

        #endregion#

        #region:::::::::::::::���������ĳ�ʼ�������̺ͽ������ĳ�ʼ��:::::::::::::::#
        /// <summary>
        /// ��ʼ�������
        /// </summary>
        private void InitData()
        {
            Common.DrawFormat.Alignment = (StringAlignment)1;
            Common.DrawFormat.LineAlignment = (StringAlignment)1;

            Common.RightFormat.Alignment = (StringAlignment)2;
            Common.RightFormat.LineAlignment = (StringAlignment)1;

            Common.LeftFormat.Alignment = (StringAlignment)0;
            Common.LeftFormat.LineAlignment = (StringAlignment)1;


            #region ::::::::::::::::::::::::�����:::::::::::::::::::::::::::::#
            m_PDrawPoint[0] = new Point(15, 260);
            m_PDrawPoint[1] = new Point(15, 235);
            m_PDrawPoint[2] = new Point(0, 380);

            m_PDrawPoint[3] = new Point(142, 385);
            m_PDrawPoint[4] = new Point(142, 415);
            m_PDrawPoint[5] = new Point(0, 0);
            m_PDrawPoint[6] = new Point(0, 0);
            m_PDrawPoint[7] = new Point(0, 0);
            m_PDrawPoint[8] = new Point(0, 0);
            m_PDrawPoint[9] = new Point(0, 0);
            #endregion#

            m_Rects = new Rectangle[30];
            for (int i = 0; i < 4; i++)
            {
                m_Rects[i] = new Rectangle(5 + 65 * i, 260, 60, 25);
            }
            for (int i = 0; i < 6; i++)
            {
                m_Rects[4 + i] = new Rectangle(5 + 65 * i, 335, 60, 25);
            }
            for (int i = 0; i < 2; i++)
            {
                m_Rects[10 + i] = new Rectangle(5 + 130 * i, 235, 125, 25);
            }
            for (int i = 0; i < 3; i++)
            {
                m_Rects[12 + i] = new Rectangle(5 + 130 * i, 310, 125, 25);
            }

        }

        #endregion#

        #region :::::::::::::::::::::::: value init :::::::::::::::::::::::::::::::#

        #region:::::::::::::::::::::::::::��ֵ����::::::::::::::::::::::::::::::::::#
        /// <summary>
        /// ��ǰ���ڽ���������
        /// </summary>
        public bool[] m_BValue = new bool[50];

        /// <summary>
        /// ǰһ�����ڽ��յ�������
        /// </summary>
        public bool[] m_OldbValue = new bool[50];

        /// <summary>
        /// ���͵�������
        /// </summary>
        public bool[] m_SetBValue = new bool[10];

        /// <summary>
        /// ���͵���������boollist�е����
        /// </summary>
        public int[] m_SetBValueNumb = new int[10];

        /// <summary>
        /// ����ģ����
        /// </summary>
        public float[] m_FValue = new float[30];

        /// <summary>
        /// ���꼯
        /// </summary>
        public Point[] m_PDrawPoint = new Point[80];

        /// <summary>
        /// ͼƬ��
        /// </summary>
        public static Image[] m_Img = new Image[30];
        #endregion#
        /// <summary>
        /// ���ο�
        /// </summary>
        public Rectangle[] m_Rects;

        public string[] m_StrRect = new string[10] { "MA11", "MA12", "MA23", "MA24", "MA13", "MA14", "MA21", "MA22", "MA27", "MA28" };
        public string[] m_Str = new string[5] { "ǣ��������", "����ͨ���", "��ȴ�����", "�ͱ�", "ˮ��" };

        #endregion#
    }
}