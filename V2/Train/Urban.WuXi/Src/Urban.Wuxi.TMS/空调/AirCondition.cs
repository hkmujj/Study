using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.Wuxi.TMS.�յ�
{
    /// <summary>
    /// �յ�����
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class AirCondition : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "�յ�����";
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

        /// <summary>
        /// �յ��趨�¶�
        /// </summary>
        private int m_SetTemp = 0;
        private int m_SetModel = 0;
        private string m_CurrentDisplay;
        private string m_PredictDisplay;
        private bool m_IsSetModel = false;
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
                    ChangeBtnState(0, 0);
                    m_IsSetModel = false;
                    m_SetTemp = 22;
                    m_PredictDisplay = m_SetTemp.ToString("0") + "��";
                    break;
                case 1:
                    ChangeBtnState(1, 0);
                    m_IsSetModel = false;
                    m_SetTemp = 24;
                    m_PredictDisplay = m_SetTemp.ToString("0") + "��";
                    break;
                case 2:
                    ChangeBtnState(2, 0);
                    m_IsSetModel = false;
                    m_SetTemp = 26;
                    m_PredictDisplay = m_SetTemp.ToString("0") + "��";
                    break;
                case 3:
                    ChangeBtnState(3, 0);
                    m_IsSetModel = false;
                    m_SetTemp = 28;
                    m_PredictDisplay = m_SetTemp.ToString("0") + "��";
                    break;
                case 4:
                    ChangeBtnState(4, 0);
                    m_IsSetModel = true;
                    m_SetModel = 0;
                    m_PredictDisplay = "ͨ��";
                    break;
                case 5:
                    ChangeBtnState(5, 0);
                    m_IsSetModel = true;
                    m_SetModel = 1;
                    m_PredictDisplay = string.Empty;
                    break;
                case 6:
                    ChangeBtnState(6, 0);
                    m_IsSetModel = true;
                    m_SetModel = 2;
                    m_PredictDisplay = string.Empty;
                    break;
                case 7:
                    m_ButtonIsDown[7] = true;
                    break;
                case 8:
                    m_ButtonIsDown[8] = true;
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
                case 7:
                    m_ButtonIsDown[7] = false;
                    ChangeBtnState(0, 1);
                    if (!m_IsSetModel)
                        OnPost(CmdType.SetFloatValue, 202, 0, m_SetTemp);
                    else
                        OnPost(CmdType.SetBoolValue, 1673 + m_SetModel, 1, 0);
                    m_CurrentDisplay = !string.IsNullOrEmpty(m_PredictDisplay) ? m_PredictDisplay : "δ֪";
                    break;
                case 8:
                    m_ButtonIsDown[8] = false;
                    break;
            }
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        /// <summary>
        /// �յ��趨��ť״̬�仯
        /// </summary>
        /// <param name="btnID"></param>
        /// <param name="typeID"></param>
        private void ChangeBtnState(int btnID, int typeID)
        {
            if (typeID == 0)
            {
                //6��������㰴��ÿ��ֻ��һ�������ǰ���״̬
                for (int i = 0; i < 7; i++)
                {
                    m_ButtonIsDown[i] = false;
                }
                m_ButtonIsDown[btnID] = true;
            }
            else if (typeID == 1)
            {
                //�����趨��ť��6����ť��λ
                for (int i = 0; i < 7; i++)
                {
                    m_ButtonIsDown[i] = false;
                }
                //��շ���λ
                OnPost(CmdType.SetFloatValue, 202, 0, 0);
                for (int i = 0; i < 3; i++)
                {
                    OnPost(CmdType.SetBoolValue, 1673 + i, 0, 0);
                }
            }
        }

        /// <summary>
        /// �����
        /// </summary>
        /// <param name="e"></param>
        private void DrawFrame(Graphics e)
        {
            for (int i = 0; i < 61; i++)
            {
                e.DrawRectangle(FormatStyle.m_WhitePen, m_Rects[i].X, m_Rects[i].Y, m_Rects[i].Width, m_Rects[i].Height);
            }

            for (int i = 0; i < 9; i++)
            {
                e.DrawString(FormatStyle.m_Str8[i], FormatStyle.m_Font10B,
                    FormatStyle.m_WhiteBrush, m_Rect2[i], m_DrawFormat);
            }
            for (int i = 0; i < 2; i++)
            {
                e.DrawRectangle(FormatStyle.m_WhitePen, m_Rect2[i + 4].X, m_Rect2[i + 4].Y, m_Rect2[i + 4].Width, m_Rect2[i + 4].Height);
            }

            //for (int i = 0; i < 6; i++)
            //{
            //    e.DrawLine(FormatStyle.WhitePen, _pDrawPoint[i], _pDrawPoint[i + 6]);
            //}
        }

        /// <summary>
        /// ��ֵ
        /// </summary>
        /// <param name="e"></param>
        private void DrawValue(Graphics e)
        {
            //�����¶�/�����¶�/�յ�Ŀ���¶�/
            for (var i = 0; i < 6; i++)
            {
                e.DrawString(FloatList[m_FoolatIds[0] + i].ToString("0") + "��", FormatStyle.m_Font12B,
                    FormatStyle.m_WhiteBrush, m_Rects[7 + i], m_DrawFormat);
                e.DrawString(FloatList[m_FoolatIds[1] + i].ToString("0") + "��", FormatStyle.m_Font12B,
                    FormatStyle.m_WhiteBrush, m_Rects[13 + i], m_DrawFormat);
                e.DrawString(Convert.ToInt32(FloatList[m_FoolatIds[2] + i]).ToString() + "��", FormatStyle.m_Font12B,
                    FormatStyle.m_WhiteBrush, m_Rects[19 + i], m_DrawFormat);
            }

            //�յ�״̬
            for (int i = 0; i < 12; i++)
            {
                e.DrawString("ͨ��", FormatStyle.m_Font12, FormatStyle.m_WhiteBrush, m_Rect3[i + 12], m_DrawFormat);
            }

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {

                    e.DrawString(string.IsNullOrEmpty(m_CurrentDisplay) ? "δ֪" : m_CurrentDisplay, FormatStyle.m_Font12, FormatStyle.m_WhiteBrush, m_Rect3[i], m_DrawFormat);
                    e.DrawString(string.IsNullOrEmpty(m_CurrentDisplay) ? "δ֪" : m_CurrentDisplay, FormatStyle.m_Font12, FormatStyle.m_WhiteBrush, m_Rect3[i + 6], m_DrawFormat);

                    ////�յ�ģʽ1����/ģʽ2
                    //if (BoolList[_boolIds[0] + i * 7])
                    //{

                    //    e.DrawImage(_images[0], rect3[i]);
                    //    e.DrawImage(_images[0], rect3[i + 6]);
                    //    break;
                    //}
                    //else if (BoolList[_boolIds[1] + i * 7])
                    //{
                    //    e.DrawImage(_images[1], rect3[i]);
                    //    e.DrawImage(_images[1], rect3[i + 6]);
                    //    break;
                    //    ;
                    //}
                    //else if (BoolList[_boolIds[2] + i * 7])
                    //{
                    //    e.DrawImage(_images[2], rect3[i]);
                    //    e.DrawImage(_images[2], rect3[i + 6]);
                    //    break;

                    //}
                    //else if (BoolList[_boolIds[3] + i * 7])
                    //{
                    //    e.DrawImage(_images[3], rect3[i]);
                    //    e.DrawImage(_images[3], rect3[i + 6]);
                    //    break;
                    //}

                    //else if (BoolList[_boolIds[4] + i * 7])
                    //{
                    //    e.DrawImage(_images[4], rect3[i]);
                    //    e.DrawImage(_images[4], rect3[i + 6]);
                    //    break;
                    //}
                    //else if (BoolList[_boolIds[5] + i * 7])
                    //{
                    //    e.DrawImage(_images[5], rect3[i]);
                    //    e.DrawImage(_images[5], rect3[i + 6]);
                    //    break;
                    //}
                    //else if (BoolList[_boolIds[6] + i * 7])
                    //{
                    //    e.DrawImage(_images[6], rect3[i]);
                    //    e.DrawImage(_images[6], rect3[i + 6]);
                    //    break;
                    //}

                    //else
                    //{


                    //    e.DrawString("δ֪", FormatStyle.Font12, FormatStyle.WhiteBrush, rect3[i]);
                    //    e.DrawString("δ֪", FormatStyle.Font12, FormatStyle.WhiteBrush, rect3[i + 6]);


                    //}

                }
            }


            for (int i = 0; i < 12; i++)
            {
                //�յ��������
                if (BoolList[m_BoolIds[7] + i])
                    e.FillRectangle(FormatStyle.m_YellowBrush, m_Rects[85 + i]);
                else
                    e.FillRectangle(FormatStyle.m_GreenBrush, m_Rects[85 + i]);
            }

            //ѹ����״̬
            for (int i = 0; i < 24; i++)
            {
                if (BoolList[m_BoolIds[8] + i * 3])
                    e.FillRectangle(FormatStyle.m_LightGreenBrush, m_Rects[61 + i]);
                else if (BoolList[m_BoolIds[9] + i * 3])
                    e.FillRectangle(FormatStyle.m_RedBrush, m_Rects[61 + i]);
                else if (BoolList[m_BoolIds[10] + i * 3])
                    e.FillRectangle(FormatStyle.m_MediumGreySolidBrush, m_Rects[61 + i]);
                else
                    e.FillRectangle(FormatStyle.m_OrangeBrush, m_Rects[61 + i]);
                e.DrawString(FormatStyle.m_Str10[i], FormatStyle.m_Font10,
                    FormatStyle.m_BlackBrush, m_Rects[61 + i], m_DrawFormat);
            }

            //�յ�����
            e.DrawString("�յ�����", FormatStyle.m_Font12B,
                FormatStyle.m_WhiteBrush, m_Rects[97], m_DrawFormat);
            e.DrawLine(FormatStyle.m_WhitePen, m_PDrawPoint[0], m_PDrawPoint[1]);
            e.DrawString("�յ�����", FormatStyle.m_Font12B,
                FormatStyle.m_WhiteBrush, m_Rects[98]);


            //����״̬
            for (int i = 0; i < 9; i++)
            {
                if (m_ButtonIsDown[i])
                    e.DrawImage(m_Images[8], m_Rects[99 + i]);
                else
                    e.DrawImage(m_Images[7], m_Rects[99 + i]);
                e.DrawString(FormatStyle.m_Str9[i], FormatStyle.m_Font12B,
                    FormatStyle.m_BlackBrush, m_Rects[99 + i], m_DrawFormat);

            }
            //�ŵ�״̬ Tc1 M11 M21 M12 M22 Tc2
            for (int i = 0; i < 6; i++)
            {
                e.DrawImage(m_Images[i + 9], m_Rect1[i]);

            }
            //�趨
            e.DrawString("�趨", FormatStyle.m_Font12B, FormatStyle.m_BlackBrush, m_Rects[106], m_DrawFormat);

            //
            //�յ��������
            for (int i = 0; i < 12; i++)
            {
                e.DrawString(FormatStyle.m_Str28[i], FormatStyle.m_Font10, FormatStyle.m_BlackBrush, m_Rects[85 + i].X + 8, m_Rects[85 + i].Y + 3);

            }
        }

        private void DrawOn(Graphics e)
        {
            DrawFrame(e);
            DrawValue(e);
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

            m_Images = new Image[20];

            m_ButtonIsDown = new bool[10];

            m_Regions = new List<Region>();
            m_BoolIds = new List<int>();
            m_FoolatIds = new List<int>();


            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Images[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }
            for (int index = 0; index < UIObj.InBoolList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InBoolList[index + 1]; i++)
                {
                    m_BoolIds.Add(UIObj.InBoolList[index] + i);
                }
            }
            for (int index = 0; index < UIObj.InFloatList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InFloatList[index + 1]; i++)
                {
                    m_FoolatIds.Add(UIObj.InFloatList[index] + i);
                }
            }

            //�ŵ�״̬
            for (int i = 0; i < 6; i++)
            {
                m_Rect1[i] = new RectangleF(180 + i * 95, 110, 80, 50);
            }

            #region :::::::::::::::::::::::: _rects ::::::::::::::::::::::
            //��
            for (int i = 0; i < 3; i++)
            {
                m_Rects[i] = new RectangleF(60, 170 + i * 29, 115, 29);
            }
            for (int i = 0; i < 2; i++)
            {
                m_Rects[3 + i] = new RectangleF(60, 257 + i * 58, 115, 58);
                m_Rects[5 + i] = new RectangleF(60, 373 + i * 29, 115, 29);
            }
            //��
            for (int i = 0; i < 9; i++)
            {
                m_Rect2[i] = new RectangleF(60, 170 + i * 29, 115, 29);
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    m_Rects[7 + i * 6 + j] = new Rectangle(175 + j * 95, 170 + i * 29, 95, 29);
                }
            }
            //ģʽͼƬ��λ��
            for (int j = 0; j < 2; j++)
            {


                for (int i = 0; i < 6; i++)
                {
                    m_Rect3[j * 6 + i] = new RectangleF(195 + i * 95, 318 + j * 29, 42, 26);

                }
            }
            //״̬
            for (int j = 0; j < 2; j++)
            {


                for (int i = 0; i < 6; i++)
                {
                    m_Rect3[j * 6 + i + 12] = new RectangleF(195 + i * 95, 258 + j * 29, 42, 26);

                }
            }


            for (int i = 0; i < 6; i++)
            {
                //ѹ����״̬
                m_Rects[61 + i * 4] = new Rectangle(178 + i * 95, 404, 21, 25);
                m_Rects[62 + i * 4] = new Rectangle(201 + i * 95, 404, 21, 25);
                m_Rects[63 + i * 4] = new Rectangle(224 + i * 95, 404, 21, 25);
                m_Rects[64 + i * 4] = new Rectangle(247 + i * 95, 404, 21, 25);

                //�յ��������
                m_Rects[85 + i * 2] = new RectangleF(180 + i * 95, 378, 30, 20);
                m_Rects[86 + i * 2] = new RectangleF(225 + i * 95, 378, 30, 20);
            }

            //�յ����á��յ�����
            for (int i = 0; i < 2; i++)
            {
                m_Rects[97 + i] = new Rectangle(60, 432 + i * 20, 685, 20);
                for (int j = 0; j < 4; j++)
                {
                    if (i == 1 && j > 1) break;
                    m_Rects[99 + i * 4 + j] = new RectangleF(120 + j * 81, 470 + i * 40, 80, 35);
                }
            }
            m_Rects[105] = new RectangleF(282, 510, 161, 35);
            //�趨
            m_Rects[106] = new RectangleF(460, 505, 75, 40);
            //����ģʽ
            m_Rects[107] = new RectangleF(650, 505, 90, 40);

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
            m_PDrawPoint[0] = new Point(60, 450);
            m_PDrawPoint[1] = new Point(745, 450);

            //MoveScreen
            for (int i = 0; i < m_PDrawPoint.Length; i++)
            {
                m_PDrawPoint[i].X = (m_PDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_PDrawPoint[i].Y = (m_PDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: _regions :::::::::::::::::::::::
            for (int i = 0; i < 9; i++)
            {
                m_Regions.Add(new Region(m_Rects[99 + i]));
            }


            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat m_DrawFormat = new StringFormat();

        public static StringFormat m_RightFormat = new StringFormat();

        public static StringFormat m_LeftFormat = new StringFormat();

        #region:::::::::::::::::::::::::::��ֵ����::::::::::::::::::::::::::::::::
        /// <summary>
        /// ���꼯
        /// </summary>
        private PointF[] m_PDrawPoint;

        /// <summary>
        /// ���ο�
        /// </summary>
        private RectangleF[] m_Rects;
        //�ſ�
        private readonly RectangleF[] m_Rect1 = new RectangleF[6];
        //����9����
        private readonly RectangleF[] m_Rect2 = new RectangleF[9];
        private readonly RectangleF[] m_Rect3 = new RectangleF[24];

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
        /// <summary>
        /// bool�߼���
        /// </summary>
        private List<int> m_BoolIds;
        /// <summary>
        /// float�߼���
        /// </summary>
        private List<int> m_FoolatIds;


        #endregion#

        #endregion
    }
}