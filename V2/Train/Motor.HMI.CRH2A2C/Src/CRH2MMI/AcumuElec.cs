using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using CommonUtil.Controls;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Resource.Images;
using MMI.Facility.Interface.Attribute;

namespace CRH2MMI
{
    /// <summary>
    /// 累计电力信息
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class AcumuElec : CRH2BaseClass
    {
        public Color TextColor = Color.FromArgb(135, 135, 0);
        private List<CommonInnerControlBase> m_Collection;
        public static Pen linePen = new Pen(Color.White, 2f) { DashStyle = DashStyle.Custom, DashPattern = new float[] { 2, 2 } };
        public static StringFormat Right = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Far };
        private bool IsConfig;
        public static StringFormat Center = new StringFormat()
        {
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Center
        };
        public override void paint(Graphics g)
        {
            GetValue();
            OnDraw(g);
        }
        public void GetValue()
        {

        }
        public override bool Init()
        {
            m_Collection = new List<CommonInnerControlBase>();
            InitDate();

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == 2)
            {

                switch (nParaB)
                {
                    case 11:
                        InitDate();
                        break;
                }
            }
        }


        public override string GetInfo()
        {
            return "累计电力信息";
        }

        public void InitDate()
        {
            var config = GlobalInfo.CurrentCRH2Config.GrandTotalPowerConfig;
            IsConfig = config != null;
            if (config == null)
            {
                return;
            }
            var old = new Point(100, 175);
            var loc = new Point(100, 175);
            var length = config.Widths.Sum();
            var size = new Size(length, 375);
            var height = (float)size.Height / (float)config.RowNum;
            var titleLoc = new Point(loc.X, loc.Y - 50);
            m_Collection.Clear();
            m_Collection.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(old, size),
                BackColorVisible = false,
                NeedDarwOutline = true,
                OutLinePen = new Pen(Color.White, 2f)
            });
            m_Collection.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(old.X - 90, old.Y - 50, size.Width + 90, size.Height + 50),
                BackColorVisible = false,
                NeedDarwOutline = true,
                OutLinePen = new Pen(Color.White, 2f)
            });

            InitTitleText("累计开始时间\r\n(时)", config.Widths[0] + config.Widths[1], 0, titleLoc, 50);
            InitTitleText("累计行车距离\r\n(km)", config.Widths[2], config.Widths[0] + config.Widths[1], titleLoc, 50);
            InitTitleText("牵引电量\r\n(kwh)", config.Widths[3], config.Widths[0] + config.Widths[1] + config.Widths[2], titleLoc, 50);
            InitTitleText("再生电量\r\n(kwh)", config.Widths[4], config.Widths[0] + config.Widths[1] + config.Widths[2] + config.Widths[3], titleLoc, 50);
            InitTitleText("消耗电量\r\n(kwh)", config.Widths[5], config.Widths[0] + config.Widths[1] + config.Widths[2] + config.Widths[3] + config.Widths[4], titleLoc, 50);
            for (var i = 0; i < config.RowNum - 1; i++)
            {
                loc.Offset(0, (int)(height * (i + 1)));
                m_Collection.Add(new Line(loc, new Point(loc.X + length, loc.Y)) { LinePen = linePen });
                loc = old;
            }
            loc = old;
            for (var i = 0; i < config.Widths.Length - 1; i++)
            {
                loc.Offset(config.Widths[i], 0);
                m_Collection.Add(new Line(loc, new Point(loc.X, loc.Y + size.Height)) { LinePen = linePen });
            }
            loc = old;
            foreach (var car in config.GrandTotalPowerCars)
            {
                m_Collection.Add(new GDIRectText()
                {
                    Text = car.CarNum,
                    OutLineRectangle = new Rectangle(loc.X - 100, loc.Y + (int)(height * (car.Row)), 100, (int)(height)),
                    NeedDarwOutline = false,
                    BackColorVisible = false,
                    TextFormat = Right,
                });
                foreach (var carDetail in car.CarDetails)
                {
                    InitRectText(carDetail.TotlaStartDataTime, config.Widths[0], 0, loc, height, car.Row);
                    InitRectText(carDetail.TotlaStartTime, config.Widths[1], config.Widths[0], loc, height, car.Row);
                    InitRectText(carDetail.TotalDistance, config.Widths[2], config.Widths[0] + config.Widths[1], loc, height, car.Row);
                    InitRectText(carDetail.TowElectricity, config.Widths[3], config.Widths[0] + config.Widths[1] + config.Widths[2], loc, height, car.Row);
                    InitRectText(carDetail.RiviveElectricity, config.Widths[4], config.Widths[0] + config.Widths[1] + config.Widths[2] + config.Widths[3], loc, height, car.Row);
                    InitRectText(carDetail.ConsumeElectricity, config.Widths[5], config.Widths[0] + config.Widths[1] + config.Widths[2] + config.Widths[3] + config.Widths[4], loc, height, car.Row);
                }
            }
        }

        private void InitRectText(string sPara, int widith, int length, Point loc, float height, int row)
        {
            m_Collection.Add(new GDIRectText()
            {
                Text = sPara,
                OutLineRectangle = new Rectangle(loc.X + length, loc.Y + (int)(height * row), widith, (int)height),
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextFormat = Right,
                TextColor = TextColor
            });
        }

        private void InitTitleText(string sPara, int widith, int length, Point loc, int height)
        {
            m_Collection.Add(new GDIRectText()
            {
                Text = sPara,
                OutLineRectangle = new Rectangle(loc.X + length, loc.Y, widith, height),
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextFormat = Right,

            });
        }
        public void OnDraw(Graphics g)
        {
            if (IsConfig)
            {
                m_Collection.ForEach(e => e.OnPaint(g));
            }
            else
            {
                g.DrawImage(ImageResource.AcumuElec, 7, 117, ImageResource.AcumuElec.Width, ImageResource.AcumuElec.Height);
            }


        }
    }
}