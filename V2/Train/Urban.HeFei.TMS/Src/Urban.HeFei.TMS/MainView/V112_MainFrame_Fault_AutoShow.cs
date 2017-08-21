#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-21
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图111-故障-No.1-自动显示界面
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
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;
using Urban.HeFei.TMS.Common;
using Urban.HeFei.TMS.Help;
using FaultInfo = Urban.HeFei.TMS.Help.FaultInfo;

namespace Urban.HeFei.TMS.MainView
{
    /// <summary>
    ///  功能描述：视图111-故障-No.1-自动显示界面
    /// 创建人：lih
    /// 创建时间：2015-8-21
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V112MainFrameFaultAutoShow : baseClass
    {
        #region 私有变量
        private ListBox<FaultInfoTms> m_ListBox;//列表控件
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);

        private Font m_ChineseFont = new Font("宋体", 22);

        private string[] m_FrameStrs;
        private Rectangle[] m_FrameStrRects;

        private List<FaultInfo> m_Infos = new List<FaultInfo>();

        private Rectangle m_FrameRect = new Rectangle(71, 142, 658, 300);
        private int m_LastViewIndex = -1;
        private Button m_ReturnArrowBtn;
        private SolidBrush m_BackgrounpSolidBrush;
        private Rectangle m_BackgrounpRectangle;
        private int m_I = 0;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "故障-自动显示界面";
        }


        /// <summary>
        /// 初始化函数：导入图片、创建控件、列表框
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });


            m_FrameStrs = new string[4] { "车号:", "", "", "" };
            m_FrameStrRects = new Rectangle[4];
            m_FrameStrRects[0] = new Rectangle(260, 213, 90, 33);
            m_FrameStrRects[1] = new Rectangle(340, 204, 38, 46);
            m_FrameStrRects[2] = new Rectangle(120, 262, 530, 46);
            m_FrameStrRects[3] = new Rectangle(120, 310, 530, 46);

            m_BackgrounpSolidBrush = new SolidBrush(Color.FromArgb(215, 215, 215));
            m_BackgrounpRectangle = new Rectangle(0, 516, 800, 84);

            var bsResult = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[1] };
            m_ReturnArrowBtn = new Button("确定", new Rectangle(324, 530, 151, 46), (int)ViewState.FiRtConfirmBtn, bsResult, true);

            m_ReturnArrowBtn.ClickEvent += _ReturnArrowBtn_ClickEvent;
            FaultListManager.FaultInfoChanged += FaultListManager_FaultInfoChanged;
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                m_LastViewIndex = (int)nParaC;
            }
            base.setRunValue(nParaA, nParaB, nParaC);
        }

        private void FaultListManager_FaultInfoChanged(FaultInfo arg1, int arg2)
        {
            if (arg1.Level != 10)
            {
                return;
            }
            if (arg2 == 1)
            {
                m_Infos.Add(arg1);
                DataChanged();
            }
            else
            {
                m_Infos.Remove(m_Infos.FirstOrDefault(f => f.LogicIndex == arg1.LogicIndex));
                DataChanged();
            }
        }

        private void DataChanged()
        {
            if (m_Infos.Count > 0)
            {
                append_postCmd(CmdType.ChangePage, (int)ViewState.FaultInfoRealTime, 0, 0);
                var fault = m_Infos[m_Infos.Count - 1];
                m_FrameStrs[1] = fault.CarNum;

                var tmp = fault.FaultName.Split('#');
                if (tmp.Length == 2)
                {
                    m_FrameStrs[2] = tmp[0];
                    m_FrameStrs[3] = tmp[1];
                }
                else if (tmp.Length == 1)
                {
                    m_FrameStrs[2] = tmp[0];
                    m_FrameStrs[3] = string.Empty;
                }

                m_CurrenInfo = fault;
            }
            else
            {
                if (m_LastViewIndex != -1)
                {
                    append_postCmd(CmdType.ChangePage, m_LastViewIndex, 0, 0);
                }

            }
        }
        #endregion

        #region 鼠标事件
        /// <summary>
        /// mouseUp
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            if ((nPoint.X >= m_ReturnArrowBtn.Rect.X)
                   && (nPoint.X <= m_ReturnArrowBtn.Rect.X + m_ReturnArrowBtn.Rect.Width)
                   && (nPoint.Y >= m_ReturnArrowBtn.Rect.Y)
                   && (nPoint.Y <= m_ReturnArrowBtn.Rect.Y + m_ReturnArrowBtn.Rect.Height))
            {
                m_ReturnArrowBtn.MouseUp(nPoint);
            }
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// mouseDown
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            if ((nPoint.X >= m_ReturnArrowBtn.Rect.X)
                      && (nPoint.X <= m_ReturnArrowBtn.Rect.X + m_ReturnArrowBtn.Rect.Width)
                      && (nPoint.Y >= m_ReturnArrowBtn.Rect.Y)
                      && (nPoint.Y <= m_ReturnArrowBtn.Rect.Y + m_ReturnArrowBtn.Rect.Height))
            {
                m_ReturnArrowBtn.MouseDown(nPoint);
            }
            return base.mouseDown(nPoint);
        }

        private FaultInfo m_CurrenInfo;
        /// <summary>
        /// _ReturnArrowBtn_ClickEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _ReturnArrowBtn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (e.Message == (int)ViewState.FiRtConfirmBtn)
            {
                m_Infos.Remove(m_CurrenInfo);
                DataChanged();
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
            if (m_Infos.Count > 0)
            {
                dcGs.FillRectangle(Brushs.BackGroundBrush, 0, 90, 800, 506);
                dcGs.FillRectangle(m_BackgrounpSolidBrush, m_BackgrounpRectangle);
                m_ReturnArrowBtn.Paint(dcGs);
                dcGs.DrawImage(m_ResourceImage[2], m_FrameRect);
                dcGs.DrawString(m_FrameStrs[0], m_ChineseFont, Brushs.BlackBrush, m_FrameStrRects[0], FontInfo.SfLc);
                for (m_I = 1; m_I < m_FrameStrRects.Length; m_I++)
                {
                    dcGs.DrawString(m_FrameStrs[m_I], m_ChineseFont, Brushs.RedBrush, m_FrameStrRects[m_I], FontInfo.SfCc);
                }

            }


            base.paint(dcGs);
        }
        #endregion
    }
}
