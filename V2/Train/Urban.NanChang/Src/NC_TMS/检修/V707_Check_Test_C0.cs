#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-10
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图707-检修-测试-No.0-主界面
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
using ES.Facility.Common;
using ES.Facility.Common.Control.Common;
using ES.Facility.Common.Control;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：视图707-检修-测试-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-10
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V707_Check_Test_C0 : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片列表
        private List<Button> _btns = new List<Button>();//按钮列表
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "检修试图-测试";
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

            String[] str = new String[]{"  分主断","  电制动\n  切除","  高速度","  2车VVVF\n  隔离","  3车VVVF\n  隔离","  4车VVVF\n  隔离","  5车VVVF\n  隔离","  保持制动切除",
                                      "  制动电阻冷却\n  风机启动","  备用_10","  备用_11","  备用_12","  备用_13","  备用_14","  备用_15","  备用_16"};
            for (int i = 0; i < 16; i++)
            {
                Button btn = new Button(
                    str[i],
                    new RectangleF(143 + (i % 4 * 116), 140 + (i / 4 * 65), 101, 60),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_10_LC_B, Background = _resource_Image[0], DownImage = _resource_Image[1]}
                    );
                btn.ClickEvent += btn_ClickEvent;
                this._btns.Add(btn);
            }

            String[] str_ = new String[] { "", "", "", "", "返回" };
            for (int i = 0; i < 5; i++)
            {
                Button btn = new Button(
                    str_[i],
                    new RectangleF(721, 120 + 76 * i, 75, 60),
                    i + 16,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[2], DownImage = _resource_Image[3] }
                    );
                btn.ClickEvent += btn_ClickEvent;
                this._btns.Add(btn);
            }
            return true;
        }
        #endregion

        #region 鼠标消息
        public override bool mouseDown(Point nPoint)
        {
            this._btns.ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            this._btns.ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 响应鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 20:
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.检修, 0, 0);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            this._btns.ForEach(a => a.Paint(dcGs));
            this.paint_Frame(dcGs);

            base.paint(dcGs);
        }

        public void paint_Frame(Graphics dcGs)
        {
            dcGs.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(4, 96, 711, 454));
            dcGs.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(716, 96, 85, 454));
        }
        #endregion
    }
}
