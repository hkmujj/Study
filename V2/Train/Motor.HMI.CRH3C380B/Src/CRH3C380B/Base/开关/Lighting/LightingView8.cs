using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util.Extension;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.开关.Lighting
{
    internal class LightingView8 : LightViewBase
    {
        public LightingView8(CRH3C380BBase baseClass)
            : base(baseClass)
        {
            var line1 = (Line)ConstInfoCollection.Find(f => f is Line && (int)f.Tag == 1);
            var line2 = (Line)ConstInfoCollection.Find(f => f is Line && (int)f.Tag == 2);
            var line3 = (Line)ConstInfoCollection.Find(f => f is Line && (int)f.Tag == 3);
            var line4 = (Line)ConstInfoCollection.Find(f => f is Line && (int)f.Tag == 4);
            var crose1 = new Point(line4.StartPoint.X, line1.StartPoint.Y);
            var lines = ConstInfoCollection.FindIndex(f => f is Line && (int)f.Tag > 5);
            ConstInfoCollection[ConstInfoCollection.FindIndex(f => f is Line && (int)f.Tag == 5)].Visible = baseClass.DMITitle.MarshallMode;
            ConstInfoCollection[ConstInfoCollection.FindIndex(f => f is Line && (int)f.Tag == 6)].Visible = baseClass.DMITitle.MarshallMode;
            ConstInfoCollection[ConstInfoCollection.FindIndex(f => f is Line && (int)f.Tag == 7)].Visible = baseClass.DMITitle.MarshallMode;
            var size0 = new Size(40, 40);
            var size1 = new Size(20, 20);
            var txtSize = new Size(130, 20);
            var numTxtSize = new Size(25, 20);
            var txtColor = Color.White;
            var inflat = new Size(8, 8);
            const int txtInterval = 3;

            ConstInfoCollection.Add(new GDIRectText
            {
                                          TextColor = txtColor,
                                          Text = "全列车照明",
                                          OutLineRectangle = new Rectangle(line1.StartPoint.Translate(0, txtInterval), txtSize)
                                      });
            ConstInfoCollection.Add(new GDIRectText
            {
                                          TextColor = txtColor,
                                          Text = "本动车组照明",
                                          OutLineRectangle = new Rectangle(line2.StartPoint.Translate(0, txtInterval), txtSize)
                                      });
            ConstInfoCollection.Add(new GDIRectText
            {
                                          TextColor = txtColor,
                                          Text = "单车照明",
                                          OutLineRectangle = new Rectangle(line3.StartPoint.Translate(0, txtInterval), txtSize)
                                      });
            ConstInfoCollection.Add(new GDIRectText
            {
                                          TextColor = txtColor,
                                          Text = "重联动车组",
                                          OutLineRectangle = new Rectangle(line4.StartPoint.Translate(txtInterval, -txtInterval), txtSize)
                                      });


            crose1.Offset(inflat.Width, inflat.Height);

            LightViewItemCollection.Add(
                new SelectableLightViewItemDecorator(
                    new LightViewItem(LightingUnit.All)
                    {
                        OutLineRectangle = new Rectangle(crose1, size0),
                        RefreshAction = o => RefreshItemView((LightViewItem)o),
                        Tag = new[] { "照明--全列车--照明1/3", "照明--全列车--照明1" }
                    },
                    inflat));

            var crose2 = new Point(line4.StartPoint.X, line2.StartPoint.Y);
            ConstInfoCollection.Add(new GDIRectText
            {
                                          TextColor = txtColor,
                                          Text = "动车组1",
                                          OutLineRectangle = new Rectangle(crose2.Translate(txtInterval, -txtInterval - txtSize.Height), txtSize)
                                      });

            crose2.Offset(inflat.Width, inflat.Height);
            LightViewItemCollection.Add(new SelectableLightViewItemDecorator(new LightViewItem(LightingUnit.Unit1)
                                                                               {
                                                                                   OutLineRectangle = new Rectangle(crose2, size1),
                                                                                   RefreshAction = o => RefreshItemView((LightViewItem)o),
                                                                                   Tag = new[] { "照明--动车组1--照明1/3", "照明--动车组1--照明1" }
                                                                               },
                inflat));

            var crose3 = new Point(line4.StartPoint.X, line3.StartPoint.Y);
            crose3.Offset(inflat.Width, inflat.Height);

            for (int i = 11; i <= 18; i++)
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
                                                  new Rectangle(crose3.Translate(0, -txtInterval - inflat.Height - numTxtSize.Height), numTxtSize),
                                                 
                                          });

                crose3.Offset(size1.Width + inflat.Width * 2, 0);
            }

            SelectStrategy = new LightItemSelectStrategy(LightViewItemCollection.AsReadOnly());

            baseClass.UpdateUiObject(CommunicationDataType.InBool, LightViewItemCollection.Select(s => s.LightViewItemDecorater.Tag).Cast<string[]>().SelectMany(s => s));

        }
    }
}
