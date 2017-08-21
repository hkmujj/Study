using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CommonUtil.Util;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Model;
using Motor.ATP._300D.Common;
using Motor.ATP._300D.ControlModel;
using Motor.ATP._300D.Station;

namespace Motor.ATP._300D
{
    [GksDataType(DataType.isMMIObjectClass)]
    class DMIMainMenu2D : ATPBase, IButtonResponser
    {
        static readonly int FC_X = FrameButton2D.FrameChange_X;
        static readonly int FC_Y = FrameButton2D.FrameChange_Y;



        private static float[] fValue;
        bool[] m_BValue = new bool[50];
        Graphics picpointer;
        Image iOutImg;
        float anglev = 0;

        readonly StringFormat m_DrawFormat = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

        //制动预警
        readonly RectWarn m_Rectwarn = new RectWarn();

        bool m_Warn = false;

        //目标距离
        readonly RectProgress m_TargetDistanceProgress = new RectProgress();
        readonly SolidBrush m_TargetDistanceBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        readonly SolidBrush blackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        readonly Font m_TargetDistanceFont = new Font(FormatStyle.StrFont, 16f);

        RectangleF m_DistanceRec = new RectangleF();

        //圆弧
        readonly Pen anhuiPen = new Pen(Color.FromArgb(85, 85, 85), 12);
        readonly Pen huiPen = new Pen(Color.FromArgb(195, 195, 195), 12);
        readonly Pen huangPen = new Pen(Color.FromArgb(223, 223, 0), 12);

        readonly Pen huiPen2 = new Pen(Color.FromArgb(195, 195, 195), 26);
        readonly Pen huangPen2 = new Pen(Color.FromArgb(223, 223, 0), 26);
        readonly Pen hongPen2 = new Pen(Color.FromArgb(191, 0, 2), 26);
        readonly Pen chengPen2 = new Pen(Color.FromArgb(234, 145, 0), 26);

        // Create coordinates of rectangle to bound ellipse.
        readonly float x = 89F + FC_X;
        readonly float y = 20F + FC_Y;
        private const float Width = 286;
        private const float Height = 286;

        // Create start and sweep angles on ellipse.
        float m_StartAngle = 0.0F;

        /// <summary>
        /// 指针旋转角度
        /// </summary>
        float m_PointerSweepAngle = 360.0F;

        /// <summary>
        /// 指针ID
        /// </summary>
        int m_SpeedPointerType = 1;

        /// <summary>
        /// 速度颜色不同情况
        /// </summary>
        int m_Speed = 1;

        RectangleF m_SpeedRegion;

        /// <summary>
        /// 命令图标区
        /// </summary>
        readonly PointF[] cmdpoint = new PointF[3];

        int cmdflag1 = 0;
        int cmdcount1 = 0;
        int cmdflag2 = 0;
        int cmdcount2 = 0;
        int cmdflag3 = 0;
        int cmdcount3 = 0;

        //运行计划信息
        readonly PointF[] planpoint = new PointF[2];
        readonly Pen planPen = new Pen(Color.FromArgb(200, 200, 200), 1);
        readonly Pen planPen1 = new Pen(Color.FromArgb(255, 255, 255), 2);
        readonly Pen planPen2 = new Pen(Color.FromArgb(200, 200, 200), 2);
        readonly Font planFont = new Font(FormatStyle.StrFont, 10f);

        //车站信息
        readonly SortedList<int, string> m_StationList = new SortedList<int, string>();

        //按钮
        static public String[] ControlButtonContentCollection = new String[8];
        static public String[] InputButtonContentCollection = new String[10];

        static public int FlashState = -1;

        /// <summary>
        /// 车次号
        /// </summary>
        static public char[] TrainNumber = new char[8];
        static public char[] DriverID = new char[8];

        /// <summary>
        /// 当前界面视图编号
        /// </summary>
        ViewType m_CurrentViewType = 0;

        readonly Font cFont = new Font("幼圆", 14f);

        /// <summary>
        /// 提示信息
        /// </summary>
        public static string Popuptxt { private get; set; }

        bool bfirst = true;

        /// <summary>
        /// 车站信息解析器
        /// </summary>
        private IStationInfoInterpreter m_StationInfoInterpreter;

        private ATPDomainBase m_ATP;

        private ControlModelView m_NectControlModelView;

        static DMIMainMenu2D()
        {
            Popuptxt = " ";
        }

        public void GetValue()
        {
            if (UIObj.InFloatList.Count >= 1)
            {
                for (var i = 0; i < fValue.Length; i++)
                {
                    if (i == 12)
                    {
                        continue;
                    }
                    fValue[i] = FloatList[UIObj.InFloatList[i]];
                }
            }
            m_BValue = UIObj.InBoolList.Select(s => BoolList[s]).ToArray();

        }

        public override bool Initalize()
        {
            ButtonVeiw.Instance.AddResponser(this);

            LoadStationInfo();

            m_StationInfoInterpreter = new ByteStationInfoInterpreter();
            m_StationInfoInterpreter.Initalize(m_StationList);

            m_ATP = ATPRepository.GetATP(this);

            m_NectControlModelView = new ControlModelView()
                                     {
                                         Location = new Point(215 + FC_X, 363 + FC_Y),
                                         Size = new Size(50, 50),
                                         ControlType = ControlType.Unknown
                                     };

            if (UIObj.ParaList.Count >= 0)
            {
                InitDate();
            }
            else
            {
                return false;
            }
            return true;
        }

        public override void paint(Graphics g)
        {
            try
            {
                GetValue();
                RefreshDate();
                DMIText();
                OnDraw(g);
            }
            catch (Exception e)
            {
                LogMgr.Error(string.Format("occuse some error when {0} .paint(Graphics). {1}", GetType(), e));
            }
        }

        public void RefreshDate()
        {
            RefreshFSModel();

            RefreshModelNotFSModel();
        }

        private void RefreshFSModel()
        {
            m_Speed = 7;
            m_Warn = false;
            if (m_BValue[0]) //CSM模式
            {
                RefreshCSMData();
            }
            else if (m_BValue[1]) //TSM模式
            {
                RefreshTSMData();
            }
        }

        private void RefreshModelNotFSModel()
        {
            //非完全监控模式下速度显示方式
            if ((int)fValue[7] == 2
                || (int)fValue[7] == 3
                || (int)fValue[7] == 5
                || (int)fValue[7] == 8
                || ((int)fValue[7] == 4 && (int)fValue[8] == 1))
            {
                m_Speed = 6;
            }
            if ((int)fValue[7] == 6)
            {
                m_Speed = 7;
            }
        }

        private void RefreshTSMData()
        {
            RefreshInAdvanceInfo();

            RefreshTargetDistance();
        }

        private void RefreshTargetDistance()
        {
            //目标距离
            if (fValue[6] < 100)
            {
                m_TargetDistanceProgress.SetRect(fValue[6] * 41 / 100f);
            }
            else if (fValue[6] < 500)
            {
                m_TargetDistanceProgress.SetRect(41 + (float)(47 * Math.Log10(fValue[6] / 100) / Math.Log10(2)));
            }
            else if (fValue[6] < 1000)
            {
                m_TargetDistanceProgress.SetRect(155 + (float)(47 * Math.Log10(fValue[6] / 500) / Math.Log10(2)));
            }
            else if (fValue[6] >= 1000)
            {
                m_TargetDistanceProgress.SetRect(202);
            }
        }

        private void RefreshInAdvanceInfo()
        {
            //制动预警
            //大小
            if (Math.Abs(fValue[5]) < float.Epsilon)
            {
                m_Rectwarn.SetRect(54);
                m_Warn = true;
            }
            else if (fValue[5] <= 4)
            {
                m_Rectwarn.SetRect(40);
                m_Warn = true;
            }
            else if (fValue[5] < 8)
            {
                m_Rectwarn.SetRect(25);
                m_Warn = true;
            }
            else
            {
                m_Rectwarn.SetRect(5);
                m_Warn = true;
            }


            //颜色
            if (fValue[0] <= fValue[2])
            {
                m_Rectwarn.SetBKColor(223, 223, 0); //黄色
                m_Speed = 3;
            }
            else if (fValue[0] > fValue[2] && fValue[0] <= fValue[1])
            {
                m_Rectwarn.SetBKColor(234, 145, 0); //橙色
                m_Speed = 4;
            }
            else if (fValue[0] > fValue[1])
            {
                m_Rectwarn.SetBKColor(191, 0, 2); //红色
                m_Speed = 5;
            }
        }

        private void RefreshCSMData()
        {
            //制动预警
            //大小
            if (Math.Abs(fValue[5]) < float.Epsilon)
            {
                m_Rectwarn.SetRect(54);
                m_Warn = true;
            }
            else if (fValue[5] <= 4)
            {
                m_Rectwarn.SetRect(40);
                m_Warn = true;
            }
            else if (fValue[5] < 8)
            {
                m_Rectwarn.SetRect(25);
                m_Warn = true;
            }
            else if (Math.Abs(fValue[5] - 8) < float.Epsilon)
            {
                m_Rectwarn.SetRect(5);
                m_Warn = true;
            }

            //颜色
            if (fValue[0] <= fValue[2])
            {
                m_Rectwarn.SetBKColor(195, 195, 195); //灰色
                m_Speed = 0;
            }
            else if (fValue[0] > fValue[2] && fValue[0] <= fValue[1])
            {
                m_Rectwarn.SetBKColor(234, 145, 0); //橙色
                m_Speed = 1;
            }
            else if (fValue[0] > fValue[1])
            {
                m_Rectwarn.SetBKColor(191, 0, 2); //红色
                m_Speed = 2;
            }
        }

        public override string GetInfo()
        {
            return "DMI主页面";
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                var oldViewType = m_CurrentViewType;
                var oldFlashSstate = FlashState;

                m_CurrentViewType = (ViewType)nParaB;
                FlashState = -1;

                switch ((ViewType)nParaB)
                {
                    case ViewType.Main:
                        ControlButtonContentCollection[0] = "";
                        ControlButtonContentCollection[1] = "";
                        ControlButtonContentCollection[2] = "";
                        ControlButtonContentCollection[3] = "";
                        ControlButtonContentCollection[4] = "";
                        ControlButtonContentCollection[5] = "";
                        ControlButtonContentCollection[6] = "";
                        ControlButtonContentCollection[7] = "";

                        ClearInputButtonContents();

                        bfirst = true;
                        break;
                    case ViewType.DriverID:
                        break;
                    case ViewType.TrainNumber:
                        break;
                    case ViewType.Data:
                        break;
                    case ViewType.ControlLevel:
                        break;
                    case ViewType.Other:
                        break;
                    case ViewType.ShowData:
                        break;
                    case ViewType.ETCSTest:
                        break;
                    case ViewType.Model:
                        break;
                    case ViewType.ConfirmStart:
                        break;
                    case (ViewType)110:
                        break;
                    case (ViewType)111:
                        break;
                    case ViewType.InputTrainData:
                        break;

                    default:
                        m_CurrentViewType = oldViewType;
                        FlashState = oldFlashSstate;
                        break;
                }
            }
        }

        /// <summary>
        /// 清除输入button 的按键内容
        /// </summary>
        public static void ClearInputButtonContents()
        {
            InputButtonContentCollection[0] = "";
            InputButtonContentCollection[1] = "";
            InputButtonContentCollection[2] = "";
            InputButtonContentCollection[3] = "";
            InputButtonContentCollection[4] = "";
            InputButtonContentCollection[5] = "";
            InputButtonContentCollection[6] = "";
            InputButtonContentCollection[7] = "";
            InputButtonContentCollection[8] = "";
            InputButtonContentCollection[9] = "";
        }

        public void InitDate()
        {
            Graphics tmpGraphic = Graphics.FromImage(Images[1]);
            tmpGraphic.DrawImage(Images[1], 0, 0, Images[1].Width, Images[1].Height);

            iOutImg = new Bitmap(Images[1].Width, Images[1].Height, tmpGraphic);
            picpointer = Graphics.FromImage(iOutImg);

            picpointer.TranslateTransform(Images[1].Width / 2, Images[1].Height / 2);
            picpointer.RotateTransform(0);
            picpointer.DrawImage(Images[1], new Point(-Images[1].Width / 2, -Images[1].Height / 2));

            picpointer.ResetTransform();

            //目标距离
            m_TargetDistanceProgress.SetPRcolor(255, 255, 255);
            m_TargetDistanceProgress.SetRect(49 + FC_X, 320 + FC_Y, 14, 0);
            m_TargetDistanceProgress.Setlevel(1);
            m_DistanceRec.X = 0 + FC_X;
            m_DistanceRec.Y = 60 + FC_Y;
            m_DistanceRec.Width = 72;
            m_DistanceRec.Height = 50;
            //速度
            m_SpeedRegion.X = 208 + FC_X;
            m_SpeedRegion.Y = 138 + FC_Y;
            m_SpeedRegion.Width = 50;
            m_SpeedRegion.Height = 50;

            //命令图标区
            cmdpoint[0].X = 183 + FC_X;
            cmdpoint[0].Y = 317 + FC_Y;

            cmdpoint[1].X = 225 + FC_X;
            cmdpoint[1].Y = 317 + FC_Y;

            cmdpoint[2].X = 265 + FC_X;
            cmdpoint[2].Y = 317 + FC_Y;

            fValue = new float[UIObj.InFloatList.Count];


        }

        //速度相关
        private void DrawSpeed(Graphics g)
        {
            //速度刻度
            g.DrawImage(Images[0], 88 + FC_X, 18 + FC_Y, Images[0].Width, Images[0].Height);

            if (m_Speed < 6)
            {
                if (fValue[4] < 150)
                {
                    m_PointerSweepAngle = fValue[4] * 135 / 150f + 5;
                }
                else if (fValue[4] <= 450)
                {
                    m_PointerSweepAngle = (fValue[4] - 150) * 145 / 300f + 140;
                }
                //圆弧
                if ((int)FloatList[50] != 3 && (int)FloatList[50] != 5)
                {
                    g.DrawArc(anhuiPen, x, y, Width, Height, 125, m_PointerSweepAngle);
                }

                m_StartAngle = m_PointerSweepAngle + 125;
            }

            switch (m_Speed)
            {
                case 0:
                    m_SpeedPointerType = 1;

                    m_PointerSweepAngle = ConvertSpeedToSweepAngle(fValue[2]);

                    //圆弧
                    if ((int)FloatList[50] != 3 && (int)FloatList[50] != 5)
                    {
                        g.DrawArc(huiPen, x, y, Width, Height, m_StartAngle, m_PointerSweepAngle - m_StartAngle + 125);
                    }
                    g.DrawArc(huiPen2, x + 7, y + 7, Width - 14, Height - 14, m_StartAngle + m_PointerSweepAngle - m_StartAngle + 125 - 3, 3);
                    break;
                case 1:

                    m_PointerSweepAngle = ConvertSpeedToSweepAngle(fValue[2]);

                    //圆弧
                    if ((int)FloatList[50] != 3 && (int)FloatList[50] != 5)
                    {
                        g.DrawArc(huangPen, x, y, Width, Height, m_StartAngle, m_PointerSweepAngle - m_StartAngle + 125);
                    }
                    g.DrawArc(huangPen2, x + 7, y + 7, Width - 14, Height - 14, m_StartAngle + m_PointerSweepAngle - m_StartAngle + 125 - 3, 3);

                    if (m_BValue[3])
                    {
                        m_SpeedPointerType = 3;

                        g.DrawArc(hongPen2, x + 7, y + 7, Width - 14, Height - 14, m_StartAngle + m_PointerSweepAngle - m_StartAngle + 125, 3);
                    }
                    else
                    {
                        m_SpeedPointerType = 5;

                        g.DrawArc(chengPen2, x + 7, y + 7, Width - 14, Height - 14, m_StartAngle + m_PointerSweepAngle - m_StartAngle + 125, 3);
                    }
                    break;
                case 2:
                    m_PointerSweepAngle = ConvertSpeedToSweepAngle(fValue[2]);

                    //圆弧
                    if ((int)FloatList[50] != 3 && (int)FloatList[50] != 5)
                    {
                        g.DrawArc(huangPen, x, y, Width, Height, m_StartAngle, m_PointerSweepAngle - m_StartAngle + 125);
                    }
                    g.DrawArc(huangPen2, x + 7, y + 7, Width - 14, Height - 14, m_StartAngle + m_PointerSweepAngle - m_StartAngle + 125 - 3, 3);

                    if (m_BValue[3])
                    {
                        m_SpeedPointerType = 3;

                        g.DrawArc(hongPen2, x + 7, y + 7, Width - 14, Height - 14, m_StartAngle + m_PointerSweepAngle - m_StartAngle + 125, 3);
                    }
                    else
                    {
                        m_SpeedPointerType = 5;

                        g.DrawArc(chengPen2, x + 7, y + 7, Width - 14, Height - 14, m_StartAngle + m_PointerSweepAngle - m_StartAngle + 125, 3);
                    }
                    break;
                case 3:
                    m_SpeedPointerType = fValue[0] > fValue[4] ? 4 : 1;

                    m_PointerSweepAngle = ConvertSpeedToSweepAngle(fValue[2]);

                    //圆弧
                    if ((int)FloatList[50] != 3 && (int)FloatList[50] != 5)
                    {
                        g.DrawArc(huangPen, x, y, Width, Height, m_StartAngle, m_PointerSweepAngle - m_StartAngle + 125);
                    }
                    g.DrawArc(huangPen2, x + 7, y + 7, Width - 14, Height - 14, m_StartAngle + m_PointerSweepAngle - m_StartAngle + 125 - 3, 3);
                    break;
                case 4:

                    m_PointerSweepAngle = ConvertSpeedToSweepAngle(fValue[2]);

                    //圆弧
                    if ((int)FloatList[50] != 3 && (int)FloatList[50] != 5)
                    {
                        g.DrawArc(huangPen, x, y, Width, Height, m_StartAngle, m_PointerSweepAngle - m_StartAngle + 125);
                    }
                    g.DrawArc(huangPen2, x + 7, y + 7, Width - 14, Height - 14, m_StartAngle + m_PointerSweepAngle - m_StartAngle + 125 - 3, 3);
                    if (m_BValue[3])
                    {
                        m_SpeedPointerType = 3;

                        g.DrawArc(hongPen2, x + 7, y + 7, Width - 14, Height - 14, m_StartAngle + m_PointerSweepAngle - m_StartAngle + 125, 3);
                    }
                    else
                    {
                        m_SpeedPointerType = 5;
                        g.DrawArc(chengPen2, x + 7, y + 7, Width - 14, Height - 14, m_StartAngle + m_PointerSweepAngle - m_StartAngle + 125, 3);
                    }
                    break;
                case 5:

                    m_PointerSweepAngle = ConvertSpeedToSweepAngle(fValue[2]);

                    //圆弧
                    if ((int)FloatList[50] != 3 && (int)FloatList[50] != 5)
                    {
                        g.DrawArc(huangPen, x, y, Width, Height, m_StartAngle, m_PointerSweepAngle - m_StartAngle + 125);
                    }
                    g.DrawArc(huangPen2, x + 7, y + 7, Width - 14, Height - 14, m_StartAngle + m_PointerSweepAngle - m_StartAngle + 125 - 3, 3);
                    if (m_BValue[3])
                    {
                        m_SpeedPointerType = 3;
                        g.DrawArc(hongPen2, x + 7, y + 7, Width - 14, Height - 14, m_StartAngle + m_PointerSweepAngle - m_StartAngle + 125, 3);
                    }
                    else
                    {
                        m_SpeedPointerType = 5;
                        g.DrawArc(chengPen2, x + 7, y + 7, Width - 14, Height - 14, m_StartAngle + m_PointerSweepAngle - m_StartAngle + 125, 3);
                    }
                    break;

                case 6:
                    m_SpeedPointerType = 1;

                    m_PointerSweepAngle = ConvertSpeedToSweepAngle(fValue[2]);

                    if ((int)FloatList[50] != 3 && (int)FloatList[50] != 5)
                    {
                        g.DrawArc(huiPen2, x + 7, y + 7, Width - 14, Height - 14, 130 + m_PointerSweepAngle - 3, 3);
                    }

                    if (fValue[0] > fValue[2])
                    {
                        if (m_BValue[3])
                        {
                            m_SpeedPointerType = 3;
                            g.DrawArc(hongPen2, x + 7, y + 7, Width - 14, Height - 14, 130 + m_PointerSweepAngle, 3);
                        }
                        else
                        {
                            m_SpeedPointerType = 5;
                            g.DrawArc(chengPen2, x + 7, y + 7, Width - 14, Height - 14, 130 + m_PointerSweepAngle, 3);
                        }
                    }
                    break;
                case 7:
                    m_SpeedPointerType = 1;
                    break;

            }

            //指针
            Graphics tmpGraphic = Graphics.FromImage(Images[m_SpeedPointerType]);
            //tmpGraphic.DrawImage(Img[izhizhen], 0, 0, Img[izhizhen].Width, Img[izhizhen].Height);

            iOutImg = new Bitmap(Images[m_SpeedPointerType].Width, Images[m_SpeedPointerType].Height, tmpGraphic);
            picpointer = Graphics.FromImage(iOutImg);

            picpointer.TranslateTransform(Images[m_SpeedPointerType].Width / 2, Images[m_SpeedPointerType].Height / 2);
            if (fValue[0] < 150)
            {
                anglev = fValue[0] * 135 / 150f - 50;
            }
            else if (fValue[0] <= 450)
            {
                anglev = (fValue[0] - 150) * 145 / 300f + 85;
            }
            picpointer.RotateTransform(anglev);
            picpointer.DrawImage(Images[m_SpeedPointerType], new Point(-Images[m_SpeedPointerType].Width / 2, -Images[m_SpeedPointerType].Height / 2));

            picpointer.ResetTransform();
            //指针
            if (iOutImg != null)
            {

                g.DrawImage(iOutImg,
                    79 + FC_X,
                    09 + FC_Y,
                    new RectangleF(0, 0, Convert.ToSingle(iOutImg.Width), Convert.ToSingle(iOutImg.Height)),
                    GraphicsUnit.Pixel);
                iOutImg.Dispose();
            }

            m_DrawFormat.Alignment = StringAlignment.Far;
            m_DrawFormat.LineAlignment = StringAlignment.Center;
            g.DrawString(((int)(fValue[0])).ToString(), m_TargetDistanceFont, m_SpeedPointerType == 3 ? m_TargetDistanceBrush : blackBrush, m_SpeedRegion, m_DrawFormat);
        }

        private float ConvertSpeedToSweepAngle(float value)
        {
            var sweepAngle = 0f;
            if (value < 150)
            {
                sweepAngle = value * 135 / 150f + 5;
            }
            else if (value <= 450)
            {
                sweepAngle = (value - 150) * 145 / 300f + 140;
            }
            return sweepAngle;
        }

        //计划信息区横坐标值
        public float getx(float distance)
        {
            float location = 0;
            if (distance < 0)
            {
                distance = 0;
            }
            else if (distance > 16000)
            {
                distance = 16000;
            }
            switch ((int)fValue[12])
            {
                case 0:
                    if (distance < 100f)
                    {
                        location = 425 + distance * 32 / 100f;
                    }
                    else if (distance < 500f)
                    {
                        location = (float)(457 + 35 * Math.Log10(distance / 100f) / Math.Log10(2));
                    }
                    else
                    {
                        location = (float)((457 + 35 * Math.Log10(5) / Math.Log10(2)) + 32 * Math.Log10(distance / 500f) / Math.Log10(2));
                    }
                    break;
                case 1:
                    if (distance < 500f)
                    {
                        location = 416 + distance * 80 / 500f;
                    }
                    else
                    {
                        location = (float)(496 + 40 * Math.Log10(distance / 500f) / Math.Log10(2));
                    }
                    break;
                case 2:
                    if (distance < 250f)
                    {
                        location = 416 + distance * 80 / 1000f;
                    }
                    else
                    {
                        location = (float)(496 + 40 * Math.Log10(distance / 250f) / Math.Log10(2));
                    }
                    break;
            }

            return location + 1;
        }

        //计划信息区纵坐标值
        public float gety(float x)
        {
            if (x < 0)
                x = 0;
            else if (x > 300)
                x = 300;
            float y = x * 0.5f;

            return 218 - y;

        }

        //计划信息区
        public void OnDrawplan(Graphics g)
        {
            //MRSP
            int yugao;
            float x = 425;
            float y = 218;
            float lastx = 425;
            float lasty = 218;
            for (int i = 0; i < 11; i++)
            {
                x = getx(fValue[15 + i * 2]);
                y = gety(fValue[16 + i * 2]);

                if (i != 0)
                {
                    if (x <= 425 || x > 700)//容错处理
                    {
                        x = 700;
                    }
                    if (i == 10)
                    {
                        x = 700;
                        y = gety(fValue[16 + 9 * 2]);
                    }
                    g.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 200)), lastx + FC_X, lasty + FC_Y, x - lastx, 218 - lasty);

                }
                lastx = x;
                lasty = y;
            }

            //坐标
            planpoint[0].Y = 68 + FC_Y;
            planpoint[1].Y = 315 + FC_Y;

            planpoint[1].X = planpoint[0].X = 425 + FC_X;
            g.DrawLine(planPen2, planpoint[0], planpoint[1]);//0
            planpoint[1].X = planpoint[0].X = 457 + FC_X;
            g.DrawLine(planPen, planpoint[0], planpoint[1]);//100

            for (int i = 0; i < 3; i++)
            {
                planpoint[1].X = planpoint[0].X = (float)(457 + 35 * Math.Log10(i + 2) / Math.Log10(2) + FC_X);
                g.DrawLine(planPen, planpoint[0], planpoint[1]);//100
            }
            planpoint[1].X = planpoint[0].X = (float)(457 + 35 * Math.Log10(5) / Math.Log10(2) + FC_X);
            g.DrawLine(planPen1, planpoint[0], planpoint[1]);//500

            for (int i = 0; i < 4; i++)
            {
                planpoint[1].X = planpoint[0].X = planpoint[0].X + 32;
                g.DrawLine(planPen, planpoint[0], planpoint[1]);
            }

            planpoint[1].X = planpoint[0].X = planpoint[0].X + 32;
            g.DrawLine(planPen2, planpoint[0], planpoint[1]);
            if ((int)fValue[12] == 0)
            {
                g.DrawString("0", planFont, m_TargetDistanceBrush, 422 + FC_X, 324 + FC_Y);
                g.DrawString("100", planFont, m_TargetDistanceBrush, 446 + FC_X, 324 + FC_Y);
                g.DrawString("200", planFont, m_TargetDistanceBrush, 480 + FC_X, 324 + FC_Y);
                g.DrawString("500", planFont, m_TargetDistanceBrush, 526 + FC_X, 324 + FC_Y);
                g.DrawString("1k", planFont, m_TargetDistanceBrush, 560 + FC_X, 324 + FC_Y);
                g.DrawString("2k", planFont, m_TargetDistanceBrush, 592 + FC_X, 324 + FC_Y);
                g.DrawString("4k", planFont, m_TargetDistanceBrush, 624 + FC_X, 324 + FC_Y);
                g.DrawString("8k", planFont, m_TargetDistanceBrush, 656 + FC_X, 324 + FC_Y);
            }
            else if ((int)fValue[12] == 1)
            {
                g.DrawString("0", planFont, m_TargetDistanceBrush, 418 + FC_X, 360 + FC_Y);
                g.DrawString("0.5k", planFont, m_TargetDistanceBrush, 490 + FC_X, 360 + FC_Y);
                g.DrawString("1k", planFont, m_TargetDistanceBrush, 530 + FC_X, 360 + FC_Y);
                g.DrawString("2k", planFont, m_TargetDistanceBrush, 570 + FC_X, 360 + FC_Y);
                g.DrawString("4k", planFont, m_TargetDistanceBrush, 610 + FC_X, 360 + FC_Y);
                g.DrawString("8k", planFont, m_TargetDistanceBrush, 650 + FC_X, 360 + FC_Y);
                g.DrawString("16k", planFont, m_TargetDistanceBrush, 690 + FC_X, 360 + FC_Y);

            }
            else if ((int)fValue[12] == 2)
            {
                g.DrawString("0", planFont, m_TargetDistanceBrush, 418 + FC_X, 360 + FC_Y);
                g.DrawString("0.25k", planFont, m_TargetDistanceBrush, 490 + FC_X, 360 + FC_Y);
                g.DrawString("0.5k", planFont, m_TargetDistanceBrush, 530 + FC_X, 360 + FC_Y);
                g.DrawString("1k", planFont, m_TargetDistanceBrush, 570 + FC_X, 360 + FC_Y);
                g.DrawString("2k", planFont, m_TargetDistanceBrush, 610 + FC_X, 360 + FC_Y);
                g.DrawString("4k", planFont, m_TargetDistanceBrush, 650 + FC_X, 360 + FC_Y);
                g.DrawString("8k", planFont, m_TargetDistanceBrush, 690 + FC_X, 360 + FC_Y);

            }


            float lastx1 = 0;
            float lastx2 = 0;
            x = 0;
            //预告
            int cmdflag = 0;
            for (int i = 0; i < 10; i++)
            {
                yugao = (int)fValue[46 + i * 2];
                if (yugao != 0)
                {
                    x = getx(fValue[45 + i * 2]);
                    if (x < 425 || x > 700)//容错处理
                    {
                        continue;
                    }

                    if (i % 2 == 0)
                    {
                        if (x > lastx1)
                        {
                            g.DrawImage(Images[43 + yugao], x + FC_X, 260 + FC_Y, Images[43 + yugao].Width, Images[43 + yugao].Height);
                            lastx1 = x + 23;
                        }
                    }
                    else
                    {
                        if (x > lastx2)
                        {
                            g.DrawImage(Images[43 + yugao], x + FC_X, 300 + FC_X, Images[43 + yugao].Width, Images[43 + yugao].Height);
                            lastx2 = x + 23;
                        }
                    }
                }
            }

            //坡度
            lastx1 = 425;
            lastx2 = 0;
            int podu = 0;
            int lastpodu = 0;

            m_DrawFormat.Alignment = StringAlignment.Center;
            m_DrawFormat.LineAlignment = StringAlignment.Center;
            for (int i = 0; i < 5; i++)
            {
                x = getx(fValue[37 + i * 2]);
                if ((x < 425 || x > 700) && i != 4)//容错处理
                {
                    continue;
                }

                podu = (int)fValue[36 + i * 2];
                if (i == 4 && fValue[37 + 3 * 2] > 0)
                {
                    x = 696;
                    podu = lastpodu;
                }
                if (podu > 0)
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(195, 195, 195)), lastx1 + FC_X, 228 + FC_Y, x - lastx1, 13);
                    if (x - lastx1 > 29)
                    {
                        g.DrawString("+" + podu.ToString() + "+",
                            new Font(FormatStyle.StrFont, 12f),
                            new SolidBrush(Color.FromArgb(85, 85, 85)),
                            new RectangleF(lastx1 + FC_X, 228 + FC_Y, x - lastx1, 13),
                            m_DrawFormat);
                    }
                }
                else if (podu == 0)
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(195, 195, 195)), lastx1 + FC_X, 228 + FC_Y, x - lastx1, 13);
                    if (x - lastx1 > 29)
                    {
                        g.DrawString("0",
                            new Font(FormatStyle.StrFont, 12f),
                            new SolidBrush(Color.FromArgb(85, 85, 85)),
                            new RectangleF(lastx1 + FC_X, 228 + FC_Y, x - lastx1, 13),
                            m_DrawFormat);
                    }
                }
                else if (podu < 0)
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(85, 85, 85)), lastx1 + FC_X, 228 + FC_Y, x - lastx1, 13);
                    if (x - lastx1 > 29)
                    {
                        g.DrawString("-" + (-podu).ToString() + "-",
                            new Font(FormatStyle.StrFont, 12f),
                            new SolidBrush(Color.FromArgb(195, 195, 195)),
                            new RectangleF(lastx1 + FC_X, 228 + FC_Y, x - lastx1, 13),
                            m_DrawFormat);
                    }

                }
                lastpodu = podu;
                lastx1 = x;
            }

            planpoint[1].X = planpoint[0].X = getx(fValue[65]) + FC_X;
            planpoint[0].Y = 53 + FC_Y;
            planpoint[1].Y = 68 + FC_Y;
            if (fValue[65] > 0)
            {
                g.DrawLine(new Pen(Color.FromArgb(223, 223, 0), 3), planpoint[0], planpoint[1]);
            }


            //加减速
            for (int i = 0; i < 10; i++)
            {
                x = getx(fValue[66 + i * 2]);
                if ((x < 425 || x > 700))//容错处理
                {
                    continue;
                }

                podu = (int)fValue[67 + i * 2];

                if ((podu <= 0 || podu >= 4))//容错处理
                {
                    continue;
                }
                g.DrawImage(Images[58 + podu], x + FC_X, 200 + FC_Y, Images[58 + podu].Width, Images[58 + podu].Height);

            }

        }
        public void OnDraw(Graphics g)
        {
            DrawBackgroud(g);

            g.DrawString(Popuptxt, cFont, m_TargetDistanceBrush, 84 + FC_X, 430 + FC_Y);

            DrawButton(g);

            bfirst = false;

            if (m_CurrentViewType == ViewType.InputTrainData)
            {
                return;
            }

            DrawSpeed(g);

            DrawInAdvanceInfo(g);

            DrawBrakeAlert(g);

            DrawTargetDistance(g);

            DrawControlModel(g);

            DrawLevelInfo(g);

            DrawBrakeState(g);

            DrawPlanArear(g);

            DrawCabSignal(g);

            //时间日期
            g.DrawString(Background2D.StrtimeH, cFont, m_TargetDistanceBrush, new RectangleF(310 + FC_X, 11 + FC_Y, 100, 30), m_DrawFormat);

            m_NectControlModelView.ControlType = m_ATP.ControlModel.NextControlModel.Type;
            m_NectControlModelView.OnDraw(g);
        }

        /// <summary>
        /// 预告信息
        /// </summary>
        /// <param name="g"></param>
        private void DrawInAdvanceInfo(Graphics g)
        {
            if (BoolList[411])
            {
                g.DrawImage(Images[77], 225 + FC_X, 373 + FC_Y, Images[77].Width, Images[77].Height);
            }
            if (BoolList[462])
            {
                g.DrawImage(Images[54], cmdpoint[0].X, cmdpoint[0].Y, Images[54].Width, Images[54].Height);
            }
        }

        private void DrawCabSignal(Graphics g)
        {
            //机车信号
            if (m_CurrentViewType == ViewType.Main)
            {
                var signal = (int)fValue[14];
                if (signal != 0)
                {
                    if (signal == 11 || signal == 13 || signal == 14)
                    {
                        if (Background2D.TimeNeedFlash)
                        {
                            g.DrawImage(Images[24 + signal], 426 + FC_X, 5 + FC_Y, Images[24 + signal].Width, Images[24 + signal].Height);
                        }
                    }
                    else
                    {
                        g.DrawImage(Images[24 + signal], 426 + FC_X, 5 + FC_Y, Images[24 + signal].Width, Images[24 + signal].Height);
                    }
                }
            }
        }

        private void DrawPlanArear(Graphics g)
        {
            //计划信息区
            if (((int)fValue[7] == 1 || (int)fValue[7] == 4) && m_CurrentViewType == ViewType.Main)
            {
                OnDrawplan(g);
                //公里标
                m_DrawFormat.Alignment = (StringAlignment)0;
                m_DrawFormat.LineAlignment = StringAlignment.Center;
                g.FillRectangle(m_TargetDistanceBrush, 544 + FC_X, 340 + FC_Y, 172, 21);
                g.DrawString("K" + ((int)(fValue[13] / 1000f)).ToString() + " + " + (((int)fValue[13]) % 1000).ToString() + "m",
                    FormatStyle.Font12,
                    blackBrush,
                    new RectangleF(544 + FC_X, 340 + FC_Y, 172, 21),
                    m_DrawFormat);
            }
        }

        private void DrawBrakeState(Graphics g)
        {
            //制动状态
            if (m_BValue[5])
            {
                g.DrawImage(Images[24], 10 + FC_X, 410 + FC_Y, Images[24].Width, Images[24].Height);
            }
            else if (m_BValue[4])
            {
                g.DrawImage(Images[22], 10 + FC_X, 410 + FC_Y, Images[22].Width, Images[22].Height);
            }
            else if (m_BValue[3])
            {
                g.DrawImage(Images[23], 10 + FC_X, 410 + FC_Y, Images[23].Width, Images[23].Height);
            }
        }

        private void DrawLevelInfo(Graphics g)
        {
            //级别信息
            if ((int)fValue[8] > 0 && (int)fValue[8] < 4)
            {
                g.DrawImage(Images[72 + (int)fValue[8]], 4 + FC_X, 367 + FC_Y, Images[72 + (int)fValue[8]].Width, Images[72 + (int)fValue[8]].Height);
            }
        }

        private void DrawControlModel(Graphics g)
        {
            //模式信息         
            if ((int)fValue[7] > 0 && (int)fValue[7] < 11)
            {
                g.DrawImage(Images[61 + (int)fValue[7]], 328 + FC_X, 313 + FC_Y, Images[61 + (int)fValue[7]].Width, Images[61 + (int)fValue[7]].Height);
            }
        }

        private void DrawTargetDistance(Graphics g)
        {
            //目标距离
            if (m_BValue[1] && (fValue[0] > fValue[4])) //TSM模式
            {
                g.DrawImage(Images[2], 20 + FC_X, 100 + FC_Y, Images[2].Width, Images[2].Height);
                m_TargetDistanceProgress.OnDraw(g);
                m_DrawFormat.Alignment = StringAlignment.Center;
                m_DrawFormat.LineAlignment = StringAlignment.Center;
                if (fValue[6] < 1000)
                {
                    g.DrawString(((int)fValue[6]).ToString(), m_TargetDistanceFont, m_TargetDistanceBrush, m_DistanceRec, m_DrawFormat);
                }
                else if (fValue[6] >= 1000)
                {
                    g.DrawString(((int)(fValue[6] / 10f)).ToString() + "0", m_TargetDistanceFont, m_TargetDistanceBrush, m_DistanceRec, m_DrawFormat);
                }
            }
        }

        private void DrawBrakeAlert(Graphics g)
        {
            //制动预警
            if (m_Warn)
            {
                m_Rectwarn.OnDraw(g);
            }
        }

        private void DrawBackgroud(Graphics g)
        {
            switch (m_CurrentViewType)
            {
                case ViewType.Main:
                    g.DrawImage(Images[6], 0 + FC_X, 0 + FC_Y, Images[6].Width, Images[6].Height);
                    OnDrawMainkey();
                    OnBtnClick();
                    break;
                case (ViewType)111:
                case (ViewType)110:
                case ViewType.ConfirmStart:
                case ViewType.Model:
                case ViewType.ETCSTest:
                case ViewType.ShowData:
                case ViewType.Other:
                case ViewType.ControlLevel:
                case ViewType.Data:
                case ViewType.TrainNumber:
                case ViewType.DriverID:
                    g.DrawImage(Images[7], 0 + FC_X, 0 + FC_Y, Images[7].Width, Images[7].Height);
                    break;
                case ViewType.InputTrainData:
                    g.DrawImage(Images[8], 0 + FC_X, 0 + FC_Y, Images[8].Width, Images[8].Height);
                    break;
            }
        }

        private void DrawButton(Graphics g)
        {
            m_DrawFormat.Alignment = StringAlignment.Center;
            m_DrawFormat.LineAlignment = StringAlignment.Center;
            for (int i = 0; i < ControlButtonContentCollection.Length; i++)
            {
                if (ControlButtonContentCollection[i] != "")
                {
                    if (FlashState == i && Background2D.TimeNeedFlash)
                    {
                        g.FillRectangle(m_TargetDistanceBrush, new RectangleF(723 + FC_X, 1 + 75 * i + FC_Y, 72, 73));
                        g.DrawString(ControlButtonContentCollection[i],
                            FormatStyle.Font14,
                            blackBrush,
                            new RectangleF(723 + FC_X, 1 + 75 * i + FC_Y, 72, 73),
                            m_DrawFormat);
                    }
                    else
                    {
                        g.DrawString(ControlButtonContentCollection[i],
                            FormatStyle.Font14,
                            m_TargetDistanceBrush,
                            new RectangleF(723 + FC_X, 1 + 75 * i + FC_Y, 72, 73),
                            m_DrawFormat);
                    }
                }
            }

            for (var i = 0; i < InputButtonContentCollection.Length; i++)
            {
                if (InputButtonContentCollection[i] != "")
                {
                    g.DrawString(InputButtonContentCollection[i],
                        FormatStyle.Font14,
                        m_TargetDistanceBrush,
                        new RectangleF(2 + 73 * i + FC_X, 527 + FC_Y, 72, 73),
                        m_DrawFormat);
                }
            }
        }

        private void DMIText()
        {
            //车站
            if (m_CurrentViewType == ViewType.Main)
            {
                Popuptxt = m_StationInfoInterpreter.Interpret(fValue[10]);
            }

            foreach (var source in BoolList.Keys.Skip(NotifyConstant.NormalNotifyStartIndex)
                                           .Take(NotifyConstant.NormalNotifyCount)
                                           .Where(w => StrDrivData2D.NotifyInfoCollection.ContainsKey(w) && BoolList[w] && !BoolOldList[w]))
            {
                Popuptxt = StrDrivData2D.NotifyInfoCollection[source].Content;
                break;
            }

            foreach (var source in BoolList.Keys.Skip(NotifyConstant.ConfirmNotifyStartIndex)
                                           .Take(NotifyConstant.ConfirmNotifyCount)
                                           .Where(w => StrDrivData2D.NotifyInfoCollection.ContainsKey(w) && BoolList[w] && !BoolOldList[w]))
            {
                StrDrivData2D.CurrentNotifyInfoItem = StrDrivData2D.NotifyInfoCollection[source];
                Popuptxt = StrDrivData2D.CurrentNotifyInfoItem.Content;
                append_postCmd(CmdType.ChangePage, 109, 0, 0);
                break;
            }

            var btnDown = BoolList.Keys.Skip(ButtonConstant.StartLogicIndex).Take(ButtonConstant.LogicIndexCount).Any(a => BoolList[a] && !BoolOldList[a]);

            if (m_CurrentViewType == ViewType.ConfirmStart && btnDown && StrDrivData2D.CurrentNotifyInfoItem.LogicIndex != 380 && StrDrivData2D.CurrentNotifyInfoItem.LogicIndex != 381)
            {
                Popuptxt = string.Empty;
                append_postCmd(CmdType.ChangePage, 100, 0, 0);
            }
        }

        private void OnDrawMainkey()
        {
            var hasSpeed = Math.Abs(FloatList[62]) > 0;

            if (hasSpeed)
            {
                DrawRunningControlBtnContent();
            }
            else
            {
                DrawParkingControlBtnContent();
            }
        }

        private void OnBtnClick()
        {
            if (bfirst)
            {
                return;
            }
            ResponseDown(Background2D.PressedButtonIndex);
            ResponseUp(Background2D.PoppedButtonIndex);
        }

        private void OnBottomControlButtonDown(ButtonType btnType)
        {
            switch (btnType)
            {
                case ButtonType.B1:
                    append_postCmd(CmdType.SetBoolValue, 4835, 1, 0);
                    break;
                case ButtonType.B2:
                    append_postCmd(CmdType.SetBoolValue, 4836, 1, 0);
                    break;
                case ButtonType.B3:
                    append_postCmd(CmdType.SetBoolValue, 4837, 1, 0);
                    break;
                case ButtonType.B4:
                    append_postCmd(CmdType.SetBoolValue, 4838, 1, 0);
                    break;
                case ButtonType.B5:
                    append_postCmd(CmdType.SetBoolValue, 4839, 1, 0);
                    break;
                case ButtonType.BSwitch:
                    append_postCmd(CmdType.SetBoolValue, 4831, 1, 0);
                    break;
            }


        }

        private void OnBottomControlButtonUp(ButtonType btnType)
        {
            switch (btnType)
            {
                case ButtonType.B1:
                    append_postCmd(CmdType.SetBoolValue, 4835, 0, 0);
                    break;
                case ButtonType.B2:
                    append_postCmd(CmdType.SetBoolValue, 4836, 0, 0);
                    break;
                case ButtonType.B3:
                    append_postCmd(CmdType.SetBoolValue, 4837, 0, 0);
                    break;
                case ButtonType.B4:
                    append_postCmd(CmdType.SetBoolValue, 4838, 0, 0);
                    break;
                case ButtonType.B5:
                    append_postCmd(CmdType.SetBoolValue, 4839, 0, 0);
                    break;
                case ButtonType.BSwitch:
                    append_postCmd(CmdType.SetBoolValue, 4831, 0, 0);
                    break;
            }
        }

        private void OnBtnDownWhenParking(ButtonType btnIndex)
        {
            if ((int)fValue[7] == 6 || (int)fValue[7] == 0)
            {
                switch (btnIndex)
                {
                    case ButtonType.F1:
                        append_postCmd(CmdType.ChangePage, 103, 0, 0);
                        break;
                    case ButtonType.F2:
                        append_postCmd(CmdType.ChangePage, 108, 0, 0);
                        break;
                    case ButtonType.F3:
                        if (Background2D.bfirstshuju)
                        {
                            append_postCmd(CmdType.ChangePage, 110, 0, 0);
                        }
                        break;
                    case ButtonType.F4:
                        append_postCmd(CmdType.ChangePage, 104, 0, 0);
                        break;
                    case ButtonType.F5:
                        append_postCmd(CmdType.ChangePage, 105, 0, 0);
                        break;
                    case ButtonType.F6:
                        if (Background2D.bfirstshuju)
                        {
                            if (StrDrivData2D.NotifyInfoCollection.ContainsKey(380))
                            {
                                StrDrivData2D.CurrentNotifyInfoItem = StrDrivData2D.NotifyInfoCollection[380];
                                Popuptxt = StrDrivData2D.CurrentNotifyInfoItem.Content;
                                append_postCmd(CmdType.ChangePage, 109, 0, 0);
                            }
                        }
                        break;
                }
            }
            else
            {
                switch (btnIndex)
                {
                    case ButtonType.F1:
                        append_postCmd(CmdType.ChangePage, 103, 0, 0);
                        break;
                    case ButtonType.F2:
                        append_postCmd(CmdType.ChangePage, 108, 0, 0);
                        break;
                    case ButtonType.F3:
                        if (Background2D.bfirstshuju)
                        {
                            append_postCmd(CmdType.ChangePage, 110, 0, 0);
                        }
                        break;
                    case ButtonType.F4:
                        append_postCmd(CmdType.ChangePage, 104, 0, 0);
                        break;
                    case ButtonType.F5:
                        append_postCmd(CmdType.ChangePage, 105, 0, 0);
                        break;
                }
            }
        }

        private void OnBtnDownWhenRunning(ButtonType btnIndex)
        {
            switch (btnIndex)
            {
                case ButtonType.F1:
                    append_postCmd(CmdType.ChangePage, 103, 0, 0);
                    break;
                case ButtonType.F3:
                    append_postCmd(CmdType.ChangePage, 110, 0, 0);
                    break;
                case ButtonType.F5:
                    append_postCmd(CmdType.ChangePage, 105, 0, 0);
                    break;
            }

            ClearInputButtonContents();
        }

        private void DrawParkingControlBtnContent()
        {
            ControlButtonContentCollection[0] = "数据";
            ControlButtonContentCollection[1] = "模式";
            ControlButtonContentCollection[2] = "";
            ControlButtonContentCollection[3] = "列控系统等级";
            ControlButtonContentCollection[4] = "其他";
            ControlButtonContentCollection[5] = "";
            ControlButtonContentCollection[6] = "";
            ControlButtonContentCollection[7] = "";

            if (Background2D.bfirstshuju)
            {
                ControlButtonContentCollection[2] = "专用";
                if ((int)fValue[7] == 6 || (int)fValue[7] == 0)
                {
                    ControlButtonContentCollection[5] = "启动";
                }
            }

            ClearInputButtonContents();
        }

        private static void DrawRunningControlBtnContent()
        {
            ControlButtonContentCollection[0] = "数据";
            ControlButtonContentCollection[1] = "";
            ControlButtonContentCollection[2] = "专用";
            ControlButtonContentCollection[3] = "";
            ControlButtonContentCollection[4] = "其他";
            ControlButtonContentCollection[5] = "";
            ControlButtonContentCollection[6] = "";
            ControlButtonContentCollection[7] = "";

            if ((int)fValue[7] == 1 || (int)fValue[7] == 4)
            {
                ControlButtonContentCollection[6] = "放大";
                ControlButtonContentCollection[7] = "缩小";
            }

            ClearInputButtonContents();
        }

        private void LoadStationInfo()
        {
            var file = Path.Combine(RecPath, "..\\Config\\车站信息.txt");

            if (File.Exists(file))
            {
                var allLines = File.ReadAllLines(file);
                foreach (var cStr in allLines)
                {
                    var split = cStr.Split(new[] { ' ', ',' }).ToList();
                    split.RemoveAll(string.IsNullOrWhiteSpace);

                    if (split.Count < 2)
                    {
                        return;
                    }

                    try
                    {
                        m_StationList.Add(int.Parse(split[0]), split[1]);
                    }
                    catch (Exception e)
                    {
                        LogMgr.Warn(string.Format("Can not parser station info of \"{0}\" {1}", cStr, e));
                    }
                }
            }
            else
            {
                LogMgr.Error(string.Format("Can not found needed file {0}", file));
            }
        }

        public bool ResponseDown(ButtonType btnIndex)
        {
            if (!UIObj.ViewList.Contains(CurrentViewIdex)||m_CurrentViewType!=ViewType.Main)
            {
                return false;
            }

            var hasSpeed = Math.Abs(FloatList[62]) > 0;
            if (hasSpeed)
            {
                OnBtnDownWhenRunning(btnIndex);
            }
            else
            {
                OnBtnDownWhenParking(btnIndex);
            }

            OnBottomControlButtonDown(btnIndex);

            return true;
        }

        public bool ResponseUp(ButtonType btnType)
        {
            if (!UIObj.ViewList.Contains(CurrentViewIdex) || m_CurrentViewType != ViewType.Main)
            {
                return false;
            }
            OnBottomControlButtonUp(btnType);
            return false;
        }
    }
}
