using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.PublicUI;
using MMI.Facility.Interface.Data;

namespace KumM_TMS.运行
{
    /// <summary>
    /// 紧急广播
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class EmergencyRadio : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "紧急广播";
        }

       

        public override bool init(ref int nErrorObjectIndex)
        {
            LoadBordercast();
            InitData();
            return true;
        }

        private void LoadBordercast()
        {
            var file = Path.Combine(RecPath, "..\\config\\消息通知.txt");
            foreach (var line in File.ReadAllLines(file,Encoding.Default))
            {
                AddStringByLine(line);
            }
        }
       

        public  void AddStringByLine( string cStr)
        {
            string[] split = cStr.Split('\t');
            if (split.Length == 2 && split[0].Trim() == "[紧急广播]")
            {
                RadioStringList.Add(split[1].Trim());
            }
        }
        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics dcGs)
        {
            //GetValue();
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        private void SetSoundIDTo0()
        {
            for (int i = 0; i < 17; i++)
            {
                buttonIsDown[i] = false;
                append_postCmd(CmdType.SetBoolValue, _setSoundIDs[i], 0, 0);
            }

        }

        private void SetSoundID(int soundId)
        {
            if (soundId >= 0 && soundId < 17)
            {
                SetSoundIDTo0();
                buttonIsDown[soundId] = true;
                isStop = false;
                //发送声音编号
                append_postCmd(CmdType.SetBoolValue, _setSoundIDs[soundId], 1, 0);
            }
        }

        public override bool mouseDown(Point nPoint)
        {
            int index = 0;
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                {
                    SetSoundID(index);
                    break;
                }
            }
            if (index == 17)
            {
                SetSoundIDTo0();
                isStop = true;
            }
            else if (index == 18)
            {
                buttonIsDown[18] = true;
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                {
                    break;
                }
            }
            if (index == 18)
            {
                buttonIsDown[18] = false;
                append_postCmd(CmdType.ChangePage, 11, 0, 0);
            }
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        /// <summary>
        /// 框架
        /// </summary>
        /// <param name="e"></param>
        private void DrawFrame(Graphics e)
        {
            e.FillRectangle(FormatStyle.MediumGreySolidBrush,
                rects[0]);
        }

        /// <summary>
        /// 填充数值
        /// </summary>
        /// <param name="e"></param>
        private void DrawValue(Graphics e)
        {
            for (int i = 0; i < RadioStringList.Count; i++)
            {
                if (buttonIsDown[i])
                    e.DrawImage(Img[1], rects[1 + i]);
                else
                    e.DrawImage(Img[0], rects[1 + i]);

                e.DrawString((i + 1) + "." + RadioStringList[i], FormatStyle.Font12,
                    FormatStyle.BlackBrush, rects[1 + i], LeftFormat);
            }

            if (isStop)
                e.DrawImage(Img[1], rects[20]);
            else
                e.DrawImage(Img[0], rects[20]);

            if (buttonIsDown[21])
                e.DrawImage(Img[1], rects[21]);
            else
                e.DrawImage(Img[0], rects[21]);
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(str1[i], FormatStyle.Font12, 
                    FormatStyle.BlackBrush, rects[20 + i], drawFormat);
            }
            
        }


        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            DrawFrame(e);
            DrawValue(e);
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        /// <summary>
        /// 初始化坐标、数组、热区
        /// </summary>
        private void InitData()
        {
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Center;

            RightFormat.LineAlignment = StringAlignment.Center;
            RightFormat.Alignment = StringAlignment.Far;

            LeftFormat.LineAlignment = StringAlignment.Center;
            LeftFormat.Alignment = StringAlignment.Near;

            pDrawPoint = new PointF[200];

            rects = new RectangleF[200];

            Img = new Image[30];

            buttonIsDown = new bool[30];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::::::::: rects :::::::::::::::::::::::::::::::::::
            rects[0] = new Rectangle(5, 100, 790, 445);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    rects[1 + i * 10 + j] = new Rectangle(15 + i * 410, 104 + j * 44, 375, 40);
                }

            }

            for (int i = 0; i < 2; i++)
            {
                rects[20 + i] = new Rectangle(410 + i * 205, 500, 175, 40);
            }

            //MoveScreen
            for (int i = 0; i < rects.Length; i++)
            {
                rects[i].X = (rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                rects[i].Y = (rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                rects[i].Width *= FormatStyle.Scale;
                rects[i].Height *= FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::::::::::: Rect ::::::::::::::::::::::::::::::::::::::
            for (int i = 0; i < 17; i++)
            {
                Rect.Add(new Region(rects[1 + i]));
            }
            Rect.Add(new Region(rects[20]));
            Rect.Add(new Region(rects[21]));
            #endregion

            #region :::::::::::::::::::::::::::::::: point ::::::::::::::::::::::::::::::::::::::

            #endregion

            _setSoundIDs = new int[Convert.ToInt32(UIObj.OutBoolList[1]) - Convert.ToInt32(UIObj.OutBoolList[0]) + 1];
            for (int i = 0; i < _setSoundIDs.Length; i++)
            {
                _setSoundIDs[i] = Convert.ToInt32(UIObj.OutBoolList[0]) + i;
            }
        }
        #endregion

        public int[] _setSoundIDs;

        public bool isStop = true;

        public List<string> RadioStringList = new List<string>();

        public string[] str1 = new string[] { "停止", "返回" };

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        private int[] lines = new int[6] { 0, 2, 3, 5, 6, 11 };
        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::
        /// <summary>
        /// 坐标集
        /// </summary>
        internal PointF[] pDrawPoint;

        /// <summary>
        /// 矩形框集
        /// </summary>
        internal RectangleF[] rects;

        /// <summary>
        /// 图片集
        /// </summary>
        internal Image[] Img;

        /// <summary>
        /// 键是否按下
        /// </summary>
        internal bool[] buttonIsDown;

        /// <summary>
        /// 按键列表
        /// </summary>
        internal List<Region> Rect;
        #endregion#
        #endregion
    }

    /// <summary>
    /// 旁路
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Bypass : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "旁路";
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
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                {
                    break;
                }
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
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                {
                    break;
                }
            }
            switch (index)
            {
                case 0:
                    buttonIsDown[0] = false;
                    append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    break;
            }
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        /// <summary>
        /// 填充数值
        /// </summary>
        /// <param name="e"></param>
        private void DrawValue(Graphics e)
        {
            for (int i = 0; i < 2; i++)
            {
                e.DrawImage(Img[3 - i], rects[i]);
                e.DrawString(FormatStyle.Str16[i], FormatStyle.Font12,
                    FormatStyle.WhiteBrush, rects[i + 2], drawFormat);
            }
            for (int i = 0; i < 11; i++)
            {
                //e.DrawRectangle(FormatStyle.WhitePen, rects[4 + i]);
                e.DrawString(FormatStyle.Str16[i + 2], FormatStyle.Font12,
                    FormatStyle.WhiteBrush, rects[4 + i], RightFormat);
                
                e.DrawImage(BoolList[80 + i] ? Img[3] : Img[2], rects[16 + i]);
            }


            //
            e.DrawLine(FormatStyle.MediumGreyPen, pDrawPoint[0], pDrawPoint[1]);

            //菜单
            for (int i = 0; i < 8; i++)
            {
                e.DrawImage(Img[0], rects[28 + i]);
            }
            if (buttonIsDown[0])
                e.DrawImage(Img[1], rects[35]);
            //
            e.DrawString("返回", FormatStyle.Font14, FormatStyle.BlackBrush,
                rects[35], drawFormat);
        }


        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            DrawValue(e);
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        /// <summary>
        /// 初始化坐标、数组、热区
        /// </summary>
        private void InitData()
        {
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Center;

            RightFormat.LineAlignment = StringAlignment.Center;
            RightFormat.Alignment = StringAlignment.Far;

            LeftFormat.LineAlignment = StringAlignment.Near;
            LeftFormat.Alignment = StringAlignment.Center;

            pDrawPoint = new PointF[200];

            rects = new RectangleF[200];

            Img = new Image[30];

            buttonIsDown = new bool[30];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::::::::: rects :::::::::::::::::::::::::::::::::::
            //
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    rects[i * 2 + j] = new Rectangle(20 + i * 80, 115 + j * 35, 80, 30);
                }
                for (int k = 0; k < 6; k++)
                {
                    rects[4 + i * 6 + k] = new Rectangle(50 + i * 250, 210 + k * 45, 160, 30);
                    rects[16 + i * 6 + k] = new Rectangle(210 + i * 250, 210 + k * 45, 80, 30);
                }
            }
            //
            for (int i = 0; i < 8; i++)
            {
                rects[28 + i] = new Rectangle(710, 100 + i * 56, 89, 54);
            }

            //MoveScreen
            for (int i = 0; i < rects.Length; i++)
            {
                rects[i].X = (rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                rects[i].Y = (rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                rects[i].Width *= FormatStyle.Scale;
                rects[i].Height *= FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::::::::::: Rect ::::::::::::::::::::::::::::::::::::::
            Rect.Add(new Region(rects[35]));
            #endregion

            #region :::::::::::::::::::::::::::::::: point ::::::::::::::::::::::::::::::::::::::
            pDrawPoint[0] = new Point(705, 95);
            pDrawPoint[1] = new Point(705, 550);

            //MoveScreen
            for (int i = 0; i < pDrawPoint.Length; i++)
            {
                pDrawPoint[i].X = (pDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                pDrawPoint[i].Y = (pDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        List<string> Stations = new List<string>();
        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::
        /// <summary>
        /// 坐标集
        /// </summary>
        internal PointF[] pDrawPoint;

        /// <summary>
        /// 矩形框集
        /// </summary>
        internal RectangleF[] rects;

        /// <summary>
        /// 图片集
        /// </summary>
        internal Image[] Img;

        /// <summary>
        /// 键是否按下
        /// </summary>
        internal bool[] buttonIsDown;

        /// <summary>
        /// 按键列表
        /// </summary>
        internal List<Region> Rect;
        #endregion#
        #endregion
    }

}