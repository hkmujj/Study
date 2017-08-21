using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace TouchNetTrain_TMS_.运行信息
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1_C2_Run_Right : baseClass
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
            return "运行信息-右侧信息";
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

            String[] _strs = new String[10] { "传动箱Ⅰ充油", "传动箱Ⅱ充油", "传动箱Ⅰ前进", "传动箱Ⅱ前进", "离齿", "转向架解锁", "停车制动", "停车制动隔离", "Ⅱ水冷却风扇低速", "Ⅱ油冷却风扇" };
            #region 4个大按钮

            //for (int j = 0; j < 4; j++)
            //{
            //    for (int i = 0; i < 2; i++)
            //    {
            //        Button btn = new Button(
            //            _strs[i + j * 2],
            //            new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * j + 34 * i, 124, 32),
            //            i,
            //            new ButtonStyle()
            //            {
            //                FontStyle = new FontStyle_ES()
            //                {
            //                    Font = Global.Font_Verdana_12_B,
            //                    TextBrush = new SolidBrush(Color.Black),
            //                    StringFormat =
            //                        new StringFormat()
            //                        {
            //                            Alignment = StringAlignment.Center,
            //                            LineAlignment = StringAlignment.Center
            //                        }
            //                },
            //                Background = _resource_Image[1 + j * 2],
            //                DownImage = _resource_Image[2 + j * 2]
            //            }
            //            );
            //        btn.MouseDownEvent += btn_MouseDownEvent;
            //        btn.ClickEvent += btn_ClickEvent;
            //        this._btns.Add(btn);
            //    }
            //}

            #endregion

            #region 2个小按钮
            //for (int i = 0; i < 2; i++)
            //{
            //    Button btn = new Button(
            //        _strs[i + 2 * 4],
            //        new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 5 + 34 * i, 124, 32),
            //        i + 2 * 4,
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
            //            Background = _resource_Image[1 + 4 * 2 + i * 2],
            //            DownImage = _resource_Image[2 + 4 * 2 + i * 2]
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
            //this._btns.ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            //this._btns.ForEach(a => a.MouseUp(nPoint));
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
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            //_btns.ForEach(a => a.Paint(dcGs));
            paint_ZhuanSu(dcGs);
            paint_YouYa(dcGs);
            paint_ShuiWen(dcGs);
            paint_ShuiWen_C(dcGs);
            paint_ShuiWen_J(dcGs);
            paint_ChuanDongXiang1ChouYou(dcGs);
            paint_ChuanDongXiang1QianJin(dcGs);
            paint_ChuanDongXiang2ChouYou(dcGs);
            paint_ChuanDongXiang2QianJin(dcGs);
            paint_LiChi(dcGs);
            paint_ZhuanXiangJiaJieSuo(dcGs);
            paint_TingCheZhiDong(dcGs);
            paint_TingCheZhiDongGeLi(dcGs);
            paint_ShuiLengQueFengShanDiSu(dcGs);
            paint_YouLengQueFengShan(dcGs);

            base.paint(dcGs);
        }

        #region 状态绘制
        /// <summary>
        /// 绘制转速
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_ZhuanSu(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[0], new Point(531, 97));
            dcGs.DrawString(
                "柴油机Ⅱ转速",
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(531, 97 + 34, 125, 34),
                Global.SF_CC
                );
            dcGs.DrawString(
                FloatList[UIObj.InFloatList[0]].ToString("0"),
                Global.Font_Verdana_13,
                Brushes.White,
                new Rectangle(531 + 5, 97 + 8, 70, 29),
                Global.SF_CC
                );

            dcGs.DrawString(
                "r/min",
                Global.Font_Verdana_12,
                Brushes.White,
                new Rectangle(531 + 5, 97 + 8, 115, 29),
                Global.SF_FC
                );
        }

        /// <summary>
        /// 绘制油压
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_YouYa(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[0], new PointF(531, 97 + (67.7F + 6) * 1));
            dcGs.DrawString(
                "柴油机Ⅱ油压",
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(531, 97 + (67.7F + 6) * 1 + 34, 125, 34),
                Global.SF_CC
                );
            dcGs.DrawString(
                FloatList[UIObj.InFloatList[1]].ToString("0"),
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(531 + 5, 97 + (67.7F + 6) * 1 + 8, 70, 29),
                Global.SF_CC
                );

            dcGs.DrawString(
                "kPa",
                Global.Font_Verdana_12,
                Brushes.White,
                new RectangleF(531 + 5, 97 + (67.7F + 6) * 1 + 8, 115, 29),
                Global.SF_FC
                );
        }

        /// <summary>
        /// 绘制水温
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_ShuiWen(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[0], new PointF(531, 97 + (67.7F + 6) * 2));
            dcGs.DrawString(
                "柴油机Ⅱ水温",
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(531, 97 + (67.7F + 6) * 2 + 34, 125, 34),
                Global.SF_CC
                );
            dcGs.DrawString(
                FloatList[UIObj.InFloatList[2]].ToString("0"),
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(531 + 5, 97 + (67.7F + 6) * 2 + 8, 70, 29),
                Global.SF_CC
                );

            dcGs.DrawString(
                "℃",
                Global.Font_Verdana_12,
                Brushes.White,
                new RectangleF(531 + 5, 97 + (67.7F + 6) * 2 + 8, 115, 29),
                Global.SF_FC
                );
        }

        /// <summary>
        /// 绘制油压
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_ShuiWen_C(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[0], new PointF(531, 97 + (67.7F + 6) * 3));
            dcGs.DrawString(
                "传动箱Ⅱ油温",
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(531, 97 + (67.7F + 6) * 3 + 34, 125, 34),
                Global.SF_CC
                );
            dcGs.DrawString(
                FloatList[UIObj.InFloatList[3]].ToString("0"),
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(531 + 5, 97 + (67.7F + 6) * 3 + 8, 70, 29),
                Global.SF_CC
                );

            dcGs.DrawString(
                "℃",
                Global.Font_Verdana_12,
                Brushes.White,
                new RectangleF(531 + 5, 97 + (67.7F + 6) * 3 + 8, 115, 29),
                Global.SF_FC
                );
        }

        /// <summary>
        /// 绘制Ⅱ静液压油温
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_ShuiWen_J(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[0], new PointF(531 + 125 + 7, 97 + (67.7F + 6) * 4));
            dcGs.DrawString(
                "Ⅱ静液压油温",
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 4 + 34, 130, 34),
                Global.SF_CC
                );
            dcGs.DrawString(
                FloatList[UIObj.InFloatList[4]].ToString("0"),
                Global.Font_Verdana_13,
                Brushes.White,
                new RectangleF(531 + 125 + 7 + 5, 97 + (67.7F + 6) * 4 + 8, 70, 29),
                Global.SF_CC
                );

            dcGs.DrawString(
                "℃",
                Global.Font_Verdana_12,
                Brushes.White,
                new RectangleF(531 + 125 + 7 + 5, 97 + (67.7F + 6) * 4 + 8, 115, 29),
                Global.SF_FC
                );
        }
        #endregion

        #region 状态绘制（Image）

        #region 传动箱充油
        /// <summary>
        /// 传动箱1充油状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_ChuanDongXiang1ChouYou(Graphics dcGs)
        {
            int index = 0;
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    index = i;
                }
            }
            dcGs.DrawImage(_resource_Image[1 + index], new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 0 + 34 * 0, 124, 32));
            dcGs.DrawString(
                "传动箱Ⅰ充油",
                Global.Font_Verdana_12,
                Brushes.Black,
                new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 0 + 34 * 0+3, 124, 32),
                Global.SF_CC
                );
        }
        /// <summary>
        /// 传动箱2充油状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_ChuanDongXiang2ChouYou(Graphics dcGs)
        {
            int index = 0;
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    index = i;
                }
            }
            dcGs.DrawImage(_resource_Image[1 + index], new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 0 + 34 * 1, 124, 32));
            dcGs.DrawString(
                "传动箱Ⅱ充油",
                Global.Font_Verdana_12,
                Brushes.Black,
                new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 0 + 34 * 1 + 3, 124, 32),
                Global.SF_CC
                );
        }
        #endregion

        #region 传动箱前进
        /// <summary>
        /// 传动箱1前进状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_ChuanDongXiang1QianJin(Graphics dcGs)
        {
            String[] strs = {"传动箱Ⅰ前进", "传动箱Ⅰ中立", "传动箱Ⅰ前进"};
            int index = 0;
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    index = i;
                }
            }
            dcGs.DrawImage(_resource_Image[3 + index], new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 1 + 34 * 0, 124, 32));
            dcGs.DrawString(
                strs[index],
                Global.Font_Verdana_12,
                Brushes.Black,
                new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 1 + 34 * 0 + 3, 124, 32),
                Global.SF_CC
                );
        }
        /// <summary>
        /// 传动箱2前进状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_ChuanDongXiang2QianJin(Graphics dcGs)
        {
            String[] strs = { "传动箱Ⅱ前进", "传动箱Ⅱ中立", "传动箱Ⅱ前进" };
            int index = 0;
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[3] + i])
                {
                    index = i;
                }
            }
            dcGs.DrawImage(_resource_Image[3 + index], new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 1 + 34 * 1, 124, 32));
            dcGs.DrawString(
                strs[index],
                Global.Font_Verdana_12,
                Brushes.Black,
                new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 1 + 34 * 1 + 3, 124, 32),
                Global.SF_CC
                );
        }
        #endregion

        #region 离齿
        /// <summary>
        /// 离齿状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_LiChi(Graphics dcGs)
        {
            int index = 0;
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[4] + i])
                {
                    index = i;
                }
            }
            dcGs.DrawImage(_resource_Image[6 + index], new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 2 + 34 * 0, 124, 32));
            dcGs.DrawString(
                "离齿",
                Global.Font_Verdana_12,
                Brushes.Black,
                new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 2 + 34 * 0 + 3, 124, 32),
                Global.SF_CC
                );
        }
        /// <summary>
        /// 转向架解锁状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_ZhuanXiangJiaJieSuo(Graphics dcGs)
        {
            int index = 0;
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[5] + i])
                {
                    index = i;
                }
            }
            dcGs.DrawImage(_resource_Image[6 + index], new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 2 + 34 * 1, 124, 32));
            dcGs.DrawString(
                "转向架解锁",
                Global.Font_Verdana_12,
                Brushes.Black,
                new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 2 + 34 * 1 + 3, 124, 32),
                Global.SF_CC
                );
        }
        #endregion

        #region 停车制动
        /// <summary>
        /// 停车制动状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_TingCheZhiDong(Graphics dcGs)
        {
            int index = 0;
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[6] + i])
                {
                    index = i;
                }
            }
            dcGs.DrawImage(_resource_Image[8 + index], new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 3 + 34 * 0, 124, 32));
            dcGs.DrawString(
                "停车制动",
                Global.Font_Verdana_12,
                Brushes.Black,
                new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 3 + 34 * 0 + 3, 124, 32),
                Global.SF_CC
                );
        }
        /// <summary>
        /// 停车制动隔离状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_TingCheZhiDongGeLi(Graphics dcGs)
        {
            int index = 0;
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[7] + i])
                {
                    index = i;
                }
            }
            dcGs.DrawImage(_resource_Image[8 + index], new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 3 + 34 * 1, 124, 32));
            dcGs.DrawString(
                "停车制动隔离",
                Global.Font_Verdana_12,
                Brushes.Black,
                new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 3 + 34 * 1 + 3, 124, 32),
                Global.SF_CC
                );
        }
        #endregion

        #region 2冷却风扇低速
        /// <summary>
        /// Ⅱ水冷却风扇低速状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_ShuiLengQueFengShanDiSu(Graphics dcGs)
        {
            int index = 0;
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[8] + i])
                {
                    index = i;
                }
            }
            dcGs.DrawImage(_resource_Image[10 + index], new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 5 + 34 * 0, 124, 32));
            dcGs.DrawString(
                "Ⅱ水冷却风扇低速",
                Global.Font_Verdana_10,
                Brushes.Black,
                new RectangleF(531 + 125 + 7-3, 97 + (67.7F + 6) * 5 + 34 * 0 + 3, 130, 32),
                Global.SF_CC
                );
        }
        /// <summary>
        /// 2油冷却风扇状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_YouLengQueFengShan(Graphics dcGs)
        {
            int index = 0;
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[9] + i])
                {
                    index = i;
                }
            }
            dcGs.DrawImage(_resource_Image[10 + index], new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 5 + 34 * 1, 124, 32));
            dcGs.DrawString(
                "Ⅱ油冷却风扇",
                Global.Font_Verdana_10,
                Brushes.Black,
                new RectangleF(531 + 125 + 7, 97 + (67.7F + 6) * 5 + 34 * 1 + 3, 124, 32),
                Global.SF_CC
                );
        }
        #endregion
        #endregion

        #endregion
    }
}
