using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Roller;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls;


// ReSharper disable once CheckNamespace
namespace Motor.HMI.CRH1A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class GT_HotBoxStatus : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("轴报");//菜单标题 
        public Rectangle Recposition = new Rectangle(0, 170, 800, 100);
        public Rectangle[] TrainRect = new Rectangle[6];//表示各车厢的边框
        public Rectangle[] NoRect = new Rectangle[8];//车编号显示位置  

        public int Weight = 65;//箱子的宽度
        public int Height = 45;//箱子的高度
        Rectangle m_Rect;//页脚区域

        public GDIRectText[] GText = new GDIRectText[8];
        public Pen TrainPen = new Pen(Color.FromArgb(85, 85, 85), 2);
        public Pen LinePen = new Pen(Color.FromArgb(70, 71, 71), 2);

        public Font Strfont = new Font("Arial", 11);
        public SolidBrush Whitebrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public Point[] Mc1 = new Point[6];//左司机室的坐标
        public Point[] Mc2 = new Point[6];//右司机室的坐标

        private PolygonRegion m_Mc1;
        private PolygonRegion m_Mc2;

        /// <summary>
        /// 选中时的框
        /// </summary>
        private Rectangle m_SelectedRectangle;
        private readonly Rectangle[] m_AllBoundRectangle = new Rectangle[8];

        private const int UnselectedIndex = -1;

        /// <summary>
        /// 温度
        /// </summary>
        public float[] Valuef = new float[9];
        /// <summary>
        /// 轴温状态 
        /// </summary>
        private readonly TemperatureState[] m_TemperatureStates = new TemperatureState[8];
        public Image Image;

        private int m_SelectTrainIdx;
        private int SelectTrainIdx
        {
            get { return m_SelectTrainIdx; }
            set
            {
                m_SelectTrainIdx = value;
                
                RefreshSelectedState(value);
            }
        }

        private string[] m_TrainIDs = { "01", "02", "03", "04", "05", "06", "07", "00" };

        /// <summary>
        /// 火车轴
        /// </summary>
        private TrainRoller m_TrainRoller;

        private HotBoxControllerRegion m_BoxControllerRegion;

        public GT_HotBoxStatus()
        {
            Resource = new HotBoxResource(this);
        }

        public HotBoxResource Resource { private set; get; }

        public override string GetInfo()
        {
            return "轴温箱系统状态";
        }


        protected override void NavigateTo(ViewConfig to)
        {
            //if (nParaA == ParaADefine.SwitchFromOhter)
            {
                ReselectIndex(-1);
            }
        }


        public override bool Initialize()
        {
            //3
            InitData();

            m_BoxControllerRegion = new HotBoxControllerRegion(this, new Rectangle(0, 500, 800, 100));

            if (UIObj.ParaList.Count > 0)
            {
                Image = Image.FromFile(RecPath + "//" + UIObj.ParaList[0]);
            }

            InitAllRectangle();

            return true;
        }

        private void InitAllRectangle()
        {
            const int INTERVAL = 3;
            m_AllBoundRectangle[0] = GetPolygonRegionBound(m_Mc1, INTERVAL);

            for (int i = 0; i < 6; i++)
            {
                var curRect = TrainRect[i];
                m_AllBoundRectangle[i + 1] = new Rectangle(curRect.X - INTERVAL, curRect.Y - INTERVAL,
                    curRect.Width + INTERVAL * 2, curRect.Height + INTERVAL * 2);
            }

            m_AllBoundRectangle[7] = GetPolygonRegionBound(m_Mc2, INTERVAL);
        }

        private Rectangle GetPolygonRegionBound(PolygonRegion region, int interval)
        {
            var minx = region.Points.Min(m => m.X);
            var miny = region.Points.Min(m => m.Y);
            var maxx = region.Points.Max(m => m.X);
            var maxy = region.Points.Max(m => m.Y);

            return new Rectangle(minx - interval, miny - interval, interval * 2 + maxx - minx, interval * 2 + maxy - miny);
        }


        private void RefreshSelectedState(int bodyId)
        {
            if (bodyId == UnselectedIndex)
            {
                m_TrainRoller.Visible = false;
                m_SelectedRectangle = Rectangle.Empty;
                return;
            }

            m_TrainRoller.TrainBodyId = m_TrainIDs[m_SelectTrainIdx];
            var tmp = new float[8];
            for (int i = 0; i < 8; i++)
            {
                tmp[i] = Valuef[bodyId];
            }
            m_TrainRoller.Tempearaturs = tmp;

            m_SelectedRectangle = m_AllBoundRectangle[bodyId];
            m_TrainRoller.Visible = true;
        }


        public override void paint(Graphics g)
        {
            GetValue();
            ReFreshData();
            DrawOn(g);

            m_BoxControllerRegion.OnDraw(g);
        }

        protected override bool OnMouseDown(Point point)
        {
            return GetSelectedIdx(point) || m_BoxControllerRegion.OnMouseDown(point);

        }

        private bool GetSelectedIdx(Point nPoint)
        {
            for (int i = 0; i < TrainRect.Length; i++)
            {
                if (TrainRect[i].Contains(nPoint))
                {
                    ReselectIndex(1 + i);
                    return true;
                }
            }
            if (m_Mc1.Contains(nPoint))
            {
                ReselectIndex(0);
                return true;
            }
            if (m_Mc2.Contains(nPoint))
            {
                ReselectIndex(7);
                return true;
            }

            return false;
        }

        private void ReselectIndex(int index)
        {
            if (SelectTrainIdx == index)
            {
                SelectTrainIdx = -1;
            }
            else
            {
                SelectTrainIdx = index;
            }
        }

        protected override bool OnMouseUp(Point point)
        {
            return m_BoxControllerRegion.OnMouseUp(point);
        }

        public void GetValue()
        {
            if (UIObj.InFloatList.Count >= 9)
            {
                for (int i = 0; i < 9; i++)
                {
                    Valuef[i] = FloatList[UIObj.InFloatList[i]];
                }
            }

        }

        public void ReFreshData()
        {
            for (int i = 0; i < 8; i++)
            {
                if (Valuef[8] > 30)//环境温度高于30
                {
                    if (Valuef[i] > 90)
                    {
                        GText[i].SetBkColor(255, 0, 0);//轴温高于90 显示为红色
                        m_TemperatureStates[i] = TemperatureState.High;
                    }
                    else
                    {
                        GText[i].SetBkColor(192, 192, 192);//正常显示
                        m_TemperatureStates[i] = TemperatureState.Normal;
                    }
                }
                else if (Valuef[8] < -10)
                {
                    if (Valuef[i] > 50)
                    {
                        GText[i].SetBkColor(255, 0, 0);//轴温高于50 显示为红色
                        m_TemperatureStates[i] = TemperatureState.High;
                    }
                    else
                    {
                        GText[i].SetBkColor(192, 192, 192);//正常显示
                        m_TemperatureStates[i] = TemperatureState.Normal;
                    }
                }
                else
                {
                    if (Valuef[i] - Valuef[8] > 60)
                    {
                        GText[i].SetBkColor(255, 0, 0);//轴温比常温高60 显示为红色
                        m_TemperatureStates[i] = TemperatureState.High;
                    }
                    else
                    {
                        GText[i].SetBkColor(192, 192, 192);//正常显示
                        m_TemperatureStates[i] = TemperatureState.Normal;
                    }
                }
                GText[i].SetText(Valuef[i].ToString());

                RefreshSelectedState(SelectTrainIdx);
            }

        }
        public void InitData()
        {

            #region::::::::::::;;;;;;;;各 车 边 框 位 置 初 始 化;;;;;;;;;;;;;;;;;;;;;;;;;;;
            //左司机室
            Mc1[0] = new Point(Recposition.X + 60, Recposition.Y);
            Mc1[1] = new Point(Recposition.X + 120, Recposition.Y);
            Mc1[2] = new Point(Recposition.X + 120, Recposition.Y + 60);
            Mc1[3] = new Point(Recposition.X + 60, Recposition.Y + 60);
            Mc1[4] = new Point(Recposition.X + 35, Recposition.Y + 40);
            Mc1[5] = new Point(Recposition.X + 35, Recposition.Y + 20);
            m_Mc1 = new PolygonRegion(Mc1);
            //右司机室
            Mc2[0] = new Point(Recposition.X + 670, Recposition.Y);
            Mc2[1] = new Point(Recposition.X + 730, Recposition.Y);
            Mc2[2] = new Point(Recposition.X + 755, Recposition.Y + 20);
            Mc2[3] = new Point(Recposition.X + 755, Recposition.Y + 40);
            Mc2[4] = new Point(Recposition.X + 730, Recposition.Y + 60);
            Mc2[5] = new Point(Recposition.X + 670, Recposition.Y + 60);
            m_Mc2 = new PolygonRegion(Mc2);
            for (int i = 0; i < 6; i++)
            {
                TrainRect[i] = new Rectangle(Recposition.X + i * 90 + 125, Recposition.Y, 85, 60);
            }
            #endregion

            #region 车轴

            var trainSize = new Size(140, 90);

            m_TrainRoller = new TrainRoller(new Rectangle(Recposition.X + Recposition.Width / 2 - trainSize.Width / 2, Recposition.Y + 100, trainSize.Width, trainSize.Height), this);

            #endregion


            #region :::::::::::::::::::车编号显示的位置设置;;;;;;;;;;;;;;;;;;;
            for (int i = 0; i < 8; i++)
            {
                NoRect[i] = new Rectangle(Recposition.X + i * 90 + 65, Recposition.Y + 5, 30, 25);
            }
            #endregion

            #region :::::::::::::::::::::::::::::温度显示文本框初始化:::::::::::::::::::::::::

            for (int i = 0; i < 8; i++)
            {
                GText[i] = new GDIRectText();
                GText[i].SetTextColor(0, 0, 0);
                GText[i].SetTextStyle(10, FormatStyle.Center, true, "Arial");
                GText[i].SetTextRect(Recposition.X + 60 + 90 * i, Recposition.Y + 30, 35, 20);
                GText[i].SetLinePen(43, 43, 43, 2);
                GText[i].Isdrawrectfrm = true;
            }
            #endregion

            #region ::::::::::::::::::::页 脚 信 息 区 域 位 置::::::::::::::::::::::::::::::::



            m_Rect = new Rectangle(Recposition.X, Recposition.Y + 210, 790, 70);

            #endregion

        }
        public void DrawOn(Graphics g)
        {
            //绘制菜单标题
            Title.OnDraw(g);

            //绘 制 车 箱 边 框
            g.DrawPolygon(TrainPen, Mc1);
            g.DrawPolygon(TrainPen, Mc2);
            for (int i = 0; i < 6; i++)
            {
                g.DrawRectangle(TrainPen, TrainRect[i]);
            }

            if (m_SelectedRectangle != Rectangle.Empty)
            {
                g.DrawRectangle(CommonResouce.BlackPen, m_SelectedRectangle);
            }

            // 绘制车轴
            m_TrainRoller.OnDraw(g);

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

            //页脚区域绘制
            g.DrawImage(Image, m_Rect);
            //温度显示
            for (int i = 0; i < 8; i++)
            {
                GText[i].OnDraw(g);
                g.DrawLine(LinePen, GText[i].OutLineRectangle.X, GText[i].OutLineRectangle.Y + GText[i].OutLineRectangle.Height, GText[i].OutLineRectangle.X
                + GText[i].OutLineRectangle.Width, GText[i].OutLineRectangle.Y + GText[i].OutLineRectangle.Height);
                g.DrawLine(LinePen, GText[i].OutLineRectangle.X + GText[i].OutLineRectangle.Width, GText[i].OutLineRectangle.Y, GText[i].OutLineRectangle.X
                + GText[i].OutLineRectangle.Width, GText[i].OutLineRectangle.Y + GText[i].OutLineRectangle.Height);
            }
        }

    }
}
