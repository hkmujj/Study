using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.Wuxi.TMS.ά��
{
    /// <summary>
    /// ��¼
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Password : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "��¼";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics dcGs)
        {
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public override bool mouseDown(Point nPoint)
        {
            int index = 0;
            for (; index < m_Regions.Count; ++index)
            {
                if (m_Regions[index].IsVisible(nPoint))
                    break;
            }
            if (index >= 0 && index < 13)
            {
                m_ButtonIsDown[index] = true;
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < m_Regions.Count; ++index)
            {
                if (m_Regions[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    m_ButtonIsDown[0] = false;
                    InputPassWord("1");
                    break;
                case 1:
                    m_ButtonIsDown[1] = false;
                    InputPassWord("2");
                    break;
                case 2:
                    m_ButtonIsDown[2] = false;
                    InputPassWord("3");
                    break;
                case 3:
                    m_ButtonIsDown[3] = false;
                    InputPassWord("4");
                    break;
                case 4:
                    m_ButtonIsDown[4] = false;
                    InputPassWord("5");
                    break;
                case 5:
                    m_ButtonIsDown[5] = false;
                    InputPassWord("6");
                    break;
                case 6:
                    m_ButtonIsDown[6] = false;
                    InputPassWord("7");
                    break;
                case 7:
                    m_ButtonIsDown[7] = false;
                    InputPassWord("8");
                    break;
                case 8:
                    m_ButtonIsDown[8] = false;
                    InputPassWord("9");
                    break;
                case 9:
                    m_ButtonIsDown[9] = false;
                    InputPassWord("0");
                    break;
                case 10:
                    m_ButtonIsDown[10] = false;
                    if (!m_Input.Equals(String.Empty))
                    {
                        m_Input = m_Input.Remove(m_Input.Length - 1);
                    }
                    if (!m_Output.Equals(String.Empty))
                    {
                        m_Output = m_Output.Remove(m_Output.Length - 1);
                    }
                    break;
                case 11:
                    m_ButtonIsDown[11] = false;
                    if (m_Input == "1")
                    {
                        OnPost(CmdType.ChangePage, 22, 0, 0);
                        m_Input = string.Empty;
                        m_Output = string.Empty;
                    }
                    break;
                case 12:
                    m_ButtonIsDown[12] = false;
                    break;
                default:
                    return false;
            }
            return true;
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void InputPassWord(string Numb)
        {
            if (m_Input.Length < 6) m_Input += Numb;
            if (m_Output.Length < 6) m_Output += "*";
        }

        private void DrawOn(Graphics e)
        {
            m_Keyboard.DrawKeyboard(e, ref m_ButtonIsDown, m_DrawFormat);

            e.DrawString("����������", FormatStyle.m_Font12, FormatStyle.m_WhiteBrush, m_Rects[14], m_LeftFormat);
            e.FillRectangle(FormatStyle.m_MediumGreySolidBrush, m_Rects[15]);

            e.DrawLine(FormatStyle.m_WhitePen, m_PDrawPoint[0], m_PDrawPoint[1]);
            e.DrawLine(FormatStyle.m_WhitePen, m_PDrawPoint[2], m_PDrawPoint[1]);

            e.DrawString(m_Output, FormatStyle.m_Font24, FormatStyle.m_WhiteBrush, m_Rects[16], m_DrawFormat);
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        /// <summary>
        /// ��ʼ�����ꡢ���顢����
        /// </summary>
        private void InitData()
        {
            m_DrawFormat.LineAlignment = (StringAlignment)1;
            m_DrawFormat.Alignment = (StringAlignment)1;

            m_RightFormat.LineAlignment = (StringAlignment)2;
            m_RightFormat.Alignment = (StringAlignment)1;

            m_LeftFormat.LineAlignment = (StringAlignment)0;
            m_LeftFormat.Alignment = (StringAlignment)1;

            m_PDrawPoint = new PointF[20];

            m_Rects = new RectangleF[120];

            m_Images = new Image[15];

            m_ButtonIsDown = new bool[15];

            m_Regions = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Images[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::: _rects ::::::::::::::::::::::
            m_Rects[0] = new RectangleF(0, 0, 800, 30);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    m_Rects[1 + i * 3 + j] = new RectangleF(346 + j * 127, 170 + i * 57, 125, 55);
                }
            }
            m_Rects[10] = new RectangleF(346, 341, 125, 55);
            m_Rects[11] = new RectangleF(473, 341, 252, 55);

            for (int i = 0; i < 2; i++)
            {
                m_Rects[12 + i] = new RectangleF(346 + i * 254, 420, 125, 55);
            }

            m_Rects[14] = new RectangleF(50, 250, 220, 30);

            m_Rects[15] = new RectangleF(50, 280, 220, 60);

            m_Rects[16] = new RectangleF(50, 285, 220, 60);

            //MoveScreen
            for (int i = 0; i < m_Rects.Length; i++)
            {
                m_Rects[i].X = (m_Rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_Rects[i].Y = (m_Rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                m_Rects[i].Width *= FormatStyle.Scale;
                m_Rects[i].Height *= FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: point ::::::::::::::::::::::
            m_PDrawPoint[0] = new PointF(270, 280);
            m_PDrawPoint[1] = new PointF(270, 340);
            m_PDrawPoint[2] = new PointF(50, 340);

            m_PDrawPoint[3] = new PointF(346, 170);

            //MoveScreen
            for (int i = 0; i < m_PDrawPoint.Length; i++)
            {
                m_PDrawPoint[i].X = (m_PDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_PDrawPoint[i].Y = (m_PDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: _regions :::::::::::::::::::::::
            for (int i = 0; i < 13; i++)
            {
                m_Regions.Add(new Region(m_Rects[1 + i]));
            }
            #endregion

            m_Keyboard = new NumbKeyboard(m_PDrawPoint[3]);
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat m_DrawFormat = new StringFormat();

        public static StringFormat m_RightFormat = new StringFormat();

        public static StringFormat m_LeftFormat = new StringFormat();
        
        public String[] m_Str1 = new String[9] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public String[] m_Str2 = new String[2] { "ȷ��", "ȡ��" };

        public String m_Numb = string.Empty;
        public String m_Sign = string.Empty;
        public String m_Input = string.Empty;
        public String m_Output = string.Empty;

        NumbKeyboard m_Keyboard;

        #region:::::::::::::::::::::::::::��ֵ����::::::::::::::::::::::::::::::::
        /// <summary>
        /// ���꼯
        /// </summary>
        private PointF[] m_PDrawPoint;

        /// <summary>
        /// ���ο�
        /// </summary>
        private RectangleF[] m_Rects;

        /// <summary>
        /// ͼƬ��
        /// </summary>
        private Image[] m_Images;

        /// <summary>
        /// ���Ƿ���
        /// </summary>
        private bool[] m_ButtonIsDown;

        /// <summary>
        /// �����б�
        /// </summary>
        private List<Region> m_Regions;
        #endregion#
        #endregion
    }
}