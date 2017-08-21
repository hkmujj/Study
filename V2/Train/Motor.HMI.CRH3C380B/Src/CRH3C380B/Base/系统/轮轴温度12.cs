using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Model;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Base.系统.WheelShaft;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource.Images;

namespace Motor.HMI.CRH3C380B.Base.系统
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIWheelShaftT12 : CRH3C380BBase
    {
        private int MenuId
        {
            get { return m_MenuId; }
            set
            {
                if (m_MenuId == value)
                {
                    return;
                }

                m_MenuId = value;
                DMITitle.TitleName = MenuId == 1
                    ? (m_IsCrh380Bl ? "系统;轮轴温度 牵引单元1-2" : "系统;轮轴温度 动车组1")
                    : (m_IsCrh380Bl ? "系统;轮轴温度 牵引单元3-4" : "系统;轮轴温度 动车组2");
            }
        }

        private Image m_Img;
        private List<CommonInnerControlBase> m_CollectionOfMenu1;
        private List<CommonInnerControlBase> m_CollectionOfMenu2;
        private List<GDIRectText> m_Text;
        private int m_MenuId;

        public override string GetInfo()
        {
            return "轮轴温度-牵引单元";
        }

        private string[] m_Diff;
        private bool m_IsCrh380Bl;

        public override bool Initalize()
        {
            m_IsCrh380Bl = GlobalParam.Instance.ProjectType == ProjectType.CRH380BL;
            m_Diff = m_IsCrh380Bl ? new[] {"牵引单元\n1-2", "牵引单元\n3-4"} : new[] {"动车组1", "动车组2"};
            m_Img = CommonImages.che;
            var locton = new Point(30, 150);
            m_CollectionOfMenu1 = new List<CommonInnerControlBase>();
            m_CollectionOfMenu2 = new List<CommonInnerControlBase>();
            var tmp = new WheelShaftCar
            {
                Location = locton,
                IsTc = true,
                TcLength = 10,
                Width = 180,
                Interval = 2,
                Direction = Direction.Left,
                Height = 70,
            };
            m_CollectionOfMenu1.AddRange(tmp.Init());
            locton.Offset(10, 180);
            var tem1 = new WheelShaftCar
            {
                Location = locton,
                IsTc = !m_IsCrh380Bl,
                TcLength = 10,
                Width = 180,
                Interval = 2,
                Direction = Direction.Right,
                Height = 70,
            };
            m_CollectionOfMenu1.AddRange(tem1.Init());
            m_CollectionOfMenu1.AddRange(InitRectangle("动车组1 前车厢", tmp.Location, true, tmp.TcLength, tmp.Width,
                tmp.Height));
            m_CollectionOfMenu1.AddRange(InitRectangle("动车组1 后车厢", tem1.Location, false, tem1.TcLength, tem1.Width,
                tem1.Height));
            m_CollectionOfMenu1.AddRange(InitCarNum(true, true, tmp.Location, true, tmp.TcLength, tmp.Width, tmp.Height));
            m_CollectionOfMenu1.AddRange(InitCarNum(true, false, tem1.Location, false, tem1.TcLength, tem1.Width,
                tem1.Height));
            InitText(locton);
            locton = new Point(30, 150);
            var tmp2 = new WheelShaftCar
            {
                Location = locton,
                IsTc = !m_IsCrh380Bl,
                TcLength = 10,
                Width = 180,
                Interval = 2,
                Direction = Direction.Left,
                Height = 70,
            };
            m_CollectionOfMenu2.AddRange(tmp2.Init());
            locton.Offset(10, 180);
            var tem3 = new WheelShaftCar
            {
                Location = locton,
                IsTc = true,
                TcLength = 10,
                Width = 180,
                Interval = 2,
                Direction = Direction.Right,
                Height = 70,
            };
            m_CollectionOfMenu2.AddRange(tem3.Init());
            m_CollectionOfMenu2.AddRange(InitRectangle("动车组2 前车厢", tmp2.Location, true, tmp2.TcLength, tmp2.Width,
                tmp2.Height));
            m_CollectionOfMenu2.AddRange(InitRectangle("动车组2 后车厢", tem3.Location, false, tem3.TcLength, tem3.Width,
                tem3.Height));
            m_CollectionOfMenu2.AddRange(InitCarNum(false, true, tmp2.Location, true, tmp2.TcLength, tmp2.Width,
                tmp2.Height));
            m_CollectionOfMenu2.AddRange(InitCarNum(false, false, tem3.Location, false, tem3.TcLength, tem3.Width,
                tem3.Height));
            return true;

        }

        private void InitText(Point locton)
        {
            m_Text = new List<GDIRectText>
            {
                new GDIRectText
                {
                    Text = "轮轴温度",
                    NeedDarwOutline = false,
                    BackColorVisible = false,
                    OutLineRectangle = new Rectangle(locton.X, 50, 130, 30)
                },
                new GDIRectText
                {
                    Text = "牵引",
                    NeedDarwOutline = false,
                    BackColorVisible = false,
                    OutLineRectangle = new Rectangle(locton.X, 80, 130, 30)
                },
                new GDIRectText
                {
                    Text = "牵引",
                    NeedDarwOutline = false,
                    BackColorVisible = false,
                    OutLineRectangle = new Rectangle(locton.X, 260, 130, 30)
                }
            };
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                MenuId = 1;

                DMITitle.BtnContentList[2].BtnStr = m_IsCrh380Bl ? "牵引单元\n3-4" : DMITitle.MarshallMode ? "动车组2" : "";

                DMITitle.BtnContentList[9].BtnStr = "主页面";
                DMITitle.TitleName = "系统;轮轴温度 " + (m_IsCrh380Bl ? "牵引单元1-2" : "动车组2");
            }
        }

        public override void Paint(Graphics g)
        {
            GetValue();
            switch (MenuId)
            {
                case 1:
                    m_CollectionOfMenu1.ForEach(e => e.OnPaint(g));
                    break;
                case 2:
                    m_CollectionOfMenu2.ForEach(e => e.OnPaint(g));
                    break;
            }

            m_Text.ForEach(e => e.OnDraw(g));
        }

        private void GetValue()
        {
            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 0: //C键
                        append_postCmd(CmdType.ChangePage, 120, 0, 0);
                        break;
                    case 6: //牵引单元1-2
                        if (DMITitle.BtnContentList[0].BtnStr != string.Empty)
                        {
                            MenuId = 1;
                            DMITitle.BtnContentList[2].BtnStr = m_Diff[1];
                            DMITitle.BtnContentList[0].BtnStr = string.Empty;
                        }
                        break;
                    case 8: //牵引单元1-3
                        if (DMITitle.BtnContentList[2].BtnStr != string.Empty)
                        {
                            MenuId = 2;
                            DMITitle.BtnContentList[0].BtnStr = m_Diff[0];
                            DMITitle.BtnContentList[2].BtnStr = string.Empty;
                        }
                        break;
                    case 15: //主菜单
                        append_postCmd(CmdType.ChangePage, 11, 0, 0);
                        break;
                }
            }
        }

        private IEnumerable<CommonInnerControlBase> InitRectangle(string str, Point location, bool isLeftTc,
            int tcLength, int widht, int height)
        {
            var lst = new List<CommonInnerControlBase>();
            var size = new Size(widht/5 - 2, 25);
            var imgSize = new Size(widht/5, 25);
            var tmpLoction = location;
            tmpLoction.X = tmpLoction.X + (isLeftTc ? tcLength : 0);
            var topY = location.Y - size.Height - 2;
            var downY = location.Y + height + 2;
            var imageLocation = location;
            imageLocation.X = location.X + (isLeftTc ? tcLength : 0) + (widht - imgSize.Width)/2;
            imageLocation.Y = location.Y + (height - imgSize.Height)/2;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    lst.Add(new GDIRectText
                    {
                        Text = "",
                        TextColor = Color.Black,
                        Tag = string.Format("{0} {3} {1} {2}", str, j + 1, (i >= 2 ? "左" : "右"), i + 1),
                        NeedDarwOutline = true,
                        BackColorVisible = true,
                        BkColor = Color.White,
                        OutLineRectangle = new Rectangle(tmpLoction.X, topY, size.Width, size.Height),
                        RefreshAction = o => RefreshItemValue((GDIRectText) o),
                        TextFormat = FontsItems.TheAlignment(FontRelated.居中),
                        DrawFont = FontsItems.FontC10
                    });
                    lst.Add(new GDIRectText
                    {
                        Text = "",
                        TextColor = Color.Black,
                        Tag = string.Format("{0} {3} {1} {2}", str, j + 1, (i >= 2 ? "右" : "左"), (i + 1)),
                        NeedDarwOutline = true,
                        BackColorVisible = true,
                        BkColor = Color.White,
                        OutLineRectangle = new Rectangle(tmpLoction.X, downY, size.Width, size.Height),
                        RefreshAction = o => RefreshItemValue((GDIRectText) o),
                        TextFormat = FontsItems.TheAlignment(FontRelated.居中),
                        DrawFont = FontsItems.FontC10
                    });
                    tmpLoction.X = tmpLoction.X + size.Width + 2 + (j == 1 ? widht/5 : 0);
                }

                lst.Add(new GDIRectText
                {
                    NeedDarwOutline = false,
                    BKImage = m_Img,
                    OutLineRectangle = new Rectangle(imageLocation, imgSize)
                });
                tmpLoction.X = (location.X + (isLeftTc ? tcLength : 0)) + (widht + 2)*(i + 1);
                imageLocation.X = (location.X + (isLeftTc ? tcLength : 0)) + (widht + 2)*(i + 1) +
                                  (widht - imgSize.Width)/2;
            }

            return lst;
        }

        private void RefreshItemValue(GDIRectText item)
        {
            var name = item.Tag as string;
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            var values = GetInFloatValue(name);
            item.Text = values.ToString("0");
        }

        /// <summary>
        /// 车辆编号
        /// </summary>
        /// <param name="isTrainOne">true 动车组1</param>
        /// <param name="isUp">true 上面四辆</param>
        /// <param name="location">坐标</param>
        /// <param name="isLeftTc"></param>
        /// <param name="tcLength"></param>
        /// <param name="widht"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private static IEnumerable<CommonInnerControlBase> InitCarNum(bool isTrainOne, bool isUp, Point location,
            bool isLeftTc,
            int tcLength, int widht, int height)
        {
            var lst = new List<CommonInnerControlBase>();
            var imgSize = new Size(widht/5, 25);
            var imageLocation = location;
            imageLocation.X = location.X + (isLeftTc ? tcLength : 0) + (widht - imgSize.Width)/2;
            imageLocation.Y = location.Y + (height - imgSize.Height)/2 + imgSize.Height;
            for (int i = 0; i < 4; i++)
            {
                lst.Add(new GDIRectText
                {
                    Text = GetCarNumText(isTrainOne, isUp, i),
                    OutLineRectangle = new Rectangle(imageLocation, imgSize),
                    NeedDarwOutline = false,
                    BackColorVisible = false,
                    TextFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    }
                });
                imageLocation.X = (location.X + (isLeftTc ? tcLength : 0)) + (widht + 2)*(i + 1) +
                                  (widht - imgSize.Width)/2;
            }

            return lst;
        }

        /// <summary>
        /// 获取车厢编号
        /// </summary>
        /// <param name="isTrainOne"></param>
        /// <param name="isUp"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static string GetCarNumText(bool isTrainOne, bool isUp, int i)
        {
            if (GlobalParam.Instance.ProjectType == ProjectType.CRH380BL)
            {
                if (isUp)
                {
                    return isTrainOne ? (i + 1).ToString("00") : (i + 9).ToString("00");
                }

                return isTrainOne ? (i + 5).ToString("00") : (i + 13).ToString("00");
            }

            return GlobalParam.Instance.ProjectType == ProjectType.CRH3C
                ? (isTrainOne && isUp
                    ? (i + 1).ToString("0")
                    : (isTrainOne ? (i + 5).ToString("0") : isUp ? (i + 11).ToString("00") : (i + 15).ToString("00")))
                : (isTrainOne && isUp
                    ? (i + 11).ToString("00")
                    : (isTrainOne ? (i + 15).ToString("00") : isUp ? (i + 21).ToString("00") : (i + 25).ToString("00")));
        }
    }
}