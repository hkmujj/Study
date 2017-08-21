using System;
using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using CommonUtil.Model;
using CommonUtil.Util.Extension;
using MMI.Facility.Interface.Attribute;
using Urban.Iran.HMI.Common;
using Urban.Iran.HMI.Controls;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class FlTest : HMIBase
    {
        private Bitmap m_Bitmap;
        private Rectangle[] m_RectArr;
        private Font m_Txt12Font;
        private Point[] m_PtArr;
        
        
        private List<CommonInnerControlBase> m_Collection;
        private List<GDIButton> m_Button;
        private readonly Color m_TextColor = GdiCommon.GrayWhitePen.Color;

        private DateTime m_Tiem1 = new DateTime();
        private DateTime m_Tiem2 = new DateTime();

        private const float Result1 = 0;
        private const float Result2 = 0;
        private const float Bar = 4.5f;

        public FlTest()
        {

        }

        public override string GetInfo()
        {
            return "FLTest";
        }

        protected override bool Initalize()
        {
            m_Bitmap = new Bitmap(RecPath + "\\frame/FLTest.jpg");
            m_RectArr = new[]
                      {
                          new Rectangle(0, 167, 800, 305),
                          new Rectangle(105, 174, 125, 50),
                          new Rectangle(105, 300, 125, 23),
                          new Rectangle(233, 328, 95, 23),
                          new Rectangle(340, 175, 110, 15),
                          new Rectangle(560, 174, 125, 50),
                          new Rectangle(560, 244, 130, 27),
                          new Rectangle(560, 295, 125, 27),
                          new Rectangle(690, 324, 95, 23),

                          new Rectangle(105, 225, 125, 23),
                          new Rectangle(105, 326, 125, 23),
                          new Rectangle(561, 225, 125, 23),
                          new Rectangle(561, 276, 125, 23),
                          new Rectangle(561, 326, 125, 23),
                          new Rectangle(347, 403, 108, 18)
                      };
            m_Txt12Font = new Font("Arial", 12);
            m_PtArr = new[]
                    {
                        new Point(5, 450),
                        new Point(350, 440)
                    };
            InitButton();
            InitGDIRectText();


            return true;
        }

        private void InitGDIRectText()
        {
            m_Collection = new List<CommonInnerControlBase>();
            InitText();
            InitValueText();
            m_Collection.Add(new GDIProgress(Direction.Up)
            {
                BackgroundColor = Color.FromArgb(90, 87, 190),
                Location = new Point(375, 211),
                MaxValue = 9,
                Size = new Size(40, 187),
                CurrentValue = 0,
                RefreshAction = m => { ((GDIProgress) m).CurrentValue = Bar; }
            });
            m_Collection.Add(new GDIRectText()
            {
                Text = "Instruction",
                TextBrush = GdiCommon.WhiteBrush,
                Location = m_PtArr[0],
                DrawFont = m_Txt12Font
            });
        }

        private void InitText()
        {
            for (int i = 1; i < 9; i++)
            {
                m_Collection.Add(new GDIRectText()
                {
                    Text =
                        i > 6
                            ? GlobleParam.ResManager.GetString("String17" + (i - 5))
                            : GlobleParam.ResManager.GetString("String17" + i),
                    DrawFont = m_Txt12Font,
                    TextBrush = (i == 3 || i == 8) ? GdiCommon.MediumGreyBrush : GdiCommon.WhiteBrush,
                    TextFormat = (i == 3 || i == 8) ? GdiCommon.LeftFormat : GdiCommon.CenterFormat,
                    OutLineRectangle = m_RectArr[i],
                    BackColorVisible = false
                });
            }
        }

        private void InitValueText()
        {
            for (int i = 9; i <= 14; i++)
            {
                m_Collection.Add(new GDIRectText()
                {
                    Text = "",
                    Tag = i,
                    BackColorVisible = false,
                    NeedDarwOutline = false,
                    TextBrush = GdiCommon.MediumGreyBrush,
                    RefreshAction = o => RefreshText((GDIRectText) o),
                    OutLineRectangle = m_RectArr[i],
                    TextFormat = GdiCommon.CenterFormat
                });
            }
        }

        private void InitButton()
        {
            m_Button = new List<GDIButton>
            {
                ButtonFactory.CreateButton(new Rectangle(120, 355, 97, 62),
                    GlobleParam.ResManagerText.GetString("Button0005"),
                    btn =>
                    {
                        btn.Tag = new object[]
                        {"Filling Test Active", GetShadowTextButtonBehavier(btn), GetNormalButtonBehavierStrategy(btn)};

                        btn.RefreshAction = o => RerershButton((GDIButton) o);
                    }),
                ButtonFactory.CreateButton(new Rectangle(580, 355, 97, 62),
                    GlobleParam.ResManagerText.GetString("Button0005"),
                    btn =>
                    {
                        btn.Tag = new object[]
                        {"Leakage Test Active", GetShadowTextButtonBehavier(btn), GetNormalButtonBehavierStrategy(btn)};

                        btn.RefreshAction = o => RerershButton((GDIButton) o);
                    })
            };
        }

        private void RefreshText(GDIRectText item)
        {
            var tag = (int)item.Tag;
            if (tag == 9)
            {
                item.Text = string.Format("{0}h {1}m {2}s", m_Tiem1.Hour, m_Tiem1.Minute, m_Tiem1.Second);
            }
            else if (tag == 10)
            {
                item.Text = Result1.ToString("F2");
            }
            else if (tag == 11)
            {
                item.Text = string.Format("{0}h {1}m {2}s", m_Tiem2.Hour, m_Tiem2.Minute, m_Tiem2.Second);
            }
            else if (tag == 12 || tag == 13)
            {
                item.Text = tag == 12 ? string.Format("{0}h {1}m {2}s", m_Tiem2.Hour, m_Tiem2.Minute, m_Tiem2.Second) : Result2.ToString("F2");
            }
            else
            {
                item.Text = Bar.ToString("F2") + "bar";
            }

        }

        private void RerershButton(GDIButton item)
        {
            var name = item.Tag as object[];
            if (name.Length < 2)
            {
                return;
            }
            var tmpActive = BoolList[GlobleParam.Instance.FindInBoolIndex(name[0].ToString())];
            item.BtnBehavierStrategy = tmpActive ? (IBtnBehavierStrategy)name[2] : (IBtnBehavierStrategy)name[1];
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

        public override void paint(Graphics g)
        {
            g.DrawImage(m_Bitmap, m_RectArr[0]);
            m_Collection.ForEach(e => e.OnPaint(g));
            m_Button.ForEach(e => e.OnPaint(g));
            //g.DrawString("Instruction", txt12Font, GdiCommon.whiteBrush, ptArr[0]);

        }
    }
}