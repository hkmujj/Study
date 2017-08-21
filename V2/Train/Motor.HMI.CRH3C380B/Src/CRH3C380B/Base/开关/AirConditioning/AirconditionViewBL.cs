using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util.Extension;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.开关.AirConditioning
{
    internal class AirconditionViewBL : AirConditionBLViewBase
    {
        public AirconditionViewBL(CRH3C380BBase baseClass)
            : base(baseClass)
        {

            var size0 = new Size(40, 40);
            var size1 = new Size(20, 20);
            var txtSize = new Size(120, 20);
            var numTxtSize = new Size(25, 20);
            var txtColor = Color.White;
            var inflat = new Size(8, 8);
            const int txtInterval = 3;
            var line7 = (Line) ConstInfoCollection.Find(f => f is Line && (int) f.Tag == 7);
            var line8 = (Line) ConstInfoCollection.Find(f => f is Line && (int) f.Tag == 8);
            var line11 = (Line) ConstInfoCollection.Find(f => f is Line && (int) f.Tag == 11);
            Graphics(txtColor, 155, 390);
            var line15 = (Line) ConstInfoCollection.Find(f => f is Line && (int) f.Tag == 15);

            ConstInfoCollection.Add(new Line(line15.EndPoint, new Point(line15.EndPoint.X + 293, line15.EndPoint.Y))
            {
                Color = txtColor,
                Tag = 18,
                Visible = baseClass.DMITitle.MarshallMode
            });
            //获取第一条Line
            var line1 = (Line) ConstInfoCollection.Find(f => f is Line && (int) f.Tag == 1);
            //获取第二条Line
            var line2 = (Line) ConstInfoCollection.Find(f => f is Line && (int) f.Tag == 2);
            //获取第三条Line
            var line3 = (Line) ConstInfoCollection.Find(f => f is Line && (int) f.Tag == 3);
            //获取第五条Line
            // var line5 = (Line)m_ConstInfoCollection.Find(f => f is Line && (int)f.Tag == 5);
            //获取第六条Line
            var line6 = (Line) ConstInfoCollection.Find(f => f is Line && (int) f.Tag == 6);
            line2.Visible = false;
            line7.Visible = false;
            line11.Visible = false;
            var crose1 = new Point(line6.StartPoint.X, line1.StartPoint.Y);



            ConstInfoCollection.Add(new GDIRectText
            {
                TextColor = txtColor,
                Text = "动车组空调",
                OutLineRectangle = new Rectangle(line1.StartPoint.Translate(0, txtInterval), txtSize)
            });
            ConstInfoCollection.Add(new GDIRectText
            {
                TextColor = txtColor,
                Text = "单车空调",
                OutLineRectangle = new Rectangle(line3.StartPoint.Translate(0, txtInterval), txtSize)
            });
            ConstInfoCollection.Add(new GDIRectText
            {
                TextColor = txtColor,
                Text = "动车",
                OutLineRectangle = new Rectangle(line6.StartPoint.Translate(txtInterval, -txtInterval), txtSize)
            });


            crose1.Offset(inflat.Width, inflat.Height);

            AirconditionViewItemCollection.Add(
                new SelectableAirconditionViewItemDecorator(
                    new AirconditionViewItem(AirconditionLevelUnit.All)
                    {
                        OutLineRectangle = new Rectangle(crose1, size0),
                        RefreshAction = o => RefreshItemView((AirconditionViewItem) o),
                        Tag = new[] {"空调紧急关", "空调-动车组1--手动开启空调", "空调-动车组1--自动开启空调"}
                    },
                    inflat));



            var crose3 = new Point(line6.StartPoint.X, line3.StartPoint.Y);
            crose3.Offset(inflat.Width, inflat.Height);

            for (int i = 11; i <= 18; i++)
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
                    Text = (i - 10).ToString("00"),
                    OutLineRectangle =
                        new Rectangle(crose3.Translate(0, -txtInterval - inflat.Height - numTxtSize.Height), numTxtSize)
                });

                crose3.Offset(size1.Width + inflat.Width*2, 0);
            }

            var location = new Point(165, 380);
            var itemSize = new Size(20, 80);
            for (int i = 11; i <= 18; i++)
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

                ConstInfoCollection.Add(new GDIRectText
                {
                    OutLineRectangle = new Rectangle(location, itemSize),
                    BkColor = Color.Black,
                    BackColorVisible = true,
                    NeedDarwOutline = true
                });
                location.Offset(size1.Width + inflat.Width*2, 0);
            }

            crose3 = new Point(line11.StartPoint.X, line8.StartPoint.Y);
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
                    Text = (i - 12).ToString("00"),
                    OutLineRectangle =
                        new Rectangle(crose3.Translate(0, -txtInterval - inflat.Height - numTxtSize.Height), numTxtSize)
                });

                crose3.Offset(size1.Width + inflat.Width*2, 0);
            }

            location = new Point(454, 380);
            itemSize = new Size(20, 80);
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

                ConstInfoCollection.Add(new GDIRectText
                {
                    OutLineRectangle = new Rectangle(location, itemSize),
                    BkColor = Color.Black,
                    BackColorVisible = true,
                    NeedDarwOutline = true
                });
                location.Offset(size1.Width + inflat.Width*2, 0);
            }

            SelectStrategy = new AirconditionSelectStrategy(AirconditionViewItemCollection.AsReadOnly());
            baseClass.UpdateUiObject(CommunicationDataType.InBool,
                AirconditionViewItemCollection.Select(s => s.AirconditionViewItemDecorater.Tag)
                    .Cast<string[]>()
                    .SelectMany(s => s)
                    .Distinct());
        }

        private void Graphics(Color txtColor, int x1, int y4)
        {
            //循环画温度的刻度线
            for (int i = 0; i < 7; i++)
            {
                if (i != 3)
                {
                    if (i == 0)
                    {
                        //画字符串"高"
                        ConstInfoCollection.Add(new GDIRectText
                        {
                            TextColor = txtColor,
                            Text = "高",
                            OutLineRectangle =
                                new Rectangle(new Point(x1 - 30, y4 + i*10).Translate(0, -20), new Size(25, 20))
                        });
                    }
                    //画字符串"低"
                    else if (i == 6)
                    {
                        ConstInfoCollection.Add(new GDIRectText
                        {
                            TextColor = txtColor,
                            Text = "低",
                            OutLineRectangle =
                                new Rectangle(new Point(x1 - 30, y4 + i*10).Translate(0, 5), new Size(25, 20))
                        });
                    }
                    //画刻度线
                    ConstInfoCollection.Add(new Line(new Point(x1 - 30, y4 + i*10), new Point(x1, y4 + i*10))
                    {
                        Color = Color.White,
                        Tag = 12 + i
                    });
                }
                else
                {
                    //画刻度线
                    ConstInfoCollection.Add(new Line(new Point(x1 - 45, y4 + i*10), new Point(x1 + 572, y4 + i*10))
                    {
                        Color = Color.White,
                        Tag = 12 + i
                    });
                    //画字符串"标准"
                    ConstInfoCollection.Add(new GDIRectText
                    {
                        TextColor = txtColor,
                        Text = "标准",
                        OutLineRectangle =
                            new Rectangle(new Point(x1 - 45, y4 + i*10).Translate(-50, -10), new Size(50, 20))
                    });
                }
            }
        }
    }
}