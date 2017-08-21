#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-4
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图102-运行-烟火信息-No.0-主界面
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：视图102-运行-烟火信息-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-4
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V102_Run_Fireworks_C0_MainView : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片资源
        private List<Button> _btns_Control = new List<Button>();//按钮列表
        private RectangleF[] _positions = new RectangleF[28];//烟火探测器位置列表
        private Brush[] _brushs = new Brush[4];//烟火状态列表
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "运行（烟火信息）试图-烟火信息";
        }

        /// <summary>
        /// 初始化函数：初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex">错误索性</param>
        /// <returns>初始化是否成功</returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            //导入图片
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    this._resource_Image.Add(Image.FromStream(fs));
                }
            });

            //组件按钮（火灾报警主机消音, 火灾报警主机复位）
            String[] str = new String[] { "火灾报警主机消音", "火灾报警主机复位" };
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    str[i],
                    new RectangleF(339 + 175 * i, 475, 163, 49),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[1], DownImage = _resource_Image[2]}
                    );
                btn.ClickEvent += btn_ClickEvent;
                this._btns_Control.Add(btn);
            }

            //返回按钮
            String[] str_ = new String[7] { "", "", "", "", "", "", "返回" };
            for (int i = 0; i < 7; i++)
            {
                Button btn_R = new Button(
                    str_[i],
                    new RectangleF(728, 106 + 63 * i, 67, 52),
                    i + 2,//按钮编号+2
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[1], DownImage = _resource_Image[2] }
                    );
                btn_R.ClickEvent += btn_ClickEvent;
                this._btns_Control.Add(btn_R);
            }

            //火灾探测器位置
            this._positions = new RectangleF[]{
                new RectangleF(91,211,20,15),new RectangleF(91,153.5f,20,15),//1,2
                new RectangleF(122,188,20,15),new RectangleF(180,188,20,15),//3,4
                new RectangleF(205,211,20,15),new RectangleF(205,153.5f,20,15),//5,6
                new RectangleF(267,211,20,15),new RectangleF(267,153.5f,20,15),//7,8
                new RectangleF(298,188,20,15),new RectangleF(400,188,20,15),//9,10
                new RectangleF(480,211,20,15),new RectangleF(480,153.5f,20,15),//11,12
                new RectangleF(512,188,20,15),new RectangleF(600,188,20,15),//13,14
                new RectangleF(205,355,20,15),new RectangleF(205,297,20,15),//15,16
                new RectangleF(180,331,20,15),new RectangleF(93,331,20,15),//17,18
                new RectangleF(416,355,20,15),new RectangleF(416,297,20,15),//19,20
                new RectangleF(391,331,20,15),new RectangleF(301,331,20,15),//21,22
                new RectangleF(480,355,20,15),new RectangleF(480,297,20,15),//23,24
                new RectangleF(513,331,20,15),new RectangleF(571,331,20,15),//25,26
                new RectangleF(596,355,20,15),new RectangleF(596,297,20,15)//23,24
            };

            this._brushs = new Brush[4]
            {
                Brushs.GreenBrush,Brushs.RedBrush,Brushs.OrangeBrush,Brushs.GrayBrush
            };

            return true;
        }
        #endregion

        #region 鼠标事件
        /// <summary>
        /// 组件鼠标按下事件监测函数
        /// </summary>
        /// <param name="nPoint">按下点坐标</param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            this._btns_Control.ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        /// <summary>
        /// 组件鼠标弹起事件监测函数
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            this._btns_Control.ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 组件按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0://火灾报警主机消音
                    break;
                case 1://火灾报警主机复位
                    break;
                case 8://返回
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, 1, 0, 0);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            this.paint_Frame(dcGs);
            this.paint_TrainState(dcGs);
            this.paint_SmokeState(dcGs);
            this._btns_Control.ForEach(a => a.Paint(dcGs));

            base.paint(dcGs);
        }

        /// <summary>
        /// 绘制背景
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new Point(0, 96), new Point(800, 96));
            dcGs.DrawImage(_resource_Image[0], new RectangleF(0, 97, 721, 457));
            dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new PointF(722, 99), new PointF(800, 99));
            dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new PointF(722, 99), new PointF(722, 552));
        }

        /// <summary>
        /// 绘制车头状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_TrainState(Graphics dcGs)
        {
            //司机室占用
            for (int i = 0; i < 2; i++)//车头1、2
            {
                for (int j = 0; j < 2; j++)//两个状态
                {
                    if (BoolList[UIObj.InBoolList[0] + j + i * 2])
                        dcGs.DrawImage(_resource_Image[7+j], new RectangleF(21+645*i, 145+141*i, 25, 106));
                }
            }

            //车头方向
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (BoolList[UIObj.InBoolList[1] + j + i * 2])
                        dcGs.DrawImage(_resource_Image[9 + j], new RectangleF(21 + 645 * i, 145 + 141 * i, 25, 106));
                }
            }

            ////TC1
            //if (BoolList[UIObj.InBoolList[0]])
            //{
            //    for (int i = 0; i < 2; i++)
            //    {
            //        if (BoolList[UIObj.InBoolList[1] + i])
            //        {
            //            dcGs.DrawImage(_resource_Image[3], new RectangleF(21, 145, 25, 106));
            //        }
            //        else dcGs.DrawImage(_resource_Image[4], new RectangleF(21, 145, 25, 106));
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < 2; i++)
            //    {
            //        if (BoolList[UIObj.InBoolList[1] + i])
            //        {
            //            dcGs.DrawImage(_resource_Image[5], new RectangleF(21, 145, 25, 106));
            //        }
            //        else dcGs.DrawImage(_resource_Image[6], new RectangleF(21, 145, 25, 106));
            //    }
            //}

            ////TC2
            //if (BoolList[UIObj.InBoolList[0] + 1])
            //{
            //    for (int i = 0; i < 2; i++)
            //    {
            //        if (BoolList[UIObj.InBoolList[2] + i])
            //        {
            //            dcGs.DrawImage(_resource_Image[4], new RectangleF(666, 286, 25, 106));
            //        }
            //        else dcGs.DrawImage(_resource_Image[3], new RectangleF(666, 286, 25, 106));
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < 2; i++)
            //    {
            //        if (BoolList[UIObj.InBoolList[2] + i])
            //        {
            //            dcGs.DrawImage(_resource_Image[6], new RectangleF(666, 286, 25, 106));
            //        }
            //        else dcGs.DrawImage(_resource_Image[5], new RectangleF(666, 286, 25, 106));
            //    }
            //}
        }

        /// <summary>
        /// 绘制火灾探测器状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_SmokeState(Graphics dcGs)
        {
            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (BoolList[UIObj.InBoolList[3] + j + i * 4])
                    {
                        dcGs.FillEllipse(this._brushs[j], this._positions[i]);
                        break;
                    }
                }

                dcGs.DrawString(
                    (i + 1).ToString(),
                    new Font("宋体", 9,
                        FontStyle.Bold),
                        Brushs.WhiteBrush,
                        new RectangleF(this._positions[i].X, this._positions[i].Y + 1, this._positions[i].Width, this._positions[i].Height),
                        FontInfo.SF_CC
                        );
            }
        }
        #endregion
    }
}
