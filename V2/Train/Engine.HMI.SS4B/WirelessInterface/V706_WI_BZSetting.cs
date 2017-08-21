/*--------------------------------------------------------------------------------------------------
 *
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-24
 *
 * -------------------------------------------------------------------------------------------------
 *
 * 功能描述：视图4-辅助-No.0-页面1
 *
 *-------------------------------------------------------------------------------------------------*/

using CommonUtil.Controls;
using CommonUtil.Util;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.define;
using SS4B_TMS.Common;
using SS4B_TMS.Config;
using SS4B_TMS.Resource;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SS4B_TMS.WirelessInterface
{
    /// <summary>
    ///     功能描述：视图4-辅助-No.0-页面1
    ///     创建人：lih
    ///     创建时间：2015-8-24
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V706WIBZSetting : baseClass
    {
        private List<SS4BListBox> m_List;
        private GDIRectText MarshallingNumText;
        private SS4BListBox MarshallingNumBox;
        private readonly SolidBrush m_CentreRectBrush = new SolidBrush(Color.FromArgb(212, 232, 255));
        private readonly Rectangle m_CentreFrameRect = new Rectangle(125, 154, 580, 240);
        private readonly Rectangle m_FlagRect = new Rectangle(1, 539, 122, 58);//标识
        private List<SS4BButton> m_ButtonList;
        private List<GDIRectText> m_RectTexts;
        private Font m_ChineseFont = new Font("宋体", 14);
        private Font m_TitleFont = new Font("宋体", 16);
        private Dictionary<int, List<SS4BListBox>> m_CarDicOne;
        private Dictionary<int, GDIRectText> m_CaeDicTwo;
        private Rectangle[] ChangedRec = new Rectangle[4];

        public override bool init(ref int nErrorObjectIndex)
        {
            m_List = new List<SS4BListBox>();
            m_ButtonList = new List<SS4BButton>();
            m_RectTexts = new List<GDIRectText>();
            m_CarDicOne = new Dictionary<int, List<SS4BListBox>>();
            m_CaeDicTwo = new Dictionary<int, GDIRectText>();
            InitRecText();
            InitListBox();
            InitButton();
            SendData();
            m_List.ForEach(f => f.SelectIndexChanged += ((i, i1) =>
                {
                    m_Row = i;
                    m_Col = i1;
                    SelectChanged();
                }));
            return true;
        }

        private void InitRecText()
        {
            var strs = new[]
            {
                "本车类型:", "车辆编号:", "运行方向:", "主从设置:", "从车数量:",
                "车辆类型", "车辆编号", "车间距离", "从1机车:", "从2机车:", "从3机车:","车辆编组参数设置","主控机车:"
            };
            var recs = new Rectangle[13];
            ChangedRec[1] = new Rectangle(129, 185 + 33 * 5, 100, 30);
            ChangedRec[0] = new Rectangle(129, 185 + 33 * 4, 100, 30);
            for (int i = 0; i < 11; i++)
            {
                if (i < 5)
                {
                    recs[i] = new Rectangle(129, 185 + i * 33, 100, 30);
                }
                else if (i < 8)
                {
                    recs[i] = new Rectangle(397 + 100 * (i - 5), 160, 100, 30);
                }
                else
                {
                    recs[i] = new Rectangle(325, 185 + (i - 8) * 33, 100, 30);
                }
            }
            recs[11] = new Rectangle(270, 60, 280, 40);
            recs[12] = recs[8];
            for (int i = 0; i < strs.Length; i++)
            {
                var gditext = new GDIRectText()
                {
                    Text = strs[i],
                    OutLineRectangle = recs[i],
                    NeedDarwOutline = false,
                    TextBrush = i == 11 ? Brushs.WhiteBrush : Brushs.BlackBrush,
                    TextFormat = FontInfo.SfCc,
                    DrawFont = i == 11 ? m_TitleFont : m_ChineseFont,
                    BackColorVisible = false
                };
                m_RectTexts.Add(gditext);
                if (i == 8)
                {
                    m_CaeDicTwo.Add(1, gditext);
                }
                if (i == 9)
                {
                    m_CaeDicTwo.Add(2, gditext);
                }
                if (i == 10)
                {
                    m_CaeDicTwo.Add(3, gditext);
                }
                if (i == 12)
                {
                    m_CaeDicTwo.Add(10, gditext);
                }
            }
            m_RectTexts.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(125, 492, 250, 30),
                BKBrush = Brushs.RedBrush,
                Text = "",
                TextBrush = Brushs.WhiteBrush,
                NeedDarwOutline = false,
                RefreshAction = (o) =>
                {
                    var gtx = (GDIRectText)o;
                    V708WIBZInfoTS.Text.ForEach(f => f.Refresh());
                    if (V708WIBZInfoTS.Fault.Count != 0)
                    {
                        if (Index % 10 != 0)
                        {
                            Index++;
                            return;
                        }
                        if (Index / 10 == V708WIBZInfoTS.Fault.Count)
                        {
                            Index = 0;
                        }
                        gtx.Text = V708WIBZInfoTS.Fault[Index / 10].Title;
                        gtx.Visible = true;
                        Index++;
                    }
                    else
                    {
                        gtx.Visible = false;
                        Index = -1;
                    }
                }
            }
            );
            MarshallingNumText = new GDIRectText()
            {
                Text = "编组序号",
                OutLineRectangle = ChangedRec[0],
                NeedDarwOutline = false,
                TextBrush = Brushs.BlackBrush,
                TextFormat = FontInfo.SfCc,
                DrawFont = m_ChineseFont,
                BackColorVisible = false,
                Visible = false
            };
            m_RectTexts.Add(MarshallingNumText);
        }

        private int Index = -1;

        private void InitListBox()
        {
            m_List.Add(new SS4BListBox()
            {
                DownPicSize = new Size(),
                OutRectangle = new Rectangle(229, 185, 100, 30),
                BackColor = Brushs.WhiteBrush,
                TextBrush = Brushs.BlackBrush,
                Direction = BoxDirection.Down,
                EnableBrush = Brushs.GrayBrush,
                SelectTextBrush = Brushs.WhiteBrush,
                Texts = new[] { "SS4B" },
                Row = 0,
                Column = 0,
                IsEnable = true,
                Type = BoxType.Text
            });
            m_List.Add(new SS4BListBox()
            {
                DownPicSize = new Size(),
                OutRectangle = new Rectangle(229, 185 + 33, 100, 30),
                BackColor = Brushs.WhiteBrush,
                TextBrush = Brushs.BlackBrush,
                Direction = BoxDirection.Down,
                EnableBrush = Brushs.GrayBrush,
                SelectTextBrush = Brushs.WhiteBrush,
                Texts = new[] { this.GetTrainConfig().Number },
                Row = 0,
                Column = 0,
                IsEnable = true,
                Type = BoxType.Text,
                Tag = "CurrentTrainNumber"
            });
            for (int i = 1; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var box = new SS4BListBox();

                    box.DownPicSize = new Size();
                    box.OutRectangle = new Rectangle(i == 1 ? 510 : 610, 185 + j * 33, 80, 30);
                    box.BackColor = Brushs.WhiteBrush;
                    box.TextBrush = Brushs.BlackBrush;
                    box.Direction = BoxDirection.Down;
                    box.EnableBrush = Brushs.GrayBrush;
                    box.SelectTextBrush = Brushs.WhiteBrush;
                    box.Texts = new[] { "0000" };
                    box.SelectBrush = Brushs.BlueBrush;
                    box.Type = BoxType.Text;
                    box.Row = j;
                    box.IsVisible = j == 0;
                    box.Column = i + 1;
                    box.Tag = (j + 1).ToString();
                    m_List.Add(box);
                    if (m_CarDicOne.ContainsKey(j + 1))
                    {
                        m_CarDicOne[j + 1].Add(box);
                    }
                    else
                    {
                        m_CarDicOne.Add(j + 1, new List<SS4BListBox>()
                        {
                            box
                        });
                    }
                    if (j == 0)
                    {
                        var box1 = new SS4BListBox
                        {
                            DownPicSize = new Size(),
                            OutRectangle = new Rectangle(i == 1 ? 510 : 610, 185 + j * 33, 80, 30),
                            BackColor = Brushs.WhiteBrush,
                            TextBrush = Brushs.BlackBrush,
                            Direction = BoxDirection.Down,
                            EnableBrush = Brushs.GrayBrush,
                            SelectTextBrush = Brushs.WhiteBrush,
                            Texts = new[] { "0000" },
                            SelectBrush = Brushs.BlueBrush,
                            Type = BoxType.Text,
                            Row = j,
                            Column = i + 1,
                            Tag = 10
                        };
                        m_List.Add(box1);
                        if (m_CarDicOne.ContainsKey(10))
                        {
                            m_CarDicOne[10].Add(box1);
                        }
                        else
                        {
                            m_CarDicOne.Add(10, new List<SS4BListBox>()
                        {
                            box1
                        });
                        }
                    }
                }
            }

            m_List.Add(new SS4BListBox()
            {
                DownPicSize = new Size(25, 30),
                OutRectangle = new Rectangle(229, 250, 100, 30),
                BackColor = Brushs.WhiteBrush,
                TextBrush = Brushs.BlackBrush,
                Direction = BoxDirection.Down,
                EnableBrush = Brushs.GrayBrush,
                SelectTextBrush = Brushs.WhiteBrush,
                Texts = new[] { "上行", "下行" },
                SelectBrush = Brushs.BlueBrush,
                Image = ImageResource.ListBoxButton,
                Row = 2,
                Column = 0,
                Tag = "Dirction"
            });
            var numBtn = new SS4BListBox();
            var main = new SS4BListBox()
            {
                DownPicSize = new Size(25, 30),
                OutRectangle = new Rectangle(229, 250 + 33, 100, 30),
                BackColor = Brushs.WhiteBrush,
                SelectTextBrush = Brushs.WhiteBrush,
                TextBrush = Brushs.BlackBrush,
                Direction = BoxDirection.Down,
                EnableBrush = Brushs.GrayBrush,
                Texts = new[] { "主控", "从控" },
                Image = ImageResource.ListBoxButton,
                SelectBrush = Brushs.BlueBrush,
                Row = 3,
                Column = 0,
                Tag = "Main"
            };
            main.SelectChanged += ((agrs) =>
            {
                if (agrs.Contains("主控"))
                {
                    foreach (var col in m_CarDicOne)
                    {
                        foreach (var c in col.Value)
                        {
                            c.IsVisible = col.Key == 1;
                        }
                    }
                    foreach (var text in m_CaeDicTwo)
                    {
                        text.Value.Visible = text.Key == 1;
                    }
                    numBtn.SetText(numBtn.Texts[0]);

                    MarshallingNumBox.IsVisible = false;
                    MarshallingNumText.Visible = false;
                    numBtn.OutRectangle = ChangedRec[2];
                    m_RectTexts[4].OutLineRectangle = ChangedRec[0];
                }
                else if (agrs.Contains("从控"))
                {
                    foreach (var col in m_CarDicOne)
                    {
                        foreach (var c in col.Value)
                        {
                            c.IsVisible = col.Key == 10;
                        }
                    }
                    foreach (var text in m_CaeDicTwo)
                    {
                        text.Value.Visible = text.Key == 10;
                    }
                    MarshallingNumBox.IsVisible = true;
                    MarshallingNumText.Visible = true;
                    numBtn.OutRectangle = ChangedRec[3];
                    m_RectTexts[4].OutLineRectangle = ChangedRec[1];
                }
            });
            m_List.Add(main);
            for (int i = 0; i < 3; i++)
            {
                var sb = new SS4BListBox()
                {
                    DownPicSize = new Size(25, 30),
                    OutRectangle = new Rectangle(417, 185 + 33 * i, 80, 30),
                    BackColor = Brushs.WhiteBrush,
                    SelectTextBrush = Brushs.WhiteBrush,
                    TextBrush = Brushs.BlackBrush,
                    Direction = BoxDirection.Down,
                    EnableBrush = Brushs.GrayBrush,
                    Texts = new[] { "SS4B", "SS4G", "SH8", "SH12" },
                    Image = ImageResource.ListBoxButton,
                    SelectBrush = Brushs.BlueBrush,
                    Row = i,
                    Column = 1,
                };
                m_List.Add(sb);

                if (m_CarDicOne.ContainsKey(i + 1))
                {
                    m_CarDicOne[i + 1].Add(sb);
                }
                else
                {
                    m_CarDicOne.Add(i + 1, new List<SS4BListBox>()
                        {
                            sb
                        });
                }
                if (i == 0)
                {
                    var sb1 = new SS4BListBox()
                    {
                        DownPicSize = new Size(25, 30),
                        OutRectangle = new Rectangle(417, 185 + 33 * i, 80, 30),
                        BackColor = Brushs.WhiteBrush,
                        SelectTextBrush = Brushs.WhiteBrush,
                        TextBrush = Brushs.BlackBrush,
                        Direction = BoxDirection.Down,
                        EnableBrush = Brushs.GrayBrush,
                        Texts = new[] { "SS4B", "SS4G", "SH8", "SH12" },
                        Image = ImageResource.ListBoxButton,
                        SelectBrush = Brushs.BlueBrush,
                        Row = i,
                        Column = 1,
                    };
                    m_List.Add(sb1);
                    if (m_CarDicOne.ContainsKey(10))
                    {
                        m_CarDicOne[10].Add(sb1);
                    }
                    else
                    {
                        m_CarDicOne.Add(10, new List<SS4BListBox>()
                        {
                            sb1
                        });
                    }
                }
            }

            //Todo 从车显示隐藏以实现  缺少当前车是从车的显示逻辑
            numBtn.SelectChanged += (args) =>
            {
                if (main.Text.Equals("主控"))
                {
                    var tmp = Convert.ToInt32(args);
                    foreach (var col in m_CarDicOne)
                    {
                        foreach (var c in col.Value)
                        {
                            c.IsVisible = col.Key <= tmp;
                        }
                    }
                    foreach (var text in m_CaeDicTwo)
                    {
                        text.Value.Visible = text.Key <= tmp;
                    }
                }
            };
            {
                numBtn.DownPicSize = new Size(25, 30);
                numBtn.OutRectangle = new Rectangle(229, 250 + 33 * 2, 100, 30);
                numBtn.BackColor = Brushs.WhiteBrush;
                numBtn.TextBrush = Brushs.BlackBrush;
                numBtn.SelectTextBrush = Brushs.WhiteBrush;
                numBtn.Direction = BoxDirection.Down;
                numBtn.EnableBrush = Brushs.GrayBrush;
                numBtn.Texts = new[] { "1", "2", "3" };
                numBtn.Image = ImageResource.ListBoxButton;
                numBtn.SelectBrush = Brushs.BlueBrush;
                numBtn.Row = 4;
                numBtn.Column = 0;
                numBtn.Tag = "Num";
            };

            m_List.Add(numBtn);
            ChangedRec[2] = new Rectangle(229, 250 + 33 * 2, 100, 30);
            ChangedRec[3] = new Rectangle(229, 250 + 33 * 3, 100, 30);
            MarshallingNumBox = new SS4BListBox();
            MarshallingNumBox.DownPicSize = new Size(25, 30);
            MarshallingNumBox.OutRectangle = ChangedRec[2];
            MarshallingNumBox.BackColor = Brushs.WhiteBrush;
            MarshallingNumBox.TextBrush = Brushs.BlackBrush;
            MarshallingNumBox.SelectTextBrush = Brushs.WhiteBrush;
            MarshallingNumBox.Direction = BoxDirection.Down;
            MarshallingNumBox.EnableBrush = Brushs.GrayBrush;
            MarshallingNumBox.Texts = new[] { "1", "2", "3" };
            MarshallingNumBox.Image = ImageResource.ListBoxButton;
            MarshallingNumBox.SelectBrush = Brushs.BlueBrush;
            MarshallingNumBox.Row = 5;
            MarshallingNumBox.IsVisible = false;
            MarshallingNumBox.Column = 0;
            MarshallingNumBox.Tag = "MarshallingNum";
            m_List.Add(MarshallingNumBox);
        }

        private void InitButton()
        {
            string[] btnStrs = new string[5] { "返回", "上移", "下移", "左移", "右移" };
            for (int i = 0; i < btnStrs.Length; i++)
            {
                var btn = new SS4BButton();
                btn.Text = btnStrs[i];
                btn.BackIamge = ImageResource.back;
                btn.MoseDownImage = ImageResource.btn_y_down;
                btn.MoseUpImage = ImageResource.back;
                btn.OutRec = new Rectangle(738, 104 + 66 * i, 58, 60);
                btn.TextBrush = Brushs.BlackBrush;
                btn.TextFormat = FontInfo.SfCc;
                btn.MouseDown += () => { };
                btn.MouseUp += () =>
                {
                    if (btn.Text.Equals("返回"))
                    {
                        append_postCmd(CmdType.ChangePage, (int)ViewState.WirelessInterface, 0, 0);
                    }
                    else if (btn.Text.Equals("上移"))
                    {
                        if (m_Row > 0)
                        {
                            m_Row--;
                            SelectChanged();
                        }
                    }
                    else if (btn.Text.Equals("下移"))
                    {
                        if (m_Row < 4)
                        {
                            m_Row++;
                            SelectChanged();
                        }
                    }
                    else if (btn.Text.Equals("左移"))
                    {
                        if (m_Col > 0)
                        {
                            m_Col--;
                            SelectChanged();
                        }
                    }
                    else if (btn.Text.Equals("右移"))
                    {
                        if (m_Col < 3)
                        {
                            m_Col++;
                            SelectChanged();
                        }
                    }
                };
                m_ButtonList.Add(btn);
            }
            var configBtn = new SS4BButton();
            configBtn.Text = "确认";
            configBtn.BackIamge = ImageResource.back;
            configBtn.MoseDownImage = ImageResource.btn_y_down;
            configBtn.MoseUpImage = ImageResource.back;
            configBtn.OutRec = new Rectangle(738, 433, 58, 95);
            configBtn.TextBrush = Brushs.BlackBrush;
            configBtn.TextFormat = FontInfo.SfCc;
            configBtn.MouseUp += () =>
            {
                if (configBtn.Text.Equals("确认"))
                {
                    SendData();
                    append_postCmd(CmdType.ChangePage, (int)ViewState.WirelessInterface, 0, 0);
                }
            };
            m_ButtonList.Add(configBtn);
            for (int i = 1; i <= 10; i++)
            {
                var btn = new SS4BButton()
                {
                    Text = i == 10 ? "0" : i.ToString(),
                    BackIamge = ImageResource.btn_b_up,
                    MoseDownImage = ImageResource.btn_y_down,
                    MoseUpImage = ImageResource.btn_b_up,
                    OutRec = new Rectangle(125 + 68 * (i - 1), 539, 60, 60)
                    ,
                    TextBrush = Brushs.WhiteBrush,
                    TextFormat = FontInfo.SfCc,
                };
                btn.MouseDown += () =>
                {
                    var tmp = m_List.Where(w => w.IsSelect && w.Type == BoxType.Text && w.IsVisible).ToList();
                    if (m_Text.Equals("0000"))
                    {
                        m_Text = btn.Text;
                    }
                    else if (m_Text.Length < 4)
                    {
                        m_Text = m_Text + btn.Text;
                    }
                    if (tmp.Count != 0)
                    {
                        tmp[0].SetText(m_Text);
                    }
                    else
                    {
                        m_Text = "0000";
                    }
                };
                btn.MouseUp += () => { };
                m_ButtonList.Add(btn);
            }
        }

        private void ResponseButton()
        {
            if (GetInBoolValue(InBoolKeys.InB设定按下状态))
            {
                append_postCmd(CmdType.ChangePage, (int)ViewState.WirelessInterface, 0, 0);
            }
            else if (GetInBoolValue(InBoolKeys.InB确认按下状态MMIE确认键按下状态))
            {
                SendData();
                append_postCmd(CmdType.ChangePage, (int)ViewState.WirelessInterface, 0, 0);
            }
            else if (GetInBoolValue(InBoolKeys.InB方向向前按下状态MMI向上按键按下状态))
            {
                if (m_Row > 0)
                {
                    m_Row--;
                    SelectChanged();
                }
            }
            else if (GetInBoolValue(InBoolKeys.InB方向向前按下状态MMI向上按键按下状态))
            {
                if (m_Row < 4)
                {
                    m_Row++;
                    SelectChanged();
                }
            }
            else if (GetInBoolValue(InBoolKeys.InB方向向左按下状态MMI向左按键按下状态))
            {
                if (m_Col > 0)
                {
                    m_Col--;
                    SelectChanged();
                }
            }
            else if (GetInBoolValue(InBoolKeys.InB方向向右按下状态MMI向右按键按下状态))
            {
                if (m_Col < 3)
                {
                    m_Col++;
                    SelectChanged();
                }
            }
        }

        private void SelectChanged()
        {
            m_List.ForEach(f => f.Select(m_Row, m_Col));
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                m_Row = 2;
                m_Col = 0;
                m_List.ForEach(f => f.Select(m_Row, m_Col));
            }
        }

        private string m_Text = "0000";
        private int m_Row = 0;
        private int m_Col = 0;

        public override bool mouseDown(Point point)
        {
            if (m_ButtonList.Any(f => f.OnMouseDown(point)))
            {
                return true;
            }
            var list = m_List.FindAll(f => f.IsVisible);
            if (list.Count != 0)
            {
                m_Text = "0000";
                if (list.Any(a => a.DownBox(point)) || list.Any(a => a.MouseDown(point)))
                {
                    return true;
                }
                // m_List.ForEach(f => f.ResetStatus());
                // list.ForEach(f => f.MouseDown(point));
            }

            return true;
        }

        public override bool mouseUp(Point point)
        {
            m_List.Where(w => w.IsVisible).ToList().ForEach(f => f.MouseUp(point));
            m_ButtonList.ForEach(f => f.OnMouseUp(point));
            return true;
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(ImageResource.flag2, m_FlagRect);
            //按钮响应
            ResponseButton();
            g.FillRectangle(m_CentreRectBrush, m_CentreFrameRect);
            m_List.ForEach(f => f.OnDraw(g));
            m_List.ForEach(f => f.OnDrawBox(g));
            m_ButtonList.ForEach(f => f.OnDraw(g));
            m_RectTexts.ForEach(f => f.OnPaint(g));

            base.paint(g);
        }

        private void SendData()
        {
            var dicBool = new Dictionary<int, int>();
            var dicFloat = new Dictionary<int, int>();

            dicBool.Add(GetOutBoolIndex(OutBoolKeys.OutB本车运行方向上行), 0);
            dicBool.Add(GetOutBoolIndex(OutBoolKeys.OutB本车运行方向下行), 0);
            dicBool.Add(GetOutBoolIndex(OutBoolKeys.OutB本车主从设置主控车), 0);
            dicBool.Add(GetOutBoolIndex(OutBoolKeys.OutB本车主从设置从控车), 0);

            var s = m_List.Find(f => f.Tag != null && f.Tag.ToString() == "Main").Text;
            if (s == "主控")
            {
                dicBool[GetOutBoolIndex(OutBoolKeys.OutB本车主从设置主控车)] = 1;
            }
            else
            {
                dicBool[GetOutBoolIndex(OutBoolKeys.OutB本车主从设置从控车)] = 1;
            }
            s = m_List.Find(f => f.Tag != null && f.Tag.ToString() == "Dirction").Text;
            if (s == "上行")
            {
                dicBool[GetOutBoolIndex(OutBoolKeys.OutB本车运行方向上行)] = 1;
            }
            else
            {
                dicBool[GetOutBoolIndex(OutBoolKeys.OutB本车运行方向下行)] = 1;
            }
            dicFloat.Add(GetOutFloatIndex(OutFloatKeys.OutF本车车辆类型), 0);
            dicFloat.Add(GetOutFloatIndex(OutFloatKeys.OutF本车车辆编号), 0);
            dicFloat.Add(GetOutFloatIndex(OutFloatKeys.OutF本车编组序号), 0);
            dicFloat.Add(GetOutFloatIndex(OutFloatKeys.OutF编组数量), 0);
            dicFloat.Add(GetOutFloatIndex(OutFloatKeys.OutF主车类型), 0);
            dicFloat.Add(GetOutFloatIndex(OutFloatKeys.OutF从1类型), 0);
            dicFloat.Add(GetOutFloatIndex(OutFloatKeys.OutF从1编号), 0);
            dicFloat.Add(GetOutFloatIndex(OutFloatKeys.OutF从1编组距离), 0);
            dicFloat.Add(GetOutFloatIndex(OutFloatKeys.OutF从2类型), 0);
            dicFloat.Add(GetOutFloatIndex(OutFloatKeys.OutF从2编号), 0);
            dicFloat.Add(GetOutFloatIndex(OutFloatKeys.OutF从2编组距离), 0);
            dicFloat.Add(GetOutFloatIndex(OutFloatKeys.OutF从3类型), 0);
            dicFloat.Add(GetOutFloatIndex(OutFloatKeys.OutF从3编号), 0);
            dicFloat.Add(GetOutFloatIndex(OutFloatKeys.OutF从3编组距离), 0);
            dicFloat.Add(GetOutFloatIndex(OutFloatKeys.OutF主车编号), 0);
            dicFloat.Add(GetOutFloatIndex(OutFloatKeys.OutF编组序号), 0);
            dicFloat.Add(GetOutFloatIndex(OutFloatKeys.OutF主车车间距), 0);

            dicFloat[GetOutFloatIndex(OutFloatKeys.OutF本车车辆类型)] = 1;
            s = m_List.Find(f => f.Tag != null && f.Tag.ToString() == "CurrentTrainNumber").Text;
            dicFloat[GetOutFloatIndex(OutFloatKeys.OutF本车车辆编号)] = s.ConvertToInt();
            s = m_List.Find(f => f.Tag != null && f.Tag.ToString() == "Num").Text;
            dicFloat[GetOutFloatIndex(OutFloatKeys.OutF编组数量)] = s.ConvertToInt() + 1;

            if (MarshallingNumBox.IsVisible)
            {
                dicFloat[GetOutFloatIndex(OutFloatKeys.OutF编组序号)] = Convert.ToInt32(MarshallingNumBox.Text);
            }

            var tmp = new Dictionary<int, List<SS4BListBox>>();
            foreach (var val in m_CarDicOne)
            {
                foreach (var inVal in val.Value.Where(inVal => inVal.IsVisible))
                {
                    if (!tmp.ContainsKey(val.Key))
                    {
                        tmp.Add(val.Key, new List<SS4BListBox>() { inVal });
                    }
                    else
                    {
                        tmp[val.Key].Add(inVal);
                    }
                }
            }
            foreach (var val in tmp)
            {
                if (val.Key == 1)
                {
                    dicFloat[GetOutFloatIndex(OutFloatKeys.OutF从1类型)] = (int)EnumUtil.FindEnumByDescriptio<CarType>(val.Value.Find(f => f.Column == 1).Text);
                    dicFloat[GetOutFloatIndex(OutFloatKeys.OutF从1编号)] = val.Value.Find(f => f.Column == 2).Text.ConvertToInt();
                    dicFloat[GetOutFloatIndex(OutFloatKeys.OutF从1编组距离)] = val.Value.Find(f => f.Column == 3).Text.ConvertToInt();
                }
                else if (val.Key == 2)
                {
                    dicFloat[GetOutFloatIndex(OutFloatKeys.OutF从2类型)] = (int)EnumUtil.FindEnumByDescriptio<CarType>(val.Value.Find(f => f.Column == 1).Text);
                    dicFloat[GetOutFloatIndex(OutFloatKeys.OutF从2编号)] = val.Value.Find(f => f.Column == 2).Text.ConvertToInt();
                    dicFloat[GetOutFloatIndex(OutFloatKeys.OutF从2编组距离)] = val.Value.Find(f => f.Column == 3).Text.ConvertToInt();
                }
                else if (val.Key == 3)
                {
                    dicFloat[GetOutFloatIndex(OutFloatKeys.OutF从3类型)] = (int)EnumUtil.FindEnumByDescriptio<CarType>(val.Value.Find(f => f.Column == 1).Text);
                    dicFloat[GetOutFloatIndex(OutFloatKeys.OutF从3编号)] = val.Value.Find(f => f.Column == 2).Text.ConvertToInt();
                    dicFloat[GetOutFloatIndex(OutFloatKeys.OutF从3编组距离)] = val.Value.Find(f => f.Column == 3).Text.ConvertToInt();
                }
                else if (val.Key == 10)
                {
                    dicFloat[GetOutFloatIndex(OutFloatKeys.OutF主车类型)] = (int)EnumUtil.FindEnumByDescriptio<CarType>(val.Value.Find(f => f.Column == 1).Text);
                    dicFloat[GetOutFloatIndex(OutFloatKeys.OutF主车编号)] = val.Value.Find(f => f.Column == 2).Text.ConvertToInt();
                    dicFloat[GetOutFloatIndex(OutFloatKeys.OutF主车车间距)] = val.Value.Find(f => f.Column == 3).Text.ConvertToInt();
                }
            }

            foreach (var b in dicBool)
            {
                append_postCmd(CmdType.SetBoolValue, b.Key, b.Value, 0);
            }
            foreach (var f in dicFloat)
            {
                append_postCmd(CmdType.SetFloatValue, f.Key, 0, f.Value);
            }
            //清除数据
            //var timer = new Timer((state =>
            //{
            //    foreach (var i in dicBool)
            //    {
            //        append_postCmd(CmdType.SetBoolValue, i.Key, 0, 0);
            //    }

            //}), null, 500, int.MaxValue);
        }
    }
}