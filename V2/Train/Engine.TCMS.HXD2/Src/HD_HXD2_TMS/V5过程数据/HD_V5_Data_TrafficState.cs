using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common.Control;

namespace HD_HXD2_TMS.V5过程数据
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V5_Data_TrafficState : baseClass
    {
        private List<Image> _images = new List<Image>();    //图片资源
        private String[] _strs1 = null;
        private String[] _strs2 = null;
        private String[] _strs3 = null;
        private String[] _strs4 = null;
        private List<Button> _btns = new List<Button>();

        public override string GetInfo()
        {
            return "过程数据界面-主变流";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    _images.Add(Image.FromStream(fs));
                }
            });

            String[] strs = {"RIOM1", "RIOM2-1", "RIOM2-2"};
            for (int i = 0; i < 3; i++)
            {
                Button btn = new Button(
                    strs[i],
                    new RectangleF(13+259*i,119+4,258,28),
                    i,
                    new ButtonStyle()
                    {
                        Background = _images[1],
                        DownImage = _images[2],
                        FontStyle = new ES.Facility.Common.FontStyle_ES()
                        {
                            Font=new Font("宋体",13),
                            TextBrush=(SolidBrush)Brushes.AliceBlue,
                            StringFormat = new StringFormat()
                            {
                                 Alignment= StringAlignment.Center,
                                 LineAlignment= StringAlignment.Center
                            }
                        }
                    },
                    false
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }
            _btns[0].IsReplication = false;

            _strs1=new String[16]
            {
                "100","10J","10K","10M","10N","10E","10F","159","160","158","2028D","10Q","32R","201D","201E","210"
            };

            _strs2 = new String[10]
            {
                "32R", "201D", "201E", "10E", "10F", "160", "158", "210", "2028D", "10Q"
            };

            _strs3 = new String[12]
            {
                "180C","1916","1970F","1970E","1970AA","1970B","1970C","1970A","716TT","256","26B","26A"
            };

            _strs4 = new String[7]
            {
                "256A","253","299M","267","47","200AJ","227"
            };

            return true;
        }

        public override bool mouseDown(Point point)
        {
            _btns.ForEach(a => a.MouseDown(point));

            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            _btns.ForEach(a => a.MouseUp(point));
            
            return base.mouseUp(point);
        }

        void btn_ClickEvent(object sender, ES.Facility.Common.Control.Common.ClickEventArgs<int> e)
        {
            if (e.Message == 0)
            {
                _btns[0].IsReplication = false;
                return;
            }

            _btns[0].IsReplication = false;
            _btns[e.Message].IsReplication = true;
            append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, 511 + e.Message, 0, 0);
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            dcGs.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            dcGs.DrawImage(_images[0], new Rectangle(0,119,800,438));

            _btns.ForEach(a => a.Paint(dcGs));

            //CC-DI-0204
            for (int i = 0; i < 16; i++)
            {
                Boolean isSet = false;
               if (BoolList[UIObj.InBoolList[0]+ i])
               {
                   isSet = true;
                   dcGs.FillRectangle(Brushes.LimeGreen, new Rectangle(109+85*(i%8), 90+119+33*(i/8), 79, 21));
               }
               dcGs.DrawString(
                   _strs1[i],
                   new Font("宋体", 11),
                   isSet?Brushes.Black:Brushes.White,
                   new Rectangle(109 + 85 * (i % 8), 90 + 119 + 33 * (i / 8), 79, 21),
                   new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                   );
            }

            //CC-DI-0205
            for (int i = 0; i < 10; i++)
            {
                Boolean isSet = false;
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    isSet = true;
                    dcGs.FillRectangle(Brushes.LimeGreen, new Rectangle(109 + 85 * (i % 8), 167 + 119 + 33 * (i / 8), 79, 21));
                }
                dcGs.DrawString(
                    _strs2[i],
                    new Font("宋体", 11),
                    isSet ? Brushes.Black : Brushes.White,
                    new Rectangle(109 + 85 * (i % 8), 167 + 119 + 33 * (i / 8), 79, 21),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
            }

            //CC-DI-0206
            for (int i = 0; i < 12; i++)
            {
                Boolean isSet = false;
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    isSet = true;
                    dcGs.FillRectangle(Brushes.LimeGreen, new Rectangle(109 + 85 * (i % 8), 239 + 119 + 33 * (i / 8), 79, 21));
                }
                dcGs.DrawString(
                    _strs3[i],
                    new Font("宋体", 11),
                    isSet ? Brushes.Black : Brushes.White,
                    new Rectangle(109 + 85 * (i % 8), 239 + 119 + 33 * (i / 8), 79, 21),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
            }

            //CC-DI-0207
            for (int i = 0; i < 7; i++)
            {
                Boolean isSet = false;
                if (BoolList[UIObj.InBoolList[3] + i])
                {
                    isSet = true;
                    dcGs.FillRectangle(Brushes.LimeGreen, new Rectangle(109 + 85 *i, 312 + 119, 79, 21));
                }
                dcGs.DrawString(
                    _strs4[i],
                    new Font("宋体", 11),
                    isSet ? Brushes.Black : Brushes.White,
                    new Rectangle(109 + 85*i, 312 + 119, 79, 21),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
            }

            dcGs.DrawString(
                FloatList[UIObj.InFloatList[0]].ToString("0.0"),
                new Font("宋体", 11),
                Brushes.Yellow,
                new Rectangle(241, 355 + 119, 79, 21),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                );

            base.paint(dcGs);
        }
    }
}
