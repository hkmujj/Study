#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-25
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图603-PIS-手动
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
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
    /// 功能描述：视图603-PIS-手动
    /// 创建人：lih
    /// 创建时间：2015-8-25
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V603PISManualModel : baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Pen m_WhileLinePen = new Pen(Color.White, 1.6f);
        private Font m_ChineseFont = new Font("宋体", 18);
        private Button[] m_Btns;//按钮列表
        private int m_I = 0;
        private Rectangle[] m_FrameRects;
        private string[] m_FrameStrs;
        private Rectangle[] m_FrameStrRects;
        private string[] m_BtnNames;
        private ViewState m_CurrentViewState;
        private Rectangle[] m_WhiteStaInfoRects;
        private Rectangle m_StationFramRec;
        private int m_IndexFlag = 0;
        private List<Button> m_StationButtons;
        private string m_NextStationStr;//下一站名
        private string m_EndStationStr;//终点站名
        private int m_NextStationIndex = 0, m_EndStationIndex = 0;//下一站终点站Index
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "PIS-手动模式";
        }


        /// <summary>
        /// 初始化函数：初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex">错误索性</param>
        /// <returns>初始化是否成功</returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_FrameRects = new Rectangle[2];
            m_FrameRects[0] = new Rectangle(37, 210, 554, 56);
            m_FrameRects[1] = new Rectangle(37, 275, 554, 56);

            m_WhiteStaInfoRects = new Rectangle[2];
            m_WhiteStaInfoRects[0] = new Rectangle(288, 211, 303, 54);
            m_WhiteStaInfoRects[1] = new Rectangle(288, 276, 303, 54);

            m_FrameStrRects = new Rectangle[3];
            m_FrameStrRects[0] = new Rectangle(49, 182, 120, 28);
            m_FrameStrRects[1] = new Rectangle(37, 210, 251, 56);
            m_FrameStrRects[2] = new Rectangle(37, 275, 251, 56);

            m_FrameStrs = new string[3] { "手动模式", "下一站", "终点站" };

            m_BtnNames = new string[6] { "自动", "半自动", "选择", "发车", "下一站", "到达站" };

            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });

            m_StationFramRec = new Rectangle(150, 150, 500, 300);


            var bs1 = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[1] };
            m_Btns = new Button[7];
            m_Btns[0] = new Button(m_BtnNames[0], new Rectangle(300, 375, 84, 50), (int)ViewState.PISAutoModel, bs1, true);
            m_Btns[1] = new Button(m_BtnNames[1], new Rectangle(400, 375, 84, 50), (int)ViewState.PISSemiAutoModel, bs1, true);

            m_Btns[2] = new Button(m_BtnNames[2], new Rectangle(604, 212, 84, 50), (int)ViewState.PismSelectNextStation, bs1, true);
            m_Btns[3] = new Button(m_BtnNames[2], new Rectangle(604, 277, 84, 50), (int)ViewState.PismSelectEndStation, bs1, true);

            m_Btns[4] = new Button(m_BtnNames[3], new Rectangle(701, 212, 84, 50), (int)ViewState.PismDepartBtn, bs1, true);
            m_Btns[5] = new Button(m_BtnNames[4], new Rectangle(701, 277, 84, 50), (int)ViewState.PismNextStationBtn, bs1, true);
            m_Btns[6] = new Button(m_BtnNames[5], new Rectangle(701, 338, 84, 50), (int)ViewState.PismArriveStationBtn, bs1, true);

            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                m_Btns[m_I].ClickEvent += V603_PIS_ManualModel_ClickEvent;
            }
            m_StationButtons = new List<Button>();
            InitStationButtons();
            m_ClearTimer = new Timer((state) =>
              {
                  append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.手动报站发车报站), 0, 0);
                  append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.手动报站到站报站), 0, 0);
              });
            return true;
        }

        private void InitStationButtons()
        {
            var bs1 = new ButtonStyle() { FontStyle = FontStyles.FsSong10CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[1] };
            var size = new Size(70, 40);
            var inval = 0;
            var loaclPoint = new Point(200, 200);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    var idnex = i * 6 + j + 1;
                    var btn = new Button(GetStation(idnex), new RectangleF(loaclPoint, size), idnex, bs1);
                    btn.ClickEvent += (sender, args) =>
                    {
                        var but = sender as Button;
                        if (!string.IsNullOrEmpty(but?.Text))
                        {
                            switch (m_CurrentViewState)
                            {
                                case ViewState.PismSelectNextStation:
                                    m_NextStationIndex = args.Message;
                                    break;
                                case ViewState.PismSelectEndStation:
                                    m_EndStationIndex = args.Message;
                                    break;
                            }
                            IsSelect = false;
                        }
                    };
                    m_StationButtons.Add(btn);
                    loaclPoint.Offset(size.Width + inval, 0);
                }
                loaclPoint.Offset(-(size.Width + inval) * 6, size.Height + inval);
            }
            var btns = new Button("取消", new RectangleF((int)(loaclPoint.X + size.Width * 2.5), loaclPoint.Y + 5, size.Width, size.Height), -1, bs1, true);
            btns.ClickEvent += (sender, args) => { IsSelect = false; };
            m_StationButtons.Add(btns);
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

            if (IsSelect)
            {
                m_StationButtons.Where(w => w.Rect.Contains(nPoint)).ToList().ForEach(f => f.MouseDown(nPoint));
            }
            else
            {
                m_Btns.Where(w => w.Rect.Contains(nPoint)).ToList().ForEach(f => f.MouseDown(nPoint));
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
            if (IsSelect)
            {
                m_StationButtons.Where(w => w.Rect.Contains(nPoint)).ToList().ForEach(f => f.MouseUp(nPoint));
            }
            else
            {
                m_Btns.Where(w => w.Rect.Contains(nPoint)).ToList().ForEach(f => f.MouseUp(nPoint));
            }
            return base.mouseUp(nPoint);
        }

        public static event Action<int> ModelChanged;
        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void V603_PIS_ManualModel_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (!Enum.TryParse(e.Message.ToString(), out m_CurrentViewState))
            {
                return;
            }
            switch (m_CurrentViewState)
            {
                case ViewState.PISAutoModel:
                    OnModelChanged(e.Message);
                    append_postCmd(CmdType.ChangePage, (int)ViewState.PISAutoModel, 0, 0);
                    break;
                case ViewState.PISSemiAutoModel:
                    OnModelChanged(e.Message);
                    append_postCmd(CmdType.ChangePage, (int)ViewState.PISAutoModel, 0, 0);
                    break;
                case ViewState.PismSelectEndStation:
                    IsSelect = true;
                    break;
                case ViewState.PismSelectNextStation:
                    IsSelect = true;
                    break;
                case ViewState.PismDepartBtn:
                    if (m_NextStationIndex > 0 && m_EndStationIndex > 0)
                    {
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.报站模式自动), 0, 0);
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.报站模式半自动), 0, 0);
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.报站模式手动), 1, 0);
                        append_postCmd(CmdType.SetFloatValue, GetOutFloatIndex(OutFloatKeys.手动报站下一站), 0, m_NextStationIndex);
                        append_postCmd(CmdType.SetFloatValue, GetOutFloatIndex(OutFloatKeys.手动报站终点站), 0, m_EndStationIndex);
                        OnModelChanged((int)ViewState.PISManualModel);
                    }

                    break;
                case ViewState.PismNextStationBtn:
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.手动报站发车报站), 1, 0);
                    m_ClearTimer.Change(200, int.MaxValue);
                    break;
                case ViewState.PismArriveStationBtn:
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.手动报站到站报站), 1, 0);
                    m_ClearTimer.Change(200, int.MaxValue);
                    break;
            }
        }

        private Timer m_ClearTimer;


        #endregion

        private bool IsSelect;
        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            for (m_I = 0; m_I < m_FrameRects.Length; m_I++)
            {
                dcGs.DrawRectangle(m_BlackLinePen, m_FrameRects[m_I]);
            }

            for (m_I = 0; m_I < m_FrameStrRects.Length; m_I++)
            {
                dcGs.DrawString(m_FrameStrs[m_I], m_ChineseFont, Brushs.BlackBrush, m_FrameStrRects[m_I], FontInfo.SfLc);
            }

            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                m_Btns[m_I].Paint(dcGs);
            }

            for (m_I = 0; m_I < m_WhiteStaInfoRects.Length; m_I++)
            {
                dcGs.FillRectangle(Brushs.WhiteBrush, m_WhiteStaInfoRects[m_I]);
            }
            m_NextStationStr = GetStation(m_NextStationIndex) ?? "";
            m_EndStationStr = GetStation(m_EndStationIndex) ?? "";
            dcGs.DrawString(m_NextStationStr, m_ChineseFont, Brushs.BlackBrush, m_WhiteStaInfoRects[0], FontInfo.SfLc);
            dcGs.DrawString(m_EndStationStr, m_ChineseFont, Brushs.BlackBrush, m_WhiteStaInfoRects[1], FontInfo.SfLc);
            if (IsSelect)
            {
                dcGs.DrawImage(m_ResourceImage[2], m_StationFramRec);
                dcGs.DrawLine(m_WhileLinePen, m_StationFramRec.X, m_StationFramRec.Y, m_StationFramRec.X + m_StationFramRec.Width, m_StationFramRec.Y);
                dcGs.DrawLine(m_WhileLinePen, m_StationFramRec.X, m_StationFramRec.Y, m_StationFramRec.X, m_StationFramRec.Y + m_StationFramRec.Height);
                dcGs.DrawLine(m_BlackLinePen, m_StationFramRec.X + m_StationFramRec.Width, m_StationFramRec.Y, m_StationFramRec.X + m_StationFramRec.Width, m_StationFramRec.Y + m_StationFramRec.Height);
                dcGs.DrawLine(m_BlackLinePen, m_StationFramRec.X, m_StationFramRec.Y + m_StationFramRec.Height, m_StationFramRec.X + m_StationFramRec.Width, m_StationFramRec.Y + m_StationFramRec.Height);
                m_StationButtons.ForEach(f => f.Paint(dcGs));
            }
            base.paint(dcGs);
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

        private static void OnModelChanged(int obj)
        {
            ModelChanged?.Invoke(obj);
        }
    }
}
