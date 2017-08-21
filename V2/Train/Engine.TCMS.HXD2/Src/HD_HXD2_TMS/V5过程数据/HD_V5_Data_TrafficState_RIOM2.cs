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
    public class HD_V5_Data_TrafficState_RIOM2 : baseClass
    {
        private List<Image> _images = new List<Image>();    //图片资源
        private String[] _strs1 = null;
        private String[] _strs2 = null;
        private String[] _strs3 = null;
        private String[] _strs4 = null;
        private String[] _strs5 = null;
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
            String[] strs = { "RIOM1", "RIOM2-1", "RIOM2-2" };
            for (int i = 0; i < 3; i++)
            {
                Button btn = new Button(
                    strs[i],
                    new RectangleF(13 + 259 * i, 119 + 4, 258, 28),
                    i,
                    new ButtonStyle()
                    {
                        Background = _images[1],
                        DownImage = _images[2],
                        FontStyle = new ES.Facility.Common.FontStyle_ES()
                        {
                            Font = new Font("宋体", 13),
                            TextBrush = (SolidBrush)Brushes.AliceBlue,
                            StringFormat = new StringFormat()
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            }
                        }
                    },
                    false
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }
            _btns[2].IsReplication = false;

            _strs1=new String[10]
            {
                "720","719-1","718","200AA","192M","192P","299D","192A","192D","192C"
            };

            _strs2 = new String[13]
            {
                "720","719-2","718","200AA","200P","200M","200N","299D","192A","680M","298D","192D","192C"
            };

            _strs3 = new String[9]
            {
                "1970G","217A","188B","1901","216A","24A","32M","24D","1J-1"
            };

            _strs4 = new String[14]
            {
                "152","150","188A","151F","1970G","188","151D","780","172A","1902","631","630","32M","151E"
            };

            _strs5 = new String[14]
            {
                "152","150","178","185","961","210B","210A","681","644","J194","J195","J193","J192","J191"
            };

            return true;
        }

        void btn_ClickEvent(object sender, ES.Facility.Common.Control.Common.ClickEventArgs<int> e)
        {
            if (e.Message == 2)
            {
                _btns[2].IsReplication = false;
                return;
            }

            _btns[2].IsReplication = false;
            _btns[e.Message].IsReplication = true;
            append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, 511 + e.Message, 0, 0);
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

        public override void paint(Graphics dcGs)
        {
            dcGs.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            dcGs.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            dcGs.DrawImage(_images[0], new Rectangle(0, 119, 800, 438));

            _btns.ForEach(a => a.Paint(dcGs));

            //CC-DI-0204
            for (int i = 0; i < 10; i++)
            {
                Boolean isSet = false;
               if (BoolList[UIObj.InBoolList[0]+ i])
               {
                   isSet = true;
                   dcGs.FillRectangle(Brushes.LimeGreen, new Rectangle(109+85*(i%8), 71+119+33*(i/8), 79, 21));
               }
               dcGs.DrawString(
                   _strs1[i],
                   new Font("宋体", 11),
                   isSet?Brushes.Black:Brushes.White,
                   new Rectangle(109 + 85 * (i % 8), 71 + 119 + 33 * (i / 8), 79, 21),
                   new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                   );
            }

            //CC-DI-0205
            for (int i = 0; i < 13; i++)
            {
                Boolean isSet = false;
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    isSet = true;
                    dcGs.FillRectangle(Brushes.LimeGreen, new Rectangle(109 + 85 * (i % 7), 138 + 119 + 33 * (i / 7), 79, 21));
                }
                dcGs.DrawString(
                    _strs2[i],
                    new Font("宋体", 11),
                    isSet ? Brushes.Black : Brushes.White,
                    new Rectangle(109 + 85 * (i % 7), 138 + 119 + 33 * (i / 7), 79, 21),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
            }

            //CC-DI-0206
            for (int i = 0; i < 9; i++)
            {
                Boolean isSet = false;
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    isSet = true;
                    dcGs.FillRectangle(Brushes.LimeGreen, new Rectangle(109 + 85 * (i % 8), 210 + 119 + 33 * (i / 8), 79, 21));
                }
                dcGs.DrawString(
                    _strs3[i],
                    new Font("宋体", 11),
                    isSet ? Brushes.Black : Brushes.White,
                    new Rectangle(109 + 85 * (i % 8), 210 + 119 + 33 * (i / 8), 79, 21),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
            }

            //CC-DI-0207
            for (int i = 0; i < 14; i++)
            {
                Boolean isSet = false;
                if (BoolList[UIObj.InBoolList[3] + i])
                {
                    isSet = true;
                    dcGs.FillRectangle(Brushes.LimeGreen, new Rectangle(109 + 85 * (i % 8), 280 + 119 + 33 * (i / 8), 79, 21));
                }
                dcGs.DrawString(
                    _strs4[i],
                    new Font("宋体", 11),
                    isSet ? Brushes.Black : Brushes.White,
                    new Rectangle(109 + 85 * (i % 8), 280 + 119 + 33 * (i / 8), 79, 21),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
            }

            //CC-DI-0207
            for (int i = 0; i < 14; i++)
            {
                Boolean isSet = false;
                if (BoolList[UIObj.InBoolList[4] + i])
                {
                    isSet = true;
                    dcGs.FillRectangle(Brushes.LimeGreen, new Rectangle(109 + 85 * (i % 8), 345 + 119 + 33 * (i / 8), 79, 21));
                }
                dcGs.DrawString(
                    _strs5[i],
                    new Font("宋体", 11),
                    isSet ? Brushes.Black : Brushes.White,
                    new Rectangle(109 + 85 * (i % 8), 345 + 119 + 33 * (i / 8), 79, 21),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
            }

            base.paint(dcGs);
        }
    }
}
