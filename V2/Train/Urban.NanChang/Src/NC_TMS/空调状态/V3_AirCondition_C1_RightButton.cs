#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-3
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图3-空调-No.1-右侧菜单按钮
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
    /// 功能描述：视图3-空调-No.1-右侧菜单按钮
    /// 创建人：唐林
    /// 创建时间：2014-7-3
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V3_AirCondition_C1_RightButton : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片资源
        private List<Button> _btn_Menu_ = new List<Button>();//按钮列表
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "空调状态视图-右侧按钮";
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

            String[] str = new String[] { "空调\n控制", "空调\n温度", "", "", "" };
            for (int i = 0; i < 5; i++)
            {
                Button btn = new Button(
                    str[i],
                    new RectangleF(720, 116 + 75 * i, 78, 60),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1]},
                    false
                    );
                btn.ClickEvent += btn_ClickEvent;
                this._btn_Menu_.Add(btn);
            }
            this._btn_Menu_[0].IsReplication = false;
            return true;
        }

        /// <summary>
        /// 根据当前视图确定按钮状态
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                if ((ViewState)nParaB == ViewState.空调_温度控制)
                {
                    this._btn_Menu_[0].IsReplication = true;
                    this._btn_Menu_[1].IsReplication = false;
                }
            }
        }
        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            this._btn_Menu_.ForEach(a => a.MouseDown(nPoint));

            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            this._btn_Menu_.ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 菜单按钮点击事件响应函数：切换相应视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            this._btn_Menu_.FindAll(a => a.ID != e.Message).ForEach(a => a.IsReplication = true);//改变按钮状态

            if (e.Message == 0) append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, 3, 0, 0);//进入空调控制视图
            else if (e.Message == 1) append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, 301, 0, 0);//进入温度设置视图
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            this._btn_Menu_.ForEach(a => a.Paint(dcGs));

            dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new Point(715, 96), new Point(715, 552));

            base.paint(dcGs);
        }
        #endregion
    }
}
