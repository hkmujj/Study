using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CommonUtil.Controls;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;
using Urban.HeFei.TMS.Common;

namespace Urban.HeFei.TMS.Maintenance
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class PasswordKeyInput : baseClass
    {
        private List<Button> m_Buttons;
        private List<Image> m_ResourceImage;
        private Font m_TextFont18 = new Font("宋体", 18f);
        private Font m_TextFont16 = new Font("宋体", 16f);
        private Rectangle m_DigitFramRec;
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private List<GDIRectText> m_Text;
        public override string GetInfo()
        {
            return "密码输入";
        }
        public static int Index { get; set; }
        private int m_LastView = -1;
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                m_LastView = (int)nParaC;
            }
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_Buttons = new List<Button>();
            m_Text = new List<GDIRectText>();

            m_DigitFramRec = new Rectangle(500, 114, 250, 307);
            m_ResourceImage = UIObj.ParaList.Select(s => Image.FromFile(Path.Combine(RecPath, s))).ToList();
            InitText();
            InitButtons();
            return true;
        }

        private void InitText()
        {
            var gdiText1 = new GDIRectText()
            {
                Text = "请输入密码",
                BackColorVisible = false,
                TextColor = Color.Black,
                NeedDarwOutline = false,
                OutLineRectangle = new Rectangle(250, 160, 200, 38),
                TextFormat = FontInfo.SfCc,
                DrawFont = m_TextFont18
            };
            var gdiText2 = new GDIRectText()
            {
                Text = "",
                BackColorVisible = true,
                TextColor = Color.Black,
                OutLinePen = m_BlackLinePen,
                NeedDarwOutline = true,
                BkColor = Color.White,
                TextFormat = FontInfo.SfLc,
                RefreshAction = (o) =>
                {
                    ((GDIRectText)o).Text = m_DisplayStr.Length != 0 ? "*".PadLeft(m_DisplayStr.Length, '*') : string.Empty;
                },
                OutLineRectangle = new Rectangle(250, 240, 200, 38),
                DrawFont = m_TextFont16

            };
            m_Text.Add(gdiText1);
            m_Text.Add(gdiText2);
        }

        private void InitButtons()
        {
            var digitBtnStyle = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[1] };
            var inputCancelStyle = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[2], DownImage = m_ResourceImage[3] };
            var inputConfirmStyle = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[4], DownImage = m_ResourceImage[5] };
            var commonBtnStyle = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[6], DownImage = m_ResourceImage[7] };
            var digitBackBtnStyle = new ButtonStyle() { FontStyle = FontStyles.FsSong14CcB, Background = m_ResourceImage[9], DownImage = m_ResourceImage[10] };

            var initPoint = new Point(550, 160);
            var str = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "backspace" };
            var size1 = new Size(50, 38);
            var inval = 1;

            var confirmBtn = new Button("确认", new RectangleF(550, 350, 65, 38), -1, inputConfirmStyle, true);
            var cancelBtn = new Button("取消", new RectangleF(630, 350, 65, 38), -1, inputCancelStyle, true);
            cancelBtn.ClickEvent += (sender, args) =>
            {
                append_postCmd(CmdType.ChangePage, m_LastView, 0, 0);
            };
            m_Buttons.Add(confirmBtn);
            m_Buttons.Add(cancelBtn);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i * 3 + j == str.Length)
                    {
                        break;
                    }
                    if (i * 3 + j == str.Length - 1)
                    {
                        var btn = new Button(str[i * 3 + j], new RectangleF(initPoint, new Size(size1.Width * 2 + inval, size1.Height)), 10, digitBackBtnStyle, true);
                        btn.ClickEvent += Btn_ClickEvent;
                        m_Buttons.Add(btn);

                    }
                    else
                    {
                        var btn = new Button(str[i * 3 + j], new RectangleF(initPoint, size1), int.Parse(str[i * 3 + j]), digitBtnStyle, true);
                        btn.ClickEvent += Btn_ClickEvent;
                        m_Buttons.Add(btn);
                    }

                    initPoint.Offset(size1.Width + inval, 0);
                }
                initPoint.Offset(-(size1.Width + inval) * 3, size1.Height + inval);
            }


        }

        /// <summary>
        /// 鼠标单点下
        /// </summary>
        /// <param name="point"/>
        /// <returns/>
        public override bool mouseDown(Point point)
        {
            m_Buttons.Where(w => w.Rect.Contains(point)).ToList().ForEach(f => f.MouseDown(point));
            return base.mouseDown(point);
        }

        /// <summary>
        /// 鼠标弹起
        /// </summary>
        /// <param name="point"/>
        /// <returns/>
        public override bool mouseUp(Point point)
        {

            m_Buttons.Where(w => w.Rect.Contains(point)).ToList().ForEach(f => f.MouseUp(point));

            return base.mouseUp(point);
        }

        private string m_DisplayStr = string.Empty;
        private void Btn_ClickEvent(object sender, ES.Facility.Common.Control.Common.ClickEventArgs<int> e)
        {
            if (e.Message == 10)
            {
                if (m_DisplayStr.Length > 1)
                {
                    m_DisplayStr = m_DisplayStr.Substring(0, m_DisplayStr.Length - 1);
                }
                else
                {
                    m_DisplayStr = string.Empty;
                }
            }
            else
            {
                m_DisplayStr += e.Message;
            }


        }

        public override void paint(Graphics g)
        {
            g.DrawImage(m_ResourceImage[8], m_DigitFramRec);
            m_Buttons.ForEach(f => f.Paint(g));
            m_Text.ForEach(f => f.OnPaint(g));
            base.paint(g);
        }
    }
}