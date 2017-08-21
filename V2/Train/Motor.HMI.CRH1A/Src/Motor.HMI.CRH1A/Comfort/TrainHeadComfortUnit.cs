using System.Drawing;
using System.Windows.Forms;
using Motor.HMI.CRH1A.Common;
using CommonUtil.Controls;

namespace Motor.HMI.CRH1A.Comfort
{
    /// <summary>
    /// 车头
    /// </summary>
    public class TrainHeadComfortUnit : ComfortUnit
    {
        public TrainHeadComfortUnit(Point startPoint)
            : base(startPoint)
        {
            RoomType = RoomType.Head;

            var temper1 = new GDIRectText();
            temper1.SetTextRect(TrainBodyRectangle.X + 10, TrainBodyRectangle.Y + 20, TemperSize.Width, TemperSize.Height);
            temper1.SetBkColor(192, 192, 192);
            temper1.SetTextColor(0, 0, 0);
            temper1.SetTextStyle(8, FormatStyle.Center, true, "Arial");
            temper1.Isdrawrectfrm = true;

            var temper2 = new GDIRectText();
            temper2.SetTextRect(TrainBodyRectangle.X + 60, TrainBodyRectangle.Y + 20, TemperSize.Width, TemperSize.Height);
            temper2.SetBkColor(192, 192, 192);
            temper2.SetTextColor(0, 0, 0);
            temper2.SetTextStyle(8, FormatStyle.Center, true, "Arial");
            temper2.Isdrawrectfrm = true;

            TempeRectangle = new GDIRectText[] { temper1, temper2 };


            LightRectangle = new Rectangle(TrainBodyRectangle.X + 40, TrainBodyRectangle.Y + 15, LightSize.Width, LightSize.Height);

        }
    }

    public class TrainTailComfortUnit : ComfortUnit
    {
        public TrainTailComfortUnit(Point startPoint)
            : base(startPoint)
        {
            RoomType = RoomType.Tail;
        }
    }

}