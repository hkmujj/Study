using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.PublicUI;
using MMI.Facility.Interface.Data;

namespace KumM_TMS.维护
{
    public class NumbKeyboard
    {
        public NumbKeyboard(PointF top)
        {
            //TODO:
            _points = top;
            Init();
        }

        #region :::::::::::: func ::::::::::::::
        public void DrawKeyboard(Graphics e, ref bool[] btnIsDown, StringFormat format)
        {
            _btnIsDown = btnIsDown;
            if (_btnIsDown.Length < 13) return;

            for (int i = 0; i < 13; i++)
            {
                if (_btnIsDown[i])
                    e.FillRectangle(blueBrush, _rects[i]);
                else
                    e.FillRectangle(FormatStyle.BlueBrush, _rects[i]);

                e.DrawString(str[i], FormatStyle.Font24, FormatStyle.BlackBrush,
                    _rects[i], format);
            }
        }
        #endregion

        #region :::::::::::: init ::::::::::::::
        private void Init()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _rects[i * 3 + j] = new RectangleF(j * 127, i * 57, 125, 55);
                }
            }
            _rects[9] = new RectangleF(0, 171, 125, 55);
            _rects[10] = new RectangleF(127, 171, 252, 55);

            for (int i = 0; i < 2; i++)
            {
                _rects[11 + i] = new RectangleF(i * 254, 250, 125, 55);
            }

            //
            for (int i = 0; i < _rects.Length; i++)
            {
                _rects[i].X = (_rects[i].X + _points.X) * FormatStyle.Scale;
                _rects[i].Y = (_rects[i].Y + _points.Y) * FormatStyle.Scale;
                _rects[i].Width *= FormatStyle.Scale;
                _rects[i].Height *= FormatStyle.Scale;
            }
        }
        #endregion

        #region :::::::::::: var :::::::::::::::
        private PointF _points;
        public PointF Points { get { return _points; } }

        private RectangleF[] _rects = new RectangleF[13];
        public RectangleF[] Rects { get { return _rects; } }

        private bool[] _btnIsDown;

        private SolidBrush blueBrush = new SolidBrush(Color.Blue);

        private String[] str = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "DEL", "确定", "取消" };
        #endregion
    }

    /// <summary>
    /// 登录
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Password : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "登录";
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
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            if (index >= 0 && index < 13)
            {
                buttonIsDown[index] = true;
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    buttonIsDown[0] = false;
                    InputPassWord("1");
                    break;
                case 1:
                    buttonIsDown[1] = false;
                    InputPassWord("2");
                    break;
                case 2:
                    buttonIsDown[2] = false;
                    InputPassWord("3");
                    break;
                case 3:
                    buttonIsDown[3] = false;
                    InputPassWord("4");
                    break;
                case 4:
                    buttonIsDown[4] = false;
                    InputPassWord("5");
                    break;
                case 5:
                    buttonIsDown[5] = false;
                    InputPassWord("6");
                    break;
                case 6:
                    buttonIsDown[6] = false;
                    InputPassWord("7");
                    break;
                case 7:
                    buttonIsDown[7] = false;
                    InputPassWord("8");
                    break;
                case 8:
                    buttonIsDown[8] = false;
                    InputPassWord("9");
                    break;
                case 9:
                    buttonIsDown[9] = false;
                    InputPassWord("0");
                    break;
                case 10:
                    buttonIsDown[10] = false;
                    if (!input.Equals(String.Empty))
                    {
                        input = input.Remove(input.Length - 1);
                    }
                    if (!output.Equals(String.Empty))
                    {
                        output = output.Remove(output.Length - 1);
                    }
                    break;
                case 11:
                    buttonIsDown[11] = false;
                    if (input == "1")
                    {
                        append_postCmd(CmdType.ChangePage, 22, 0, 0);
                        input = string.Empty;
                        output = string.Empty;
                    }
                    break;
                case 12:
                    buttonIsDown[12] = false;
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
            if (input.Length < 6) input += Numb;
            if (output.Length < 6) output += "*";
        }

        private void DrawOn(Graphics e)
        {
            keyboard.DrawKeyboard(e, ref buttonIsDown, drawFormat);

            e.DrawString("请输入密码", FormatStyle.Font12, FormatStyle.WhiteBrush, rects[14], LeftFormat);
            e.FillRectangle(FormatStyle.MediumGreySolidBrush, rects[15]);

            e.DrawLine(FormatStyle.WhitePen, pDrawPoint[0], pDrawPoint[1]);
            e.DrawLine(FormatStyle.WhitePen, pDrawPoint[2], pDrawPoint[1]);

            e.DrawString(output, FormatStyle.Font24, FormatStyle.WhiteBrush, rects[16], drawFormat);
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

            RightFormat.LineAlignment = StringAlignment.Far;
            RightFormat.Alignment = StringAlignment.Center;

            LeftFormat.LineAlignment = StringAlignment.Near;
            LeftFormat.Alignment = StringAlignment.Center;

            pDrawPoint = new PointF[20];

            rects = new RectangleF[120];

            Img = new Image[15];

            buttonIsDown = new bool[15];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::: rects ::::::::::::::::::::::
            rects[0] = new RectangleF(0, 0, 800, 30);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    rects[1 + i * 3 + j] = new RectangleF(346 + j * 127, 170 + i * 57, 125, 55);
                }
            }
            rects[10] = new RectangleF(346, 341, 125, 55);
            rects[11] = new RectangleF(473, 341, 252, 55);

            for (int i = 0; i < 2; i++)
            {
                rects[12 + i] = new RectangleF(346 + i * 254, 420, 125, 55);
            }

            rects[14] = new RectangleF(50, 250, 220, 30);

            rects[15] = new RectangleF(50, 280, 220, 60);

            rects[16] = new RectangleF(50, 285, 220, 60);

            //MoveScreen
            for (int i = 0; i < rects.Length; i++)
            {
                rects[i].X = (rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                rects[i].Y = (rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                rects[i].Width *= FormatStyle.Scale;
                rects[i].Height *= FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: point ::::::::::::::::::::::
            pDrawPoint[0] = new PointF(270, 280);
            pDrawPoint[1] = new PointF(270, 340);
            pDrawPoint[2] = new PointF(50, 340);

            pDrawPoint[3] = new PointF(346, 170);

            //MoveScreen
            for (int i = 0; i < pDrawPoint.Length; i++)
            {
                pDrawPoint[i].X = (pDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                pDrawPoint[i].Y = (pDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: Rect :::::::::::::::::::::::
            for (int i = 0; i < 13; i++)
            {
                Rect.Add(new Region(rects[1 + i]));
            }
            #endregion

            keyboard = new NumbKeyboard(pDrawPoint[3]);
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();
        
        public String[] str1 = new String[9] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public String[] str2 = new String[2] { "确定", "取消" };

        public String numb = string.Empty;
        public String sign = string.Empty;
        public String input = string.Empty;
        public String output = string.Empty;

        NumbKeyboard keyboard;

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
    /// 检修主界面
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class RepairMain : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "检修主界面";
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
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            if (index >= 0 && index < 13)
            {
                buttonIsDown[index] = true;                
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            if (index >= 0 && index < 13)
            {
                buttonIsDown[index] = false;
            }
            switch (index)
            {
                case 2:
                    append_postCmd(CmdType.ChangePage, 23, 0, 0);
                    break;
                case 3:
                    append_postCmd(CmdType.ChangePage, 24, 0, 0);
                    break;
                case 4:
                    append_postCmd(CmdType.ChangePage, 25, 0, 0);
                    break;
                case 5:
                    append_postCmd(CmdType.ChangePage, 26, 0, 0);
                    break;
                case 7:
                    append_postCmd(CmdType.ChangePage, 27, 0, 0);
                    break;
                case 8:
                    append_postCmd(CmdType.ChangePage, 28, 0, 0);
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
            e.DrawString("设  定", FormatStyle.Font22, FormatStyle.WhiteBrush, rects[1], LeftFormat);
            e.DrawLine(FormatStyle.WhitePen, pDrawPoint[0], pDrawPoint[1]);
            e.DrawString("记录与下载", FormatStyle.Font22, FormatStyle.WhiteBrush, rects[23], LeftFormat);
            e.DrawLine(FormatStyle.WhitePen, pDrawPoint[2], pDrawPoint[3]);
            e.DrawString("查  询", FormatStyle.Font22, FormatStyle.WhiteBrush, rects[24], LeftFormat);
            e.DrawLine(FormatStyle.WhitePen, pDrawPoint[4], pDrawPoint[5]);

            for (int i = 0; i < 13; i++)
            {
                if (buttonIsDown[i])
                    e.DrawImage(Img[1], rects[25 + i]);
                else
                    e.DrawImage(Img[0], rects[25 + i]);
                e.DrawString(str1[i], FormatStyle.Font12, FormatStyle.WhiteBrush, rects[25 + i], drawFormat);
            }

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

            RightFormat.LineAlignment = StringAlignment.Far;
            RightFormat.Alignment = StringAlignment.Center;

            LeftFormat.LineAlignment = StringAlignment.Near;
            LeftFormat.Alignment = StringAlignment.Center;

            pDrawPoint = new PointF[20];

            rects = new RectangleF[120];

            Img = new Image[15];

            buttonIsDown = new bool[15];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::: rects ::::::::::::::::::::::
            rects[0] = new RectangleF(0, 0, 800, 30);
            rects[1] = new RectangleF(20, 120, 300, 40);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    rects[i * 7 + j + 2] = new RectangleF(60 + j * 95, 180 + i * 150, 90, 50);
                }
            }
            rects[23] = new RectangleF(20, 270, 300, 40);
            rects[24] = new RectangleF(20, 420, 300, 40);

            for (int i = 0; i < 6; i++)
            {
                rects[25 + i] = rects[2 + i];
            }
            for (int i = 0; i < 2; i++)
            {
                rects[31 + i] = rects[9 + i];
            }
            for (int i = 0; i < 5; i++)
            {
                rects[33 + i] = rects[16 + i];
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

            #region :::::::::::::::::::::::: point ::::::::::::::::::::::
            pDrawPoint[0] = new PointF(60, 160);
            pDrawPoint[1] = new PointF(700, 160);

            pDrawPoint[2] = new PointF(60, 310);
            pDrawPoint[3] = new PointF(700, 310);

            pDrawPoint[4] = new PointF(60, 460);
            pDrawPoint[5] = new PointF(700, 460);

            for (int i = 0; i < pDrawPoint.Length; i++)
            {
                pDrawPoint[i].X = (pDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                pDrawPoint[i].Y = (pDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: Rect :::::::::::::::::::::::
            for (int i = 0; i < 6; i++)
            {
                Rect.Add(new Region(rects[2 + i]));
            }
            for (int i = 0; i < 2; i++)
            {
                Rect.Add(new Region(rects[9 + i]));
            }
            for (int i = 0; i < 5; i++)
            {
                Rect.Add(new Region(rects[16 + i]));
            }
            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        public String[] str1 = new String[13] { "时间", "密码", "轮径", "车号", "加速度测试", "测试", "控制参数", "数据记录", "USB", "端口数据", "版本", "I / O", "参数明细" };
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
    /// 轮径设置
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class WheelDia : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "轮径设置";
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
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            if (index >= 0 && index < 13)
            {
                buttonIsDown[index] = true;
            }
            else if (index >= 13 && index < 19)
            {
                for (int i = 0; i < 6; i++)
                {
                    buttonIsDown[13 + i] = false;
                }
                buttonIsDown[index] = true;
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            if (index >= 0 && index < 13)
            {
                buttonIsDown[index] = false;
            }
            switch (index)
            {
                case 0:
                    WheelSet("1");
                    break;
                case 1:
                    WheelSet("2");
                    break;
                case 2:
                    WheelSet("3");
                    break;
                case 3:
                    WheelSet("4");
                    break;
                case 4:
                    WheelSet("5");
                    break;
                case 5:
                    WheelSet("6");
                    break;
                case 6:
                    WheelSet("7");
                    break;
                case 7:
                    WheelSet("8");
                    break;
                case 8:
                    WheelSet("9");
                    break;
                case 9:
                    WheelSet("0");
                    break;
                case 10:
                    if (!input[flagmenu].Equals(String.Empty))
                    {
                        input[flagmenu] = input[flagmenu].Remove(input[flagmenu].Length - 1);
                    }
                    break;
                case 11:
                    append_postCmd(CmdType.ChangePage, 22, 0, 0);
                    input[flagmenu] = string.Empty;
                    break;
                case 12:
                    append_postCmd(CmdType.ChangePage, 22, 0, 0);
                    input[flagmenu] = string.Empty;
                    break;
                case 13:
                    flagmenu = 0;
                    break;
                case 14:
                    flagmenu = 1;
                    break;
                case 15:
                    flagmenu = 2;
                    break;
                case 16:
                    flagmenu = 3;
                    break;
                case 17:
                    flagmenu = 4;
                    break;
                case 18:
                    flagmenu = 5;
                    break;
                default:
                    return false;
            }
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void WheelSet(String Numb)
        {
            if (input[flagmenu].Length < 3) input[flagmenu] += Numb;
        }

        private void DrawOn(Graphics e)
        {
            keyboard.DrawKeyboard(e, ref buttonIsDown, drawFormat);

            for (int i = 0; i < 6; i++)
            {
                e.DrawImage(Img[0], rects[14 + i]);
            }

            e.DrawString("原轮径", FormatStyle.Font12, FormatStyle.WhiteBrush, rects[20], RightFormat);
            e.DrawString("新轮径", FormatStyle.Font12, FormatStyle.WhiteBrush, rects[21], RightFormat);

            for (int i = 0; i < 2; i++)
            {
                e.FillRectangle(FormatStyle.WhiteBrush, rects[22 + i]);
            }

            e.DrawImage(Img[1], rects[14 + flagmenu]);
            e.DrawString(str3[flagmenu], FormatStyle.Font16, FormatStyle.BlackBrush, rects[24], drawFormat);
            e.DrawString(input[flagmenu], FormatStyle.Font16, FormatStyle.BlackBrush, rects[25], drawFormat);

            for (int i = 0; i < 6; i++)
            {
                e.DrawString(str4[i], FormatStyle.Font16, FormatStyle.BlackBrush, rects[14 + i], drawFormat);
            }
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

            RightFormat.LineAlignment = StringAlignment.Far;
            RightFormat.Alignment = StringAlignment.Center;

            LeftFormat.LineAlignment = StringAlignment.Near;
            LeftFormat.Alignment = StringAlignment.Center;

            pDrawPoint = new PointF[20];

            rects = new RectangleF[120];

            Img = new Image[15];

            buttonIsDown = new bool[20];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::: rects ::::::::::::::::::::::
            rects[0] = new RectangleF(0, 0, 800, 30);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    rects[1 + i * 3 + j] = new RectangleF(376 + j * 127, 170 + i * 57, 125, 55);
                }
            }
            rects[10] = new RectangleF(376, 341, 125, 55);
            rects[11] = new RectangleF(503, 341, 252, 55);
            for (int i = 0; i < 2; i++)
            {
                rects[12 + i] = new RectangleF(376 + i * 254, 420, 125, 55);
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    rects[14 + i * 3 + j] = new RectangleF(50 + j * 102, 170 + i * 62, 102, 62);
                }
            }

            rects[20] = new RectangleF(40, 400, 70, 40);
            rects[21] = new RectangleF(180, 400, 70, 40);

            for (int i = 0; i < 2; i++)
            {
                rects[22 + i] = new RectangleF(110 + i * 140, 400, 70, 40);
            }

            rects[24] = new RectangleF(110, 400, 70, 40);
            rects[25] = new RectangleF(250, 400, 70, 40);


            //MoveScreen
            for (int i = 0; i < rects.Length; i++)
            {
                rects[i].X = (rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                rects[i].Y = (rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                rects[i].Width *= FormatStyle.Scale;
                rects[i].Height *= FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: point ::::::::::::::::::::::
            pDrawPoint[0] = new PointF(376, 170);

            //MoveScreen
            for (int i = 0; i < pDrawPoint.Length; i++)
            {
                pDrawPoint[i].X = (pDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                pDrawPoint[i].Y = (pDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: Rect :::::::::::::::::::::::
            for (int i = 0; i < 19; i++)
            {
                Rect.Add(new Region(rects[1 + i]));
            }
            #endregion

            keyboard = new NumbKeyboard(pDrawPoint[0]);
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        public String[] str3 = new String[6] { "840", "840", "840", "840", "840", "840" };
        public String[] str4 = new String[6] { "TC1", "MP1", "M1", "TC2", "MP2", "M2" };

        int flagmenu = 0;

        public String numb = String.Empty;
        public String str = String.Empty;
        public String[] input = new String[6] { "", "", "", "", "", "" };


        NumbKeyboard keyboard;
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
    /// 车号设置
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class TrainNumSetup : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "车号设置";
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
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }

            if (index >= 0 && index < 13)
            {
                buttonIsDown[index] = true;
            }
            else if (index >= 13 && index < 19)
            {
                ButtonReset(index);
                setTrainIndex = index - 13;
                currentID = TrainID[setTrainIndex];
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    buttonIsDown[0] = false;
                    InputPassWord("1");
                    break;
                case 1:
                    buttonIsDown[1] = false;
                    InputPassWord("2");
                    break;
                case 2:
                    buttonIsDown[2] = false;
                    InputPassWord("3");
                    break;
                case 3:
                    buttonIsDown[3] = false;
                    InputPassWord("4");
                    break;
                case 4:
                    buttonIsDown[4] = false;
                    InputPassWord("5");
                    break;
                case 5:
                    buttonIsDown[5] = false;
                    InputPassWord("6");
                    break;
                case 6:
                    buttonIsDown[6] = false;
                    InputPassWord("7");
                    break;
                case 7:
                    buttonIsDown[7] = false;
                    InputPassWord("8");
                    break;
                case 8:
                    buttonIsDown[8] = false;
                    InputPassWord("9");
                    break;
                case 9:
                    buttonIsDown[9] = false;
                    InputPassWord("0");
                    break;
                case 10:
                    buttonIsDown[10] = false;
                    if (!input.Equals(String.Empty))
                    {
                        input = input.Remove(input.Length - 1);
                    }
                    break;
                case 11:
                    buttonIsDown[11] = false;
                    if (input != null)
                    {
                        append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[setTrainIndex], 0, int.Parse(input));
                        append_postCmd(CmdType.ChangePage, 22, 0, 0);
                        TrainID[setTrainIndex] = input.PadLeft(4, '0');
                        input = string.Empty;
                        currentID = string.Empty;
                        setTrainIndex = 0;
                    }
                    break;
                case 12:
                    buttonIsDown[12] = false;
                    append_postCmd(CmdType.ChangePage, 22, 0, 0);
                    input = string.Empty;
                    currentID = string.Empty;
                    setTrainIndex = 0;
                    break;
                default:
                    return false;
            }
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void InputPassWord(string Numb)
        {
            bool btnIsDown = false;
            for (int i = 0; i < 6; i++)
            {
                if (buttonIsDown[13 + i])
                {
                    btnIsDown = true;
                    break;
                }
            }

            if (btnIsDown && input.Length < 4) input += Numb;
        }

        /// <summary>
        /// 车厢按钮复位
        /// </summary>
        /// <param name="index"></param>
        private void ButtonReset(int index)
        {
            for (int i = 0; i < 6; i++)
            {
                buttonIsDown[13 + i] = false;
            }
            buttonIsDown[index] = true;
        }

        private void DrawOn(Graphics e)
        {
            keyboard.DrawKeyboard(e, ref buttonIsDown, drawFormat);

            e.DrawString("原车号", FormatStyle.Font14, FormatStyle.WhiteBrush, rects[20], LeftFormat);
            e.DrawString("新车号", FormatStyle.Font14, FormatStyle.WhiteBrush, rects[21], LeftFormat);
            for (int i = 0; i < 2; i++)
            {
                e.FillRectangle(FormatStyle.WhiteBrush, rects[16 + i]);
            }

            e.DrawString(currentID, FormatStyle.Font20B, FormatStyle.BlackBrush, rects[16], LeftFormat);


            //e.DrawString(((int)fValue[0]).ToString("00000"), FormatStyle.Font20, FormatStyle.BlackBrush, rects[18], drawFormat);
            e.DrawString(input, FormatStyle.Font20B, FormatStyle.BlackBrush, rects[17], LeftFormat);

            e.FillRectangle(blueBrush, rects[22]);
            e.DrawString("列车号", FormatStyle.Font14B, FormatStyle.BlackBrush, rects[22], drawFormat);

            for (int i = 0; i < 6; i++)
            {
                if (!buttonIsDown[13 + i])
                    e.DrawImage(Img[0], rects[23 + i]);
                else
                    e.DrawImage(Img[1], rects[23 + i]);
                e.DrawString(str4[i], FormatStyle.Font16B, FormatStyle.BlackBrush, rects[23 + i], drawFormat);
            }
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

            pDrawPoint = new PointF[20];

            rects = new RectangleF[50];

            Img = new Image[15];

            buttonIsDown = new bool[20];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::: rects ::::::::::::::::::::::
            rects[0] = new RectangleF(0, 0, 800, 30);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    rects[1 + i * 3 + j] = new RectangleF(376 + j * 127, 170 + i * 57, 125, 55);
                }
            }
            rects[10] = new RectangleF(376, 341, 125, 55);
            rects[11] = new RectangleF(503, 341, 252, 55);
            for (int i = 0; i < 2; i++)
            {
                rects[12 + i] = new RectangleF(376 + i * 254, 420, 125, 55);
            }

            rects[14] = new RectangleF(80, 220, 70, 50);
            rects[15] = new RectangleF(80, 273, 70, 50);

            for (int i = 0; i < 2; i++)
            {
                rects[16 + i] = new RectangleF(180, 190 + 35 * i, 100, 30);
            }

            rects[18] = new RectangleF(150, 220, 100, 50);
            rects[19] = new RectangleF(150, 273, 100, 50);

            for (int i = 0; i < 2; i++)
            {
                rects[20 + i] = new RectangleF(110, 190 + i * 35, 70, 30);
            }

            rects[22] = new RectangleF(83f, 280, 275, 30);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    rects[23 + i * 3 + j] = new RectangleF(83f + j * 91f, 312 + i * 42, 90, 40);
                }
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

            #region :::::::::::::::::::::::: point ::::::::::::::::::::::
            pDrawPoint[0] = new PointF(376, 170);

            //MoveScreen
            for (int i = 0; i < pDrawPoint.Length; i++)
            {
                pDrawPoint[i].X = (pDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                pDrawPoint[i].Y = (pDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: Rect :::::::::::::::::::::::
            for (int i = 0; i < 13; i++)
            {
                Rect.Add(new Region(rects[1 + i]));
            }
            for (int i = 0; i < 6; i++)
            {
                Rect.Add(new Region(rects[23 + i]));
            }
            #endregion
            
            keyboard = new NumbKeyboard(pDrawPoint[0]);
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        public String[] str3 = new String[6] { "840", "840", "840", "840", "840", "840" };
        public String[] str4 = new String[6] { "TC1", "MP1", "M1", "TC2", "MP2", "M2" };

        private string currentID = string.Empty;
        private int setTrainIndex = 0;
        public String numb = string.Empty;
        public String input = string.Empty;

        NumbKeyboard keyboard;

        private SolidBrush blueBrush = new SolidBrush(Color.Blue);

        public static string[] TrainID = new string[6] { "0001", "0002", "0003", "0004", "0005", "0006" }; 
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
    /// 加速度测试
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class AccelerationTest : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "加速度测试";
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
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }

            if (index >= 0 && index < 14)
            {
                buttonIsDown[index] = true;
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    buttonIsDown[0] = false;
                    InputPassWord("1");
                    break;
                case 1:
                    buttonIsDown[1] = false;
                    InputPassWord("2");
                    break;
                case 2:
                    buttonIsDown[2] = false;
                    InputPassWord("3");
                    break;
                case 3:
                    buttonIsDown[3] = false;
                    InputPassWord("4");
                    break;
                case 4:
                    buttonIsDown[4] = false;
                    InputPassWord("5");
                    break;
                case 5:
                    buttonIsDown[5] = false;
                    InputPassWord("6");
                    break;
                case 6:
                    buttonIsDown[6] = false;
                    InputPassWord("7");
                    break;
                case 7:
                    buttonIsDown[7] = false;
                    InputPassWord("8");
                    break;
                case 8:
                    buttonIsDown[8] = false;
                    InputPassWord("9");
                    break;
                case 9:
                    buttonIsDown[9] = false;
                    InputPassWord("0");
                    break;
                case 10:
                    buttonIsDown[10] = false;
                    if (!input.Equals(String.Empty))
                    {
                        input = input.Remove(input.Length - 1);
                    }
                    break;
                case 11:
                    buttonIsDown[11] = false;
                    //if (input != null)
                    //{
                    //    append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, int.Parse(input));
                    //    append_postCmd(CmdType.ChangePage, 22, 0, 0);
                    //    string.Empty;
                    //}
                    break;
                case 12:
                    buttonIsDown[12] = false;
                    append_postCmd(CmdType.ChangePage, 22, 0, 0);
                    input = string.Empty;
                    break;
                case 13:
                    buttonIsDown[13] = false;
                    break;
                default:
                    return false;
            }
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void InputPassWord(string Numb)
        {
            if (input.Length < 6) input += Numb;
        }

        private void DrawOn(Graphics e)
        {
            keyboard.DrawKeyboard(e, ref buttonIsDown, drawFormat);

            e.DrawRectangle(FormatStyle.WhitePen, rects[14].X, rects[14].Y, rects[14].Width, rects[14].Height);
            e.FillRectangle(FormatStyle.BlueBrush, rects[15]);

            for (int i = 0; i < 2; i++)
            {
                e.FillRectangle(FormatStyle.BlackBrush, rects[16 + i]);
                e.DrawRectangle(FormatStyle.WhitePen, rects[16 + i].X, rects[16 + i].Y, rects[16 + i].Width, rects[16 + i].Height);
                e.DrawString(str3[i], FormatStyle.Font12, FormatStyle.BlackBrush, rects[18 + i], RightFormat);
            }

            e.DrawRectangle(FormatStyle.WhitePen, rects[20].X, rects[20].Y, rects[20].Width, rects[20].Height);
            e.FillRectangle(FormatStyle.BlueBrush, rects[21]);

            for (int i = 0; i < 3; i++)
            {
                e.FillRectangle(FormatStyle.BlackBrush, rects[22 + i]);
                e.DrawRectangle(FormatStyle.WhitePen, rects[22 + i].X, rects[22 + i].Y, rects[22 + i].Width, rects[22 + i].Height);
                e.DrawString(str3[i + 2], FormatStyle.Font12, FormatStyle.BlackBrush, rects[25 + i], RightFormat);
            }

            if (buttonIsDown[13])
                e.FillRectangle(new SolidBrush(Color.Blue), rects[28]);
            else
                e.FillRectangle(FormatStyle.BlueBrush, rects[28]);
            e.DrawString("开始测试", FormatStyle.Font20, FormatStyle.BlackBrush, rects[28], drawFormat);
            e.DrawRectangle(FormatStyle.WhitePen, rects[28].X, rects[28].Y, rects[28].Width, rects[28].Height);
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

            RightFormat.LineAlignment = StringAlignment.Far;
            RightFormat.Alignment = StringAlignment.Center;

            LeftFormat.LineAlignment = StringAlignment.Near;
            LeftFormat.Alignment = StringAlignment.Center;

            pDrawPoint = new PointF[20];

            rects = new RectangleF[120];

            Img = new Image[15];

            buttonIsDown = new bool[15];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::: rects ::::::::::::::::::::::
            rects[0] = new RectangleF(0, 0, 800, 30);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    rects[1 + i * 3 + j] = new RectangleF(376 + j * 127, 170 + i * 57, 125, 55);
                }
            }
            rects[10] = new RectangleF(376, 341, 125, 55);
            rects[11] = new RectangleF(503, 341, 252, 55);
            for (int i = 0; i < 2; i++)
            {
                rects[12 + i] = new RectangleF(376 + i * 254, 420, 125, 55);
            }

            rects[14] = new RectangleF(30, 170, 330, 100);
            rects[15] = new RectangleF(31, 171, 329, 99);

            for (int i = 0; i < 2; i++)
            {
                rects[16 + i] = new RectangleF(140, 185 + i * 40, 200, 35);
                rects[18 + i] = new RectangleF(30, 185 + i * 40, 105, 35);
            }

            rects[20] = new RectangleF(30, 420, 330, 55);
            rects[21] = new RectangleF(31, 274, 329, 136);

            for (int i = 0; i < 3; i++)
            {
                rects[22 + i] = new RectangleF(140, 288 + i * 40, 200, 35);
                rects[25 + i] = new RectangleF(30, 288 + i * 40, 105, 35);
            }

            rects[28] = new RectangleF(31, 421, 329, 54);

            //MoveScreen
            for (int i = 0; i < rects.Length; i++)
            {
                rects[i].X = (rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                rects[i].Y = (rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                rects[i].Width *= FormatStyle.Scale;
                rects[i].Height *= FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: point ::::::::::::::::::::::
            pDrawPoint[0] = new PointF(376, 170);

            //MoveScreen
            for (int i = 0; i < pDrawPoint.Length; i++)
            {
                pDrawPoint[i].X = (pDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                pDrawPoint[i].Y = (pDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: Rect :::::::::::::::::::::::
            for (int i = 0; i < 13; i++)
            {
                Rect.Add(new Region(rects[1 + i]));
            }
            Rect.Add(new Region(rects[28]));
            #endregion

            keyboard = new NumbKeyboard(pDrawPoint[0]);
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        public String[] str3 = new String[5] { "起始速度:", "目标速度:", "平均加速度", "制动距离", "平均减速度" };
        public String[] str4 = new String[6] { "TC1", "MP1", "M1", "TC2", "MP2", "M2" };

        public String numb = string.Empty;
        public String input = string.Empty;

        NumbKeyboard keyboard;
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
    /// 测试
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Test : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "测试";
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
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                {
                    buttonIsDown[index] = true;
                    break;
                }
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
                    buttonIsDown[index] = false;
                    break;
                }
            }
            switch (index)
            {
                case 4:
                    append_postCmd(CmdType.ChangePage, 22, 0, 0);
                    break;
            }
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void DrawOn(Graphics e)
        {
            //e.FillRectangle(WhiteBrush, new Rectangle(374, 168, 380, 227));
            e.DrawLine(FormatStyle.WhitePen, pDrawPoint[0], pDrawPoint[1]);
            e.DrawLine(FormatStyle.MediumGreyPen, pDrawPoint[2], pDrawPoint[3]);

            for (int i = 0; i < 5; i++)
            {
                if (buttonIsDown[i])
                    e.DrawImage(Img[2], rects[1 + i]);
                else
                    e.DrawImage(Img[0], rects[1 + i]);
            }
            e.DrawString("返回", FormatStyle.Font12, FormatStyle.BlackBrush, rects[5], drawFormat);

            for (int i = 0; i < 20; i++)
            {
                if (buttonIsDown[5 + i])
                    e.DrawImage(Img[2], rects[6 + i]);
                else
                    e.DrawImage(Img[1], rects[6 + i]);
                e.DrawString(str1[i], FormatStyle.Font12, FormatStyle.BlackBrush, rects[6 + i], drawFormat);
            }
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

            RightFormat.LineAlignment = StringAlignment.Far;
            RightFormat.Alignment = StringAlignment.Center;

            LeftFormat.LineAlignment = StringAlignment.Near;
            LeftFormat.Alignment = StringAlignment.Center;

            pDrawPoint = new PointF[20];

            rects = new RectangleF[120];

            Img = new Image[15];

            buttonIsDown = new bool[25];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::: rects ::::::::::::::::::::::
            rects[0] = new RectangleF(0, 0, 800, 30);

            for (int i = 0; i < 5; i++)
            {
                rects[1 + i] = new RectangleF(708, 115 + i * 85, 80, 70);
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    rects[6 + i * 4 + j] = new RectangleF(75 + j * 150, 120 + i * 78, 107, 71);
                }

                rects[22 + i] = new RectangleF(75 + i * 150, 450, 107, 71);
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

            #region :::::::::::::::::::::::: point ::::::::::::::::::::::
            pDrawPoint[0] = new PointF(0, 100);
            pDrawPoint[1] = new PointF(800, 100);

            pDrawPoint[2] = new PointF(700, 101);
            pDrawPoint[3] = new PointF(700, 550);

            for (int i = 0; i < pDrawPoint.Length; i++)
            {
                pDrawPoint[i].X = (pDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                pDrawPoint[i].Y = (pDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: Rect :::::::::::::::::::::::
            for (int i = 0; i < 25; i++)
            {
                Rect.Add(new Region(rects[1 + i]));
            }
            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        public String[] str1 = new String[20] { "备用_1", "电制动\n切除", "合主断", "VVVF隔离", "分主断", "备用_6", "备用_7", 
            "备用_8", "备用_9", "备用_10", "备用_11", "备用_12", "备用_13", "备用_14", "备用_15", "备用_16", "备用", "备用", "备用", "备用" };
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
    /// 数据记录
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class DataLog : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "数据记录";
        }

        

        //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
     

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
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                {
                    buttonIsDown[index] = true;
                    break;
                }
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
                    buttonIsDown[index] = false;
                    break;
                }
            }
            switch (index)
            {
                case 0:
                    //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                    break;
                case 4:
                    append_postCmd(CmdType.ChangePage, 22, 0, 0);
                    //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
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
            //e.FillRectangle(WhiteBrush, new Rectangle(374, 168, 380, 227));
            e.DrawLine(FormatStyle.WhitePen, pDrawPoint[0], pDrawPoint[1]);

            for (int i = 0; i < 5; i++)
            {
                if (buttonIsDown[i])
                    e.DrawImage(Img[1], rects[1 + i]);
                else
                    e.DrawImage(Img[0], rects[1 + i]);
            }
            e.DrawString("数据\n重置", FormatStyle.Font12, FormatStyle.BlackBrush, rects[1], drawFormat);
            e.DrawString("返回", FormatStyle.Font12, FormatStyle.BlackBrush, rects[5], drawFormat);

            for (int i = 0; i < 6; i++)
            {
                e.DrawString(str1[i], FormatStyle.Font12, FormatStyle.WhiteBrush, rects[6 + i], RightFormat);
                e.DrawRectangle(FormatStyle.MediumGreyPen, rects[18 + i].X, rects[18 + i].Y, rects[18 + i].Width, rects[18 + i].Height);
                e.DrawString(str2[i], FormatStyle.Font14, FormatStyle.WhiteBrush, rects[12 + i], LeftFormat);
            }
            //for (int i = 0; i < 6; i++)
            //{
            //   e.DrawString(FormatStyle.fValue[i].ToString(), FormatStyle.Font14, FormatStyle.WhiteBrush, rects[18 + i], drawFormat);
            //}

            e.FillRectangle(FormatStyle.BlackBrush, rects[24]);
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

            RightFormat.LineAlignment = StringAlignment.Far;
            RightFormat.Alignment = StringAlignment.Center;

            LeftFormat.LineAlignment = StringAlignment.Near;
            LeftFormat.Alignment = StringAlignment.Center;

            pDrawPoint = new PointF[20];

            rects = new RectangleF[120];

            Img = new Image[15];

            buttonIsDown = new bool[10];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::: rects ::::::::::::::::::::::
            rects[0] = new RectangleF(0, 0, 800, 30);
            for (int i = 0; i < 5; i++)
            {
                rects[1 + i] = new RectangleF(708, 115 + i * 85, 80, 70);
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    rects[6 + i * 3 + j] = new RectangleF(0 + i * 320, 200 + j * 65, 150, 50);
                    rects[12 + i * 3 + j] = new RectangleF(308 + i * 320, 200 + j * 65, 50, 50);
                    rects[18 + i * 3 + j] = new RectangleF(155 + i * 320, 200 + j * 65, 150, 45);
                }
            }

            rects[24] = new RectangleF(400, 390, 250, 55);

            //MoveScreen
            for (int i = 0; i < rects.Length; i++)
            {
                rects[i].X = (rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                rects[i].Y = (rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                rects[i].Width *= FormatStyle.Scale;
                rects[i].Height *= FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: point ::::::::::::::::::::::
            pDrawPoint[0] = new PointF(0, 100);
            pDrawPoint[1] = new PointF(800, 100);


            #endregion

            #region :::::::::::::::::::::::: Rect :::::::::::::::::::::::
            for (int i = 0; i < 5; i++)
            {
                Rect.Add(new Region(rects[1 + i]));
            }
            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        public String[] str1 = new String[6] { "里程", "辅助能耗", "TC1\n压缩机工作时间", "牵引能耗", "TC2\n压缩机工作时间", "制动能耗" };
        public String[] str2 = new String[6] { "Km", "Kw.h", "Min", "Kw.h", "Min", "Kw.h" };

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

    [GksDataType(DataType.isMMIObjectClass)]
    class USB : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "USB下载";
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
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    buttonIsDown[0] = true;
                    break;
                case 1:
                    buttonIsDown[1] = true;
                    break;
                case 2:
                    buttonIsDown[2] = true;
                    break;
                case 3:
                    buttonIsDown[3] = true;
                    break;
                case 4:
                    buttonIsDown[4] = true;
                    break;
                case 5:
                    buttonIsDown[5] = true;
                    break;
                case 6:
                    buttonIsDown[6] = true;
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
                    break;
            }
            switch (index)
            {
                case 0:
                    append_postCmd(CmdType.ChangePage, 22, 0, 0);
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
                e.DrawImage(Img[0], rects[i]);
                e.DrawString(str1[i], FormatStyle.Font12, FormatStyle.WhiteBrush, rects[3 + i], drawFormat);
            }

            e.DrawString("欢迎使用USB下载系统", FormatStyle.Font20, FormatStyle.WhiteBrush, rects[6], drawFormat);
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

            RightFormat.LineAlignment = StringAlignment.Far;
            RightFormat.Alignment = StringAlignment.Center;

            LeftFormat.LineAlignment = StringAlignment.Near;
            LeftFormat.Alignment = StringAlignment.Center;

            pDrawPoint = new PointF[20];

            rects = new RectangleF[120];

            Img = new Image[15];

            buttonIsDown = new bool[10];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::: rects ::::::::::::::::::::::
            for (int i = 0; i < 3; i++)
            {
                rects[i] = new RectangleF(200 + i * 150, 250, 37, 37);
                rects[3 + i] = new RectangleF(175 + i * 150, 300, 100, 40);
            }
            rects[6] = new RectangleF(150, 100, 500, 60);

            //MoveScreen
            for (int i = 0; i < rects.Length; i++)
            {
                rects[i].X = (rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                rects[i].Y = (rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                rects[i].Width *= FormatStyle.Scale;
                rects[i].Height *= FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: Rect :::::::::::::::::::::::
            Rect.Add(new Region(new Rectangle(500, 250, 50, 90)));
            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        public String[] str1 = new String[3] { "故障下载1", "程序更新2", "→退出" };
        public String[] str2 = new String[8] { "Km", "Km", "Min", "Min", "Kw.h", "Kw.h", "Kw.h", "" };

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
