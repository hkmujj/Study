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
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using ES.Facility.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using NC_TMS.Common;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：公共组件-No.1-切换视图按钮
    /// 创建人：唐林
    /// 创建时间：2014-7-1
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class VC_C1_ViewChangeButton : baseClass
    {
        #region 私有变量
        private ViewState _currentViewState;                      //当前视图
        private List<Image> _resource_Image = new List<Image>();    //图片资源
        private List<Button> _btns_Down_TabView = new List<Button>();//按钮列表
        #endregion

        #region 框架初始化函数
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
                using (FileStream fs = new FileStream(Path.Combine(RecPath , a), FileMode.Open))
                {
                    this._resource_Image.Add(Image.FromStream(fs));
                }
            });

            String[] _strs_Btn_TabView = new String[7] { "运行", "车辆状态", "空调状态", "事件", "通讯状态", "帮助", "检修" };
            ButtonStyle bs = new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1]};
            for (int i = 0; i < 7; i++)
            {
                Button btn = new Button(
                    _strs_Btn_TabView[i],
                    new Rectangle(115 + i * 97, 555, 90, 40),
                    i,
                    bs,
                    false
                    );
                btn.ClickEvent += btn_ClickEvent;
                this._btns_Down_TabView.Add(btn);
            }
            return true;
        }

        /// <summary>
        /// 获取到当前运行视图：根据当前视图设置按钮状态
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                this._currentViewState = (ViewState)nParaB;
                switch (this._currentViewState)
                {
                    case ViewState.故障:
                        this._btns_Down_TabView[(Int32)ViewState.故障 - 1].IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != (Int32)ViewState.故障 - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.检修:
                        this._btns_Down_TabView[(Int32)ViewState.检修 - 1].IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != (Int32)ViewState.检修 - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.故障提示:
                        this._btns_Down_TabView[(Int32)ViewState.运行 - 1].IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != (Int32)ViewState.运行 - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = true;
                        break;
                    case ViewState.火灾重启:
                        this._btns_Down_TabView[(Int32)ViewState.运行 - 1].IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != (Int32)ViewState.运行 - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = true;
                        break;
                    case ViewState.紧急广播:
                        this._btns_Down_TabView[(Int32)ViewState.运行 - 1].IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != (Int32)ViewState.运行 - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.站点:
                        this._btns_Down_TabView[(Int32)ViewState.运行 - 1].IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != (Int32)ViewState.运行 - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.空调_温度控制:
                        this._btns_Down_TabView[(Int32)ViewState.空调_模式选择 - 1].IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != (Int32)ViewState.空调_模式选择 - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = true;
                        break;
                    case ViewState.登陆:
                        this._btns_Down_TabView[(Int32)ViewState.检修 - 1].IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != (Int32)ViewState.检修 - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.时间设置:
                        this._btns_Down_TabView[(Int32)ViewState.检修 - 1].IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != (Int32)ViewState.检修 - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.密码设置:
                        this._btns_Down_TabView[(Int32)ViewState.检修 - 1].IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != (Int32)ViewState.检修 - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.轮径设置:
                        this._btns_Down_TabView[(Int32)ViewState.检修 - 1].IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != (Int32)ViewState.检修 - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.车号设置:
                        this._btns_Down_TabView[(Int32)ViewState.检修 - 1].IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != (Int32)ViewState.检修 - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.加速度测试:
                        this._btns_Down_TabView[(Int32)ViewState.检修 - 1].IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != (Int32)ViewState.检修 - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.测试:
                        this._btns_Down_TabView[(Int32)ViewState.检修 - 1].IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != (Int32)ViewState.检修 - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.数据记录:
                        this._btns_Down_TabView[(Int32)ViewState.检修 - 1].IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != (Int32)ViewState.检修 - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.运行帮助:
                        this._btns_Down_TabView.Find(a => a.ID == 6 - 1).IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != 6 - 1).ForEach(b => b.IsReplication = true);
                        break;
                    case ViewState.车辆帮助:
                        this._btns_Down_TabView.Find(a => a.ID == 6 - 1).IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != 6 - 1).ForEach(b => b.IsReplication = true);
                        break;
                    case ViewState.空调帮助:
                        this._btns_Down_TabView.Find(a => a.ID == 6 - 1).IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != 6 - 1).ForEach(b => b.IsReplication = true);
                        break;
                    case ViewState.通讯帮助:
                        this._btns_Down_TabView.Find(a => a.ID == 6 - 1).IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != 6 - 1).ForEach(b => b.IsReplication = true);
                        break;
                    case ViewState.提示信息_方向列表:
                        this._btns_Down_TabView[(Int32)ViewState.运行 - 1].IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != (Int32)ViewState.运行 - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = true;
                        break;
                    case ViewState.提示信息_列车逻辑:
                        this._btns_Down_TabView[(Int32)ViewState.运行 - 1].IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != (Int32)ViewState.运行 - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = true;
                        break;
                    case ViewState.提示信息_旁路信息:
                        this._btns_Down_TabView[(Int32)ViewState.运行 - 1].IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != (Int32)ViewState.运行 - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = true;
                        break;
                    case ViewState.提示信息_限速条件:
                        this._btns_Down_TabView[(Int32)ViewState.运行 - 1].IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != (Int32)ViewState.运行 - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = true;
                        break;
                    default:
                        if (this._btns_Down_TabView.Find(a => a.ID == nParaB - 1) != null)
                            this._btns_Down_TabView.Find(a => a.ID == nParaB - 1).IsReplication = false;
                        this._btns_Down_TabView.FindAll(a => a.ID != nParaB - 1).ForEach(b => b.IsReplication = true);
                        this._btns_Down_TabView[(Int32)ViewState.帮助 - 1].Enable = true;
                        break;
                }
            }
        }
        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            this._btns_Down_TabView.ToList().ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            this._btns_Down_TabView.ToList().ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (Convert.ToInt32(((Int32)this._currentViewState).ToString().Substring(0, 1)) == e.Message + 1)
            {
                (this._btns_Down_TabView.Find(a=>a.ID==e.Message)).IsReplication = false;
                return;
            }//点击的为同一个视图，函数返回

            switch ((ViewState)(e.Message + 1))
            {
                case ViewState.检修://点击检修按钮，进入登陆视图
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.登陆, 0, 0);
                    break;
                case ViewState.帮助://根据当前视图跳转到相应的帮助视图
                    String temp = ((Int32)this._currentViewState).ToString();
                    if (temp.StartsWith("1")) append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.运行帮助, 0, 0);
                    else if (temp.StartsWith("2")) append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.车辆帮助, 0, 0);
                    else if (temp.StartsWith("3")) append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.空调帮助, 0, 0);
                    else if (temp.StartsWith("5")) append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.通讯帮助, 0, 0);
                    break;
                default://切换到相应视图
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, e.Message + 1, 0, 0);
                    break;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(0, 552), new PointF(800, 552));
            this._btns_Down_TabView.ToList().ForEach(a => a.Paint(dcGs));
            base.paint(dcGs);
        }
        #endregion
    }
}
