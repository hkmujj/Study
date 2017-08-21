using System;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using CommonUtil.Util;

namespace Engine.TCMS.HXD3D.CommonControl
{
    public class HXD3DBlueButton : GDIButton
    {
        private bool m_IsSelected;
        private Brush m_SelectedBackgroundBrush;
        private Brush m_NormalBackgroundBrush;

        public bool IsSelected
        {
            get { return m_IsSelected; }
            set
            {
                m_IsSelected = value;
                UpdateBackBrush();
            }
        }

        public Brush SelectedBackgroundBrush
        {
            get { return m_SelectedBackgroundBrush; }
            set
            {
                m_SelectedBackgroundBrush = value;
                UpdateBackBrush();
            }
        }

        public Brush NormalBackgroundBrush
        {
            get { return m_NormalBackgroundBrush; }
            set
            {
                m_NormalBackgroundBrush = value;
                UpdateBackBrush();
            }
        }

        internal class HXD3DButtonStrategy : BtnBehavierNormalStrategy
        {
            private Rectangle m_InnerOutline;

            /// <summary>
            /// </summary>
            /// <param name="btn"></param>
            public HXD3DButtonStrategy(HXD3DBlueButton btn) : base(btn)
            {
                btn.OutLineChanged += (sender, args) => UpdateInneroutline();

                UpdateInneroutline();
            }

            private void UpdateInneroutline()
            {
                m_InnerOutline = Rectangle.Inflate(Control.OutLineRectangle, -3, -3);
            }

            /// <summary>鼠标按下后弹起</summary>
            /// <param name="point"></param>
            public override bool OnMouseUp(Point point)
            {
                if (!Control.Visible || !Control.IsEnable)
                {
                    return false;
                }

                if (Control.Contains(point))
                {
                    if (Control.CurrentMouseState == MouseState.MouseDown)
                    {
                        HandleUtil.OnHandle(Control.ButtonClickEvent, Control, EventArgs.Empty);
                    }

                    HandleUtil.OnHandle(Control.ButtonUpEvent, Control, EventArgs.Empty);

                    Control.OnButtonUp();

                    return true;
                }

                Control.OnButtonUp();

                return false;
            }

            /// <summary>OnDraw</summary>
            /// <param name="g"></param>
            public override void OnDraw(Graphics g)
            {
                if (Control.Visible)
                {
                    base.OnDraw(g);
                    g.DrawRectangle(Control.CurrentMouseState == MouseState.MouseUp ? Pens.White : Pens.Gray,
                        m_InnerOutline);
                }
            }
        }

        public HXD3DBlueButton()
        {
            BtnBehavierStrategy = new HXD3DButtonStrategy(this);
            NormalBackgroundBrush = null;
            SelectedBackgroundBrush = Brushes.Blue;
            TextControl.BkColor = Color.Blue;
            IsSelected = true;
            TextControl.TextColor = Color.White;
            TextControl.TextFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
        }

        private void UpdateBackBrush()
        {
            TextControl.BKBrush = IsSelected ? SelectedBackgroundBrush : NormalBackgroundBrush;
        }
    }
}