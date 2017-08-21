#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-3
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图301-空调-No.0-温度设置
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
using NC_TMS.Common;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：视图301-空调-No.0-温度设置
    /// 创建人：唐林
    /// 创建时间：2014-7-3
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V301_AirCondition_C2_TemperatureControl : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//乳品资源
        private List<Button> _btn_Trains = new List<Button>();//车厢按钮列表
        private List<Button> _btn_AdjustTemperature = new List<Button>();//温度上下调节按钮列表
        private Button _btn_OK;//设定按钮
        private Group _group;//“组”控件
        private Int32 _temperature = 18;//设定温度
        #endregion

        public static float[] Temperatures = new float[6];

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "空调状态视图-温度控制";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath ,a), FileMode.Open))
                {
                    this._resource_Image.Add(Image.FromStream(fs));
                }
            });

            //设定温度的车辆按钮
            String[] str_Train = new String[] { "全车", "Tc1", "Mp1", "M1", "M2", "Mp2", "Tc2" };
            for (int i = 0; i < 7; i++)
            {
                Button btn_Train = new Button(
                    str_Train[i],
                    new RectangleF(77 + 85 * i - 5, 402, 77, 42),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_10_CC_W, Background = _resource_Image[0], DownImage = _resource_Image[1]},
                    false
                    );
                btn_Train.ClickEvent += btn_Train_ClickEvent;
                this._btn_Trains.Add(btn_Train);
            }

            //温度调节按钮
            for (int i = 0; i < 2; i++)
            {
                Button btn_Adjust = new Button(
                    "",
                    new RectangleF(157 + 85 * (i + 1), 457, 77, 42),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_10_CC_W, Background = _resource_Image[4 + i * 2], DownImage = _resource_Image[5 + i * 2] }
                    );
                btn_Adjust.ClickEvent += btn_Adjust_ClickEvent;
                this._btn_AdjustTemperature.Add(btn_Adjust);
            }

            //设定按钮
            this._btn_OK = new Button(
                "设定",
                new RectangleF(77 + 85 * 6 - 5, 457, 77, 42),
                0,
                new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[2], DownImage = _resource_Image[3] }
                );
            this._btn_OK.ClickEvent += _btn_OK_ClickEvent;

            this._group = new Group("空调温度", new Font("宋体", 11), new Rectangle(12, 377, 688, 147));

            return true;
        }
        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            this._btn_Trains.ForEach(a => a.MouseDown(nPoint));
            this._btn_AdjustTemperature.ForEach(a => a.MouseDown(nPoint));
            this._btn_OK.MouseDown(nPoint);

            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            this._btn_Trains.ForEach(a => a.MouseUp(nPoint));
            this._btn_AdjustTemperature.ForEach(a => a.MouseUp(nPoint));
            this._btn_OK.MouseUp(nPoint);
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 车厢按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_Train_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            this._btn_Trains.FindAll(a => a.ID != e.Message).ForEach(b => b.IsReplication = true);
        }

        /// <summary>
        /// 设定按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _btn_OK_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            Button btn = this._btn_Trains.Find(a => !a.IsReplication);
            if (btn == null)
                return;

            if (btn.ID == 0)
                for (int i = 0; i < 6; i++)
                {
                    Temperatures[i] = _temperature;
                }
            else Temperatures[btn.ID - 1] = _temperature;
        }

        /// <summary>
        /// 温度调节按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_Adjust_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0:
                    if (this._temperature > 18) this._temperature--;
                    break;
                case 1:
                    if (this._temperature < 28) this._temperature++;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new Point(0, 96), new Point(800, 96));
            dcGs.DrawString("选择\n车厢", new Font("宋体", 10f), Brushs.WhiteBrush, new RectangleF(12, 405, 60, 42), FontInfo.SF_CC);
            dcGs.DrawString("温度\n设定", new Font("宋体", 10f), Brushs.WhiteBrush, new RectangleF(12, 460, 60, 42), FontInfo.SF_CC);

            this._btn_Trains.ForEach(a => a.Paint(dcGs));
            this._btn_AdjustTemperature.ForEach(a => a.Paint(dcGs));
            this._btn_OK.Paint(dcGs);
            this._group.Paint(dcGs);
            this.paint_TemperatureData(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 温度绘制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_TemperatureData(Graphics dcGs)
        {
            dcGs.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(157, 457, 77, 42));
            dcGs.DrawString(_temperature.ToString(), new Font("UnidreamLED", 34), Brushs.WhiteBrush, new Rectangle(157, 459, 77, 42), FontInfo.SF_CC);
        }
        #endregion
    }
}
