#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-25
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图602-PIS-半自动
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
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.HeFei.TMS.Common;
using Urban.HeFei.TMS.Resource;

namespace Urban.HeFei.TMS.PIS
{
    /// <summary>
    /// 功能描述：视图602-PIS-半自动
    /// 创建人：lih
    /// 创建时间：2015-8-25
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V602PISSemiAutoModel : baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Button[] m_Btns;//按钮列表

        private Rectangle m_FrameRect = new Rectangle(76, 112, 566, 301);
        private Rectangle m_FillRect1 = new Rectangle(113, 142, 493, 242);
        private Font m_ChineseFont = new Font("宋体", 18);
        private Rectangle[] m_Rect1Strs;
        private string[] m_Str1S;
        private int m_I = 0;
        private Rectangle[] m_ShowInfoFrameRects;

        private Rectangle[] m_InfoRects;

        private string[] m_BtnNameStrs;
        private string m_ModelName = "自动";

        private int m_IndexFlag = 0;

        private string m_NextStationStr;//下一站名
        private string m_EndStationStr;//终点站名
        private int m_NextStationIndex = 0, m_EndStationIndex = 0;//下一站终点站Index
        private string[] m_StopModelStrs;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "PIS-半自动模式";
        }


        /// <summary>
        /// 初始化函数：初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex">错误索性</param>
        /// <returns>初始化是否成功</returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_Rect1Strs = new Rectangle[4];
            m_Rect1Strs[0] = new Rectangle(107, 173, 100, 30);
            m_Rect1Strs[1] = new Rectangle(107, 251, 100, 30);
            m_Rect1Strs[2] = new Rectangle(107, 289, 100, 30);
            m_Rect1Strs[3] = new Rectangle(107, 328, 100, 30);

            m_InfoRects = new Rectangle[4];
            m_InfoRects[0] = new Rectangle(215, 173, 360, 30);
            m_InfoRects[1] = new Rectangle(215, 251, 360, 30);
            m_InfoRects[2] = new Rectangle(215, 289, 360, 30);
            m_InfoRects[3] = new Rectangle(215, 328, 360, 30);

            m_Str1S = new string[4] { "模式", "下一站", "终点站", "停靠侧" };

            m_ShowInfoFrameRects = new Rectangle[2];
            m_ShowInfoFrameRects[0] = new Rectangle(215, 162, 360, 57);
            m_ShowInfoFrameRects[1] = new Rectangle(215, 241, 360, 131);

            m_BtnNameStrs = new string[3] { "修改", "半自动", "手动" };
            m_StopModelStrs = new string[] { "左", "右", "左右" };

            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });

            var bs1 = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[1], DownImage = m_ResourceImage[2] };
            m_Btns = new Button[3];
            m_Btns[0] = new Button(m_BtnNameStrs[0], new Rectangle(150, 431, 84, 50), (int)ViewState.PISRouteSet, bs1, true);
            m_Btns[1] = new Button(m_BtnNameStrs[1], new Rectangle(300, 431, 84, 50), (int)ViewState.PISSemiAutoModel, bs1, true);
            m_Btns[2] = new Button(m_BtnNameStrs[2], new Rectangle(400, 431, 84, 50), (int)ViewState.PISManualModel, bs1, true);
            m_Btns[0].ClickEvent += V602_PIS_SemiModel_ClickEvent;
            m_Btns[1].ClickEvent += V602_PIS_SemiModel_ClickEvent;
            m_Btns[2].ClickEvent += V602_PIS_SemiModel_ClickEvent;
            ChangedButton((int)ViewState.PISAutoModel);
            V603PISManualModel.ModelChanged += (args) => { ChangedButton(args); };
            return true;
        }
        #endregion

        #region 鼠标事件
        /// <summary>
        /// mouseDown
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {

            m_Btns.ToList().Where(f => f.Rect.Contains(nPoint)).ToList().ForEach(f => f.MouseDown(nPoint));

            return base.mouseDown(nPoint);
        }

        private void ChangedButton(int index, bool isCurrent = false)
        {
            var tmp = (ViewState)index;
            switch (tmp)
            {
                case ViewState.PISAutoModel:
                    m_Btns[1].Text = "半自动";
                    m_Btns[1].SetID((int)ViewState.PISSemiAutoModel);
                    m_Btns[2].Text = "手动";
                    m_Btns[2].SetID((int)ViewState.PISManualModel);
                    m_ModelName = "自动";
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.报站模式自动), 1, 0);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.报站模式半自动), 0, 0);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.报站模式手动), 0, 0);
                    break;
                case ViewState.PISManualModel:

                    if (!isCurrent)
                    {
                        m_Btns[1].Text = "半自动";
                        m_Btns[1].SetID((int)ViewState.PISSemiAutoModel);
                        m_Btns[2].Text = "自动";
                        m_Btns[2].SetID((int)ViewState.PISAutoModel);
                        m_ModelName = "手动";
                    }
                    append_postCmd(CmdType.ChangePage, (int)ViewState.PISManualModel, 0, 0);
                    break;
                case ViewState.PISSemiAutoModel:
                    m_Btns[1].Text = "自动";
                    m_Btns[1].SetID((int)ViewState.PISAutoModel);
                    m_Btns[2].Text = "手动";
                    m_Btns[2].SetID((int)ViewState.PISManualModel);
                    m_ModelName = "半自动";
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.报站模式自动), 0, 0);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.报站模式半自动), 1, 0);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.报站模式手动), 0, 0);
                    break;
                case ViewState.PISRouteSet:
                    append_postCmd(CmdType.ChangePage, (int)ViewState.PISManualModel, 0, 0);
                    break;
            }
        }

        /// <summary>
        /// mouseUp
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            m_Btns.ToList().Where(f => f.Rect.Contains(nPoint)).ToList().ForEach(f => f.MouseUp(nPoint));

            return base.mouseUp(nPoint);
        }


        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void V602_PIS_SemiModel_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            ChangedButton(e.Message, true);
        }



        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            // OnSendData();
            dcGs.DrawImage(m_ResourceImage[0], m_FrameRect);
            dcGs.FillRectangle(Brushs.BlackBrush, m_FillRect1);

            for (m_I = 0; m_I < m_Rect1Strs.Length; m_I++)
            {
                dcGs.DrawString(m_Str1S[m_I], m_ChineseFont, Brushs.WhiteBrush, m_Rect1Strs[m_I], FontInfo.SfRc);
            }


            m_NextStationIndex = (int)GetInFloatValue(InFloatKeys.PIS下一站);
            m_EndStationIndex = (int)GetInFloatValue(InFloatKeys.PIS终点站);
            m_NextStationStr = GetStation(m_NextStationIndex) ?? "";
            m_EndStationStr = GetStation(m_EndStationIndex) ?? "";

            dcGs.DrawImage(m_ResourceImage[3], m_ShowInfoFrameRects[0]);
            dcGs.DrawImage(m_ResourceImage[4], m_ShowInfoFrameRects[1]);

            dcGs.DrawString(m_NextStationStr, m_ChineseFont, Brushs.BlackBrush, m_InfoRects[1], FontInfo.SfLc);
            dcGs.DrawString(m_EndStationStr, m_ChineseFont, Brushs.BlackBrush, m_InfoRects[2], FontInfo.SfLc);
            dcGs.DrawString(GetStopModelString((int)GetInFloatValue(InFloatKeys.PIS停靠侧)), m_ChineseFont, Brushs.BlackBrush, m_InfoRects[3], FontInfo.SfLc);
            dcGs.DrawString(m_ModelName, m_ChineseFont, Brushs.BlackBrush, m_InfoRects[0], FontInfo.SfLc);

            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                m_Btns[m_I].Paint(dcGs);
            }
            base.paint(dcGs);
        }

        private string GetStopModelString(int index)
        {
            if (index - 1 >= 0 && index <= m_StopModelStrs.Length)
            {
                return m_StopModelStrs[index - 1];
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取站台信息
        /// </summary>
        /// <param name="stationNum"></param>
        /// <returns></returns>
        private string GetStation(int stationNum)
        {
            var stationName = string.Empty;
            CommonStatus.StationList.TryGetValue(stationNum, out stationName);
            return stationName;
        }
        #endregion
    }
}
