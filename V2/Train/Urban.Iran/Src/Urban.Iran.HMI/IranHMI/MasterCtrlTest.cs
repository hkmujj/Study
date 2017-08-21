using System;
using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using CommonUtil.Model;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using MMI.Facility.Interface.Attribute;
using Urban.Iran.HMI.Common;
using Urban.Iran.HMI.Controls;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class MasterCtrlTest : HMIBase
    {
        private Rectangle[] m_RectArr;
        private Bitmap[] m_BmpArr;
        private List<CommonInnerControlBase> m_LstCollection;
        private readonly string[] m_Str0 = { "MCM/Mp1", "MCM/M", "MCM/Mp2" };
        private readonly string[] m_Str1 = { "1", "2", "3", "FWd", "Rev" };
        private readonly string[] m_Str2 = { "Driving direction", "Master controller", "Legend", "Passed", "Failed", "Active", "Inactive" };
        private readonly string[] m_Str4 = { "ATO", "Fwd", "Rev" };
        private readonly Point[] m_Point = { new Point(486, 105), new Point(486, 147), new Point(486, 197), new Point(486, 247), new Point(486, 284), new Point(486, 317) };
        private readonly string[] m_Str3 = { "100% Traction", "50% Traction", "Coasting", "50% Brake", "100% Brake", "Fast brake" };
        private GDIButton m_Botton;
        private Color m_TextColor = GdiCommon.GrayWhitePen.Color;

        public override string GetInfo()
        {
            return "MasterCtrlTest";
        }


        protected override bool Initalize()
        {
            m_RectArr = new[]
                      {
                          new Rectangle(25, 72, 117, 22),
                          new Rectangle(25, 180, 117, 22),
                          new Rectangle(25, 288, 117, 22),
                          new Rectangle(25, 112, 21, 22),
                          new Rectangle(73, 112, 21, 22),
                          new Rectangle(121, 112, 21, 22),
                          new Rectangle(49, 155, 21, 22),
                          new Rectangle(97, 155, 21, 22),
                          new Rectangle(25, 220, 21, 22),
                          new Rectangle(73, 220, 21, 22),
                          new Rectangle(121, 220, 21, 22),
                          new Rectangle(49, 263, 21, 22),
                          new Rectangle(97, 263, 21, 22),
                          new Rectangle(25, 328, 21, 22),
                          new Rectangle(73, 328, 21, 22),
                          new Rectangle(121, 328, 21, 22),
                          new Rectangle(49, 371, 21, 22),
                          new Rectangle(97, 371, 21, 22),

                          new Rectangle(19, 95, 33, 12),
                          new Rectangle(67, 95, 33, 12),
                          new Rectangle(115, 95, 33, 12),
                          new Rectangle(43, 141, 33, 12),
                          new Rectangle(91, 141, 33, 12),

                          new Rectangle(19, 203, 33, 12),
                          new Rectangle(67, 203, 33, 12),
                          new Rectangle(115, 203, 33, 12),
                          new Rectangle(43, 249, 33, 12),
                          new Rectangle(91, 249, 33, 12),

                          new Rectangle(19, 311, 33, 12),
                          new Rectangle(67, 311, 33, 12),
                          new Rectangle(115, 311, 33, 12),
                          new Rectangle(43, 357, 33, 12),
                          new Rectangle(91, 357, 33, 12), //32

                          new Rectangle(200, 170, 144, 14),
                          new Rectangle(360, 75, 150, 14),
                          new Rectangle(685, 75, 65, 14),
                          new Rectangle(641, 99, 65, 14),
                          new Rectangle(722, 99, 65, 14),
                          new Rectangle(655, 203, 65, 14),
                          new Rectangle(723, 203, 68, 14),
                          new Rectangle(0, 62, 800, 404),
                          new Rectangle(208, 110, 128, 64),

                          new Rectangle(240,100,60,60), 
                          new Rectangle(210,190,60,60),
                          new Rectangle(275,190,60,60)

                      };
            m_BmpArr = new[]
                     {
                         new Bitmap(RecPath + "\\frame/actived.jpg"),
                         new Bitmap(RecPath + "\\frame/inActived.jpg"),
                         new Bitmap(RecPath + "\\frame/drDirection.jpg"),
                         new Bitmap(RecPath + "\\frame/controller.jpg"),
                         new Bitmap(RecPath + "\\frame/passed.jpg"),
                         new Bitmap(RecPath + "\\frame/failed.jpg"),
                         new Bitmap(RecPath + "\\frame/MasterCtrl.jpg")
                     };
            m_LstCollection = new List<CommonInnerControlBase>();
            m_Botton = ButtonFactory.CreateButton(new Rectangle(670, 300, 97, 62),
                GlobleParam.ResManagerText.GetString("Button0005"), btn =>
                {
                    btn.Tag = new object[] { GetShadowTextButtonBehavier(btn), GetNormalButtonBehavierStrategy(btn) };
                    btn.RefreshAction = o => RerershButton((GDIButton)o);
                });
            for (int i = 0; i < 3; i++)
            {
                m_LstCollection.Add(new GDIRectText
                {
                    Text = m_Str0[i],
                    DrawFont = GdiCommon.Txt12Font,
                    TextColor = GdiCommon.MediumGreyBrush.Color,
                    OutLineRectangle = m_RectArr[i],
                    NeedDarwOutline = false,
                    TextFormat = GdiCommon.CenterFormat,
                    BackColorVisible = false
                });
            }
            for (int i = 0; i < 3; i++)
            {
                var tmp = i == 0 ? 18 : i == 1 ? 23 : 28;
                for (int j = 0; j < 5; j++)
                {
                    m_LstCollection.Add(new GDIRectText
                    {
                        Text = m_Str1[j],
                        DrawFont = GdiCommon.Txt12Font,
                        TextColor = GdiCommon.MediumGreyBrush.Color,
                        OutLineRectangle = m_RectArr[tmp + j],
                        NeedDarwOutline = false,
                        TextFormat = GdiCommon.CenterFormat,
                        BackColorVisible = false
                    });
                    m_LstCollection.Add(new GDIRectText
                    {
                        Text = "",
                        BKImage = m_BmpArr[1],
                        OutLineRectangle = m_RectArr[i * 5 + j + 3],
                        NeedDarwOutline = false,
                        RefreshAction = o => RefrshImage((GDIRectText)o),
                        Tag = m_Str0[i] + " " + m_Str1[j] + " " + "Active"
                    });
                }
            }
            for (int i = 0; i < m_Str2.Length; i++)
            {
                m_LstCollection.Add(new GDIRectText
                {
                    Text = m_Str2[i],
                    DrawFont = GdiCommon.Txt12Font,
                    TextColor = GdiCommon.MediumGreyBrush.Color,
                    OutLineRectangle = m_RectArr[33 + i],
                    NeedDarwOutline = false,
                    TextFormat = GdiCommon.CenterFormat,
                    BackColorVisible = false
                });

            }
            var size = new Size(120, 25);
            var itemSize = new Size(40, 24);
            for (int i = 0; i < m_Str3.Length; i++)
            {
                m_LstCollection.Add(new GDIRectText
                {
                    Text = m_Str3[i],
                    DrawFont = GdiCommon.Txt12Font,
                    TextColor = GdiCommon.MediumGreyBrush.Color,
                    OutLineRectangle = new Rectangle(m_Point[i], size),
                    NeedDarwOutline = false,
                    TextFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Center
                    },
                    BackColorVisible = false
                });
                m_LstCollection.Add(new GDITwoOutLineRec(2)
                {
                    Tag = new[]
                    {
                        m_Str3[i]+" Passed",
                        m_Str3[i]+" Failed"
                    },
                    OutLineRectangle = new Rectangle(new Point(m_Point[i].X - 45, m_Point[i].Y), itemSize),
                    NeedDarwOutline = true,
                    OutLinePen = GdiCommon.GrayWhitePen,
                    RefreshAction = o => RefreshClor((GDITwoOutLineRec)o)
                });
            }
            m_LstCollection.Add(new GDITwoOutLineRec(2)
            {
                Text = "0%",
                OutLineRectangle = new Rectangle(382, 314, 59, 27),
                BackColorVisible = false,
                NeedDarwOutline = true,
                OutLinePen = GdiCommon.GrayWhitePen,
                TextFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                },
                Tag = "Master controller value",
                RefreshAction = o =>
                {
                    ((GDITwoOutLineRec)o).Text =
                        FloatList[
                            GlobleParam.Instance.FindInFloatIndex((string)((GDITwoOutLineRec)o).Tag)]
                            .ToString("F0") + "%";
                }
            });

            for (int i = 0; i < m_Str4.Length; i++)
            {
                m_LstCollection.Add(new GDITwoOutLineRec(2)
                  {
                      Text = m_Str4[i],
                      DrawFont = GdiCommon.Txt15Font,
                      TextColor = GdiCommon.MediumGreyBrush.Color,
                      TextFormat = new StringFormat
                      {
                          Alignment = StringAlignment.Center,
                          LineAlignment = StringAlignment.Center
                      },
                      OutLinePen = GdiCommon.GrayWhitePen,
                      NeedDarwOutline = true,
                      OutLineRectangle = m_RectArr[42 + i],
                      RefreshAction = o => RefreshClor((GDITwoOutLineRec)o),
                      Tag = new[]
                            {
                                m_Str4[i]+" Passed",
                                m_Str4[i]+" Failed"
                            },
                  });
            }
            m_LstCollection.Add(new GDIProgress(Direction.Up)
            {
                BackgroundColor = Color.FromArgb(90, 87, 190),
                Location = new Point(415, 108),
                Size = new Size(19, 100),
                RefreshAction = o => RefreshValue((GDIProgress)o),
                Tag = "Master controller value",
                MaxValue = 100,
                CurrentValue = 0

            });
            m_LstCollection.Add(new GDIProgress(Direction.Down)
            {
                BackgroundColor = Color.FromArgb(90, 87, 190),
                Location = new Point(415, 208),
                Size = new Size(19, 100),
                RefreshAction = o => RefreshValue((GDIProgress)o),
                Tag = "Master controller value",
                MaxValue = 100,
                CurrentValue = 0

            });
            m_LstCollection.Add(new GDIRectText
            {
                Text = GlobleParam.ResManagerText.GetString("Text0030"),
                DrawFont = GdiCommon.Txt12Font,
                TextColor = GdiCommon.MediumGreyBrush.Color,
                OutLineRectangle = new Rectangle(245, 408, 300, 50),
                NeedDarwOutline = false ,
                TextFormat = GdiCommon.CenterFormat,
                BackColorVisible = false
            });
            return true;
        }
        private void RerershButton(GDIButton item)
        {
            var name = item.Tag as object[];
            //if (name.Length < 2)
            //{
            //    return;
            //}
            //var tmpActive = BoolList[Convert.ToInt32(GlobleParam.Instance.InBoolTable.Rows.Find(name[0])[1])];
            item.BtnBehavierStrategy = (IBtnBehavierStrategy)name[0];// : (IBtnBehavierStrategy)name[1];
        }
        private void RefreshValue(GDIProgress item)
        {
            var name = item.Tag as string;
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            var tmp = FloatList[GlobleParam.Instance.FindInFloatIndex(name)];
            item.CurrentValue = 0;
            if (tmp > 0 && item.Direction == Direction.Up)
            {
                item.CurrentValue = Math.Abs(tmp);
            }
            if (tmp < 0 && item.Direction == Direction.Down)
            {
                item.CurrentValue = Math.Abs(tmp);
            }
            if (tmp == 0)
            {
                item.CurrentValue = 0;
            }
        }

        private void RefreshClor(GDITwoOutLineRec item)
        {
            var names = item.Tag as string[];
            if (names.Length < 2)
            {
                return;
            }
            if (BoolList[GlobleParam.Instance.FindInBoolIndex(names[0])])
            {
                item.BkColor = Color.Blue;
                item.BackColorVisible = true;
            }
            else if (BoolList[GlobleParam.Instance.FindInBoolIndex(names[1])])
            {
                item.BkColor = Color.Red;
                item.BackColorVisible = true;
            }
            else
            {
                item.BackColorVisible = false;
            }
        }
        private void RefrshImage(GDIRectText item)
        {
            var names = item.Tag as string;
            if (names.Length < 1)
            {
                LogMgr.Info("this tag < 1");
            }
            item.BKImage = BoolList[GlobleParam.Instance.FindInBoolIndex(names)] ? m_BmpArr[0] : m_BmpArr[1];
        }
        public override void paint(Graphics g)
        {
            g.DrawImage(m_BmpArr[6], m_RectArr[40]);
            m_LstCollection.ForEach(f => f.OnPaint(g));
            m_Botton.OnPaint(g);
        }

        private IBtnBehavierStrategy GetShadowTextButtonBehavier(GDIButton btn)
        {
            btn.TextColor = m_TextColor;
            return new IranShadowTextButtonBehavier(btn)
            {
                ShadowTextBrush = Brushes.Black,
                LocationTraslateAction = point => point.Translate(-2, -2)
            };
        }

        private IBtnBehavierStrategy GetNormalButtonBehavierStrategy(GDIButton btn)
        {
            return new IranHmiDefaultButtonBehavier(btn);
        }
    }
}