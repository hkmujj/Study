#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-21
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
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.HeFei.TMS.Common;

namespace Urban.HeFei.TMS.CommonPart
{
    /// <summary>
    /// 功能描述：公共组件-No.1-切换视图按钮
    /// 创建人：lih
    /// 创建时间：2015-08-05
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class VcC1ViewChangeButton : baseClass
    {
        #region 私有变量
        private  ViewState m_CurrentViewState;                      //当前视图
        private List<Image> m_ResourceImage = new List<Image>();    //图片资源
        private List<Button> m_BtnsDownTabView = new List<Button>();//按钮列表

        private SolidBrush m_BackgrounpSolidBrush;
        private Rectangle m_BackgrounpRectangle;
        private bool m_FlagSet = false;

        private ButtonStyle m_NormalBs;
        private ButtonStyle m_WarningBs;
        private ButtonStyle m_FaultBs;
        private int m_BtnIndex = 0;
        private bool[] m_BtnsFlag;
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
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "VC_C1_ViewChangeButton";
        //}

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });

            var strsBtnTabView = new string[8] { "主界面", "牵引", "制动", "辅助", "空调", "PIS", "历史" ,"维护"};
            m_NormalBs = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[1] };

            m_WarningBs = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[2], DownImage = m_ResourceImage[3] };

            m_FaultBs = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[4], DownImage = m_ResourceImage[5] };
            m_BtnsFlag = new bool[8] { false, false, false, false, false, false, false, false };
            for (var i = 0; i < strsBtnTabView.Length; i++)
            {
                var btn = new Button(
                    strsBtnTabView[i],
                    new Rectangle(10+(int)(i * 98.75), 516, 88, 84),
                    i,
                    m_NormalBs,
                    false
                    );
                btn.ClickEvent += btn_ClickEvent;
                m_BtnsDownTabView.Add(btn);
            }

            m_BackgrounpSolidBrush = new SolidBrush(Color.FromArgb(215, 215, 215));
            m_BackgrounpRectangle = new Rectangle(0, 516, 800, 84);

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

                if (CommonStatus.IsBlackScreen == true)
                {
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + CommonStatus.SdBoolBaseNumber, 1, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1] + CommonStatus.SdBoolBaseNumber, 1, 0);
                    CommonStatus.IsBlackScreen = false;
                }

                m_CurrentViewState = (ViewState)nParaB;
                CommonStatus.CurrentViewState = m_CurrentViewState;

                if (CommonStatus.InterfaceNameDicts.ContainsKey(CommonStatus.CurrentViewState))
                {
                    CommonStatus.CurrentInterfaceName = CommonStatus.InterfaceNameDicts[CommonStatus.CurrentViewState];
                }

                
                if (ViewState.BypassInfo2==m_CurrentViewState       //旁路
                    || ViewState.BypassInfo == m_CurrentViewState   //旁路
                    || ViewState.BrakePage2 == m_CurrentViewState   //制动
                    || ViewState.BrakePage3 == m_CurrentViewState   //制动
                    || ViewState.AssistPage2 == m_CurrentViewState  //辅助
                    || ViewState.AirConditionerPage2 == m_CurrentViewState  //空调
                    || (ViewState.PISSemiAutoModel <= m_CurrentViewState && m_CurrentViewState <= ViewState.PISSpecialInfo) //PIS
                    || (ViewState.MtTrainInfo<=m_CurrentViewState && m_CurrentViewState<=ViewState.MtAdjustBrightness)    //维护
                   )//这些视图切换放在各自的buttonClick事件中处理
                {
                    return;
                }

                switch (m_CurrentViewState)//目前只处理导航项的视图切换
                {
                    default:
                        if (m_BtnsDownTabView.Find(a => a.ID == nParaB - 1) != null)
                            m_BtnsDownTabView.Find(a => a.ID == nParaB - 1).IsReplication = false;
                        m_BtnsDownTabView.FindAll(a => a.ID != nParaB - 1).ForEach(b => b.IsReplication = true);
                        break;
                }
            }
        }
        #endregion

        #region 鼠标事件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            //判断点击的位置是否属于该button集合
            for (var i = 0; i < m_BtnsDownTabView.Count; i++)
            {
                if((nPoint.X>=m_BtnsDownTabView[i].Rect.X) 
                    && (nPoint.X<=m_BtnsDownTabView[i].Rect.X+m_BtnsDownTabView[i].Rect.Width)
                    && (nPoint.Y >= m_BtnsDownTabView[i].Rect.Y)
                    && (nPoint.Y <= m_BtnsDownTabView[i].Rect.Y + m_BtnsDownTabView[i].Rect.Height))
                {
                    m_BtnsDownTabView[i].MouseDown(nPoint);
                    break;
                }
            }

            return base.mouseDown(nPoint);
        }

        /// <summary>
        /// mouseUp
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            for (var i = 0; i < m_BtnsDownTabView.Count; i++)
            {
                if((nPoint.X>=m_BtnsDownTabView[i].Rect.X) 
                    && (nPoint.X<=m_BtnsDownTabView[i].Rect.X+m_BtnsDownTabView[i].Rect.Width)
                    && (nPoint.Y >= m_BtnsDownTabView[i].Rect.Y)
                    && (nPoint.Y <= m_BtnsDownTabView[i].Rect.Y + m_BtnsDownTabView[i].Rect.Height))
                {
                    m_BtnsDownTabView[i].MouseUp(nPoint);
                    break;
                }
            }

            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (Convert.ToInt32(((int)m_CurrentViewState).ToString().Substring(0, 1)) == e.Message + 1&&
                m_CurrentViewState!= ViewState.FaultInfo&&
                m_CurrentViewState!= ViewState.BypassInfo)
            {
                (m_BtnsDownTabView.Find(a=>a.ID==e.Message)).IsReplication = false;
                return;
            }//点击的为同一个视图，函数返回

            switch ((ViewState)(e.Message + 1))
            {
                //case ViewState.检修://点击检修按钮，进入登陆视图
                //    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.登陆, 0, 0);
                //    break;
                //case ViewState.帮助://根据当前视图跳转到相应的帮助视图
                //    String temp = ((Int32)this._currentViewState).ToString();
                //    if (temp.StartsWith("1")) append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.运行帮助, 0, 0);
                //    else if (temp.StartsWith("2")) append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.车辆帮助, 0, 0);
                //    else if (temp.StartsWith("3")) append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.空调帮助, 0, 0);
                //    else if (temp.StartsWith("4")) append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.通讯帮助, 0, 0);
                //    break;
                default://切换到相应视图
                    append_postCmd(CmdType.ChangePage, e.Message + 1, 0, 0);
                    break;
            }
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.FillRectangle(m_BackgrounpSolidBrush,m_BackgrounpRectangle);
            for(m_BtnIndex=0;m_BtnIndex<8;m_BtnIndex++)
            {
                m_BtnsFlag[m_BtnIndex]=false;
            }
            
            for (m_BtnIndex = 0; m_BtnIndex < 2; m_BtnIndex++)
            {
                if (BoolList[UIObj.InBoolList[0] + m_BtnIndex])
                {
                    m_BtnsFlag[0] = true;
                    m_BtnsDownTabView[0].Style = (m_BtnIndex == 0) ? m_WarningBs : m_FaultBs;
                    m_BtnsDownTabView[0].Paint(dcGs);
                }
                if (BoolList[UIObj.InBoolList[2] + m_BtnIndex])
                {
                    m_BtnsFlag[1] = true;
                    m_BtnsDownTabView[1].Style = (m_BtnIndex == 0) ? m_WarningBs : m_FaultBs;
                    m_BtnsDownTabView[1].Paint(dcGs);
                }
                if (BoolList[UIObj.InBoolList[4] + m_BtnIndex])
                {
                    m_BtnsFlag[2] = true;
                    m_BtnsDownTabView[2].Style = (m_BtnIndex == 0) ? m_WarningBs : m_FaultBs;
                    m_BtnsDownTabView[2].Paint(dcGs);
                }
                if (BoolList[UIObj.InBoolList[6] + m_BtnIndex])
                {
                    m_BtnsFlag[3] = true;
                    m_BtnsDownTabView[3].Style = (m_BtnIndex == 0) ? m_WarningBs : m_FaultBs;
                    m_BtnsDownTabView[3].Paint(dcGs);
                }
                if (BoolList[UIObj.InBoolList[8] + m_BtnIndex])
                {
                    m_BtnsFlag[4] = true;
                    m_BtnsDownTabView[4].Style = (m_BtnIndex == 0) ? m_WarningBs : m_FaultBs;
                    m_BtnsDownTabView[4].Paint(dcGs);
                }
                if (BoolList[UIObj.InBoolList[10] + m_BtnIndex])
                {
                    m_BtnsFlag[5] = true;
                    m_BtnsDownTabView[5].Style = (m_BtnIndex == 0) ? m_WarningBs : m_FaultBs;
                    m_BtnsDownTabView[5].Paint(dcGs);
                }
                if (BoolList[UIObj.InBoolList[12] + m_BtnIndex])
                {
                    m_BtnsFlag[6] = true;
                    m_BtnsDownTabView[6].Style = (m_BtnIndex == 0) ? m_WarningBs : m_FaultBs;
                    m_BtnsDownTabView[6].Paint(dcGs);
                }
                if (BoolList[UIObj.InBoolList[14] + m_BtnIndex])
                {
                    m_BtnsFlag[7] = true;
                    m_BtnsDownTabView[7].Style = (m_BtnIndex == 0) ? m_WarningBs : m_FaultBs;
                    m_BtnsDownTabView[7].Paint(dcGs);
                }
            }

            for (m_BtnIndex = 0; m_BtnIndex < 8; m_BtnIndex++)
            {
                if (!m_BtnsFlag[m_BtnIndex])
                {
                    m_BtnsDownTabView[m_BtnIndex].Style = m_NormalBs;
                    m_BtnsDownTabView[m_BtnIndex].Paint(dcGs);
                }
            }

               

            base.paint(dcGs);
        }
        #endregion
    }
}
