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

namespace TouchNetTrain_TMS_
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1_C0_Run_Left : baseClass
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
            return "运行信息-左侧信息";
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

            String[] _strs = new String[7] { "散热工况", "车上控制", "作业风扇手动", "作业机具锁定", "作业冷却风扇", "Ⅰ水冷却风扇低速", "Ⅰ油冷却风扇" };
            #region 3个大按钮
            for (int i = 0; i < 3; i++)
            {
                Button btn = new Button(
                    _strs[i],
                    new RectangleF(11, 102 + (67.7F + 6) * i, 124, 57),
                    i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = Global.Font_Verdana_12,
                            TextBrush = new SolidBrush(Color.Black),
                            StringFormat = Global.SF_CC
                        },
                        Background = _resource_Image[1],
                        DownImage = _resource_Image[2]
                    }
                    );
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }
            #endregion

            #region 4个小按钮(修改为状态显示)
            //for (int i = 0; i < 2; i++)
            //{
            //    Button btn = new Button(
            //        _strs[3 + i + 2 * 0],
            //        new RectangleF(11 + 126 * 0, 97 + (67.7F + 6) * (3 + 2 * 0) + 34 * i, 124, 32),
            //        i,
            //        new ButtonStyle()
            //        {
            //            FontStyle = new FontStyle_ES()
            //            {
            //                Font = Global.Font_Verdana_12_B,
            //                TextBrush = new SolidBrush(Color.Black),
            //                StringFormat =
            //                    new StringFormat()
            //                    {
            //                        Alignment = StringAlignment.Center,
            //                        LineAlignment = StringAlignment.Center
            //                    }
            //            },
            //            Background = _resource_Image[3],
            //            DownImage = _resource_Image[4]
            //        }
            //        );
            //    btn.MouseDownEvent += btn_MouseDownEvent;
            //    btn.ClickEvent += btn_ClickEvent;
            //    this._btns.Add(btn);
            //}
            //for (int i = 0; i < 2; i++)
            //{
            //    Button btn = new Button(
            //        _strs[3 + i + 2 * 1],
            //        new RectangleF(11 + 126 * 1, 97 + (67.7F + 6) * (3 + 2 * 1) + 34 * i, 124, 32),
            //        i,
            //        new ButtonStyle()
            //        {
            //            FontStyle = new FontStyle_ES()
            //            {
            //                Font = new Font("Verdana", 9, FontStyle.Bold),
            //                TextBrush = new SolidBrush(Color.Black),
            //                StringFormat =
            //                    new StringFormat()
            //                    {
            //                        Alignment = StringAlignment.Center,
            //                        LineAlignment = StringAlignment.Center
            //                    }
            //            },
            //            Background = _resource_Image[5 + i * 2],
            //            DownImage = _resource_Image[6 + i * 2]
            //        }
            //        );
            //    btn.MouseDownEvent += btn_MouseDownEvent;
            //    btn.ClickEvent += btn_ClickEvent;
            //    this._btns.Add(btn);
            //}
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
            paint_ZhuanSu(dcGs);
            paint_YouYa(dcGs);
            paint_ShuiWen(dcGs);
            paint_ShuiWen_C(dcGs);
            paint_ShuiWen_J(dcGs);
            paint_YouWen_Z(dcGs);
            paint_DianYa(dcGs);
            paint_ZuoYeJiJuSuoDing(dcGs);
            paint_ZuoYeLengQueFengShan(dcGs);
            paint_ShuiLengQueFengShanDiSu(dcGs);
            paint_YouLengQueFengShan(dcGs);

            base.paint(dcGs);
        }

        #region 状态绘制（String）
        /// <summary>
        /// 绘制转速
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_ZhuanSu(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[0], new Point(137, 97));
            dcGs.DrawString(
                "柴油机转速",
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(137, 97 + 34, 125, 34),
                Global.SF_CC
                );
            dcGs.DrawString(
                FloatList[UIObj.InFloatList[0]].ToString("0"),
                Global.Font_Verdana_13,
                Brushes.White,
                new Rectangle(137 + 5, 97 + 8, 70, 29),
                Global.SF_CC
                );

            dcGs.DrawString(
                "r/min",
                Global.Font_Verdana_12,
                Brushes.White,
                new Rectangle(137 + 5, 97 + 8, 115, 29),
                Global.SF_FC
                );
        }

        /// <summary>
        /// 绘制油压
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_YouYa(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[0], new PointF(137, 97 + (67.7F + 6) * 1));
            dcGs.DrawString(
                "柴油机油压",
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(137, 97 + (67.7F + 6) * 1 + 34, 125, 34),
                Global.SF_CC
                );
            dcGs.DrawString(
                FloatList[UIObj.InFloatList[1]].ToString("0"),
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(137 + 5, 97 + (67.7F + 6) * 1 + 8, 70, 29),
                Global.SF_CC
                );

            dcGs.DrawString(
                "kPa",
                Global.Font_Verdana_12,
                Brushes.White,
                new RectangleF(137 + 5, 97 + (67.7F + 6) * 1 + 8, 115, 29),
                Global.SF_FC
                );
        }

        /// <summary>
        /// 绘制水温
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_ShuiWen(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[0], new PointF(137, 97 + (67.7F + 6) * 2));
            dcGs.DrawString(
                "柴油机水温",
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(137, 97 + (67.7F + 6) * 2 + 34, 125, 34),
                Global.SF_CC
                );
            dcGs.DrawString(
                FloatList[UIObj.InFloatList[2]].ToString("0"),
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(137 + 5, 97 + (67.7F + 6) * 2 + 8, 70, 29),
                Global.SF_CC
                );

            dcGs.DrawString(
                "℃",
                Global.Font_Verdana_12,
                Brushes.White,
                new RectangleF(137 + 5, 97 + (67.7F + 6) * 2 + 8, 115, 29),
                Global.SF_FC
                );
        }

        /// <summary>
        /// 绘制油压
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_ShuiWen_C(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[0], new PointF(137, 97 + (67.7F + 6) * 3));
            dcGs.DrawString(
                "传动箱油温",
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(137, 97 + (67.7F + 6) * 3 + 34, 125, 34),
                Global.SF_CC
                );
            dcGs.DrawString(
                FloatList[UIObj.InFloatList[3]].ToString("0"),
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(137 + 5, 97 + (67.7F + 6) * 3 + 8, 70, 29),
                Global.SF_CC
                );

            dcGs.DrawString(
                "℃",
                Global.Font_Verdana_12,
                Brushes.White,
                new RectangleF(137 + 5, 97 + (67.7F + 6) * 3 + 8, 115, 29),
                Global.SF_FC
                );
        }

        /// <summary>
        /// 绘制油压
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_ShuiWen_J(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[0], new PointF(137, 97 + (67.7F + 6) * 4));
            dcGs.DrawString(
                "Ⅰ静液压油温",
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(137, 97 + (67.7F + 6) * 4 + 34, 130, 34),
                Global.SF_CC
                );
            dcGs.DrawString(
                FloatList[UIObj.InFloatList[4]].ToString("0"),
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(137 + 5, 97 + (67.7F + 6) * 4 + 8, 70, 29),
                Global.SF_CC
                );

            dcGs.DrawString(
                "℃",
                Global.Font_Verdana_12,
                Brushes.White,
                new RectangleF(137 + 5, 97 + (67.7F + 6) * 4 + 8, 115, 29),
                Global.SF_FC
                );
        }

        /// <summary>
        /// 绘制作业液压
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_YouWen_Z(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[0], new PointF(11, 97 + (67.7F + 6) * 4));
            dcGs.DrawString(
                "作业压油温",
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(11, 97 + (67.7F + 6) * 4 + 34, 130, 34),
                Global.SF_CC
                );
            dcGs.DrawString(
                FloatList[UIObj.InFloatList[5]].ToString("0"),
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(11 + 5, 97 + (67.7F + 6) * 4 + 8, 70, 29),
                Global.SF_CC
                );

            dcGs.DrawString(
                "℃",
                Global.Font_Verdana_12,
                Brushes.White,
                new RectangleF(11 + 5, 97 + (67.7F + 6) * 4 + 8, 115, 29),
                Global.SF_FC
                );
        }

        /// <summary>
        /// 控制电压
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_DianYa(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[0], new PointF(11, 97 + (67.7F + 6) * 5));
            dcGs.DrawString(
                "控制电压",
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(11, 97 + (67.7F + 6) * 5 + 34, 130, 34),
                Global.SF_CC
                );
            dcGs.DrawString(
                FloatList[UIObj.InFloatList[6]].ToString("0.0"),
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(11 + 5, 97 + (67.7F + 6) * 5 + 8, 70, 29),
                Global.SF_CC
                );

            dcGs.DrawString(
                "V",
                Global.Font_Verdana_12,
                Brushes.White,
                new RectangleF(11 + 5, 97 + (67.7F + 6) * 5 + 8, 115, 29),
                Global.SF_FC
                );
        }
        #endregion

        #region 状态绘制（Image）
        /// <summary>
        /// 绘制作业机具锁定状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_ZuoYeJiJuSuoDing(Graphics dcGs)
        {
            int index = 0;
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    index = i;
                }
            }
            dcGs.DrawImage(_resource_Image[3 + index], new RectangleF(11 + 126 * 0, 97 + (67.7F + 6) * (3 + 2 * 0) + 34 * 0, 124, 32));
            dcGs.DrawString(
                "作业机具锁定",
                Global.Font_Verdana_12,
                Brushes.Black,
                new RectangleF(11 + 126 * 0, 97 + (67.7F + 6) * (3 + 2 * 0) + 34 * 0+3, 124, 32),
                Global.SF_CC
                );
        }

        /// <summary>
        /// 绘制作业冷却风扇状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_ZuoYeLengQueFengShan(Graphics dcGs)
        {
            int index = 0;
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    index = i;
                }
            }
            dcGs.DrawImage(_resource_Image[5 + index], new RectangleF(11 + 126 * 0, 97 + (67.7F + 6) * (3 + 2 * 0) + 34 * 1, 124, 32));
            dcGs.DrawString(
                "作业冷却风扇",
                Global.Font_Verdana_12,
                Brushes.Black,
                new RectangleF(11 + 126 * 0, 97 + (67.7F + 6) * (3 + 2 * 0) + 34 * 1 + 3, 124, 32),
                Global.SF_CC
                );
        }

        /// <summary>
        /// 绘制1水冷却风扇低速状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_ShuiLengQueFengShanDiSu(Graphics dcGs)
        {
            int index = 0;
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    index = i;
                }
            }
            dcGs.DrawImage(_resource_Image[7 + index], new RectangleF(11 + 126 * 1, 97 + (67.7F + 6) * (3 + 2 * 1) + 34 * 0, 124, 32));
            dcGs.DrawString(
                "Ⅰ水冷却风扇低速",
                Global.Font_Verdana_10,
                Brushes.Black,
                new RectangleF(11 + 126 * 1-3, 97 + (67.7F + 6) * (3 + 2 * 1) + 34 * 0 + 3, 130, 32),
                Global.SF_CC
                );
        }

        /// <summary>
        /// 绘制1油冷却风扇状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_YouLengQueFengShan(Graphics dcGs)
        {
            int index = 0;
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[3] + i])
                {
                    index = i;
                }
            }
            dcGs.DrawImage(_resource_Image[9 + index], new RectangleF(11 + 126 * 1, 97 + (67.7F + 6) * (3 + 2 * 1) + 34 * 1, 124, 32));
            dcGs.DrawString(
                "Ⅰ油冷却风扇",
                Global.Font_Verdana_10,
                Brushes.Black,
                new RectangleF(11 + 126 * 1-3, 97 + (67.7F + 6) * (3 + 2 * 1) + 34 * 1 + 3, 130, 32),
                Global.SF_CC
                );
        }
        #endregion

        #endregion
    }
}
