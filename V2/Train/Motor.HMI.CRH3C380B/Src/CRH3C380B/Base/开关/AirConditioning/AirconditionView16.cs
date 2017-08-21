using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util.Extension;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.开关.AirConditioning
{
    internal class AirconditionView16 : AirconditionView8
    {
        public AirconditionView16(CRH3C380BBase baseClass)
            : base(baseClass)
        {
            var line7 = (Line) ConstInfoCollection.Find(f => f is Line && (int) f.Tag == 7);
            var line8 = (Line) ConstInfoCollection.Find(f => f is Line && (int) f.Tag == 8);
            var line11 = (Line) ConstInfoCollection.Find(f => f is Line && (int) f.Tag == 11);

            var size1 = new Size(20, 20);
            var txtSize = new Size(120, 20);
            var numTxtSize = new Size(25, 20);
            var txtColor = Color.White;
            var inflat = new Size(8, 8);
            const int txtInterval = 3;
            var crose2 = new Point(line11.StartPoint.X, line7.StartPoint.Y);
            ConstInfoCollection.Add(new GDIRectText
            {
                TextColor = txtColor,
                Text = "动车组2",
                OutLineRectangle = new Rectangle(crose2.Translate(txtInterval, -txtInterval - txtSize.Height), txtSize)
            });

            crose2.Offset(inflat.Width, inflat.Height);
            AirconditionViewItemCollection.Add(
                new SelectableAirconditionViewItemDecorator(new AirconditionViewItem(AirconditionLevelUnit.Unit1)
                {
                    OutLineRectangle = new Rectangle(crose2, size1),
                    RefreshAction = o => RefreshItemView((AirconditionViewItem) o),
                    Tag = new[] {"空调紧急关", "空调-动车组2--手动开启空调", "空调-动车组2--自动开启空调"}
                },
                    inflat));
            var crose3 = new Point(line11.StartPoint.X, line8.StartPoint.Y);
            crose3.Offset(inflat.Width, inflat.Height);

            for (int i = 21; i <= 28; i++)
            {

                AirconditionViewItemCollection.Add(
                    new SelectableAirconditionViewItemDecorator(new AirconditionViewItem((AirconditionLevelUnit) i)
                    {
                        OutLineRectangle = new Rectangle(crose3, size1),
                        RefreshAction = o => RefreshItemView((AirconditionViewItem) o),
                        Tag =
                            new[]
                            {
                                "空调紧急关",
                                string.Format("空调-单车空调--{0}---手动开启空调", i),
                                string.Format("空调-单车空调--{0}---自动开启空调", i)
                            }
                    },
                        inflat));

                ConstInfoCollection.Add(new GDIRectText
                {
                    TextColor = txtColor,
                    Text = baseClass.DMITitle.MarshallMode ? (i).ToString("00") : (i - 10).ToString("0"),
                    OutLineRectangle =
                        new Rectangle(crose3.Translate(0, -txtInterval - inflat.Height - numTxtSize.Height), numTxtSize)
                });

                crose3.Offset(size1.Width + inflat.Width*2, 0);
            }

            var location = new Point(454, 380);
            var itemSize = new Size(20, 80);
            for (int i = 21; i <= 28; i++)
            {
                AirconditionViewItemCollection.Add(
                    new SelectableAirconditionViewItemDecorator(new AirconditionViewItem((AirconditionLevelUnit) i)
                    {
                        RefreshAction = o => RefreshItemView((AirconditionViewItem) o),
                        OutLineRectangle = new Rectangle(),
                        Tag =
                            new[]
                            {
                                string.Format("空调-温度--{0}--0", i),
                                string.Format("空调-温度--{0}--1", i),
                                string.Format("空调-温度--{0}--2", i)
                            },

                    },
                        inflat));

                ConstInfoCollection.Add(new AirconditionViewItem {OutLineRectangle = new Rectangle(location, itemSize)});
                location.Offset(size1.Width + inflat.Width*2, 0);
            }

            SelectStrategy = new AirconditionSelectStrategy(AirconditionViewItemCollection.AsReadOnly());
            baseClass.UpdateUiObject(CommunicationDataType.InBool,
                AirconditionViewItemCollection.Select(s => s.AirconditionViewItemDecorater.Tag)
                    .Cast<string[]>()
                    .SelectMany(s => s)
                    .Distinct());

        }
    }
}
