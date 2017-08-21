#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-10
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图704-检修-轮径设置-No.0-主界面
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using NC_TMS.Common;
using ES.Facility.Common.Control.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：视图704-检修-轮径设置-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-10
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V704_Check_WheelRadius_C0 : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片列表
        private List<Button> _btns = new List<Button>();//按钮列表
        private List<Button> _btns_Train = new List<Button>();//车厢按钮列表
        private String _newWheelRadius = String.Empty;//新轮径
        private String _oldWheelRadius = "802";//旧轮径
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "检修试图-轮径设置";
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
                using (FileStream fs = new FileStream(Path.Combine(RecPath , a), FileMode.Open))
                {
                    this._resource_Image.Add(Image.FromStream(fs));
                }
            });

            for (int i = 0; i < 10; i++)
            {
                Button btn = new Button(
                    ((i + 1) % 10).ToString(),
                    new RectangleF(403 + (i % 3 * 102), 187 + (i / 3 * 48), 100, 44),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_13_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1]}
                    );
                btn.ClickEvent += btn_ClickEvent;
                this._btns.Add(btn);
            }

            Button btn_DEL = new Button(
                "DEL",
                new RectangleF(403 + (10 % 3 * 102), 187 + (10 / 3 * 48), 202, 44),
                10,
                new ButtonStyle() { FontStyle = FontStyles.FS_Song_13_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1]}
                );
            btn_DEL.ClickEvent += btn_ClickEvent;
            this._btns.Add(btn_DEL);

            String[] str_ = new String[] { "确定", "取消" };
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    str_[i],
                    new RectangleF(403 + (i * 204), 393, 100, 44),
                    11 + i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_13_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1]}
                    );
                btn.ClickEvent += btn_ClickEvent;
                this._btns.Add(btn);
            }

            String[] str_Train = new String[] { "Tc1", "Mp1", "M1", "M2", "Mp2", "Tc2" };
            for (int i = 0; i < 6; i++)
            {
                Button btn_Train = new Button(
                    str_Train[i],
                    new RectangleF(97 + (i % 3 * 100), 187 + (i / 3 * 48), 98, 44),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_13_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1]},
                    false
                    );
                if (i == 0) btn_Train.IsReplication = false;
                btn_Train.ClickEvent += btn_Train_ClickEvent;
                this._btns_Train.Add(btn_Train);
            }

            return true;
        }
        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            this._btns.ForEach(a => a.MouseDown(nPoint));
            this._btns_Train.ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            this._btns.ForEach(a => a.MouseUp(nPoint));
            this._btns_Train.ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 数字模块按钮响应点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 10://DEL按钮
                    if (this._newWheelRadius.Length == 0)
                        break;
                    this._newWheelRadius = this._newWheelRadius.Substring(0, this._newWheelRadius.Length - 1);
                    break;
                case 11://确定按钮
                    break;
                case 12://取消按钮
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.检修, 0, 0);
                    break;
                default://0-9数字按钮
                    if (this._newWheelRadius.Length == 3)
                        break;
                    this._newWheelRadius += ((e.Message + 1) % 10).ToString();
                    break;
            }
        }

        /// <summary>
        /// 车辆选择按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_Train_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            this._btns_Train.FindAll(a => a.ID != e.Message).ForEach(b => b.IsReplication = true);
            this._newWheelRadius = String.Empty;
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            this._btns.ForEach(a => a.Paint(dcGs));
            this._btns_Train.ForEach(a => a.Paint(dcGs));
            this.paint_SetValue(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 绘制轮径输入值
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_SetValue(Graphics dcGs)
        {
            String[] str = new String[] { "原轮径：", "新轮径：" };
            String[] str_ = new String[] { this._oldWheelRadius, this._newWheelRadius };
            for (int i = 0; i < 2; i++)
            {
                dcGs.DrawString(str[i], new Font("宋体", 10.5f), Brushs.WhiteBrush, new RectangleF(128 + 128 * i, 378, 100, 36), FontInfo.SF_LC);
                dcGs.FillRectangle(Brushs.WhiteBrush, new Rectangle(174 + 126 * i, 377, 56, 36));
                dcGs.DrawString(str_[i], new Font("宋体", 11f), Brushs.BlackBrush, new RectangleF(174 + 126 * i, 377, 56, 36), FontInfo.SF_CC);
            }

        }
        #endregion
    }
}
