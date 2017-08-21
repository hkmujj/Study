using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using ATPComControl;
using ATPComControl.SpeedArc;
using MMI.Facility.Interface.Attribute;
using Motor.ATP._200H.Common;
using Motor.ATP._200H.Resource;

namespace Motor.ATP._200H
{
    /// <summary>
    /// 功能描述 MainAeroB类 
    ///     信号屏主界面 B区显示信息 包括预警信息 
    /// 创建人：袁 凯    
    /// 创建时间：2013-12-13
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class MainAeroB : ShowTypeEffectBaseClass
    {
        #region 私有字段

        private RectText m_BText; //B 区 控 制 模 式 信 息
        private readonly int m_StartAngle = 135;

        // Create coordinates of rectangle to bound ellipse.
        public int m_X = Common2D.RectB[0].X + 25;
        public int m_Y = Common2D.RectB[0].Y + 20;
        public int m_Width = 280;
        public int m_Height = 280;

        public Graphics m_Picpointer;
        private int m_Izhizhen;
        public float m_Anglev = 0;
        private readonly string[] m_SpeedStr = {"400", "300", "200", "150", "100", "50", "0"};
        private Image[] m_Img;
        private SpeedPointer m_SpeedPointer;

        private readonly Font m_SpeedValueFont = new Font("宋体", 16, FontStyle.Bold);

        private readonly StringFormat m_SpeedValueStringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        #endregion

        #region 绘制速度的元素

        /// <summary>
        /// 限速刻度画刷
        /// </summary>
        private Pen m_XianShuKeDuPen = Common2D.GrayPen20;

        /// <summary>
        /// 绘制限速圆弧的画刷
        /// </summary>
        private Pen m_XianShuArcPen = Common2D.GrayPen12;

        /// <summary>
        /// 目标速度画刷
        /// </summary>
        private Pen m_TargetSpeedPen = Common2D.GrayPen12;


        #endregion

        #region 接口数据

        /// <summary>
        /// 是否处于TSM模式（true 表示处于该模式）
        /// </summary>
        private bool m_IsTsm;

        /// <summary>
        /// 是否处于CSM模式
        /// </summary>
        private bool m_IsCSM;

        /// <summary>
        /// 当前速度
        /// </summary>
        private int m_CurrentSpeed;

        /// <summary>
        /// 目标速度
        /// </summary>
        private int m_TargetSpeed;

        /// <summary>
        /// 允许速度
        /// </summary>
        private int m_AllowSpeed;

        /// <summary>
        /// EBI紧急制动速度
        /// </summary>
        private int m_EbiSpeed;

        /// <summary>
        /// SBI最大常用制动速度
        /// </summary>
        private int m_SbiSpeed;

        /// <summary>
        /// 当前控制模式
        /// </summary>
        private ControModelEnum m_ControlMode;

        #endregion

        private ISpeedConverter m_SpeedConverter;

        private Image m_ImagePointer;
        

        #region  重载方法

        /// <summary>
        /// 获取图元对象名称
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "信号屏主界面B区信息显示区域";
        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        protected override void Initalize()
        {
            if (UIObj.ParaList.Count >= 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    m_Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
                }
            }

            m_Img = new Image[]
            {
                ImageResource.zhizhenhui, ImageResource.zhizhenhuang, ImageResource.zhizhencheng, ImageResource.zhizhenhong
            };

            m_SpeedConverter = new ATP200HConvert();
            var tmp = new RectangleF(100, 30, 270, 270);
            m_SpeedPointer = new SpeedPointer(225,
                -45f,
                -45f,
                400,
                0,
                tmp,
                new PointF(tmp.X + tmp.Width/2, tmp.Y + tmp.Height/2),
                m_ImagePointer,
                m_SpeedConverter);

            #region B 区 控 制 模 式 信 息

            m_BText = new RectText();
            m_BText.SetBkColor(1, 2, 3);
            m_BText.SetTextColor(255, 255, 255);
            m_BText.SetTextStyle(16, FormatStyle.Center, true, "宋体");
            m_BText.SetTextRect(Common2D.RectB[5].X + 3, Common2D.RectB[5].Y + 3, Common2D.RectB[5].Width - 8, 35);

            #endregion
        }

        /// <summary>
        /// 绘制方法(该方法会被定时器按一定的时间间隔重复调用) 
        /// </summary>
        /// <param name="dcGs">参数 GDI+ 绘图对象</param>
        public override void paint(Graphics dcGs)
        {
            dcGs.SmoothingMode = SmoothingMode.HighQuality;
            RefreshShowType();

            if (ShowType == ShowType.Normal)
            {
                GetValue();
                RefreshData();
                RefreshDrawElement();
                OnDraw(dcGs);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 数据获取方法
        /// </summary>
        private void GetValue()
        {
            var adc = GlobalParam.Instance.DetailConfig.AdapterConfig;
            m_CurrentSpeed =
                Math.Abs(
                    Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.Inf当前速度)]*adc.CurrentSpeedCoefficient));
            m_TargetSpeed =
                Math.Abs(Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.Inf目标速度)]*adc.TargetSpeedCoefficient));
            m_AllowSpeed =
                Math.Abs(Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.Inf允许速度)]*adc.PermitSpeedCoefficient));
                //允许速度(限速)
            m_ControlMode = ControModelEnumConvert.ConvertFrom(FloatList[this.GetInFloatIndex(InFloatKeys.Inf控制模式1到7)]);
            m_SbiSpeed =
                Math.Abs(
                    Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.Inf最大常用制动速度SBI)]*
                                    adc.NormalBrakeSpeedCoefficient));
            m_EbiSpeed =
                Math.Abs(
                    Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.Inf紧急制动速度EBI)]*
                                    adc.EmergBrakeSpeedCoefficient));

            m_IsTsm = BoolList[this.GetInboolIndex(InBoolKeys.InbTSM标志)]; //TSM状态
            m_IsCSM = BoolList[this.GetInboolIndex(InBoolKeys.InbCSM标志)]; //CSM模式
        }

        /// <summary>
        /// 刷新数据方法
        /// </summary>
        private void RefreshData()
        {
        }

        /// <summary>
        /// 绘制方法
        /// </summary>
        /// <param name="g"></param>
        private void OnDraw(Graphics g)
        {
            OnDrawV(g);
            DrawSpeedTable(g);
        }

        /// <summary>
        /// 刷新绘制 弧 刻度以及 指针元素
        /// </summary>
        private void RefreshDrawElement()
        {
            SetXianShuPen();
            SetTargetArcPen();
            SetPointColor();
        }

        /// <summary>
        /// 速度相关
        /// </summary>
        /// <param name="g"></param>
        public void OnDrawV(Graphics g)
        {
            switch (IsDrawArc())
            {
                case DrawModelEnum.DrawDetail:
                    //绘制限速信息
                    g.DrawArc(m_XianShuArcPen, m_X, m_Y, m_Width, m_Height, m_StartAngle, SetSweepAngle(m_AllowSpeed));
                    g.DrawArc(m_XianShuKeDuPen, m_X + 8, m_Y + 8, m_Width - 16, m_Height - 16,
                        m_StartAngle + SetSweepAngle(m_AllowSpeed) - 3, 3);

                    //绘制目标速度
                    g.DrawArc(m_TargetSpeedPen, m_X, m_Y, m_Width, m_Height, m_StartAngle, SetSweepAngle(m_TargetSpeed));
                    break;
                case DrawModelEnum.DrawKeDu:
                    //绘制限速刻度
                    g.DrawArc(m_XianShuKeDuPen, m_X + 8, m_Y + 8, m_Width - 16, m_Height - 16,
                        m_StartAngle + SetSweepAngle(m_AllowSpeed) - 3, 3);
                    break;
            }

            //绘制当前速度超过限速情况
            if (m_CurrentSpeed > m_AllowSpeed && m_CurrentSpeed < m_EbiSpeed)
            {
                if (IsDrawCaoShuBiaoZhi())
                {
                    g.DrawArc(Common2D.OrangePen20, m_X + 8, m_Y + 8, m_Width - 16, m_Height - 16,
                        m_StartAngle + SetSweepAngle(m_CurrentSpeed) - 3, 3);
                }
            }
            else if (m_CurrentSpeed > m_EbiSpeed)
            {
                if (IsDrawCaoShuBiaoZhi())
                {
                    g.DrawArc(Common2D.RedPen20, m_X + 8, m_Y + 8, m_Width - 16, m_Height - 16,
                        m_StartAngle + SetSweepAngle(m_SbiSpeed) - 3, 3);
                }
            }

            m_SpeedPointer.PaintPointerColor(g, m_Img[m_Izhizhen], m_CurrentSpeed);
            //显示数值速度
            g.DrawString(Convert.ToInt32(m_CurrentSpeed).ToString(), m_SpeedValueFont,
                Common2D.BlackBrush, m_X + 140, m_Y + 145, m_SpeedValueStringFormat);
        }

        private void DrawSpeedTable(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;

            #region ;;;;;;;;;B 区 表 盘 绘 制;;;;;;;;;;;;;;;;;;;

            //  g.DrawEllipse(Common2D.white_Pen2, Common2D.RectB[0].X +30, Common2D.RectB[0].Y +25,270, 270);
            for (int i = 0; i <= 30; i++)
            {
                if (i%5 == 0)
                {
                    g.DrawLine(Common2D.WhitePen2,
                        Convert.ToInt32(Common2D.RectB[0].X + 165 + 135*Math.Cos((i*9 - 45)*3.1415/180)),
                        Convert.ToInt32(Common2D.RectB[0].Y + 160 - 135*Math.Sin((i*9 - 45)*3.1415/180)),
                        Convert.ToInt32(Common2D.RectB[0].X + 165 + 110*Math.Cos((i*9 - 45)*3.1415/180))
                        , Convert.ToInt32(Common2D.RectB[0].Y + 160 - 110*Math.Sin((i*9 - 45)*3.1415/180)));
                    if (i > 20)
                    {
                        g.DrawString(m_SpeedStr[i/5], Common2D.Font12, Common2D.WhiteBrush,
                            Convert.ToInt32(Common2D.RectB[0].X + 165 + 100*Math.Cos((i*9 - 45)*3.1415/180) - 7),
                            Convert.ToInt32(Common2D.RectB[0].Y + 160 - 100*Math.Sin((i*9 - 45)*3.1415/180) - 7));
                    }
                    else if (i <= 20)
                    {
                        g.DrawString(m_SpeedStr[i/5], Common2D.Font12, Common2D.WhiteBrush,
                            Convert.ToInt32(Common2D.RectB[0].X + 165 + 100*Math.Cos((i*9 - 45)*3.1415/180) - 20),
                            Convert.ToInt32(Common2D.RectB[0].Y + 160 - 100*Math.Sin((i*9 - 45)*3.1415/180) - 7));
                    }

                }
                else
                {
                    g.DrawLine(Common2D.WhitePen2,
                        Convert.ToInt32(Common2D.RectB[0].X + 165 + 135*Math.Cos((i*9 - 45)*3.1415/180)),
                        Convert.ToInt32(Common2D.RectB[0].Y + 160 - 135*Math.Sin((i*9 - 45)*3.1415/180)),
                        Convert.ToInt32(Common2D.RectB[0].X + 165 + 125*Math.Cos((i*9 - 45)*3.1415/180))
                        , Convert.ToInt32(Common2D.RectB[0].Y + 160 - 125*Math.Sin((i*9 - 45)*3.1415/180)));
                }
            }

            #endregion
        }

        /// <summary>
        /// 判断是否需要绘制弧形
        /// </summary>
        /// <returns></returns>
        private DrawModelEnum IsDrawArc()
        {
            switch (m_ControlMode)
            {
                case ControModelEnum.部分:
                case ControModelEnum.目视:
                case ControModelEnum.调车:
                case ControModelEnum.引导:
                    return DrawModelEnum.DrawKeDu;

                case ControModelEnum.LKJ:
                case ControModelEnum.待机:
                    return DrawModelEnum.DrawNull;

                case ControModelEnum.完全:
                    return DrawModelEnum.DrawDetail;
            }
            return DrawModelEnum.DrawNull;
        }

        #endregion


        #region 设置绘制元素的方法

        /// <summary>
        /// 设置绘值限速刻度,以及弧形画刷颜色
        /// </summary>
        /// <returns></returns>
        private void SetXianShuPen()
        {
            if (m_IsCSM) //运行在CSM区
            {
                m_XianShuKeDuPen = Common2D.GrayPen20;
                m_XianShuArcPen = Common2D.GrayPen12;
            }
            else if (m_IsTsm) //运行在TSM区
            {
                m_XianShuKeDuPen = Common2D.YellowPen20;
                m_XianShuArcPen = Common2D.YellowPen12;
            }
        }

        /// <summary>
        /// 设置目标速度圆弧
        /// </summary>
        private void SetTargetArcPen()
        {
            if (m_IsCSM) //运行在CSM区
            {
                m_TargetSpeedPen = Common2D.DarkGrayPen12;
            }
            else if (m_IsTsm) //运行在TSM区
            {
                m_TargetSpeedPen = Common2D.GrayPen12;
            }
        }

        /// <summary>
        /// 设置角度
        /// </summary>
        private int SetSweepAngle(int allowSpeed)
        {
            if (allowSpeed <= 200)
            {
                return Convert.ToInt32(allowSpeed*135/150f);
            }
            if (allowSpeed <= 400)
            {
                return Convert.ToInt32((allowSpeed - 200)*135/300f + 180);
            }

            return 0;
        }

        /// <summary>
        /// 设置指针颜色
        /// </summary>
        private void SetPointColor()
        {
            if (m_IsCSM)
            {
                if (m_CurrentSpeed <= m_AllowSpeed)
                {
                    m_Izhizhen = 0;
                }
                else if (m_CurrentSpeed > m_AllowSpeed && m_CurrentSpeed <= m_EbiSpeed)
                {
                    m_Izhizhen = 2;
                }
                else
                {
                    m_Izhizhen = 3;
                }
            }

            if (m_IsTsm)
            {
                if (m_CurrentSpeed < m_TargetSpeed)
                {
                    m_Izhizhen = 0;
                }
                else if (m_CurrentSpeed < m_AllowSpeed)
                {
                    m_Izhizhen = 1;
                }
                else if (m_CurrentSpeed < m_EbiSpeed)
                {
                    m_Izhizhen = 2;
                }
                else
                {
                    m_Izhizhen = 3;
                }
            }

            //LKJ 部分模式指针一直灰色
            if (IsDrawCaoShuBiaoZhi() == false)
            {
                m_Izhizhen = 0;
            }
        }

        /// <summary>
        /// 判断是否需要绘制预警信息
        /// </summary>
        /// <returns></returns>
        private bool IsDrawCaoShuBiaoZhi()
        {
            if (m_ControlMode == ControModelEnum.LKJ || m_ControlMode == ControModelEnum.待机)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}