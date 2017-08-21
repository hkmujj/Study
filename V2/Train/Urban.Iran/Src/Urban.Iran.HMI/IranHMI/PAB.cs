using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class PAB : HMIBase
    {
        private FjButtonEx m_LegendBtn;

        /// <summary>
        /// 输入， 大小， 输出 
        /// </summary>
        private List<Tuple<string[], Rectangle, string[]>> m_ItemCollection;

        private Dictionary<string[], Rectangle> m_TextDictionary;
        private Bitmap[,] m_PabMap;
        private List<CommonInnerControlBase> m_Collection;

        private List<SelectableControl> m_ItemBtns;

        private List<FjButtonEx> m_CutControlBtns;

        private SelectableControl m_CurrentSelectedItem;

        public override string GetInfo()
        {
            return "PAB";
        }

        protected override bool Initalize()
        {
            m_Collection = new List<CommonInnerControlBase>();
            m_ItemBtns = new List<SelectableControl>();

            InitItemDictionary();
            InitPic();
            InitTextDictionary();
            InitButton();
            InitGDIRectText();

            InitCutControlBtns();

            UpdateUiobject(CommunicationIndexType.InBool, m_ItemCollection.SelectMany(s => s.Item1.Skip(1)));
            UpdateUiobject(CommunicationIndexType.InFloat, m_TextDictionary.Keys.SelectMany(s => s.Skip(1)));

            UIObj.InBoolList.ForEach(e => append_postCmd(CmdType.SetInBoolValue, e, 1, 0));

            return true;
        }

        private void InitCutControlBtns()
        {
            const int btnX = 330;
            var btn1 = new FjButtonEx(1, GlobleParam.ResManagerText.GetString("Button0013"),
                new Rectangle(GlobleRect.m_LegendbtnRect.X + btnX + 2 + GlobleRect.m_LegendbtnRect.Width,
                    GlobleRect.m_LegendbtnRect.Y,
                    GlobleRect.m_LegendbtnRect.Width, GlobleRect.m_LegendbtnRect.Height));
            var btn2 = new FjButtonEx(1, GlobleParam.ResManagerText.GetString("Button0012"),
                new Rectangle(GlobleRect.m_LegendbtnRect.X + btnX,
                    GlobleRect.m_LegendbtnRect.Y, GlobleRect.m_LegendbtnRect.Width, GlobleRect.m_LegendbtnRect.Height));
            m_CutControlBtns = new List<FjButtonEx>()
            {
                btn1,
                btn2
            };

            btn1.MouseDown += (sender, pt) =>
            {
                if (m_CurrentSelectedItem != null)
                {
                    append_postCmd(CmdType.SetBoolValue,
                        Convert.ToInt32(
                            GlobleParam.Instance.FindOutBoolIndex(
                                ((Tuple<string[], Rectangle, string[]>) (m_CurrentSelectedItem.InnerControl.Tag)).Item3[
                                    0])), 1, 0);
                    SelectItem(null);
                }
            };
            btn2.MouseDown += (sender, pt) =>
            {
                if (m_CurrentSelectedItem != null)
                {
                    append_postCmd(CmdType.SetBoolValue,
                        Convert.ToInt32(
                            GlobleParam.Instance.FindOutBoolIndex(
                                ((Tuple<string[], Rectangle, string[]>) (m_CurrentSelectedItem.InnerControl.Tag)).Item3[
                                    1])), 1, 0);
                    SelectItem(null);
                }
            };
        }

        private void InitGDIRectText()
        {
            m_Collection.Add(new GDIRectText()
            {
                NeedDarwOutline = false,
                BackColorVisible = true,
                BKImage = new Bitmap(RecPath + "\\frame/pab.jpg"),
                OutLineRectangle = new Rectangle(120, 127, 632, 271)
            });
            foreach (var key in m_TextDictionary.Keys)
            {
                m_Collection.Add(new GDIRectText()
                {
                    Text = "",
                    TextBrush = GdiCommon.MediumGreyBrush,
                    TextFormat = GdiCommon.CenterFormat,
                    NeedDarwOutline = true,
                    BackColorVisible = true,
                    OutLinePen = GdiCommon.GreyBluePen,
                    BkColor = GdiCommon.DarkBlueBrush.Color,
                    DrawFont = GdiCommon.Txt8Font,
                    Tag = key,
                    OutLineRectangle = m_TextDictionary[key],
                    RefreshAction = o => RefreshText((GDIRectText) o),
                });
            }

            var selectedPen = new Pen(Color.White, 3);
            foreach (var item in m_ItemCollection)
            {
                var btn = new GDIButton()
                {
                    OutLineRectangle = item.Item2,
                    Tag = item,
                    RefreshAction = o => RefreshItem((GDIButton) o),
                };
                btn.BtnBehavierStrategy = new IranImageButtonBehavier(btn);
                btn.TextControl.Visible = false;

                if (item.Item3 != null)
                {
                    btn.ButtonDownEvent = SelectToCut;
                }

                m_ItemBtns.Add(new SelectableControl(btn) {SelectedPen = selectedPen});
            }
        }

        private void SelectToCut(object sender, EventArgs eventArgs)
        {
            var itm = m_ItemBtns.Find(f => f.InnerControl == sender);
            SelectItem(itm);
        }

        private void SelectItem(SelectableControl btn)
        {
            if (m_CurrentSelectedItem != null)
            {
                m_CurrentSelectedItem.Selected = false;
            }
            m_CurrentSelectedItem = btn;
            if (m_CurrentSelectedItem != null)
            {
                m_CurrentSelectedItem.Selected = true;
            }
        }

        private void InitButton()
        {
            m_LegendBtn = new FjButtonEx(1, GlobleParam.ResManager.GetString("String32"), GlobleRect.m_LegendbtnRect);
            m_LegendBtn.MouseDown += LegendBtnMouseDown;
        }

        private void InitTextDictionary()
        {
            var itemSize = new Size(26, 20);
            var itemSize1 = new Size(41, 19);
            m_TextDictionary = new Dictionary<string[], Rectangle>
            {
                {new[] {"1", "TC1 蓄电池电压"}, new Rectangle(227, 154, itemSize.Width, itemSize.Height)},
                {new[] {"2", "TC1 蓄电池电流"}, new Rectangle(200, 178, itemSize1.Width, itemSize1.Height)},
                {new[] {"3", "TC1 蓄电池温度"}, new Rectangle(200, 200, itemSize1.Width, itemSize1.Height)},
                {new[] {"1", "TC2 蓄电池电压"}, new Rectangle(617, 154, itemSize.Width, itemSize.Height)},
                {new[] {"2", "TC2 蓄电池电流"}, new Rectangle(626, 183, itemSize1.Width + 3, itemSize1.Height)},
                {new[] {"3", "TC2 蓄电池温度"}, new Rectangle(626, 205, itemSize1.Width + 3, itemSize1.Height)},
                {new[] {"2", "TC1 BCM电流"}, new Rectangle(155, 179, itemSize1.Width, itemSize1.Height)},
                {new[] {"2", "TC2 BCM电流"}, new Rectangle(674, 183, itemSize1.Width + 3, itemSize1.Height)},
                {new[] {"1", "TC1 ACM供电供电电压"}, new Rectangle(188, 284, itemSize1.Width + 3, itemSize1.Height - 2)},
                {new[] {"2", "TC1 ACM供电供电电流"}, new Rectangle(188, 304, itemSize1.Width + 3, itemSize1.Height - 2)},
                {new[] {"1", "M ACM供电供电电压"}, new Rectangle(393, 197, itemSize1.Width + 3, itemSize1.Height - 2)},
                {new[] {"2", "M ACM供电供电电流"}, new Rectangle(393, 217, itemSize1.Width + 3, itemSize1.Height - 2)},
                {new[] {"1", "TC2 ACM供电供电电压"}, new Rectangle(639, 284, itemSize1.Width + 3, itemSize1.Height - 2)},
                {new[] {"2", "TC2 ACM供电供电电流"}, new Rectangle(639, 304, itemSize1.Width + 3, itemSize1.Height - 2)},
                {new[] {"1", "MP1  MCM 电压"}, new Rectangle(265, 295, itemSize1.Width + 3, itemSize1.Height - 2)},
                {new[] {"4", "MP1  MCM 牵引力"}, new Rectangle(265, 362, itemSize1.Width + 3, itemSize1.Height - 2)},
                {new[] {"2", "MP1  MCM 电流"}, new Rectangle(265, 379, itemSize1.Width + 3, itemSize1.Height - 2)},
                {new[] {"1", "M1  MCM 电压"}, new Rectangle(392, 295, itemSize1.Width + 3, itemSize1.Height - 2)},
                {new[] {"4", "M1  MCM 牵引力"}, new Rectangle(392, 362, itemSize1.Width + 3, itemSize1.Height - 2)},
                {new[] {"2", "M1  MCM 电流"}, new Rectangle(392, 379, itemSize1.Width + 3, itemSize1.Height - 2)},
                {new[] {"1", "MP2  MCM 电压"}, new Rectangle(562, 295, itemSize1.Width + 3, itemSize1.Height - 2)},
                {new[] {"4", "MP2  MCM 牵引力"}, new Rectangle(562, 362, itemSize1.Width + 3, itemSize1.Height - 2)},
                {new[] {"2", "MP2  MCM 电流"}, new Rectangle(562, 379, itemSize1.Width + 3, itemSize1.Height - 2)},
                {new[] {"2", "MP1 受电弓电流"}, new Rectangle(290, 202, itemSize1.Width + 3, itemSize1.Height - 2)},
                {new[] {"2", "MP2 受电弓电流"}, new Rectangle(535, 202, itemSize1.Width + 3, itemSize1.Height - 2)}
            };
        }

        private void InitPic()
        {
            m_PabMap = new Bitmap[9, 5];

            m_PabMap[0, 1] = new Bitmap(RecPath + "\\frame/pLowR.png");
            m_PabMap[0, 2] = new Bitmap(RecPath + "\\frame/pFaultyR.jpg");
            m_PabMap[0, 3] = new Bitmap(RecPath + "\\frame/psUnknownR.jpg");
            m_PabMap[0, 4] = new Bitmap(RecPath + "\\frame/pUpR.jpg");

            m_PabMap[1, 1] = new Bitmap(RecPath + "\\frame/pLow.png");
            m_PabMap[1, 2] = new Bitmap(RecPath + "\\frame/pFaulty.jpg");
            m_PabMap[1, 3] = new Bitmap(RecPath + "\\frame/psUnknown.jpg");
            m_PabMap[1, 4] = new Bitmap(RecPath + "\\frame/pUp.jpg");

            m_PabMap[2, 1] = new Bitmap(RecPath + "\\frame/lcOpenL.jpg");
            m_PabMap[2, 2] = new Bitmap(RecPath + "\\frame/lcClosedL.jpg");

            m_PabMap[3, 1] = new Bitmap(RecPath + "\\frame/lcOpenR.jpg");
            m_PabMap[3, 2] = new Bitmap(RecPath + "\\frame/lcClosedR.jpg");

            m_PabMap[4, 1] = new Bitmap(RecPath + "\\frame/lcOpen.jpg");
            m_PabMap[4, 2] = new Bitmap(RecPath + "\\frame/lcClosed.jpg");

            m_PabMap[5, 1] = new Bitmap(RecPath + "\\frame/wpsDisconnected.png");
            m_PabMap[5, 2] = new Bitmap(RecPath + "\\frame/wpConnected.jpg");
            m_PabMap[5, 3] = new Bitmap(RecPath + "\\frame/wpGrounded.jpg");
            m_PabMap[5, 4] = new Bitmap(RecPath + "\\frame/wpsFaulty.png");

            m_PabMap[6, 1] = new Bitmap(RecPath + "\\frame/bcmOk.png");
            m_PabMap[6, 2] = new Bitmap(RecPath + "\\frame/bcmFaulty.jpg");
            m_PabMap[6, 3] = new Bitmap(RecPath + "\\frame/bcmUnkown.jpg");

            m_PabMap[7, 1] = new Bitmap(RecPath + "\\frame/acmOff.jpg");
            m_PabMap[7, 2] = new Bitmap(RecPath + "\\frame/acmCutout.jpg");
            m_PabMap[7, 3] = new Bitmap(RecPath + "\\frame/acmFaulty.jpg");
            m_PabMap[7, 4] = new Bitmap(RecPath + "\\frame/acmInOp.jpg");

            m_PabMap[8, 1] = new Bitmap(RecPath + "\\frame/mcmOff.jpg");
            m_PabMap[8, 2] = new Bitmap(RecPath + "\\frame/mcmCutout.jpg");
            m_PabMap[8, 3] = new Bitmap(RecPath + "\\frame/mcmFaulty.jpg");
            m_PabMap[8, 4] = new Bitmap(RecPath + "\\frame/mcmInOp.jpg");
        }

        private void InitItemDictionary()
        {
            m_ItemCollection = new List<Tuple<string[], Rectangle, string[]>>()
            {
                new Tuple<string[], Rectangle, string[]>(

                    new[]
                    {
                        "1", "MP1车  受电弓低压（Low Voltage）", "MP1车  受电弓故障（Pantorgraph faulty）",
                        "MP1车  受电弓降（Pantorgraph down）",
                        "MP1车  受电弓升（Pantorgraph up）"
                    },
                    new Rectangle(263, 129, 48, 48), null),
                new Tuple<string[], Rectangle, string[]>(
                    new[]
                    {
                        "0", "MP2车  受电弓低压（Low Voltage）", "MP2车  受电弓故障（Pantorgraph faulty）",
                        "MP2车  受电弓降（Pantorgraph down）",
                        "MP2车  受电弓升（Pantorgraph up）"
                    },
                    new Rectangle(561, 129, 48, 48), null),
                new Tuple<string[], Rectangle, string[]>(
                    new[]
                    {
                        "2", "TC1车  蓄电池线路断路器BATCB LVB断开（Line contactor open）",
                        "TC1车  蓄电池线路断路器BATCB LVB闭合（Line contactor closed）"
                    },
                    new Rectangle(236, 129, 20, 20), null),
                new Tuple<string[], Rectangle, string[]>(
                    new[]
                    {
                        "2", "TC1车  辅助逆变器线路断路器ACMCB LVB断开（Line contactor open）",
                        "TC1车  辅助逆变器线路断路器ACMCB LVB闭合（Line contactor closed）"
                    },
                    new Rectangle(150, 246, 32, 32), null),

                new Tuple<string[], Rectangle, string[]>(
                    new[]
                    {
                        "3", "TC2车  蓄电池线路断路器BATCB LVB断开（Line contactor open）",
                        "TC2车  蓄电池线路断路器BATCB LVB闭合（Line contactor closed）"
                    },
                    new Rectangle(616, 128, 20, 20), null),
                new Tuple<string[], Rectangle, string[]>(
                    new[]
                    {
                        "3", "TC2车  辅助逆变器线路断路器ACMCB LVB断开（Line contactor open）",
                        "TC2车  辅助逆变器线路断路器ACMCB LVB闭合（Line contactor closed）"
                    },
                    new Rectangle(692, 244, 32, 32), null),
                new Tuple<string[], Rectangle, string[]>(
                    new[]
                    {
                        "4", "MP1车  线路断路器HSCB1 LVB断开（Line contactor open）",
                        "MP1车  线路断路器HSCB1 LVB闭合（Line contactor closed）"
                    },
                    new Rectangle(263, 237, 48, 48), null),
                new Tuple<string[], Rectangle, string[]>(
                    new[]
                    {
                        "4", "MP1车  线路断路器HSCB2 LVB断开（Line contactor open）",
                        "MP1车  线路断路器HSCB2 LVB闭合（Line contactor closed）"
                    },
                    new Rectangle(329, 237, 48, 48), null),
                new Tuple<string[], Rectangle, string[]>(
                    new[]
                    {
                        "4", "MP2车  线路断路器HSCB1 LVB断开（Line contactor open）",
                        "MP2车  线路断路器HSCB1 LVB闭合（Line contactor closed）"
                    },
                    new Rectangle(495, 237, 48, 48), null),
                new Tuple<string[], Rectangle, string[]>(
                    new[]
                    {
                        "4", "MP2车  线路断路器HSCB2 LVB断开（Line contactor open）",
                        "MP2车  线路断路器HSCB2 LVB闭合（Line contactor closed）"
                    },
                    new Rectangle(561, 237, 48, 48), null),
                new Tuple<string[], Rectangle, string[]>(
                    new[]
                    {
                        "5", "MP1车  车间供电断开（workshop power supply disconnected）",
                        "MP1车  车间供电闭合（workshop power supply connected）", "MP1车  车间供电接地（workshop power supply grounded）",
                        "MP1车  车间供电故障（workshop power supply faulty）"
                    },
                    new Rectangle(328, 129, 48, 48), null),
                new Tuple<string[], Rectangle, string[]>(
                    new[]
                    {
                        "5", "MP2车  车间供电断开（workshop power supply disconnected）",
                        "MP2车  车间供电闭合（workshop power supply connected）", "MP2车  车间供电接地（workshop power supply grounded）",
                        "MP2车  车间供电故障（workshop power supply faulty）"
                    },
                    new Rectangle(495, 129, 48, 48), null),
                new Tuple<string[], Rectangle, string[]>(
                    new[] {"6", "TC1车   BCM正常（BCM OK）", "TC1车   BCM故障（BCM faulty）", "TC1车   BCM切除（BCM off）"},
                    new Rectangle(153, 129, 48, 48), null),
                new Tuple<string[], Rectangle, string[]>(
                    new[] {"6", "TC2车   BCM正常（BCM OK）", "TC2车   BCM故障（BCM faulty）", "TC2车   BCM切除（BCM off）"},
                    new Rectangle(673, 129, 48, 48), null),
                new Tuple<string[], Rectangle, string[]>(
                    new[]
                    {
                        "7", "TC1车  辅助逆变器ACM关闭（ACM off）", "TC1车  辅助逆变器ACM切除（ACM cut out）",
                        "TC1车  辅助逆变器ACM故障（ACM faulty）",
                        "TC1车  辅助逆变器ACM工作（ACM in op。）"
                    },
                    new Rectangle(188, 236, 48, 48), new string[]
                    {

                        "ACM-CUT-IN(切除)-TC1-1车",
                        "ACM-CUT-OUT(复位)-TC1-1车",
                    }),
                new Tuple<string[], Rectangle, string[]>(
                    new[]
                    {
                        "7", "M车  辅助逆变器ACM关闭（ACM off）", "M车  辅助逆变器ACM切除（ACM cut out）", "M车  辅助逆变器ACM故障（ACM faulty）",
                        "M车  辅助逆变器ACM工作（ACM in op。）"
                    },
                    new Rectangle(390, 237, 48, 48), new[]
                    {
                        "ACM-CUT-IN(切除)M-3车",
                        "ACM-CUT-OUT(复位)-M-3车",
                    }),
                new Tuple<string[], Rectangle, string[]>(
                    new[]
                    {
                        "7", "TC2车  辅助逆变器ACM关闭（ACM off）", "TC2车  辅助逆变器ACM切除（ACM cut out）",
                        "TC2车  辅助逆变器ACM故障（ACM faulty）",
                        "TC2车  辅助逆变器ACM工作（ACM in op。）"
                    },
                    new Rectangle(638, 236, 48, 48), new string[]
                    {
                        "ACM-CUT-IN(切除)-TC2-5车",
                        "ACM-CUT-OUT(复位)-TC2-5车",
                    }),
                new Tuple<string[], Rectangle, string[]>(
                    new[]
                    {
                        "8", "MP1车  牵引逆变器MCM关闭（MCM off）", "MP1车  牵引逆变器MCM切除（MCM cut out）",
                        "MP1车  牵引逆变器MCM故障（MCM faulty）",
                        "MP1车  牵引逆变器MCM工作（MCM in op）"
                    },
                    new Rectangle(263, 315, 48, 48), new string[]
                    {
                        "MCM-CUT-IN(切除)-MP1-2车",
                        "MCM-CUT-OUT(复位)-MP1-2车",
                    }),
                new Tuple<string[], Rectangle, string[]>(
                    new[]
                    {
                        "8", "M车  牵引逆变器MCM关闭（MCM off）", "M车  牵引逆变器MCM切除（MCM cut out）", "M车  牵引逆变器MCM故障（MCM faulty）",
                        "M车  牵引逆变器MCM工作（MCM in op）"
                    },
                    new Rectangle(390, 314, 48, 48), new string[]
                    {
                        "MCM-CUT-IN(切除)-M1-3车",
                        "MCM-CUT-OUT(复位)-M1-3车",
                    }),
                new Tuple<string[], Rectangle, string[]>(
                    new[]
                    {
                        "3", "M车  辅助逆变器线路断路器ACMCB LVB断开（Line contactor open）",
                        "M车  辅助逆变器线路断路器ACMCB LVB闭合（Line contactor closed）"
                    },
                    new Rectangle(445, 245, 32, 32), null),
                new Tuple<string[], Rectangle, string[]>(
                    new[]
                    {
                        "8", "MP2车  牵引逆变器MCM关闭（MCM off）", "MP2车  牵引逆变器MCM切除（MCM cut out）",
                        "MP2车  牵引逆变器MCM故障（MCM faulty）",
                        "MP2车  牵引逆变器MCM工作（MCM in op）"
                    },
                    new Rectangle(560, 315, 48, 48), new string[]
                    {
                        "MCM-CUT-IN(切除)-MP2-4车",
                        "MCM-CUT-OUT(复位)-MP2-4车",
                    }
                    )
            };
            m_ItemCollection =
                m_ItemCollection.Select(
                    s => new Tuple<string[], Rectangle, string[]>(s.Item1, Rectangle.Inflate(s.Item2, 1, 1), s.Item3))
                    .ToList();
        }

        private void RefreshText(GDIRectText item)
        {
            var names = (string[]) item.Tag;
            if (names.Length < 2)
            {
                return;
            }
            var temp = FloatList[Convert.ToInt32(GlobleParam.Instance.FindInFloatIndex(names[1]))].ToString("F0");
            if (names[0].Equals("1"))
            {
                item.Text = temp.ToString(CultureInfo.InvariantCulture) + "V";
            }
            else if (names[0].Equals("2"))
            {
                item.Text = temp.ToString(CultureInfo.InvariantCulture) + "A";
            }
            else if (names[0].Equals("3"))
            {
                item.Text = temp.ToString(CultureInfo.InvariantCulture) + "°";
            }
            else if (names[0].Equals("4"))
            {
                item.Text = temp.ToString(CultureInfo.InvariantCulture) + "kN";
            }
        }

        private void RefreshItem(GDIButton item)
        {
            var names = ((Tuple<string[], Rectangle, string[]>) item.Tag).Item1;
            if (names.Length < 2)
            {
                return;
            }
            for (var i = 1; i < names.Length; i++)
            {
                if (!BoolList[GlobleParam.Instance.FindInBoolIndex(names[i])])
                {
                    continue;
                }
                item.BackImage = m_PabMap[Convert.ToInt32(names[0]), i];
            }
        }

        public override void paint(Graphics g)
        {
            GlobleParam.Instance.CurrentIranViewIndex = (IranViewIndex) 6;

            m_Collection.ForEach(e => e.OnPaint(g));

            m_ItemBtns.ForEach(e => e.OnPaint(g));

            m_LegendBtn.OnDraw(g);
            if (BoolList[UIObj.InBoolList[6]] && m_CurrentSelectedItem != null)
            {
                m_CutControlBtns.ForEach(e => e.OnDraw(g));
            }
        }

        public override bool mouseDown(Point point)
        {
            var controlBtn = m_CutControlBtns.FirstOrDefault(a => a.IsVisible(point));
            if (controlBtn != null)
            {
                if (BoolList[UIObj.InBoolList[6]] && m_CurrentSelectedItem != null)
                {
                    controlBtn.OnMouseDown(point);
                }
                return true;
            }
            SelectItem(null);
            if (m_ItemBtns.Any(a => a.OnMouseDown(point)))
            {
                return true;
            }
            if (m_LegendBtn.IsVisible(point))
            {
                m_LegendBtn.OnMouseDown(point);
                return true;
            }
            return true;
        }

        public override bool mouseUp(Point point)
        {
            m_ItemBtns.ForEach(e =>
            {
                var itm = ((Tuple<string[], Rectangle, string[]>) e.InnerControl.Tag);
                if (itm.Item3 != null)
                {
                    foreach (var inx in itm.Item3)
                    {
                        append_postCmd(CmdType.SetBoolValue, Convert.ToInt32(GlobleParam.Instance.FindOutBoolIndex(inx)),
                            0,
                            0);
                    }
                }
            });
            return false;
        }

        private void LegendBtnMouseDown(FjButtonEx btnSender, Point pt)
        {
            ChangedPage(IranViewIndex.PABLegend);
            ;
        }
    }
}