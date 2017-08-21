#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：维护-No.0-主界面
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

namespace Urban.HeFei.TMS.Maintenance
{
    /// <summary>
    /// 功能描述：维护-No.0-主界面
    /// 创建人：lih
    /// 创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V8MaintenanceC0MainView : baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Font m_ChineseFont = new Font("宋体", 16);
        private Button[] m_Btns;//按钮列表
        private string[] m_BtnNames;
        private int m_I = 0;
        private Rectangle[] m_FrameRects;
        private string[] m_FrameStrs;
        private Rectangle[] m_FrameStrRects;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "维护-主界面";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V8_Maintenance_C0_MainView";
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
                using (var fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });

            m_FrameStrs = new string[3] { "信息与状态", "试验", "设置" };
            m_BtnNames = new string[10] { "列车信息", "网络状态", "软件版本", "接口检查", "能耗信息", "加速度测试", "制动自检", "亮度调节", "时间设置", "系统复位" };

            m_FrameRects = new Rectangle[3];
            for (m_I = 0; m_I < m_FrameRects.Length; m_I++)
            {
                m_FrameRects[m_I] = new Rectangle(68, 143 + m_I * 114, 632, 70);
            }
            m_FrameStrRects = new Rectangle[3];
            for (m_I = 0; m_I < m_FrameStrRects.Length; m_I++)
            {
                m_FrameStrRects[m_I] = new Rectangle(95, 118 + m_I * 114, 120, 25);
            }

            var bs1 = new ButtonStyle() { FontStyle = FontStyles.FsSong14CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[0] };
            m_Btns = new Button[10];
            for (m_I = 0; m_I < 5; m_I++)
            {
                m_Btns[m_I] = new Button(m_BtnNames[m_I], new RectangleF(90 + m_I * 120, 155, 113, 45), (int)(ViewState.MtTrainInfo + m_I), bs1, true);
            }

            m_Btns[5] = new Button(m_BtnNames[5], new RectangleF(90, 269, 113, 45), (int)ViewState.MtAccelerationTest, bs1, true);
            m_Btns[6] = new Button(m_BtnNames[6], new RectangleF(210, 269, 113, 45), (int)ViewState.MtBrakeSelfChecking, bs1, true);

            m_Btns[7] = new Button(m_BtnNames[7], new RectangleF(90, 383, 113, 45), (int)ViewState.MtPasswordSetting, bs1, true);
            m_Btns[8] = new Button(m_BtnNames[8], new RectangleF(210, 383, 113, 45), (int)ViewState.MtAdjustBrightness, bs1, true);
            m_Btns[9] = new Button(m_BtnNames[9], new RectangleF(330, 269, 113, 45), (int)ViewState.SystemReset, bs1, true);
            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                m_Btns[m_I].ClickEvent += V8_Maintenance_C0_MainView_ClickEvent;
            }

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
            //判断点击的位置是否属于该button集合
            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                if ((nPoint.X >= m_Btns[m_I].Rect.X)
                       && (nPoint.X <= m_Btns[m_I].Rect.X + m_Btns[m_I].Rect.Width)
                       && (nPoint.Y >= m_Btns[m_I].Rect.Y)
                       && (nPoint.Y <= m_Btns[m_I].Rect.Y + m_Btns[m_I].Rect.Height))
                {
                    m_Btns[m_I].MouseDown(nPoint);
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
            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                if ((nPoint.X >= m_Btns[m_I].Rect.X)
                       && (nPoint.X <= m_Btns[m_I].Rect.X + m_Btns[m_I].Rect.Width)
                       && (nPoint.Y >= m_Btns[m_I].Rect.Y)
                       && (nPoint.Y <= m_Btns[m_I].Rect.Y + m_Btns[m_I].Rect.Height))
                {
                    m_Btns[m_I].MouseUp(nPoint);
                    break;
                }
            }
            return base.mouseUp(nPoint);
        }

      
        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void V8_Maintenance_C0_MainView_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            ViewState tempViewState;
            if (!Enum.TryParse(e.Message.ToString(), out tempViewState))
            {
                return;
            }
            if ((ViewState.MtTrainInfo <= tempViewState)
                && (tempViewState <= ViewState.SystemReset))
            {
                if ((ViewState)e.Message < ViewState.MtAccelerationTest)
                {
                    append_postCmd(CmdType.ChangePage, e.Message, 0, 0);
                    CommonStatus.CurrentViewState = tempViewState;
                    CommonStatus.CurrentInterfaceName = CommonStatus.InterfaceNameDicts[CommonStatus.CurrentViewState];
                }
                else
                {
                    PasswordKeyInput.Index = e.Message;
                    append_postCmd(CmdType.ChangePage, (int)ViewState.PawsswordSetView, 0, 0);
                }

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
            for (m_I = 0; m_I < m_FrameRects.Length; m_I++)
            {
                dcGs.DrawImage(m_ResourceImage[1], m_FrameRects[m_I]);
            }

            for (m_I = 0; m_I < m_FrameStrRects.Length; m_I++)
            {
                dcGs.DrawString(m_FrameStrs[m_I], m_ChineseFont, Brushs.BlackBrush, m_FrameStrRects[m_I], FontInfo.SfLc);
            }

            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                m_Btns[m_I].Paint(dcGs);
            }

            base.paint(dcGs);
        }
        #endregion

    }
}
