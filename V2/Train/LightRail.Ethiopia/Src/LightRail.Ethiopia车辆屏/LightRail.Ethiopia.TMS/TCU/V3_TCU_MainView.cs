#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-1
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：公共组件-No.1-切换视图按钮
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using LightRail.Ethiopia.TMS.Common;
using LightRail.Ethiopia.TMS.Control;

namespace LightRail.Ethiopia.TMS.TCU
{
    /// <summary>
    /// 功能描述：公共组件-No.1-切换视图按钮
    /// 创建人：唐林
    /// 创建时间：2014-7-1
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V3_TCU_MainView : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();    //图片资源
        private Grid grid;
        private List<List<object>> _values = new List<List<object>>();
        #endregion

        #region 框架函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共试图-视图切换按钮";
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

            _values = new List<List<object>>()
            {
                //第一页
                new List<object>(){"safety brake",null,null,null,null},
                new List<object>(){"traction enable",null,null,null,null},
                new List<object>(){"all friction brakes released",null,null,null,null},
                new List<object>(){"brake overtake",null,null,null,null},
                new List<object>(){"forced brake",null,null,null,null},
                new List<object>(){"charging contactor state",null,null,null,null},
                new List<object>(){"main contactor state",null,null,null,null},
                new List<object>(){"VVVF state",null,null,null,null},
                new List<object>(){"drive enable",null,null,null,null},
                new List<object>(){"sanding recommend",null,null,null,null},

                //第二页
                //new List<object>(){"hardware overvoltage",null,null,null,null},
                //new List<object>(){"hardware line overcurrent",null,null,null,null},
                new List<object>(){"traction inverter 1 warning",null,null,null,null},
                new List<object>(){"traction inverter 1 fault",null,null,null,null},
                new List<object>(){"traction inverter 1 working",null,null,null,null},
                new List<object>(){"brake chopper 1 working",null,null,null,null},
                new List<object>(){"main contactor 1 closed",null,null,null,null},
                new List<object>(){"motor 1 slide",null,null,null,null},
                new List<object>(){"motor 1 slip",null,null,null,null},
                new List<object>(){"cooling fan OK",null,null,null,null},

                //第三页
                new List<object>(){"slip frequency",0,0,0,0},
                new List<object>(){"stator frequency",0,0,0,0},
                new List<object>(){"DynaCurrent_TCU",0,0,0,0},
                new List<object>(){"OutputVoltage_TCU",0,0,0,0},
                new List<object>(){"InputVoltage_TCU",0,0,0,0},
                new List<object>(){"output_Freq",0,0,0,0},
                new List<object>(){"Net Current",0,0,0,0},
                new List<object>(){"iFaultReg1_TCU",0,0,0,0},
                new List<object>(){"iFaultReg2_TCU",0,0,0,0},
            };

            grid = new Grid(
                new RectangleF(62, 206, 701, 304),
                new Row[10],
                new Column[5]
                {
                    new Column()
                    {
                        Width = 328,
                        Font = new Font("Verdana", 11),
                        SF=new StringFormat(){Alignment = StringAlignment.Near,LineAlignment = StringAlignment.Center},
                        Brush = Brushes.Yellow,
                        LinePen = new Pen(Color.Yellow)
                    } ,
                    new Column()
                    {
                        Width = 93.25F,
                        Font = new Font("Verdana", 11),
                        SF=new StringFormat(){Alignment = StringAlignment.Center,LineAlignment = StringAlignment.Center} ,
                        Brush = Brushes.Yellow,
                        LinePen = new Pen(Color.Yellow)
                    } ,
                    new Column()
                    {
                        Width = 93.25F,
                        Font = new Font("Verdana", 11),
                        SF=new StringFormat(){Alignment = StringAlignment.Center,LineAlignment = StringAlignment.Center} ,
                        Brush = Brushes.Yellow,
                        LinePen = new Pen(Color.Yellow)
                    } ,
                    new Column()
                    {
                        Width = 93.25F,
                        Font = new Font("Verdana", 11),
                        SF=new StringFormat(){Alignment = StringAlignment.Center,LineAlignment = StringAlignment.Center},
                        Brush = Brushes.Yellow,
                        LinePen = new Pen(Color.Yellow)
                    } ,
                    new Column()
                    {
                        Width = 93.25F,
                        Font = new Font("Verdana", 11),
                        SF=new StringFormat(){Alignment = StringAlignment.Center,LineAlignment = StringAlignment.Center} ,
                        Brush = Brushes.Yellow,
                        LinePen = new Pen(Color.Yellow)
                    } ,
                },
                _values,
                new PageButton()
                {
                    Rect = new Rectangle(62 + 80 * 0, 90, 70, 45),
                    UpImage = _resource_Image[1 + 0 * 2],
                    DownImage = _resource_Image[1 + 0 * 2 + 1],
                    DisableImage = _resource_Image[1 + 0 * 2 + 1],
                }, 
                new PageButton()
                {
                    Rect = new Rectangle(62 + 80 * 1, 90, 70, 45),
                    UpImage = _resource_Image[1 + 1 * 2],
                    DownImage = _resource_Image[1 + 1 * 2 + 1],
                    DisableImage = _resource_Image[1 + 1 * 2 + 1],
                }
                );
            return true;
        }

        public override bool mouseDown(Point nPoint)
        {
            grid.MouseDown(nPoint);

            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            grid.MouseUp(nPoint);

            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            getValue();
            grid.Paint(dcGs, new Rectangle(62 + 80 * 2, 90, 70, 45));

            paint_Frame(dcGs);


            base.paint(dcGs);
        }
        #endregion

        /// <summary>
        /// 主要框架绘制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[0], new Point(390, 117));

            dcGs.DrawLine(new Pen(new SolidBrush(Color.Yellow), 1), new PointF(62, 150), new PointF(763, 150));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.Yellow), 1), new PointF(390, 150 + 28), new PointF(763, 150 + 28));

            dcGs.DrawLine(new Pen(new SolidBrush(Color.Yellow), 1), new PointF(62, 150), new PointF(62, 150 + 28 + 28));
            for (int i = 0; i < 5; i++)
            {
                dcGs.DrawLine(new Pen(new SolidBrush(Color.Yellow), 1), new PointF(390 + 93.25F * i, 150 + 28 * (i % 2)), new PointF(390 + 93.25F * i, 150 + 28 + 28));
            }

            dcGs.DrawString(
                "Signal",
                new Font("Verdana", 20),
                Brushes.Yellow,
                new RectangleF(62, 150, 328, 56),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );

            String[] str1 = new[] { "Mc1", "Mc2" };
            for (int i = 0; i < 2; i++)
            {
                dcGs.DrawString(
                    str1[i],
                    new Font("Verdana", 13),
                    Brushes.Yellow,
                    new RectangleF(390 + 93.25F * 2 * i, 150, 93.25F * 2, 28),
                    new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                    );
            }

            String[] str2 = new[] { "MSR1", "MSR2", "MSR1", "MSR2" };
            for (int i = 0; i < 4; i++)
            {
                dcGs.DrawString(
                    str2[i],
                    new Font("Verdana", 13),
                    Brushes.Yellow,
                    new RectangleF(390 + 93.25F * i, 150+28, 93.25F, 28),
                    new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                    );
            }
        }

        /// <summary>
        /// 获取网络数据（由三个位置数据表示：第一个位置：起始接口编号；第二个位置：包括条目数目；第三个位置：每个条目的状态数目）
        /// </summary>
        private void getValue()
        {
            for (int i = 0; i < UIObj.InBoolList[1]; i++)//条目数
            {
                Image image = null;
                for (int j = 0; j < UIObj.InBoolList[2]; j++)//状态数
                {
                    if (BoolList[UIObj.InBoolList[0] + i * UIObj.InBoolList[2] + j])
                    {
                        image = _resource_Image[j + 5];
                    }
                }
                _values[i / 4][i % 4 + 1] = image;
            }

            for (int i = 0; i < UIObj.InFloatList[1]; i++)
            {
                _values[i/4+18][i%4 + 1] = FloatList[UIObj.InFloatList[0] + i];
            }
        }
    }
}
