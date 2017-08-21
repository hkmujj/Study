using System;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using Motor.ATP._200H.Resource;

namespace Motor.ATP._200H
{
    /// <summary>
    /// 功能描述 MainAreoA类 
    ///     信号屏主界面 A区显示信息 包括预警信息 目标距离信息
    /// 创建人：袁 凯    
    /// 创建时间：2013-12-13
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class MainAreoA : baseClass
    {
        #region 私有字段

        private RectText m_AText; //A 区 警 示 信 息

        /// <summary>
        /// 制 动 预 警
        /// </summary>
        private readonly RectWarn m_Rectwarn = new RectWarn();

        private bool m_Warn;

        #endregion

        #region 接口数据

        /// <summary>
        /// 制动预警时间
        /// </summary>
        private int m_ZhiDongYuJinTime;


        /// <summary>
        /// 目标距离
        /// </summary>
        private int m_TargetDistance;

        /// <summary>
        /// 是否处于TSM模式（true 表示处于该模式）
        /// </summary>
        private bool m_IsTsm;

        /// <summary>
        /// 是否处于CSM模式
        /// </summary>
        private bool m_IsCSM;

        /// <summary>
        /// 列车当前速度
        /// </summary>
        private int m_CurrentSpeed;

        /// <summary>
        /// 列车允许速度
        /// </summary>
        private int m_AllowSpeed;

        /// <summary>
        /// 预警速度
        /// </summary>
        private int m_YuJinSpeed;

        #endregion

        #region  重载方法

        /// <summary>
        /// 获取图元对象名称
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "信号屏主界面A区信息显示区域";
        }

        /// <summary>
        /// 图元初始化过程执行过程 (只在初始化过程执行一次)
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            nErrorObjectIndex = -1;

            return true;
        }

        /// <summary>
        /// 绘制方法(该方法会被定时器按一定的时间间隔重复调用) 
        /// </summary>
        /// <param name="dcGs">参数 GDI+ 绘图对象</param>
        public override void paint(Graphics dcGs)
        {
            base.paint(dcGs);
            GetValue();
            RefreshData();
            OnDraw(dcGs);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化数据方法
        /// </summary>
        private void InitData()
        {
            #region A 区 警 示 信 息 坐 标 信 息

            m_AText = new RectText();
            m_AText.SetBkColor(1, 2, 3);
            m_AText.SetTextColor(255, 255, 255);
            m_AText.SetTextStyle(14, FormatStyle.Center, true, "宋体");
            m_AText.SetTextRect(Common2D.RectA[1].X + 5, Common2D.RectA[1].Y + 3, Common2D.RectA[1].Width - 10, 30);
            //A_text.isdrawrectfrm = true;

            #endregion
        }

        #endregion

        /// <summary>
        /// 获取接口数据
        /// </summary>
        private void GetValue()
        {
            m_ZhiDongYuJinTime = Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.Inf制动预警时间)]); //制动预警时间

            if (m_ZhiDongYuJinTime < 0)
            {
                m_ZhiDongYuJinTime = 0;
            }

            m_TargetDistance = Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.Inf目标距离)]); //目标距离

            if (m_TargetDistance < 0)
            {
                m_TargetDistance = 0;
            }

            m_CurrentSpeed = Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.Inf当前速度)]);
            m_AllowSpeed = Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.Inf允许速度)]);
            m_YuJinSpeed = Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.Inf预警速度)]);

            m_IsTsm = BoolList[this.GetInboolIndex(InBoolKeys.InbTSM标志)]; //TSM状态
            m_IsCSM = BoolList[this.GetInboolIndex(InBoolKeys.InbCSM标志)]; //CSM模式
        }

        /// <summary>
        /// 刷新数据方法
        /// </summary>
        private void RefreshData()
        {
            m_AText.SetText(m_TargetDistance.ToString());

            m_Warn = false;
            if (m_IsCSM) //CSM模式
            {
                //制动预警
                //大小
                if (m_ZhiDongYuJinTime == 0)
                {
                    m_Rectwarn.SetRect(54);
                    m_Warn = true;
                }
                else if (m_ZhiDongYuJinTime <= 4)
                {
                    m_Rectwarn.SetRect(40);
                    m_Warn = true;
                }
                else if (m_ZhiDongYuJinTime < 8)
                {
                    m_Rectwarn.SetRect(25);
                    m_Warn = true;
                }
                else if (m_ZhiDongYuJinTime == 8)
                {
                    m_Rectwarn.SetRect(5);
                    m_Warn = true;
                }
                else
                {
                    m_Warn = false;
                }
                //颜色
                if (m_CurrentSpeed <= m_AllowSpeed)
                {
                    m_Rectwarn.SetBKColor(195, 195, 195); //灰色
                    //isudu = 0;
                }
                else if (m_CurrentSpeed > m_AllowSpeed && m_CurrentSpeed <= m_YuJinSpeed)
                {
                    m_Rectwarn.SetBKColor(234, 145, 0); //橙色
                    // isudu = 1;
                }
                else if (m_CurrentSpeed > m_YuJinSpeed)
                {
                    m_Rectwarn.SetBKColor(191, 0, 2); //红色
                    // isudu = 2;
                }
            }

                #region TSM 模式	

            else if (m_IsTsm) //TSM模式
            {
                //制动预警
                //大小
                if (m_ZhiDongYuJinTime == 0)
                {
                    m_Rectwarn.SetRect(54);
                    m_Warn = true;
                }
                else if (m_ZhiDongYuJinTime <= 4)
                {
                    m_Rectwarn.SetRect(40);
                    m_Warn = true;
                }
                else if (m_ZhiDongYuJinTime < 8)
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
                if (m_CurrentSpeed <= m_AllowSpeed)
                {
                    m_Rectwarn.SetBKColor(223, 223, 0); //黄色
                    // isudu = 3;
                }
                else if (m_CurrentSpeed > m_AllowSpeed && m_CurrentSpeed <= m_YuJinSpeed)
                {
                    m_Rectwarn.SetBKColor(234, 145, 0); //橙色
                    //isudu = 4;
                }
                else if (m_CurrentSpeed > m_YuJinSpeed)
                {
                    m_Rectwarn.SetBKColor(191, 0, 2); //红色
                    // isudu = 5;
                }
            }

            #endregion

            if (Main.CurrentModel != ControModelEnum.完全)
            {
                m_Warn = false;
            }
        }

        /// <summary>
        /// 绘制方法
        /// </summary>
        /// <param name="g"></param>
        private void OnDraw(Graphics g)
        {
            //制动预警
            //if (Main.Model == ControModelEnum.完全)//当前只有处于完全模式才显示预警时间
            //{
            //    Warn = true;
            //}
            //else
            //{
            //    Warn = false;
            //}

            if (m_Warn)
            {
                m_Rectwarn.OnDraw(g);
            }

            #region A1区绘制预警标表示 制动前的预警时间

            //if (_isTSM)//TSM模式绘制 预警标
            //{
            //     if (_zhiDongYuJinTime > 8)
            //    {
            //        g.FillRectangle(Common2D.gray_Brush, new RectangleF(Common2D.RectA[0].Width / 2 - 2.5f, Common2D.RectA[0].Y + 
            //            Common2D.RectA[0].Height / 2 - 2.5f,
            //              5, 5));
            //    }
            //    else
            //    {
            //        g.FillRectangle(Common2D.gray_Brush, new RectangleF(Common2D.RectA[0].Width / 2 - 5 * (8 - _zhiDongYuJinTime) / 2, 
            //            Common2D.RectA[0].Y + Common2D.RectA[0].Height / 2 -
            //            5 * (8 - _zhiDongYuJinTime) / 2, 5 * (8 - _zhiDongYuJinTime), 5 * (8 - _zhiDongYuJinTime)));
            //    }
            //}

            //if (_isCSM)//当处于CSM模式时 绘制预警标
            //{
            //    if (_zhiDongYuJinTime <= 8)
            //    {
            //        g.FillRectangle(Common2D.gray_Brush, new RectangleF(Common2D.RectA[0].Width / 2 - 5 * (8 - _zhiDongYuJinTime) / 2, Common2D.RectA[0].Y + Common2D.RectA[0].Height / 2 -
            //        5 * (8 - _zhiDongYuJinTime) / 2, 5 * (8 - _zhiDongYuJinTime), 5 * (8 - _zhiDongYuJinTime)));
            //    }
            //}

            #endregion

            #region A2区 以柱状图显示 目标距离

            if (m_IsTsm && m_Warn) //当处于TSM模式时显示目标距离;  当前改为只有在 TSM以及 完全模式才显示目标距离
            {
                m_AText.OnDraw(g);
                g.DrawLine(Common2D.WhitePen4, Common2D.RectA[1].X + 17, Common2D.RectA[1].Y + 250,
                    Common2D.RectA[1].X + 37, Common2D.RectA[1].Y + 250);
                g.DrawLine(Common2D.WhitePen4, Common2D.RectA[1].X + 17, Common2D.RectA[1].Y + 50,
                    Common2D.RectA[1].X + 37, Common2D.RectA[1].Y + 50);
                for (int i = 1; i < 7; i++)
                {
                    g.DrawLine(Common2D.WhitePen2, Common2D.RectA[1].X + 20, Common2D.RectA[1].Y + 250 - 20*i,
                        Common2D.RectA[1].X + 35, Common2D.RectA[1].Y + 250 - 20*i);
                }
                g.DrawLine(Common2D.WhitePen4, Common2D.RectA[1].X + 17, Common2D.RectA[1].Y + 90,
                    Common2D.RectA[1].X + 37, Common2D.RectA[1].Y + 90);

                for (int i = 1; i < 5; i++)
                {
                    g.DrawLine(Common2D.WhitePen2, Common2D.RectA[1].X + 25, Common2D.RectA[1].Y + 50 + 10*i,
                        Common2D.RectA[1].X + 37, Common2D.RectA[1].Y + 50 + 10*i);
                }

                if (m_TargetDistance >= 0 && m_TargetDistance <= 1000)
                {
                    g.FillRectangle(Common2D.WhiteBrush,
                        new Rectangle(Common2D.RectA[1].X + 40,
                            Common2D.RectA[1].Y + 250 - Convert.ToInt32(m_TargetDistance/5),
                            15, Convert.ToInt32(m_TargetDistance/5)));
                }
                else
                {
                    g.FillRectangle(Common2D.WhiteBrush, new Rectangle(Common2D.RectA[1].X + 40, Common2D.RectA[1].Y +
                                                                                                 250 - 200, 15, 200));
                }
            }

            #endregion
        }
    }
}
