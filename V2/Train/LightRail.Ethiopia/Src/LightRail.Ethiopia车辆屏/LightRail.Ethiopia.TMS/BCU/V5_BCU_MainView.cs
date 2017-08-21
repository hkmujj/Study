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

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using LightRail.Ethiopia.TMS.Control;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace LightRail.Ethiopia.TMS.BCU
{
    /// <summary>
    /// 功能描述：公共组件-No.1-切换视图按钮
    /// 创建人：唐林
    /// 创建时间：2014-7-1
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V5_BCU_MainView : baseClass
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
                new List<object>(){"HM_BackupMode_BCU",null},
                new List<object>(){"BrakeReleased_CAN_BCU",null},
                new List<object>(){"BrakeApplied_CAN_BCU",null},
                new List<object>(){"HB_ON_CAN_BCU",null},
                new List<object>(){"WSP_TC_CAN_BCU",null},
                new List<object>(){"Major_Event_CAN_BCU",null},
                new List<object>(){"Minor_Event_CAN_BCU",null},
                new List<object>(){"load_BCU",null,0},
                new List<object>(){"load_sensor1_BCU",0},
                new List<object>(){"load_sensor2_BCU",0},
                new List<object>(){"iDiagnosisReg1_BCU",0},
                new List<object>(){"iDiagnosisReg2_BCU",0},

                //第二页
                new List<object>(){"iDiagnosisReg3_BCU",0},
                new List<object>(){"iDiagnosisReg4_BCU",0},
            };

            grid = new Grid(
                new RectangleF(62, 150 + 28, 662, 364 - 28),
                new Row[12],
                new Column[2]
                {
                    new Column()
                    {
                        Width = 390,
                        Font = new Font("Verdana", 11),
                        SF=new StringFormat(){Alignment = StringAlignment.Near,LineAlignment = StringAlignment.Center},
                        Brush = Brushes.Yellow,
                        LinePen = new Pen(Brushes.Yellow,1)
                    } ,
                    new Column()
                    {
                        Width = 272,
                        Font = new Font("Verdana", 11),
                        SF=new StringFormat(){Alignment = StringAlignment.Center,LineAlignment = StringAlignment.Center} ,
                        Brush = Brushes.Yellow,
                        LinePen = new Pen(Brushes.Yellow,1)
                    } 
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
            dcGs.DrawImage(_resource_Image[0], new Point(452, 117-16));

            dcGs.DrawLine(new Pen(new SolidBrush(Color.Yellow), 1), new PointF(62, 150), new PointF(724, 150));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.Yellow), 1), new PointF(62, 150), new PointF(62, 150 + 28));

            for (int i = 0; i < 2; i++)
            {
                dcGs.DrawLine(new Pen(new SolidBrush(Color.Yellow), 1), new PointF(452 + 272 * i, 150), new PointF(452 + 272 * i, 150 + 28));
            }

            dcGs.DrawString(
                "Signal",
                new Font("Verdana", 13),
                Brushes.Yellow,
                new RectangleF(62, 150, 390, 28),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );

            dcGs.DrawString(
                "Status",
                new Font("Verdana", 13),
                Brushes.Yellow,
                new RectangleF(452, 150, 272, 28),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );
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
                    if (BoolList[UIObj.InBoolList[0] + i * 2 + j])
                    {
                        image = _resource_Image[j + 5];
                    }
                }
                _values[i][1] = image;
            }

            for (int i = 0; i < UIObj.InFloatList[1]; i++)
            {
                _values[i + 7][1] = FloatList[UIObj.InFloatList[0] + i];
            }
        }
    }
}
