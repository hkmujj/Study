using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.Wuxi.TMS.ά��
{
    [GksDataType(DataType.isMMIObjectClass)]
    class USB : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "USB����";
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
            switch (index)
            {
                case 0:
                    m_ButtonIsDown[0] = true;
                    break;
                case 1:
                    m_ButtonIsDown[1] = true;
                    break;
                case 2:
                    m_ButtonIsDown[2] = true;
                    break;
                case 3:
                    m_ButtonIsDown[3] = true;
                    break;
                case 4:
                    m_ButtonIsDown[4] = true;
                    break;
                case 5:
                    m_ButtonIsDown[5] = true;
                    break;
                case 6:
                    m_ButtonIsDown[6] = true;
                    break;
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
                    OnPost(CmdType.ChangePage, 22, 0, 0);
                    break;
                default:
                    return false;
            }
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void DrawOn(Graphics e)
        {
            for (int i = 0; i < 3; i++)
            {
                e.DrawImage(m_Images[0], m_Rects[i]);
                e.DrawString(m_Str1[i], FormatStyle.m_Font12, FormatStyle.m_WhiteBrush, m_Rects[3 + i], m_DrawFormat);
            }

            e.DrawString("��ӭʹ��USB����ϵͳ", FormatStyle.m_Font20, FormatStyle.m_WhiteBrush, m_Rects[6], m_DrawFormat);
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

            m_Rects = new RectangleF[120];

            m_Images = new Image[15];

            m_ButtonIsDown = new bool[10];

            m_Regions = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Images[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::: _rects ::::::::::::::::::::::
            for (int i = 0; i < 3; i++)
            {
                m_Rects[i] = new RectangleF(200 + i * 150, 250, 37, 37);
                m_Rects[3 + i] = new RectangleF(175 + i * 150, 300, 100, 40);
            }
            m_Rects[6] = new RectangleF(150, 100, 500, 60);

            //MoveScreen
            for (int i = 0; i < m_Rects.Length; i++)
            {
                m_Rects[i].X = (m_Rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_Rects[i].Y = (m_Rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                m_Rects[i].Width *= FormatStyle.Scale;
                m_Rects[i].Height *= FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: _regions :::::::::::::::::::::::
            m_Regions.Add(new Region(new Rectangle(500, 250, 50, 90)));
            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat m_DrawFormat = new StringFormat();

        public static StringFormat m_RightFormat = new StringFormat();

        public static StringFormat m_LeftFormat = new StringFormat();

        public String[] m_Str1 = new String[3] { "��������1", "�������2", "���˳�" };
        public String[] m_Str2 = new String[8] { "Km", "Km", "Min", "Min", "Kw.h", "Kw.h", "Kw.h", "" };

        #region:::::::::::::::::::::::::::��ֵ����::::::::::::::::::::::::::::::::

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