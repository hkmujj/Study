using System.Drawing;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Sanitary;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;


namespace Motor.HMI.CRH1A
{
    [GksDataType(DataType.isMMIObjectClass)]
    class GT_Sanitary : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("污物");//菜单标题
        public Rectangle Recposition = new Rectangle(0, 130, 800, 100);
        public Rectangle[] TrainRect = new Rectangle[8];//表示各车厢的边框
        public Rectangle[] WsRect = new Rectangle[8];//污水箱位置
        public Rectangle[] NoRect = new Rectangle[8];//车编号显示位置
        public Rectangle[] JsRect = new Rectangle[8];//净水箱位置
        public Rectangle[] WcuRect = new Rectangle[8];//上侧厕所位置
        public Rectangle[] WcdRect = new Rectangle[8];//下车厕所位置
        public int Weight = 65;//箱子的宽度
        public int Height = 45;//箱子的高度

        public Rectangle PageFootRectangle { get { return m_Rect; } }
        Rectangle m_Rect;//页脚区域

        public SolidBrush Recbrush = new SolidBrush(Color.FromArgb(119, 136, 153));
        public Pen Rectpen = new Pen(Color.FromArgb(209, 226, 242), 3);
        public Pen TrainPen = new Pen(Color.FromArgb(177, 177, 177), 2);
        public Font Strfont = new Font("Arial", 11);
        public SolidBrush Whitebrush = new SolidBrush(Color.FromArgb(255, 255, 255));

        public Image[] Image = new Image[6];
        public bool[] Valueb = new bool[32];
        public float[] Valuef = new float[16];

        public string[] SrtWs = new string[6] { "<20%", "<40%", "<60%", "<80%", ">80%", "Full" };//污水信息显示
        public string[] SrtJs = new string[6] { "Empty", "<25%", "<50%", "<75%", ">75%", "100%" };//净水信息显示

        private SanitaryButtonMgr m_SanitaryButtonMgr;

        /// <summary>
        /// 选中的净水箱
        /// </summary>
        private int m_SelectedCleanBoxIdx;

        private Pen m_SelectedOutlinePen = new Pen(Color.Black, 2);

        public GT_Sanitary()
        {
            m_SanitaryButtonMgr = new SanitaryButtonMgr(this);
        }

        public override string GetInfo()
        {
            return "污物系统";
        }


        protected override void NavigateTo(ViewConfig to)
        {
            //if (nParaA == ParaADefine.SwitchFromOhter)
            {
                ReselectCleanBox(-1);
            }
        }

        private void ReselectCleanBox(int idx)
        {
            if (idx == m_SelectedCleanBoxIdx)
            {
                m_SelectedCleanBoxIdx = -1;
            }
            else
            {
                m_SelectedCleanBoxIdx = idx;
            }
        }

        public override bool Initialize()
        {
            //3
            InitData();

            InitButton();

            //////////////////加 载 图 片
            if (UIObj.ParaList.Count >= 6)
                for (int i = 0; i < 6; i++)
                {
                    Image[i] = System.Drawing.Image.FromFile(RecPath + "//" + UIObj.ParaList[i]);
                }

            return true;
        }

        /// <summary>
        /// 初始化按键
        /// </summary>
        private void InitButton()
        {
            m_SanitaryButtonMgr.Init();
            m_SanitaryButtonMgr.ClearAllDownHandler +=
                (sender, args) =>
                    OnPost(CmdType.SetBoolValue,
                        UIObj.OutBoolList[SanitaryOutBoolIdxAdpt.GetClearAllIdx(m_SelectedCleanBoxIdx)], 1, 0);

            m_SanitaryButtonMgr.ClearSigleDownHandler +=
                (sender, args) =>
                    OnPost(CmdType.SetBoolValue,
                        UIObj.OutBoolList[SanitaryOutBoolIdxAdpt.GetClearSingleIdx(m_SelectedCleanBoxIdx)], 1, 0);

            m_SanitaryButtonMgr.ClearAllUpHandler += (sender, args) => OnPost(CmdType.SetBoolValue,
                        UIObj.OutBoolList[SanitaryOutBoolIdxAdpt.GetClearAllIdx(m_SelectedCleanBoxIdx)], 0, 0);
            m_SanitaryButtonMgr.ClearSigleUpHandler += (sender, args) => OnPost(CmdType.SetBoolValue,
                    UIObj.OutBoolList[SanitaryOutBoolIdxAdpt.GetClearSingleIdx(m_SelectedCleanBoxIdx)], 0, 0);
        }

        protected override bool OnMouseDown(Point point)
        {
            for (int i = 0; i < JsRect.Length; i++)
            {
                if (JsRect[i].Contains(point))
                {
                    ReselectCleanBox(i);
                    return true;
                }
            }
            m_SanitaryButtonMgr.OnButtonDown(point.X, point.Y);
            return true;
        }

        protected override bool OnMouseUp(Point point)
        {
            m_SanitaryButtonMgr.OnButtonUp(point.X, point.Y);
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            ReFreshData();
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 32)
            {
                for (int i = 0; i < 32; i++)
                {
                    Valueb[i] = BoolList[UIObj.InBoolList[i]];
                }
            }
            if (UIObj.InFloatList.Count >= 16)
            {
                for (int i = 0; i < 16; i++)
                {
                    Valuef[i] = FloatList[UIObj.InFloatList[i]];
                }
            }

        }

        public void ReFreshData()
        {
            for (int i = 0; i < 16; i++)
            {

            }

        }
        public void InitData()
        {

            #region::::::::::::;;;;;;;;各 车 边 框 位 置 初 始 化;;;;;;;;;;;;;;;;;;;;;;;;;;;

            for (int i = 0; i < 8; i++)
            {
                TrainRect[i] = new Rectangle(Recposition.X + i * 90 + 40, Recposition.Y, 85, 234);
            }
            #endregion
            #region::::::::::::::::::::污 水 箱 的 位 置 设 定:::::::::::::::::::::::::::::
            for (int i = 0; i < 8; i++)
            {
                WsRect[i] = new Rectangle(Recposition.X + i * 90 + 50, Recposition.Y + 30, Weight, Height);

            }
            #endregion

            #region :::::::::::::::::::::::净 水 箱 的 位 置 设 定;;;;;;;;;;;;;;;;;;;;;;;;;;
            for (int i = 0; i < 8; i++)
            {
                JsRect[i] = new Rectangle(Recposition.X + i * 90 + 50, Recposition.Y + 90, Weight, Height);
            }
            #endregion


            #region :;;;;:::::::::::::::: 厕 所 位 置 设 定;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
            for (int i = 0; i < 8; i++)
            {
                WcuRect[i] = new Rectangle(Recposition.X + i * 90 + 60, Recposition.Y + 145, 40, 40);//上8个
                WcdRect[i] = new Rectangle(Recposition.X + i * 90 + 60, Recposition.Y + 190, 40, 40);//上8个
            }

            #endregion
            #region :::::::::::::::::::车编号显示的位置设置;;;;;;;;;;;;;;;;;;;
            for (int i = 0; i < 8; i++)
            {
                NoRect[i] = new Rectangle(Recposition.X + i * 90 + 65, Recposition.Y + 10, 30, 25);
            }
            #endregion

            #region ::::::::::::::::::::页 脚 信 息 区 域 位 置::::::::::::::::::::::::::::::::
            m_Rect = new Rectangle(Recposition.X, Recposition.Y + 240, 790, 80);

            #endregion
        }
        public void DrawOn(Graphics g)
        {

            //绘制菜单标题
            Title.OnDraw(g);

            //绘 制 车 箱 边 框
            for (int i = 0; i < 8; i++)
            {
                g.DrawRectangle(TrainPen, TrainRect[i]);
            }

            //绘制编号
            for (int i = 0; i < 8; i++)
            {
                if (i < 7)
                {
                    g.DrawString("0" + (i + 1).ToString(), new Font("Arial", 12), new SolidBrush(Color.White), NoRect[i]);
                }
                else
                {
                    g.DrawString("00", new Font("Arial", 12), new SolidBrush(Color.White), NoRect[i]);
                }

            }
            //绘制污水箱
            for (int i = 0; i < 8; i++)
            {
                g.DrawImage(Image[0], WsRect[i]);
                g.DrawRectangle(TrainPen, WsRect[i]);
            }
            //显示污水箱水位信息
            for (int i = 0; i < 8; i++)
            {
                if (Valuef[i] < 20)
                {
                    g.DrawString(SrtWs[0], Strfont, Whitebrush, WsRect[i].X + 12, WsRect[i].Y + 20);
                }
                else if (Valuef[i] >= 20 && Valuef[i] < 40)
                {
                    g.DrawString(SrtWs[1], Strfont, Whitebrush, WsRect[i].X + 12, WsRect[i].Y + 20);
                }
                else if (Valuef[i] >= 40 && Valuef[i] < 60)
                {
                    g.DrawString(SrtWs[2], Strfont, Whitebrush, WsRect[i].X + 12, WsRect[i].Y + 20);
                }
                else if (Valuef[i] >= 60 && Valuef[i] < 80)
                {
                    g.DrawString(SrtWs[3], Strfont, Whitebrush, WsRect[i].X + 12, WsRect[i].Y + 20);
                }
                else if (Valuef[i] >= 80 && Valuef[i] < 100)
                {
                    g.DrawString(SrtWs[4], Strfont, Whitebrush, WsRect[i].X + 12, WsRect[i].Y + 20);
                }
                else
                {
                    g.DrawString(SrtWs[5], Strfont, Whitebrush, WsRect[i].X + 12, WsRect[i].Y + 20);
                }
            }
            //绘制净水箱
            for (int i = 0; i < 8; i++)
            {
                g.DrawImage(Image[1], JsRect[i]);
                g.DrawRectangle(TrainPen, JsRect[i]);
            }

            //显示净水箱信息
            for (int i = 0; i < 8; i++)
            {
                if (Valuef[i + 8] <= 0)
                {
                    g.DrawString(SrtJs[0], Strfont, Whitebrush, JsRect[i].X + 12, JsRect[i].Y + 20);
                }
                else if (Valuef[i + 8] >= 0 && Valuef[i + 8] < 25)
                {
                    g.DrawString(SrtJs[1], Strfont, Whitebrush, JsRect[i].X + 12, JsRect[i].Y + 20);
                }
                else if (Valuef[i + 8] >= 25 && Valuef[i + 8] < 50)
                {
                    g.DrawString(SrtJs[2], Strfont, Whitebrush, JsRect[i].X + 12, JsRect[i].Y + 20);
                }
                else if (Valuef[i + 8] >= 50 && Valuef[i + 8] < 75)
                {
                    g.DrawString(SrtJs[3], Strfont, Whitebrush, JsRect[i].X + 12, JsRect[i].Y + 20);
                }
                else if (Valuef[i + 8] >= 75 && Valuef[i + 8] < 100)
                {
                    g.DrawString(SrtJs[4], Strfont, Whitebrush, JsRect[i].X + 12, JsRect[i].Y + 20);
                }
                else
                {
                    g.DrawString(SrtJs[5], Strfont, Whitebrush, JsRect[i].X + 12, JsRect[i].Y + 20);
                }
            }


            //绘 制 厕 所

            for (int i = 0; i < 8; i++)
            {

                //上边
                if (!Valueb[i] && !Valueb[i + 8])//没人 没故障
                {
                    g.DrawImage(Image[2], WcuRect[i]);
                }
                if (!Valueb[i] && Valueb[i + 8])//没人 有故障
                {
                    g.DrawImage(Image[3], WcuRect[i]);
                }
                if (Valueb[i] && !Valueb[i + 8])//有人 没故障
                {
                    g.DrawImage(Image[4], WcuRect[i]);
                }
                if (Valueb[i] && Valueb[i + 8])//有人 有故障
                {
                    g.DrawImage(Image[5], WcuRect[i]);
                }

                //下边
                if (!Valueb[i + 16] && !Valueb[i + 24])//没人 没故障
                {
                    g.DrawImage(Image[2], WcdRect[i]);
                }
                if (!Valueb[i] && Valueb[i + 8])//没人 有故障
                {
                    g.DrawImage(Image[3], WcdRect[i]);
                }
                if (Valueb[i] && !Valueb[i + 8])//有人 没故障
                {
                    g.DrawImage(Image[4], WcdRect[i]);
                }
                if (Valueb[i] && Valueb[i + 8])//有人 有故障
                {
                    g.DrawImage(Image[5], WcdRect[i]);
                }

            }
            //页脚区域绘制

            g.FillRectangle(Recbrush, m_Rect);
            g.DrawRectangle(Rectpen, m_Rect);
            if (-1 != m_SelectedCleanBoxIdx)
            {
                // 绘制button
                m_SanitaryButtonMgr.OnDraw(g);
                g.DrawRectangle(m_SelectedOutlinePen, JsRect[m_SelectedCleanBoxIdx]);

            }

        }


    }
}
