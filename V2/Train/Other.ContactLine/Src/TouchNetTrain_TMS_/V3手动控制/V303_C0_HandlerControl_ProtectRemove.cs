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
    /// 保护解除
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V303_C0_HandlerControl_ProtectRemove : baseClass
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
            return "手动控制-保护解除";
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

            String[] _strs = { "转向架解锁","作业机具锁定", "顶窗锁定", 
                                 "空转保护", "分动箱离齿", "车齿箱离齿", 
                                 "转向架锁紧","分动箱合齿", "车齿箱合齿",
                                 };
            for (int i = 0; i < 6; i++)
            {
                Button btn = new Button(
                    _strs[i],
                    new RectangleF(162 + 53.5F, 110 + 103 + 10 + (7 + 42) * i, 202, 42),
                    i,
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
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            for (int i = 0; i < 3; i++)
            {
                Button btn = new Button(
                    _strs[i + 6],
                    new RectangleF(162 + 53.5F+309, 110 + 103 + 15 + (10 + 42) * i, 202, 42),
                    i + 6,
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
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

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
            dcGs.DrawLine(
                Global.Pen_White_2,
                new Point(162, 213),
                new Point(780, 213)
                );
            //中间竖线
            dcGs.DrawLine(
                Global.Pen_White_2,
                new PointF(471, 110),
                new PointF(471, 520)
                );

            //第一排文字
            String[] strs = { "牵引运行\n保护及联锁解除", "作业工况\n保护及联锁解除" };
            for (int i = 0; i < 2; i++)
            {
                dcGs.DrawString(
                    strs[i],
                    Global.Font_Verdana_12,
                    Brushes.White,
                    new RectangleF(162 + 309 * i, 110, 309, 103),
                    Global.SF_CC
                    );
            }
        }

        #endregion
    }
}
