using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace KumM_TMS.����״̬
{
    /// <summary>
    /// ����״̬
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class CarState : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "����״̬";
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
            //GetValue();
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public override bool mouseDown(Point nPoint)
        {
            int index = 0;
            for (; index < Rect.Count; index++)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    buttonIsDown[0] = true;
                    break;
            }

            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < Rect.Count; index++)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    buttonIsDown[0] = false;
                    self_inspection = !self_inspection;
                    if (self_inspection)//�Լ쿪ʼ
                    {
                        append_postCmd(CmdType.SetBoolValue, 1671, 1, 0);
                        append_postCmd(CmdType.SetBoolValue, 1672, 0, 0);
                    }
                    else//�Լ�ȡ��
                    {
                        if (BoolList[1585] || BoolList[1588])
                        {
                            append_postCmd(CmdType.SetBoolValue, 1671, 0, 0);
                            append_postCmd(CmdType.SetBoolValue, 1672, 1, 0);
                        }
                        else
                        {
                            append_postCmd(CmdType.SetBoolValue, 1671, 0, 0);
                            append_postCmd(CmdType.SetBoolValue, 1672, 0, 0);
                        }
                    }
                    self_inspectionStr = self_inspection ? "�Լ�ȡ��" : "�Լ쿪ʼ";
                    break;
            }
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="e"></param>
        private void DrawFrame(Graphics e)
        {
            for (int i = 0; i < 70; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, rects[i].X, rects[i].Y, rects[i].Width, rects[i].Height);
            }
            for (int i = 0; i < 10; i++)
            {
                e.DrawString(FormatStyle.Str7[i], FormatStyle.Font12B,
                    FormatStyle.WhiteBrush, rects[i], drawFormat);
            }
            for (int i = 0; i < 6; i++)
            {
                e.DrawLine(FormatStyle.WhitePen, pDrawPoint[i], pDrawPoint[i + 6]);
            }

            for (int i = 0; i < 4; i++)
            {
                e.DrawString("-", FormatStyle.Font16B, FormatStyle.WhiteBrush,
                    rects[10 + i * 6], drawFormat);
                e.DrawString("-", FormatStyle.Font16B, FormatStyle.WhiteBrush,
                    rects[15 + i * 6], drawFormat);
                e.DrawString("-", FormatStyle.Font16B, FormatStyle.WhiteBrush,
                    rects[35 + i * 6], drawFormat);
                e.DrawString(i == 0 ? "" : "-", FormatStyle.Font16B, FormatStyle.WhiteBrush,
                    rects[36 + i * 6], drawFormat);
                e.DrawString("-", FormatStyle.Font16B, FormatStyle.WhiteBrush,
                    rects[37 + i * 6], drawFormat);
                e.DrawString("-", FormatStyle.Font16B, FormatStyle.WhiteBrush,
                    rects[38 + i * 6], drawFormat);
            }

            for (int i = 0; i < 3; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, rects[83 + i].X, rects[83 + i].Y, rects[83 + i].Width, rects[83 + i].Height);
                if (BoolList[1585 + i])
                {
                    e.DrawString(self_inspectionArr1[i], FormatStyle.Font12, FormatStyle.WhiteBrush, rects[84], drawFormat);
                }
                if (BoolList[1588 + i])
                {
                    e.DrawString(self_inspectionArr2[i], FormatStyle.Font12, FormatStyle.WhiteBrush, rects[85], drawFormat);
                }
            }
            e.DrawString("�ƶ��Լ�", FormatStyle.Font14B, FormatStyle.WhiteBrush,
                rects[83], drawFormat);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="e"></param>
        private void DrawValue(Graphics e)
        {
            for (int i = 0; i < 4; i++)
            {
                //VVVF
                if (BoolList[742 + i * 4])
                    e.FillRectangle(FormatStyle.LightGreenBrush, rects[11 + i]);
                else if (BoolList[743 + i * 4])
                    e.FillRectangle(FormatStyle.WhiteBrush, rects[11 + i]);
                else if (BoolList[744 + i * 4])
                    e.FillRectangle(FormatStyle.RedBrush, rects[11 + i]);
                else if (BoolList[745 + i * 4])
                    e.FillRectangle(FormatStyle.MediumGreySolidBrush, rects[11 + i]);

                //����
                if (BoolList[758 + i * 3])
                    e.FillRectangle(FormatStyle.LightGreenBrush, rects[17 + i]);
                else if (BoolList[759 + i * 3])
                    e.FillRectangle(FormatStyle.BlackBrush, rects[17 + i]);
                else if (BoolList[760 + i * 3])
                    e.FillRectangle(FormatStyle.MediumGreySolidBrush, rects[17 + i]);

                //�м��ѹ
                e.DrawString(Convert.ToInt32(FloatList[71 + i]).ToString(),
                    FormatStyle.Font12B, FormatStyle.WhiteBrush, rects[23 + i], drawFormat);

                //�������
                e.DrawString(Convert.ToInt32(FloatList[75 + i]).ToString(),
                    FormatStyle.Font12B, FormatStyle.WhiteBrush, rects[29 + i], drawFormat);

            }

            for (int i = 0; i < 2; i++)
            {
                //SIV
                if (BoolList[770 + i * 2])
                    e.FillRectangle(FormatStyle.LightGreenBrush, rects[34 + i * 5]);
                else if (BoolList[771 + i * 2])
                    e.FillRectangle(FormatStyle.RedBrush, rects[34 + i * 5]);
                if (BoolList[1094 + i])
                {
                    e.FillRectangle(FormatStyle.MediumGreySolidBrush, rects[34 + i * 5]);
                }
                //�����ѹ
                e.DrawString(Convert.ToInt32(FloatList[79 + i]).ToString(),
                    FormatStyle.Font12B, FormatStyle.WhiteBrush, rects[40 + i * 5], drawFormat);

                //110V���
                e.DrawString(Convert.ToInt32(FloatList[81 + i]).ToString(),
                    FormatStyle.Font12B, FormatStyle.WhiteBrush, rects[46 + i * 5], drawFormat);

                //������
                e.DrawString(Convert.ToInt32(FloatList[83 + i]).ToString(),
                    FormatStyle.Font12B, FormatStyle.WhiteBrush, rects[52 + i * 5], drawFormat);
                                
            }

            //�ܷ�ѹ��
            for (int i = 0; i < 6; i++)
            {
                e.DrawString(Convert.ToInt32(FloatList[98 + i]).ToString(),
                    FormatStyle.Font12B, FormatStyle.WhiteBrush, rects[58 + i], drawFormat);
            }

            //�ջ�ѹ��
            for (int i = 0; i < 12; i++)
            {
                e.DrawString(Convert.ToInt32(FloatList[104 + i]).ToString(),
                    FormatStyle.Font12B, FormatStyle.WhiteBrush, rects[71 + i], drawFormat);
            }

            //RFK
            if (BoolList[775])
                e.FillRectangle(FormatStyle.MediumGreySolidBrush, rects[36]);
            else if (BoolList[774])
                e.FillRectangle(FormatStyle.LightGreenBrush, rects[36]);
            e.DrawString("RFK", FormatStyle.Font14, FormatStyle.WhiteBrush,
                rects[36], drawFormat);
        }

        private void DrawOn(Graphics e)
        {
            DrawValue(e);
            DrawFrame(e);

            if (buttonIsDown[0])
                e.DrawImage(Img[1], rects[86]);
            else
                e.DrawImage(Img[0], rects[86]);

            e.DrawString(self_inspectionStr, FormatStyle.Font14B, FormatStyle.BlackBrush,
                rects[86], drawFormat);
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        /// <summary>
        /// ��ʼ�����ꡢ���顢����
        /// </summary>
        private void InitData()
        {
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Center;

            RightFormat.LineAlignment = StringAlignment.Far;
            RightFormat.Alignment = StringAlignment.Center;

            LeftFormat.LineAlignment = StringAlignment.Near;
            LeftFormat.Alignment = StringAlignment.Center;

            pDrawPoint = new PointF[20];

            rects = new RectangleF[150];

            Img = new Image[10];

            buttonIsDown = new bool[10];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::::: rects :::::::::::::::::::::::::::::::::
            for (int i = 0; i < 10; i++)
            {
                rects[i] = new Rectangle(10, 170 + i * 29, 115, 29);

                for (int j = 0; j < 6; j++)
                {
                    rects[10 + i * 6 + j] = new Rectangle(125 + j * 95, 170 + i * 29, 95, 29);
                }
            }

            for (int i = 0; i < 6; i++)
            {
                rects[71 + i * 2] = new Rectangle(125 + i * 95, 431, 48, 29);
                rects[72 + i * 2] = new Rectangle(173 + i * 95, 431, 48, 29);
            }

            rects[83] = new Rectangle(10, 470, 115, 48);
            for (int i = 0; i < 2; i++)
            {
                rects[84 + i] = new Rectangle(125 + i * 285, 470, 285, 48);
            }

            rects[86] = new Rectangle(705, 470, 90, 48);

            //MoveScreen
            for (int i = 0; i < rects.Length; i++)
            {
                rects[i].X = (rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                rects[i].Y = (rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                rects[i].Width *= FormatStyle.Scale;
                rects[i].Height *= FormatStyle.Scale;
            }
            #endregion

            #region ::::::::::::::::::::::::::: pDrawPoint :::::::::::::::::::::::::::
            for (int i = 0; i < 6; i++)
            {
                pDrawPoint[i] = new Point(172 + i * 95, 431);
                pDrawPoint[i + 6] = new Point(172 + i * 95, 460);
            }

            //MoveScreen
            for (int i = 0; i < pDrawPoint.Length; i++)
            {
                pDrawPoint[i].X = (pDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                pDrawPoint[i].Y = (pDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::::::::::: Rect ::::::::::::::::::::::::::::::::::::::
            Rect.Add(new Region(rects[86]));
            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        //�Լ�
        private bool self_inspection = false;
        private string self_inspectionStr = "�Լ쿪ʼ";
        private string[] self_inspectionArr1 = { "һ��Ԫ�ƶ��Լ쿪ʼ", "һ��Ԫ�ƶ��Լ�ɹ�", "һ��Ԫ�ƶ��Լ�ȡ��" };
        private string[] self_inspectionArr2 = { "����Ԫ�ƶ��Լ쿪ʼ", "����Ԫ�ƶ��Լ�ɹ�", "����Ԫ�ƶ��Լ�ȡ��" };
        #region:::::::::::::::::::::::::::��ֵ����::::::::::::::::::::::::::::::::
        /// <summary>
        /// ���꼯
        /// </summary>
        internal PointF[] pDrawPoint;

        /// <summary>
        /// ���ο�
        /// </summary>
        internal RectangleF[] rects;

        /// <summary>
        /// ͼƬ��
        /// </summary>
        internal Image[] Img;

        /// <summary>
        /// ���Ƿ���
        /// </summary>
        internal bool[] buttonIsDown;

        /// <summary>
        /// �����б�
        /// </summary>
        internal List<Region> Rect;
        #endregion#
        #endregion
    }

    /// <summary>
    /// ����״̬����
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class CarStateHelp : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "����״̬����";
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
            //GetValue();
            DrawOn(dcGs);
            base.paint(dcGs);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void DrawOn(Graphics e)
        {
            e.DrawImage(Img[0], rects[0]);
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        /// <summary>
        /// ��ʼ�����ꡢ���顢����
        /// </summary>
        private void InitData()
        {
            pDrawPoint = new PointF[20];

            rects = new RectangleF[20];

            Img = new Image[10];

            buttonIsDown = new bool[10];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            rects[0] = new RectangleF(0, 100, 798, 450);

            //MoveScreen
            for (int i = 0; i < rects.Length; i++)
            {
                rects[i].X = (rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                rects[i].Y = (rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                rects[i].Width *= FormatStyle.Scale;
                rects[i].Height *= FormatStyle.Scale;
            }
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        #region:::::::::::::::::::::::::::��ֵ����::::::::::::::::::::::::::::::::
        /// <summary>
        /// ���꼯
        /// </summary>
        internal PointF[] pDrawPoint;

        /// <summary>
        /// ���ο�
        /// </summary>
        internal RectangleF[] rects;

        /// <summary>
        /// ͼƬ��
        /// </summary>
        internal Image[] Img;

        /// <summary>
        /// ���Ƿ���
        /// </summary>
        internal bool[] buttonIsDown;

        /// <summary>
        /// �����б�
        /// </summary>
        internal List<Region> Rect;
        #endregion#
        #endregion
    }
}
