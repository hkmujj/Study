#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-25
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图5-PIS-No.0-主界面
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
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

namespace Urban.HeFei.TMS.AC
{

    /// <summary>
    /// 功能描述：视图5-空调-No.0-界面1
    /// 创建人：lih
    /// 创建时间：2015-8-25
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V5AirConditionerC0Page1 : baseClass
    {

        #region 稀有变量
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private List<Button> m_BtnsMode = new List<Button>();
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private List<Button> m_BtnsDownTabView = new List<Button>();
        private int m_I, m_J = 0;
        private SolidBrush m_RectBrush = new SolidBrush(Color.FromArgb(1, 116, 154));

        private SolidBrush m_ControlBrush = new SolidBrush(Color.FromArgb(147, 147, 147));

        private Font m_DigitFont = new Font("Arial", 18);

        private Font m_ChineseFont = new Font("宋体", 18);
        private Font m_ChineseFontB = new Font("宋体", 14);
        private Font m_ChineseFontA = new Font("宋体", 14, FontStyle.Bold);
        private Rectangle m_PageRect = new Rectangle(710, 335, 56, 27);
        private SolidBrush m_BlackBrush = new SolidBrush(Color.Black);
        private Rectangle[] m_FrameRects;
        private Rectangle m_FrameSpeA = new Rectangle(12, 452, 128, 50);
        private string[] m_FrameStrs;
        private int[] m_RectYs;
        private int[] m_RectXs;
        private Rectangle[] m_Rect1;
        private Rectangle[] m_Rect2;
        private Rectangle[] m_RectStr2;
        private Rectangle[] m_Rect3;
        private Rectangle[] m_RectStr3;
        private string[] m_ModelStrs;
        private string m_PageStr = "页1-2";

        private bool[] m_Rect1Flags;
        private bool[] m_Rect3Flags;
        private string[] m_Rect2Strs;

        private int[] m_Rect1Count;

        private int m_IndexFlag = 0;

        private ViewState CurrentViewState
        {
            get { return m_CurrentViewState; }
            set
            {
                m_CurrentViewState = value;
                if (m_CurrentViewState == ViewState.AirConditionP1Manual)
                {
                    m_Control.ForEach(f => f.Visible = true);
                }
                else
                {
                    m_Control.ForEach(f => f.Visible = false);
                }
            }
        }

        private int AirConditionNum
        {
            get { return m_AirConditionNum; }
            set
            {
                m_AirConditionNum = value;
                append_postCmd(CmdType.SetFloatValue, GetOutFloatIndex(OutFloatKeys.空调温度), 0, m_AirConditionNum);
            }
        }

        private int m_TimeCount = 0;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "空调-界面1";
        }

        private List<GDIRectText> m_Control;

        /// <summary>
        /// 初始化函数：导入图片、创建控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_Control = new List<GDIRectText>();
            m_FrameRects = new Rectangle[3];
            m_FrameRects[0] = new Rectangle(12, 262, 674, 84);
            m_FrameRects[1] = new Rectangle(12, 350, 674, 45);
            m_FrameRects[2] = new Rectangle(12, 400, 674, 45);
            m_FrameStrs = new string[4] { "空调机组状态", "客室温度", "控制模式", "空调模式选择" };

            m_RectXs = new int[6];
            for (m_I = 0; m_I < m_RectXs.Length; m_I++)
            {
                m_RectXs[m_I] = 177 + m_I * 86;
            }

            m_RectYs = new int[4];
            m_RectYs[0] = 266;
            m_RectYs[1] = 306;
            m_RectYs[2] = 355;
            m_RectYs[3] = 404;

            m_Rect1 = new Rectangle[12];
            m_Rect1Flags = new bool[12];
            m_Rect1Count = new int[12];
            for (m_I = 0; m_I < m_RectXs.Length; m_I++)
            {
                m_Rect1Count[2 * m_I] = 0;
                m_Rect1Count[2 * m_I + 1] = 0;
                m_Rect1Flags[2 * m_I] = false;
                m_Rect1Flags[2 * m_I + 1] = false;
                m_Rect1[2 * m_I] = new Rectangle(m_RectXs[m_I], m_RectYs[0], 60, 36);
                m_Rect1[2 * m_I + 1] = new Rectangle(m_RectXs[m_I], m_RectYs[1], 60, 36);
            }


            m_Rect2 = new Rectangle[6];
            m_RectStr2 = new Rectangle[6];
            m_Rect2Strs = new string[6] { "", "", "", "", "", "" };
            for (m_I = 0; m_I < m_Rect2.Length; m_I++)
            {
                m_Rect2[m_I] = new Rectangle(m_RectXs[m_I], m_RectYs[2], 58, 35);
                m_RectStr2[m_I] = new Rectangle(m_RectXs[m_I] + 1, m_RectYs[2] + 1, 57, 34);
            }

            m_Rect3 = new Rectangle[6];
            m_RectStr3 = new Rectangle[6];
            m_ModelStrs = new string[3] { "集控", "本控", "?" };
            for (m_I = 0; m_I < m_Rect3.Length; m_I++)
            {
                m_Rect3[m_I] = new Rectangle(m_RectXs[m_I], m_RectYs[3], 58, 35);
                m_RectStr3[m_I] = new Rectangle(m_RectXs[m_I] + 1, m_RectYs[3] + 1, 57, 34);
            }

            //
            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });

            //模式按钮列表
            var strs1 = new string[] { "自动", "手动", "通风", "停止" };
            for (var i = 0; i < strs1.Length; i++)
            {
                var btn = new Button(
                    strs1[i],
                    new RectangleF(140 + i * 90, 452, 86, 50),
                    (int)(ViewState.AirConditionP1Auto + i),
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyleEs() { Font = new Font("宋体", 18), TextBrush = Brushs.BlackBrush, StringFormat = FontInfo.SfCc },
                        Background = m_ResourceImage[0],
                        DownImage = m_ResourceImage[1]
                    },
                    false
                    );
                btn.ClickEvent += btn_Mode_ClickEvent;
                m_BtnsMode.Add(btn);
            }
            m_BtnsMode[0].IsReplication = false;
            CurrentViewState = ViewState.AirConditionP1Auto;
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + CommonStatus.SdBoolBaseNumber, 1, 0);
            var btntc = new Button("仅TC车", new RectangleF(140 + 4 * 90, 452, 86, 50), 4, new ButtonStyle()
            {
                FontStyle = new FontStyleEs() { Font = new Font("宋体", 18), TextBrush = Brushs.BlackBrush, StringFormat = FontInfo.SfCc },
                Background = m_ResourceImage[0],
                DownImage = m_ResourceImage[1]
            }, false);
            btntc.ClickEvent += (sender, args) =>
            {
                var btn = sender as Button;
                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[4] + CommonStatus.SdBoolBaseNumber, btn.IsReplication ? 0 : 1, 0);

            };
            var bs1 = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[2], DownImage = m_ResourceImage[2] };
            var btn1 = new Button(
                 "",
                 new RectangleF(713, 359, 57, 34),
                 5000,
                 bs1,
                 false
                 );
            btn1.ClickEvent += btn1_ClickEvent;//up
            m_BtnsDownTabView.Add(btn1);
            m_BtnsDownTabView.Add(btntc);
            m_Control.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(590, 460, 25, 25),
                BKImage = m_ResourceImage[14],
                NeedDarwOutline = false,
                BackColorVisible = true,
                Visible = false,
                BkColor = Color.Empty,
                Tag = 2

            });
            m_Control.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(620, 460, 50, 25),
                BkColor = Brushs.WhiteBrush.Color,
                TextBrush = Brushs.BlackBrush,
                BackColorVisible = true,
                NeedDarwOutline = false,
                Text = AirConditionNum.ToString(),
                Visible = false,
                TextFormat = new StringFormat()
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Center,
                },
                RefreshAction = (o) =>
                {
                    var tmp = o as GDIRectText;
                    if (tmp != null)
                    {
                        tmp.Text = AirConditionNum.ToString();
                    }
                }
            });
            m_Control.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(675, 460, 25, 25),
                BKImage = m_ResourceImage[15],
                NeedDarwOutline = false,
                BackColorVisible = true,
                BkColor = Color.Empty,
                Visible = false,
                Tag = 1

            });
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
            for (var i = 0; i < m_BtnsMode.Count; i++)
            {
                if ((nPoint.X >= m_BtnsMode[i].Rect.X)
                    && (nPoint.X <= m_BtnsMode[i].Rect.X + m_BtnsMode[i].Rect.Width)
                    && (nPoint.Y >= m_BtnsMode[i].Rect.Y)
                    && (nPoint.Y <= m_BtnsMode[i].Rect.Y + m_BtnsMode[i].Rect.Height))
                {
                    m_BtnsMode[i].MouseDown(nPoint);
                    m_BtnsMode[i].IsReplication = false;
                    m_BtnsMode.FindAll(a => a.ID != m_BtnsMode[i].ID).ForEach(b => b.IsReplication = true);
                    break;
                }
            }


            for (var i = 0; i < m_BtnsDownTabView.Count; i++)
            {
                if ((nPoint.X >= m_BtnsDownTabView[i].Rect.X)
                    && (nPoint.X <= m_BtnsDownTabView[i].Rect.X + m_BtnsDownTabView[i].Rect.Width)
                    && (nPoint.Y >= m_BtnsDownTabView[i].Rect.Y)
                    && (nPoint.Y <= m_BtnsDownTabView[i].Rect.Y + m_BtnsDownTabView[i].Rect.Height))
                {
                    m_BtnsDownTabView[i].MouseDown(nPoint);
                    break;
                }
            }
            if (m_Control.Any(w => w.Visible && w.OutLineRectangle.Contains(nPoint) && w.Tag != null && (int)w.Tag == 1))
            {
                AirConditionNum++;
            }
            else if (m_Control.Any(w => w.Visible && w.OutLineRectangle.Contains(nPoint) && w.Tag != null && (int)w.Tag == 2))
            {
                AirConditionNum--;
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
            for (var i = 0; i < m_BtnsMode.Count; i++)
            {
                if ((nPoint.X >= m_BtnsMode[i].Rect.X)
                    && (nPoint.X <= m_BtnsMode[i].Rect.X + m_BtnsMode[i].Rect.Width)
                    && (nPoint.Y >= m_BtnsMode[i].Rect.Y)
                    && (nPoint.Y <= m_BtnsMode[i].Rect.Y + m_BtnsMode[i].Rect.Height))
                {
                    m_BtnsMode[i].MouseUp(nPoint);
                    break;
                }
            }

            for (var i = 0; i < m_BtnsDownTabView.Count; i++)
            {
                if ((nPoint.X >= m_BtnsDownTabView[i].Rect.X)
                    && (nPoint.X <= m_BtnsDownTabView[i].Rect.X + m_BtnsDownTabView[i].Rect.Width)
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
        /// 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_Mode_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            ViewState temp;
            if (!Enum.TryParse(e.Message.ToString(), out temp))
            {
                return;
            }
            switch (temp)
            {
                case ViewState.AirConditionP1Auto:
                    CurrentViewState = ViewState.AirConditionP1Auto;
                    break;
                case ViewState.AirConditionP1Manual:
                    CurrentViewState = ViewState.AirConditionP1Manual;
                    break;
                case ViewState.AirConditionP1Air:
                    CurrentViewState = ViewState.AirConditionP1Air;
                    break;
                case ViewState.AirConditionP1Stop:
                    CurrentViewState = ViewState.AirConditionP1Stop;
                    break;
                default: break;
            }

        }

        /// <summary>
        /// btn1_ClickEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn1_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            append_postCmd(CmdType.ChangePage, (int)ViewState.AirConditionerPage2, 0, 0);
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            m_BtnsMode.ToList().ForEach(a => a.Paint(dcGs));
            m_BtnsDownTabView.ToList().ForEach(a => a.Paint(dcGs));

            OnSendData();

            dcGs.DrawString(m_PageStr, m_ChineseFontA, m_BlackBrush, m_PageRect, FontInfo.SfCc);

            for (m_I = 0; m_I < m_FrameRects.Length; m_I++)
            {
                dcGs.DrawRectangle(m_BlackLinePen, m_FrameRects[m_I]);
                dcGs.DrawString(m_FrameStrs[m_I], m_ChineseFontB, Brushs.BlackBrush, m_FrameRects[m_I], FontInfo.SfLc);
            }
            dcGs.DrawString(m_FrameStrs[3], m_ChineseFontB, Brushs.BlackBrush, m_FrameSpeA, FontInfo.SfLc);


            for (m_I = 0; m_I < 6; m_I++)
            {
                m_Rect1Flags[2 * m_I] = false;
                m_Rect1Flags[2 * m_I + 1] = false;
                for (m_J = 0; m_J < 8; m_J++)
                {
                    if (m_J == 1 || m_J == 5)
                    {
                        if (BoolList[UIObj.InBoolList[m_I] + m_J])
                        {
                            m_Rect1Flags[2 * m_I] = true;
                            PaintAc(dcGs, 2 * m_I);
                        }
                        if (BoolList[UIObj.InBoolList[m_I] + m_J + 8])
                        {
                            m_Rect1Flags[2 * m_I + 1] = true;
                            PaintAc(dcGs, 2 * m_I + 1);
                        }
                    }
                    else
                    {
                        if (BoolList[UIObj.InBoolList[m_I] + m_J])
                        {
                            m_Rect1Flags[2 * m_I] = true;
                            dcGs.DrawImage(m_ResourceImage[3 + m_J], m_Rect1[2 * m_I]);
                        }
                        if (BoolList[UIObj.InBoolList[m_I] + m_J + 8])
                        {
                            m_Rect1Flags[2 * m_I + 1] = true;
                            dcGs.DrawImage(m_ResourceImage[3 + m_J], m_Rect1[2 * m_I + 1]);
                        }
                    }
                }
            }

            for (m_I = 0; m_I < 6; m_I++)
            {
                if (m_Rect1Flags[2 * m_I] == false)
                {
                    dcGs.DrawImage(m_ResourceImage[11], m_Rect1[2 * m_I]);
                }
                if (m_Rect1Flags[2 * m_I + 1] == false)
                {
                    dcGs.DrawImage(m_ResourceImage[11], m_Rect1[2 * m_I + 1]);
                }
            }

            for (m_I = 0; m_I < m_Rect2.Length; m_I++)
            {
                dcGs.DrawRectangle(m_BlackLinePen, m_Rect2[m_I]);
                dcGs.FillRectangle(m_RectBrush, m_RectStr2[m_I]);

                m_Rect2Strs[m_I] = FloatList[UIObj.InFloatList[m_I]].ToString("0");
                dcGs.DrawString(m_Rect2Strs[m_I], m_DigitFont, Brushs.BlackBrush, m_Rect2[m_I], FontInfo.SfCc);
            }

            for (m_I = 0; m_I < m_Rect3.Length; m_I++)
            {

                if (BoolList[UIObj.InBoolList[m_I + 6] + 1])
                {
                    dcGs.DrawRectangle(m_BlackLinePen, m_Rect3[m_I]);
                    dcGs.FillRectangle(m_RectBrush, m_RectStr3[m_I]);
                    dcGs.DrawString(m_ModelStrs[1], m_ChineseFont, Brushs.BlackBrush, m_Rect3[m_I], FontInfo.SfCc);
                }
                else if (BoolList[UIObj.InBoolList[m_I + 6]])
                {
                    dcGs.DrawRectangle(m_BlackLinePen, m_Rect3[m_I]);
                    dcGs.FillRectangle(m_RectBrush, m_RectStr3[m_I]);
                    dcGs.DrawString(m_ModelStrs[0], m_ChineseFont, Brushs.BlackBrush, m_Rect3[m_I], FontInfo.SfCc);
                }
                else
                {
                    dcGs.DrawImage(m_ResourceImage[11], m_Rect3[m_I]);
                }

            }
            m_Control.ForEach(f => f.OnPaint(dcGs));
            //
            base.paint(dcGs);
        }

        /// <summary>
        /// paintAC
        /// </summary>
        /// <param name="dcGs"></param>
        /// <param name="cindex"></param>
        private void PaintAc(Graphics dcGs, int cindex)
        {
            if (m_Rect1Count[cindex] < 2)
            {
                m_Rect1Count[cindex]++;
            }
            else if (m_Rect1Count[cindex] >= 2 && m_Rect1Count[cindex] <= 4)
            {
                dcGs.DrawImage(m_ResourceImage[3 + m_J], m_Rect1[cindex]);
                m_Rect1Count[cindex]++;
            }
            else
            {
                m_Rect1Count[cindex] = 0;
            }
        }

        private bool m_AnlyTcTrain = false;
        private ViewState m_CurrentViewState;
        private int m_AirConditionNum = 22;

        /// <summary>
        /// onSendData
        /// </summary>
        private void OnSendData()
        {
            switch (CurrentViewState)
            {
                case ViewState.AirConditionP1Auto:
                    m_IndexFlag = 0;
                    break;
                case ViewState.AirConditionP1Manual:
                    m_IndexFlag = 1;
                    break;
                case ViewState.AirConditionP1Air:
                    m_IndexFlag = 2;
                    break;
                case ViewState.AirConditionP1Stop:
                    m_IndexFlag = 3;
                    break;

                default: break;
            }

            for (m_I = 0; m_I < 4; m_I++)
            {
                if (m_I != m_IndexFlag)
                {
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[m_I] + CommonStatus.SdBoolBaseNumber, 0, 0);
                }
                else
                {
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[m_I] + CommonStatus.SdBoolBaseNumber, 1, 0);
                }
            }

        }
        #endregion
    }
}
