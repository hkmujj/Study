using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util.Extension;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.开关.Lighting
{
    internal class LightingView16 : LightingView8
    {
        public LightingView16(CRH3C380BBase baseClass)
            : base(baseClass)
        {
            var line5 = (Line)ConstInfoCollection.Find(f => f is Line && (int)f.Tag == 5);
            var line7 = (Line)ConstInfoCollection.Find(f => f is Line && (int)f.Tag == 6);
            var line6 = (Line)ConstInfoCollection.Find(f => f is Line && (int)f.Tag == 7);

            var size0 = new Size(40, 40);
            var size1 = new Size(20, 20);
            var txtSize = new Size(130, 20);
            var numTxtSize = new Size(25, 20);
            var txtColor = Color.White;
            var inflat = new Size(8, 8);
            const int txtInterval = 3;

            var crose2 = new Point(line5.StartPoint.X, line7.StartPoint.Y);
            ConstInfoCollection.Add(new GDIRectText
            {
                TextColor = txtColor,
                Text = "动车组2",
                OutLineRectangle = new Rectangle(crose2.Translate(txtInterval, -txtInterval - txtSize.Height), txtSize)
            });
            crose2.Offset(inflat.Width, inflat.Height);
            LightViewItemCollection.Add(new SelectableLightViewItemDecorator(new LightViewItem(LightingUnit.Unit1)
            {
                OutLineRectangle = new Rectangle(crose2, size1),
                RefreshAction = o => RefreshItemView((LightViewItem)o),
                Tag = new[] { "照明--动车组2--照明1/3", "照明--动车组2--照明1" }
            },
                inflat));

            var crose3 = new Point(line5.StartPoint.X, line6.StartPoint.Y);
            crose3.Offset(inflat.Width, inflat.Height);

            for (int i = 21; i <= 28; i++)
            {

                LightViewItemCollection.Add(new SelectableLightViewItemDecorator(new LightViewItem((LightingUnit)i)
                {
                    OutLineRectangle = new Rectangle(crose3, size1),
                    RefreshAction = o => RefreshItemView((LightViewItem)o),
                    Tag =
                        new[]
                                                                                           {
                                                                                               string.Format("照明-单车照明--{0}--照明1/3", i),
                                                                                               string.Format("照明-单车照明--{0}--照明1", i)
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

                crose3.Offset(size1.Width + inflat.Width * 2, 0);
            }
            SelectStrategy = new LightItemSelectStrategy(LightViewItemCollection.AsReadOnly());

            baseClass.UpdateUiObject(CommunicationDataType.InBool, LightViewItemCollection.Select(s => s.LightViewItemDecorater.Tag).Cast<string[]>().SelectMany(s => s));

        }
    }
}