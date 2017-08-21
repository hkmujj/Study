using System;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Global;
using MMI.Facility.Interface.Attribute;

namespace Motor.HMI.CRH1A.Brake
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    class GT_Brakes : CRH1BaseClass
    {
        GT_MenuTitle m_Title = new GT_MenuTitle("制动");//菜单标题
        Rectangle m_Recposition = new Rectangle(0, 170, 800, 100);
        readonly CRH1AButton[] m_BottomButtons = new CRH1AButton[2];//底部按钮
        Rectangle[] m_TrainRect = new Rectangle[8];//表示各车厢的边框
        Rectangle[] m_NoRect = new Rectangle[8];//车编号显示位置    
        Rectangle[] m_EmergencyRect = new Rectangle[8];//紧急制动状态显示位置
        Rectangle[] m_StopRect = new Rectangle[5];//停放制动状态显示位置
        GDIRectText[] m_GText = new GDIRectText[8];//显示制动力的文本框
        Rectangle[] m_StrRect = new Rectangle[8];
        Rectangle m_Rect;//页脚区域

        SolidBrush m_Recbrush = new SolidBrush(Color.FromArgb(119, 136, 153));//背景画刷
        SolidBrush m_GreenBrush = new SolidBrush(Color.FromArgb(0, 126, 0));//绿色画刷
        SolidBrush m_RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));//红色画刷
        SolidBrush m_BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));//红色画刷 

        Pen m_Rectpen = new Pen(Color.FromArgb(209, 226, 242), 3);//背景画笔

        Font m_Strfont = new Font("Arial", 11);
        Point[] m_Mc1 = new Point[6];//左司机室的坐标
        Point[] m_Mc2 = new Point[6];//右司机室的坐标

        float[] m_Valuef = new float[8];
        Image[] m_Image = new Image[5];

        /// <summary>
        /// 紧急制动状态 0表示解除 1表示实施
        /// </summary>
        private bool[] m_IsEnmergencyBrakeStatus = new bool[8];

        /// <summary>
        /// 停放制动 
        /// </summary>
        private bool[] m_IsTingFangBrakeStatus = new bool[5];

        /// <summary>
        /// 制动可用性  0表示可用 1表示不可用
        /// </summary>
        private bool[] m_IsBrakeUseful = new bool[8];

        /// <summary>
        /// 车轮防滑状态(1表示处于工作状态)
        /// </summary>
        private bool m_IsCheLunFangFaStatus;

        public override string GetInfo()
        {
            return "制动系统状态";
        }


        public override bool Initialize()
        {
            if (UIObj.ParaList.Count >= 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    m_Image[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
                }
            }
            InitData();

            return true;
        }
        public override void paint(Graphics g)
        {
            GetValue();
            ReFreshData();
            DrawOn(g);
        }

        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 22)
            {
                //紧急制动状态
                for (int i = 0; i < 8; i++)
                {
                    m_IsEnmergencyBrakeStatus[i] = BoolList[UIObj.InBoolList[i]];

                }

                //制动可用性
                for (int i = 0; i < 8; i++)
                {
                    m_IsBrakeUseful[i] = BoolList[UIObj.InBoolList[i + 8]];
                }

                //停放制动状态
                for (int i = 0; i < 5; i++)
                {
                    m_IsTingFangBrakeStatus[i] = BoolList[UIObj.InBoolList[i + 16]];
                }

                //获取车轮防滑状态
                m_IsCheLunFangFaStatus = BoolList[UIObj.InBoolList[21]];
            }

            if (UIObj.InFloatList.Count >= 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    m_Valuef[i] = FloatList[UIObj.InFloatList[i]];
                }
            }
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void ReFreshData()
        {
            for (int i = 0; i < 8; i++)
            {
                m_GText[i].SetText(Convert.ToInt32(m_Valuef[i]).ToString());
            }
        }

        public void InitData()
        {
            #region ;;;;;;;;;;;;;;;; 按钮初始化:::::::::::::::::::::::
            m_BottomButtons[0] = new CRH1AButton();
            m_BottomButtons[0].SetButtonRect(m_Recposition.X + 330, m_Recposition.Y + 213, 90, 50);
            m_BottomButtons[0].SetButtonColor(192, 192, 192);
            m_BottomButtons[0].SetButtonText("车轮防滑");
            m_BottomButtons[0].CurrentMouseState = MouseState.MouseDown;
            m_BottomButtons[0].IsEnable = false;

            m_BottomButtons[1] = new CRH1AButton();
            m_BottomButtons[1].SetButtonRect(m_Recposition.X + 450, m_Recposition.Y + 213, 90, 50);
            m_BottomButtons[1].SetButtonColor(192, 192, 192);
            m_BottomButtons[1].SetButtonText("除冰制动");
            m_BottomButtons[1].CurrentMouseState = MouseState.MouseDown;
            m_BottomButtons[1].IsEnable = false;
            m_BottomButtons[1].TextColor = CommonResouce.GrayBrush.Color;
            m_BottomButtons[1].RefreshAction += o =>
            {
                var btn = ((CRH1AButton)o);
                if (GlobalParam.Instance.TrainInfo.AbsSpeed >= 80 && !btn.IsEnable)
                {
                    btn.TextColor = Color.Black;
                    btn.CurrentMouseState = MouseState.MouseUp;
                    btn.IsEnable = true;
                }
                else if (GlobalParam.Instance.TrainInfo.AbsSpeed < 80 && btn.IsEnable)
                {
                    btn.TextColor = CommonResouce.GrayBrush.Color;
                    btn.CurrentMouseState = MouseState.MouseDown;
                    btn.IsEnable = false;
                }
            };


            #endregion

            #region::::::::::::;;;;;;;;各 车 边 框 位 置 初 始 化;;;;;;;;;;;;;;;;;;;;;;;;;;;
            //左司机室
            m_Mc1[0] = new Point(m_Recposition.X + 55, m_Recposition.Y + 50);
            m_Mc1[1] = new Point(m_Recposition.X + 115, m_Recposition.Y + 50);
            m_Mc1[2] = new Point(m_Recposition.X + 115, m_Recposition.Y + 110);
            m_Mc1[3] = new Point(m_Recposition.X + 55, m_Recposition.Y + 110);
            m_Mc1[4] = new Point(m_Recposition.X + 30, m_Recposition.Y + 90);
            m_Mc1[5] = new Point(m_Recposition.X + 30, m_Recposition.Y + 70);

            //右司机室
            m_Mc2[0] = new Point(m_Recposition.X + 670, m_Recposition.Y + 50);
            m_Mc2[1] = new Point(m_Recposition.X + 730, m_Recposition.Y + 50);
            m_Mc2[2] = new Point(m_Recposition.X + 755, m_Recposition.Y + 70);
            m_Mc2[3] = new Point(m_Recposition.X + 755, m_Recposition.Y + 90);
            m_Mc2[4] = new Point(m_Recposition.X + 730, m_Recposition.Y + 110);
            m_Mc2[5] = new Point(m_Recposition.X + 670, m_Recposition.Y + 110);
            for (int i = 0; i < 6; i++)
            {
                m_TrainRect[i] = new Rectangle(m_Recposition.X + i * 90 + 125, m_Recposition.Y + 50, 85, 60);
            }
            #endregion

            #region :::::::::::::::::::车编号显示的位置设置;;;;;;;;;;;;;;;;;;;
            for (int i = 0; i < 8; i++)
            {

                m_NoRect[i] = new Rectangle(m_Recposition.X + i * 90 + 65, m_Recposition.Y + 55, 30, 25);
            }
            #endregion

            #region ::::::::::::::::::::页 脚 信 息 区 域 位 置::::::::::::::::::::::::::::::::
            m_Rect = new Rectangle(m_Recposition.X, m_Recposition.Y + 203, 790, 80);

            #endregion

            #region ::::::::::::::::::::紧 急 制 动 状 态 显 示 位 置::::::::::::::::::::::::::::::::

            for (int i = 0; i < 8; i++)
            {
                m_EmergencyRect[i] = new Rectangle(m_NoRect[i].X - 10, m_NoRect[i].Y - 60, 50, 50);
            }
            #endregion

            #region ::::::::::::::::::::停 放 制 动 状 态 位 置::::::::::::::::::::::::::::::::


            m_StopRect[0] = new Rectangle(m_EmergencyRect[0].X, m_EmergencyRect[0].Y + 120, 50, 50);
            m_StopRect[1] = new Rectangle(m_EmergencyRect[2].X, m_EmergencyRect[2].Y + 120, 50, 50);
            m_StopRect[2] = new Rectangle(m_EmergencyRect[3].X, m_EmergencyRect[3].Y + 120, 50, 50);
            m_StopRect[3] = new Rectangle(m_EmergencyRect[5].X, m_EmergencyRect[5].Y + 120, 50, 50);
            m_StopRect[4] = new Rectangle(m_EmergencyRect[7].X, m_EmergencyRect[7].Y + 120, 50, 50);


            #endregion

            #region :::::::::::::::::::::::::制 动 力 显 示 位 置:::::::::::::::;;;;;:::::::

            for (int i = 0; i < 8; i++)
            {
                m_GText[i] = new GDIRectText();
                m_GText[i].SetBkColor(203, 203, 203);
                m_GText[i].SetDrawFrm(true);
                m_GText[i].SetLinePen(173, 173, 173, 2);
                m_GText[i].SetTextColor(0, 128, 0);
                m_GText[i].SetTextStyle(9, FormatStyle.Center, true, "Arial");
                m_GText[i].SetTextRect(m_Recposition.X + i * 90 + 55, m_Recposition.Y + 75, 40, 20);
                m_StrRect[i] = new Rectangle(m_Recposition.X + i * 90 + 95, m_Recposition.Y + 77, 30, 15);

            }
            #endregion


            GlobalInfo.Instance.ButtonStatusChange += () =>
            {
                foreach (var bottomButton in m_BottomButtons)
                {
                    bottomButton.IsEnable = GlobalInfo.Instance.ButtonEnable;
                }
            };
        }
        public void DrawOn(Graphics g)
        {

            //绘制菜单标题
            m_Title.OnDraw(g);

            //绘制紧急制动状态
            for (int i = 0; i < 8; i++)
            {
                g.DrawImage(m_IsEnmergencyBrakeStatus[i] ? m_Image[1] : m_Image[0], m_EmergencyRect[i]);
            }

            //绘制停放制动状态
            for (int i = 0; i < 5; i++)
            {
                g.DrawImage(m_IsTingFangBrakeStatus[i] ? m_Image[2] : m_Image[3], m_StopRect[i]);
            }

            ////绘 制 车 箱 边 框
            g.FillPolygon(m_IsBrakeUseful[0] ? m_RedBrush : m_GreenBrush, m_Mc1);

            //
            for (int i = 0; i < 6; i++)
            {
                g.FillRectangle(m_IsBrakeUseful[i + 1] ? m_RedBrush : m_GreenBrush, m_TrainRect[i]);
            }

            g.FillPolygon(m_IsBrakeUseful[7] ? m_RedBrush : m_GreenBrush, m_Mc2);

            //绘制编号
            for (int i = 0; i < 8; i++)
            {
                if (i < 7)
                {
                    g.DrawString("0" + (i + 1), new Font("Arial", 12), new SolidBrush(Color.White), m_NoRect[i]);
                }
                else
                {
                    g.DrawString("00", new Font("Arial", 12), new SolidBrush(Color.White), m_NoRect[i]);
                }
            }

            //页脚区域绘制

            g.FillRectangle(m_Recbrush, m_Rect);
            g.DrawRectangle(m_Rectpen, m_Rect);

            //绘制底部按钮
            if (m_IsCheLunFangFaStatus)
            {
                m_BottomButtons[0].SetButtonColor(255, 255, 0);
            }
            else
            {
                m_BottomButtons[0].SetButtonColor(192, 192, 192);
            }

            for (int i = 0; i < 2; i++)
            {
                m_BottomButtons[i].OnPaint(g);
            }

            if (BoolList[UIObj.InBoolList[22]])
            {
                g.FillRectangle(m_RedBrush, m_TrainRect[0]);
            }
            if (BoolList[UIObj.InBoolList[23]])
            {
                g.FillRectangle(m_RedBrush, m_TrainRect[3]);
            }
            if (BoolList[UIObj.InBoolList[24]])
            {
                g.FillRectangle(m_RedBrush, m_TrainRect[5]);
            }
            //制动力显示文本框
            {
                for (int i = 0; i < 8; i++)
                {
                    m_GText[i].OnDraw(g);
                    g.DrawString("kN", m_Strfont, m_BlackBrush, m_StrRect[i]);
                }
            }

        }
        protected override bool OnMouseDown(Point point)
        {
            return m_BottomButtons[1].OnMouseDown(point);
        }
        protected override bool OnMouseUp(Point point)
        {
            return m_BottomButtons[1].OnMouseUp(point);
        }
    }
}
