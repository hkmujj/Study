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
    /// 离合齿
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V301_C0_HandlerControl_Clutch : baseClass
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
            return "运行信息-离合齿";
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
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    _resource_Image.Add(Image.FromStream(fs));
                }
            });

            String[] _strs = { "手动", "F1", "F2", "F3", "F4", "F5", "F6" };
            #region 4个大按钮
            Button btn1 = new Button(
                _strs[0],
                new RectangleF(462, 137, 102, 36),
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

            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 2; i++)
                {
                    Button btn = new Button(
                        _strs[1 + i + j * 2],
                        new RectangleF(323 + i * 223, 302 + j * 74, 64, 40),
                        1 + i + j * 2,
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
            paint_HeChiOrLiChi(dcGs);
            paint_State(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 绘制线框
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            for (int i = 0; i < 3; i++)
            {
                dcGs.DrawLine(
                    Global.Pen_White_2,
                    new Point(162, 110 + 88 * i),
                    new Point(740, 110 + 88 * i)
                    );
                dcGs.DrawLine(
                    Global.Pen_White_2,
                    new Point(162, 364 + 78 * i),
                    new Point(740, 364 + 78 * i)
                    );
            }
            for (int i = 0; i < 2; i++)
            {
                dcGs.DrawLine(
                    Global.Pen_White_2,
                    new Point(283 + 457 * i, 110),
                    new Point(283 + 457 * i, 520)
                    );
            }

            dcGs.DrawLine(
                    Global.Pen_White_2,
                    new Point(512, 198),
                    new Point(512, 520)
                    );

            //会该
            dcGs.DrawLines(
                Global.Pen_White_2,
                new Point[] { new Point(162, 110), new Point(162, 237), new Point(153, 245), new Point(162, 253), new Point(162, 520) }
                );
            dcGs.DrawString(
                "离合齿\n控制方式",
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(162, 110, 121, 88),
                Global.SF_CC
                );
            String[] strs = { "分动箱", "车齿箱1", "车齿箱2" };
            for (int i = 0; i < 3; i++)
            {
                dcGs.DrawString(
                    strs[i],
                    Global.Font_Verdana_13,
                    Brushes.White,
                    new RectangleF(162, 110 + 88 * 2 + 78 * i, 121, 78),
                    Global.SF_CC
                    );
            }

        }

        /// <summary>
        /// 绘制合齿与离齿状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_HeChiOrLiChi(Graphics dcGs)
        {
            String[] strs1 = { "合齿", "离齿" };
            for (int i = 0; i < 2; i++)
            {
                dcGs.DrawString(
                    strs1[i],
                    Global.Font_Verdana_13,
                    Brushes.White,
                    new RectangleF(345 + 225 * i, 195, 102, 40),
                    Global.SF_CC
                    );
                bool isSet = BoolList[UIObj.InBoolList[0] + i * 2];
                dcGs.DrawImage(_resource_Image[isSet ? 4 : 5], new Rectangle(345 + 225 * i, 230, 102, 37));
            }
        }


        /// <summary>
        /// 绘制离合齿状态-分动箱-车齿箱1-车齿箱2
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_State(Graphics dcGs)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    bool isSet = !BoolList[UIObj.InBoolList[1] + j * 2 + i * 4];
                    dcGs.DrawImage(
                        _resource_Image[isSet ? 7 : 6],
                        new RectangleF(323 + 97 + j * 223, 302 + i * 74, 64, 40)
                        );
                    String[] strs1 = { "S1", "S2", "S3", "S4", "S5", "S6" };
                    dcGs.DrawString(
                        strs1[i * 2 + j],
                        Global.Font_Verdana_12_B,
                        Brushes.Black,
                        new RectangleF(323 + 97 + j * 223, 302 + i * 74 + 2, 64, 40),
                        Global.SF_CC
                        );
                }
            }
        }

        #endregion
    }
}
