using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace TouchNetTrain_TMS_.辅助功能
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V8_C0_Assist : baseClass
    {
        private List<Image> _resource_Image = new List<Image>();    //图片资源
        private List<Button> _btns = new List<Button>();//按钮列表
        private int _currentLight = 0;

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "辅助功能-主界面";
        }

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Image.Add(Image.FromStream(fs));
                }
            });

            #region 触摸屏换向按钮
            //触摸屏换向按钮
            Button btn1 = new Button(
                    "触摸屏换向",
                    new RectangleF(20 + 15 + 30 + 55, 104 + 20 + 105 + 28 + 23, 135, 40),
                    0,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = Global.Font_Verdana_12,
                            TextBrush = new SolidBrush(Color.Black),
                            StringFormat = Global.SF_CC
                        },
                        Background = _resource_Image[7],
                        DownImage = _resource_Image[8]
                    }
                    );
            btn1.MouseDownEvent += btn_MouseDownEvent;
            btn1.ClickEvent += btn_ClickEvent;
            _btns.Add(btn1);
            #endregion

            #region 数据记录与转存按钮
            //数据记录与转存按钮
            Button btn2 = new Button(
                    "数据存储",
                    new RectangleF(310 + 15 + 50 + 15, 104 + 20 + 28 + 12, 115, 35),
                    1,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = Global.Font_Verdana_12,
                            TextBrush = new SolidBrush(Color.Black),
                            StringFormat = Global.SF_CC
                        },
                        Background = _resource_Image[9],
                        DownImage = _resource_Image[10]
                    }
                    );
            btn2.MouseDownEvent += btn_MouseDownEvent;
            btn2.ClickEvent += btn_ClickEvent;
            _btns.Add(btn2);

            Button btn3 = new Button(
                    "数据转存",
                    new RectangleF(310 + 15 + 50 + 15, 104 + 20 + 28 + 38 + 10 + 38, 150, 39),
                    2,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = Global.Font_Verdana_12,
                            TextBrush = new SolidBrush(Color.Black),
                            StringFormat = Global.SF_CC
                        },
                        Background = _resource_Image[11],
                        DownImage = _resource_Image[12]
                    }
                    );
            btn3.MouseDownEvent += btn_MouseDownEvent;
            btn3.ClickEvent += btn_ClickEvent;
            _btns.Add(btn3);
            #endregion

            #region 触摸调试
            //触摸调试
            Button btn4 = new Button(
                    "触摸调试",
                    new RectangleF(580 + 15, 104 + 20 + 28, 119, 40),
                    3,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = Global.Font_Verdana_12,
                            TextBrush = new SolidBrush(Color.Black),
                            StringFormat = Global.SF_CC
                        },
                        Background = _resource_Image[13],
                        DownImage = _resource_Image[14]
                    }
                    );
            btn4.MouseDownEvent += btn_MouseDownEvent;
            btn4.ClickEvent += btn_ClickEvent;
            _btns.Add(btn4);

            Button btn5 = new Button(
                    " 亮度",
                    new RectangleF(580 + 15, 104 + 20 + 28 + 30 + 40, 119, 40),
                    4,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = Global.Font_Verdana_12,
                            TextBrush = new SolidBrush(Color.Black),
                            StringFormat = Global.SF_NC
                        },
                        Background = _resource_Image[15],
                        DownImage = _resource_Image[16]
                    }
                    );
            btn5.MouseDownEvent += btn_MouseDownEvent;
            btn5.ClickEvent += btn_ClickEvent;
            _btns.Add(btn5);

            Button btn6 = new Button(
                    " 亮度",
                    new RectangleF(580 + 15, 104 + 20 + 28 + 60 + 80, 119, 40),
                    5,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = Global.Font_Verdana_12,
                            TextBrush = new SolidBrush(Color.Black),
                            StringFormat = Global.SF_NC
                        },
                        Background = _resource_Image[17],
                        DownImage = _resource_Image[18]
                    }
                    );
            btn6.MouseDownEvent += btn_MouseDownEvent;
            btn6.ClickEvent += btn_ClickEvent;
            _btns.Add(btn6);
            #endregion

            return true;
        }
        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            _btns.ForEach(a => a.MouseDown(nPoint));

            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btns.ForEach(a => a.MouseUp(nPoint));

            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 菜单切换按钮鼠标按下事件响应函数：改变按钮字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0://上一条
                    break;
                case 1://上一页
                    break;
                case 2://下一页
                    break;
                case 4://确定
                    if (_currentLight < 10) _currentLight++;
                    break;
                case 5://历史
                    if (_currentLight > 0) _currentLight--;
                    break;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            _btns.ForEach(a => a.Paint(dcGs));
            paint_TimeSetting(dcGs);
            paint_Touch(dcGs);
            paint_Data(dcGs);
            paint_Test(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 绘制时间设置
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_TimeSetting(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[0], new Rectangle(20, 104, 270, 105));
            dcGs.DrawString(
                "时间日期设置",
                Global.Font_Verdana_13,
                Brushes.Black,
                new RectangleF(20, 104, 270, 28),
                Global.SF_CC
                );
            dcGs.DrawString(
                "HH:MM:SS",
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(20 + 10, 104 + 33, 270, 38),
                Global.SF_NC
                );
            dcGs.DrawString(
                "yyyy/mm/dd",
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(20 + 10, 104 + 33 + 38, 270, 38),
                Global.SF_NN
                );
        }

        /// <summary>
        /// 绘制触摸屏换向
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Touch(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[1], new Rectangle(20, 104 + 20 + 105, 270, 125));
            dcGs.DrawString(
                "触摸屏Ⅰ/Ⅱ端换向",
                Global.Font_Verdana_13,
                Brushes.Black,
                new RectangleF(20, 104 + 20 + 105, 270, 28),
                Global.SF_CC
                );
            dcGs.DrawImage(_resource_Image[4], new Rectangle(20 + 15, 104 + 20 + 105 + 28 + 20, 55, 45));
        }

        /// <summary>
        /// 绘制数据记录
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Data(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[2], new Rectangle(310, 104, 242, 250));
            dcGs.DrawString(
                "数据记录与转存",
                Global.Font_Verdana_13,
                Brushes.Black,
                new RectangleF(310, 104, 242, 28),
                Global.SF_CC
                );
            dcGs.DrawImage(_resource_Image[5], new Rectangle(310 + 15, 104 + 20 + 28 + 10, 50, 38));
            dcGs.DrawImage(_resource_Image[6], new Rectangle(310 + 15 + 10, 104 + 20 + 28 + 38 + 10 + 30, 23, 58));
        }

        private SolidBrush sb = new SolidBrush(Color.FromArgb(243, 172, 106));
        /// <summary>
        /// 绘制触摸调试
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Test(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[3], new Rectangle(580, 104, 195, 250));
            dcGs.DrawString(
                "触摸调试",
                Global.Font_Verdana_13,
                Brushes.Black,
                new RectangleF(580, 104, 195, 28),
                Global.SF_CC
                );

            //亮度条
            dcGs.FillRectangle(Brushes.White, new Rectangle(580 + 15 + 120 + 20, 104 + 20 + 28 + 30 + 40, 20, 110));
            //填充亮度
            if (_currentLight == 0) return;
            for (int i = 1; i <= _currentLight; i++)
            {
                dcGs.FillRectangle(sb, new RectangleF(580 + 15 + 120 + 20 + 3, 104 + 20 + 28 + 30 + 40 + 110 - 2 - (9.5F + 1) * i, 14, 9.5F));
            }
        }
        #endregion
    }
}
