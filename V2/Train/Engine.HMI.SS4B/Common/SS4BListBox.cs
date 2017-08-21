using CommonUtil.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SS4B_TMS.Common
{
    public enum ListBoxStatus
    {
        Normal,
        Downing
    }

    public enum BoxDirection
    {
        Center,
        Up,
        Down
    }

    public enum BoxType
    {
        Text,
        Box,
    }

    public class SS4BListBox
    {
        private Rectangle m_OutRectangle;
        private IList<string> m_Texts;

        public event Action<string> SelectChanged;

        public event Action<int, int> SelectIndexChanged;

        public BoxType Type { get; set; }

        public SS4BListBox()
        {
            Location = new Point(0, 0);
            Size = new Size(0, 0);
            DownPicSize = new Size(0, 0);
            OutRectangle = new Rectangle(Location, Size);
            TextFormat = new StringFormat()
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };
            m_GdiRect = new List<GDIRectText>();
            IsVisible = true;
            IsEnable = false;
            TextFont = new Font("宋体", 16);
            Type = BoxType.Box;
        }

        public object Tag { get; set; }

        public void ResetStatus()
        {
            Status = ListBoxStatus.Normal;
        }

        public void Select(int row, int y)
        {
            if (row == Row && y == Column)
            {
                IsSelect = true;
                // m_GdiRect[0].BKBrush = SelectBrush;
            }
            else
            {
                IsSelect = false;
            }
        }

        public bool DownBox(Point point)
        {
            if (Status == ListBoxStatus.Downing && Type != BoxType.Text)
            {
                var tmp = m_GdiRect.Where(w => w.OutLineRectangle.Contains(point)).ToList();
                if (tmp.Count() != 0)
                {
                    var s = m_GdiRect.FindIndex(f => tmp[0].Equals(f));

                    Text = m_GdiRect[s].Text;
                }
                IsSelect = true;
                Status = ListBoxStatus.Normal;
                OnSelectIndexChanged(Row, Column);
                return true;
            }
            return false;
        }

        public bool MouseDown(Point point)
        {
            if (m_OutRectangle.Contains(point) && Status == ListBoxStatus.Normal)
            {
                IsDown = true;
                Status = Type == BoxType.Text ? ListBoxStatus.Normal : ListBoxStatus.Downing;
                IsSelect = true;
                var s = m_GdiRect.FindIndex(f => Text.Equals(f.Text));
                m_GdiRect.ForEach(f =>
                {
                    f.TextBrush = TextBrush;
                    f.BKBrush = BackColor;
                });
                if (s != -1)
                {
                    m_GdiRect[s].BKBrush = SelectBrush;
                    m_GdiRect[s].TextBrush = SelectTextBrush;
                }
                OnSelectIndexChanged(Row, Column);
                return true;
            }
            else
            {
                m_IsDownBox = false;
                IsSelect = false;
            }
            return false;
        }

        public void MouseUp(Point point)
        {
            if (m_OutRectangle.Contains(point))
            {
                IsDown = false;
            }
        }

        private bool m_IsDownBox = false;
        public List<Rectangle> BoxRec { get; private set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public ListBoxStatus Status { get; private set; }
        public BoxDirection Direction { get; set; }

        public IList<string> Texts
        {
            get { return m_Texts; }
            set
            {
                m_Texts = value;
                InitBoxRec();
            }
        }

        public Brush SelectBrush { get; set; }

        public bool IsSelect
        {
            get { return m_IsSelect; }
            private set
            {
                m_IsSelect = value;
            }
        }

        public string Text
        {
            get { return m_Text; }
            private set
            {
                if (m_Text == value)
                {
                    return;
                }
                m_Text = value;
                OnSelectChanged(m_Text);
            }
        }

        public void SetText(string text)
        {
            Text = text;
        }

        public bool IsDown { get; private set; }
        public bool IsEnable { get; set; }
        public bool IsVisible { get; set; }
        public Point Location { get; private set; }
        public Font TextFont { get; set; }
        public Size Size { get; private set; }
        public Size DownPicSize { get; set; }
        public Image Image { get; set; }
        public Brush TextBrush { get; set; }
        public Brush SelectTextBrush { get; set; }
        public Brush BackColor { get; set; }
        public Brush EnableBrush { get; set; }
        public StringFormat TextFormat { get; set; }
        private List<GDIRectText> m_GdiRect;
        private string m_Text;
        private Rectangle imageRec = new Rectangle();
        private bool m_IsSelect;
        private Rectangle m_SelectRectangle = new Rectangle();

        public Rectangle OutRectangle
        {
            get { return m_OutRectangle; }
            set
            {
                m_OutRectangle = value;
                Location = m_OutRectangle.Location;
                Size = m_OutRectangle.Size;
                imageRec.X = m_OutRectangle.X + m_OutRectangle.Width - DownPicSize.Width;
                imageRec.Y = m_OutRectangle.Y + ((m_OutRectangle.Size.Height - DownPicSize.Height) / 2);
                imageRec.Size = DownPicSize;
                m_SelectRectangle.X = m_OutRectangle.X + 2;
                m_SelectRectangle.Y = m_OutRectangle.Y + 2;
                m_SelectRectangle.Width = m_OutRectangle.Width - 4 - DownPicSize.Width;
                m_SelectRectangle.Height = m_OutRectangle.Height - 4;
                InitBoxRec();
            }
        }

        public void OnDraw(Graphics g)
        {
            if (IsVisible)
            {
                g.FillRectangle(IsEnable ? EnableBrush : BackColor, OutRectangle);
                if (IsSelect && !IsEnable)
                {
                    g.FillRectangle(SelectBrush, m_SelectRectangle);
                }
                if (Image != null)
                {
                    g.DrawImage(Image, imageRec);
                }
                g.DrawString(Text, TextFont, IsEnable ? TextBrush : IsSelect ? SelectTextBrush : TextBrush, OutRectangle, TextFormat);
            }
        }

        public void OnDrawBox(Graphics g)
        {
            if (Status == ListBoxStatus.Downing && Type == BoxType.Box)
            {
                DrawBox(g);
            }
        }

        private void InitBoxRec()
        {
            if (Texts == null)
            {
                return;
            }
            if (Texts.Count != 0)
            {
                Text = Texts[0];
            }
            m_GdiRect.Clear();
            switch (Direction)
            {
                case BoxDirection.Center:
                    for (int i = 0; i < Texts.Count; i++)
                    {
                    }
                    break;

                case BoxDirection.Up:
                    for (int i = 0; i < Texts.Count; i++)
                    {
                        m_GdiRect.Add(new GDIRectText()
                        {
                            Text = Texts[i],
                            OutLineRectangle = new Rectangle(Location.X, Location.Y - (int)(TextFont.Size * (Texts.Count - i)), Size.Width, Size.Height),
                            TextFormat = TextFormat,
                            TextBrush = TextBrush,
                            DrawFont = TextFont,
                            BackColorVisible = true,
                            BKBrush = BackColor,
                            NeedDarwOutline = false,
                        });
                    }
                    break;

                case BoxDirection.Down:
                    for (int i = 0; i < Texts.Count; i++)
                    {
                        m_GdiRect.Add(new GDIRectText()
                        {
                            Text = Texts[i],
                            OutLineRectangle = new Rectangle(Location.X, Location.Y + (int)((TextFont.Size + 5) * i), Size.Width, (int)TextFont.Size + 5),
                            TextFormat = TextFormat,
                            TextBrush = TextBrush,
                            BackColorVisible = true,
                            BKBrush = BackColor,
                            DrawFont = TextFont,
                            NeedDarwOutline = false,
                        });
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DrawBox(Graphics g)
        {
            m_GdiRect.ForEach(f => f.OnDraw(g));
        }

        protected virtual void OnSelectChanged(string obj)
        {
            SelectChanged?.Invoke(obj);
        }

        protected virtual void OnSelectIndexChanged(int arg1, int arg2)
        {
            SelectIndexChanged?.Invoke(arg1, arg2);
        }
    }
}