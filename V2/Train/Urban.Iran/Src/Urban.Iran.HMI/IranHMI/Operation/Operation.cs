using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;
using Urban.Iran.HMI.Controls;

namespace Urban.Iran.HMI.Operation
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Operation : HMIBase
    {
        private Bitmap m_OperBkBmp;
        private Bitmap[] m_BmpArr;
        private Rectangle[] m_BmpRectArr;
        private Rectangle m_BkRect;
        private Rectangle[] m_RectArr;
        private Rectangle m_CurPersentRect;
        private Matrix m_Matrix;
        private Point m_CenterPoint;
        private bool m_IsFirst;
        private float m_CurMark;
        private float m_CurLimitMark;
        private FjButtonEx m_Button;
        private int m_PullEffort;
        private int m_BrakeEffort;
        private List<CommonInnerControlBase> m_Collection;

        public override string GetInfo()
        {
            return "Operation";
        }

        protected override bool Initalize()
        {

            m_OperBkBmp = new Bitmap(RecPath + "\\frame/Operation.jpg");
            m_BmpArr = new[]
                     {
                         new Bitmap(RecPath + "\\frame/velocity.jpg"),
                         new Bitmap(RecPath + "\\frame/limitVelocity.jpg")
                     };
            m_BmpRectArr = new[]
                         {
                             new Rectangle(391, 325, 12, 85),
                             new Rectangle(388, 445, 18, 17)
                         };

            m_CurPersentRect = new Rectangle(602, 0, 30, 119);
            m_BkRect = new Rectangle(0, 140, 800, 328);
            m_RectArr = new[]
                      {
                          new Rectangle(26, 220, 95, 59),
                          new Rectangle(26, 300, 95, 59),
                          new Rectangle(26, 380, 95, 59),
                          new Rectangle(144, 220, 95, 59),
                          new Rectangle(144, 300, 95, 59),
                          new Rectangle(144, 380, 95, 59),
                          new Rectangle(545, 170, 140, 20),
                          new Rectangle(730, 142, 69, 46),
                          new Rectangle(590, 445, 50, 20)

                      };
            m_Collection = new List<CommonInnerControlBase>
            {
                new GDITwoOutLineRec(2)
                {
                    Text = "",
                    OutLineRectangle = new Rectangle(26, 220, 95, 59),
                    BackColorVisible = false,
                    Tag = new[]
                    {
                        "TRB", "Wash", "ATO", "ATP", "Manual", "Towing", "Towed"
                    },
                    RefreshAction = o => RefreshModel((GDITwoOutLineRec) o),
                    FillOutRectangle = true,
                    BkColor = Color.Yellow,
                    OutLinePen = GdiCommon.MediumGreyPen,
                    TextFormat = GdiCommon.CenterFormat
                },
                new GDITwoOutLineRec(2)
                {
                    Text = "",
                    OutLineRectangle = new Rectangle(26, 380, 95, 59),
                    BackColorVisible = false,
                    Tag = new Tuple<string, string, string[]>(
                        "Slip/Slide(黄)",
                        "Heavy Slip/Slide(红）",
                        new[] {"No Slip/Slide", "Slip/Slide", "Heavy Slip/Slide"}),
                    FillOutRectangle = true,
                    RefreshAction = o => RefreshSkip((GDIRectText) o),
                    OutLinePen = GdiCommon.MediumGreyPen,
                    TextFormat = GdiCommon.CenterFormat
                },
                new GDITwoOutLineRec(2)
                {
                    Text = "",
                    OutLineRectangle = new Rectangle(144, 220, 95, 59),
                    BackColorVisible = false,
                    Tag = new Tuple<string, string[]>(
                        "Emergency brake(红)",
                        new[] {"No Em. brake", "Emergency brake"}),
                    FillOutRectangle = true,
                    RefreshAction = o => RefreshItem((GDIRectText) o),
                    BkColor = Color.Red,
                    OutLinePen = GdiCommon.MediumGreyPen,
                    TextFormat = GdiCommon.CenterFormat
                },
                new GDITwoOutLineRec(2)
                {
                    Text = "",
                    OutLineRectangle = new Rectangle(144, 300, 95, 59),
                    BackColorVisible = false,
                    Tag = new Tuple<string, string[]>(
                        "Traction Blok（红)",
                        new[] {"No Traction Block", "Traction Block"}),
                    FillOutRectangle = true,
                    RefreshAction = o => RefreshItem((GDIRectText) o),
                    BkColor = Color.Red,
                    OutLinePen = GdiCommon.MediumGreyPen,
                    TextFormat = GdiCommon.CenterFormat
                },
                new GDITwoOutLineRec(2)
                {
                    Text = "",
                    OutLineRectangle = new Rectangle(144, 380, 95, 59),
                    BackColorVisible = false,
                    Tag = new Tuple<string, string[]>(
                        "Ready to dirve(红)",
                        new[] {"Ready to dirve", "Not Ready to dirve"}),
                    FillOutRectangle = true,
                    RefreshAction = o => RefreshItem((GDIRectText) o),
                    BkColor = Color.Red,
                    OutLinePen = GdiCommon.MediumGreyPen,
                    TextFormat = GdiCommon.CenterFormat
                },
                new GDITwoOutLineRec(2)
                {
                    Text = "",
                    Tag = "Right doors released",
                    BackColorVisible = false,
                    RefreshAction = o => RefreshText((GDIRectText) o),
                    OutLineRectangle = new Rectangle(680, 142, 95, 59),
                    TextBrush = GdiCommon.MediumGreyBrush,
                    OutLinePen = GdiCommon.MediumGreyPen,
                    TextFormat = GdiCommon.CenterFormat
                },
                new GDITwoOutLineRec(2)
                {
                    Text = "",
                    Tag ="Left doors released",
                    BackColorVisible = false,
                    RefreshAction = o => RefreshText((GDIRectText) o),
                    OutLineRectangle = new Rectangle(26, 142, 95, 59),
                    TextBrush = GdiCommon.MediumGreyBrush,
                    OutLinePen = GdiCommon.MediumGreyPen,
                    TextFormat = GdiCommon.CenterFormat
                }
            };
            m_IsFirst = true;
            m_CenterPoint = new Point(397, 325);
            m_CurMark = 0.0f;
            m_CurLimitMark = 0.0f;
            m_PullEffort = 0;
            m_BrakeEffort = 0;
            //m_Button = new FJButtonE(GlobleParam.ResManager.GetString("String31"), GdiCommon.BtnBkBitmap);
            m_Button = new FjButtonEx(49, "Legend", new Rectangle(701, 402, 97, 62));
            m_Button.MouseDown += m_Button_MouseDown;
            return true;
        }

        public override bool mouseDown(Point point)
        {
            if (m_Button.IsVisible(point))
            {
                m_Button.OnMouseDown(point);
            }
            return true;
        }

        void m_Button_MouseDown(FjButtonEx btnSender, Point pt)
        {
            //append_postCmd(CmdType.ChangePage, btnSender.BtnIndex, 0, 0);
            ChangedPage((IranViewIndex)btnSender.BtnIndex);
        }

        #region  Refersh

        private void RefreshText(GDIRectText item)
        {
            var names = item.Tag as string;
            if (names.Length < 1)
            {
                return;
            }
            var tmp1 = BoolList[GlobleParam.Instance.FindInBoolIndex(names)];
            //var tmp2 = BoolList[GlobleParam.Instance.FindInBoolIndex(names[1])];
            item.Visible = tmp1;
            item.Text = names;
        }

        private void RefreshItem(GDIRectText item)
        {
            var names = item.Tag as Tuple<string, string[]>;
            var tmp = BoolList[GlobleParam.Instance.FindInBoolIndex(names.Item1)];
            item.BackColorVisible = tmp;
            item.Text = tmp ? names.Item2[1] : names.Item2[0];
            item.TextBrush = tmp ? GdiCommon.BlackBrush : GdiCommon.MediumGreyBrush;
        }

        private void RefreshSkip(GDIRectText item)
        {
            var names = item.Tag as Tuple<string, string, string[]>;
            item.BackColorVisible = true;
            item.TextBrush = GdiCommon.MediumGreyBrush;
            if (BoolList[GlobleParam.Instance.FindInBoolIndex(names.Item1)])
            {
                item.Text = names.Item3[1];
                item.BkColor = Color.Yellow;
                item.TextBrush = GdiCommon.BlackBrush;
            }
            else if (BoolList[GlobleParam.Instance.FindInBoolIndex(names.Item2)])
            {
                item.Text = names.Item3[2];
                item.BkColor = Color.Red;
                item.TextBrush = GdiCommon.BlackBrush;
            }
            else
            {
                item.Text = names.Item3[0];
                item.BackColorVisible = false;
            }
        }

        private void RefreshModel(GDIRectText item)
        {
            var names = item.Tag as string[];
            if (names.Length < 1)
            {
                return;
            }
            item.Text = names.FirstOrDefault(name => BoolList[GlobleParam.Instance.FindInBoolIndex(name)]);

            if (!string.IsNullOrEmpty(item.Text))
            {
                item.BackColorVisible = !item.Text.Equals("ATO") && !item.Text.Equals("ATP");
                item.TextBrush = item.BackColorVisible ? GdiCommon.BlackBrush : GdiCommon.MediumGreyBrush;
            }
            else
            {
                item.BackColorVisible = false;
            }
        }

        #endregion

        public override void paint(Graphics g)
        {
            GlobleParam.Instance.CurrentIranViewIndex = (IranViewIndex)15;
            GdiCommon.m_StrFormat.Alignment = StringAlignment.Center;
            GdiCommon.m_StrFormat.LineAlignment = StringAlignment.Center;
            g.DrawImage(m_OperBkBmp, m_BkRect);

            m_Collection.ForEach(e => e.OnPaint(g));

            g.DrawString(GlobleParam.ResManager.GetString("String30"),
                GdiCommon.Txt12Font,
                GdiCommon.MediumGreyBrush,
                m_RectArr[6],
                GdiCommon.m_StrFormat);

            m_Button.OnDraw(g);

            m_PullEffort = Convert.ToInt32(FloatList[4]);
            m_BrakeEffort = Convert.ToInt32(FloatList[5]);

            DrawTE_BE(g);

            if (m_IsFirst)
            {
                m_Matrix = g.Transform;
                m_IsFirst = false;
            }
            m_CurMark = DrawVelocityMark(g, 0, m_CurMark, Math.Max(0f, Math.Min(FloatList[1], 100f)));
            m_CurLimitMark = DrawVelocityMark(g, 1, m_CurLimitMark, Math.Max(0f, Math.Min(FloatList[2], 100f)));

        }

        private void DrawTE_BE(Graphics g)
        {
            var persent = (m_PullEffort <= 0) ? -m_BrakeEffort : m_PullEffort;

            var height = (Math.Abs(persent * 119)) / 100;
            m_CurPersentRect.Y = (persent > 0) ? 317 - height : 318;
            m_CurPersentRect.Height = height;
            g.FillRectangle(GdiCommon.OceanBlueBrush, m_CurPersentRect);
            g.DrawString(persent + "%", GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, m_RectArr[8], GdiCommon.m_StrFormat);
        }

        private float DrawVelocityMark(Graphics g, int index, float curVelocity, float targetVelocity)
        {
            //var temp = (curVelocity < 50) ? 0.67f : 0.5f;
            //if (Math.Abs(curVelocity - targetVelocity) < 1f)
            //{
            //    curVelocity = targetVelocity;
            //}
            //else if (curVelocity > targetVelocity)
            //{
            //    curVelocity -= temp;
            //}
            //else if (curVelocity < targetVelocity)
            //{
            //    curVelocity += temp;
            //}

            m_Matrix.RotateAt(CancluateAngle(targetVelocity), m_CenterPoint);
            g.Transform = m_Matrix;
            g.DrawImage(m_BmpArr[index], m_BmpRectArr[index]);
            m_Matrix.Reset();
            g.Transform = m_Matrix;

            return curVelocity;
        }

        private float CancluateAngle(float mark)
        {
            return mark * 271 / 100.0f + 44;
        }
    }
}