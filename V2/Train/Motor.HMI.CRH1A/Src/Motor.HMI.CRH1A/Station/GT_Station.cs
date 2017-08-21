using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls.Button;
using CommonUtil.Controls.Roundness;
using CommonUtil.Util.Extension;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Global;
using Motor.HMI.CRH1A.Common.Train;
using Motor.HMI.CRH1A.Station.Car;
using Motor.HMI.CRH1A.Station.Door;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH1A.Station
{
    [GksDataType(DataType.isMMIObjectClass)]
    class GT_Station : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("车站");//菜单标题
        public Rectangle Recposition = new Rectangle(0, 140, 800, 100);
        public CRH1AButton[] GButton = new CRH1AButton[3];
        public Rectangle[] Carroom = new Rectangle[6];//表示六节车厢

        public Pen Pen = new Pen(Color.FromArgb(100, 100, 100), 2);//信息栏边框
        public Pen Blackpen = new Pen(Color.FromArgb(85, 85, 85), 2);

        public Point[] Mc1 = new Point[6];//左司机室的坐标
        public Point[] Mc2 = new Point[6];//右司机室的坐标

        Rectangle m_InfoRect;
        public Rectangle[] StrRect = new Rectangle[8];
        Image m_Image;

        private StationDoors m_StationDoors;

        private readonly List<IDoorViewCar> m_DoorViewCars;

        public StationResource Resource { private set; get; }

        /// <summary>
        /// 司机
        /// </summary>
        private List<GDIRoundness> m_Drivers;

        public GT_Station()
        {
            m_DoorViewCars = new List<IDoorViewCar>();
        }

        public override string GetInfo()
        {
            return "车站";
        }

        public override bool Initialize()
        {
            Resource = new StationResource(this);

            //3
            InitData();
            if (UIObj.ParaList.Count > 0)
                m_Image = Image.FromFile(RecPath + "//" + UIObj.ParaList[0]);

            return true;
        }

        public override void Dispose()
        {
            m_DoorViewCars.ForEach(e => e.Dispose());
        }

        public override void paint(Graphics g)
        {
            DrawOn(g);
        }

        public void InitData()
        {   ///////////底部按钮初始化
            GButton[0] = new CRH1AButton();
            GButton[0].SetButtonRect(Recposition.X + 454, Recposition.Y + 375, 80, 50);
            GButton[0].SetButtonColor(192, 192, 192);
            GButton[0].SetButtonText("列车状态");

            GButton[1] = new CRH1AButton();
            GButton[1].SetButtonRect(Recposition.X + 624, Recposition.Y + 375, 80, 50);
            GButton[1].SetButtonColor(192, 192, 192);
            GButton[1].SetButtonText("主菜单");
            GButton[2] = new CRH1AButton();
            GButton[2].SetButtonColor(192, 192, 192);
            GButton[2].SetButtonRect(Recposition.X + 710, Recposition.Y + 375, 80, 50);
            GButton[2].SetButtonText("运行");

            #region::::::::::::;;;;;;;;司 机 室 初 始 化;;;;;;;;;;;;;;;;;;;;;;;;;;;
            //左司机室
            Mc1[0] = new Point(Recposition.X + 70, Recposition.Y);
            Mc1[1] = new Point(Recposition.X + 150, Recposition.Y);
            Mc1[2] = new Point(Recposition.X + 150, Recposition.Y + 70);
            Mc1[3] = new Point(Recposition.X + 70, Recposition.Y + 70);
            Mc1[4] = new Point(Recposition.X + 45, Recposition.Y + 45);
            Mc1[5] = new Point(Recposition.X + 45, Recposition.Y + 25);
            //右司机室
            Mc2[0] = new Point(Recposition.X + 660, Recposition.Y);
            Mc2[1] = new Point(Recposition.X + 740, Recposition.Y);
            Mc2[2] = new Point(Recposition.X + 765, Recposition.Y + 25);
            Mc2[3] = new Point(Recposition.X + 765, Recposition.Y + 45);
            Mc2[4] = new Point(Recposition.X + 740, Recposition.Y + 70);
            Mc2[5] = new Point(Recposition.X + 660, Recposition.Y + 70);

            m_DoorViewCars.Add(new DriverCar(0, CarStyle.Left, DriverLocation.Left, this) { Location = Recposition.Location.Translate(70, 0) });
            m_DoorViewCars.Add(new DriverCar(7, CarStyle.Right, DriverLocation.Right, this) { Location = Recposition.Location.Translate(660, 0) });

            m_Drivers = new List<GDIRoundness>()
                        {
                            new GDIRoundness()
                            {
                                Center = new Point(70, 173),
                                R = 5,
                                ContentColor = Color.Black,
                                NeedDrawContent = true,
                                Visible = false,
                            },
                            new GDIRoundness()
                            {
                                Center = new Point(740, 173),
                                R = 5,
                                ContentColor = Color.Black,
                                NeedDrawContent = true,
                                Visible = false,
                            }
                        };
            GlobalEvent.Instance.DriverLocationChanged += (sender, args) =>
            {
                m_Drivers[0].Visible = args.Arg.NewValue == DriverLocation.Left;
                m_Drivers[1].Visible = args.Arg.NewValue == DriverLocation.Right;
            };


            #endregion

            #region ::::::::::::::::::车 厢 初 始 化::::::::::::::::::::::::::::::::::
            for (int i = 0; i < 6; i++)
            {
                Carroom[i] = new Rectangle(Recposition.X + 84 * i + 154, Recposition.Y, 80, 70);
                m_DoorViewCars.Add(new PassengerCar(i + 1, CarStyle.Left,this) { Location = Recposition.Location.Translate(84 * i + 154, 0) });
            }
            #endregion

            #region::::::::::::::::::::车 门 初 始 化:::::::::::::::::::::::::::::

            m_StationDoors = new StationDoors(this, Recposition.Location, UIObj.InBoolList.Take(GlobalParam.CarCount * 5 * 2).ToArray());

            #endregion

            #region :::::::::::::::::::车编号显示的位置设置;;;;;;;;;;;;;;;;;;;
            for (int i = 0; i < 8; i++)
            {
                StrRect[i] = new Rectangle(Recposition.X + i * 84 + 110, Recposition.Y - 25, 30, 25);
            }
            #endregion


            //信息栏位置设定
            m_InfoRect = new Rectangle(Recposition.X, Recposition.Y + 200, 790, 110);

        }
        public void DrawOn(Graphics g)
        {
            m_DoorViewCars.ForEach(e => e.OnDraw(g));

            

            // 绘制菜单标题
            Title.OnDraw(g);
            //绘制底部按钮
            for (int i = 0; i < 3; i++)
            {
                GButton[i].OnDraw(g);
            }

            //绘制提示信息
            g.DrawImage(m_Image, m_InfoRect);

            return;

            //绘制左司机室
            g.DrawPolygon(Blackpen, Mc1);

            //绘制右司机室
            g.DrawPolygon(Blackpen, Mc2);


            // 司机室圆点
            m_Drivers.ForEach(e => e.OnDraw(g));

            //绘制车厢
            for (int i = 0; i < 6; i++)
            {
                g.DrawRectangle(Blackpen, Carroom[i]);
            }

            m_StationDoors.OnDraw(g);


           
            //绘制编号
            for (int i = 0; i < 8; i++)
            {
                if (i < 7)
                {
                    g.DrawString("0" + (i + 1).ToString(), new Font("Arial", 12), new SolidBrush(Color.White), StrRect[i]);
                }
                else
                {
                    g.DrawString("00", new Font("Arial", 12), new SolidBrush(Color.White), StrRect[i]);
                }
            }
        }

        protected override bool OnMouseDown(Point point)
        {
            OnButtonDown(point.X, point.Y);
            return true;
        }
        protected override bool OnMouseUp(Point point)
        {
            OnButtonUp(point.X, point.Y);
            return true;
        }

        public void OnButtonDown(int x, int y)
        {
            //  按 钮 响 应 事 件
            for (int i = 0; i < 3; i++)
            {
                if (GButton[i].Contains(x, y))
                {

                    GButton[i].OnButtonDown();

                }

            }

        }

        public void OnButtonUp(int x, int y)
        {
            for (int i = 0; i < 3; i++)
            {
                if (GButton[i].Contains(x, y))
                {
                    switch (i)
                    {
                        case 0:
                            OnPost(CmdType.ChangePage, 6, 0, 0);
                            break;
                        case 1:
                            OnPost(CmdType.ChangePage, 1, 0, 0);
                            break;
                        case 2:
                            OnPost(CmdType.ChangePage, 3, 0, 0);
                            break;
                    }
                    GButton[i].OnButtonUp();

                }
            }

        }

    }

}
