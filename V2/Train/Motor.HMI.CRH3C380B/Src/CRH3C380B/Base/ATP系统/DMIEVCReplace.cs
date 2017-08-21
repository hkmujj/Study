using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource.Images;
using Coordinate = Motor.HMI.CRH3C380B.Base.底层共用.Coordinate;

namespace Motor.HMI.CRH3C380B.Base.ATP系统
{

    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIEVCReplace : CRH3C380BBase
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::

        //2
        public override string GetInfo()
        {
            return "EVC更换显示";
        }

        //6
        public override bool Initalize()
        {
            InitData();
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            m_Shoudiangongindex = DMITitle.MarshallMode ? new[] {924, 927, 930, 933} : new[] {924, 927};

            if (nParaA != SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                return;
            }

            for (var i = 0; i < 10; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
            }
        }

        public override void Paint(Graphics g)
        {
            GetValue();
            Draw(g);
        }


        private void Draw(Graphics e)
        {
            PaintRectangles(e);
            PaintImg(e);
            m_CommonInnerControlBases.ForEach(g => g.OnDraw(e));
        }

        private void PaintImg(Graphics e)
        {
            e.DrawImage(
                m_Shoudiangongindex.Any(s => BoolList[s])
                    ? CommonImages.升弓_0
                    : CommonImages.降弓_0, m_ImgRec[0]);
            if (true) //轴
            {
                e.DrawImage(CommonImages.zhou, m_ImgRec[1]);
            }
            if (BoolList[611]) //门
            {
                e.DrawImage(CommonImages.doubleDoor, m_ImgRec[2]);
            }
            else
            {
                e.DrawImage(CommonImages.doubleDoor, m_ImgRec[2]);
            }
            if (BoolList[613]) //司机室
            {

            }
            else
            {
                e.DrawImage(CommonImages.sijishi, m_ImgRec[3]);
            }
        }

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::

        private void GetValue()
        {
            if (DMIButton.BtnUpList.Count == 0)
            {
                return;
            }

            switch (DMIButton.BtnUpList[0])
            {
                case 0: //C键
                    append_postCmd(CmdType.ChangePage, 140, 0, 0);
                    break;
                case 15: //主页面
                    append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    break;
            }
        }

        private void PaintRectangles(Graphics e)
        {
            e.DrawString("ATP系统;EVC替换显示", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));
            e.DrawString("V", FontsItems.FontC24B,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_PrivateRectsList[0], FontsItems.TheAlignment(FontRelated.靠左下));
            e.DrawString("为", FontsItems.FontC12B,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_PrivateRectsList[2], FontsItems.TheAlignment(FontRelated.靠左下));
            e.FillRectangle(Brushes.White, m_PrivateRectsList[1]);
            e.DrawString("km/h", FontsItems.FontC16B,
                SolidBrushsItems.BlackBrush,
                m_PrivateRectsList[1], FontsItems.TheAlignment(FontRelated.靠右下));
            e.DrawString(FloatList[m_EvcDictionary[1]].ToString("0"), FontsItems.FontC24B,
                SolidBrushsItems.BlackBrush,
                m_PrivateRectsList[3], FontsItems.TheAlignment(FontRelated.靠右下));

        }

        #endregion

        private void InitData()
        {
            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIATPSystem, ref m_RectsList))
            {

            }
            const int x = 25;
            const int x1 = 770;
            const int x2 = 100;
            const int x3 = 170;
            const int x4 = x3 + 300;

            const int y = 310;
            const int y2 = 500;
            const int y3 = 360;
            const int y4 = 30;
            var point1 = new Point(x, y);
            var point2 = new Point(x1, y);

            var point3 = new Point(x2, y);
            var point4 = new Point(x2, y2);

            var point5 = new Point(x2, y3);
            var point6 = new Point(x1, y3);

            var point7 = new Point(x3, y4);
            var point8 = new Point(x3, y);

            var point9 = new Point(x4, y4);
            var point10 = new Point(x4, y3);
            var sizeRectanfle = new Size(140, 50);
            var sizeRectanfle2 = new Size(140, 40);
            var sizeRectanfle1 = new Size(92, 40);
            m_PrivateRectsList = new List<Rectangle>
            {
                new Rectangle(new Point(x3 + 80, y4 + 70), sizeRectanfle),
                new Rectangle(new Point(x3 + 80, y4 + 70 + sizeRectanfle.Height), sizeRectanfle2),
                new Rectangle(new Point(x3 + 105, y4 + 70), sizeRectanfle),
                new Rectangle(new Point(x3 + 80, y4 + 70 + sizeRectanfle.Height), sizeRectanfle1),
            };
            m_EvcDictionary = new Dictionary<int, int> {{1, GetInFloatIndex("EVC显示列车速度")}};
            m_CommonInnerControlBases = new List<CommonInnerControlBase>
            {
                new Line(point1, point2)
                {
                    Color = Color.White,
                    Tag = 1
                },
                new Line(point3, point4)
                {
                    Color = Color.White,
                    Tag = 2
                },
                new Line(point5, point6)
                {
                    Color = Color.White,
                    Tag = 3
                },
                new Line(point7, point8)
                {
                    Color = Color.White,
                    Tag = 4
                },
                new Line(point9, point10)
                {
                    Color = Color.White,
                    Tag = 4
                }
            };
            m_ImgRec = new List<Rectangle>();
            var size = new Size(50, 50);
            var point = new Point(470, 310);
            for (var i = 0; i < 4; i++)
            {
                m_ImgRec.Add(new Rectangle(point, size));
                point.Offset(size.Width, 0);
            }
        }

        #endregion

        private Dictionary<int, int> m_EvcDictionary;

        private List<Rectangle> m_PrivateRectsList;
        private List<RectangleF> m_RectsList;
        private List<Rectangle> m_ImgRec;

        private readonly string[] m_BtnStr =
        {
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "主页面"
        };

        private int[] m_Shoudiangongindex;
        private List<CommonInnerControlBase> m_CommonInnerControlBases;

    }
}