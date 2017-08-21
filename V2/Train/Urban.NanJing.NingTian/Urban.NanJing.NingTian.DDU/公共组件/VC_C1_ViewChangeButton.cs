#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-21
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
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using Urban.NanJing.NingTian.DDU.Common;

namespace Urban.NanJing.NingTian.DDU.公共组件
{
    /// <summary>
    /// 功能描述：公共组件-No.1-切换视图按钮
    /// 创建人：唐林
    /// 创建时间：2014-7-21
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
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
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Image.Add(Image.FromStream(fs));
                }
            });

            string[] _strs_Btn_TabView = new string[7] { "运行", "参数", "空调", "通讯", "PIS", "帮助", "检修" };
            ButtonStyle bs = new ButtonStyle() { FontStyle = FontStyles.FS_Song_20_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1], DisableImage=_resource_Image[2] };
            for (int i = 0; i < 7; i++)
            {
                Button btn = new Button(
                    _strs_Btn_TabView[i],
                    new Rectangle(2+i * 114, 535, 113, 62),
                    i,
                    bs,
                    false
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns_Down_TabView.Add(btn);
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
                _currentViewState = (ViewState)nParaB;
                switch (_currentViewState)
                {
                    case ViewState.检修:
                        _btns_Down_TabView[(int)ViewState.检修 - 1].IsReplication = false;
                        _btns_Down_TabView.FindAll(a => a.ID != (int)ViewState.检修 - 1).ForEach(b => b.IsReplication = true);
                        _btns_Down_TabView[(int)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.PIS:
                        _btns_Down_TabView[(int)ViewState.PIS - 1].IsReplication = false;
                        _btns_Down_TabView.FindAll(a => a.ID != (int)ViewState.PIS - 1).ForEach(b => b.IsReplication = true);
                        _btns_Down_TabView[(int)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.紧急广播:
                        _btns_Down_TabView[(int)ViewState.PIS - 1].IsReplication = false;
                        _btns_Down_TabView.FindAll(a => a.ID != (int)ViewState.PIS - 1).ForEach(b => b.IsReplication = true);
                        _btns_Down_TabView[(int)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.站点:
                        _btns_Down_TabView[(int)ViewState.PIS - 1].IsReplication = false;
                        _btns_Down_TabView.FindAll(a => a.ID != (int)ViewState.PIS - 1).ForEach(b => b.IsReplication = true);
                        _btns_Down_TabView[(int)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.登陆:
                        _btns_Down_TabView[(int)ViewState.检修 - 1].IsReplication = false;
                        _btns_Down_TabView.FindAll(a => a.ID != (int)ViewState.检修 - 1).ForEach(b => b.IsReplication = true);
                        _btns_Down_TabView[(int)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.时间设置:
                        _btns_Down_TabView[(int)ViewState.检修 - 1].IsReplication = false;
                        _btns_Down_TabView.FindAll(a => a.ID != (int)ViewState.检修 - 1).ForEach(b => b.IsReplication = true);
                        _btns_Down_TabView[(int)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.密码设置:
                        _btns_Down_TabView[(int)ViewState.检修 - 1].IsReplication = false;
                        _btns_Down_TabView.FindAll(a => a.ID != (int)ViewState.检修 - 1).ForEach(b => b.IsReplication = true);
                        _btns_Down_TabView[(int)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.轮径设置:
                        _btns_Down_TabView[(int)ViewState.检修 - 1].IsReplication = false;
                        _btns_Down_TabView.FindAll(a => a.ID != (int)ViewState.检修 - 1).ForEach(b => b.IsReplication = true);
                        _btns_Down_TabView[(int)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.加速度测试:
                        _btns_Down_TabView[(int)ViewState.检修 - 1].IsReplication = false;
                        _btns_Down_TabView.FindAll(a => a.ID != (int)ViewState.检修 - 1).ForEach(b => b.IsReplication = true);
                        _btns_Down_TabView[(int)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.测试:
                        _btns_Down_TabView[(int)ViewState.检修 - 1].IsReplication = false;
                        _btns_Down_TabView.FindAll(a => a.ID != (int)ViewState.检修 - 1).ForEach(b => b.IsReplication = true);
                        _btns_Down_TabView[(int)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.数据记录:
                        _btns_Down_TabView[(int)ViewState.检修 - 1].IsReplication = false;
                        _btns_Down_TabView.FindAll(a => a.ID != (int)ViewState.检修 - 1).ForEach(b => b.IsReplication = true);
                        _btns_Down_TabView[(int)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.运行帮助:
                        _btns_Down_TabView.Find(a => a.ID == 6 - 1).IsReplication = false;
                        _btns_Down_TabView.FindAll(a => a.ID != 6 - 1).ForEach(b => b.IsReplication = true);
                        break;
                    case ViewState.车辆帮助:
                        _btns_Down_TabView.Find(a => a.ID == 6 - 1).IsReplication = false;
                        _btns_Down_TabView.FindAll(a => a.ID != 6 - 1).ForEach(b => b.IsReplication = true);
                        break;
                    case ViewState.空调帮助:
                        _btns_Down_TabView.Find(a => a.ID == 6 - 1).IsReplication = false;
                        _btns_Down_TabView.FindAll(a => a.ID != 6 - 1).ForEach(b => b.IsReplication = true);
                        break;
                    case ViewState.通讯帮助:
                        _btns_Down_TabView.Find(a => a.ID == 6 - 1).IsReplication = false;
                        _btns_Down_TabView.FindAll(a => a.ID != 6 - 1).ForEach(b => b.IsReplication = true);
                        break;
                    case ViewState.盘路信息:

                        //this._btns_Down_TabView[(Int32)ViewState.运行 - 1].IsReplication = false;
                        _btns_Down_TabView.ForEach(b => b.IsReplication = true);
                        _btns_Down_TabView[(int)ViewState.帮助 - 1].Enable = false;
                        break;
                    case ViewState.故障:
                        //this._btns_Down_TabView[(Int32)ViewState.运行 - 1].IsReplication = false;
                        _btns_Down_TabView.ForEach(b => b.IsReplication = true);
                        _btns_Down_TabView[(int)ViewState.帮助 - 1].Enable = true;
                        break;
                    default:
                        if (_btns_Down_TabView.Find(a => a.ID == nParaB - 1) != null)
                            _btns_Down_TabView.Find(a => a.ID == nParaB - 1).IsReplication = false;
                        _btns_Down_TabView.FindAll(a => a.ID != nParaB - 1).ForEach(b => b.IsReplication = true);
                        _btns_Down_TabView[(int)ViewState.帮助 - 1].Enable = true;
                        break;
                }
            }
        }
        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            _btns_Down_TabView.ToList().ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btns_Down_TabView.ToList().ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (Convert.ToInt32(((int)_currentViewState).ToString().Substring(0, 1)) == e.Message + 1&&
                _currentViewState!= ViewState.故障&&
                _currentViewState!= ViewState.盘路信息)
            {
                (_btns_Down_TabView.Find(a=>a.ID==e.Message)).IsReplication = false;
                return;
            }//点击的为同一个视图，函数返回

            switch ((ViewState)(e.Message + 1))
            {
                case ViewState.检修://点击检修按钮，进入登陆视图
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)ViewState.登陆, 0, 0);
                    break;
                case ViewState.帮助://根据当前视图跳转到相应的帮助视图
                    string temp = ((int)_currentViewState).ToString();
                    if (temp.StartsWith("1")) append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)ViewState.运行帮助, 0, 0);
                    else if (temp.StartsWith("2")) append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)ViewState.车辆帮助, 0, 0);
                    else if (temp.StartsWith("3")) append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)ViewState.空调帮助, 0, 0);
                    else if (temp.StartsWith("4")) append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)ViewState.通讯帮助, 0, 0);
                    break;
                default://切换到相应视图
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, e.Message + 1, 0, 0);
                    break;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 533, 800, 65));
            _btns_Down_TabView.ToList().ForEach(a => a.Paint(g));
            base.paint(g);
        }
        #endregion
    }
}
