using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using HD_HXD2_TMS.Extension;

namespace HD_HXD2_TMS.V1主界面
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V1_RightModule : baseClass
    {
        private List<Image> _images = new List<Image>();    //图片资源
        private List<Button> _btns = new List<Button>();
        private Int32 _oldSpeed = 0;
        
        public override string GetInfo()
        {
            return "主界面-右侧状态";
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

            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    "",
                    new RectangleF(658+43*i, 301+50, 39, 40),
                    i,
                    new ButtonStyle()
                    {
                        Background = _images[0+i*2],
                        DownImage = _images[1+i*2],
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("宋体", 13),
                            TextBrush = (SolidBrush)Brushes.White,
                            StringFormat = new StringFormat()
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            }
                        }
                    }
                    );
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            return true;
        }

        public override bool mouseDown(Point point)
        {
            if (HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentAView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentBView)
                return base.mouseDown(point);

            _btns.ForEach(a => a.MouseDown(point));

            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            if (HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentAView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentBView)
                return base.mouseDown(point);

            _btns.ForEach(a => a.MouseUp(point));

            return base.mouseUp(point);
        }

        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            this.SetOutBoolValue( UIObj.OutBoolList[0] + e.Message, 0, 0);
        }

        void btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            this.SetOutBoolValue( UIObj.OutBoolList[0] + e.Message, 1, 0);
        }

        private Int32 _count1 = 0;
        private Int32 _count = 0;
        private Int32 _oldValue1 = 0;
        private Int32 _oldValue2 = 0;

        public override void paint(Graphics dcGs)
        {
            //实际牵引力
            dcGs.DrawRectangle(Pens.White, new Rectangle(548,167,80,42));
            dcGs.DrawString(
                "实际牵引力",
                new Font("宋体", 10),
                Brushes.White,
                new RectangleF(548, 209, 80, 30),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );
            bool isBreaking = false;
            if (BoolList[UIObj.InBoolList[0]]) isBreaking = true;
            String realValue = FloatList[UIObj.InFloatList[isBreaking ? 0 : 2]].ToString("0");
            if (!isBreaking&&realValue!="0") realValue = "-" + realValue;
            dcGs.DrawString(
                realValue,
                new Font("宋体", 18),
                Brushes.Yellow,
                new RectangleF(548, 167, 80, 42),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );

            //设定牵引力
            dcGs.DrawRectangle(Pens.White, new Rectangle(548, 239, 80, 42));
            dcGs.DrawString(
                "设定牵引力",
                new Font("宋体", 10),
                Brushes.White,
                new RectangleF(548, 281, 80, 30),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );
            String value = FloatList[UIObj.InFloatList[isBreaking ? 1 : 3]].ToString("0");
            if (!isBreaking&&value!="0") value = "-" + value;
            dcGs.DrawString(
                value,
                new Font("宋体", 18),
                Brushes.Yellow,
                new RectangleF(548, 239, 80, 42),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );

            //实际速度
            dcGs.DrawRectangle(Pens.White, new Rectangle(658, 167, 80, 42));
            dcGs.DrawString(
                "实际速度",
                new Font("宋体", 10),
                Brushes.White,
                new RectangleF(658, 209, 80, 30),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );
            dcGs.DrawString(
                "km/h",
                new Font("宋体", 11),
                Brushes.Yellow,
                new RectangleF(738+5, 167, 80, 42),
                new StringFormat() { LineAlignment = StringAlignment.Far, Alignment = StringAlignment.Near }
                );
            Brush b = Brushes.LimeGreen;
            String strTemp = FloatList[UIObj.InFloatList[4]].ToString();
            strTemp = strTemp.Split('.')[0];
            _oldValue1 = Convert.ToInt32(strTemp);
            

            if (_oldSpeed != _oldValue1)
            {
                if (_oldValue1 != _oldValue2)
                {
                    _count = (_count + 1) % 20;
                    _oldValue2 = _oldValue1;
                    if (_count1 > 10) _count = 0;
                    _count1++;
                }
                else
                {
                    b = new SolidBrush(Color.FromArgb(0, 255, 255));
                    _count = (_count + 1) % 60;
                }

                if (_count == 0)
                {
                    _count1 = 0;
                    _oldSpeed = Convert.ToInt32(FloatList[UIObj.InFloatList[4]].ToString("0"));
                }
            }
            dcGs.DrawString(
                _oldValue1.ToString(),
                new Font("宋体", 18),
                b,
                new RectangleF(658, 167, 80, 42),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );

            //设定速度
            dcGs.DrawRectangle(Pens.White, new Rectangle(658, 239, 80, 42));
            dcGs.DrawString(
                "设定速度",
                new Font("宋体", 10),
                Brushes.White,
                new RectangleF(658, 281, 80, 30),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );
            dcGs.DrawString(
                "km/h",
                new Font("宋体", 11),
                Brushes.Yellow,
                new RectangleF(738+5, 239, 80, 42),
                new StringFormat() { LineAlignment = StringAlignment.Far, Alignment = StringAlignment.Near }
                );
            strTemp = FloatList[UIObj.InFloatList[5]].ToString();
            strTemp = strTemp.Split('.')[0];
            dcGs.DrawString(
                strTemp,
                new Font("宋体", 18),
                Brushes.Yellow,
                new RectangleF(658, 239, 80, 42),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );

            //司控器级位
            dcGs.DrawRectangle(Pens.White, new Rectangle(658, 448, 80, 42));
            dcGs.DrawString(
                "司控器级位",
                new Font("宋体", 10),
                Brushes.White,
                new RectangleF(658, 490, 80, 30),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );
            String level = FloatList[UIObj.InFloatList[6]].ToString("0.0");
            if (BoolList[UIObj.InBoolList[2]]) level = "*";
            else if (BoolList[UIObj.InBoolList[3]]) level = "-" + level;
            dcGs.DrawString(
                level,
                new Font("宋体", 18),
                Brushes.Yellow,
                new RectangleF(658, 448, 80, 42),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );

            _btns.ForEach(a => a.Paint(dcGs));

            base.paint(dcGs);
        }
    }
}
