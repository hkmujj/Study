using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace TouchNetTrain_TMS_.手动控制
{
    /// <summary>
    /// 转向架
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V302_C0_HandlerControl_Turn : baseClass
    {
        private List<Image> _resource_Image = new List<Image>();    //图片资源
        private List<Button> _btns = new List<Button>();//按钮列表

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "运行信息-转向架支撑";
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

            String[] _strs = { "转向架手动控制",
                                 "转向架锁紧", "转向架解锁", 
                                 "DT11", "DT21", "DT16", "DT26",
                                 "DT12", "DT22", "DT17", "DT27", 
                                 "DT13", "DT23", "DT18", "DT28", 
                                 "DT14", "DT24", "DT19", "DT29", 
                                 "DT15", "DT25"
                                 };
            #region 转向架手动控制按钮
            Button btn1 = new Button(
                _strs[0],
                new RectangleF(162 + 207.5F, 110 + 4.5F, 203, 42),
                0,
                new ButtonStyle()
                {
                    FontStyle = new FontStyle_ES()
                    {
                        Font = Global.Font_Verdana_12_B,
                        TextBrush = new SolidBrush(Color.Black),
                        StringFormat =
                            new StringFormat()
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            }
                    },
                    Background = _resource_Image[0],
                    DownImage = _resource_Image[1]
                }
                );
            btn1.MouseDownEvent += btn_MouseDownEvent;
            btn1.ClickEvent += btn_ClickEvent;
            _btns.Add(btn1);

            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    _strs[1 + i],
                    new RectangleF(162 + 95.5F + (191 + 118) * i, 110 + 51 + 11, 118, 39),
                    1 + i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = Global.Font_Verdana_12_B,
                            TextBrush = new SolidBrush(Color.Black),
                            StringFormat =
                                new StringFormat()
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center
                                }
                        },
                        Background = _resource_Image[2],
                        DownImage = _resource_Image[3]
                    }
                    );
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            for (int i = 0; i < 18; i++)
            {
                Button btn = new Button(
                    _strs[3 + i],
                    new RectangleF(162 + 9.625F + (i % 2) * (77.25F * 3) + (i % 4 / 2) * (154.5F * 2), 110 + 153 + 12.5F + (i / 4) * 51, 58, 26),
                    3 + i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = Global.Font_Verdana_12,
                            TextBrush = new SolidBrush(Color.Black),
                            StringFormat =
                                new StringFormat()
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center
                                }
                        },
                        Background = _resource_Image[4],
                        DownImage = _resource_Image[5]
                    }
                    );
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

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
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 4800 + e.Message, 1, 0);
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 4800 + e.Message, 0, 0);
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            _btns.ForEach(a => a.Paint(dcGs));
            paint_Frame(dcGs);
            paint_TurnState(dcGs);
            paint_State(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 绘制线框
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            //外侧线框
            dcGs.DrawLines(
                Global.Pen_White_2,
                new[] { new PointF(162, 110), new PointF(162, 321), new PointF(153, 327.5F), new Point(162, 334), 
                    new Point(162, 520),new PointF(780, 520), new PointF(780, 110),new PointF(162, 110) }
                );
            //中间横线
            for (int i = 0; i < 7; i++)
            {
                if (i == 1)
                {
                    dcGs.DrawLine(
                        Global.Pen_White_2,
                        new Point(162, 110 + 51 + 51 * i + 10),
                        new Point(780, 110 + 51 + 51 * i + 10)
                        );
                    continue;
                }
                dcGs.DrawLine(
                    Global.Pen_White_2,
                    new Point(162, 110 + 51 + 51 * i),
                    new Point(780, 110 + 51 + 51 * i)
                    );
            }
            //中间竖线
            for (int i = 0; i < 3; i++)
            {
                dcGs.DrawLine(
                    Global.Pen_White_2,
                    new PointF(162 + 154.5F * (i + 1), 222 - 61 * (i % 2)),
                    new PointF(162 + 154.5F * (i + 1), 520)
                    );
            }
        }

        /// <summary>
        /// 绘制锁紧与解锁状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_TurnState(Graphics dcGs)
        {
            String[] strs = { "Ⅰ端锁紧", "Ⅱ端锁紧", "Ⅰ端解锁", "Ⅱ端解锁" };
            for (int i = 0; i < 4; i++)
            {
                dcGs.DrawString(
                    strs[i],
                    Global.Font_Verdana_12,
                    Brushes.White,
                    new RectangleF(162 + 1.5F + (i % 2) * (77.25F + 77.25F + 77.25F - 10) + (i / 2) * (154.5F + 154.5F), 110 + 51 + 61, 80.25F, 41),
                    Global.SF_CC
                    );
                bool isSet = BoolList[UIObj.InBoolList[0] + i * 2];
                dcGs.DrawImage(_resource_Image[isSet ? 6 : 7], new RectangleF(162 + 77.25F + 14.125F + (i % 2) * (49 + 14.125F + 14.125F) + (i / 2) * (154.5F + 154.5F), 110 + 51 + 61 + 6, 49, 29));
            }
        }

        /// <summary>
        /// 绘制转向架状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_State(Graphics dcGs)
        {
            String[] _strs = {"KP11", "KP21", "KP16", "KP26",
                              "KP12", "KP22", "S17", "S27", 
                              "KP13", "KP23", "S18", "S28", 
                              "KP14", "KP24", "S19", "S29", 
                              "KP15", "KP25"
                              };
            for (int i = 0; i < 18; i++)
            {
                bool isSet = BoolList[UIObj.InBoolList[1] + i * 2];
                dcGs.DrawImage(
                    _resource_Image[isSet ? 9 : 8],
                    new RectangleF(162 + 9.625F + 77.25F + 77.25F * (i % 2) + (i % 4 / 2) * (154.5F * 2), 110 + 153 + 12.5F + (i / 4) * 51, 58, 26)
                    );
                dcGs.DrawString(
                    _strs[i],
                    Global.Font_Verdana_12,
                    Brushes.Black,
                    new RectangleF(162 + 9.625F + 77.25F + 77.25F * (i % 2) + (i % 4 / 2) * (154.5F * 2), 110 + 153 + 12.5F + (i / 4) * 51, 58, 26),
                    Global.SF_CC
                    );
            }
        }

        #endregion
    }
}
